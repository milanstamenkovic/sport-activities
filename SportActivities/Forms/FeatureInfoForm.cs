using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharpMap.Data;

namespace SportActivities.Forms
{
    public partial class FeatureInfoForm : Form
    {
        private FeatureDataSet featureDataSet;

        public FeatureInfoForm(FeatureDataSet fds)
        {
            InitializeComponent();
            featureDataSet = fds;

            populateComboBox();
        }

        private void showFeatureInfo()
        {
            int index = Convert.ToInt32(((ComboboxItem)comboBoxTables.SelectedItem).Value);
            dgvTable.DataSource = featureDataSet.Tables[index];
        }

        private void populateComboBox()
        {
            for (int i = 0; i < featureDataSet.Tables.Count; ++i)
            {
                FeatureDataTable table = featureDataSet.Tables[i];
                if (table.Rows.Count > 0)
                {
                    ComboboxItem item = new ComboboxItem();
                    item.Text = Utils.beautify(table.TableName);
                    item.Value = i;
                    comboBoxTables.Items.Add(item);
                }
            }

            if (comboBoxTables.Items.Count > 0)
            {
                comboBoxTables.SelectedItem = comboBoxTables.Items[0];
                showFeatureInfo();
                Show();
            }
            else
            {
                MessageBox.Show("There is not any intersection here!");
                Close();
            }
        }

        private void comboBoxTables_SelectedIndexChanged(object sender, EventArgs e)
        {
            showFeatureInfo();
        }
    }
}
