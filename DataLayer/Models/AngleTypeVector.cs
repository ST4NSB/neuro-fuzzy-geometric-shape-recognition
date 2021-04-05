namespace DataLayer.Models
{
    public struct AngleTypeVector
    {
        // multiple of 45
        public object Acute; // [0 - 25]
        public object MediumAcute; // [26 - 75]
        public object Right; // [75 - 115]
        public object Obtuse; // [116 - 180)

        public AngleTypeVector(int Acute = 0, 
                               int MediumAcute = 0, 
                               int Right = 0, 
                               int Obtuse = 0)
        {
            this.Acute = Acute;
            this.MediumAcute = MediumAcute;
            this.Right = Right;
            this.Obtuse = Obtuse;
        }

        public AngleTypeVector(double Acute = 0.0d, 
                               double MediumAcute = 0.0d, 
                               double Right = 0.0d, 
                               double Obtuse = 0.0d)
        {
            this.Acute = Acute;
            this.MediumAcute = MediumAcute;
            this.Right = Right;
            this.Obtuse = Obtuse;
        }
    }
}
