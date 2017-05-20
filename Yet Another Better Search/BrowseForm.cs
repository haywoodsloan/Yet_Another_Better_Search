using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            if (!browsing)
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

                browsing = true;
                browseBtn.Text = "Stop";

                if (resultTree.Nodes.Count > 0 &&
                    compareFilePaths(resultTree.Nodes[0].FullPath, rootPathText.Text))

                {
                    resultTree.BeginUpdate();

                    TreeNode rootNode = resultTree.Nodes[0];
                    await Task.Run(() => resumeBrowse(rootNode, 0));

                    resultTree.EndUpdate();
                }
                else
                {
                    resultTree.Nodes.Clear();

                    TreeNode resultNode = await Task.Run(() =>
                        createNodeFor(getFilePath(rootPathText.Text), 0)
                    );

                    resultTree.Nodes.Add(resultNode);
                }

                browsing = false;
                browseBtn.Text = "Browse";
            }
            else
            {
                browsing = false;
            }
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

                browsing = true;
                browseBtn.Text = "Stop";

                TreeNodeCollection subNodes = await Task.Run(() =>
                    createNodeFor(getNodeFilePath(targetNode), 0).Nodes
                );

                browsing = false;
                browseBtn.Text = "Browse";

                targetNode.Nodes.Clear();
                foreach (TreeNode subNode in subNodes)
                {
                    targetNode.Nodes.Add(subNode);
                }
            }
        }

        private void resultTree_OnMouseMove(object sender, MouseEventArgs e)
        {
            TreeNode mouseNode = resultTree.GetNodeAt(e.Location);

            if (mouseNode == null)
            {
                toolTip.Hide();
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

                    NodeToolTip toolTip = new NodeToolTip();
                }
            }
        }

        private void resumeBrowse(TreeNode rootNode, int depth)
        {
            if (rootNode.Nodes.Count == 1 &&
                rootNode.Nodes[0].Text == notBrowsedText)
            {
                TreeNodeCollection subNodes = createNodeFor(getNodeFilePath(rootNode), depth).Nodes;

                resultTree.BeginInvoke(new Action(() =>
                {
                    rootNode.Nodes.Clear();
                    foreach (TreeNode subNode in subNodes)
                    {
                        rootNode.Nodes.Add(subNode);
                    }
                }));
            }
            else if (rootNode.Nodes.Count > 0)
            {
                if (unlimitedDepthCheck.Checked || depth < searchDepthValue.Value)
                {
                    List<Task> browseTasks = new List<Task>();

                    foreach (TreeNode subNode in rootNode.Nodes)
                    {
                        if (subNode.Nodes.Count > 0)
                        {
                            browseTasks.Add(Task.Run(() => resumeBrowse(subNode, depth + 1)));
                        }
                    }

                    foreach (Task browseTask in browseTasks)
                    {
                        browseTask.Wait();
                    }
                }
            }
        }

        private TreeNode createNodeFor(string rootPath, int depth)
        {
            TreeNode rootNode = new TreeNode(depth == 0 ? rootPath : Path.GetFileName(rootPath));
            rootNode.ImageIndex = resultTreeImageList.Images.Count;
            rootNode.SelectedImageIndex = resultTreeImageList.Images.Count;

            MinimalDirectoryInfo rootInfo = new MinimalDirectoryInfo(rootPath);
            rootNode.Tag = rootInfo;

            if (!browsing)
            {
                rootNode.Nodes.Add(createNotSearchedNode());
                return rootNode;
            }

            IEnumerable<string> folderContents;

            try
            {
                folderContents = Directory.EnumerateFileSystemEntries(rootPath);
            }
            catch
            {
                rootNode.Nodes.Add(createNoAccessNode());
                return rootNode;
            }

            List<Task<TreeNode>> createNodeTasks = new List<Task<TreeNode>>();
            foreach (string content in folderContents)
            {
                if (Directory.Exists(content))
                {
                    if (unlimitedDepthCheck.Checked || depth < searchDepthValue.Value)
                    {
                        createNodeTasks.Add(Task.Run(() => createNodeFor(content, depth + 1)));
                    }
                    else
                    {
                        TreeNode folderNode = new TreeNode(Path.GetFileName(content));
                        folderNode.ImageIndex = resultTreeImageList.Images.Count;
                        folderNode.SelectedImageIndex = resultTreeImageList.Images.Count;

                        MinimalDirectoryInfo folderInfo = new MinimalDirectoryInfo(content);
                        folderNode.Tag = folderInfo;

                        folderNode.Nodes.Add(createNotSearchedNode());
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

            foreach (Task<TreeNode> createNodeTask in createNodeTasks)
            {
                createNodeTask.Wait();
                rootNode.Nodes.Add(createNodeTask.Result);
            }

            return rootNode;
        }

        private static TreeNode createNotSearchedNode()
        {
            TreeNode notSearched = new TreeNode(notBrowsedText);
            notSearched.ImageIndex = 0;
            notSearched.SelectedImageIndex = 0;

            return notSearched;
        }

        private static TreeNode createNoAccessNode()
        {
            TreeNode noAccess = new TreeNode(noAccessText);
            noAccess.ImageIndex = 1;
            noAccess.SelectedImageIndex = 1;

            return noAccess;
        }

        public static string parseFileSize(long absSize)
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

        public static bool compareFilePaths(string path1, string path2)
        {
            return getFilePath(path1).Equals(getFilePath(path2), StringComparison.InvariantCultureIgnoreCase);
        }

        public static string getNodeFilePath(TreeNode node)
        {
            return getFilePath(node.FullPath);
        }

        public static string getFilePath(string path)
        {
            string matchPatern = $@"\{Path.DirectorySeparatorChar}\{Path.DirectorySeparatorChar}+";
            return Regex.Replace(path.Trim(), matchPatern, Path.DirectorySeparatorChar.ToString());
        }

        NodeToolTip toolTip = new NodeToolTip();

        volatile bool browsing = false;

        const string notBrowsedText = "This folder was not searched, right click to browse deeper";
        const string noAccessText = "This folder was not searched because it could not be accessed";
        const string browseWarningText = "Please select a folder before browsing";
    }
}
