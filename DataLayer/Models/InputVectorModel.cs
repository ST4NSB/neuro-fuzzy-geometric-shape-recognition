using DataLayer.Enums;

namespace DataLayer.Models
{
    public class InputVectorModel
    {
        public int[] InputNodes { get; set; }
        public GeometricalShapeType Label { get; set; }
    }
}
