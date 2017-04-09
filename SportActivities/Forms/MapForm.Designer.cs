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
            this.label1 = new System.Windows.Forms.Label();
            this.labelMouseCoords = new System.Windows.Forms.Label();
            this.btnShowLabels = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.noneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zoomInToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zoomOutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zoomPolygonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.drawPointToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.drawLineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.drawPolygonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.queryBoxToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label2 = new System.Windows.Forms.Label();
            this.activeToolLabel = new System.Windows.Forms.Label();
            this.btnRouting = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mapBox
            // 
            this.mapBox.ActiveTool = SharpMap.Forms.MapBox.Tools.None;
            this.mapBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mapBox.BackColor = System.Drawing.SystemColors.ActiveBorder;
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
            this.mapBox.MouseMove += new SharpMap.Forms.MapBox.MouseEventHandler(this.mapBox_MouseMove);
            this.mapBox.GeometryDefined += new SharpMap.Forms.MapBox.GeometryDefinedHandler(this.mapBox_GeometryDefined);
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
            this.layersTreeView.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.layersTreeView_NodeMouseDoubleClick);
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
            this.mapCheckBox.Size = new System.Drawing.Size(47, 17);
            this.mapCheckBox.TabIndex = 2;
            this.mapCheckBox.Text = "Map";
            this.mapCheckBox.UseVisualStyleBackColor = true;
            this.mapCheckBox.CheckedChanged += new System.EventHandler(this.mapCheckBox_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 407);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Coordinates:";
            // 
            // labelMouseCoords
            // 
            this.labelMouseCoords.AutoSize = true;
            this.labelMouseCoords.Location = new System.Drawing.Point(13, 429);
            this.labelMouseCoords.Name = "labelMouseCoords";
            this.labelMouseCoords.Size = new System.Drawing.Size(27, 13);
            this.labelMouseCoords.TabIndex = 5;
            this.labelMouseCoords.Text = "N/A";
            // 
            // btnShowLabels
            // 
            this.btnShowLabels.Location = new System.Drawing.Point(16, 457);
            this.btnShowLabels.Name = "btnShowLabels";
            this.btnShowLabels.Size = new System.Drawing.Size(99, 23);
            this.btnShowLabels.TabIndex = 6;
            this.btnShowLabels.Text = "Show labels";
            this.btnShowLabels.UseVisualStyleBackColor = true;
            this.btnShowLabels.Click += new System.EventHandler(this.btnShowLabels_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1096, 24);
            this.menuStrip1.TabIndex = 9;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.Checked = true;
            this.toolsToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.noneToolStripMenuItem,
            this.panToolStripMenuItem,
            this.zoomInToolStripMenuItem,
            this.zoomOutToolStripMenuItem,
            this.zoomPolygonToolStripMenuItem,
            this.drawPointToolStripMenuItem,
            this.drawLineToolStripMenuItem,
            this.drawPolygonToolStripMenuItem,
            this.queryBoxToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            this.toolsToolStripMenuItem.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolsToolStripMenuItem_DropDownItemClicked);
            // 
            // noneToolStripMenuItem
            // 
            this.noneToolStripMenuItem.Checked = true;
            this.noneToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.noneToolStripMenuItem.Name = "noneToolStripMenuItem";
            this.noneToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.noneToolStripMenuItem.Tag = "9";
            this.noneToolStripMenuItem.Text = "None";
            // 
            // panToolStripMenuItem
            // 
            this.panToolStripMenuItem.Name = "panToolStripMenuItem";
            this.panToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.panToolStripMenuItem.Tag = "0";
            this.panToolStripMenuItem.Text = "Pan";
            // 
            // zoomInToolStripMenuItem
            // 
            this.zoomInToolStripMenuItem.Name = "zoomInToolStripMenuItem";
            this.zoomInToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.zoomInToolStripMenuItem.Tag = "1";
            this.zoomInToolStripMenuItem.Text = "Zoom In";
            // 
            // zoomOutToolStripMenuItem
            // 
            this.zoomOutToolStripMenuItem.Name = "zoomOutToolStripMenuItem";
            this.zoomOutToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.zoomOutToolStripMenuItem.Tag = "2";
            this.zoomOutToolStripMenuItem.Text = "Zoom Out";
            // 
            // zoomPolygonToolStripMenuItem
            // 
            this.zoomPolygonToolStripMenuItem.Name = "zoomPolygonToolStripMenuItem";
            this.zoomPolygonToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.zoomPolygonToolStripMenuItem.Tag = "5";
            this.zoomPolygonToolStripMenuItem.Text = "Zoom Window";
            // 
            // drawPointToolStripMenuItem
            // 
            this.drawPointToolStripMenuItem.Name = "drawPointToolStripMenuItem";
            this.drawPointToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.drawPointToolStripMenuItem.Tag = "6";
            this.drawPointToolStripMenuItem.Text = "Draw Point";
            // 
            // drawLineToolStripMenuItem
            // 
            this.drawLineToolStripMenuItem.Name = "drawLineToolStripMenuItem";
            this.drawLineToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.drawLineToolStripMenuItem.Tag = "7";
            this.drawLineToolStripMenuItem.Text = "Draw Line";
            // 
            // drawPolygonToolStripMenuItem
            // 
            this.drawPolygonToolStripMenuItem.Name = "drawPolygonToolStripMenuItem";
            this.drawPolygonToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.drawPolygonToolStripMenuItem.Tag = "8";
            this.drawPolygonToolStripMenuItem.Text = "Draw Polygon";
            // 
            // queryBoxToolStripMenuItem
            // 
            this.queryBoxToolStripMenuItem.Name = "queryBoxToolStripMenuItem";
            this.queryBoxToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.queryBoxToolStripMenuItem.Tag = "3";
            this.queryBoxToolStripMenuItem.Text = "Query Box";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Active Tool:";
            // 
            // activeToolLabel
            // 
            this.activeToolLabel.AutoSize = true;
            this.activeToolLabel.BackColor = System.Drawing.Color.PaleGreen;
            this.activeToolLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.activeToolLabel.Cursor = System.Windows.Forms.Cursors.Default;
            this.activeToolLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.activeToolLabel.Location = new System.Drawing.Point(83, 21);
            this.activeToolLabel.Name = "activeToolLabel";
            this.activeToolLabel.Padding = new System.Windows.Forms.Padding(5);
            this.activeToolLabel.Size = new System.Drawing.Size(45, 25);
            this.activeToolLabel.TabIndex = 11;
            this.activeToolLabel.Text = "None";
            // 
            // btnRouting
            // 
            this.btnRouting.Location = new System.Drawing.Point(16, 504);
            this.btnRouting.Name = "btnRouting";
            this.btnRouting.Size = new System.Drawing.Size(99, 23);
            this.btnRouting.TabIndex = 12;
            this.btnRouting.Text = "Routing";
            this.btnRouting.UseVisualStyleBackColor = true;
            this.btnRouting.Click += new System.EventHandler(this.btnRouting_Click);
            // 
            // MapForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1096, 742);
            this.Controls.Add(this.btnRouting);
            this.Controls.Add(this.activeToolLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnShowLabels);
            this.Controls.Add(this.mapCheckBox);
            this.Controls.Add(this.labelMouseCoords);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.layersTreeView);
            this.Controls.Add(this.mapBox);
            this.Controls.Add(this.menuStrip1);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MapForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Text = "Sport activities";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SharpMap.Forms.MapBox mapBox;
        private System.Windows.Forms.TreeView layersTreeView;
        private System.Windows.Forms.CheckBox mapCheckBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelMouseCoords;
        private System.Windows.Forms.Button btnShowLabels;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem panToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zoomInToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zoomOutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zoomPolygonToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem noneToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label activeToolLabel;
        private System.Windows.Forms.ToolStripMenuItem drawPolygonToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem drawPointToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem drawLineToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem queryBoxToolStripMenuItem;
        private System.Windows.Forms.Button btnRouting;
    }
}

