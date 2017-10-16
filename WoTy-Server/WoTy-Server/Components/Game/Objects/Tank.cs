using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WoTy_Server
{
    class Tank
    {
        private Control Control;

        private GObject box;

        public Tank()
        {
            box = new GObject();
        }

        public void Create(Control c)
        {
            Control = c;

            box.Create(50, 92);
        }

        public GObject GetObject()
        {
            return box;
        }

        const float tower_rotation_speed = 2.00F;
        public float tower_rotation_degree = 0.00F;

        public void TowerRotate()
        {
            if (Control._turnRight ^ Control._turnLeft)
            {
                tower_rotation_degree += Control._turnRight ? tower_rotation_speed : tower_rotation_speed * -1;
            }
        }


        const float engine_power = 0.08F;
        const float resistance_power = 0.05F;
        const float brake_power = 0.06F;
        const float place_rotation_speed = 0.80F;
        const float driving_rotation_speed = 0.20F;
        const float on_rotation_forward_speed = 2.00F;
        const float forward_speed = 2.50F;
        const float on_rotation_back_speed = 1.00F;
        const float back_speed = 1.20F;
        const float null_speed = 0.00F;
        public float driving_speed = 0.00F;
        public float position_x = 0.00F;
        public float position_y = 0.00F;
        public float rotation_degree = 0.00F;


        /*private void Move()
        {
            float temp_speed, temp_x, temp_y;
            if (Control._moveUp ^ Control._moveDown)
            {
                temp_speed = Control._moveUp ? forward_speed : back_speed;
                temp_x = (float)Math.Cos(rotation_degree * Math.PI / 180) * temp_speed;
                temp_y = (float)Math.Sin(rotation_degree * Math.PI / 180) * temp_speed;
                position_x = Control._moveUp ? position_x + temp_x : position_x - temp_x;
                position_y = Control._moveUp ? position_y  + temp_y : position_y - temp_y;
            }
            if (Control._moveRight ^ Control._moveLeft)
            {
                temp_speed = Control._moveUp || Control._moveDown ? driving_rotation_speed : place_rotation_speed;
                rotation_degree = Control._moveRight ? rotation_degree + temp_speed : rotation_degree - temp_speed;
                if (rotation_degree >= 360 || rotation_degree <= -360) rotation_degree = 0;
            }
        }*/



        public void Move(float external_resistance = 0.0f)
        {
            float temp_speed = null_speed;
            if (Control._moveUp)
            {
                temp_speed = Control._moveRight || Control._moveLeft ? on_rotation_forward_speed : forward_speed;
                if (temp_speed > driving_speed)
                {
                    driving_speed += engine_power;
                }
            }
            else if (Control._moveDown)
            {
                temp_speed = -1 * (Control._moveRight || Control._moveLeft ? on_rotation_back_speed : back_speed);
                if (temp_speed < driving_speed)
                {
                    if (driving_speed >= null_speed)
                    {
                        driving_speed -= brake_power;
                    }
                    else
                    {
                        driving_speed -= engine_power;
                    }
                }
            }
            if (driving_speed <= resistance_power && driving_speed >= resistance_power * -1)
            {
                driving_speed = null_speed;
            }
            else
            {
                if (driving_speed > resistance_power)
                {
                    driving_speed -= resistance_power;
                }
                if (driving_speed < resistance_power * -1)
                {
                    driving_speed += resistance_power;
                }
            }
            if(external_resistance != 0.0f)
            {
                driving_speed = driving_speed >= forward_speed * external_resistance ? driving_speed * external_resistance : driving_speed;
            }
            driving_speed = (float)Math.Round(driving_speed, 2);
            position_x += (float)Math.Cos(rotation_degree * Math.PI / 180) * driving_speed;
            position_y += (float)Math.Sin(rotation_degree * Math.PI / 180) * driving_speed;
            box.SetPosition(position_x, position_y);

            float temp_speed_r = null_speed;
            if (Control._moveRight ^ Control._moveLeft)
            {
                temp_speed_r = (Math.Abs(driving_speed) / forward_speed) * (place_rotation_speed - driving_rotation_speed);
                temp_speed_r = driving_speed == null_speed 
                    ? (place_rotation_speed) * (external_resistance != 0.0f ? external_resistance * 0.4f : 1)
                    : (place_rotation_speed - temp_speed_r) * (external_resistance != 0.0f ? external_resistance * 0.6f : 1);
                temp_speed_r = (float)Math.Round(temp_speed_r, 2);
                rotation_degree = Control._moveRight ? rotation_degree + temp_speed_r : rotation_degree - temp_speed_r;
                if (rotation_degree >= 360 || rotation_degree <= -360)
                {
                    rotation_degree = 0;
                }
            }
            box.SetAngle(rotation_degree);

            TowerRotate();
        }
    }
}
