using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editor.StreetsEditor.RenderCubes
{
    public class RenderCube
    {
        public double MinX { get; set; }

        public double MaxX { get; set; }

        public double MinY { get; set; }

        public double MaxY { get; set; }

        public List<StreetSegment> StreetSegments { get; set; }
    }

    public class RenderCubesGenerator
    {
        public static RenderCube[] Generate(List<StreetSegment> segments)
        {
            var minX = segments.Min(_ => Math.Min(_.Point1.X, _.Point2.X));
            var maxX = segments.Max(_ => Math.Max(_.Point1.X, _.Point2.X));

            var minY = segments.Min(_ => Math.Min(_.Point1.Y, _.Point2.Y));
            var maxY = segments.Max(_ => Math.Max(_.Point1.Y, _.Point2.Y));

            return null;
        }
    }
}
