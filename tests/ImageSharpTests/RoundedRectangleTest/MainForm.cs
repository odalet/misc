using System;
using System.Drawing;
using System.Windows.Forms;
using SI= SixLabors.ImageSharp;

namespace RoundedRectangleTest
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            Lena = ImageSharpUtils.GetLena();
        }

        private SI.Image Lena { get; }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Render();
        }

        private void SetImage(Image image)
        {
            var original = pictureBox.Image;
            pictureBox.Image = image;
            if (original != null)
                original.Dispose();
        }

        private void Render()
        {
            var previousCursor = Cursor;
            Cursor = Cursors.WaitCursor;
            try
            {
                var transformed = ImageSharpUtils.Render(Lena, percentTrackBar.Value);
                SetImage(transformed.ToImage());
            }
            finally
            {
                Cursor = previousCursor;
            }
        }

        private void refreshButton_Click(object sender, EventArgs e) => Render();
        private void percentTrackBar_ValueChanged(object sender, EventArgs e) => Render();
    }
}
