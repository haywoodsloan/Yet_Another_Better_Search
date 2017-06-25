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
    public enum SearchType
    {
        Files,
        Folders,
        Both
    }

    public enum NameCriteria
    {
        Any,
        Match
    }

    public enum DateCriteria
    {
        Any,
        On,
        Before,
        Between,
        After
    }

    public enum SizeCriteria
    {
        Any,
        [Description("Less Than")]
        LessThan,
        Between,
        [Description("Greater Than")]
        GreaterThan
    }

    public struct SearchCriteria
    {
        public SearchType SearchType;

        public NameCriteria NameCriteria;
        public bool UseRegex;
        public bool MatchCase;
        public string NameText;

        public DateCriteria ModifiedDateCriteria;
        public DateTime FirstModifiedDate;
        public DateTime SecondModifiedDate;

        public DateCriteria CreatedDateCriteria;
        public DateTime FirstCreatedDate;
        public DateTime SecondCreatedDate;

        public SizeCriteria SizeCriteria;
        public long FirstSize;
        public long SecondSize;
    }

    public partial class SearchCriteriaForm : Form
    {
        public SearchCriteriaForm(bool searchMode)
        {
            InitializeComponent();

            if (searchMode)
            {
                Text = "Search Criteria";
                okayBtn.Text = "Search";
            }
            else
            {
                Text = "Filter Criteria";
                okayBtn.Text = "Filter";
            }

            searchTypeCombo.DataSource = EnumExtension.GetDescriptions(typeof(SearchType));
            nameCriteriaCombo.DataSource = EnumExtension.GetDescriptions(typeof(NameCriteria));
            modifiedCriteriaCombo.DataSource = EnumExtension.GetDescriptions(typeof(DateCriteria));
            creationCriteriaCombo.DataSource = EnumExtension.GetDescriptions(typeof(DateCriteria));
            sizeCriteriaCombo.DataSource = EnumExtension.GetDescriptions(typeof(SizeCriteria));
            firstSizeScaleCombo.DataSource = EnumExtension.GetDescriptions(typeof(FileSizeScale));
            secondSizeScaleCombo.DataSource = EnumExtension.GetDescriptions(typeof(FileSizeScale));
        }
    }
}
