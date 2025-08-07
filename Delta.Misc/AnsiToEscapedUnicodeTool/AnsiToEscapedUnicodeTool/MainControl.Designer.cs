namespace Delta.AnsiToEscapedUnicodeTool
{
    partial class MainControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.table = new System.Windows.Forms.TableLayoutPanel();
            this.outputBox = new System.Windows.Forms.RichTextBox();
            this.buttonsPanel = new System.Windows.Forms.Panel();
            this.utoaButton = new System.Windows.Forms.Button();
            this.atouButton = new System.Windows.Forms.Button();
            this.inputBox = new System.Windows.Forms.RichTextBox();
            this.optionsPanel = new System.Windows.Forms.Panel();
            this.reinterpretCheckBox = new System.Windows.Forms.CheckBox();
            this.oneCurlyQuoteRadioButton = new System.Windows.Forms.RadioButton();
            this.twoSingleQuotesRadioButton = new System.Windows.Forms.RadioButton();
            this.oneSingleQuoteRadioButton = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.table.SuspendLayout();
            this.buttonsPanel.SuspendLayout();
            this.optionsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // table
            // 
            this.table.ColumnCount = 1;
            this.table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.table.Controls.Add(this.outputBox, 0, 3);
            this.table.Controls.Add(this.buttonsPanel, 0, 2);
            this.table.Controls.Add(this.inputBox, 0, 1);
            this.table.Controls.Add(this.optionsPanel, 0, 0);
            this.table.Dock = System.Windows.Forms.DockStyle.Fill;
            this.table.Location = new System.Drawing.Point(0, 0);
            this.table.Name = "table";
            this.table.RowCount = 4;
            this.table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 54F));
            this.table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37F));
            this.table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.table.Size = new System.Drawing.Size(564, 349);
            this.table.TabIndex = 0;
            // 
            // outputBox
            // 
            this.outputBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outputBox.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.outputBox.Location = new System.Drawing.Point(3, 223);
            this.outputBox.Name = "outputBox";
            this.outputBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth;
            this.outputBox.ShowSelectionMargin = true;
            this.outputBox.Size = new System.Drawing.Size(558, 123);
            this.outputBox.TabIndex = 2;
            this.outputBox.Text = "";
            this.outputBox.WordWrap = false;
            // 
            // buttonsPanel
            // 
            this.buttonsPanel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonsPanel.Controls.Add(this.utoaButton);
            this.buttonsPanel.Controls.Add(this.atouButton);
            this.buttonsPanel.Location = new System.Drawing.Point(130, 186);
            this.buttonsPanel.Name = "buttonsPanel";
            this.buttonsPanel.Size = new System.Drawing.Size(303, 31);
            this.buttonsPanel.TabIndex = 0;
            // 
            // utoaButton
            // 
            this.utoaButton.Location = new System.Drawing.Point(153, 3);
            this.utoaButton.Name = "utoaButton";
            this.utoaButton.Size = new System.Drawing.Size(144, 23);
            this.utoaButton.TabIndex = 1;
            this.utoaButton.Text = "Unicode --> Ansi";
            this.utoaButton.UseVisualStyleBackColor = true;
            this.utoaButton.Click += new System.EventHandler(this.utoaButton_Click);
            // 
            // atouButton
            // 
            this.atouButton.Location = new System.Drawing.Point(3, 3);
            this.atouButton.Name = "atouButton";
            this.atouButton.Size = new System.Drawing.Size(144, 23);
            this.atouButton.TabIndex = 0;
            this.atouButton.Text = "Ansi --> Unicode";
            this.atouButton.UseVisualStyleBackColor = true;
            this.atouButton.Click += new System.EventHandler(this.atouButton_Click);
            // 
            // inputBox
            // 
            this.inputBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inputBox.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.inputBox.Location = new System.Drawing.Point(3, 57);
            this.inputBox.Name = "inputBox";
            this.inputBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth;
            this.inputBox.ShowSelectionMargin = true;
            this.inputBox.Size = new System.Drawing.Size(558, 123);
            this.inputBox.TabIndex = 1;
            this.inputBox.Text = "";
            this.inputBox.WordWrap = false;
            // 
            // optionsPanel
            // 
            this.optionsPanel.Controls.Add(this.reinterpretCheckBox);
            this.optionsPanel.Controls.Add(this.oneCurlyQuoteRadioButton);
            this.optionsPanel.Controls.Add(this.twoSingleQuotesRadioButton);
            this.optionsPanel.Controls.Add(this.oneSingleQuoteRadioButton);
            this.optionsPanel.Controls.Add(this.label1);
            this.optionsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.optionsPanel.Location = new System.Drawing.Point(3, 3);
            this.optionsPanel.Name = "optionsPanel";
            this.optionsPanel.Size = new System.Drawing.Size(558, 48);
            this.optionsPanel.TabIndex = 0;
            // 
            // reinterpretCheckBox
            // 
            this.reinterpretCheckBox.AutoSize = true;
            this.reinterpretCheckBox.Location = new System.Drawing.Point(140, 27);
            this.reinterpretCheckBox.Name = "reinterpretCheckBox";
            this.reinterpretCheckBox.Size = new System.Drawing.Size(304, 17);
            this.reinterpretCheckBox.TabIndex = 4;
            this.reinterpretCheckBox.Text = "Reinterpret back quotes encoding when converting to Ansi";
            this.reinterpretCheckBox.UseVisualStyleBackColor = true;
            // 
            // oneCurlyQuoteCheckBox
            // 
            this.oneCurlyQuoteRadioButton.AutoSize = true;
            this.oneCurlyQuoteRadioButton.Location = new System.Drawing.Point(376, 3);
            this.oneCurlyQuoteRadioButton.Name = "oneCurlyQuoteCheckBox";
            this.oneCurlyQuoteRadioButton.Size = new System.Drawing.Size(103, 17);
            this.oneCurlyQuoteRadioButton.TabIndex = 3;
            this.oneCurlyQuoteRadioButton.TabStop = true;
            this.oneCurlyQuoteRadioButton.Text = "One Curly Quote";
            this.oneCurlyQuoteRadioButton.UseVisualStyleBackColor = true;
            // 
            // twoSingleQuotesCheckBox
            // 
            this.twoSingleQuotesRadioButton.AutoSize = true;
            this.twoSingleQuotesRadioButton.Location = new System.Drawing.Point(255, 3);
            this.twoSingleQuotesRadioButton.Name = "twoSingleQuotesCheckBox";
            this.twoSingleQuotesRadioButton.Size = new System.Drawing.Size(115, 17);
            this.twoSingleQuotesRadioButton.TabIndex = 2;
            this.twoSingleQuotesRadioButton.TabStop = true;
            this.twoSingleQuotesRadioButton.Text = "Two Single Quotes";
            this.twoSingleQuotesRadioButton.UseVisualStyleBackColor = true;
            // 
            // oneSingleQuoteCheckBox
            // 
            this.oneSingleQuoteRadioButton.AutoSize = true;
            this.oneSingleQuoteRadioButton.Location = new System.Drawing.Point(140, 3);
            this.oneSingleQuoteRadioButton.Name = "oneSingleQuoteCheckBox";
            this.oneSingleQuoteRadioButton.Size = new System.Drawing.Size(109, 17);
            this.oneSingleQuoteRadioButton.TabIndex = 1;
            this.oneSingleQuoteRadioButton.TabStop = true;
            this.oneSingleQuoteRadioButton.Text = "One Single Quote";
            this.oneSingleQuoteRadioButton.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Single Quotes Translation:";
            // 
            // MainControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.table);
            this.Name = "MainControl";
            this.Size = new System.Drawing.Size(564, 349);
            this.table.ResumeLayout(false);
            this.buttonsPanel.ResumeLayout(false);
            this.optionsPanel.ResumeLayout(false);
            this.optionsPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel table;
        private System.Windows.Forms.Panel buttonsPanel;
        private System.Windows.Forms.Button atouButton;
        private System.Windows.Forms.Button utoaButton;
        private System.Windows.Forms.RichTextBox outputBox;
        private System.Windows.Forms.RichTextBox inputBox;
        private System.Windows.Forms.Panel optionsPanel;
        private System.Windows.Forms.RadioButton oneCurlyQuoteRadioButton;
        private System.Windows.Forms.RadioButton twoSingleQuotesRadioButton;
        private System.Windows.Forms.RadioButton oneSingleQuoteRadioButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox reinterpretCheckBox;
    }
}
