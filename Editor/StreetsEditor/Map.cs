using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
//using Editor.StreetsEditor.Classes;

namespace Editor.StreetsEditor
{
	public class Map
	{

		private readonly ObservableCollection<Cycle> _cycleList = new ObservableCollection<Cycle>();

        private readonly ObservableCollection<StreetSegment> _streetList = new ObservableCollection<StreetSegment>();
		public ObservableCollection<StreetSegment> StreetList
		{
			get { return _streetList; }
		}

		public StreetSegment GetStreetByPoints(Point point1, Point point2)
		{
			foreach (StreetSegment street in StreetList)
			{
				if ((street.Point1 == point1) && (street.Point2 == point2))
				{
					return street;
				}

				if ((street.Point1 == point2) && (street.Point2 == point1))
				{
					return street;
				}
			}
			return null;
		}

		public List<StreetSegment> GetStreetsByPoint(Point point)
		{
		    return StreetList.Where(street => (street.Point1 == point) || (street.Point2 == point)).ToList();
		}


	    public void BuildSideWalks()
		{
			byte c = 0;

			foreach (StreetSegment street in _streetList)
			{
				c += 50;


				var actualStreetSegment = street;

				if (actualStreetSegment.RightCycle != null)
				{
					continue;
				}

				var actualPoint = actualStreetSegment.Point1;
				var nextPoint = actualStreetSegment.Point2;

				var firstPoint = actualStreetSegment.Point1;


				var rightCycle = new Cycle();
				_cycleList.Add(rightCycle);

				rightCycle.Color = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(c, c, 40));

				rightCycle.IsSelected = true;

				actualStreetSegment.RightCycle = rightCycle;

				actualStreetSegment.IsSelected = true;

				do
				{
					StreetSegment nextStreetSegment = null;
					double angle = 1000;
					foreach (StreetSegment tmpStreet in GetStreetsByPoint(nextPoint))
					{


						if (tmpStreet != actualStreetSegment)
						{
							var v1 = new Vector(actualPoint.X - nextPoint.X, actualPoint.Y - nextPoint.Y);
							var v2 = new Vector(tmpStreet.Point2.X - tmpStreet.Point1.X, tmpStreet.Point2.Y - tmpStreet.Point1.Y);

							if (tmpStreet.Point2 == nextPoint)
							{
								v2.X = -v2.X;
								v2.Y = -v2.Y;
							}

							double tmpAngle = (Math.Atan2(v2.X, v2.Y) - Math.Atan2(v1.X, v1.Y)) * 180 / Math.PI;
							if (tmpAngle < 0) tmpAngle += 360;

							//double tmpAngle = Vector.AngleBetween(v1, v2);
							if (tmpAngle < angle)
							{
								angle = tmpAngle;
								nextStreetSegment = tmpStreet;
							}
						}
					}

					actualStreetSegment = nextStreetSegment;

					if (nextPoint == nextStreetSegment.Point1)
					{
						actualStreetSegment.RightCycle = rightCycle;
					}
					else
					{
						actualStreetSegment.LeftCycle = rightCycle;
					}


					actualPoint = (nextPoint == nextStreetSegment.Point1) ? nextStreetSegment.Point1 : nextStreetSegment.Point2;
					nextPoint = (nextPoint == nextStreetSegment.Point1) ? nextStreetSegment.Point2 : nextStreetSegment.Point1;
				} while (nextPoint != firstPoint);




			}



			// Now we are going to join sidewalks

			//foreach (Cycle cycle in cycleList)
			//{
			//	int index = 0;
			//	StreetSegment actualStreet = cycle.StreetList.ElementAt(index);

			//	while (index < cycle.StreetList.Count)
			//	{
			//		StreetSegment nextStreet = cycle.StreetList.ElementAt((index + 1) % cycle.StreetList.Count);

			//		double dX1 = actualStreet.RightSideWalkPoint1.X - actualStreet.RightSideWalkPoint2.X;
			//		double dX2 = nextStreet.RightSideWalkPoint1.X - nextStreet.RightSideWalkPoint2.X;

			//		double dY1 = actualStreet.RightSideWalkPoint1.Y - actualStreet.RightSideWalkPoint2.Y;
			//		double dY2 = nextStreet.RightSideWalkPoint1.Y - nextStreet.RightSideWalkPoint2.Y;

			//		//double X = (dX2 * street1.RightSideWalkPoint1.X - dX1 * street1.RightSideWalkPoint2.X) / (dX2 - dX1);

			//		//if ((X > street1.RightSideWalkPoint1.X) && (street1.RightSideWalkPoint1.X > street1.RightSideWalkPoint1.X))
			//		//{
			//		//}


			//		index++;
			//	}


			//}



			//foreach (StreetSegment street1 in streetList)
			//{
			//    foreach (StreetSegment street2 in streetList)
			//    {
			//        double dX1 = street1.RightSideWalkPoint1.X - street1.RightSideWalkPoint2.X;
			//        double dX2 = street2.RightSideWalkPoint1.X - street2.RightSideWalkPoint2.X;

			//        double X = (dX2 * street1.RightSideWalkPoint1.X - dX1 * street1.RightSideWalkPoint2.X) / ( dX2 - dX1 );

			//        if ((X > street1.RightSideWalkPoint1.X) && (street1.RightSideWalkPoint1.X > street1.RightSideWalkPoint1.X))
			//        {
			//        }

			//    }
			//}
		}




		


	}
}
