using GeoAPI.CoordinateSystems.Transformations;
using Npgsql;
using SharpMap.Layers;
using SharpMap.Forms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using GeoAPI.CoordinateSystems;
using ProjNet.CoordinateSystems;
using SportActivities.DataModels;
using SharpMap.Data;
using GeoAPI.Geometries;
using SportActivities.Forms;

namespace SportActivities
{
    public partial class MapForm : Form
    {
         
        private Dictionary<string, LayerModel> layers;
        private List<LayerRecord> layerRecords;
        private DataManagement dataManagement;
        private bool showLabels;
        private bool showFeatureInfo;

        public MapForm()
        {
            InitializeComponent();

            dataManagement = DataManagement.Instance;

            layers = new Dictionary<string, LayerModel>();
            layerRecords = dataManagement.GetAllLayers();

            createVectorAndLabelInitialLayers();

            AddToTreeView();

            mapBox.Map.BackgroundLayer.Add(CreateBackgroundLayer());

            mapBox.Map.ZoomToExtents();
            mapBox.EnableShiftButtonDragRectangleZoom = true;

            mapBox.ActiveTool = MapBox.Tools.Pan;
            mapBox.Refresh();
        }

        private void createVectorAndLabelInitialLayers()
        {
            foreach (LayerRecord record in layerRecords)
            {
                if (!record.TableName.Equals("putevi") && !record.TableName.Equals("zgrade"))
                {
                    VectorLayer vectorLayer = new VectorLayer(record.TableName);
                    vectorLayer.DataSource = new SharpMap.Data.Providers.PostGIS(dataManagement.connectionParams, vectorLayer.LayerName, "gid");
                    vectorLayer.CoordinateTransformation = dataManagement.transfCoord;
                    vectorLayer.ReverseCoordinateTransformation = dataManagement.reverseTransfCoord;
                    vectorLayer.Style.EnableOutline = true;

                    layers.Add(record.TableName, new LayerModel(vectorLayer, dataManagement.createLabelLayer(vectorLayer, "Gid"), record));
                }
            }
        }

        private void AddToTreeView()
        {
            TreeNode[] nodes = new TreeNode[layers.Count];

            int i = 0;
            foreach(LayerModel layer in layers.Values)
            {
                nodes[i] = new TreeNode(layer.vectorLayer.LayerName);
                nodes[i++].Checked = false;
            }

            layersTreeView.Nodes.AddRange(nodes);
        }
        
        public ILayer CreateBackgroundLayer()
        {
            return new TileLayer(new BruTile.Web.OsmTileSource(), "OSM");
        }

        private void mapBox_MouseMove(Coordinate worldPos, MouseEventArgs imagePos)
        {
            Coordinate coord = dataManagement.reverseTransfCoord.MathTransform.Transform(worldPos);
            latStatusBar.Text = Convert.ToString(Math.Round(coord.X, 5));
            lngStatusBar.Text = Convert.ToString(Math.Round(coord.Y, 5));
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
                    mapBox.Map.Layers.Remove(layers[e.Node.Text].vectorLayer);
                    if (showLabels)
                        mapBox.Map.Layers.Remove(layers[e.Node.Text].labelLayer);
                    mapBox.Refresh();
                }
            }
        }

        private void uncheckLayerTreeView()
        {
            foreach (TreeNode node in layersTreeView.Nodes)
                node.Checked = false;
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
            mapBox.Map.BackgroundLayer.Clear();

            foreach(TreeNode layerNode in layersTreeView.Nodes)
                if (layerNode.Checked)
                    mapBox.Map.Layers.Add(layers[layerNode.Text].vectorLayer);

            if (showLabels)
                foreach (TreeNode layerNode in layersTreeView.Nodes)
                    if (layerNode.Checked)
                        mapBox.Map.Layers.Add(layers[layerNode.Text].labelLayer);

            if (mapBox.Map.Layers.Count > 0)
                mapBox.Map.ZoomToExtents();

            if (mapCheckBox.Checked)
                mapBox.Map.BackgroundLayer.Add(CreateBackgroundLayer());

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
            showLabels = !showLabels;
            if (showLabels)
            {
                btnShowLabels.BackColor = SystemColors.ActiveCaption;
            } 
            else
            {
                btnShowLabels.BackColor = SystemColors.Control;
            }
            renderActiveLayers();
        }

        private void mapBox_GeometryDefined(IGeometry geometry)
        {
            VectorLayer geometryLayer = dataManagement.GeometryFilter(mapBox.Map.Layers, geometry);
            mapBox.Map.Layers.Clear();

            mapBox.Map.Layers.Add(geometryLayer);

            mapBox.Refresh();
            mapBox.Invalidate();
        }

        private void toolsToolStripMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            ToolStripMenuItem toolStripMenu = sender as ToolStripMenuItem;

            foreach(ToolStripMenuItem item in toolStripMenu.DropDownItems)
                item.Checked = false;

            activeToolLabel.Text = e.ClickedItem.Text;
            ((ToolStripMenuItem)e.ClickedItem).Checked = true;

            mapBox.ActiveTool = getTool(int.Parse((string)e.ClickedItem.Tag));
        }

        private MapBox.Tools getTool(int value)
        {
            switch (value)
            {
                case 0:
                    return MapBox.Tools.Pan;
                case 1:
                    return MapBox.Tools.ZoomIn;
                case 2:
                    return MapBox.Tools.ZoomOut;
                case 3:
                    return MapBox.Tools.QueryBox;
                case 5:
                    return MapBox.Tools.ZoomWindow;
                case 6:
                    return MapBox.Tools.DrawPoint;
                case 7:
                    return MapBox.Tools.DrawLine;
                case 8:
                    return MapBox.Tools.DrawPolygon;
                default:
                    return MapBox.Tools.None;

            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void layersTreeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            LayerModel layerModel = layers[e.Node.Text];
            LayerSettings layerSettingsForm = new LayerSettings(ref mapBox, ref layerModel);
            layerSettingsForm.ShowDialog();
        }

        private void mapBox_MouseClick(object sender, MouseEventArgs e)
        {
            if(showFeatureInfo)
            {
                FeatureDataSet fds = dataManagement.GetFeatureDataSet(mapBox.Map.Layers, mapBox.Map.ImageToWorld(e.Location), mapBox.Map.Zoom / 100.0);
                FeatureInfoForm form = new FeatureInfoForm(fds);
            }
        }

        private void btnFeatureInfo_Click(object sender, EventArgs e)
        {
            showFeatureInfo = !showFeatureInfo;

            if(showFeatureInfo)
            {
                btnFeatureInfo.BackColor = SystemColors.ActiveCaption;
            }
            else
            {
                btnFeatureInfo.BackColor = SystemColors.Control;
            }
        }

        private void btnRouting_Click(object sender, EventArgs e)
        {
            mapBox.Map.Layers.Add(dataManagement.createRoutingLayer(new Coordinate(21.8660417174969, 43.3815275647817), new Coordinate(21.8890036212656, 43.3591535811185)));
            mapBox.Refresh();
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            using (DefinitonQueryForm form = new DefinitonQueryForm())
            {
                form.ShowDialog();

                Query query = form.getQuery();

                if(query != null)
                {
                    uncheckLayerTreeView();

                    VectorLayer queryLayer = dataManagement.DefinitionQueryFilter(query);
                    mapBox.Map.Layers.Clear();
                    mapBox.Map.BackgroundLayer.Clear();

                    mapBox.Map.Layers.Add(queryLayer);
                    mapBox.Map.ZoomToExtents();

                    if (mapCheckBox.Checked)
                        mapBox.Map.BackgroundLayer.Add(CreateBackgroundLayer());

                    mapBox.Refresh();
                    mapBox.Invalidate();

                    FeatureInfoForm fdsForm = new FeatureInfoForm(dataManagement.getFeatureDataSetForLayer(queryLayer));
                }
            };
        }

        private void mapBox_MapQueried(FeatureDataTable data)
        {
            int x = 10;
        }

        private void mapBox_MapQueryDone(object sender, EventArgs e)
        {
            int x = 10;
        }
    }
}