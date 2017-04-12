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
            this.panelQuery.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBoxAttributes
            // 
            this.comboBoxAttributes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAttributes.FormattingEnabled = true;
            this.comboBoxAttributes.Location = new System.Drawing.Point(134, 16);
            this.comboBoxAttributes.Name = "comboBoxAttributes";
            this.comboBoxAttributes.Size = new System.Drawing.Size(121, 21);
            this.comboBoxAttributes.TabIndex = 0;
            // 
            // comboBoxQuery
            // 
            this.comboBoxQuery.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxQuery.FormattingEnabled = true;
            this.comboBoxQuery.Location = new System.Drawing.Point(273, 16);
            this.comboBoxQuery.Name = "comboBoxQuery";
            this.comboBoxQuery.Size = new System.Drawing.Size(81, 21);
            this.comboBoxQuery.TabIndex = 1;
            // 
            // textBoxValue
            // 
            this.textBoxValue.Location = new System.Drawing.Point(372, 16);
            this.textBoxValue.Name = "textBoxValue";
            this.textBoxValue.Size = new System.Drawing.Size(155, 20);
            this.textBoxValue.TabIndex = 2;
            // 
            // comboBoxLayer
            // 
            this.comboBoxLayer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLayer.FormattingEnabled = true;
            this.comboBoxLayer.Location = new System.Drawing.Point(3, 16);
            this.comboBoxLayer.Name = "comboBoxLayer";
            this.comboBoxLayer.Size = new System.Drawing.Size(111, 21);
            this.comboBoxLayer.TabIndex = 4;
            this.comboBoxLayer.SelectedIndexChanged += new System.EventHandler(this.comboBoxLayer_SelectedIndexChanged);
            // 
            // btnFilter
            // 
            this.btnFilter.Location = new System.Drawing.Point(464, 71);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(75, 23);
            this.btnFilter.TabIndex = 5;
            this.btnFilter.Text = "Filter";
            this.btnFilter.UseVisualStyleBackColor = true;
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // panelQuery
            // 
            this.panelQuery.Controls.Add(this.comboBoxLayer);
            this.panelQuery.Controls.Add(this.comboBoxAttributes);
            this.panelQuery.Controls.Add(this.comboBoxQuery);
            this.panelQuery.Controls.Add(this.textBoxValue);
            this.panelQuery.Location = new System.Drawing.Point(12, 12);
            this.panelQuery.Name = "panelQuery";
            this.panelQuery.Size = new System.Drawing.Size(536, 53);
            this.panelQuery.TabIndex = 6;
            // 
            // DefinitonQueryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(560, 113);
            this.Controls.Add(this.panelQuery);
            this.Controls.Add(this.btnFilter);
            this.Name = "DefinitonQueryForm";
            this.Text = "DefinitonQueryForm";
            this.panelQuery.ResumeLayout(false);
            this.panelQuery.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxAttributes;
        private System.Windows.Forms.ComboBox comboBoxQuery;
        private System.Windows.Forms.TextBox textBoxValue;
        private System.Windows.Forms.ComboBox comboBoxLayer;
        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.Panel panelQuery;
    }
}