using System;

namespace Editor.StreetsEditor
{
    public class StreetSegment : Line
    {
        #region LeftSideWalkPoint1
        private Point _leftSideWalkPoint1;
        public Point LeftSideWalkPoint1
        {
            get { return _leftSideWalkPoint1; }
            set
            {
                _leftSideWalkPoint1 = value;
                OnPropertyChanged("LeftSideWalkPoint1");
            }
        }
        #endregion

        #region LeftSideWalkPoint2
        private Point _leftSideWalkPoint2;
        public Point LeftSideWalkPoint2
        {
            get { return _leftSideWalkPoint2; }
            set
            {
                _leftSideWalkPoint2 = value;
                OnPropertyChanged("LeftSideWalkPoint2");
            }
        }
        #endregion

        #region RightSideWalkPoint1
        private Point _rightSideWalkPoint1;
        public Point RightSideWalkPoint1
        {
            get { return _rightSideWalkPoint1; }
            set
            {
                _rightSideWalkPoint1 = value;
                OnPropertyChanged("RightSideWalkPoint1");
            }
        }
        #endregion

        #region RightSideWalkPoint2
        private Point _rightSideWalkPoint2;
        public Point RightSideWalkPoint2
        {
            get { return _rightSideWalkPoint2; }
            set
            {
                _rightSideWalkPoint2 = value;
                OnPropertyChanged("RightSideWalkPoint2");
            }
        }
        #endregion

        #region RightCycle
        private Cycle _rightCycle;
        public Cycle RightCycle
        {
            get { return _rightCycle; }
            set
            {
                _rightCycle = value;
                OnPropertyChanged("RightCycle");
            }
        }
        #endregion
        #region LeftCycle
        private Cycle _leftCycle;
        public Cycle LeftCycle
        {
            get { return _leftCycle; }
            set
            {
                _leftCycle = value;
                OnPropertyChanged("LeftCycle");
            }
        }
        #endregion

        #region IsSelected
        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                OnPropertyChanged("IsSelected");
            }
        }
        #endregion

        #region Name
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }
        #endregion

        #region Constructor
        public StreetSegment(Point point1, Point point2, string name) : base(point1, point2)
        {
            _leftSideWalkPoint1 = new Point(0, 0, 0);
            _leftSideWalkPoint2 = new Point(0, 0, 0);

            _rightSideWalkPoint1 = new Point(0, 0, 0);
            _rightSideWalkPoint2 = new Point(0, 0, 0);

            Name = name;

            CalculateSideWalk();

            point1.PropertyChanged += Point_PropertyChanged;
            point2.PropertyChanged += Point_PropertyChanged;
        }
        #endregion


        public Street Street { get; set; }

        #region Next and previous streets
        private StreetSegment _nextLeftStreetSegment;
        public StreetSegment NextLeftStreetSegment
        {
            get { return _nextLeftStreetSegment; }
            set
            {
                _nextLeftStreetSegment = value;
                OnPropertyChanged("NextLeftStreetSegment");
            }
        }

        private StreetSegment _nextRightStreetSegment;
        public StreetSegment NextRightStreetSegment
        {
            get { return _nextRightStreetSegment; }
            set
            {
                _nextRightStreetSegment = value;
                OnPropertyChanged("NextRightStreetSegment");
            }
        }

        private StreetSegment _previousLeftStreetSegment;
        public StreetSegment PreviousLeftStreetSegment
        {
            get { return _previousLeftStreetSegment; }
            set
            {
                _previousLeftStreetSegment = value;
                OnPropertyChanged("PreviousLeftStreetSegment");
            }
        }

        private StreetSegment _previousRightStreetSegment;
        public StreetSegment PreviousRightStreetSegment
        {
            get { return _previousRightStreetSegment; }
            set
            {
                _previousRightStreetSegment = value;
                OnPropertyChanged("PreviousRightStreetSegment");
            }
        }
        #endregion

        private void Point_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            CalculateSideWalk();
        }

        #region CalculateSideWalk

        protected void CalculateSideWalk() 
        {


            var normal = new Point(Point1.Y - Point2.Y, -Point1.X + Point2.X, 0);

            var length = Math.Sqrt(normal.X * normal.X + normal.Y * normal.Y);

            normal.X = normal.X / length;
            normal.Y = normal.Y / length;

            const double size = 30;

            _rightSideWalkPoint1.SetCoordinates(Point1.X + normal.X * size, Point1.Y + normal.Y * size, 0.0);
            _rightSideWalkPoint2.SetCoordinates(Point2.X + normal.X * size, Point2.Y + normal.Y * size, 0.0);

            _leftSideWalkPoint1.SetCoordinates(Point1.X - normal.X * size, Point1.Y - normal.Y * size, 0.0);
            _leftSideWalkPoint2.SetCoordinates(Point2.X - normal.X * size, Point2.Y - normal.Y * size, 0.0);

        }

        #endregion
    }
}
