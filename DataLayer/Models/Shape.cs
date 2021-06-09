using DataLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Globalization;

namespace DataLayer.Models
{
    public class Shape
    {

        #region Private members

        private GeometricalShapeType _shapeType;
        private System.Drawing.Point _gravityCenter;
        private List<System.Drawing.Point> _listOfHolePoints;
        private List<System.Drawing.Point> _listOfInterestPoints;
        private List<Segment> __listOfSegments;

        #endregion

        #region Properties

        public GeometricalShapeType ShapeType
        {
            get { return _shapeType; }
            set { _shapeType = value; }
        }

        public System.Drawing.Point GravityCenter
        {
            get { return _gravityCenter; }
            private set => _gravityCenter = value;
        }


        public List<System.Drawing.Point> ListOfHolePoints
        {
            get => _listOfHolePoints;
            set { _listOfHolePoints = value; }
        }

        public List<System.Drawing.Point> ListOfInterestPoints
        {
            get { return _listOfInterestPoints; }
            private set => _listOfInterestPoints = value;
        }
        public List<Segment> ListOfSegments
        {
            get { return __listOfSegments; }
            private set => __listOfSegments = value;
        }


        #endregion

        #region Constructors

        public Shape(GeometricalShapeType shapeType)
        {
            _shapeType = shapeType;
            _listOfHolePoints = new List<System.Drawing.Point>();
            _listOfInterestPoints = new List<System.Drawing.Point>();
            _gravityCenter = new System.Drawing.Point();
            __listOfSegments = new List<Segment>();
        }

        #endregion

        #region Public Methods

        public void TakePointsFromShape(Bitmap bitmap)
        {
            Color white = Color.FromArgb(0, 0, 0, 0);

            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    Color pixelColor = bitmap.GetPixel(i, j);
                    if (pixelColor != white)
                    {
                        this._listOfHolePoints.Add(new System.Drawing.Point(i, j));
                    }
                }
            }
        }

        public bool IsCorrectShape()
        {
            return true;
        }

        public void CalculateGravityCenter()
        {
            int sumX = 0;
            int sumY = 0;
            int totalPoints = ListOfHolePoints.Count;

            foreach (var point in ListOfHolePoints)
            {
                sumX += point.X;
                sumY += point.Y;
            }

            _gravityCenter = new System.Drawing.Point(Convert.ToInt32(sumX / totalPoints), Convert.ToInt32(sumY / totalPoints));

        }

        public Bitmap GenerateShapeWithSignificant()
        {
            Bitmap image = new Bitmap(300, 300);

            for (int i = 0; i < 300; i++)
            {

                for (int j = 0; j < 300; j++)
                {

                    if (ListOfHolePoints.Contains(new System.Drawing.Point(i, j)))
                    {
                        image.SetPixel(i, j, Color.Black);
                    }
                    else
                    {
                        image.SetPixel(i, j, Color.White);
                    }

                }
            }

            return image;
        }

        #endregion

    }

    public class Segment
    {
        public List<System.Drawing.Point> ListOfPoints { get; set; }

        public Segment()
        {
            ListOfPoints = new List<System.Drawing.Point>();
        }

    }
}
