using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class Point
    {
        public int x = 0;
        public int y = 0;

        public Point(int x = 0, int y = 0)
        {
            this.x = x;
            this.y = y;
        }
    }
}
