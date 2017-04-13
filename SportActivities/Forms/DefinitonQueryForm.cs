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

        private int extraConditions1;
        private int extraConditions2;

        public DefinitonQueryForm()
        {
            InitializeComponent();

            extraConditions1 = extraConditions2 = 0;

            populateLayersCombobox();
            populateAttributesCombobox(((ComboboxItem)comboBoxLayer.SelectedItem).Value.ToString(), extraConditions1);
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

        private void populateAttributesCombobox(string layer, int extraConditions)
        {
            comboBoxAttributes.Items.Clear();
            
            List<object> attrs = DataManagement.Instance.getAllLayerAttributes(layer);

            ComboboxItem[] items = new ComboboxItem[attrs.Count];

            for(int i = 0; i < attrs.Count; ++i)
            {
                ComboboxItem item = new ComboboxItem();
                item.Text = Utils.beautify(attrs[i].ToString());
                item.Value = attrs[i];
                items[i] = item;
            }

            comboBoxAttributes.Items.AddRange(items);

            for(int i = 0; i < extraConditions; ++i)
            {

            }
            comboBoxAttributes.SelectedItem = comboBoxAttributes.Items[0];
        }

        private void populateConditionsCombobox()
        {
            comboBoxQuery.Items.AddRange(conditions);
        }

        private void addCondition(Control parentPanel, int extraConditions)
        {
            ComboBox operators = new ComboBox();
            operators.Name = "operatorComboBox" + extraConditions;
            operators.Items.Add("OR");
            operators.Items.Add("AND");
            operators.Items.Add("NOT");
            operators.Location = new Point(60, 30 * extraConditions);
            operators.Width = 50;
            operators.DropDownStyle = ComboBoxStyle.DropDownList;

            
            ComboBox attributes = new ComboBox();
            attributes.Location = new Point(130, 30 * extraConditions);
            attributes.DropDownStyle = ComboBoxStyle.DropDownList;

            ComboBox condition = new ComboBox();
            condition.Location = new Point(270, 30 * extraConditions);
            condition.Items.AddRange(conditions);
            condition.Width = 80;
            condition.DropDownStyle = ComboBoxStyle.DropDownList;

            TextBox conditionValue = new TextBox();
            conditionValue.Location = new Point(370, 30 * extraConditions);
            conditionValue.Width = 130;


            Point btnLoc = btnFilter.Location;
            btnFilter.Location = new Point(btnLoc.X, btnLoc.Y + 30);

            parentPanel.Controls.Add(operators);
            parentPanel.Controls.Add(attributes);
            parentPanel.Controls.Add(condition);
            parentPanel.Controls.Add(conditionValue);
            
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            query = new Query();

            query.TableName = ((ComboboxItem)comboBoxLayer.SelectedItem).Value.ToString();

            string attribute = ((ComboboxItem)comboBoxAttributes.SelectedItem).Value.ToString();

            query.Condition = createCondition(attribute, textBoxValue.Text, comboBoxQuery.SelectedItem.ToString());

            isQueryDefined = true;
            Close();
        }

        private string createCondition(string attribute, string value, string sign)
        {
            string condition = "";
            if (sign.Equals("Equals"))
            {
                condition = "cast(" + attribute + " as text) = '" + value + "'";

            }
            else if (sign.Equals("Like"))
            {
                condition = "cast(" + attribute + " as text) ilike '%" + value + "%'";
            }
            else
            {
                condition = attribute + " " + sign + " " + value;
            }

            return condition;
        }

        private void comboBoxLayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            populateAttributesCombobox(((ComboboxItem)comboBoxLayer.SelectedItem).Value.ToString(), extraConditions1);
        }

        public Query getQuery()
        {
           return query;
        }

        public bool IsQueryDefined()
        {
            return isQueryDefined;
        }

        private void DefinitonQueryForm_Load(object sender, EventArgs e)
        {

        }

        private void btnAddCondition_Click_1(object sender, EventArgs e)
        {
            extraConditions1++;

            addCondition(Controls.Find("panelQuery", true)[0], extraConditions1);

            moveControl(btnAddCondition);
            moveControl(panelQuery2);
            moveControl(spatialQueryCheckBox);
            moveControl(relationComboBox);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            extraConditions2++;

            addCondition(Controls.Find("panelQuery2", true)[0], extraConditions2);

            moveControl(btnAddCondition2);
        }

        private void moveControl(Control control)
        {
            Point loc = control.Location;
            control.Location = new Point(loc.X, loc.Y + 30);
        }

        private void spatialQueryCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            panelQuery2.Enabled = spatialQueryCheckBox.Checked;
            relationComboBox.Enabled = spatialQueryCheckBox.Checked;
        }
    }
}
