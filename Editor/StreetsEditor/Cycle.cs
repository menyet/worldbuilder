using System.Collections.Generic;

namespace Editor.StreetsEditor
{
    public class Cycle : NotifyPropertyChanged
    {
        public List<StreetSegment> StreetList { get; set; }

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


        private System.Windows.Media.Brush _color;
        public System.Windows.Media.Brush Color
        {
            get { return _color; }
            set
            {
                _color = value;
                OnPropertyChanged("Color");
            }
        }


    }
}
