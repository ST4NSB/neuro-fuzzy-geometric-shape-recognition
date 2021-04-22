using DataLayer.Enums;
using System.Collections.Generic;

namespace DataLayer.Models
{
    public class HiddenNodeModel
    {
        public double ActivationThreshold { get; set; }
        public Dictionary<int, int> WeightsIndexLayer { get; set; }
        public GeometricalShapeType OutputNodeLabel { get; set; }

        public HiddenNodeModel()
        {
            WeightsIndexLayer = new Dictionary<int, int>();
        }
    }
}
