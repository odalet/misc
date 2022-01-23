using System;
#if !IMAGESHARP_V2
using System.IO;
#endif
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SD = System.Drawing;

namespace RoundedRectangleTest
{
    internal static class ImageSharpUtils
    {
        public static Image GetLena() => Image.Load(Properties.Resources.LenaPngBytes);

#if IMAGESHARP_V2
        public static unsafe SD.Image ToImage(this Image<Rgba32> image)
        {
            var w = image.Width;
            var h = image.Height;
            var result = new SD.Bitmap(w, h, SD.Imaging.PixelFormat.Format32bppArgb);
            var bd = result.LockBits(new SD.Rectangle(0, 0, w, h), SD.Imaging.ImageLockMode.WriteOnly, SD.Imaging.PixelFormat.Format32bppArgb);            
            try
            {
                image.ProcessPixelRows(accessor =>
                {
                    var destination = new Span<Bgra32>((void*)bd.Scan0, w * h);
                    for (var y =0; y < w; y++)
                    {
                        var row = accessor.GetRowSpan(y);
                        for (var x =0; x < h; x++)
                        {
                            var bgra = new Bgra32();
                            bgra.FromRgba32(row[x]);
                            destination[y * w + x] = bgra;
                        }
                    }
                });
            }
            finally
            {
                result.UnlockBits(bd);
            }

            return result;
        }
#else
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
#endif
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
