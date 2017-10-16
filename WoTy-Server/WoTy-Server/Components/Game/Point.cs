using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoTy_Server
{
    class Point
    {
        public float X { get; set; }
        public float Y { get; set; }

        public Point()
        {
            X = 0.0f;
            Y = 0.0f;
        }

        public Point(float X_init, float Y_init)
        {
            X = X_init;
            Y = Y_init;
        }

        public void SetPoint(float X, float Y)
        {
            this.X = X;
            this.Y = Y;
        }
    }
}
