using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
//using Editor.StreetsEditor.Classes;

namespace Editor.StreetsEditor
{
	public class Map
	{

		protected ObservableCollection<Cycle> cycleList = new ObservableCollection<Cycle>();

		protected ObservableCollection<Street> streetList = new ObservableCollection<Street>();
		public ObservableCollection<Street> StreetList
		{
			get { return streetList; }
		}

		public Street GetStreetByPoints(Point Point1, Point Point2)
		{
			foreach (Street street in StreetList)
			{
				if ((street.Point1 == Point1) && (street.Point2 == Point2))
				{
					return street;
				}
				else if ((street.Point1 == Point2) && (street.Point2 == Point1))
				{
					return street;
				}
			}
			return null;
		}

		public List<Street> GetStreetsByPoint(Point Point)
		{
			List<Street> list = new List<Street>();

			foreach (Street street in StreetList)
			{
				if ((street.Point1 == Point) || (street.Point2 == Point))
				{
					list.Add(street);
				}
			}
			return list;
		}


		public void BuildSideWalks()
		{
			byte c = 0;

			foreach (Street street in streetList)
			{
				c += 50;


				Street actualStreet = street;

				if (actualStreet.RightCycle != null)
				{
					continue;
				}

				Point actualPoint = actualStreet.Point1;
				Point nextPoint = actualStreet.Point2;

				Point firstPoint = actualStreet.Point1;


				Cycle rightCycle = new Cycle();
				cycleList.Add(rightCycle);

				rightCycle.Color = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(c, c, 40));

				rightCycle.IsSelected = true;

				actualStreet.RightCycle = rightCycle;

				actualStreet.IsSelected = true;

				do
				{
					Street nextStreet = null;
					double angle = 1000;
					foreach (Street tmpStreet in GetStreetsByPoint(nextPoint))
					{


						if (tmpStreet != actualStreet)
						{
							Vector v1 = new Vector(actualPoint.X - nextPoint.X, actualPoint.Y - nextPoint.Y);
							Vector v2 = new Vector(tmpStreet.Point2.X - tmpStreet.Point1.X, tmpStreet.Point2.Y - tmpStreet.Point1.Y);

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
								nextStreet = tmpStreet;
							}
						}
					}

					actualStreet = nextStreet;

					if (nextPoint == nextStreet.Point1)
					{
						actualStreet.RightCycle = rightCycle;
					}
					else
					{
						actualStreet.LeftCycle = rightCycle;
					}


					actualPoint = (nextPoint == nextStreet.Point1) ? nextStreet.Point1 : nextStreet.Point2;
					nextPoint = (nextPoint == nextStreet.Point1) ? nextStreet.Point2 : nextStreet.Point1;
				} while (nextPoint != firstPoint);




			}



			// Now we are going to join sidewalks

			//foreach (Cycle cycle in cycleList)
			//{
			//	int index = 0;
			//	Street actualStreet = cycle.StreetList.ElementAt(index);

			//	while (index < cycle.StreetList.Count)
			//	{
			//		Street nextStreet = cycle.StreetList.ElementAt((index + 1) % cycle.StreetList.Count);

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



			//foreach (Street street1 in streetList)
			//{
			//    foreach (Street street2 in streetList)
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
