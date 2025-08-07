using System;
using System.Windows.Forms;
using Delta.AnsiToEscapedUnicodeTool.Engine;

namespace Delta.AnsiToEscapedUnicodeTool
{
    internal partial class MainControl : UserControl
    {
        private CodecOptions options;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainControl"/> class.
        /// </summary>
        public MainControl()
        {
            InitializeComponent();

            oneSingleQuoteRadioButton.CheckedChanged += (s, _) => UpdateOptions();
            twoSingleQuotesRadioButton.CheckedChanged += (s, _) => UpdateOptions();
            oneCurlyQuoteRadioButton.CheckedChanged += (s, _) => UpdateOptions();
            reinterpretCheckBox.CheckedChanged += (s, _) => UpdateOptions();

            LoadSettings();
        }

        private void UpdateOptions()
        {
            if (options == null)
                options = new CodecOptions();

            if (oneSingleQuoteRadioButton.Checked)
                options.SingleQuoteEscapeMode = SingleQuoteEscapeMode.Normal;
            if (twoSingleQuotesRadioButton.Checked)
                options.SingleQuoteEscapeMode = SingleQuoteEscapeMode.DoubleQuote;
            if (oneCurlyQuoteRadioButton.Checked)
                options.SingleQuoteEscapeMode = SingleQuoteEscapeMode.SpecialQuote;

            options.ReinterpretQuotes = reinterpretCheckBox.Checked;

            SaveSettings();
        }

        private void LoadSettings()
        {
            options = new CodecOptions();
            try
            {
                options.ReinterpretQuotes = Properties.Settings.Default.ReinterpretQuotes;
                var mode = Properties.Settings.Default.SingleQuoteEscapeMode;
                options.SingleQuoteEscapeMode = (SingleQuoteEscapeMode)mode;
            }
            catch (Exception ex)
            {
                var debugException = ex;
            }

            // Check the correct checkbox
            oneSingleQuoteRadioButton.Checked = options.SingleQuoteEscapeMode == SingleQuoteEscapeMode.Normal;
            twoSingleQuotesRadioButton.Checked = options.SingleQuoteEscapeMode == SingleQuoteEscapeMode.DoubleQuote;
            oneCurlyQuoteRadioButton.Checked = options.SingleQuoteEscapeMode == SingleQuoteEscapeMode.SpecialQuote;

            reinterpretCheckBox.Checked = options.ReinterpretQuotes;
        }

        private void SaveSettings()
        {
            if (options == null) return;

            Properties.Settings.Default.SingleQuoteEscapeMode = (int)options.SingleQuoteEscapeMode;
            Properties.Settings.Default.Save();
            Properties.Settings.Default.Reload();
        }

        private void atouButton_Click(object sender, EventArgs e)
        {
            outputBox.Text = Codec.EncodeNonAsciiCharacters(inputBox.Text, options);
        }

        private void utoaButton_Click(object sender, EventArgs e)
        {
            outputBox.Text = Codec.DecodeEncodedNonAsciiCharacters(inputBox.Text,options);
        }
    }
}
