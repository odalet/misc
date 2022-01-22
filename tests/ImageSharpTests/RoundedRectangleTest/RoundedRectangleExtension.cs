﻿using System.Collections.Generic;
using System.Linq;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing;

namespace RoundedRectangleTest
{
    public static class RoundedRectangleExtension
    {
        public static IPath ToRoundedRectangle(this RectangleF rectangle, float cornerRadius)
        {
            IEnumerable<PointF> makeTopLeftCorner()
            {
                var ox = rectangle.Left + cornerRadius;
                var oy = rectangle.Top + cornerRadius;
                var clip = new RectangleF(rectangle.Left, rectangle.Top, cornerRadius, cornerRadius);
                var ellipse = new EllipsePolygon(ox, oy, cornerRadius);
                return ellipse.ClipCorner(clip);
            }

            IEnumerable<PointF> makeTopRightCorner()
            {
                var ox = rectangle.Right - cornerRadius;
                var oy = rectangle.Top + cornerRadius;
                var clip = new RectangleF(ox, rectangle.Top, cornerRadius, cornerRadius);
                var ellipse = new EllipsePolygon(ox, oy, cornerRadius);
                return ellipse.ClipCorner(clip);
            }

            IEnumerable<PointF> makeBottomRightCorner()
            {
                var ox = rectangle.Right - cornerRadius;
                var oy = rectangle.Bottom - cornerRadius;
                var clip = new RectangleF(ox, oy, cornerRadius, cornerRadius);
                var ellipse = new EllipsePolygon(ox, oy, cornerRadius);
                return ellipse.ClipCorner(clip);
            }

            IEnumerable<PointF> makeBottomLeftCorner()
            {
                var ox = rectangle.Left + cornerRadius;
                var oy = rectangle.Bottom - cornerRadius;
                var clip = new RectangleF(rectangle.Left, oy, cornerRadius, cornerRadius);
                var ellipse = new EllipsePolygon(ox, oy, cornerRadius);

                // Special case here: the first point should be returned last; other ones are good
                var clipped = ellipse.ClipCorner(clip);
                var first = clipped.First();
                foreach (var point in clipped.Skip(1))
                    yield return point;
                yield return first;
            }

            return new PathBuilder()
                .AddLines(makeTopLeftCorner())
                .AddLines(makeTopRightCorner())
                .AddLines(makeBottomRightCorner())
                .AddLines(makeBottomLeftCorner())
                .CloseFigure()
                .Build()
                ;
        }

        private static bool IsInRect(this PointF point, RectangleF rectangle) =>
            point.X >= rectangle.Left && point.X <= rectangle.Right &&
            point.Y >= rectangle.Top && point.Y <= rectangle.Bottom;

        private static IEnumerable<PointF> ClipCorner(this EllipsePolygon ellipse, RectangleF clip) =>
            ellipse.Points.ToArray().Where(p => p.IsInRect(clip));
    }
}