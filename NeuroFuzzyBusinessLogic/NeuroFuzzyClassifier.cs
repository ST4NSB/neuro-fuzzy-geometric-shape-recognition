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
       
        #region PUBLIC METHODS

        public NeuroFuzzyClassifier(List<Point> pointsOfInterest, Point gravityCenter)
        {
            _gravityCenter = gravityCenter;
            _pointsOfInterest = pointsOfInterest;
        }

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

        #endregion
        
        #region PRIVATE METHODS

        private Point FindBottomMostPoint()
        {
            List<Point> aux = _pointsOfInterest.OrderBy(o => o.y).ToList();
            return aux[0];
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
