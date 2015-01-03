using System;

namespace Editor.StreetsEditor
{
    public class StreetSegment : Line
    {
        #region LeftSideWalkPoint1
        protected Point leftSideWalkPoint1;
        public Point LeftSideWalkPoint1
        {
            get { return leftSideWalkPoint1; }
            set
            {
                leftSideWalkPoint1 = value;
                OnPropertyChanged("LeftSideWalkPoint1");
            }
        }
        #endregion

        #region LeftSideWalkPoint2
        protected Point leftSideWalkPoint2;
        public Point LeftSideWalkPoint2
        {
            get { return leftSideWalkPoint2; }
            set
            {
                leftSideWalkPoint2 = value;
                OnPropertyChanged("LeftSideWalkPoint2");
            }
        }
        #endregion

        #region RightSideWalkPoint1
        protected Point rightSideWalkPoint1;
        public Point RightSideWalkPoint1
        {
            get { return rightSideWalkPoint1; }
            set
            {
                rightSideWalkPoint1 = value;
                OnPropertyChanged("RightSideWalkPoint1");
            }
        }
        #endregion

        #region RightSideWalkPoint2
        protected Point rightSideWalkPoint2;
        public Point RightSideWalkPoint2
        {
            get { return rightSideWalkPoint2; }
            set
            {
                rightSideWalkPoint2 = value;
                OnPropertyChanged("RightSideWalkPoint2");
            }
        }
        #endregion

        #region RightCycle
        protected Cycle rightCycle;
        public Cycle RightCycle
        {
            get { return rightCycle; }
            set
            {
                rightCycle = value;
                OnPropertyChanged("RightCycle");
            }
        }
        #endregion
        #region LeftCycle
        protected Cycle leftCycle;
        public Cycle LeftCycle
        {
            get { return leftCycle; }
            set
            {
                leftCycle = value;
                OnPropertyChanged("LeftCycle");
            }
        }
        #endregion

        #region IsSelected
        protected bool isSelected;
        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                isSelected = value;
                OnPropertyChanged("IsSelected");
            }
        }
        #endregion

        #region Name
        protected string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }
        #endregion

        #region Constructor
        public StreetSegment(Point Point1, Point Point2, string Name) : base(Point1, Point2)
        {
            leftSideWalkPoint1 = new Point(0, 0, 0);
            leftSideWalkPoint2 = new Point(0, 0, 0);

            rightSideWalkPoint1 = new Point(0, 0, 0);
            rightSideWalkPoint2 = new Point(0, 0, 0);

            this.Name = Name;

            calculateSideWalk();

            Point1.PropertyChanged += Point_PropertyChanged;
            Point2.PropertyChanged += Point_PropertyChanged;
        }
        #endregion


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

        protected StreetSegment _previousLeftStreetSegment;
        public StreetSegment PreviousLeftStreetSegment
        {
            get { return _previousLeftStreetSegment; }
            set
            {
                _previousLeftStreetSegment = value;
                OnPropertyChanged("PreviousLeftStreetSegment");
            }
        }
        
        protected StreetSegment _previousRightStreetSegment;
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
            calculateSideWalk();
        }

        #region CalculateSideWalk

        protected void calculateSideWalk() 
        {


            Point normal = new Point(Point1.Y - Point2.Y, -Point1.X + Point2.X, 0);

            double length = Math.Sqrt(normal.X * normal.X + normal.Y * normal.Y);

            normal.X = normal.X / length;
            normal.Y = normal.Y / length;

            double size = 30;

            rightSideWalkPoint1.SetCoordinates(Point1.X + normal.X * size, Point1.Y + normal.Y * size, 0.0);
            rightSideWalkPoint2.SetCoordinates(Point2.X + normal.X * size, Point2.Y + normal.Y * size, 0.0);

            leftSideWalkPoint1.SetCoordinates(Point1.X - normal.X * size, Point1.Y - normal.Y * size, 0.0);
            leftSideWalkPoint2.SetCoordinates(Point2.X - normal.X * size, Point2.Y - normal.Y * size, 0.0);

        }

        #endregion
    }
}
