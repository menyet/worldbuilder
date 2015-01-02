using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editor.StreetsEditor
{
    public class Point : NotifyPropertyChanged
    {
        protected double x;
        public double X
        {
            get { return x; }
            set
            {
                x = value;
                OnPropertyChanged("X");
            }
        }

        protected double y;
        public double Y
        {
            get { return y; }
            set
            {
                y = value;
                OnPropertyChanged("Y");
            }
        }

        protected double z;
        public double Z
        {
            get { return z; }
            set 
            {
                z = value;
                OnPropertyChanged("Z");
            }
        }

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


        public Point(double X, double Y, double Z)
        {
            this.X = X;
            this.Y = Y;
            this.Y = Y;
        }

        public void SetCoordinates(double X, double Y, double Z)
        {
            this.X = X;
            this.Y = Y;
            this.Y = Y;
        }

    }
}
