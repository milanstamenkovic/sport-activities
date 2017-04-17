using GeoAPI.CoordinateSystems.Transformations;
using GeoAPI.Geometries;
using NetTopologySuite.Geometries;
using NetTopologySuite.Utilities;
using Npgsql;
using ProjNet.CoordinateSystems;
using ProjNet.CoordinateSystems.Transformations;
using SharpMap.Data;
using SharpMap.Data.Providers;
using SharpMap.Layers;
using SportActivities.DataModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Drawing;
using System.Linq;

namespace SportActivities
{
    public class DataManagement
    {
        private static DataManagement instance;

        public CoordinateTransformationFactory ctFact { get; set; }
        public ICoordinateTransformation transfCoord { get; set; }
        public ICoordinateTransformation reverseTransfCoord { get; set; }
        public  ConnectionStringSettingsCollection connectionStrings { get; set; }
        public string connectionParams;

        private DataManagement()
        {
            ctFact = new CoordinateTransformationFactory();
            transfCoord = ctFact.CreateFromCoordinateSystems(GeographicCoordinateSystem.WGS84, ProjectedCoordinateSystem.WebMercator);
            reverseTransfCoord = ctFact.CreateFromCoordinateSystems(ProjectedCoordinateSystem.WebMercator, GeographicCoordinateSystem.WGS84);

            connectionStrings = ConfigurationManager.ConnectionStrings;
            connectionParams = connectionStrings["PostgreSQL"].ConnectionString;
        }

        public static DataManagement Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new DataManagement();
                }
                return instance;
            }
        }

        public LabelLayer createLabelLayer(VectorLayer vectorLayer, string attributeName)
        {
            LabelLayer labelLayer = new LabelLayer(vectorLayer.LayerName);
            labelLayer.DataSource = vectorLayer.DataSource;
            labelLayer.LabelColumn = attributeName;
            labelLayer.Style.CollisionDetection = true;
            labelLayer.Style.CollisionBuffer = new SizeF(20, 20);
            labelLayer.MultipartGeometryBehaviour = LabelLayer.MultipartGeometryBehaviourEnum.Largest;
            labelLayer.Style.Font = new Font(FontFamily.GenericSansSerif, 10);
            labelLayer.CoordinateTransformation = transfCoord;
            labelLayer.ReverseCoordinateTransformation = reverseTransfCoord;

            return labelLayer;
        }

        public List<object> getAllLayerAttributes(string layer)
        {
            List<object> attributes;

            using (NpgsqlConnection conn = new NpgsqlConnection(connectionParams))
            {
                conn.Open();

                using (NpgsqlCommand command = new NpgsqlCommand("select column_name from information_schema.columns where table_name='" + layer + "';", conn))
                {
                    NpgsqlDataReader reader = command.ExecuteReader();
                    attributes = new List<object>();
                    while (reader.Read())
                    {
                        attributes.Add(reader[0]);
                    }
                }
            }

            return attributes;
        }

        public List<LayerRecord> GetAllLayers()
        {
            List<LayerRecord> layers;
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionParams))
            {
                conn.Open();

                using (NpgsqlCommand command = new NpgsqlCommand("select * from geometry_columns where f_table_schema = 'public' and f_geometry_column = 'geom';", conn))
                {

                    NpgsqlDataReader reader = command.ExecuteReader();
                    layers = new List<LayerRecord>();
                    while (reader.Read())
                    {
                        layers.Add(new LayerRecord(reader[1].ToString(), reader[2].ToString(),
                            reader[3].ToString(), (int)reader[4], (int)reader[5], reader[6].ToString()));
                    }
                }
            }

            return layers;
        }

        public FeatureDataSet GetFeatureDataSet(LayerCollection layers, Coordinate coord, double areaSize)
        {
            IGeometry geometry = getGeometryFromPoint(coord, areaSize);
            FeatureDataSet fds = new FeatureDataSet();

            for(int i = 0; i < layers.Count; ++i)
            {
                if(layers[i] is VectorLayer)
                {
                    VectorLayer layer = (VectorLayer)layers[i];
                    if (layer.IsQueryEnabled)
                        layer.ExecuteIntersectionQuery(geometry.EnvelopeInternal, fds);
                }
            }
            return fds;
        }

        private IGeometry getGeometryFromPoint(Coordinate coord, double size)
        {
            coord.X -= size / 2;
            coord.Y -= size / 2;

            GeometricShapeFactory gf = new GeometricShapeFactory();
            gf.Base = coord;
            gf.Centre = coord;
            gf.Size = size;
            gf.Width = size;
            gf.Height = size;

            IPolygon circle = gf.CreateCircle();

            circle.Coordinates[circle.Coordinates.Length - 1] = circle.Coordinates.First();

            LinearRing lr = new LinearRing(circle.Coordinates);
            return new Polygon(lr);
        }

        public VectorLayer GeometryFilter(LayerCollection layers, IGeometry geometry)
        {
            FeatureDataSet fds = new FeatureDataSet();
            VectorLayer resultLayer = createLayer("Geometry layer");
            Collection<IGeometry> geomColl = new Collection<IGeometry>();

            for(int i = 0; i < layers.Count; ++i)
            {
                VectorLayer layer = (VectorLayer)layers[i];
                if (layer.IsQueryEnabled)
                {
                    layer.ExecuteIntersectionQuery(geometry.EnvelopeInternal, fds);

                    foreach (FeatureDataRow fdr in fds.Tables[i].Rows)
                        geomColl.Add(fdr.Geometry);
                }
            }

            resultLayer.DataSource = new GeometryProvider(geomColl);
            return resultLayer;
        }

        public VectorLayer getLayerFromFeatureDataSet(FeatureDataSet fds)
        {
            VectorLayer resultLayer = createLayer("FeatureData layer");
            Collection<IGeometry> geomColl = new Collection<IGeometry>();

            foreach(FeatureDataTable table in fds.Tables)
            {
                foreach (FeatureDataRow row in table.Rows)
                    geomColl.Add(row.Geometry);
            }

            resultLayer.DataSource = new GeometryProvider(geomColl);
            return resultLayer;
        }

        public VectorLayer DefinitionQueryFilter(Query query)
        {
            Collection<IGeometry> geometries = new Collection<IGeometry>();
            VectorLayer resultLayer = createLayer("Query Layer");
            PostGIS provider = new PostGIS(connectionParams, query.TableName, "geom", "gid");

            if (query.Condition != null)
                provider.DefinitionQuery = query.Condition;

            resultLayer.DataSource = provider;
            
            return resultLayer;
        }

        public FeatureDataSet getFeatureDataSetForLayer(VectorLayer layer)
        {
            FeatureDataSet fds = new FeatureDataSet();

            if (layer.IsQueryEnabled)
                layer.ExecuteIntersectionQuery(layer.Envelope, fds);

            return fds;
        }
        
        public VectorLayer createRoutingLayer(Coordinate start, Coordinate end)
        {
            //prepare temp table
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionParams))
            {
                conn.Open();

                using (NpgsqlCommand command = new NpgsqlCommand("drop table if exists temp_route;", conn))
                {
                    command.ExecuteNonQuery();
                }

                using (NpgsqlCommand command = new NpgsqlCommand("CREATE TABLE temp_route" +
                    " AS select * from pgr_fromAtoB('ways'," + start.X + "," + start.Y + "," + end.X + "," + end.Y + ");", conn))
                {
                    command.ExecuteNonQuery();
                }
            }

            //create layer
            VectorLayer layer = new VectorLayer("RouteAtoB");
            var postGisProvider = new PostGIS(connectionParams, "temp_route", "geom", "seq");

            postGisProvider.SRID = 4326;
            layer.DataSource = postGisProvider;

            layer.CoordinateTransformation = transfCoord;
            layer.ReverseCoordinateTransformation = reverseTransfCoord;

            layer.Style.Line = new Pen(Color.IndianRed, 5);

            return layer;
        }

        private VectorLayer createLayer(string name)
        {
            VectorLayer layer = new VectorLayer(name);
            layer.CoordinateTransformation = transfCoord;
            layer.ReverseCoordinateTransformation = reverseTransfCoord;
            return layer;
        }
    }
}
