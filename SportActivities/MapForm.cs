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
        private Dictionary<string, VectorLayer> layers;
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

            _ctFact = new CoordinateTransformationFactory();
            transfCoord = _ctFact.CreateFromCoordinateSystems(ProjNet.CoordinateSystems.GeographicCoordinateSystem.WGS84, ProjNet.CoordinateSystems.ProjectedCoordinateSystem.WebMercator);
            reverseTransfCoord = _ctFact.CreateFromCoordinateSystems(ProjNet.CoordinateSystems.ProjectedCoordinateSystem.WebMercator, ProjNet.CoordinateSystems.GeographicCoordinateSystem.WGS84);


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
    }
}
