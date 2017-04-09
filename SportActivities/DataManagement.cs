using GeoAPI.CoordinateSystems.Transformations;
using GeoAPI.Geometries;
using NetTopologySuite.Geometries;
using NetTopologySuite.Utilities;
using Npgsql;
using ProjNet.CoordinateSystems;
using ProjNet.CoordinateSystems.Transformations;
using SharpMap.Data;
using SharpMap.Layers;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;

namespace SportActivities
{
    public class DataManagement
    {
        public CoordinateTransformationFactory ctFact { get; set; }
        public ICoordinateTransformation transfCoord { get; set; }
        public ICoordinateTransformation reverseTransfCoord { get; set; }
        public  ConnectionStringSettingsCollection connectionStrings { get; set; }
        public string connectionParams;

        public DataManagement()
        {
            ctFact = new CoordinateTransformationFactory();
            transfCoord = ctFact.CreateFromCoordinateSystems(GeographicCoordinateSystem.WGS84, ProjectedCoordinateSystem.WebMercator);
            reverseTransfCoord = ctFact.CreateFromCoordinateSystems(ProjectedCoordinateSystem.WebMercator, GeographicCoordinateSystem.WGS84);

            connectionStrings = ConfigurationManager.ConnectionStrings;
            connectionParams = connectionStrings["PostgreSQL"].ConnectionString;
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

        public List<string> getAllLayerAttributes(string layer)
        {
            List<string> attributes;

            using (NpgsqlConnection conn = new NpgsqlConnection(connectionParams))
            {
                conn.Open();

                using (NpgsqlCommand command = new NpgsqlCommand("select column_name from information_schema.columns where table_name='" + layer + "';", conn))
                {
                    NpgsqlDataReader reader = command.ExecuteReader();
                    attributes = new List<string>();
                    while (reader.Read())
                    {
                        attributes.Add(reader[0].ToString());
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

                using (NpgsqlCommand command = new NpgsqlCommand("select * from geometry_columns where f_table_schema = 'public';", conn))
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



        public FeatureDataSet getFeatureDataSet(LayerCollection layers, Coordinate coords)
        {
            IGeometry geometry = CreateGeometry(coords);
            FeatureDataSet fds = new FeatureDataSet();
            
            foreach(VectorLayer layer in layers)
                if (layer.IsQueryEnabled)
                    layer.ExecuteIntersectionQuery(geometry.EnvelopeInternal, fds);

            return fds;
        }

        private IGeometry getGeometry(Coordinate[] coordinates)
        {
            Coordinate[] transformedCoordinates = new Coordinate[coordinates.Length];

            for (int i = 0; i < coordinates.Length - 1; i++)
            {
                Coordinate transformed = reverseTransfCoord.MathTransform.Transform(coordinates[i]);
                transformedCoordinates[i] = transformed;
            }
            transformedCoordinates[coordinates.Length - 1] = transformedCoordinates.First();

            LinearRing lr = new LinearRing(transformedCoordinates);
            return new Polygon(lr); ;
        }

        private IGeometry CreateGeometry(Coordinate coords)
        {
            double circleSize = 500;
            coords.X -= circleSize / 2;
            coords.Y -= circleSize / 2;

            GeometricShapeFactory gf = new GeometricShapeFactory();
            gf.Base = coords;
            gf.Centre = coords;
            gf.Size = circleSize;
            gf.Width = circleSize;
            gf.Height = circleSize;

            IPolygon circle = gf.CreateCircle();

            return getGeometry(circle.Coordinates);
        }

    }
}
