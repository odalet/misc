using System;
using System.ComponentModel;

using CITray.Api.UI;

namespace CITray.UI.Options
{
    public partial class ErrorPanel : BaseOptionsPanel
    {
        private Exception exception = null;
        public ErrorPanel()
        {
            InitializeComponent();
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Exception Exception 
        {
            get { return exception; }
            set
            {
                exception = value;
                UpdateControl();
            }
        }

        private void UpdateControl()
        {
            if (exception == null) rtb.Text = "Unknown Error...";
            else rtb.Text = exception.ToString();
        }
    }
}
