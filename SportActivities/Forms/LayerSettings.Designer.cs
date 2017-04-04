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
            this.comboboxLabel = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.labelLayerName = new System.Windows.Forms.Label();
            this.buttonApply = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.lbLayerName = new System.Windows.Forms.Label();
            this.lblOutlineColor = new System.Windows.Forms.Label();
            this.lblOutlineWidth = new System.Windows.Forms.Label();
            this.nudOutlineWidth = new System.Windows.Forms.NumericUpDown();
            this.lblGeometryColor = new System.Windows.Forms.Label();
            this.nudPointSize = new System.Windows.Forms.NumericUpDown();
            this.lblPointSize = new System.Windows.Forms.Label();
            this.cdColor = new System.Windows.Forms.ColorDialog();
            this.btnGeometryColor = new System.Windows.Forms.Button();
            this.btnOutlineColor = new System.Windows.Forms.Button();
            this.btnLabelColor = new System.Windows.Forms.Button();
            this.lblLabelColor = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudOutlineWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPointSize)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Layer label:";
            // 
            // comboboxLabel
            // 
            this.comboboxLabel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboboxLabel.FormattingEnabled = true;
            this.comboboxLabel.Location = new System.Drawing.Point(114, 42);
            this.comboboxLabel.Name = "comboboxLabel";
            this.comboboxLabel.Size = new System.Drawing.Size(100, 21);
            this.comboboxLabel.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Layer name:";
            // 
            // labelLayerName
            // 
            this.labelLayerName.AutoSize = true;
            this.labelLayerName.Location = new System.Drawing.Point(111, 9);
            this.labelLayerName.Name = "labelLayerName";
            this.labelLayerName.Size = new System.Drawing.Size(0, 13);
            this.labelLayerName.TabIndex = 3;
            // 
            // buttonApply
            // 
            this.buttonApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonApply.Location = new System.Drawing.Point(148, 296);
            this.buttonApply.Name = "buttonApply";
            this.buttonApply.Size = new System.Drawing.Size(75, 23);
            this.buttonApply.TabIndex = 4;
            this.buttonApply.Text = "Apply";
            this.buttonApply.UseVisualStyleBackColor = true;
            this.buttonApply.Click += new System.EventHandler(this.buttonApply_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.Location = new System.Drawing.Point(67, 296);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 5;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // lbLayerName
            // 
            this.lbLayerName.AutoSize = true;
            this.lbLayerName.Location = new System.Drawing.Point(114, 9);
            this.lbLayerName.Name = "lbLayerName";
            this.lbLayerName.Size = new System.Drawing.Size(0, 13);
            this.lbLayerName.TabIndex = 6;
            // 
            // lblOutlineColor
            // 
            this.lblOutlineColor.AutoSize = true;
            this.lblOutlineColor.Location = new System.Drawing.Point(12, 170);
            this.lblOutlineColor.Name = "lblOutlineColor";
            this.lblOutlineColor.Size = new System.Drawing.Size(69, 13);
            this.lblOutlineColor.TabIndex = 7;
            this.lblOutlineColor.Text = "Outline color:";
            // 
            // lblOutlineWidth
            // 
            this.lblOutlineWidth.AutoSize = true;
            this.lblOutlineWidth.Location = new System.Drawing.Point(12, 209);
            this.lblOutlineWidth.Name = "lblOutlineWidth";
            this.lblOutlineWidth.Size = new System.Drawing.Size(71, 13);
            this.lblOutlineWidth.TabIndex = 8;
            this.lblOutlineWidth.Text = "Outline width:";
            // 
            // nudOutlineWidth
            // 
            this.nudOutlineWidth.Location = new System.Drawing.Point(114, 207);
            this.nudOutlineWidth.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudOutlineWidth.Name = "nudOutlineWidth";
            this.nudOutlineWidth.ReadOnly = true;
            this.nudOutlineWidth.Size = new System.Drawing.Size(100, 20);
            this.nudOutlineWidth.TabIndex = 9;
            // 
            // lblGeometryColor
            // 
            this.lblGeometryColor.AutoSize = true;
            this.lblGeometryColor.Location = new System.Drawing.Point(12, 128);
            this.lblGeometryColor.Name = "lblGeometryColor";
            this.lblGeometryColor.Size = new System.Drawing.Size(60, 13);
            this.lblGeometryColor.TabIndex = 10;
            this.lblGeometryColor.Text = "Point color:";
            // 
            // nudPointSize
            // 
            this.nudPointSize.Location = new System.Drawing.Point(114, 246);
            this.nudPointSize.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudPointSize.Name = "nudPointSize";
            this.nudPointSize.ReadOnly = true;
            this.nudPointSize.Size = new System.Drawing.Size(100, 20);
            this.nudPointSize.TabIndex = 12;
            this.nudPointSize.Visible = false;
            // 
            // lblPointSize
            // 
            this.lblPointSize.AutoSize = true;
            this.lblPointSize.Location = new System.Drawing.Point(12, 248);
            this.lblPointSize.Name = "lblPointSize";
            this.lblPointSize.Size = new System.Drawing.Size(55, 13);
            this.lblPointSize.TabIndex = 11;
            this.lblPointSize.Text = "Point size:";
            this.lblPointSize.Visible = false;
            // 
            // btnGeometryColor
            // 
            this.btnGeometryColor.Location = new System.Drawing.Point(114, 123);
            this.btnGeometryColor.Name = "btnGeometryColor";
            this.btnGeometryColor.Size = new System.Drawing.Size(100, 23);
            this.btnGeometryColor.TabIndex = 13;
            this.btnGeometryColor.Text = "Choose";
            this.btnGeometryColor.UseVisualStyleBackColor = true;
            this.btnGeometryColor.Click += new System.EventHandler(this.btnGeometryColor_Click);
            // 
            // btnOutlineColor
            // 
            this.btnOutlineColor.Location = new System.Drawing.Point(114, 165);
            this.btnOutlineColor.Name = "btnOutlineColor";
            this.btnOutlineColor.Size = new System.Drawing.Size(100, 23);
            this.btnOutlineColor.TabIndex = 14;
            this.btnOutlineColor.Text = "Choose";
            this.btnOutlineColor.UseVisualStyleBackColor = true;
            this.btnOutlineColor.Click += new System.EventHandler(this.btnOutlineColor_Click);
            // 
            // btnLabelColor
            // 
            this.btnLabelColor.Location = new System.Drawing.Point(114, 81);
            this.btnLabelColor.Name = "btnLabelColor";
            this.btnLabelColor.Size = new System.Drawing.Size(100, 23);
            this.btnLabelColor.TabIndex = 16;
            this.btnLabelColor.Text = "Choose";
            this.btnLabelColor.UseVisualStyleBackColor = true;
            this.btnLabelColor.Click += new System.EventHandler(this.btnLabelColor_Click);
            // 
            // lblLabelColor
            // 
            this.lblLabelColor.AutoSize = true;
            this.lblLabelColor.Location = new System.Drawing.Point(12, 86);
            this.lblLabelColor.Name = "lblLabelColor";
            this.lblLabelColor.Size = new System.Drawing.Size(62, 13);
            this.lblLabelColor.TabIndex = 15;
            this.lblLabelColor.Text = "Label color:";
            // 
            // LayerSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(235, 331);
            this.Controls.Add(this.btnLabelColor);
            this.Controls.Add(this.lblLabelColor);
            this.Controls.Add(this.btnOutlineColor);
            this.Controls.Add(this.btnGeometryColor);
            this.Controls.Add(this.nudPointSize);
            this.Controls.Add(this.lblPointSize);
            this.Controls.Add(this.lblGeometryColor);
            this.Controls.Add(this.nudOutlineWidth);
            this.Controls.Add(this.lblOutlineWidth);
            this.Controls.Add(this.lblOutlineColor);
            this.Controls.Add(this.lbLayerName);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonApply);
            this.Controls.Add(this.labelLayerName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboboxLabel);
            this.Controls.Add(this.label1);
            this.Name = "LayerSettings";
            this.Text = " ";
            ((System.ComponentModel.ISupportInitialize)(this.nudOutlineWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPointSize)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboboxLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelLayerName;
        private System.Windows.Forms.Button buttonApply;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label lbLayerName;
        private System.Windows.Forms.Label lblOutlineColor;
        private System.Windows.Forms.Label lblOutlineWidth;
        private System.Windows.Forms.NumericUpDown nudOutlineWidth;
        private System.Windows.Forms.Label lblGeometryColor;
        private System.Windows.Forms.NumericUpDown nudPointSize;
        private System.Windows.Forms.Label lblPointSize;
        private System.Windows.Forms.ColorDialog cdColor;
        private System.Windows.Forms.Button btnGeometryColor;
        private System.Windows.Forms.Button btnOutlineColor;
        private System.Windows.Forms.Button btnLabelColor;
        private System.Windows.Forms.Label lblLabelColor;
    }
}