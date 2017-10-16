using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoTy_Server
{
    class World
    {
        private List<Tank> Tanks;
        // TODO

        public World()
        {
            Tanks = new List<Tank>();
        }

        public void AddTank(Tank t)
        {
            Tanks.Add(t);
        }


        public void Move(Tank body)
        {
            for (int j = 0; j < Tanks.Count; j++)
            {
                if (body != Tanks[j])
                {
                    Point MTV;
                    float MTVLength;

                    
                    if (VerifyCollision(body.GetObject(), Tanks[j].GetObject(), out MTV, out MTVLength) && !float.IsNaN(MTVLength))
                    {
                        MTVLength -= 0.001f;
                        body.position_x += MTV.X * -MTVLength;
                        body.position_y += MTV.Y * -MTVLength;
                        body.Move(0.2F);
                    } else
                    {
                        body.Move();
                    }
                }
            }
        }

        public  static bool VerifyCollision(GObject body1, GObject body2, out Point MVP, out float MVPLength)
        {
            MVPLength = 0.0f;
            MVP = new Point(0, 0);

            Point[] GeneralNormals = new Point[8];

            Point[] BodyINormals = body1.GetNormals();
            Point[] BodyJNormals = body2.GetNormals();

            Point[] BodyIGlobalVertex = body1.GetGlobalVertex();
            Point[] BodyJGlobalVertex = body2.GetGlobalVertex();

            for (int k = 0; k < 4; k++)
            {
                GeneralNormals[k] = BodyINormals[k];
                GeneralNormals[k + 4] = BodyJNormals[k];
            }

            bool IsFirst = true;

            for (int k = 0; k < 8; k++)
            {
                Point BodyIProjection = GetProjection(BodyIGlobalVertex, GeneralNormals[k]);
                Point BodyJProjection = GetProjection(BodyJGlobalVertex, GeneralNormals[k]);

                if (BodyIProjection.X < BodyJProjection.Y || BodyJProjection.X < BodyIProjection.Y)
                {
                    return false;
                }

                if (IsFirst)
                {
                    MVP = GeneralNormals[k];
                    MVPLength = (BodyJProjection.Y - BodyIProjection.X > 0) ? BodyJProjection.Y - BodyIProjection.X :
                                                                              BodyIProjection.Y - BodyJProjection.X;
                    IsFirst = false;
                }
                else
                {
                    float TempMVPLength = (BodyJProjection.Y - BodyIProjection.X > 0) ? BodyJProjection.Y - BodyIProjection.X :
                                                                                        BodyIProjection.Y - BodyJProjection.X;

                    if (Math.Abs(TempMVPLength) < Math.Abs(MVPLength))
                    {
                        MVP = GeneralNormals[k];
                        MVPLength = TempMVPLength;
                    }
                }
            }

            return true;
        }

        private static Point GetProjection(Point[] GlobalVertex, Point Normal)
        {
            Point result = new Point();

            result.X = GlobalVertex[0].X * Normal.X + GlobalVertex[0].Y * Normal.Y;
            result.Y = result.X;

            for (int i = 1; i < 4; i++)
            {
                float Temp = GlobalVertex[i].X * Normal.X + GlobalVertex[i].Y * Normal.Y;

                if (Temp > result.X)
                    result.X = Temp;

                if (Temp < result.Y)
                    result.Y = Temp;
            }

            return result;
        }
    }
}
