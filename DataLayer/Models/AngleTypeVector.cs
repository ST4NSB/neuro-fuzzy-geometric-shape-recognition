namespace DataLayer.Models
{
    public struct AngleTypeVector
    {
        // [0, x] - range of values
        public int Acute; // [0 - 25]
        public int MediumAcute; // [26 - 75]
        public int Right; // [75 - 115]
        public int Obtuse; // [116 - 180)

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
    }
}
