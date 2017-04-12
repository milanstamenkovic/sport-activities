using SportActivities.DataModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SportActivities.Forms
{
    public partial class DefinitonQueryForm : Form
    {
        private string[] conditions = new string[]
        {
            "Equals",
            "Like",
            ">",
            "<",
            "="
        };

        private Query query;
        private bool isQueryDefined;

        public DefinitonQueryForm()
        {
            InitializeComponent();

            populateLayersCombobox();
            populateAttributesCombobox();
            populateConditionsCombobox();

            isQueryDefined = false;
        }

        private void populateLayersCombobox()
        {
            List<LayerRecord> records = DataManagement.Instance.GetAllLayers();

            foreach(LayerRecord record in records)
            {
                ComboboxItem item = new ComboboxItem();
                item.Text = Utils.beautify(record.TableName);
                item.Value = record.TableName;
                comboBoxLayer.Items.Add(item);
            }
            comboBoxLayer.SelectedItem = comboBoxLayer.Items[0];
        }

        private void populateAttributesCombobox()
        {
            comboBoxAttributes.Items.Clear();

            List<object> attrs = DataManagement.Instance
                .getAllLayerAttributes(((ComboboxItem)comboBoxLayer.SelectedItem).Value.ToString());

            foreach(object attr in attrs)
            {
                ComboboxItem item = new ComboboxItem();
                item.Text = Utils.beautify(attr.ToString());
                item.Value = attr;
                comboBoxAttributes.Items.Add(item);
            }
            comboBoxAttributes.SelectedItem = comboBoxAttributes.Items[0];
        }

        private void populateConditionsCombobox()
        {
            comboBoxQuery.Items.AddRange(conditions);
        }

        private void btnAddCondition_Click(object sender, EventArgs e)
        {

        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            query = new Query();

            query.TableName = ((ComboboxItem)comboBoxLayer.SelectedItem).Value.ToString();

            string attribute = ((ComboboxItem)comboBoxAttributes.SelectedItem).Value.ToString();

            string condition = "";
            if (comboBoxQuery.SelectedItem.Equals("Equals"))
            {
                condition = "cast(" + attribute + " as text) = '" + textBoxValue.Text + "'";

            }
            else if (comboBoxQuery.SelectedItem.Equals("Like"))
            {
                condition = "cast(" + attribute + " as text) ilike '%" + textBoxValue.Text + "%'";
            }
            else
            {
                condition = attribute + " " + comboBoxQuery.SelectedItem + " " + textBoxValue.Text;
            }

            query.Condition = condition;
            isQueryDefined = true;
            Close();
        }

        private void comboBoxLayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            populateAttributesCombobox();
        }

        public Query getQuery()
        {
           return query;
        }

        public bool IsQueryDefined()
        {
            return isQueryDefined;
        }

        private void comboBoxQuery_TextUpdate(object sender, EventArgs e)
        {
            comboBoxQuery.Items.Add(comboBoxQuery.Text);
        }
    }
}
