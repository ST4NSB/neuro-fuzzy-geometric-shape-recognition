using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Models;

namespace NeuroFuzzyBusinessLogic
{
    public class NeuroFuzzyClassifier
    {
        private Point _gravityCenter;
        private List<Point> _pointsOfInterest;

        #region CONSTRUCTOR
        public NeuroFuzzyClassifier(List<Point> pointsOfInterest, Point gravityCenter)
        {
            _gravityCenter = gravityCenter;
            _pointsOfInterest = pointsOfInterest;
        }
        #endregion

        #region PUBLIC METHODS

        public List<Point> ComputeConvexHullGrahamScan()
        {
            var _hullPoints = new List<Point>();
            var startPoint = FindBottomMostPoint();
            var sortedPoints = _pointsOfInterest.OrderBy(p => Math.Atan2(p.y - startPoint.y, p.x - startPoint.x)).ToList(); // sort by angle with acording to startPoint

            _hullPoints.Add(sortedPoints[0]);
            _hullPoints.Add(sortedPoints[1]);

            for (int i = 2; i < sortedPoints.Count; i++)
            {
                var nextPoint = sortedPoints[i];
                var middlePoint = _hullPoints[_hullPoints.Count - 1];

                _hullPoints.RemoveAt(_hullPoints.Count - 1);
                while (_hullPoints[_hullPoints.Count - 1] != null && GetSignOfCrossProduct(_hullPoints[_hullPoints.Count - 1], middlePoint, nextPoint) <= 0)
                {
                    middlePoint = _hullPoints[_hullPoints.Count - 1];
                    _hullPoints.RemoveAt(_hullPoints.Count - 1);
                }

                _hullPoints.Add(middlePoint);
                _hullPoints.Add(sortedPoints[i]);
            }

            // the very last point pushed in could have been collinear, so we check for that
            var aux = _hullPoints[_hullPoints.Count - 1];
            _hullPoints.RemoveAt(_hullPoints.Count - 1);
            if (GetSignOfCrossProduct(_hullPoints[_hullPoints.Count - 1], aux, startPoint) > 0)
                _hullPoints.Add(aux);

            return _hullPoints;
        }

        public List<int> BuildTangentVectors(List<Point> pointsList)
        {
            float currentSlope, nextSlope;
            int tangentVectorDirectionIndex = -1;
            Point first, second, third;
            List<int> directionTangents = new List<int>();

            // go clockwise (check 3 points at a time)
            for (int i = pointsList.Count - 1; i >= 0; i--)   
            {
                first = pointsList[i];

                if (i == 1)
                {
                    second = pointsList[i - 1];
                    third = pointsList[pointsList.Count - 1];
                }
                else if (i == 0) 
                {
                    second = pointsList[pointsList.Count - 1];
                    third  = pointsList[pointsList.Count - 2];
                }
                else
                {
                    second = pointsList[i - 1];
                    third  = pointsList[i - 2];
                }

                currentSlope = CalculateSlope(first, second);
                nextSlope = CalculateSlope(second, third);


                if ((currentSlope >= 0 && nextSlope >= 0) || (currentSlope <= 0 && nextSlope <= 0))
                    tangentVectorDirectionIndex++;

                if ((currentSlope > 0  && nextSlope < 0)  || (currentSlope < 0 && nextSlope > 0)   ||
                    (currentSlope == 0 && nextSlope != 0) || (currentSlope != 0 && nextSlope == 0) || 
                    (currentSlope == 0 && nextSlope == 0)) 
                    directionTangents.Add(tangentVectorDirectionIndex);
            }
            return directionTangents;
        }

        public List<float> TangentVectorsToDegrees(List<int> tanVectors, int numOfInterestPoints)
        {
            float radianDegree = 0.0f;
            List<float> degreesList = new List<float>();

            for (int i = 0; i < tanVectors.Count; i++) 
            {
                if (i == tanVectors.Count - 1)
                    radianDegree = (float)(Math.Abs(tanVectors[i] - tanVectors[0]) * 2 * Math.PI) / numOfInterestPoints;
                else
                    radianDegree = (float)(Math.Abs(tanVectors[i] - tanVectors[i + 1]) * 2 * Math.PI) / numOfInterestPoints;

                degreesList.Add(ConvertRadiansToDegrees(radianDegree));
            }

            return degreesList;
        }

        public AngleTypeVector ConvertShapeAnglesToBSWinput(List<float> angles)
        {
            AngleTypeVector bswInput = new AngleTypeVector();

            foreach (float angle in angles)
            {
                if (angle >= 0   &&  angle <= 25)  bswInput.Acute++;
                if (angle >= 26  &&  angle <= 75)  bswInput.MediumAcute++;
                if (angle >= 76  &&  angle <= 115) bswInput.Right++;
                if (angle >= 116 &&  angle <= 180) bswInput.Obtuse++;
            }

            return bswInput;
        }

        #endregion

        #region PRIVATE METHODS
        
        private Point FindBottomMostPoint()
        {
            List<Point> aux = _pointsOfInterest.OrderBy(o => o.y).ToList();
            return aux[0];
        }

        private float ConvertRadiansToDegrees(float angle)
        {
            return (float)(180 / Math.PI) * angle;
        }

        private float CalculateSlope(Point first, Point second)
        {
            if (second.x == first.x)
                return 0;
            else
                return (float)(second.y - first.y) / (second.x - first.x);
        }

        private int GetSignOfCrossProduct(Point a, Point b, Point c)
        {
            float area = (b.x - a.x) * (c.y - a.y) - (b.y - a.y) * (c.x - a.x);
            if (area < 0) return -1; // clockwise
            if (area > 0) return  1; // counter-clockwise
            return 0;                // collinear
        }

        #endregion
    }
}
