using System;
using System.IO;
using System.Runtime.InteropServices;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SD = System.Drawing;

namespace RoundedRectangleTest
{
    internal static class ImageSharpUtils
    {
        public static Image GetLena() => Image.Load(Properties.Resources.LenaPngBytes);

        //private struct Bgra32
        //{
        //    public byte B;
        //    public byte G;
        //    public byte R;
        //    public byte A;
        //}

        public static unsafe SD.Image ToImage(this Image<Rgba32> image)
        {
            if (!image.TryGetSinglePixelSpan(out var source))
            {
                // Slow...
                using var ms = new MemoryStream();
                image.SaveAsBmp(ms);
                return SD.Image.FromStream(ms);
            }

            var w = image.Width;
            var h = image.Height;

            var result = new SD.Bitmap(w, h, SD.Imaging.PixelFormat.Format32bppArgb);
            var bd = result.LockBits(new SD.Rectangle(0, 0, w, h), SD.Imaging.ImageLockMode.WriteOnly, SD.Imaging.PixelFormat.Format32bppArgb);
            try
            {
                var destination = new Span<Bgra32>((void*)bd.Scan0, w * h);
                for (var i = 0; i < w * h; i++)
                {
                    var bgra = new Bgra32();
                    bgra.FromRgba32(source[i]);
                    destination[i] = bgra;
                }
            }
            finally
            {
                result.UnlockBits(bd);
            }

            return result;
        }

        public static Image<Rgba32> Render(Image source, int percents)
        {
            var gray = source.CloneAs<L8>();
            var destination = gray.CloneAs<Rgba32>();
            var (width, height) = (destination.Width, destination.Height);
            destination.Mutate(x =>
            {
                var surface = new RenderSurface(x, width, height, true);
                surface.DrawProgressBar(percents, 3 * width / 4);
            });

            return destination;
        }
    }
}
