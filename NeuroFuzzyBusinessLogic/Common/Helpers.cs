using DataLayer.Models;
using System;

namespace NeuroFuzzyBusinessLogic.Common
{
    public static class Helpers
    {
        public static double GetEuclideanDistance(AngleTypeVector avg, AngleTypeVector vector)
        {
            return Math.Sqrt(Math.Pow((double)avg.Acute - (int)vector.Acute, 2) +
                   Math.Pow((double)avg.AcuteRight - (int)vector.AcuteRight, 2) +
                   Math.Pow((double)avg.Obtuse - (int)vector.Obtuse, 2) +
                   Math.Pow((double)avg.ObtuseRight - (int)vector.ObtuseRight, 2));
        }
    }
}
