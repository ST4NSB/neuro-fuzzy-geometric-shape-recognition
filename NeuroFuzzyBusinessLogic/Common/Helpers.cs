using DataLayer.Models;
using System;

namespace NeuroFuzzyBusinessLogic.Common
{
    public static class Helpers
    {
        public static double GetEuclideanDistance(AngleTypeVector avg, AngleTypeVector vector)
        {
            return Math.Sqrt(Math.Pow((double)avg.Acute - (int)vector.Acute, 2) +
                   Math.Pow((double)avg.MediumAcute - (int)vector.MediumAcute, 2) +
                   Math.Pow((double)avg.Obtuse - (int)vector.Obtuse, 2) +
                   Math.Pow((double)avg.Right - (int)vector.Right, 2));
        }
    }
}
