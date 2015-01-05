using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editor.StreetsEditor
{
    using System.Globalization;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Media;

    using Editor.OpenStreetMapImporter;

    public class StreetsEditorOptimized : FrameworkElement
    {
        public static readonly DependencyProperty MapProperty = DependencyProperty.Register("Map", typeof(Map), typeof(StreetsEditor), new PropertyMetadata(default(Map)));

        public Map Map
        {
            get { return (Map)GetValue(MapProperty); }
            set { SetValue(MapProperty, value); }
        }

        public static readonly DependencyProperty ZoomProperty = DependencyProperty.Register(
            "Zoom", typeof(double), typeof(StreetsEditor), new FrameworkPropertyMetadata(1.0, FrameworkPropertyMetadataOptions.AffectsMeasure));

        public double Zoom
        {
            get { return (double)GetValue(ZoomProperty); }
            set { SetValue(ZoomProperty, value); }
        }

        public static readonly DependencyProperty OffsetXProperty = DependencyProperty.Register(
            "OffsetX", typeof(double), typeof(StreetsEditor), new FrameworkPropertyMetadata(default(double), FrameworkPropertyMetadataOptions.AffectsRender));

        public double OffsetX
        {
            get { return (double)GetValue(OffsetXProperty); }
            set { SetValue(OffsetXProperty, value); }
        }

        public static readonly DependencyProperty OffsetYProperty = DependencyProperty.Register(
            "OffsetY", typeof(double), typeof(StreetsEditor), new FrameworkPropertyMetadata(default(double), FrameworkPropertyMetadataOptions.AffectsMeasure));

        private bool _isMouseDown;

        private double _mouseX;

        private double _mouseY;

        public double OffsetY
        {
            get { return (double)GetValue(OffsetYProperty); }
            set { SetValue(OffsetYProperty, value); }
        }

        public StreetsEditorOptimized()
        {
            Map = new Importer().Import("D:\\map.osm");
            
            MouseMove += OnMouseMove;
            MouseDown += OnMouseDown;
            MouseWheel += OnMouseWheel;

        }

        private void OnMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
            {
                Zoom += 0.1;
            }
            else
            {
                Zoom -= 0.1;
            }

            this.InvalidateVisual();
        }

        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            _isMouseDown = true;

            /*if (e.RightButton == MouseButtonState.Pressed)
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
            }*/
        }


        private void OnMouseMove(object sender, MouseEventArgs mouseEventArgs)
        {
            var oldMouseX = _mouseX;
            var oldMouseY = _mouseY;

            _mouseX = mouseEventArgs.GetPosition(sender as StreetsEditorOptimized).X;
            _mouseY = mouseEventArgs.GetPosition(sender as StreetsEditorOptimized).Y;
            
            //if (_isMouseDown)
            if (mouseEventArgs.LeftButton == MouseButtonState.Pressed)
            {

                /*if (_selectedPoint != null)
                {
                    _selectedPoint.X = (_mouseX - OffsetX)/Zoom;
                    _selectedPoint.Y = (_mouseY - OffsetY)/Zoom;
                }
                else*/
                {
                    OffsetX += _mouseX - oldMouseX;
                    OffsetY += _mouseY - oldMouseY;
                    this.InvalidateVisual();
                }
            }
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            return availableSize;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            return finalSize;
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            drawingContext.DrawRectangle(
                new SolidColorBrush(Color.FromArgb(0, 0, 0, 0)),
                new Pen(new SolidColorBrush(Color.FromArgb(0, 0, 0, 0)), 1.0),
                new Rect(0, 0, ActualWidth, ActualHeight));

             



            foreach (var streetSegment in Map.StreetList)
            {

                if (streetSegment.Street != null)
                {

                    FormattedText formattedText = new FormattedText(
                        streetSegment.Street.Name,
                        CultureInfo.GetCultureInfo("en-us"),
                        FlowDirection.LeftToRight,
                        new Typeface("Verdana"),
                        32,
                        Brushes.Black);


                    /*drawingContext.DrawText(
                        formattedText,
                        new System.Windows.Point(streetSegment.Point1.X, streetSegment.Point1.Y));*/

                }

                drawingContext.DrawLine(new Pen(new SolidColorBrush(Color.FromRgb(0, 0, 0)), 1.0), new System.Windows.Point(streetSegment.Point1.X * Zoom + OffsetX, streetSegment.Point1.Y * Zoom + 500 + OffsetY), new System.Windows.Point(streetSegment.Point2.X * Zoom + OffsetX, streetSegment.Point2.Y * Zoom + 500 + OffsetY));
            }

            //drawingContext.DrawLine(new Pen(new SolidColorBrush(Color.FromRgb(0, 0, 0)), 1.0), new System.Windows.Point(0, 0), new System.Windows.Point(100, 100));

        }
    }
}
