namespace SportActivities
{
    partial class LayerSettings
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
            this.label1 = new System.Windows.Forms.Label();
            this.layerLabelCombobox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Layer label:";
            // 
            // layerLabelCombobox
            // 
            this.layerLabelCombobox.FormattingEnabled = true;
            this.layerLabelCombobox.Location = new System.Drawing.Point(81, 13);
            this.layerLabelCombobox.Name = "layerLabelCombobox";
            this.layerLabelCombobox.Size = new System.Drawing.Size(121, 21);
            this.layerLabelCombobox.TabIndex = 1;
            // 
            // LayerSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(421, 417);
            this.Controls.Add(this.layerLabelCombobox);
            this.Controls.Add(this.label1);
            this.Name = "LayerSettings";
            this.Text = "LayerSettings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox layerLabelCombobox;
    }
}