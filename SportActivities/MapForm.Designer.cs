namespace SportActivities
{
    partial class MapForm
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
            this.components = new System.ComponentModel.Container();
            this.mapBox = new SharpMap.Forms.MapBox();
            this.layersTreeView = new System.Windows.Forms.TreeView();
            this.mapQueryToolStrip1 = new SharpMap.Forms.ToolBar.MapQueryToolStrip(this.components);
            this.mapVariableLayerToolStrip1 = new SharpMap.Forms.ToolBar.MapVariableLayerToolStrip(this.components);
            this.SuspendLayout();
            // 
            // mapBox
            // 
            this.mapBox.ActiveTool = SharpMap.Forms.MapBox.Tools.None;
            this.mapBox.BackColor = System.Drawing.Color.White;
            this.mapBox.Cursor = System.Windows.Forms.Cursors.Default;
            this.mapBox.FineZoomFactor = 10D;
            this.mapBox.Location = new System.Drawing.Point(266, 50);
            this.mapBox.MapQueryMode = SharpMap.Forms.MapBox.MapQueryType.LayerByIndex;
            this.mapBox.Name = "mapBox";
            this.mapBox.QueryGrowFactor = 5F;
            this.mapBox.QueryLayerIndex = 0;
            this.mapBox.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            this.mapBox.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            this.mapBox.ShowProgressUpdate = false;
            this.mapBox.Size = new System.Drawing.Size(818, 689);
            this.mapBox.TabIndex = 0;
            this.mapBox.Text = "mapBox1";
            this.mapBox.WheelZoomMagnitude = -2D;
            // 
            // layersTreeView
            // 
            this.layersTreeView.CheckBoxes = true;
            this.layersTreeView.Location = new System.Drawing.Point(13, 50);
            this.layersTreeView.Name = "layersTreeView";
            this.layersTreeView.Size = new System.Drawing.Size(247, 350);
            this.layersTreeView.TabIndex = 1;
            // 
            // mapQueryToolStrip1
            // 
            this.mapQueryToolStrip1.Enabled = false;
            this.mapQueryToolStrip1.Location = new System.Drawing.Point(0, 0);
            this.mapQueryToolStrip1.MapControl = null;
            this.mapQueryToolStrip1.Name = "mapQueryToolStrip1";
            this.mapQueryToolStrip1.Size = new System.Drawing.Size(1096, 25);
            this.mapQueryToolStrip1.TabIndex = 2;
            this.mapQueryToolStrip1.Text = "mapQueryToolStrip1";
            this.mapQueryToolStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.mapQueryToolStrip1_ItemClicked);
            // 
            // mapVariableLayerToolStrip1
            // 
            this.mapVariableLayerToolStrip1.Enabled = false;
            this.mapVariableLayerToolStrip1.Location = new System.Drawing.Point(0, 25);
            this.mapVariableLayerToolStrip1.MapControl = null;
            this.mapVariableLayerToolStrip1.Name = "mapVariableLayerToolStrip1";
            this.mapVariableLayerToolStrip1.Size = new System.Drawing.Size(1096, 25);
            this.mapVariableLayerToolStrip1.TabIndex = 3;
            this.mapVariableLayerToolStrip1.Text = "mapVariableLayerToolStrip1";
            // 
            // MapForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1096, 751);
            this.Controls.Add(this.mapVariableLayerToolStrip1);
            this.Controls.Add(this.mapQueryToolStrip1);
            this.Controls.Add(this.layersTreeView);
            this.Controls.Add(this.mapBox);
            this.Name = "MapForm";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SharpMap.Forms.MapBox mapBox;
        private System.Windows.Forms.TreeView layersTreeView;
        private SharpMap.Forms.ToolBar.MapQueryToolStrip mapQueryToolStrip1;
        private SharpMap.Forms.ToolBar.MapVariableLayerToolStrip mapVariableLayerToolStrip1;
    }
}

