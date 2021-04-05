using DataLayer.Enums;
using DataLayer.Models;
using System;
using System.Collections.Generic;

namespace NeuroFuzzyBusinessLogic.Common
{
    public static class Extensions
    {
        public static void AddDistance(this Dictionary<double, List<Tuple<AngleTypeVector, GeometricalShapeType>>> dictionary, 
                                       double distance,
                                       Tuple<AngleTypeVector, GeometricalShapeType> value)
        {
            if (dictionary.ContainsKey(distance))
            {
                dictionary[distance].Add(value);
            }
            else
            {
                dictionary.Add(distance, new List<Tuple<AngleTypeVector, GeometricalShapeType>>() { value });
            }
        }
    }
}
