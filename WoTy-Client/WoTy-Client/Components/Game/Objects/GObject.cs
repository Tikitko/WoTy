using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WoTy_Client
{
    class GObject
    {
        private Grid obj;

        public void Create(float h, float w)
        {
            obj = new Grid();
            obj.RenderTransformOrigin = new System.Windows.Point(0.5, 0.5);
            obj.Height = h;
            obj.Width = w;
        }

        public Grid GetObject()
        {
            return obj;
        }

        public void SetPosition(float x, float y)
        {
            obj.Margin = new Thickness(-800 + x, -800 + y, -800, -800);
        }

        public void SetAngle(float a)
        {
            obj.RenderTransform = new RotateTransform(a);
        }
    }
}