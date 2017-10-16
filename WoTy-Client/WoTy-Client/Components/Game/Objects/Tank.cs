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
    class Tank
    {
        private GObject box;
        private Image tower;
        private Image body;

        public Tank()
        {
            box = new GObject();
            body = new Image();
            tower = new Image();
        }

        public void Create()
        {
            box.Create(50, 92);

            body.Source = new BitmapImage(new Uri("textures/tank1.png", UriKind.Relative));
            body.Height = 50;
            body.Width = 92;
            box.GetObject().Children.Add(body);

            tower.Source = new BitmapImage(new Uri("textures/tank2.png", UriKind.Relative));
            tower.Height = 36.0;
            tower.Width = 67.2;
            tower.Margin = new Thickness(0, 0, -55, 0);
            tower.RenderTransformOrigin = new System.Windows.Point(0.35, 0.5);
            box.GetObject().Children.Add(tower);
        }

        public GObject GetObject()
        {
            return box;
        }


        public float driving_speed = 0.00F;
        public float position_x = 0.00F;
        public float position_y = 0.00F;
        public float rotation_degree = 0.00F;

        public float tower_rotation_degree = 0.00F;

        public void Move()
        {
            box.SetPosition(position_x, position_y);
            box.SetAngle(rotation_degree);

            tower.RenderTransform = new RotateTransform(tower_rotation_degree);
        }
    }
}
