using SharpMap.Data;
using SharpMap.Layers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SportActivities
{
    

    public partial class LayerSettings : Form
    {
        VectorLayer layer;
        FeatureDataSet featureData;

        public LayerSettings(VectorLayer layer)
        {
            InitializeComponent();

            this.layer = layer;
            getFeatureData();
            initLayerLabelComboBox();
        }

        private void initLayerLabelComboBox()
        {
            DataColumnCollection columns = featureData.Tables[0].Columns;

            for(int i = 0; i < columns.Count; ++i)
            {
                ComboboxItem item = ComboboxItem.getInstance();
                item.Value = columns[i].ColumnName;
                item.Text = beautify((string)item.Value);
                layerLabelCombobox.Items.Add(item);
            }
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
            if (layer.IsQueryEnabled)
            {
                layer.ExecuteIntersectionQuery(layer.DataSource.GetExtents(), featureData);
            }
            else
            {
                Console.WriteLine("Layer hasn't query enabled!");
            }
        }

    }
}
