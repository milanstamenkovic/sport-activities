using SharpMap.Data;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using SportActivities.DataModels;
using System.Drawing;
using SharpMap.Forms;

namespace SportActivities
{
    public partial class LayerSettings : Form
    {
        private MapBox mapBox;
        private LayerModel layer;
        private FeatureDataSet featureData;
        private DataManagement dataManagement;
        private string geometryType;

        public LayerSettings(ref MapBox mapBox, ref LayerModel layer)
        {
            InitializeComponent();

            dataManagement = new DataManagement();
            this.mapBox = mapBox;
            this.layer = layer;
            getFeatureData();
            initLayerLabelComboBox();

            lbLayerName.Text = layer.vectorLayer.LayerName;
            btnLabelColor.BackColor = layer.labelLayer.Style.ForeColor;
            btnGeometryColor.BackColor = layer.geometryColor;
            btnOutlineColor.BackColor = layer.vectorLayer.Style.Outline.Color;
            nudOutlineWidth.Value = (decimal) layer.vectorLayer.Style.Outline.Width;

            geometryType = this.layer.layerRecord.Type;
            if (geometryType == "POINT")
            {
                lblPointSize.Show();
                nudPointSize.Show();
                nudPointSize.Value = (decimal) layer.vectorLayer.Style.PointSize;
            }
            else if (geometryType == "MULTILINESTRING")
            {
                lblGeometryColor.Text = "Line color:";
            }
            else if (geometryType == "MULTIPOLYGON")
            {
                lblGeometryColor.Text = "Fill color:";
            }
        }

        private void initLayerLabelComboBox()
        {
            DataColumnCollection columns = featureData.Tables[0].Columns;

            for (int i = 0; i < columns.Count; ++i)
            {
                ComboboxItem item = ComboboxItem.getInstance();
                item.Value = columns[i].ColumnName;
                item.Text = beautify((string)item.Value);
                comboboxLabel.Items.Add(item);
            }
            
            comboboxLabel.SelectedIndex = comboboxLabel.FindStringExact(layer.labelLayer.LabelColumn);
        }

        private string beautify(string param)
        {
            string result = param.First().ToString().ToUpper() + param.Substring(1);

            result = result.Replace("_", " ");
            return result;
        }

        private void getFeatureData()
        {
            featureData = new FeatureDataSet();
            if (layer.vectorLayer.IsQueryEnabled)
            {
                layer.vectorLayer.ExecuteIntersectionQuery(layer.vectorLayer.DataSource.GetExtents(), featureData);
            }
            else
            {
                Console.WriteLine("Layer hasn't query enabled!");
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            layer.labelLayer.LabelColumn = comboboxLabel.SelectedItem.ToString();
            layer.labelLayer.Style.ForeColor = btnLabelColor.BackColor;
            layer.geometryColor = btnGeometryColor.BackColor;
            layer.vectorLayer.Style.Outline.Color = btnOutlineColor.BackColor;
            layer.vectorLayer.Style.Outline.Width = (float) nudOutlineWidth.Value;

            if (geometryType == "POINT")
            {
                layer.vectorLayer.Style.PointColor = new SolidBrush(layer.geometryColor);
                layer.vectorLayer.Style.PointSize = (float) nudPointSize.Value;
            }
            else if (geometryType == "MULTILINESTRING")
            {
                layer.vectorLayer.Style.Line.Color = layer.geometryColor;
            }
            else if (geometryType == "MULTIPOLYGON")
            {
                layer.vectorLayer.Style.Fill = new SolidBrush(layer.geometryColor);
            }

            mapBox.Refresh();

            Close();
        }

        private void btnLabelColor_Click(object sender, EventArgs e)
        {
            cdColor.Color = btnLabelColor.BackColor;
            DialogResult result = cdColor.ShowDialog();
            if (result == DialogResult.OK)
            {
                btnLabelColor.BackColor = cdColor.Color;
            }
        }

        private void btnGeometryColor_Click(object sender, EventArgs e)
        {
            cdColor.Color = btnGeometryColor.BackColor;
            DialogResult result = cdColor.ShowDialog();
            if (result == DialogResult.OK)
            {
                btnGeometryColor.BackColor = cdColor.Color;
            }
        }

        private void btnOutlineColor_Click(object sender, EventArgs e)
        {
            cdColor.Color = btnOutlineColor.BackColor;
            DialogResult result = cdColor.ShowDialog();
            if (result == DialogResult.OK)
            {
                btnOutlineColor.BackColor = cdColor.Color;
            }
        }
    }
}
