namespace DataLayer.Models
{
    public class ConfusionMatrixModel
    {
        public int TruePositive { get; set; }
        public int FalsePositive { get; set; }
        public int TrueNegative { get; set; }
        public int FalseNegative { get; set; }

        public double Accuracy { get; set; }
        public double Precision { get; set; }
        public double Recall { get; set; }
        public double Specificity { get; set; }
        public int Beta { get; set; }
        public double Fmeasure { get; set; }
    }
}
