using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Editor.StreetsEditor.RenderCubes
{
    public class RenderCube
    {
        public RenderCube(SplitType splitType, string address)
        {
            Console.WriteLine($"Creating cube {address}");

            SplitType = splitType;
            //SplitType = SplitType.Vertical;

            _address = address;
        }

        public SplitType SplitType { get; }

        private string _address;
        
        public double MinX { get; set; }

        public double MaxX { get; set; }

        public double MinY { get; set; }

        public double MaxY { get; set; }

        public List<Tuple<Point, Point>> Lines { get; } = new List<Tuple<Point, Point>>();

        public RenderCube[] Children { get; set; }

        private bool IsInFirstCube(Point p)
        {
            switch (SplitType)
            {
                case SplitType.Horizontal:
                    return p.X < (MinX + MaxX) / 2;
                case SplitType.Vertical:
                    return p.Y < (MinY + MaxY) / 2;
            }

            throw new InvalidOperationException("Invalid SplitType");
        }

        private void AddInternal(Tuple<Point, Point> line)
        {
            var p1 = line.Item1;
            var p2 = line.Item2;

            var isPoint1InCube1 = IsInFirstCube(line.Item1);
            var isPoint2InCube1 = IsInFirstCube(line.Item2);

            if (isPoint1InCube1 == isPoint2InCube1)
            {
                var index = isPoint1InCube1 ? 0 : 1;

                if (line.Item1.Y < Children[index].MinY - double.Epsilon
                || line.Item2.Y < Children[index].MinY - double.Epsilon
                || line.Item1.Y > Children[index].MaxY + double.Epsilon
                || line.Item2.Y > Children[index].MaxY + double.Epsilon
                )
                {
                    throw new InvalidOperationException("Invalid measures");
                }

                //Console.WriteLine($"Addign line to child {index}");
                Children[index].Add(line);
            }
            else if (SplitType == SplitType.Horizontal)
            {

                var middleX = (MinX + MaxX) / 2;
                var middleY = p1.Y + (p2.Y - p1.Y) / (p2.X - p1.X) * (middleX - p1.X);

                var middlePoint = new Point(middleX, middleY, 0);

                var l1 = new Tuple<Point, Point>(p1, new Point(middleX, middleY, 0));
                var l2 = new Tuple<Point, Point>(p2, new Point(middleX, middleY, 0));

                var index = isPoint1InCube1 ? 0 : 1;

                Children[index].Add(l1);
                Children[(index + 1) % 2].Add(l2);

            }
            else if (SplitType == SplitType.Vertical)
            {

                var middleY = (MinY + MaxY) / 2;
                var middleX = p1.X + (p2.X - p1.X) / (p2.Y - p1.Y) * (middleY - p1.Y);

                var middlePoint = new Point(middleX, middleY, 0);

                var l1 = new Tuple<Point, Point>(p1, new Point(middleX, middleY, 0));
                var l2 = new Tuple<Point, Point>(p2, new Point(middleX, middleY, 0));

                var index = isPoint1InCube1 ? 0 : 1;

                Children[index].Add(l1);
                Children[(index + 1) % 2].Add(l2);

            }
            else
            {
                throw new InvalidOperationException();
            }

        }

        public void Add(Tuple<Point, Point> line)
        {
            if (line.Item1.Y < MinY - double.Epsilon
                || line.Item2.Y < MinY - double.Epsilon
                || line.Item1.Y > MaxY + double.Epsilon
                || line.Item2.Y > MaxY + double.Epsilon
                )
            {
                throw new InvalidOperationException("Invalid measures");
            }

            if (Children != null)
            {
                AddInternal(line);
                return;
            }

            Lines.Add(line);

            if (Lines.Count > 100)
            {
                var childSplitType = SplitType == SplitType.Horizontal ?
                    SplitType.Vertical : SplitType.Horizontal;

                //splitType = SplitType.Vertical;

                var c1 = new RenderCube(childSplitType, _address + "1")
                {
                    MinX = MinX,
                    MinY = MinY,
                    MaxX = SplitType == SplitType.Horizontal ? (MinX + MaxX) / 2.0 : MaxX,
                    MaxY = SplitType == SplitType.Horizontal ? MaxY : (MinY + MaxY) / 2.0,
                };
                var c2 = new RenderCube(childSplitType, _address + "2")
                {
                    MinX = SplitType == SplitType.Horizontal ? (MinX + MaxX) / 2.0 : MinX,
                    MinY = SplitType == SplitType.Horizontal ? MinY : (MinY + MaxY) / 2.0,
                    MaxX = MaxX,
                    MaxY = MaxY,
                };

                Children = new[] { c1, c2 };
                foreach (var l in Lines)
                {
                    AddInternal(l);
                }

                Lines.Clear();
            }
        }
    }
}
