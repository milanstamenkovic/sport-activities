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
            string selectedTable = comboBoxTables.SelectedItem.ToString();
            dgvTable.DataSource = featureDataSet.Tables.Where(x => x.TableName.Equals(selectedTable)).First();
        }

        private void populateComboBox()
        {
            comboBoxTables.DataSource = featureDataSet.Tables.Select(x => x.TableName).ToList();

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
