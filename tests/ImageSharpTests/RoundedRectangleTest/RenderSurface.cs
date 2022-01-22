using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Processing;

namespace RoundedRectangleTest
{
    internal enum FontSize
    {
        Small = 20,
        Medium = 40
    }

    internal sealed class RenderSurface
    {
        private readonly Color[] palette;
        private readonly Dictionary<FontSize, Font> fonts;

        public RenderSurface(IImageProcessingContext context, int width, int height, bool debugMode)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
            SurfaceWidth = width;
            SurfaceHeight = height;
            DebugMode = debugMode; // This draws red rectangles around the text in DrawText

            fonts = new Dictionary<FontSize, Font>
            {
                [FontSize.Small] = SystemFonts.CreateFont("Arial", (float)FontSize.Small),
                [FontSize.Medium] = SystemFonts.CreateFont("Arial", (float)FontSize.Medium)
            };

            // Make a red -> green palette for the progress bar
            palette = Enumerable.Range(0, 101).Select(i =>
            {
                var ratio = i / 100f;
                var r = 1f - ratio;
                var g = ratio;
                return new Color(new Vector4(r, g, 0f, 1f));
            }).ToArray();
        }

        private IImageProcessingContext Context { get; }
        private int SurfaceWidth { get; }
        private int SurfaceHeight { get; }
        private bool DebugMode { get; }

        public void DrawText(string text, HorizontalAlignment halign, VerticalAlignment valign, FontSize fontSize = FontSize.Small, int margin = 4)
        {
            var font = fonts[fontSize];
            var size = TextMeasurer.Measure(text, new RendererOptions(font));
            var aligned = Align((int)size.Width, (int)size.Height, halign, valign, margin);

            if (DebugMode)
                _ = Context.Draw(Pens.Solid(Color.Red, 1f), aligned);
            _ = Context.DrawText(text, font, Color.White, new PointF(aligned.X, aligned.Y));
        }

        public void DrawProgressBar(int percentage, int width, FontSize fontSize = FontSize.Medium, int borderThickness = 2, int cornerRadius = 4, int padding = 4)
        {
            // Clamp percentage to the [0;100] range
            var percents = percentage < 0 ? 0 : (percentage > 100 ? 100 : percentage);
            var text = $"{percents}%";

            var font = fonts[fontSize];

            // In order for the text to feel correctly vertically centered, we measure a string with characters
            // that go below (like g or j) and others that go up (like t or f). We say this should be the text height.
            var textHeight = TextMeasurer.Measure("gf", new RendererOptions(font)).Height;
            var textWidth = TextMeasurer.Measure(text, new RendererOptions(font)).Width;
            var textSizeWithPadding = new SizeF(textWidth + padding, textHeight + padding);

            var rectangleSize = new SizeF(
                width >= textSizeWithPadding.Width ? width : textSizeWithPadding.Width,
                textSizeWithPadding.Height);

            var color = palette[percents];
            var borderRectangle = Align(rectangleSize, HorizontalAlignment.Center, VerticalAlignment.Center, 0);

            if (cornerRadius <= 0) // No rounded corners
            {
                var valueWidth = borderRectangle.Width * (percents / 100f);
                var valueRectangle = new RectangularPolygon(
                    borderRectangle.Left, borderRectangle.Top,
                    valueWidth, borderRectangle.Height);

                _ = Context.Fill(color.WithAlpha(0.33f), valueRectangle);
                _ = Context.Draw(Pens.Solid(color, borderThickness), borderRectangle);
            }
            else
            {
                var invertedValueWidth = borderRectangle.Width * (1f - percents / 100f);
                var invertedValueRectangle = new RectangularPolygon(
                    borderRectangle.Right - invertedValueWidth, borderRectangle.Top,
                    invertedValueWidth, borderRectangle.Height);

                var contour = borderRectangle.ToRoundedRectangle(cornerRadius);
                var clippedValueRectangle = contour.Clip(invertedValueRectangle);

                _ = Context.Fill(color.WithAlpha(0.33f), clippedValueRectangle);
                _ = Context.Draw(Pens.Solid(color, borderThickness), contour);
            }

            // Draw the text
            var alignedText = Align(textWidth, textHeight, HorizontalAlignment.Center, VerticalAlignment.Center, 0);
            _ = Context.DrawText(text, font, Color.White, new PointF(alignedText.X, alignedText.Y));
        }

        private RectangleF Align(SizeF size, HorizontalAlignment halign, VerticalAlignment valign, int margin) =>
            Align(size.Width, size.Height, halign, valign, margin);

        private RectangleF Align(float width, float height, HorizontalAlignment halign, VerticalAlignment valign, int margin)
        {
            var w = (int)Math.Round(width);
            var left = halign switch
            {
                HorizontalAlignment.Left => margin,
                HorizontalAlignment.Right => SurfaceWidth - margin - w,
                _ => (SurfaceWidth - w) / 2,
            };

            var h = (int)Math.Round(height);
            var top = valign switch
            {
                VerticalAlignment.Top => margin,
                VerticalAlignment.Bottom => SurfaceHeight - margin - h,
                _ => (SurfaceHeight - h) / 2,
            };

            return new RectangleF(left < 0 ? 0 : left, top < 0 ? 0 : top, width, height);
        }
    }
}
