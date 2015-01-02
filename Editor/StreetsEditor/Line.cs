using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editor.StreetsEditor
{
    public class Line : NotifyPropertyChanged
    {
        public Point Point1 { get; set; }
        public Point Point2 { get; set; }

        public Line(Point Point1, Point Point2)
        {
            this.Point1 = Point1;
            this.Point2 = Point2;
        }
    }
}
