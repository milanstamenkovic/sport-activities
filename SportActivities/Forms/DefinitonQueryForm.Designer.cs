namespace SportActivities.Forms
{
    partial class DefinitonQueryForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.comboBoxAttributes = new System.Windows.Forms.ComboBox();
            this.comboBoxQuery = new System.Windows.Forms.ComboBox();
            this.textBoxValue = new System.Windows.Forms.TextBox();
            this.comboBoxLayer = new System.Windows.Forms.ComboBox();
            this.btnFilter = new System.Windows.Forms.Button();
            this.panelQuery = new System.Windows.Forms.Panel();
            this.btnAddCondition = new System.Windows.Forms.Button();
            this.panelQuery2 = new System.Windows.Forms.Panel();
            this.btnAddCondition2 = new System.Windows.Forms.Button();
            this.comboBoxLayer2 = new System.Windows.Forms.ComboBox();
            this.comboBoxAttributes2 = new System.Windows.Forms.ComboBox();
            this.comboBoxQuery2 = new System.Windows.Forms.ComboBox();
            this.textBoxValue2 = new System.Windows.Forms.TextBox();
            this.relationComboBox = new System.Windows.Forms.ComboBox();
            this.spatialQueryCheckBox = new System.Windows.Forms.CheckBox();
            this.panelQuery.SuspendLayout();
            this.panelQuery2.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBoxAttributes
            // 
            this.comboBoxAttributes.AccessibleName = "attributesComboBox1";
            this.comboBoxAttributes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAttributes.FormattingEnabled = true;
            this.comboBoxAttributes.Location = new System.Drawing.Point(130, 0);
            this.comboBoxAttributes.Name = "comboBoxAttributes";
            this.comboBoxAttributes.Size = new System.Drawing.Size(121, 21);
            this.comboBoxAttributes.TabIndex = 1;
            // 
            // comboBoxQuery
            // 
            this.comboBoxQuery.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxQuery.FormattingEnabled = true;
            this.comboBoxQuery.Location = new System.Drawing.Point(270, 0);
            this.comboBoxQuery.Name = "comboBoxQuery";
            this.comboBoxQuery.Size = new System.Drawing.Size(80, 21);
            this.comboBoxQuery.TabIndex = 1;
            // 
            // textBoxValue
            // 
            this.textBoxValue.Location = new System.Drawing.Point(370, 0);
            this.textBoxValue.Name = "textBoxValue";
            this.textBoxValue.Size = new System.Drawing.Size(130, 20);
            this.textBoxValue.TabIndex = 2;
            // 
            // comboBoxLayer
            // 
            this.comboBoxLayer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLayer.FormattingEnabled = true;
            this.comboBoxLayer.Location = new System.Drawing.Point(0, 0);
            this.comboBoxLayer.Name = "comboBoxLayer";
            this.comboBoxLayer.Size = new System.Drawing.Size(110, 21);
            this.comboBoxLayer.TabIndex = 0;
            this.comboBoxLayer.SelectedIndexChanged += new System.EventHandler(this.comboBoxLayer_SelectedIndexChanged);
            // 
            // btnFilter
            // 
            this.btnFilter.Location = new System.Drawing.Point(437, 161);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(75, 23);
            this.btnFilter.TabIndex = 5;
            this.btnFilter.Text = "Filter";
            this.btnFilter.UseVisualStyleBackColor = true;
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // panelQuery
            // 
            this.panelQuery.AutoSize = true;
            this.panelQuery.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelQuery.Controls.Add(this.btnAddCondition);
            this.panelQuery.Controls.Add(this.comboBoxLayer);
            this.panelQuery.Controls.Add(this.comboBoxAttributes);
            this.panelQuery.Controls.Add(this.comboBoxQuery);
            this.panelQuery.Controls.Add(this.textBoxValue);
            this.panelQuery.Location = new System.Drawing.Point(12, 12);
            this.panelQuery.Name = "panelQuery";
            this.panelQuery.Size = new System.Drawing.Size(503, 52);
            this.panelQuery.TabIndex = 6;
            // 
            // btnAddCondition
            // 
            this.btnAddCondition.Location = new System.Drawing.Point(392, 26);
            this.btnAddCondition.Name = "btnAddCondition";
            this.btnAddCondition.Size = new System.Drawing.Size(108, 23);
            this.btnAddCondition.TabIndex = 5;
            this.btnAddCondition.Text = "Add Condition";
            this.btnAddCondition.UseVisualStyleBackColor = true;
            this.btnAddCondition.Click += new System.EventHandler(this.btnAddCondition_Click_1);
            // 
            // panelQuery2
            // 
            this.panelQuery2.AutoSize = true;
            this.panelQuery2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelQuery2.Controls.Add(this.btnAddCondition2);
            this.panelQuery2.Controls.Add(this.comboBoxLayer2);
            this.panelQuery2.Controls.Add(this.comboBoxAttributes2);
            this.panelQuery2.Controls.Add(this.comboBoxQuery2);
            this.panelQuery2.Controls.Add(this.textBoxValue2);
            this.panelQuery2.Enabled = false;
            this.panelQuery2.Location = new System.Drawing.Point(12, 103);
            this.panelQuery2.Name = "panelQuery2";
            this.panelQuery2.Size = new System.Drawing.Size(503, 52);
            this.panelQuery2.TabIndex = 7;
            // 
            // btnAddCondition2
            // 
            this.btnAddCondition2.Location = new System.Drawing.Point(392, 26);
            this.btnAddCondition2.Name = "btnAddCondition2";
            this.btnAddCondition2.Size = new System.Drawing.Size(108, 23);
            this.btnAddCondition2.TabIndex = 5;
            this.btnAddCondition2.Text = "Add Condition";
            this.btnAddCondition2.UseVisualStyleBackColor = true;
            this.btnAddCondition2.Click += new System.EventHandler(this.button1_Click);
            // 
            // comboBoxLayer2
            // 
            this.comboBoxLayer2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLayer2.FormattingEnabled = true;
            this.comboBoxLayer2.Location = new System.Drawing.Point(0, 0);
            this.comboBoxLayer2.Name = "comboBoxLayer2";
            this.comboBoxLayer2.Size = new System.Drawing.Size(110, 21);
            this.comboBoxLayer2.TabIndex = 4;
            this.comboBoxLayer2.SelectedIndexChanged += new System.EventHandler(this.comboBoxLayer2_SelectedIndexChanged);
            // 
            // comboBoxAttributes2
            // 
            this.comboBoxAttributes2.AccessibleName = "attributesComboBox2";
            this.comboBoxAttributes2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAttributes2.FormattingEnabled = true;
            this.comboBoxAttributes2.Location = new System.Drawing.Point(130, 0);
            this.comboBoxAttributes2.Name = "comboBoxAttributes2";
            this.comboBoxAttributes2.Size = new System.Drawing.Size(121, 21);
            this.comboBoxAttributes2.TabIndex = 0;
            // 
            // comboBoxQuery2
            // 
            this.comboBoxQuery2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxQuery2.FormattingEnabled = true;
            this.comboBoxQuery2.Location = new System.Drawing.Point(270, 0);
            this.comboBoxQuery2.Name = "comboBoxQuery2";
            this.comboBoxQuery2.Size = new System.Drawing.Size(81, 21);
            this.comboBoxQuery2.TabIndex = 1;
            // 
            // textBoxValue2
            // 
            this.textBoxValue2.Location = new System.Drawing.Point(370, 0);
            this.textBoxValue2.Name = "textBoxValue2";
            this.textBoxValue2.Size = new System.Drawing.Size(130, 20);
            this.textBoxValue2.TabIndex = 2;
            // 
            // relationComboBox
            // 
            this.relationComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.relationComboBox.Enabled = false;
            this.relationComboBox.FormattingEnabled = true;
            this.relationComboBox.Items.AddRange(new object[] {
            "Contains",
            "Within",
            "Intersect",
            "In Distance"});
            this.relationComboBox.Location = new System.Drawing.Point(140, 71);
            this.relationComboBox.Name = "relationComboBox";
            this.relationComboBox.Size = new System.Drawing.Size(83, 21);
            this.relationComboBox.TabIndex = 8;
            // 
            // spatialQueryCheckBox
            // 
            this.spatialQueryCheckBox.AutoSize = true;
            this.spatialQueryCheckBox.Location = new System.Drawing.Point(13, 73);
            this.spatialQueryCheckBox.Name = "spatialQueryCheckBox";
            this.spatialQueryCheckBox.Size = new System.Drawing.Size(121, 17);
            this.spatialQueryCheckBox.TabIndex = 9;
            this.spatialQueryCheckBox.Text = "Enable spatial query";
            this.spatialQueryCheckBox.UseVisualStyleBackColor = true;
            this.spatialQueryCheckBox.CheckedChanged += new System.EventHandler(this.spatialQueryCheckBox_CheckedChanged);
            // 
            // DefinitonQueryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(526, 208);
            this.Controls.Add(this.spatialQueryCheckBox);
            this.Controls.Add(this.relationComboBox);
            this.Controls.Add(this.panelQuery2);
            this.Controls.Add(this.panelQuery);
            this.Controls.Add(this.btnFilter);
            this.Name = "DefinitonQueryForm";
            this.Text = "DefinitonQueryForm";
            this.Load += new System.EventHandler(this.DefinitonQueryForm_Load);
            this.panelQuery.ResumeLayout(false);
            this.panelQuery.PerformLayout();
            this.panelQuery2.ResumeLayout(false);
            this.panelQuery2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxAttributes;
        private System.Windows.Forms.ComboBox comboBoxQuery;
        private System.Windows.Forms.TextBox textBoxValue;
        private System.Windows.Forms.ComboBox comboBoxLayer;
        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.Panel panelQuery;
        private System.Windows.Forms.Button btnAddCondition;
        private System.Windows.Forms.Panel panelQuery2;
        private System.Windows.Forms.Button btnAddCondition2;
        private System.Windows.Forms.ComboBox comboBoxLayer2;
        private System.Windows.Forms.ComboBox comboBoxAttributes2;
        private System.Windows.Forms.ComboBox comboBoxQuery2;
        private System.Windows.Forms.TextBox textBoxValue2;
        private System.Windows.Forms.ComboBox relationComboBox;
        private System.Windows.Forms.CheckBox spatialQueryCheckBox;
    }
}