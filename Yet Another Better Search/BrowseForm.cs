using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Yet_Another_Better_Search
{
    public partial class BrowseForm : Form
    {
        public BrowseForm()
        {
            InitializeComponent();
        }

        private void unlimitedDepthCheck_OnCheck(object sender, EventArgs e)
        {
            searchDepthValue.Enabled = !unlimitedDepthCheck.Checked;
        }

        private void selectBtn_OnClick(object sender, EventArgs e)
        {
            rootSelect.SelectedPath = rootPathText.Text;

            if (rootSelect.ShowDialog() == DialogResult.OK)
            {
                rootPathText.Text = rootSelect.SelectedPath;
            }
        }

        private async void browseBtn_OnClick(object sender, EventArgs e)
        {
            if (!Directory.Exists(rootPathText.Text))
            {
                string defaultText = rootPathText.Text;
                rootPathText.Text = browseWarningText;

                Color defaultColor = rootPathText.BackColor;
                rootPathText.BackColor = Color.Yellow;
                await Task.Delay(400);
                rootPathText.BackColor = defaultColor;
                await Task.Delay(400);
                rootPathText.BackColor = Color.Yellow;
                await Task.Delay(750);

                rootPathText.BackColor = defaultColor;
                if (rootPathText.Text == browseWarningText)
                {
                    rootPathText.Text = defaultText;
                }

                return;
            }

            browseBtn.Enabled = false;

            resultTree.Nodes.Clear();
            resultTree.Nodes.Add(await createNodeFor(rootPathText.Text, 0));

            browseBtn.Enabled = true;
        }

        private void openLocation_OnClick(object sender, EventArgs e)
        {
            if (nodeContextMenu.Tag != null)
            {
                TreeNode targetNode = (TreeNode)nodeContextMenu.Tag;

                string dirStr = getNodeFilePath(targetNode.Tag.GetType() == typeof(MinimalDirectoryInfo) ?
                    targetNode : targetNode.Parent);

                Process.Start("explorer.exe", dirStr);
            }
        }

        private void fileProperties_OnClick(object sender, EventArgs e)
        {
            if (nodeContextMenu.Tag != null)
            {
                TreeNode targetNode = (TreeNode)nodeContextMenu.Tag;
                Unmanaged.ShowFileProperties(getNodeFilePath(targetNode));
            }
        }

        private async void browseHere_OnClick(object sender, EventArgs e)
        {
            if (notBrowsedContextMenu.Tag != null)
            {
                TreeNode targetNode = (TreeNode)notBrowsedContextMenu.Tag;
                TreeNodeCollection subNodes = (await createNodeFor(getNodeFilePath(targetNode), 0)).Nodes;

                targetNode.Nodes.Clear();
                foreach (TreeNode subNode in subNodes)
                {
                    targetNode.Nodes.Add(subNode);
                }
            }
        }

        private void resultTree_OnMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                resultTree.SelectedNode = e.Node;

                if (e.Node.Tag == null)
                {
                    if (e.Node.Text == notBrowsedText)
                    {
                        notBrowsedContextMenu.Tag = e.Node.Parent;
                        notBrowsedContextMenu.Show(resultTree.PointToScreen(e.Location));
                    }
                }
                else
                {
                    nodeContextMenu.Tag = e.Node;
                    nodeContextMenu.Show(resultTree.PointToScreen(e.Location));
                }
            }
        }
        
        private async Task<TreeNode> createNodeFor(string rootPath, int depth)
        {
            TreeNode rootNode = new TreeNode(depth == 0 ? rootPath : Path.GetFileName(rootPath));
            rootNode.ImageIndex = resultTreeImageList.Images.Count;
            rootNode.SelectedImageIndex = resultTreeImageList.Images.Count;

            MinimalDirectoryInfo rootInfo = new MinimalDirectoryInfo(rootPath);
            rootNode.Tag = rootInfo;

            IEnumerable<string> folderContents = null;

            try
            {
                folderContents = Directory.EnumerateFileSystemEntries(rootPath);
            }
            catch
            {
                TreeNode noAccess = new TreeNode(noAccessText);
                noAccess.ImageIndex = 1;
                noAccess.SelectedImageIndex = 1;

                rootNode.Nodes.Add(noAccess);
                return rootNode;
            }

            List<Task<TreeNode>> createNodeTasks = new List<Task<TreeNode>>();

            foreach (string content in folderContents)
            {
                if (Directory.Exists(content))
                {
                    if (depth < searchDepthValue.Value || unlimitedDepthCheck.Checked)
                    {
                        createNodeTasks.Add(createNodeFor(content, depth + 1));
                    }
                    else
                    {
                        TreeNode folderNode = new TreeNode(Path.GetFileName(content));
                        folderNode.ImageIndex = resultTreeImageList.Images.Count;
                        folderNode.SelectedImageIndex = resultTreeImageList.Images.Count;

                        MinimalDirectoryInfo folderInfo = new MinimalDirectoryInfo(content);
                        folderNode.Tag = folderInfo;

                        TreeNode notSearched = new TreeNode(notBrowsedText);
                        notSearched.ImageIndex = 0;
                        notSearched.SelectedImageIndex = 0;

                        folderNode.Nodes.Add(notSearched);
                        rootNode.Nodes.Add(folderNode);
                    }
                }
                else
                {
                    TreeNode fileNode = new TreeNode(Path.GetFileName(content));
                    fileNode.ImageIndex = resultTreeImageList.Images.Count;
                    fileNode.SelectedImageIndex = resultTreeImageList.Images.Count;

                    MinimalFileInfo fileInfo = new MinimalFileInfo(content);
                    fileNode.Tag = fileInfo;

                    rootNode.Nodes.Add(fileNode);
                }
            }

            foreach(Task<TreeNode> createNodeTask in createNodeTasks)
            {
                rootNode.Nodes.Add(await createNodeTask);
            }

            return rootNode;
        }

        private string parseFileSize(long absSize)
        {
            if (absSize == 0)
            {
                return "0 Bytes";
            }

            string[] suffixes = { "Bytes", "KB", "MB", "GB", "TB", "PB", "EB" };
            int sizeOrder = (int)Math.Log(absSize, 1024);

            double reducedSize = absSize / Math.Pow(1024, sizeOrder);
            return reducedSize.ToString("F2") + suffixes[sizeOrder];
        }

        private string getNodeFilePath(TreeNode node)
        {
            return node.FullPath.Replace(new string(Path.DirectorySeparatorChar, 2),
                Path.DirectorySeparatorChar.ToString());
        }
        
        const string notBrowsedText = "This folder was not searched, right click to browse deeper";
        const string noAccessText = "This folder was not searched because it could not be accessed";
        const string browseWarningText = "Please select a folder before browsing";
    }
}
