namespace Yet_Another_Better_Search
{
    partial class SearchCriteriaForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SearchCriteriaForm));
            this.label1 = new System.Windows.Forms.Label();
            this.searchTypeCombo = new System.Windows.Forms.ComboBox();
            this.modifiedCriteriaCombo = new System.Windows.Forms.ComboBox();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.caseCheck = new System.Windows.Forms.CheckBox();
            this.regexCheck = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.firstModifiedDatePicker = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.nameCriteriaCombo = new System.Windows.Forms.ComboBox();
            this.modifiedAndLabel = new System.Windows.Forms.Label();
            this.secondModifiedDatePicker = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.creationCriteriaCombo = new System.Windows.Forms.ComboBox();
            this.firstCreationDatePicker = new System.Windows.Forms.DateTimePicker();
            this.secondCreationDatePicker = new System.Windows.Forms.DateTimePicker();
            this.creationAndLabel = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.sizeCriteriaCombo = new System.Windows.Forms.ComboBox();
            this.sizeAndLabel = new System.Windows.Forms.Label();
            this.firstSizeTextBox = new System.Windows.Forms.TextBox();
            this.firstSizeScaleCombo = new System.Windows.Forms.ComboBox();
            this.secondSizeScaleCombo = new System.Windows.Forms.ComboBox();
            this.secondSizeTextBox = new System.Windows.Forms.TextBox();
            this.okayBtn = new System.Windows.Forms.Button();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Search Type:";
            // 
            // searchTypeCombo
            // 
            this.searchTypeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.searchTypeCombo.FormattingEnabled = true;
            this.searchTypeCombo.Location = new System.Drawing.Point(89, 12);
            this.searchTypeCombo.Name = "searchTypeCombo";
            this.searchTypeCombo.Size = new System.Drawing.Size(90, 21);
            this.searchTypeCombo.TabIndex = 1;
            // 
            // modifiedCriteriaCombo
            // 
            this.modifiedCriteriaCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.modifiedCriteriaCombo.FormattingEnabled = true;
            this.modifiedCriteriaCombo.Location = new System.Drawing.Point(94, 66);
            this.modifiedCriteriaCombo.Name = "modifiedCriteriaCombo";
            this.modifiedCriteriaCombo.Size = new System.Drawing.Size(90, 21);
            this.modifiedCriteriaCombo.TabIndex = 1;
            // 
            // nameTextBox
            // 
            this.nameTextBox.Location = new System.Drawing.Point(152, 39);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(211, 20);
            this.nameTextBox.TabIndex = 2;
            this.nameTextBox.Visible = false;
            // 
            // caseCheck
            // 
            this.caseCheck.AutoSize = true;
            this.caseCheck.Location = new System.Drawing.Point(369, 41);
            this.caseCheck.Name = "caseCheck";
            this.caseCheck.Size = new System.Drawing.Size(83, 17);
            this.caseCheck.TabIndex = 3;
            this.caseCheck.Text = "Match Case";
            this.caseCheck.UseVisualStyleBackColor = true;
            this.caseCheck.Visible = false;
            // 
            // regexCheck
            // 
            this.regexCheck.AutoSize = true;
            this.regexCheck.Location = new System.Drawing.Point(458, 41);
            this.regexCheck.Name = "regexCheck";
            this.regexCheck.Size = new System.Drawing.Size(79, 17);
            this.regexCheck.TabIndex = 3;
            this.regexCheck.Text = "Use Regex";
            this.regexCheck.UseVisualStyleBackColor = true;
            this.regexCheck.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Modified Date:";
            // 
            // firstModifiedDatePicker
            // 
            this.firstModifiedDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.firstModifiedDatePicker.Location = new System.Drawing.Point(190, 67);
            this.firstModifiedDatePicker.Name = "firstModifiedDatePicker";
            this.firstModifiedDatePicker.Size = new System.Drawing.Size(105, 20);
            this.firstModifiedDatePicker.TabIndex = 5;
            this.firstModifiedDatePicker.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Name:";
            // 
            // nameCriteriaCombo
            // 
            this.nameCriteriaCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.nameCriteriaCombo.FormattingEnabled = true;
            this.nameCriteriaCombo.Location = new System.Drawing.Point(56, 39);
            this.nameCriteriaCombo.Name = "nameCriteriaCombo";
            this.nameCriteriaCombo.Size = new System.Drawing.Size(90, 21);
            this.nameCriteriaCombo.TabIndex = 1;
            // 
            // modifiedAndLabel
            // 
            this.modifiedAndLabel.AutoSize = true;
            this.modifiedAndLabel.Location = new System.Drawing.Point(301, 69);
            this.modifiedAndLabel.Name = "modifiedAndLabel";
            this.modifiedAndLabel.Size = new System.Drawing.Size(25, 13);
            this.modifiedAndLabel.TabIndex = 6;
            this.modifiedAndLabel.Text = "and";
            this.modifiedAndLabel.Visible = false;
            // 
            // secondModifiedDatePicker
            // 
            this.secondModifiedDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.secondModifiedDatePicker.Location = new System.Drawing.Point(332, 66);
            this.secondModifiedDatePicker.Name = "secondModifiedDatePicker";
            this.secondModifiedDatePicker.Size = new System.Drawing.Size(105, 20);
            this.secondModifiedDatePicker.TabIndex = 5;
            this.secondModifiedDatePicker.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 96);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(75, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Creation Date:";
            // 
            // creationCriteriaCombo
            // 
            this.creationCriteriaCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.creationCriteriaCombo.FormattingEnabled = true;
            this.creationCriteriaCombo.Location = new System.Drawing.Point(93, 93);
            this.creationCriteriaCombo.Name = "creationCriteriaCombo";
            this.creationCriteriaCombo.Size = new System.Drawing.Size(90, 21);
            this.creationCriteriaCombo.TabIndex = 1;
            // 
            // firstCreationDatePicker
            // 
            this.firstCreationDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.firstCreationDatePicker.Location = new System.Drawing.Point(189, 94);
            this.firstCreationDatePicker.Name = "firstCreationDatePicker";
            this.firstCreationDatePicker.Size = new System.Drawing.Size(105, 20);
            this.firstCreationDatePicker.TabIndex = 5;
            this.firstCreationDatePicker.Visible = false;
            // 
            // secondCreationDatePicker
            // 
            this.secondCreationDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.secondCreationDatePicker.Location = new System.Drawing.Point(331, 93);
            this.secondCreationDatePicker.Name = "secondCreationDatePicker";
            this.secondCreationDatePicker.Size = new System.Drawing.Size(105, 20);
            this.secondCreationDatePicker.TabIndex = 5;
            this.secondCreationDatePicker.Visible = false;
            // 
            // creationAndLabel
            // 
            this.creationAndLabel.AutoSize = true;
            this.creationAndLabel.Location = new System.Drawing.Point(300, 96);
            this.creationAndLabel.Name = "creationAndLabel";
            this.creationAndLabel.Size = new System.Drawing.Size(25, 13);
            this.creationAndLabel.TabIndex = 6;
            this.creationAndLabel.Text = "and";
            this.creationAndLabel.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 123);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(30, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Size:";
            // 
            // sizeCriteriaCombo
            // 
            this.sizeCriteriaCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.sizeCriteriaCombo.FormattingEnabled = true;
            this.sizeCriteriaCombo.Location = new System.Drawing.Point(49, 120);
            this.sizeCriteriaCombo.Name = "sizeCriteriaCombo";
            this.sizeCriteriaCombo.Size = new System.Drawing.Size(90, 21);
            this.sizeCriteriaCombo.TabIndex = 1;
            // 
            // sizeAndLabel
            // 
            this.sizeAndLabel.AutoSize = true;
            this.sizeAndLabel.Location = new System.Drawing.Point(276, 123);
            this.sizeAndLabel.Name = "sizeAndLabel";
            this.sizeAndLabel.Size = new System.Drawing.Size(25, 13);
            this.sizeAndLabel.TabIndex = 6;
            this.sizeAndLabel.Text = "and";
            this.sizeAndLabel.Visible = false;
            // 
            // firstSizeTextBox
            // 
            this.firstSizeTextBox.Location = new System.Drawing.Point(145, 120);
            this.firstSizeTextBox.Name = "firstSizeTextBox";
            this.firstSizeTextBox.Size = new System.Drawing.Size(69, 20);
            this.firstSizeTextBox.TabIndex = 2;
            this.firstSizeTextBox.Visible = false;
            // 
            // firstSizeScaleCombo
            // 
            this.firstSizeScaleCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.firstSizeScaleCombo.FormattingEnabled = true;
            this.firstSizeScaleCombo.Location = new System.Drawing.Point(220, 120);
            this.firstSizeScaleCombo.Name = "firstSizeScaleCombo";
            this.firstSizeScaleCombo.Size = new System.Drawing.Size(50, 21);
            this.firstSizeScaleCombo.TabIndex = 1;
            this.firstSizeScaleCombo.Visible = false;
            // 
            // secondSizeScaleCombo
            // 
            this.secondSizeScaleCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.secondSizeScaleCombo.FormattingEnabled = true;
            this.secondSizeScaleCombo.Location = new System.Drawing.Point(382, 120);
            this.secondSizeScaleCombo.Name = "secondSizeScaleCombo";
            this.secondSizeScaleCombo.Size = new System.Drawing.Size(50, 21);
            this.secondSizeScaleCombo.TabIndex = 1;
            this.secondSizeScaleCombo.Visible = false;
            // 
            // secondSizeTextBox
            // 
            this.secondSizeTextBox.Location = new System.Drawing.Point(307, 120);
            this.secondSizeTextBox.Name = "secondSizeTextBox";
            this.secondSizeTextBox.Size = new System.Drawing.Size(69, 20);
            this.secondSizeTextBox.TabIndex = 2;
            this.secondSizeTextBox.Visible = false;
            // 
            // okayBtn
            // 
            this.okayBtn.Location = new System.Drawing.Point(194, 160);
            this.okayBtn.Name = "okayBtn";
            this.okayBtn.Size = new System.Drawing.Size(75, 23);
            this.okayBtn.TabIndex = 7;
            this.okayBtn.Text = "Okay";
            this.okayBtn.UseVisualStyleBackColor = true;
            // 
            // cancelBtn
            // 
            this.cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelBtn.Location = new System.Drawing.Point(275, 160);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(75, 23);
            this.cancelBtn.TabIndex = 7;
            this.cancelBtn.Text = "Cancel";
            this.cancelBtn.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Red;
            this.label9.Location = new System.Drawing.Point(12, 144);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(82, 13);
            this.label9.TabIndex = 8;
            this.label9.Text = "(Warning Label)";
            this.label9.Visible = false;
            // 
            // SearchCriteriaForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(544, 194);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.okayBtn);
            this.Controls.Add(this.sizeAndLabel);
            this.Controls.Add(this.creationAndLabel);
            this.Controls.Add(this.modifiedAndLabel);
            this.Controls.Add(this.secondCreationDatePicker);
            this.Controls.Add(this.secondModifiedDatePicker);
            this.Controls.Add(this.firstCreationDatePicker);
            this.Controls.Add(this.firstModifiedDatePicker);
            this.Controls.Add(this.regexCheck);
            this.Controls.Add(this.caseCheck);
            this.Controls.Add(this.secondSizeTextBox);
            this.Controls.Add(this.firstSizeTextBox);
            this.Controls.Add(this.secondSizeScaleCombo);
            this.Controls.Add(this.nameTextBox);
            this.Controls.Add(this.firstSizeScaleCombo);
            this.Controls.Add(this.sizeCriteriaCombo);
            this.Controls.Add(this.creationCriteriaCombo);
            this.Controls.Add(this.modifiedCriteriaCombo);
            this.Controls.Add(this.nameCriteriaCombo);
            this.Controls.Add(this.searchTypeCombo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SearchCriteriaForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Search or Filter Criteria";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox searchTypeCombo;
        private System.Windows.Forms.ComboBox modifiedCriteriaCombo;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.CheckBox caseCheck;
        private System.Windows.Forms.CheckBox regexCheck;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker firstModifiedDatePicker;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox nameCriteriaCombo;
        private System.Windows.Forms.Label modifiedAndLabel;
        private System.Windows.Forms.DateTimePicker secondModifiedDatePicker;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox creationCriteriaCombo;
        private System.Windows.Forms.DateTimePicker firstCreationDatePicker;
        private System.Windows.Forms.DateTimePicker secondCreationDatePicker;
        private System.Windows.Forms.Label creationAndLabel;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox sizeCriteriaCombo;
        private System.Windows.Forms.Label sizeAndLabel;
        private System.Windows.Forms.TextBox firstSizeTextBox;
        private System.Windows.Forms.ComboBox firstSizeScaleCombo;
        private System.Windows.Forms.ComboBox secondSizeScaleCombo;
        private System.Windows.Forms.TextBox secondSizeTextBox;
        private System.Windows.Forms.Button okayBtn;
        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.Label label9;
    }
}