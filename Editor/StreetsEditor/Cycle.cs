using System.Collections.Generic;

namespace Editor.StreetsEditor
{
    public class Cycle : NotifyPropertyChanged
    {
        protected List<StreetSegment> streetList = new List<StreetSegment>();
        public List<StreetSegment> StreetList;

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


        protected System.Windows.Media.Brush color;
        public System.Windows.Media.Brush Color
        {
            get { return color; }
            set
            {
                color = value;
                OnPropertyChanged("Color");
            }
        }


    }
}
