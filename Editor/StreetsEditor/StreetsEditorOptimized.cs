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
    using Editor.StreetsEditor.RenderCubes;

    public class StreetsEditorOptimized : FrameworkElement
    {
        public static readonly DependencyProperty MapProperty = DependencyProperty.Register("Map", typeof(Map), typeof(StreetsEditor), new PropertyMetadata(default(Map)));

        public Map Map
        {
            get { return (Map)GetValue(MapProperty); }
            set { SetValue(MapProperty, value); }
        }

        private RenderCubes.RenderCube MainCube { get; }

        public static readonly DependencyProperty ZoomProperty = DependencyProperty.Register(
            "Zoom", typeof(double), typeof(StreetsEditor), new FrameworkPropertyMetadata(3.0, FrameworkPropertyMetadataOptions.AffectsMeasure));

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
            Map = new Importer().Import("C:\\map\\map");
            
            MouseMove += OnMouseMove;
            MouseDown += OnMouseDown;
            MouseWheel += OnMouseWheel;

            OffsetX = (Map.StreetList.Max(_ => _.Point1.X) - Map.StreetList.Min(_ => _.Point1.X)) / 2
                - Map.StreetList.Max(_ => _.Point1.X);

            OffsetY = (Map.StreetList.Max(_ => _.Point1.Y) - Map.StreetList.Min(_ => _.Point1.Y)) / 2
                - Map.StreetList.Max(_ => _.Point1.Y);

            MainCube = RenderCubesGenerator.Generate(Map.StreetList);
        }

        private void OnMouseWheel(object sender, MouseWheelEventArgs e)
        {
            Zoom += 0.001 * e.Delta;
            InvalidateVisual();
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

        Pen pen = new Pen(new SolidColorBrush(Color.FromRgb(0, 0, 0)), 1.0);

        Pen RedPen = new Pen(new SolidColorBrush(Color.FromRgb(150, 0, 0)), 3.0);

        const double padding = 200;

        double viewportLeft = padding;
        double viewportTop = padding;

        double viewportRight => ActualWidth - padding;
        double viewportBottom => ActualHeight - padding;

        double viewportWidth => viewportRight - viewportLeft;
        double viewportHeight => viewportBottom - viewportTop;


        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            drawingContext.DrawRectangle(
                new SolidColorBrush(Color.FromArgb(0, 0, 0, 0)),
                new Pen(new SolidColorBrush(Color.FromArgb(0, 0, 0, 0)), 1.0),
                new Rect(0, 0, ActualWidth, ActualHeight));


            RenderNodes(MainCube, drawingContext);

            drawingContext.DrawRectangle(
                new SolidColorBrush(Color.FromArgb(0, 0, 0, 0)),
                RedPen,
                new Rect(viewportLeft, viewportTop, viewportWidth, viewportHeight));

            //var minX = (viewportLeft - 500) / Zoom - OffsetX;
            //var maxX = (viewportRight - 500) / Zoom - OffsetX;

            //var minY = (viewportTop - 500) / Zoom - OffsetY;
            //var maxY = (viewportBottom - 500) / Zoom - OffsetY;

            //Console.WriteLine($"MinX  {minX} maxX  {maxX} MinY  {minY} MaxY  {maxY}");

            //Console.WriteLine($"RMinX {MainCube.MinX} RmaxX {MainCube.MaxX} RMinY {MainCube.MinY} RMaxY {MainCube.MaxY}");




            foreach (var streetSegment in Map.StreetList)
            {

                if (streetSegment.Street != null)
                {

                    //FormattedText formattedText = new FormattedText(
                    //    streetSegment.Street.Name,
                    //    CultureInfo.GetCultureInfo("en-us"),
                    //    FlowDirection.LeftToRight,
                    //    new Typeface("Verdana"),
                    //    32,
                    //    Brushes.Black);


                    /*drawingContext.DrawText(
                        formattedText,
                        new System.Windows.Point(streetSegment.Point1.X, streetSegment.Point1.Y));*/

                }

                //drawingContext.DrawLine(
                //    pen,
                //    new System.Windows.Point(
                //        streetSegment.Point1.X * Zoom + 500 + OffsetX * Zoom,
                //        streetSegment.Point1.Y * Zoom + 500 + OffsetY * Zoom),
                //    new System.Windows.Point(
                //        streetSegment.Point2.X * Zoom + 500 + OffsetX * Zoom,
                //        streetSegment.Point2.Y * Zoom + 500 + OffsetY * Zoom));

                
            }

            //drawingContext.DrawLine(new Pen(new SolidColorBrush(Color.FromRgb(0, 0, 0)), 1.0), new System.Windows.Point(0, 0), new System.Windows.Point(100, 100));

        }

        private void RenderNodes(RenderCube cube, DrawingContext drawingContext)
        {
            var minX = (viewportLeft - 500) / Zoom - OffsetX;
            var maxX = (viewportRight - 500) / Zoom - OffsetX;

            var minY = (viewportTop - 500) / Zoom - OffsetY;
            var maxY = (viewportBottom - 500) / Zoom - OffsetY;

            if (cube.MaxX < minX || cube.MinX > maxX ||
                    cube.MaxY < minY || cube.MinY > maxY)
            {
                return;
            }

                drawingContext.DrawRectangle(new SolidColorBrush(Color.FromRgb(200, 200, 200)),
                pen,
                new Rect(
                    new System.Windows.Point(cube.MinX * Zoom + 500 + OffsetX * Zoom,
                        cube.MinY * Zoom + 500 + OffsetY * Zoom),
                    new System.Windows.Point(cube.MaxX * Zoom + 500 + OffsetX * Zoom,
                        cube.MaxY * Zoom + 500 + OffsetY * Zoom)));

            if (cube.Children != null)
            {
                foreach(var i in cube.Children)
                {

                    //var minX = (viewportLeft - 500) / Zoom - OffsetX;
                    //var maxX = (viewportRight - 500) / Zoom - OffsetX;

                    //var minY = (viewportTop - 500) / Zoom - OffsetY;
                    //var maxY = (viewportBottom - 500) / Zoom - OffsetY;

                    //if (minX < i.MinX && i.MaxX < maxX &&
                    //    minY < i.MinY && i.MaxY < maxY)
                    //{
                        RenderNodes(i, drawingContext);
                    //}
                }
            }

            if (cube.Lines != null)
            {
                if (!(cube.MaxX < minX || cube.MinX > maxX ||
                    cube.MaxY < minY || cube.MinY > maxY))
                {
                    foreach (var ln in cube.Lines)
                    {
                        drawingContext.DrawLine(
                        pen,
                        new System.Windows.Point(
                            ln.Item1.X * Zoom + 500 + OffsetX * Zoom,
                            ln.Item1.Y * Zoom + 500 + OffsetY * Zoom),
                        new System.Windows.Point(
                            ln.Item2.X * Zoom + 500 + OffsetX * Zoom,
                            ln.Item2.Y * Zoom + 500 + OffsetY * Zoom));
                    }
                }
            }

        }
    }
}
