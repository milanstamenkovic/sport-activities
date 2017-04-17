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

        private ComboboxItem[] operations = new ComboboxItem[]
        {
            new ComboboxItem("And", "AND"),
            new ComboboxItem("Or", "OR"),
            new ComboboxItem("Not", "AND NOT")
        };

        private ComboboxItem[] relations = new ComboboxItem[]
        {
            new ComboboxItem("Contains", "_st_contains"),
            new ComboboxItem("Intersects", "_st_intersects"),
            new ComboboxItem("Within", "_st_within"),
            new ComboboxItem("In distance", "st_dwithin")
            //new ComboboxItem("Fully in distance", "_st_dfullywithin")
        };

        private Query query = null;

        private int extraConditions1;
        private int extraConditions2;

        private ComboboxItem[] layerItems;
        private List<LayerRecord> records;

        public DefinitonQueryForm()
        {
            InitializeComponent();

            extraConditions1 = extraConditions2 = 0;

            records = DataManagement.Instance.GetAllLayers();
            createLayerItems();
            comboBoxLayer.DataSource = records.Select(r => r.TableName).ToList();

            comboBoxQuery.Items.AddRange(conditions);
            comboBoxQuery.SelectedItem = comboBoxQuery.Items[2];

            comboBoxQuery2.Items.AddRange(conditions);
            comboBoxQuery2.SelectedItem = comboBoxQuery2.Items[2];

            relationComboBox.Items.AddRange(relations);
            relationOperationCombobox.Items.AddRange(operations);

            relationComboBox.SelectedItem = relationComboBox.Items[0];
            relationOperationCombobox.SelectedItem = relationOperationCombobox.Items[0];
        }

        private void createLayerItems()
        {
            List<LayerRecord> records = DataManagement.Instance.GetAllLayers();

            layerItems = new ComboboxItem[records.Count];
            for(int i = 0; i < records.Count; ++i)
                layerItems[i] = new ComboboxItem(Utils.beautify(records[i].TableName), records[i].TableName);
        }

        private void populateAttributesCombobox(ComboBox comboBoxAttrs, string layer, string sufix, int extraConditions)
        {
            comboBoxAttrs.Items.Clear();

            List<object> attrs = DataManagement.Instance.getAllLayerAttributes(layer);

            ComboboxItem[] items = new ComboboxItem[attrs.Count];

            for (int i = 0; i < attrs.Count; ++i)
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

                if (extraConditionComboBox != null)
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
            operators.Items.AddRange(operations);

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
            condition.SelectedItem = condition.Items[0];

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
            query.TableName = comboBoxLayer.SelectedItem.ToString();

            string attribute = ((ComboboxItem)comboBoxAttributes.SelectedItem).Value.ToString();
            
            query.Condition = createCondition(attribute, textBoxValue.Text, comboBoxQuery.SelectedItem.ToString());
            query.Condition = addExtraConditions(query.Condition, extraConditions1, "_1");

            if (textBoxValue.Text.Equals("") || query.Condition == null)
            {
                MessageBox.Show("You didn't specified all values");
                query = null;
                return;
            }

            if (spatialQueryCheckBox.Checked)
            {
                string tableName2 = comboBoxLayer2.SelectedItem.ToString();
                string relationOperation = ((ComboboxItem)relationOperationCombobox.SelectedItem).Value.ToString();

                query.Condition += " " + relationOperation + " gid in (select " + query.TableName + ".gid from " + query.TableName + "," + tableName2 + " where ";
                query.Condition += ((ComboboxItem)relationComboBox.SelectedItem).Value.ToString();

                if (distanceInput.Enabled)
                {
                    query.Condition += "(" + query.TableName + ".geom::geography," + tableName2 + ".geom::geography" + "," + distanceInput.Value.ToString();
                }
                else
                {
                    query.Condition += "(" + query.TableName + ".geom," + tableName2 + ".geom";
                }

                query.Condition += ")";

                attribute = ((ComboboxItem)comboBoxAttributes2.SelectedItem).Value.ToString();

              
                string condition2 = createCondition(attribute, textBoxValue2.Text, comboBoxQuery2.SelectedItem.ToString());
                condition2 = addExtraConditions(condition2, extraConditions2, "_2");
                if (textBoxValue2.Text.Equals("") || condition2 == null)
                {
                    MessageBox.Show("You didn't specified all values");
                    query = null;
                    return;
                }

                query.Condition += " AND " + tableName2 + ".gid in (select gid from " + tableName2 + " where "
                    + condition2 + "))";

            }
            Close();
        }

        private string addExtraConditions(string resultCondition, int extraConditions, string sufix)
        {
            for (int i = 0; i < extraConditions; ++i)
            {
                string attribute = ((ComboboxItem)((ComboBox)Controls.Find("attributesComboBox" + (i + 1).ToString() + sufix, true)[0]).SelectedItem).Value.ToString();
                string condition = ((ComboBox)Controls.Find("conditionComboBox" + (i + 1).ToString() + sufix, true)[0]).SelectedItem.ToString();
                string conditionValue = ((TextBox)Controls.Find("conditionValue" + (i + 1).ToString() + sufix, true)[0]).Text;
                if(conditionValue.Equals(""))
                {
                    return null;
                }
                string _operator = ((ComboboxItem)((ComboBox)Controls.Find("operatorComboBox" + (i + 1).ToString() + sufix, true)[0]).SelectedItem).Value.ToString();

                resultCondition += " " + _operator + " ";
                resultCondition += createCondition(attribute, conditionValue, condition);
            }

            return resultCondition;
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
            if (spatialQueryCheckBox.Checked)
                comboBoxLayer2.DataSource = records.Where(y => y.TableName != comboBoxLayer.SelectedItem.ToString()).Select(x => x.TableName).ToList();

            populateAttributesCombobox(comboBoxAttributes, comboBoxLayer.SelectedItem.ToString(), "1", extraConditions1);
        }

        public Query getQuery()
        {
            return query;
        }

        private void btnAddCondition_Click_1(object sender, EventArgs e)
        {
            extraConditions1++;

            Control panel = Controls.Find("panelQuery", true)[0];
            addCondition(panel, "1", extraConditions1);

            ComboBox attrsComboBox = (ComboBox)panel.Controls.Find("attributesComboBox" + extraConditions1.ToString() + "_1", true)[0];

            foreach (object item in comboBoxAttributes.Items)
            {
                attrsComboBox.Items.Add(item);
            }
            attrsComboBox.SelectedItem = attrsComboBox.Items[0];

            moveControl(btnAddCondition);
            moveControl(panelQuery2);
            moveControl(spatialQueryCheckBox);
            moveControl(relationComboBox);
            moveControl(relationOperationCombobox);
            moveControl(distanceInput);
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
            panelQuery2.Enabled
                = relationComboBox.Enabled
                = relationOperationCombobox.Enabled
                = spatialQueryCheckBox.Checked;

            if (spatialQueryCheckBox.Checked)
                comboBoxLayer2.DataSource = records.Where(y => y.TableName != comboBoxLayer.SelectedItem.ToString()).Select(x => x.TableName).ToList();
            else
                comboBoxLayer.DataSource = records.Select(x => x.TableName).ToList();
        }

        private void comboBoxLayer2_SelectedIndexChanged(object sender, EventArgs e)
        {
            populateAttributesCombobox(comboBoxAttributes2, comboBoxLayer2.SelectedItem.ToString(), "2", extraConditions2);
        }

        private void relationComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (relationComboBox.Items.IndexOf(relationComboBox.SelectedItem) > 2)
                distanceInput.Enabled = true;
            else
                distanceInput.Enabled = false;
        }
    }
}
