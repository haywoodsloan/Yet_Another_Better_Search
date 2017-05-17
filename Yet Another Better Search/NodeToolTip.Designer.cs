namespace Yet_Another_Better_Search
{
    partial class NodeToolTip
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
            this.toolTipText = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // toolTipText
            // 
            this.toolTipText.AutoSize = true;
            this.toolTipText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolTipText.Location = new System.Drawing.Point(0, 0);
            this.toolTipText.Name = "toolTipText";
            this.toolTipText.Size = new System.Drawing.Size(65, 13);
            this.toolTipText.TabIndex = 0;
            this.toolTipText.Text = "Default Text";
            // 
            // NodeToolTip
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(66, 17);
            this.Controls.Add(this.toolTipText);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "NodeToolTip";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "NodeToolTip";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label toolTipText;
    }
}