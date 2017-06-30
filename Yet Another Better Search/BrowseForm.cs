using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Yet_Another_Better_Search
{
    public partial class BrowseForm : Form
    {
        struct ResumeInfo
        {
            public ResumeInfo(TreeNode node, int depth)
            {
                Node = node;
                Depth = depth;
            }

            public TreeNode Node { get; private set; }
            public int Depth { get; private set; }
        }

        struct FilteredNode
        {
            public FilteredNode(TreeNode node, TreeNode parentNode)
            {
                Node = node;
                ParentNode = parentNode;
            }

            public TreeNode Node { get; private set; }
            public TreeNode ParentNode { get; private set; }
        }

        public BrowseForm()
        {
            InitializeComponent();
            toolTip = new NodeToolTip(resultTree);
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
                filterBtn.Enabled = false;
                searchBtn.Enabled = false;

                if (resultTree.Nodes.Count > 0 &&
                    compareFilePaths(resultTree.Nodes[0].FullPath, rootPathText.Text))

                {
                    TreeNode rootNode = resultTree.Nodes[0];
                    List<ResumeInfo> infos = listResumeNodes(rootNode, 0);

                    if (infos.Count > 0)
                    {
                        resultTree.BeginUpdate();
                        await Task.Run(() => resumeBrowse(infos));
                        resultTree.EndUpdate();
                    }
                }
                else
                {
                    resultTree.Nodes.Clear();

                    TreeNode resultNode = await Task.Run(() =>
                        createNodeFor(getFilePath(rootPathText.Text), 0)
                    );

                    resultTree.Nodes.AddNodeSorted(resultNode);
                }

                browsing = false;
                browseBtn.Text = "Browse";
                browseBtn.Enabled = true;
                filterBtn.Enabled = true;
                searchBtn.Enabled = true;

                GC.Collect();
            }
            else
            {
                browseBtn.Enabled = false;
                filterBtn.Enabled = false;
                searchBtn.Enabled = false;
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
                filterBtn.Enabled = false;
                searchBtn.Enabled = false;

                TreeNodeCollection subNodes = await Task.Run(() =>
                    createNodeFor(getNodeFilePath(targetNode), 0).Nodes
                );

                browsing = false;
                browseBtn.Text = "Browse";
                browseBtn.Enabled = true;
                filterBtn.Enabled = true;
                searchBtn.Enabled = true;

                targetNode.Nodes.Clear();
                foreach (TreeNode subNode in subNodes)
                {
                    targetNode.Nodes.AddNodeSorted(subNode);
                }

                GC.Collect();
            }
        }

        private void filterBtn_OnClick(object sender, EventArgs e)
        {
            SearchCriteriaForm criteraForm = new SearchCriteriaForm(false);

            if (filteredNodes.Count > 0)
            {
                filterBtn.Enabled = false;
                searchBtn.Enabled = false;
                resultTree.BeginUpdate();

                unfilterNodes();

                resultTree.EndUpdate();
                filterBtn.Enabled = true;
                searchBtn.Enabled = true;
                filterBtn.Text = "Filter";
            }
            else
            {
                if (criteraForm.ShowDialog(this) == DialogResult.OK)
                {
                    filterBtn.Enabled = false;
                    searchBtn.Enabled = false;
                    resultTree.BeginUpdate();

                    if (!filterNode(resultTree.Nodes[0], criteraForm.GetSearchCriteria()))
                    {
                        foreach (TreeNode childNode in resultTree.Nodes[0].Nodes)
                        {
                            FilteredNode filteredNode = new FilteredNode(childNode, resultTree.Nodes[0]);
                            filteredNodes.Add(filteredNode);
                        }

                        resultTree.Nodes[0].Nodes.Clear();
                    }

                    resultTree.EndUpdate();
                    filterBtn.Enabled = true;
                    searchBtn.Enabled = true;

                    if (filteredNodes.Count > 0)
                    {
                        filterBtn.Text = "Unfilter";
                    }
                }
            }
        }

        private void resultTree_OnMouseMove(object sender, MouseEventArgs e)
        {
            TreeViewHitTestInfo hitTest = resultTree.HitTest(e.Location);

            if (hitTest.Location != TreeViewHitTestLocations.Label ||
                hitTest.Node == null)
            {
                toolTip.HideToolTip();
            }
            else
            {
                toolTip.ShowToolTip(hitTest.Node, e.Location);
            }
        }

        private void resultTree_OnMouseLeave(object sender, EventArgs e)
        {
            toolTip.HideToolTip();
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

        private List<ResumeInfo> listResumeNodes(TreeNode rootNode, int depth)
        {
            List<ResumeInfo> nodeList = new List<ResumeInfo>();

            if (rootNode.Nodes.Count == 1 &&
                rootNode.Nodes[0].Text == notBrowsedText)
            {
                ResumeInfo info = new ResumeInfo(rootNode, depth);
                nodeList.Add(info);
            }
            else if (rootNode.Nodes.Count > 0)
            {
                if (unlimitedDepthCheck.Checked || depth < searchDepthValue.Value)
                {
                    foreach (TreeNode subNode in rootNode.Nodes)
                    {
                        if (subNode.Nodes.Count > 0)
                        {
                            nodeList.AddRange(listResumeNodes(subNode, depth + 1));
                        }
                    }
                }
            }

            return nodeList;
        }

        private void resumeBrowse(List<ResumeInfo> infos)
        {
            Task[] resumeTasks = new Task[infos.Count];
            for (int i = 0; i < infos.Count; i++)
            {
                ResumeInfo info = infos[i];
                resumeTasks[i] = Task.Run(() =>
                {
                    TreeNodeCollection subNodes = createNodeFor(
                        getNodeFilePath(info.Node),
                        info.Depth).Nodes;

                    BeginInvoke(new Action(() =>
                    {
                        info.Node.Nodes.Clear();
                        foreach (TreeNode subNode in subNodes)
                        {
                            info.Node.Nodes.Add(subNode);
                        }
                    }));
                });
            }

            foreach (Task resumeTask in resumeTasks)
            {
                resumeTask.Wait();
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
                rootNode.Nodes.AddNodeSorted(createNotSearchedNode());
                return rootNode;
            }

            IEnumerable<string> folderContents;

            try
            {
                folderContents = Directory.EnumerateFileSystemEntries(rootPath);
            }
            catch
            {
                rootNode.Nodes.AddNodeSorted(createNoAccessNode());
                return rootNode;
            }

            List<Task<TreeNode>> createNodeTasks = new List<Task<TreeNode>>();
            foreach (string content in folderContents)
            {
                if (!browsing)
                {
                    rootNode.Nodes.Clear();
                    rootNode.Nodes.AddNodeSorted(createNotSearchedNode());

                    return rootNode;
                }

                if (Directory.Exists(content))
                {
                    if ((unlimitedDepthCheck.Checked || depth < searchDepthValue.Value) && browsing)
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

                        folderNode.Nodes.AddNodeSorted(createNotSearchedNode());
                        rootNode.Nodes.AddNodeSorted(folderNode);
                    }
                }
                else
                {
                    TreeNode fileNode = new TreeNode(Path.GetFileName(content));
                    fileNode.ImageIndex = resultTreeImageList.Images.Count;
                    fileNode.SelectedImageIndex = resultTreeImageList.Images.Count;

                    MinimalFileInfo fileInfo = new MinimalFileInfo(content);
                    fileNode.Tag = fileInfo;

                    rootNode.Nodes.AddNodeSorted(fileNode);
                }
            }

            foreach (Task<TreeNode> createNodeTask in createNodeTasks)
            {
                createNodeTask.Wait();
                rootNode.Nodes.AddNodeSorted(createNodeTask.Result);
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

        private bool filterNode(TreeNode rootNode, SearchCriteria criteria)
        {
            bool nodeMatch = rootNode.Tag == null ? false :
                rootNode.CompareSearchCriteria(criteria);

            if (rootNode.Nodes.Count > 0)
            {
                List<TreeNode> failedNodes = new List<TreeNode>();

                foreach (TreeNode childNode in rootNode.Nodes)
                {
                    if (filterNode(childNode, criteria))
                    {
                        nodeMatch = true;
                    }
                    else
                    {
                        failedNodes.Add(childNode);
                    }
                }

                if (nodeMatch && failedNodes.Count > 0)
                {
                    foreach (TreeNode failedNode in failedNodes)
                    {
                        FilteredNode filteredNode = new FilteredNode(failedNode, rootNode);
                        filteredNodes.Add(filteredNode);

                        rootNode.Nodes.Remove(failedNode);
                    }
                }
            }

            return nodeMatch;
        }

        private void unfilterNodes()
        {
            foreach(FilteredNode filteredNode in filteredNodes)
            {
                filteredNode.ParentNode.Nodes.AddNodeSorted(filteredNode.Node);
            }

            filteredNodes.Clear();
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

        private NodeToolTip toolTip;
        private List<FilteredNode> filteredNodes = new List<FilteredNode>();

        private volatile bool browsing = false;

        public const string notBrowsedText = "This folder was not searched, right click to browse deeper";
        public const string noAccessText = "This folder was not searched because it could not be accessed";
        public const string browseWarningText = "Please select a folder before browsing";
    }
}
