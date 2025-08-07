using System;
using System.Windows.Forms;

namespace Delta.AnsiToEscapedUnicodeTool
{
    internal partial class MainForm : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainForm"/> class.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();

            base.Text = "Ansi To Escaped Unicode Tool " + ThisAssembly.Version; 
        }
    }
}
