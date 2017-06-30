using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Yet_Another_Better_Search
{
    public static class ExtensionMethods
    {
        public static void AddNodeSorted(this TreeNodeCollection nodes, TreeNode node)
        {
            for (int i = nodes.Count - 1; i >= 0; i--)
            {
                if (string.Compare(node.Text, nodes[i].Text) > 0)
                {
                    nodes.Insert(i + 1, node);
                    return;
                }
            }

            nodes.Insert(0, node);
        }

        public static bool IsFileSystemNode(this TreeNode node)
        {
            return node.Tag != null && node.Tag.GetType().IsSubclassOf(typeof(MinimalFileSystemInfo));
        }

        public static bool CompareSearchCriteria(this TreeNode node, SearchCriteria criteria)
        {
            if(!node.CompareSearchType(criteria))
            {
                return false;
            }

            if(!node.CompareNameCriteria(criteria))
            {
                return false;
            }

            if(!node.CompareModifiedCriteria(criteria))
            {
                return false;
            }

            if(!node.CompareCreationCriteria(criteria))
            {
                return false;
            }

            if(!node.CompareSizeCriteria(criteria))
            {
                return false;
            }

            return true;
        }

        public static bool CompareSearchType(this TreeNode node, SearchCriteria criteria)
        {
            if (!node.IsFileSystemNode())
            {
                return false;
            }

            if (node.Tag.GetType() == typeof(MinimalDirectoryInfo) &&
                criteria.SearchType == SearchType.Files)
            {
                return false;
            }

            if (node.Tag.GetType() == typeof(MinimalFileInfo) &&
                criteria.SearchType == SearchType.Folders)
            {
                return false;
            }

            return true;
        }

        public static bool CompareNameCriteria(this TreeNode node, SearchCriteria criteria)
        {
            switch (criteria.NameCriteria)
            {
                case NameCriteria.Match:
                    if (criteria.UseRegex)
                    {
                        return Regex.IsMatch(node.Text, criteria.NameText,
                            criteria.MatchCase ? RegexOptions.None : RegexOptions.IgnoreCase);
                    }
                    else
                    {
                        return node.Text.IndexOf(criteria.NameText, criteria.MatchCase ?
                            StringComparison.InvariantCulture : StringComparison.InvariantCultureIgnoreCase) != -1;
                    }
            }

            return true;
        }

        public static bool CompareModifiedCriteria(this TreeNode node, SearchCriteria criteria)
        {
            if (!node.IsFileSystemNode())
            {
                return false;
            }

            MinimalFileSystemInfo info = (MinimalFileSystemInfo)node.Tag;
            return CompareDateCriteria(criteria.ModifiedDateCriteria, info.LastWriteTime,
                criteria.FirstModifiedDate, criteria.SecondModifiedDate);
        }

        public static bool CompareCreationCriteria(this TreeNode node, SearchCriteria criteria)
        {
            if (!node.IsFileSystemNode())
            {
                return false;
            }

            MinimalFileSystemInfo info = (MinimalFileSystemInfo)node.Tag;
            return CompareDateCriteria(criteria.CreatedDateCriteria, info.CreationTime,
                criteria.FirstCreatedDate, criteria.SecondCreatedDate);
        }

        private static bool CompareDateCriteria(DateCriteria criteria, DateTime nodeDate,
            DateTime firstDate, DateTime secondDate)
        {
            switch (criteria)
            {
                case DateCriteria.On:
                    return nodeDate.Date == firstDate.Date;
                case DateCriteria.Before:
                    return nodeDate < firstDate;
                case DateCriteria.After:
                    return nodeDate > firstDate;
                case DateCriteria.Between:
                    return nodeDate >= firstDate && nodeDate <= secondDate;
            }

            return true;
        }

        public static bool CompareSizeCriteria(this TreeNode node, SearchCriteria criteria)
        {
            if(!node.IsFileSystemNode())
            {
                return false;
            }

            if(node.Tag.GetType() == typeof(MinimalDirectoryInfo))
            {
                return true;
            }

            MinimalFileInfo info = (MinimalFileInfo)node.Tag;

            switch(criteria.SizeCriteria)
            {
                case SizeCriteria.LessThan:
                    return info.Length < criteria.FirstSize;
                case SizeCriteria.GreaterThan:
                    return info.Length > criteria.FirstSize;
                case SizeCriteria.Between:
                    return info.Length >= criteria.FirstSize &&
                        info.Length <= criteria.SecondSize;
            }

            return true;
        }
    }
}
