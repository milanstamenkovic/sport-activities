using GeoAPI.CoordinateSystems.Transformations;
using Npgsql;
using ProjNet.CoordinateSystems.Transformations;
using SharpMap;
using SharpMap.Layers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SportActivities
{
    public partial class MapForm : Form
    {
        public const int MAP_HEIGHT = 400;
        public const int MAP_WIDTH = 300;

        private LayerCollection layers;
        private int zoomLevel;
        private List<LayerRecord> layerRecords;
        private String connectionParams;

        private CoordinateTransformationFactory _ctFact ;
        public ICoordinateTransformation transfCoord;
        public ICoordinateTransformation reverseTransfCoord;
        public MapForm()
        {
            InitializeComponent();

            connectionParams =
                         System.Configuration.ConfigurationManager.
                         ConnectionStrings["PostgreSQL"].ConnectionString;

            layerRecords = GetAllLayers();
            layers = new LayerCollection();

            _ctFact = new CoordinateTransformationFactory();
            transfCoord = _ctFact.CreateFromCoordinateSystems(ProjNet.CoordinateSystems.GeographicCoordinateSystem.WGS84, ProjNet.CoordinateSystems.ProjectedCoordinateSystem.WebMercator);
            reverseTransfCoord = _ctFact.CreateFromCoordinateSystems(ProjNet.CoordinateSystems.ProjectedCoordinateSystem.WebMercator, ProjNet.CoordinateSystems.GeographicCoordinateSystem.WGS84);


            foreach (LayerRecord record in layerRecords)
            {
                if (!record.TableName.Equals("putevi") && !record.TableName.Equals("zgrade"))
                {
                    VectorLayer layer = new VectorLayer(record.TableName);
                    layer.DataSource = new SharpMap.Data.Providers.PostGIS(connectionParams, record.TableName, "gid");
                    layer.CoordinateTransformation = transfCoord;
                    layer.ReverseCoordinateTransformation = reverseTransfCoord;
                    layers.Add(layer);
                }
            }

            AddToTreeView();

            mapBox.Map.Layers.AddCollection(layers);
            mapBox.Map.BackgroundLayer.Add(CreateBackgroundLayer());

            mapBox.Map.ZoomToExtents();
            mapBox.Refresh();
            mapBox.EnableShiftButtonDragRectangleZoom = true;
            mapBox.ActiveTool = SharpMap.Forms.MapBox.Tools.Pan;
        }

        private void AddToTreeView()
        {
            TreeNode[] nodes = new TreeNode[layers.Count];

            for(int i = 0; i < layers.Count; ++i)
            {
                nodes[i] = new TreeNode(layers[i].LayerName);
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

        private void mapVariableLayerToolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void mapQueryToolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
