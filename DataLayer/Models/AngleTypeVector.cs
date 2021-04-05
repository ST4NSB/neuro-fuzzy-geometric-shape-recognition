namespace DataLayer.Models
{
    public struct AngleTypeVector
    {
        // multiple of 45
        public object Acute; // 1 - 45
        public object AcuteRight; // 46 - 90
        public object ObtuseRight; // 91 - 135
        public object Obtuse; // 136 - 180

        public AngleTypeVector(int Acute = 0, 
                               int AcuteRight = 0, 
                               int ObtuseRight = 0, 
                               int Obtuse = 0)
        {
            this.Acute = Acute;
            this.AcuteRight = AcuteRight;
            this.ObtuseRight = ObtuseRight;
            this.Obtuse = Obtuse;
        }

        public AngleTypeVector(double Acute = 0.0d, 
                               double AcuteRight = 0.0d, 
                               double ObtuseRight = 0.0d, 
                               double Obtuse = 0.0d)
        {
            this.Acute = Acute;
            this.AcuteRight = AcuteRight;
            this.ObtuseRight = ObtuseRight;
            this.Obtuse = Obtuse;
        }
    }
}
