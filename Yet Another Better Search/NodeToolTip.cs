using System;
using System.Windows.Forms;
using System.Drawing;
using System.Timers;

namespace Yet_Another_Better_Search
{
    public partial class NodeToolTip : Form
    {
        public NodeToolTip()
        {
            InitializeComponent();

            toolTipTimer.AutoReset = false;
            toolTipTimer.Elapsed += toolTipTimer_Elapsed;

            CreateHandle();
        }
        
        public void ResetToolTipTimer()
        {
            if (Visible)
            {
                BeginInvoke(new Action(() => Hide()));
            }

            toolTipTimer.Stop();
            DisplayedNode = null;
        }

        public void BeginToolTipTimer(TreeNode targetNode)
        {
            if(targetNode == null)
            {
                ResetToolTipTimer();
            }
            else if (DisplayedNode != targetNode)
            {
                if (Visible)
                {
                    BeginInvoke(new Action(() => Hide()));
                }

                DisplayedNode = targetNode;
                toolTipTimer.Start();
            }
        }
        
        private void toolTipTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
           showToolTip();
        }

        private delegate void showToolTipDelegate();
        private void showToolTip()
        {
            if(InvokeRequired)
            {
                showToolTipDelegate callback = new showToolTipDelegate(showToolTip);
                BeginInvoke(callback);
            }
            else
            {
                if (DisplayedNode.Tag.GetType() == typeof(MinimalDirectoryInfo))
                {
                    MinimalDirectoryInfo dirInfo = (MinimalDirectoryInfo)DisplayedNode.Tag;
                    toolTipText.Text = $"{BrowseForm.getNodeFilePath(DisplayedNode)}" +
                        $"\nCreated: {dirInfo.CreationTime.ToString()}" +
                        $"\nModified: {dirInfo.LastWriteTime.ToString()}";
                }
                else
                {
                    MinimalFileInfo fileInfo = (MinimalFileInfo)DisplayedNode.Tag;
                    toolTipText.Text = $"{BrowseForm.getNodeFilePath(DisplayedNode)}" +
                        $"\nCreated: {fileInfo.CreationTime.ToString()}" +
                        $"\nModified: {fileInfo.LastWriteTime.ToString()}" +
                        $"\nSize: {BrowseForm.parseFileSize(fileInfo.Length)}";
                }

                Size = toolTipText.Size;

                Point showLocation = Cursor.Position;
                showLocation.Offset(15, 15);
                Location = showLocation;

                Show();
                BringToFront();
            }
        }

        public TreeNode DisplayedNode { get; private set; }

        private System.Timers.Timer toolTipTimer = new System.Timers.Timer(500);
    }
}
