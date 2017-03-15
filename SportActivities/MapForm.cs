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

        private LayerCollection activeLayers;
        private List<string> activeLayersNames;
        private Dictionary<string, VectorLayer> layers;
        private int zoomLevel;
        private List<LayerRecord> layerRecords;
        private String connectionParams;

        private CoordinateTransformationFactory _ctFact;
        public ICoordinateTransformation transfCoord;
        public ICoordinateTransformation reverseTransfCoord;
        public MapForm()
        {
            InitializeComponent();

            connectionParams =
                         System.Configuration.ConfigurationManager.
                         ConnectionStrings["PostgreSQL"].ConnectionString;

            layers = new Dictionary<string, VectorLayer>();
            layerRecords = GetAllLayers();
            activeLayers = new LayerCollection();

            _ctFact = new CoordinateTransformationFactory();
            transfCoord = _ctFact.CreateFromCoordinateSystems(ProjNet.CoordinateSystems.GeographicCoordinateSystem.WGS84, ProjNet.CoordinateSystems.ProjectedCoordinateSystem.WebMercator);
            reverseTransfCoord = _ctFact.CreateFromCoordinateSystems(ProjNet.CoordinateSystems.ProjectedCoordinateSystem.WebMercator, ProjNet.CoordinateSystems.GeographicCoordinateSystem.WGS84);


            populateCollection();

            AddToTreeView();

            //mapBox.Map.Layers.AddCollection(activeLayers);
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
            TreeNode[] nodes = new TreeNode[layers.Count + 1];

            nodes[0] = new TreeNode("mapa");
            nodes[0].Checked = true;

            int i = 1;
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

        private void mapVariableLayerToolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void mapQueryToolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void layersTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void layersTreeView_ItemDrag(object sender, ItemDragEventArgs e)
        {

        }

        private void layersTreeView_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if(e.Action != TreeViewAction.Unknown)
            {
                if(e.Node.Checked)
                {
                    mapBox.Map.Layers.Add(layers[e.Node.Text]);
                }
                else
                {
                    mapBox.Map.Layers.Remove(layers[e.Node.Text]);
                }

                mapBox.Refresh();
            }
        }
    }
}
