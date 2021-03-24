using DataLayer.Enums;
using System.Collections.Generic;

namespace DataLayer
{
    public class PredictionHistoryModel
    {
        public List<GeometricalShapeType> ActualValues { get; set; }
        public List<GeometricalShapeType> PredictedValues { get; set; }

        public PredictionHistoryModel()
        {
            ActualValues = new List<GeometricalShapeType>();
            PredictedValues = new List<GeometricalShapeType>();
        }
    }
}
