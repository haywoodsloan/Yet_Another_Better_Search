using System.Windows.Forms;

namespace Yet_Another_Better_Search
{
    public static class ExtensionMethods
    {
        public static void AddNodeSorted(this TreeNodeCollection nodes, TreeNode node)
        {
            for(int i = nodes.Count - 1; i >= 0; i--)
            {
                if(string.Compare(node.Text, nodes[i].Text) > 0)
                {
                    nodes.Insert(i + 1, node);
                    return;
                }
            }

            nodes.Add(node);
        }
    }
}
