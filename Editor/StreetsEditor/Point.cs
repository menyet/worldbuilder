using GalaSoft.MvvmLight;

namespace Editor.StreetsEditor
{
    public class Point : ViewModelBase
    {
        private double _x;
        private double _y;
        private double _z;
        private bool _isSelected;

        public double X
        {
            get { return _x; }
            set { Set(() => X, ref _x, value); }
        }

        public double Y
        {
            get { return _y; }
            set { Set(() => Y, ref _y, value); }
        }

        public double Z
        {
            get { return _z; }
            set { Set(() => Z, ref _z, value); }
        }

        public bool IsSelected
        {
            get { return _isSelected; }
            set { Set(() => IsSelected, ref _isSelected, value); }
        }

        public Point(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public void SetCoordinates(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

    }
}
