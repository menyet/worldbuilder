using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;
using Editor.OpenStreetMapImporter;

namespace Editor.StreetsEditor
{
    /// <summary>
    /// Interaction logic for StreetsEditor.xaml
    /// </summary>
    public partial class StreetsEditor
    {

        public static readonly DependencyProperty MapProperty = DependencyProperty.Register("Map", typeof(Map), typeof(StreetsEditor), new PropertyMetadata(default(Map)));

        public Map Map
        {
            get { return (Map) GetValue(MapProperty); }
            set { SetValue(MapProperty, value); }
        }

        public static readonly DependencyProperty ZoomProperty = DependencyProperty.Register(
            "Zoom", typeof (double), typeof (StreetsEditor), new FrameworkPropertyMetadata(default(double), FrameworkPropertyMetadataOptions.AffectsMeasure));

        public double Zoom
        {
            get { return (double) GetValue(ZoomProperty); }
            set { SetValue(ZoomProperty, value); }
        }

        public static readonly DependencyProperty OffsetXProperty = DependencyProperty.Register(
            "OffsetX", typeof (double), typeof (StreetsEditor), new FrameworkPropertyMetadata(default(double), FrameworkPropertyMetadataOptions.AffectsMeasure));

        public double OffsetX
        {
            get { return (double) GetValue(OffsetXProperty); }
            set { SetValue(OffsetXProperty, value); }
        }

        public static readonly DependencyProperty OffsetYProperty = DependencyProperty.Register(
            "OffsetY", typeof(double), typeof(StreetsEditor), new FrameworkPropertyMetadata(default(double), FrameworkPropertyMetadataOptions.AffectsMeasure));

        public double OffsetY
        {
            get { return (double) GetValue(OffsetYProperty); }
            set { SetValue(OffsetYProperty, value); }
        }

        //public Map Map { get; set; }

        public StreetsEditor()
        {
            //Map = new Map();

            Map = new Importer().Import("D:\\map.osm");

            /*Point point1 = new Point(100, 100,0);
            Point point2 = new Point(200, 100, 0);
            Point point3 = new Point(100, 200, 0);
            Point point4 = new Point(200, 200, 0);

            
            Map.StreetList.Add(new StreetSegment(point1, point2, "0"));
            Map.StreetList.Add(new StreetSegment(point2, point4, "1"));
            Map.StreetList.Add(new StreetSegment(point4, point3, "2"));
            Map.StreetList.Add(new StreetSegment(point1, point3, "3"));*/

            InitializeComponent();

            

        }

        Point _selectedPoint;
        bool _isMouseDown;

        private void StreetPoint_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _isMouseDown = true;

            if (e.RightButton == MouseButtonState.Pressed)
            {
                _selectedPoint.IsSelected = false;
                
                var pointEllipse = sender as Ellipse;
                var newPoint = pointEllipse.Tag as Point;
                Map.StreetList.Add(new StreetSegment(_selectedPoint, newPoint, Map.StreetList.Count().ToString(CultureInfo.InvariantCulture)));

                _selectedPoint = newPoint;
                _selectedPoint.IsSelected = true;

                e.Handled = true;
            }
            else
            {

                if (_selectedPoint != null)
                {
                    _selectedPoint.IsSelected = false;
                }

                var pointEllipse = sender as Ellipse;
                _selectedPoint = pointEllipse.Tag as Point;
                _selectedPoint.IsSelected = true;

                e.Handled = true;

                //Canvas.SetLeft(pointEllipse, Canvas.GetLeft(pointEllipse) + 1);
                //selectedPoint.X++;
            }
        }

        private double _mouseX;
        private double _mouseY;

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            var oldMouseX = _mouseX;
            var oldMouseY = _mouseY;

            _mouseX = e.GetPosition(sender as Canvas).X;
            _mouseY = e.GetPosition(sender as Canvas).Y;


            if (_isMouseDown)
            {

                if (_selectedPoint != null)
                {
                    _selectedPoint.X = (_mouseX - OffsetX)/Zoom;
                    _selectedPoint.Y = (_mouseY - OffsetY)/Zoom;
                }
                else
                {
                    OffsetX += _mouseX - oldMouseX;
                    OffsetY += _mouseY - oldMouseY;
                }
            }
            
        }

        private void Canvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            _isMouseDown = false;

            /*if (selectedPoint != null)
            {
                selectedPoint.IsSelected = false;
                selectedPoint = null;
            }*/
        }

        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _isMouseDown = true;

            if (e.RightButton == MouseButtonState.Pressed)
            {
                _selectedPoint.IsSelected = false;
                var newPoint = new Point(e.GetPosition((Canvas)sender).X, e.GetPosition((Canvas)sender).Y, 0);

                Map.StreetList.Add(new StreetSegment(_selectedPoint, newPoint,
                    Map.StreetList.Count().ToString(CultureInfo.InvariantCulture)));

                _selectedPoint = newPoint;
                _selectedPoint.IsSelected = true;
            }
            else
            {
                if (_selectedPoint != null)
                {
                    _selectedPoint.IsSelected = false;
                    _selectedPoint = null;
                }
            }
            
        }

        private void BuildSidewalks_Click(object sender, RoutedEventArgs e)
        {
            Map.BuildSideWalks();
        }


        private void StreetsEditor_OnMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
            {
                Zoom += 0.1;
            }
            else
            {
                Zoom -= 0.1;
            }
        }
    }
}
