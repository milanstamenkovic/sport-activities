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

namespace SportActivities
{
    public partial class MapForm : Form
    {
         
        private Dictionary<string, LayerModel> layers;
        private List<LayerRecord> layerRecords;
        private DataManagement dataManagement;
        private bool showLabels;

        public MapForm()
        {
            InitializeComponent();

            dataManagement = new DataManagement();

            layers = new Dictionary<string, LayerModel>();
            layerRecords = dataManagement.GetAllLayers();

            createVectorAndLabelInitialLayers();

            AddToTreeView();

            mapBox.Map.BackgroundLayer.Add(CreateBackgroundLayer());

            mapBox.Map.ZoomToExtents();

            mapBox.Refresh();
            mapBox.EnableShiftButtonDragRectangleZoom = true;
            mapBox.ActiveTool = MapBox.Tools.Pan;
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

        private void mapBox_MouseMove(GeoAPI.Geometries.Coordinate worldPos, MouseEventArgs imagePos)
        {
            IProjectedCoordinateSystem utmProj = createUtmProjection(34);
            IGeographicCoordinateSystem geoCs = utmProj.GeographicCoordinateSystem;
            ICoordinateTransformation transform = dataManagement.ctFact.CreateFromCoordinateSystems(geoCs, utmProj);
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
                    mapBox.Map.Layers.Remove(layers[e.Node.Text].vectorLayer);
                    if (showLabels)
                        mapBox.Map.Layers.Remove(layers[e.Node.Text].labelLayer);
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
                    mapBox.Map.Layers.Add(layers[layerNode.Text].vectorLayer);

            if (showLabels)
                foreach (TreeNode layerNode in layersTreeView.Nodes)
                    if (layerNode.Checked)
                        mapBox.Map.Layers.Add(layers[layerNode.Text].labelLayer);

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

        private void mapBox_GeometryDefined(GeoAPI.Geometries.IGeometry geometry)
        {
            //foreach (VectorLayer layer in layers.Values)
            //{
            //    if(layer.IsQueryEnabled)
            //        layer.
            //}
        }

        private void toolsToolStripMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            ToolStripMenuItem toolStripMenu = sender as ToolStripMenuItem;

            foreach(ToolStripMenuItem item in toolStripMenu.DropDownItems)
                item.Checked = false;

            activeToolLabel.Text = e.ClickedItem.Text;
            ((ToolStripMenuItem)e.ClickedItem).Checked = true;

            mapBox.ActiveTool = getTool(Int32.Parse((string)e.ClickedItem.Tag));
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
    }
}