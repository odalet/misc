#if !IMAGESHARP_V2
using System;
using System.Collections.Generic;
using System.Numerics;
#endif
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing;

namespace RoundedRectangleTest
{
    public static class RoundedRectangleExtension
    {
        public static IPath ToRoundedRectangle(this RectangleF rectangle, float cornerRadius)
        {
            return new PathBuilder()
                .AddEllipticalArc(rectangle.Left + cornerRadius, rectangle.Top + cornerRadius, cornerRadius, cornerRadius, 0, -90, -90)
                .AddEllipticalArc(rectangle.Right - cornerRadius, rectangle.Top + cornerRadius, cornerRadius, cornerRadius, 0, 180, -90)
                .AddEllipticalArc(rectangle.Right - cornerRadius, rectangle.Bottom - cornerRadius, cornerRadius, cornerRadius, 0, 90, -90)
                .AddEllipticalArc(rectangle.Left + cornerRadius, rectangle.Bottom - cornerRadius, cornerRadius, cornerRadius, 0, 0, -90)
                .CloseFigure()
                .Build()
                ;
        }

#if !IMAGESHARP_V2
        // NB: this does not support passing the builder's transformation to the arc...
        private static PathBuilder AddEllipticalArc(this PathBuilder builder, float x, float y, float radiusX, float radiusY, float rotation, float startAngle, float sweepAngle) =>
            builder.AddSegment(new EllipticalArcLineSegment(x, y, radiusX, radiusY, rotation, startAngle, sweepAngle, Matrix3x2.Identity));

        // Copied from https://github.com/SixLabors/ImageSharp.Drawing/blob/7ffae70dfb9eaf8a7e03ed98d8c2e60e0aed2ed7/src/ImageSharp.Drawing/Shapes/EllipticalArcLineSegment.cs
        private sealed class EllipticalArcLineSegment : ILineSegment
        {
            private const float MinimumSqrDistance = 1.75f;
            private readonly PointF[] linePoints;
            private readonly float x;
            private readonly float y;
            private readonly float radiusX;
            private readonly float radiusY;
            private readonly float rotation;
            private readonly float startAngle;
            private readonly float sweepAngle;
            private readonly Matrix3x2 transformation;

            public EllipticalArcLineSegment(float x, float y, float radiusX, float radiusY, float rotation, float startAngle, float sweepAngle, Matrix3x2 transformation)
            {
                this.x = x;
                this.y = y;
                this.radiusX = radiusX;
                this.radiusY = radiusY;
                this.rotation = rotation % 360;
                this.startAngle = startAngle % 360;
                this.transformation = transformation;
                this.sweepAngle = sweepAngle;
                if (sweepAngle > 360)
                    this.sweepAngle = 360;

                if (sweepAngle < -360)
                    this.sweepAngle = -360;

                linePoints = GetDrawingPoints();
                EndPoint = linePoints[linePoints.Length - 1];
            }

            public PointF EndPoint { get; }

            public EllipticalArcLineSegment Transform(Matrix3x2 matrix) => matrix.IsIdentity
                ? this
                : new EllipticalArcLineSegment(x, y, radiusX, radiusY, rotation, startAngle, sweepAngle, Matrix3x2.Multiply(transformation, matrix));

            ILineSegment ILineSegment.Transform(Matrix3x2 matrix) => Transform(matrix);

            private PointF[] GetDrawingPoints()
            {
                var points = new List<PointF>() { CalculatePoint(startAngle) };

                if (sweepAngle < 0)
                {
                    for (var i = startAngle; i > startAngle + sweepAngle; i--)
                    {
                        var end = i - 1;
                        if (end <= startAngle + sweepAngle)
                            end = startAngle + sweepAngle;

                        points.AddRange(GetDrawingPoints(i, end, 0));
                    }
                }
                else
                {
                    for (var i = startAngle; i < startAngle + sweepAngle; i++)
                    {
                        var end = i + 1;
                        if (end >= startAngle + sweepAngle)
                            end = startAngle + sweepAngle;

                        points.AddRange(GetDrawingPoints(i, end, 0));
                    }
                }

                return points.ToArray();
            }

            private List<PointF> GetDrawingPoints(float start, float end, int depth)
            {
                if (depth > 1000)
                    return new List<PointF>();

                var points = new List<PointF>();

                var startP = CalculatePoint(start);
                var endP = CalculatePoint(end);
                if ((new Vector2(endP.X, endP.Y) - new Vector2(startP.X, startP.Y)).LengthSquared() < MinimumSqrDistance)
                    points.Add(endP);
                else
                {
                    float mid = start + (end - start) / 2;
                    points.AddRange(GetDrawingPoints(start, mid, depth + 1));
                    points.AddRange(GetDrawingPoints(mid, end, depth + 1));
                }

                return points;
            }

            private PointF CalculatePoint(float angle)
            {
                var x = radiusX * MathF.Sin(MathF.PI * angle / 180) * MathF.Cos(MathF.PI * rotation / 180) -
                    radiusY * MathF.Cos(MathF.PI * angle / 180) * MathF.Sin(MathF.PI * rotation / 180) + this.x;
                var y = radiusX * MathF.Sin(MathF.PI * angle / 180) * MathF.Sin(MathF.PI * rotation / 180) +
                    radiusY * MathF.Cos(MathF.PI * angle / 180) * MathF.Cos(MathF.PI * rotation / 180) + this.y;

                return PointF.Transform(new PointF(x, y), transformation);
            }

            public ReadOnlyMemory<PointF> Flatten() => linePoints;
        }
#endif
    }
}