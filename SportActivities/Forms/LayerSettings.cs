using SharpMap.Data; 
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using SportActivities.DataModels; 

namespace SportActivities
{
    public partial class LayerSettings : Form
    {
        private LayerModel layer;
        private FeatureDataSet featureData;
        private DataManagement dataManagement;

        public LayerSettings(ref LayerModel layer)
        {
            InitializeComponent();

            dataManagement = new DataManagement();
            this.layer = layer;
            getFeatureData();
            initLayerLabelComboBox();
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
            Close();
        }
    }
}
