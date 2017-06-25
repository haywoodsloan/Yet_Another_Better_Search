using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

            searchTypeCombo.DataSource = EnumEx.GetDescriptions(typeof(SearchType));
            nameCriteriaCombo.DataSource = EnumEx.GetDescriptions(typeof(NameCriteria));
            modifiedCriteriaCombo.DataSource = EnumEx.GetDescriptions(typeof(DateCriteria));
            creationCriteriaCombo.DataSource = EnumEx.GetDescriptions(typeof(DateCriteria));
            sizeCriteriaCombo.DataSource = EnumEx.GetDescriptions(typeof(SizeCriteria));
            firstSizeScaleCombo.DataSource = EnumEx.GetDescriptions(typeof(FileSizeScale));
            secondSizeScaleCombo.DataSource = EnumEx.GetDescriptions(typeof(FileSizeScale));
        }

        private void searchTypeCombo_OnChange(object sender, EventArgs e)
        {
            SearchType searchType = (SearchType)EnumEx.GetValueFromDescription(typeof(SearchType),
                searchTypeCombo.SelectedItem.ToString());

            SizeCriteria sizeCriteria = (SizeCriteria)EnumEx.GetValueFromDescription(typeof(SizeCriteria),
                sizeCriteriaCombo.SelectedItem.ToString());

            if ((searchType == SearchType.Folders || searchType == SearchType.Both) &&
                sizeCriteria != SizeCriteria.Any)
            {
                folderSizeLabel.Visible = true;
            }
            else
            {
                folderSizeLabel.Visible = false;
            }
        }

        private void nameCriteriaCombo_OnChange(object sender, EventArgs e)
        {
            NameCriteria nameCriteria = (NameCriteria)EnumEx.GetValueFromDescription(typeof(NameCriteria),
                nameCriteriaCombo.SelectedItem.ToString());

            switch (nameCriteria)
            {
                case NameCriteria.Any:
                    nameTextBox.Visible = false;
                    regexCheck.Visible = false;
                    caseCheck.Visible = false;
                    break;
                case NameCriteria.Match:
                    nameTextBox.Visible = true;
                    regexCheck.Visible = true;
                    caseCheck.Visible = true;
                    break;
            }
        }

        private void modifiedCriteriaCombo_OnChange(object sender, EventArgs e)
        {
            DateCriteria modifiedCriteria = (DateCriteria)EnumEx.GetValueFromDescription(typeof(DateCriteria),
                modifiedCriteriaCombo.SelectedItem.ToString());

            switch (modifiedCriteria)
            {
                case DateCriteria.Any:
                    firstModifiedDatePicker.Visible = false;
                    firstModifiedTimePicker.Visible = false;
                    modifiedAndLabel.Visible = false;
                    secondModifiedDatePicker.Visible = false;
                    secondModifiedTimePicker.Visible = false;
                    break;
                case DateCriteria.On:
                    firstModifiedDatePicker.Visible = true;
                    firstModifiedTimePicker.Visible = false;
                    modifiedAndLabel.Visible = false;
                    secondModifiedDatePicker.Visible = false;
                    secondModifiedTimePicker.Visible = false;
                    break;
                case DateCriteria.After:
                case DateCriteria.Before:
                    firstModifiedDatePicker.Visible = true;
                    firstModifiedTimePicker.Visible = true;
                    modifiedAndLabel.Visible = false;
                    secondModifiedDatePicker.Visible = false;
                    secondModifiedTimePicker.Visible = false;
                    break;
                case DateCriteria.Between:
                    firstModifiedDatePicker.Visible = true;
                    firstModifiedTimePicker.Visible = true;
                    modifiedAndLabel.Visible = true;
                    secondModifiedDatePicker.Visible = true;
                    secondModifiedTimePicker.Visible = true;
                    break;
            }
        }

        private void creationCriteriaCombo_OnChange(object sender, EventArgs e)
        {
            DateCriteria creationCriteria = (DateCriteria)EnumEx.GetValueFromDescription(typeof(DateCriteria),
                creationCriteriaCombo.SelectedItem.ToString());

            switch (creationCriteria)
            {
                case DateCriteria.Any:
                    firstCreationDatePicker.Visible = false;
                    firstCreationTimePicker.Visible = false;
                    creationAndLabel.Visible = false;
                    secondCreationDatePicker.Visible = false;
                    secondCreationTimePicker.Visible = false;
                    break;
                case DateCriteria.On:
                    firstCreationDatePicker.Visible = true;
                    firstCreationTimePicker.Visible = false;
                    creationAndLabel.Visible = false;
                    secondCreationDatePicker.Visible = false;
                    secondCreationTimePicker.Visible = false;
                    break;
                case DateCriteria.After:
                case DateCriteria.Before:
                    firstCreationDatePicker.Visible = true;
                    firstCreationTimePicker.Visible = true;
                    creationAndLabel.Visible = false;
                    secondCreationDatePicker.Visible = false;
                    secondCreationTimePicker.Visible = false;
                    break;
                case DateCriteria.Between:
                    firstCreationDatePicker.Visible = true;
                    firstCreationTimePicker.Visible = true;
                    creationAndLabel.Visible = true;
                    secondCreationDatePicker.Visible = true;
                    secondCreationTimePicker.Visible = true;
                    break;
            }
        }

        private void sizeCriteriaCombo_OnChange(object sender, EventArgs e)
        {
            SizeCriteria sizeCriteria = (SizeCriteria)EnumEx.GetValueFromDescription(typeof(SizeCriteria),
                sizeCriteriaCombo.SelectedItem.ToString());

            SearchType searchType = (SearchType)EnumEx.GetValueFromDescription(typeof(SearchType),
                searchTypeCombo.SelectedItem.ToString());

            if((searchType == SearchType.Folders || searchType == SearchType.Both) &&
                sizeCriteria != SizeCriteria.Any)
            {
                folderSizeLabel.Visible = true;
            }
            else
            {
                folderSizeLabel.Visible = false;
            }

            switch (sizeCriteria)
            {
                case SizeCriteria.Any:
                    firstSizeTextBox.Visible = false;
                    firstSizeScaleCombo.Visible = false;
                    sizeAndLabel.Visible = false;
                    secondSizeTextBox.Visible = false;
                    secondSizeScaleCombo.Visible = false;
                    break;
                case SizeCriteria.LessThan:
                case SizeCriteria.GreaterThan:
                    firstSizeTextBox.Visible = true;
                    firstSizeScaleCombo.Visible = true;
                    sizeAndLabel.Visible = false;
                    secondSizeTextBox.Visible = false;
                    secondSizeScaleCombo.Visible = false;
                    break;
                case SizeCriteria.Between:
                    firstSizeTextBox.Visible = true;
                    firstSizeScaleCombo.Visible = true;
                    sizeAndLabel.Visible = true;
                    secondSizeTextBox.Visible = true;
                    secondSizeScaleCombo.Visible = true;
                    break;
            }
        }

        private bool validateNameCriteria()
        {
            NameCriteria nameCriteria = (NameCriteria)EnumEx.GetValueFromDescription(typeof(NameCriteria),
                nameCriteriaCombo.SelectedItem.ToString());

            if(nameCriteria == NameCriteria.Match)
            {
                if(regexCheck.Checked)
                {
                    try
                    {
                        new Regex(nameTextBox.Text);
                    }
                    catch (ArgumentException)
                    {
                        return false;
                    }

                    return true;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
        }

        private bool validateModifiedCriteria()
        {
            DateCriteria modifiedCriteria = (DateCriteria)EnumEx.GetValueFromDescription(typeof(DateCriteria),
                modifiedCriteriaCombo.SelectedItem.ToString());

            DateTime firstDateTime = firstModifiedDatePicker.Value;
            firstDateTime.Subtract(firstDateTime.TimeOfDay);
            firstDateTime.Add(firstModifiedTimePicker.Value.TimeOfDay);

            DateTime secondDateTime = secondModifiedDatePicker.Value;
            secondDateTime.Subtract(secondDateTime.TimeOfDay);
            secondDateTime.Add(secondModifiedTimePicker.Value.TimeOfDay);

            return validateDateCriteria(modifiedCriteria, firstDateTime, secondDateTime);
        }

        private bool validateCreationCriteria()
        {
            DateCriteria creationCriteria = (DateCriteria)EnumEx.GetValueFromDescription(typeof(DateCriteria),
                creationCriteriaCombo.SelectedItem.ToString());

            DateTime firstDateTime = firstCreationDatePicker.Value;
            firstDateTime.Subtract(firstDateTime.TimeOfDay);
            firstDateTime.Add(firstCreationTimePicker.Value.TimeOfDay);

            DateTime secondDateTime = secondCreationDatePicker.Value;
            secondDateTime.Subtract(secondDateTime.TimeOfDay);
            secondDateTime.Add(secondCreationTimePicker.Value.TimeOfDay);

            return validateDateCriteria(creationCriteria, firstDateTime, secondDateTime);
        }

        private bool validateDateCriteria(DateCriteria dateCriteria, 
            DateTime firstDateTime, DateTime secondDateTime)
        {
            switch (dateCriteria)
            {
                case DateCriteria.On:
                    return firstDateTime.Date <= DateTime.Today.Date;
                case DateCriteria.After:
                    return firstDateTime <= DateTime.Now;
                case DateCriteria.Between:
                    return firstDateTime <= DateTime.Now 
                        && firstDateTime <= secondDateTime;
                default:
                    return true;
            }
        }
    }
}
