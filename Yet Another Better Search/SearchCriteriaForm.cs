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

    public enum NameValidation
    {
        Good,
        BadRegex
    }

    public enum DateValidation
    {
        Good,
        AfterNow,
        NegativeSpan
    }

    public enum SizeValidation
    {
        Good,
        LessThanZero,
        NegativeSpan,
        SizeBad,
        FirstSizeBad,
        SecondSizeBad
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

            if ((searchType == SearchType.Folders || searchType == SearchType.Both) &&
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

        private void okayBtn_OnClick(object sender, EventArgs e)
        {
            NameValidation nameResult = validateNameCriteria();
            switch (nameResult)
            {
                case NameValidation.BadRegex:
                    warningLabel.Text = "Warning: A bad regular expression was provided for the name criteria";
                    warningLabel.Visible = true;
                    return;
            }

            DateValidation modifiedResult = validateModifiedCriteria();
            switch (modifiedResult)
            {
                case DateValidation.AfterNow:
                    warningLabel.Text = "Warning: The modified date criteria is in the future";
                    warningLabel.Visible = true;
                    return;
                case DateValidation.NegativeSpan:
                    warningLabel.Text = "Warning: The modified date criteria has inverted begin and end dates";
                    warningLabel.Visible = true;
                    return;
            }

            DateValidation creationResult = validateCreationCriteria();
            switch (creationResult)
            {
                case DateValidation.AfterNow:
                    warningLabel.Text = "Warning: The creation date criteria is in the future";
                    warningLabel.Visible = true;
                    return;
                case DateValidation.NegativeSpan:
                    warningLabel.Text = "Warning: The creation date criteria has inverted begin and end dates";
                    warningLabel.Visible = true;
                    return;
            }

            SizeValidation sizeResult = validateSizeCriteria();
            switch (sizeResult)
            {
                case SizeValidation.SizeBad:
                    warningLabel.Text = "Warning: The size criteria is not a valid number";
                    warningLabel.Visible = true;
                    return;
                case SizeValidation.FirstSizeBad:
                    warningLabel.Text = "Warning: The size criteria minimum is not a valid number";
                    warningLabel.Visible = true;
                    return;
                case SizeValidation.SecondSizeBad:
                    warningLabel.Text = "Warning: The size criteria maximum is not a valid number";
                    warningLabel.Visible = true;
                    return;
                case SizeValidation.NegativeSpan:
                    warningLabel.Text = "Warning: The size criteria has inverted miniumum and maximum values";
                    warningLabel.Visible = true;
                    return;
                case SizeValidation.LessThanZero:
                    warningLabel.Text = "Warning: The size criteria is less than zero";
                    warningLabel.Visible = true;
                    return;
            }

            warningLabel.Visible = false;
            DialogResult = DialogResult.OK;
        }

        private NameValidation validateNameCriteria()
        {
            NameCriteria nameCriteria = (NameCriteria)EnumEx.GetValueFromDescription(typeof(NameCriteria),
                nameCriteriaCombo.SelectedItem.ToString());

            if (nameCriteria == NameCriteria.Match &&
                regexCheck.Checked)
            {
                try
                {
                    new Regex(nameTextBox.Text);
                }
                catch (ArgumentException)
                {
                    return NameValidation.BadRegex;
                }
            }

            return NameValidation.Good;
        }

        private DateValidation validateModifiedCriteria()
        {
            DateCriteria modifiedCriteria = (DateCriteria)EnumEx.GetValueFromDescription(typeof(DateCriteria),
                modifiedCriteriaCombo.SelectedItem.ToString());

            DateTime firstDateTime = firstModifiedDatePicker.Value.Date;
            firstDateTime = firstDateTime.Add(firstModifiedTimePicker.Value.TimeOfDay);

            DateTime secondDateTime = secondModifiedDatePicker.Value.Date;
            secondDateTime = secondDateTime.Add(secondModifiedTimePicker.Value.TimeOfDay);

            return validateDateCriteria(modifiedCriteria, firstDateTime, secondDateTime);
        }

        private DateValidation validateCreationCriteria()
        {
            DateCriteria creationCriteria = (DateCriteria)EnumEx.GetValueFromDescription(typeof(DateCriteria),
                creationCriteriaCombo.SelectedItem.ToString());

            DateTime firstDateTime = firstCreationDatePicker.Value.Date;
            firstDateTime = firstDateTime.Add(firstCreationTimePicker.Value.TimeOfDay);

            DateTime secondDateTime = secondCreationDatePicker.Value.Date;
            secondDateTime = secondDateTime.Add(secondCreationTimePicker.Value.TimeOfDay);

            return validateDateCriteria(creationCriteria, firstDateTime, secondDateTime);
        }

        private DateValidation validateDateCriteria(DateCriteria dateCriteria,
            DateTime firstDateTime, DateTime secondDateTime)
        {
            switch (dateCriteria)
            {
                case DateCriteria.On:
                    if (firstDateTime.Date > DateTime.Today.Date)
                        return DateValidation.AfterNow;

                    break;
                case DateCriteria.After:
                    if (firstDateTime > DateTime.Now)
                        return DateValidation.AfterNow;

                    break;
                case DateCriteria.Between:
                    if (firstDateTime > DateTime.Now)
                        return DateValidation.AfterNow;

                    if (firstDateTime > secondDateTime)
                        return DateValidation.NegativeSpan;

                    break;
            }

            return DateValidation.Good;
        }

        private SizeValidation validateSizeCriteria()
        {
            SizeCriteria sizeCriteria = (SizeCriteria)EnumEx.GetValueFromDescription(typeof(SizeCriteria),
                sizeCriteriaCombo.SelectedItem.ToString());

            double firstSize;
            double secondSize;

            long firstTotalSize = 0;
            long secondTotalSize = 0;

            if (sizeCriteria != SizeCriteria.Any)
            {
                if (double.TryParse(firstSizeTextBox.Text, out firstSize))
                {
                    int firstSizeScale = (int)EnumEx.GetValueFromDescription(typeof(FileSizeScale),
                        firstSizeScaleCombo.SelectedItem.ToString());

                    firstTotalSize = (long)(firstSize * Math.Pow(1024, firstSizeScale));
                }
                else
                {
                    return sizeCriteria == SizeCriteria.Between ? 
                        SizeValidation.FirstSizeBad : SizeValidation.SizeBad;
                }
            }

            if (sizeCriteria == SizeCriteria.Between)
            {
                if (double.TryParse(secondSizeTextBox.Text, out secondSize))
                {
                    int secondSizeScale = (int)EnumEx.GetValueFromDescription(typeof(FileSizeScale),
                        secondSizeScaleCombo.SelectedItem.ToString());

                    secondTotalSize = (long)(secondSize * Math.Pow(1024, secondSizeScale));
                }
                else
                {
                    return SizeValidation.SecondSizeBad;
                }
            }

            switch (sizeCriteria)
            {
                case SizeCriteria.LessThan:
                    if (firstTotalSize == 0)
                        return SizeValidation.LessThanZero;

                    break;
                case SizeCriteria.Between:
                    if (firstTotalSize > secondTotalSize)
                        return SizeValidation.NegativeSpan;

                    break;
            }

            return SizeValidation.Good;
        }
    }
}
