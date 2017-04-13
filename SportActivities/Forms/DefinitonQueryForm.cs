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

            populateLayersCombobox(comboBoxLayer);
            populateLayersCombobox(comboBoxLayer2);

            populateAttributesCombobox(comboBoxAttributes, ((ComboboxItem)comboBoxLayer.SelectedItem).Value.ToString(), "1", extraConditions1);
            populateAttributesCombobox(comboBoxAttributes2, ((ComboboxItem)comboBoxLayer2.SelectedItem).Value.ToString(), "2", extraConditions2);

            comboBoxQuery.Items.AddRange(conditions);
            comboBoxQuery2.Items.AddRange(conditions);

            isQueryDefined = false;
        }

        private void populateLayersCombobox(ComboBox combobox)
        {
            List<LayerRecord> records = DataManagement.Instance.GetAllLayers();

            foreach(LayerRecord record in records)
            {
                ComboboxItem item = new ComboboxItem();
                item.Text = Utils.beautify(record.TableName);
                item.Value = record.TableName;
                combobox.Items.Add(item);
            }
            combobox.SelectedItem = combobox.Items[0];
        }

        private void populateAttributesCombobox(ComboBox comboBoxAttrs, string layer, string sufix, int extraConditions)
        {
            comboBoxAttrs.Items.Clear();
            
            List<object> attrs = DataManagement.Instance.getAllLayerAttributes(layer);

            ComboboxItem[] items = new ComboboxItem[attrs.Count];

            for(int i = 0; i < attrs.Count; ++i)
            {
                ComboboxItem item = new ComboboxItem();
                item.Text = Utils.beautify(attrs[i].ToString());
                item.Value = attrs[i];
                items[i] = item;
            }

            comboBoxAttrs.Items.AddRange(items);
            comboBoxAttrs.SelectedItem = comboBoxAttrs.Items[0];

            for (int i = 0; i < extraConditions; ++i)
            {
                ComboBox extraConditionComboBox = (ComboBox)Controls.Find("attributesComboBox" + (i + 1).ToString() + "_" + sufix, true)[0];

                if(extraConditionComboBox != null)
                {
                    extraConditionComboBox.Items.Clear();
                    extraConditionComboBox.Items.AddRange(items);
                    extraConditionComboBox.SelectedItem = extraConditionComboBox.Items[0];
                }
            }
        }

        private void addCondition(Control parentPanel, string sufix, int extraConditions)
        {
            ComboBox operators = new ComboBox();
            operators.Name = "operatorComboBox" + extraConditions.ToString() + "_" + sufix;
            operators.Items.Add("OR");
            operators.Items.Add("AND");
            operators.Items.Add("NOT");
            operators.Location = new Point(60, 30 * extraConditions);
            operators.Width = 50;
            operators.DropDownStyle = ComboBoxStyle.DropDownList;

            operators.SelectedItem = operators.Items[0];
            
            ComboBox attributes = new ComboBox();
            attributes.Location = new Point(130, 30 * extraConditions);
            attributes.DropDownStyle = ComboBoxStyle.DropDownList;
            attributes.Name = "attributesComboBox" + extraConditions.ToString() + "_" + sufix;

            ComboBox condition = new ComboBox();
            condition.Location = new Point(270, 30 * extraConditions);
            condition.Items.AddRange(conditions);
            condition.Width = 80;
            condition.DropDownStyle = ComboBoxStyle.DropDownList;
            condition.Name = "conditionComboBox" + extraConditions.ToString() + "_" + sufix;

            TextBox conditionValue = new TextBox();
            conditionValue.Location = new Point(370, 30 * extraConditions);
            conditionValue.Width = 130;
            conditionValue.Name = "conditionValue" + extraConditions.ToString() + "_" + sufix;

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

            for(int i = 0; i < extraConditions1; ++i)
            {
                attribute = ((ComboboxItem)((ComboBox)Controls.Find("attributesComboBox" + (i + 1).ToString() + "_1", true)[0]).SelectedItem).Value.ToString();
                string condition = ((ComboBox)Controls.Find("conditionComboBox" + (i + 1).ToString() + "_1", true)[0]).SelectedItem.ToString();
                string conditionValue = ((TextBox)Controls.Find("conditionValue" + (i + 1).ToString() + "_1", true)[0]).Text;

                string _operator = ((ComboBox)Controls.Find("operatorComboBox" + (i + 1).ToString() + "_1", true)[0]).SelectedItem.ToString();

                query.Condition += " " + _operator + " ";
                query.Condition += createCondition(attribute, conditionValue, condition);
            }

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
            populateAttributesCombobox(comboBoxAttributes, ((ComboboxItem)comboBoxLayer.SelectedItem).Value.ToString(), "1", extraConditions1);
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

            Control panel = Controls.Find("panelQuery", true)[0];
            addCondition(panel, "1", extraConditions1);

            ComboBox attrsComboBox = (ComboBox)panel.Controls.Find("attributesComboBox" + extraConditions1.ToString() + "_1", true)[0];

            foreach(object item in comboBoxAttributes.Items)
            {
                attrsComboBox.Items.Add(item);
            }
            attrsComboBox.SelectedItem = attrsComboBox.Items[0];

            moveControl(btnAddCondition);
            moveControl(panelQuery2);
            moveControl(spatialQueryCheckBox);
            moveControl(relationComboBox);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            extraConditions2++;

            Control panel = Controls.Find("panelQuery2", true)[0];
            addCondition(panel, "2", extraConditions2);

            ComboBox attrsComboBox = (ComboBox)panel.Controls.Find("attributesComboBox" + extraConditions2.ToString() + "_2", true)[0];

            foreach (object item in comboBoxAttributes2.Items)
            {
                attrsComboBox.Items.Add(item);
            }
            attrsComboBox.SelectedItem = attrsComboBox.Items[0];

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

        private void comboBoxLayer2_SelectedIndexChanged(object sender, EventArgs e)
        {
            populateAttributesCombobox(comboBoxAttributes2, ((ComboboxItem)comboBoxLayer2.SelectedItem).Value.ToString(), "2", extraConditions2);
        }
    }
}
