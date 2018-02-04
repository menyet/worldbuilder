using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editor.StreetsEditor.RenderCubes
{
    public class RenderCube
    {
        public SplitType SplitType { get; set; }


        public double MinX { get; set; }

        public double MaxX { get; set; }

        public double MinY { get; set; }

        public double MaxY { get; set; }

        public List<Tuple<Point, Point>> Lines { get; } = new List<Tuple<Point, Point>>();

        public RenderCube[] Children { get; set; }

        public void Add(Tuple<Point, Point> line)
        {
            if (Children != null)
            {

            }

            Lines.Add(line);

            if (Lines.Count > 100)
            {
                var splitType = SplitType == SplitType.Horizontal ?
                    SplitType.Vertical : SplitType.Horizontal;

                Children = new[]
                {
                    new RenderCube
                    {
                        SplitType = splitType,
                        MinX = MinX,
                        MinY = MinY,
                        MaxX = splitType == SplitType.Horizontal ? (MinX + MaxX) / 2 : MaxX,
                        MaxY = splitType == SplitType.Horizontal ? MaxY : (MinY + MaxY) / 2,
                    },
                    new RenderCube
                    {
                        SplitType = splitType,
                        MinX = splitType == SplitType.Horizontal ? MaxX : (MinX + MaxX) / 2.0,
                        MinY = splitType == SplitType.Horizontal ? (MinY + MaxY) / 2.0 : MaxY,
                        MaxX = MaxX,
                        MaxY = MaxY,
                    }
                };
            }
        }
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
