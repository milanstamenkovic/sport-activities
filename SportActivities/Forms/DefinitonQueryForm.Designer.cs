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
            this.button1 = new System.Windows.Forms.Button();
            this.comboBoxLayer = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // comboBoxAttributes
            // 
            this.comboBoxAttributes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAttributes.FormattingEnabled = true;
            this.comboBoxAttributes.Location = new System.Drawing.Point(144, 12);
            this.comboBoxAttributes.Name = "comboBoxAttributes";
            this.comboBoxAttributes.Size = new System.Drawing.Size(121, 21);
            this.comboBoxAttributes.TabIndex = 0;
            // 
            // comboBoxQuery
            // 
            this.comboBoxQuery.FormattingEnabled = true;
            this.comboBoxQuery.Location = new System.Drawing.Point(283, 12);
            this.comboBoxQuery.Name = "comboBoxQuery";
            this.comboBoxQuery.Size = new System.Drawing.Size(81, 21);
            this.comboBoxQuery.TabIndex = 1;
            // 
            // textBoxValue
            // 
            this.textBoxValue.Location = new System.Drawing.Point(382, 12);
            this.textBoxValue.Name = "textBoxValue";
            this.textBoxValue.Size = new System.Drawing.Size(155, 20);
            this.textBoxValue.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(552, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // comboBoxLayer
            // 
            this.comboBoxLayer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLayer.FormattingEnabled = true;
            this.comboBoxLayer.Location = new System.Drawing.Point(13, 12);
            this.comboBoxLayer.Name = "comboBoxLayer";
            this.comboBoxLayer.Size = new System.Drawing.Size(111, 21);
            this.comboBoxLayer.TabIndex = 4;
            // 
            // DefinitonQueryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(639, 47);
            this.Controls.Add(this.comboBoxLayer);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBoxValue);
            this.Controls.Add(this.comboBoxQuery);
            this.Controls.Add(this.comboBoxAttributes);
            this.Name = "DefinitonQueryForm";
            this.Text = "DefinitonQueryForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxAttributes;
        private System.Windows.Forms.ComboBox comboBoxQuery;
        private System.Windows.Forms.TextBox textBoxValue;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox comboBoxLayer;
    }
}