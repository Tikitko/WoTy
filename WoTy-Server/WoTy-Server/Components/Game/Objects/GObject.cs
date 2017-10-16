using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WoTy_Server
{
    class GObject
    {
        private Point[] Vertex;

        private Point Position;
        private float Angle;

        private Point[] GlobalVertex; // private
        private Point[] Normals;

        public GObject()
        {
            Vertex = new Point[4] { new Point(20, 20), new Point(20, -20), new Point(-20, -20), new Point(-20, 20) };
            Position = new Point(0, 0);
            Angle = 0.0f;

            GlobalVertex = new Point[4] { new Point(), new Point(), new Point(), new Point() };
            Normals = new Point[4] { new Point(), new Point(), new Point(), new Point() };
        }

        public void Create(float h, float w)
        {

            Vertex[0].SetPoint(w / 2, h / 2);
            Vertex[1].SetPoint(w / 2, -h / 2);
            Vertex[2].SetPoint(-w / 2, -h / 2);
            Vertex[3].SetPoint(-w / 2, h / 2);
        }

        public Point GetPosition()
        {
            return Position;
        }

        public float GetAngle()
        {
            return Angle;
        }

        public void SetPosition(float x, float y)
        {
            Position = new Point(x,y);
        }

        public void SetAngle(float a)
        {
            Angle = a;
        }

        public Point[] GetGlobalVertex()
        {
            SetGlobalVertex();

            return GlobalVertex;
        }

        public Point[] GetNormals()
        {
            SetNormals();

            return Normals;
        }

        private void SetGlobalVertex()
        {
            float sin, cos;
            sin = (float)Math.Sin(Angle * Math.PI / 180);
            cos = (float)Math.Cos(Angle * Math.PI / 180);
            for (int i = 0; i < 4; i++)
            {
                GlobalVertex[i].X = (Vertex[i].X * cos - Vertex[i].Y * sin) + Position.X/2;
                GlobalVertex[i].Y = (Vertex[i].Y * cos + Vertex[i].X * sin) + Position.Y/2;
            }
        }

        private void SetNormals()
        {
            for (int i = 0; i < 4; i++)
            {
                Point Vertex1 = GlobalVertex[i];
                Point Vertex2 = GlobalVertex[(i + 1 == Vertex.Length) ? 0 : i + 1];

                Point Edge = new Point(Vertex2.X - Vertex1.X, Vertex2.Y - Vertex1.Y);

                float Length = (float)Math.Sqrt(Edge.X * Edge.X + Edge.Y * Edge.Y);

                Normals[i].X = -Edge.Y / Length;
                Normals[i].Y = Edge.X / Length;
            }
        }
    }
}