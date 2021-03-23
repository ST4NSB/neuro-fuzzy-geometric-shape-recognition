using DataLayer.Enums;
using System.Collections.Generic;

namespace DataLayer
{
    public class PredictionHistoryModel
    {
        public List<GeometricalShape> ActualValues { get; set; }
        public List<GeometricalShape> PredictedValues { get; set; }

        public PredictionHistoryModel()
        {
            ActualValues = new List<GeometricalShape>();
            PredictedValues = new List<GeometricalShape>();
        }
    }
}
