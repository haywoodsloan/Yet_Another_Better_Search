using System.Windows.Forms;
using System.Drawing;

namespace Yet_Another_Better_Search
{
    public class NodeToolTip
    {
        public NodeToolTip(Control _tipControl)
        {
            tipControl = _tipControl;
        }      

        public void HideToolTip()
        {
            toolTip.Hide(tipControl);
            DisplayedNode = null;
        }

        public void ShowToolTip(TreeNode targetNode, Point tipLocation)
        {
            if (targetNode == null)
            {
                toolTip.Hide(tipControl);
                DisplayedNode = null;
            }
            else if (DisplayedNode != targetNode)
            {
                DisplayedNode = targetNode;

                string tipText;
                if (DisplayedNode.Tag.GetType() == typeof(MinimalDirectoryInfo))
                {
                    MinimalDirectoryInfo dirInfo = (MinimalDirectoryInfo)DisplayedNode.Tag;
                    tipText = $"{BrowseForm.getNodeFilePath(DisplayedNode)}" +
                        $"\nCreated: {dirInfo.CreationTime.ToString()}" +
                        $"\nModified: {dirInfo.LastWriteTime.ToString()}";
                }
                else
                {
                    MinimalFileInfo fileInfo = (MinimalFileInfo)DisplayedNode.Tag;
                    tipText = $"{BrowseForm.getNodeFilePath(DisplayedNode)}" +
                        $"\nCreated: {fileInfo.CreationTime.ToString()}" +
                        $"\nModified: {fileInfo.LastWriteTime.ToString()}" +
                        $"\nSize: {BrowseForm.parseFileSize(fileInfo.Length)}";
                }
                                
                tipLocation.Offset(10, 20);
                toolTip.Show(tipText, tipControl, tipLocation);
            }
        }
        
        public TreeNode DisplayedNode { get; private set; }

        private ToolTip toolTip = new ToolTip();
        private Control tipControl;
    }
}
