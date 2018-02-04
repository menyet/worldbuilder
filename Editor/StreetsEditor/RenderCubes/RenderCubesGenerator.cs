using System;
using System.Collections.Generic;
using System.Linq;

namespace Editor.StreetsEditor.RenderCubes
{
    public class RenderCubesGenerator
    {
        public static RenderCube Generate(IEnumerable<StreetSegment> segments)
        {
            var minX = segments.Min(_ => Math.Min(_.Point1.X, _.Point2.X));
            var maxX = segments.Max(_ => Math.Max(_.Point1.X, _.Point2.X));

            var minY = segments.Min(_ => Math.Min(_.Point1.Y, _.Point2.Y));
            var maxY = segments.Max(_ => Math.Max(_.Point1.Y, _.Point2.Y));

            var root = new RenderCube(SplitType.Horizontal, "R")
            {
                MinX = segments.Min(_ => Math.Min(_.Point1.X, _.Point2.X)),
                MaxX = segments.Max(_ => Math.Max(_.Point1.X, _.Point2.X)),
                MinY = segments.Min(_ => Math.Min(_.Point1.Y, _.Point2.Y)),
                MaxY = segments.Max(_ => Math.Max(_.Point1.Y, _.Point2.Y)),
            };

            foreach(var s in segments)
            {
                root.Add(new Tuple<Point, Point>(s.Point1, s.Point2));
            }

            return root;
        }
    }
}
