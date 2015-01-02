using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Editor.OpenStreetMapImporter;

namespace Editor.StreetsEditor
{
    /// <summary>
    /// Interaction logic for StreetsEditor.xaml
    /// </summary>
    public partial class StreetsEditor : UserControl
    {

        public static readonly DependencyProperty MapProperty = DependencyProperty.Register("Map", typeof(Map), typeof(StreetsEditor), new PropertyMetadata(default(Map)));

        public Map Map
        {
            get
            {
                return (Map)GetValue(MapProperty);
            }
            set
            {
                SetValue(MapProperty, value);
            }
        }

        //public Map Map { get; set; }

        protected string h = "Asd:";
        public string H 
        {
            get { return h; }
        }

        public StreetsEditor()
        {
            //Map = new Map();

            Map = new Importer().Import("D:\\map.osm");

            /*Point point1 = new Point(100, 100,0);
            Point point2 = new Point(200, 100, 0);
            Point point3 = new Point(100, 200, 0);
            Point point4 = new Point(200, 200, 0);

            
            Map.StreetList.Add(new Street(point1, point2, "0"));
            Map.StreetList.Add(new Street(point2, point4, "1"));
            Map.StreetList.Add(new Street(point4, point3, "2"));
            Map.StreetList.Add(new Street(point1, point3, "3"));*/

            InitializeComponent();

            

        }

        Point selectedPoint;
        bool IsMouseDown = false;

        private void StreetPoint_MouseDown(object sender, MouseButtonEventArgs e)
        {
            IsMouseDown = true;

            if (e.RightButton == MouseButtonState.Pressed)
            {
                selectedPoint.IsSelected = false;
                
                Ellipse pointEllipse = sender as Ellipse;
                Point newPoint = pointEllipse.Tag as Point;
                Map.StreetList.Add(new Street(selectedPoint, newPoint, Map.StreetList.Count().ToString()));

                selectedPoint = newPoint;
                selectedPoint.IsSelected = true;

                e.Handled = true;
            }
            else
            {

                if (selectedPoint != null)
                {
                    selectedPoint.IsSelected = false;
                }

                Ellipse pointEllipse = sender as Ellipse;
                selectedPoint = pointEllipse.Tag as Point;
                selectedPoint.IsSelected = true;

                e.Handled = true;

                //Canvas.SetLeft(pointEllipse, Canvas.GetLeft(pointEllipse) + 1);
                //selectedPoint.X++;
            }
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsMouseDown && (selectedPoint != null)) 
            {
                selectedPoint.X = e.GetPosition(sender as Canvas).X;
                selectedPoint.Y = e.GetPosition(sender as Canvas).Y;
            }
            
        }

        private void Canvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            IsMouseDown = false;

            /*if (selectedPoint != null)
            {
                selectedPoint.IsSelected = false;
                selectedPoint = null;
            }*/
        }

        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            IsMouseDown = true;

            if (e.RightButton == MouseButtonState.Pressed)
            {
                selectedPoint.IsSelected = false;
                Point newPoint = new Point(e.GetPosition((Canvas)sender).X, e.GetPosition((Canvas)sender).Y, 0);

                Map.StreetList.Add(new Street(selectedPoint, newPoint, Map.StreetList.Count().ToString()));

                selectedPoint = newPoint;
                selectedPoint.IsSelected = true;
            }
            else
            {
                if (selectedPoint != null)
                {
                    selectedPoint.IsSelected = false;
                    selectedPoint = null;
                }
            }
            
        }

        private void BuildSidewalks_Click(object sender, RoutedEventArgs e)
        {
            Map.BuildSideWalks();
        }

    }
}
