﻿using SharpMap.Data;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using SportActivities.DataModels;
using System.Drawing;
using SharpMap.Forms;
using System.Collections.Generic;

namespace SportActivities
{
    public partial class LayerSettings : Form
    {
        private MapBox mapBox;
        private LayerModel layer;
        private string geometryType;

        public LayerSettings(ref MapBox mapBox, ref LayerModel layer)
        {
            InitializeComponent();

            this.mapBox = mapBox;
            this.layer = layer;

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
            List<object> attributes = DataManagement.Instance.getAllLayerAttributes(layer.vectorLayer.LayerName);

            foreach(object attr in attributes)
            {
                ComboboxItem item = ComboboxItem.getInstance();
                item.Value = attr.ToString();
                item.Text = Utils.beautify(attr.ToString());
                comboboxLabel.Items.Add(item);
            }
            
            comboboxLabel.SelectedIndex = comboboxLabel.FindStringExact(layer.labelLayer.LabelColumn);
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
            showColorDialog(btnLabelColor);
        }

        private void btnGeometryColor_Click(object sender, EventArgs e)
        {
            showColorDialog(btnGeometryColor);
        }

        private void btnOutlineColor_Click(object sender, EventArgs e)
        {
            showColorDialog(btnOutlineColor);
        }

        private void showColorDialog(Control control)
        {
            cdColor.Color = control.BackColor;
            DialogResult result = cdColor.ShowDialog();
            if (result == DialogResult.OK)
            {
                control.BackColor = cdColor.Color;
            }
        }
    }
}
