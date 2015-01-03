namespace Editor.StreetsEditor
{
    public class Line : NotifyPropertyChanged
    {
        public Point Point1 { get; set; }
        public Point Point2 { get; set; }

        public Line(Point point1, Point point2)
        {
            Point1 = point1;
            Point2 = point2;
        }
    }
}
