using DataLayer.Models;
using System.Collections.Generic;

namespace NeuroFuzzyBusinessLogic.Common
{
    public static class Extensions
    {
        public static void AddDistance(this Dictionary<int, List<InputVectorModel>> dictionary, 
                                       int distance,
                                       InputVectorModel value)
        {
            if (dictionary.ContainsKey(distance))
            {
                dictionary[distance].Add(value);
            }
            else
            {
                dictionary.Add(distance, new List<InputVectorModel>() { value });
            }
        }
    }
}
