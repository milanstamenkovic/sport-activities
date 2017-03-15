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
            this.mapBox = new SharpMap.Forms.MapBox();
            this.layersTreeView = new System.Windows.Forms.TreeView();
            this.mapCheckBox = new System.Windows.Forms.CheckBox();
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
            this.layersTreeView.AllowDrop = true;
            this.layersTreeView.CheckBoxes = true;
            this.layersTreeView.Location = new System.Drawing.Point(13, 70);
            this.layersTreeView.Name = "layersTreeView";
            this.layersTreeView.Size = new System.Drawing.Size(247, 330);
            this.layersTreeView.TabIndex = 1;
            this.layersTreeView.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.layersTreeView_AfterCheck);
            this.layersTreeView.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.layersTreeView_ItemDrag);
            this.layersTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.layersTreeView_AfterSelect);
            this.layersTreeView.DragDrop += new System.Windows.Forms.DragEventHandler(this.layersTreeView_DragDrop);
            this.layersTreeView.DragEnter += new System.Windows.Forms.DragEventHandler(this.layersTreeView_DragEnter);
            // 
            // mapCheckBox
            // 
            this.mapCheckBox.AutoSize = true;
            this.mapCheckBox.Checked = true;
            this.mapCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mapCheckBox.Location = new System.Drawing.Point(13, 50);
            this.mapCheckBox.Name = "mapCheckBox";
            this.mapCheckBox.Size = new System.Drawing.Size(53, 17);
            this.mapCheckBox.TabIndex = 2;
            this.mapCheckBox.Text = "Mapa";
            this.mapCheckBox.UseVisualStyleBackColor = true;
            this.mapCheckBox.CheckedChanged += new System.EventHandler(this.mapCheckBox_CheckedChanged);
            // 
            // MapForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1096, 742);
            this.Controls.Add(this.mapCheckBox);
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
        private System.Windows.Forms.CheckBox mapCheckBox;
    }
}

