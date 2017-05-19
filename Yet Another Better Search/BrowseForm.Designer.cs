namespace Yet_Another_Better_Search
{
    partial class BrowseForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BrowseForm));
            this.label1 = new System.Windows.Forms.Label();
            this.rootSelect = new System.Windows.Forms.FolderBrowserDialog();
            this.rootPathText = new System.Windows.Forms.TextBox();
            this.browseBtn = new System.Windows.Forms.Button();
            this.resultTree = new System.Windows.Forms.TreeView();
            this.resultTreeImageList = new System.Windows.Forms.ImageList(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.unlimitedDepthCheck = new System.Windows.Forms.CheckBox();
            this.searchDepthValue = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.nodeContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openLocationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filePropertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectBtn = new System.Windows.Forms.Button();
            this.notBrowsedContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.browseHereToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.searchDepthValue)).BeginInit();
            this.nodeContextMenu.SuspendLayout();
            this.notBrowsedContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Browse Root:";
            // 
            // rootSelect
            // 
            this.rootSelect.Description = "Select the root folder for the file browser.";
            // 
            // rootPathText
            // 
            this.rootPathText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rootPathText.Location = new System.Drawing.Point(12, 25);
            this.rootPathText.Name = "rootPathText";
            this.rootPathText.Size = new System.Drawing.Size(460, 20);
            this.rootPathText.TabIndex = 1;
            this.rootPathText.Text = "Select a folder, then click browse";
            // 
            // browseBtn
            // 
            this.browseBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.browseBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.browseBtn.Location = new System.Drawing.Point(531, 23);
            this.browseBtn.Name = "browseBtn";
            this.browseBtn.Size = new System.Drawing.Size(52, 23);
            this.browseBtn.TabIndex = 2;
            this.browseBtn.Text = "Browse";
            this.browseBtn.UseVisualStyleBackColor = true;
            this.browseBtn.Click += new System.EventHandler(this.browseBtn_OnClick);
            // 
            // resultTree
            // 
            this.resultTree.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.resultTree.ImageIndex = 0;
            this.resultTree.ImageList = this.resultTreeImageList;
            this.resultTree.Location = new System.Drawing.Point(12, 70);
            this.resultTree.Name = "resultTree";
            this.resultTree.SelectedImageIndex = 0;
            this.resultTree.Size = new System.Drawing.Size(571, 398);
            this.resultTree.TabIndex = 3;
            this.resultTree.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.resultTree_OnMouseClick);
            this.resultTree.MouseMove += new System.Windows.Forms.MouseEventHandler(this.resultTree_OnMouseMove);
            // 
            // resultTreeImageList
            // 
            this.resultTreeImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("resultTreeImageList.ImageStream")));
            this.resultTreeImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.resultTreeImageList.Images.SetKeyName(0, "warning.ico");
            this.resultTreeImageList.Images.SetKeyName(1, "forbidden.ico");
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 54);
            this.label2.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Results:";
            // 
            // unlimitedDepthCheck
            // 
            this.unlimitedDepthCheck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.unlimitedDepthCheck.AutoSize = true;
            this.unlimitedDepthCheck.Checked = true;
            this.unlimitedDepthCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.unlimitedDepthCheck.Location = new System.Drawing.Point(514, 50);
            this.unlimitedDepthCheck.Name = "unlimitedDepthCheck";
            this.unlimitedDepthCheck.Size = new System.Drawing.Size(69, 17);
            this.unlimitedDepthCheck.TabIndex = 5;
            this.unlimitedDepthCheck.Text = "Unlimited";
            this.unlimitedDepthCheck.UseVisualStyleBackColor = true;
            this.unlimitedDepthCheck.CheckStateChanged += new System.EventHandler(this.unlimitedDepthCheck_OnCheck);
            // 
            // searchDepthValue
            // 
            this.searchDepthValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.searchDepthValue.Enabled = false;
            this.searchDepthValue.Location = new System.Drawing.Point(468, 48);
            this.searchDepthValue.Name = "searchDepthValue";
            this.searchDepthValue.Size = new System.Drawing.Size(40, 20);
            this.searchDepthValue.TabIndex = 6;
            this.searchDepthValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.searchDepthValue.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(386, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Search Depth:";
            // 
            // nodeContextMenu
            // 
            this.nodeContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openLocationToolStripMenuItem,
            this.filePropertiesToolStripMenuItem});
            this.nodeContextMenu.Name = "nodeContextMenu";
            this.nodeContextMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.nodeContextMenu.Size = new System.Drawing.Size(150, 48);
            // 
            // openLocationToolStripMenuItem
            // 
            this.openLocationToolStripMenuItem.Name = "openLocationToolStripMenuItem";
            this.openLocationToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.openLocationToolStripMenuItem.Text = "Open location";
            this.openLocationToolStripMenuItem.Click += new System.EventHandler(this.openLocation_OnClick);
            // 
            // filePropertiesToolStripMenuItem
            // 
            this.filePropertiesToolStripMenuItem.Name = "filePropertiesToolStripMenuItem";
            this.filePropertiesToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.filePropertiesToolStripMenuItem.Text = "File properties";
            this.filePropertiesToolStripMenuItem.Click += new System.EventHandler(this.fileProperties_OnClick);
            // 
            // selectBtn
            // 
            this.selectBtn.AutoSize = true;
            this.selectBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.selectBtn.Location = new System.Drawing.Point(478, 23);
            this.selectBtn.Name = "selectBtn";
            this.selectBtn.Size = new System.Drawing.Size(47, 23);
            this.selectBtn.TabIndex = 8;
            this.selectBtn.Text = "Select";
            this.selectBtn.UseVisualStyleBackColor = true;
            this.selectBtn.Click += new System.EventHandler(this.selectBtn_OnClick);
            // 
            // notBrowsedContextMenu
            // 
            this.notBrowsedContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.browseHereToolStripMenuItem});
            this.notBrowsedContextMenu.Name = "notBrowsedContextMenu";
            this.notBrowsedContextMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.notBrowsedContextMenu.Size = new System.Drawing.Size(139, 26);
            // 
            // browseHereToolStripMenuItem
            // 
            this.browseHereToolStripMenuItem.Name = "browseHereToolStripMenuItem";
            this.browseHereToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.browseHereToolStripMenuItem.Text = "Browse here";
            this.browseHereToolStripMenuItem.Click += new System.EventHandler(this.browseHere_OnClick);
            // 
            // BrowseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(595, 480);
            this.Controls.Add(this.selectBtn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.searchDepthValue);
            this.Controls.Add(this.unlimitedDepthCheck);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.resultTree);
            this.Controls.Add(this.browseBtn);
            this.Controls.Add(this.rootPathText);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "BrowseForm";
            this.Text = "Yet Another Better Search";
            ((System.ComponentModel.ISupportInitialize)(this.searchDepthValue)).EndInit();
            this.nodeContextMenu.ResumeLayout(false);
            this.notBrowsedContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FolderBrowserDialog rootSelect;
        private System.Windows.Forms.TextBox rootPathText;
        private System.Windows.Forms.Button browseBtn;
        private System.Windows.Forms.TreeView resultTree;
        private System.Windows.Forms.ImageList resultTreeImageList;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox unlimitedDepthCheck;
        private System.Windows.Forms.NumericUpDown searchDepthValue;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ContextMenuStrip nodeContextMenu;
        private System.Windows.Forms.ToolStripMenuItem openLocationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem filePropertiesToolStripMenuItem;
        private System.Windows.Forms.Button selectBtn;
        private System.Windows.Forms.ContextMenuStrip notBrowsedContextMenu;
        private System.Windows.Forms.ToolStripMenuItem browseHereToolStripMenuItem;
    }
}

