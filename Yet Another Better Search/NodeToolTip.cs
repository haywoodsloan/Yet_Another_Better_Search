using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Yet_Another_Better_Search
{
    public partial class NodeToolTip : Form
    {
        public NodeToolTip()
        {
            InitializeComponent();
        }

        public void ShowToolTip(TreeNode targetNode, Point location)
        {          
            if(targetNode.Tag == null)
            {
                return;
            }

            if(targetNode.Tag.GetType() == typeof(MinimalDirectoryInfo))
            {
                MinimalDirectoryInfo dirInfo = (MinimalDirectoryInfo)targetNode.Tag;
                toolTipText.Text = $"{BrowseForm.getNodeFilePath(targetNode)}" +
                    $"\nCreated: {dirInfo.CreationTime.ToString()}" +
                    $"\nModified: {dirInfo.LastWriteTime.ToString()}";
            }
            else
            {
                MinimalFileInfo fileInfo = (MinimalFileInfo)targetNode.Tag;
                toolTipText.Text = $"{BrowseForm.getNodeFilePath(targetNode)}" +
                    $"\nCreated: {fileInfo.CreationTime.ToString()}" +
                    $"\nModified: {fileInfo.LastWriteTime.ToString()}" +
                    $"\nSize: {BrowseForm.parseFileSize(fileInfo.Length)}";
            }

            DisplayedNode = targetNode;
            Size = toolTipText.Size;
            Location = location;

            Show();
            BringToFront();
        }

        public TreeNode DisplayedNode { get; private set; }
    }
}
