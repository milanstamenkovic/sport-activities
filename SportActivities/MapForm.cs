using GeoAPI.CoordinateSystems.Transformations;
using Npgsql;
using ProjNet.CoordinateSystems.Transformations; 
using SharpMap.Layers;
using System;
using System.Collections.Generic; 
using System.Configuration; 
using System.Drawing; 
using System.Windows.Forms;
using GeoAPI.CoordinateSystems;
using ProjNet.CoordinateSystems;

namespace SportActivities
{
    public partial class MapForm : Form
    {
        private ConnectionStringSettingsCollection connectionStrings;
        private Dictionary<string, VectorLayer> layers;
        private List<LayerRecord> layerRecords;

        private String connectionParams;

        private CoordinateTransformationFactory _ctFact;
        public ICoordinateTransformation transfCoord;
        public ICoordinateTransformation reverseTransfCoord;

        public MapForm()
        {
            InitializeComponent();

            connectionStrings = ConfigurationManager.ConnectionStrings;

            connectionParams = connectionStrings["PostgreSQL"].ConnectionString;

            layers = new Dictionary<string, VectorLayer>();
            layerRecords = GetAllLayers();

            _ctFact = new CoordinateTransformationFactory();
            transfCoord = _ctFact.CreateFromCoordinateSystems(GeographicCoordinateSystem.WGS84, ProjectedCoordinateSystem.WebMercator);
            reverseTransfCoord = _ctFact.CreateFromCoordinateSystems(ProjectedCoordinateSystem.WebMercator, GeographicCoordinateSystem.WGS84);

            populateCollection();

            AddToTreeView();

            mapBox.Map.BackgroundLayer.Add(CreateBackgroundLayer());

            mapBox.Map.ZoomToExtents();

            mapBox.Refresh();
            mapBox.EnableShiftButtonDragRectangleZoom = true;
            mapBox.ActiveTool = SharpMap.Forms.MapBox.Tools.Pan;
        }

        private void populateCollection()
        {
            foreach (LayerRecord record in layerRecords)
            {
                if (!record.TableName.Equals("putevi") && !record.TableName.Equals("zgrade"))
                {
                    VectorLayer layer = new VectorLayer(record.TableName);
                    layer.DataSource = new SharpMap.Data.Providers.PostGIS(connectionParams, record.TableName, "gid");
                    layer.CoordinateTransformation = transfCoord;
                    layer.ReverseCoordinateTransformation = reverseTransfCoord;

                    layers.Add(layer.LayerName, layer);
                }
            }
        }

        private void AddToTreeView()
        {
            TreeNode[] nodes = new TreeNode[layers.Count];

            int i = 0;
            foreach(VectorLayer layer in layers.Values)
            {
                nodes[i] = new TreeNode(layer.LayerName);
                nodes[i++].Checked = false;
            }

            layersTreeView.Nodes.AddRange(nodes);
        }

        public ILayer CreateBackgroundLayer()
        {
            return new TileLayer(new BruTile.Web.OsmTileSource(), "OSM");
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

        private void mapBox_MouseMove(GeoAPI.Geometries.Coordinate worldPos, MouseEventArgs imagePos)
        {
            IProjectedCoordinateSystem utmProj = createUtmProjection(34);
            IGeographicCoordinateSystem geoCs = utmProj.GeographicCoordinateSystem;
            CoordinateTransformationFactory ctFac = new CoordinateTransformationFactory();
            ICoordinateTransformation transform = ctFac.CreateFromCoordinateSystems(geoCs, utmProj);
            double[] coordsGeo = new double[2];
            double[] coordsUtm;
            coordsGeo[0] = worldPos.X;
            coordsGeo[1] = worldPos.Y;
            coordsUtm = transform.MathTransform.Transform(coordsGeo);
            worldPos.X = coordsUtm[0];
            worldPos.Y = coordsUtm[1];

            labelMouseCoords.Text = worldPos.X + ", " + worldPos.Y;
        } 

        private IProjectedCoordinateSystem createUtmProjection(int utmZone)
        {
            CoordinateSystemFactory cFac = new CoordinateSystemFactory();
            IEllipsoid elipsoid = cFac.CreateFlattenedSphere("WGS 84", 6378137, 298.257, LinearUnit.Metre);
            IHorizontalDatum datum = cFac.CreateHorizontalDatum("WGS_1984", DatumType.HD_Geocentric, elipsoid, null);
            IGeographicCoordinateSystem gcs = cFac.CreateGeographicCoordinateSystem("WGS 84", AngularUnit.Degrees, datum,
                PrimeMeridian.Greenwich, new AxisInfo("Lon", AxisOrientationEnum.East), new AxisInfo("Lat", AxisOrientationEnum.North));

            List<ProjectionParameter> parameters = new List<ProjectionParameter>();

            parameters.Add(new ProjectionParameter("latitude_of_origin", 0.0));
            parameters.Add(new ProjectionParameter("central_meridian", -183 + 6 * utmZone));
            parameters.Add(new ProjectionParameter("scale_factor", 0.9996));
            parameters.Add(new ProjectionParameter("false_easting", 500000));
            parameters.Add(new ProjectionParameter("false_northing", 0.0));
            IProjection projection = cFac.CreateProjection("Transverse Mercator", "Transverse Mercator", parameters);


            return cFac.CreateProjectedCoordinateSystem("WGS 84 / UTM zone " + utmZone + "N", gcs, projection, LinearUnit.Metre,
                new AxisInfo("East", AxisOrientationEnum.East), new AxisInfo("North", AxisOrientationEnum.North));
        }

        private void layersTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void layersTreeView_ItemDrag(object sender, ItemDragEventArgs e)
        {
            DoDragDrop(e.Item, DragDropEffects.Move);
        }

        private void layersTreeView_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if(e.Action != TreeViewAction.Unknown)
            {
                if (e.Node.Checked)
                    renderActiveLayers();
                else
                {
                    mapBox.Map.Layers.Remove(layers[e.Node.Text]);
                    mapBox.Refresh();
                }

            }
        }

        private void layersTreeView_DragDrop(object sender, DragEventArgs e)
        {
            TreeNode NewNode;

            if (e.Data.GetDataPresent("System.Windows.Forms.TreeNode", false))
            {
                Point pt = ((TreeView)sender).PointToClient(new Point(e.X, e.Y));
                TreeNode PreviousNode = layersTreeView.GetNodeAt(pt);
                int index = -1;
                if (PreviousNode == null)
                    index = layersTreeView.Nodes.Count;
                else
                    index = layersTreeView.Nodes.IndexOf(PreviousNode) + 1;

                NewNode = (TreeNode)e.Data.GetData("System.Windows.Forms.TreeNode");

                layersTreeView.Nodes.Insert(index, (TreeNode)NewNode.Clone());
                NewNode.Remove();

                renderActiveLayers();
            }
        }

        private void layersTreeView_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void renderActiveLayers()
        {
            mapBox.Map.Layers.Clear();

            foreach(TreeNode layerNode in layersTreeView.Nodes)
                if (layerNode.Checked)
                    mapBox.Map.Layers.Add(layers[layerNode.Text]);

            mapBox.Refresh();
        }

        private void mapCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (mapCheckBox.Checked)
                mapBox.Map.BackgroundLayer.Add(CreateBackgroundLayer());
            else
                mapBox.Map.BackgroundLayer.Clear();

            mapBox.Refresh();
            mapBox.Invalidate();
        }

        private void btnShowLabels_Click(object sender, EventArgs e)
        {
            foreach (string attribute in getAllAttributesForLayer("zatvoreni"))
            {
                Console.WriteLine(attribute);
                LabelLayer labelLayer = getLabelLayer();
                labelLayer.CoordinateTransformation = _ctFact.CreateFromCoordinateSystems(ProjNet.CoordinateSystems.GeographicCoordinateSystem.WGS84, ProjectedCoordinateSystem.WebMercator);
                labelLayer.ReverseCoordinateTransformation = _ctFact.CreateFromCoordinateSystems(ProjectedCoordinateSystem.WebMercator, ProjNet.CoordinateSystems.GeographicCoordinateSystem.WGS84);
                mapBox.Map.Layers.Add(labelLayer);
                mapBox.Refresh(); 
            }
        }

        private List<string> getAllAttributesForLayer(string layer)
        {
            List<string> attributes;

            using (NpgsqlConnection conn = new NpgsqlConnection(connectionStrings["PostgreSQL"].ConnectionString))
            {
                conn.Open();

                using (NpgsqlCommand command = new NpgsqlCommand("select column_name from information_schema.columns where table_name='" + layer + "';", conn))
                {
                    NpgsqlDataReader reader = command.ExecuteReader();
                    attributes = new List<string>();
                    while (reader.Read()) {
                        attributes.Add(reader[0].ToString());
                    }
                }
            }

            return attributes;
        }

        private LabelLayer getLabelLayer()
        {
            LabelLayer labelLayer = new LabelLayer("Test label layer");
            labelLayer.DataSource = layers["zatvoreni"].DataSource;
            labelLayer.LabelColumn = "sport";
            labelLayer.Style.CollisionDetection = true;
            labelLayer.Style.CollisionBuffer = new SizeF(20, 20);
            labelLayer.MultipartGeometryBehaviour = LabelLayer.MultipartGeometryBehaviourEnum.Largest;
            labelLayer.Style.Font = new System.Drawing.Font(FontFamily.GenericSansSerif, 10);

            return labelLayer;
        }
    }
}
