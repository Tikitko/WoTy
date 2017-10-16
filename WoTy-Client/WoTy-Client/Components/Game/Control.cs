using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WoTy_Client
{
    class Control
    {
        public bool _action = false;
        public bool _turnLeft = false;
        public bool _turnRight = false;
        public bool _moveUp = false;
        public bool _moveDown = false;
        public bool _moveLeft = false;
        public bool _moveRight = false;

        public void KeyDown(KeyEventArgs e)
        {
            if (e.IsRepeat)
            {
                return;
            }
            switch (e.Key)
            {
                case Key.C:
                    _action = true;
                    break;
                case Key.Z:
                    _turnLeft = true;
                    break;
                case Key.X:
                    _turnRight = true;
                    break;
                case Key.W:
                    _moveUp = true;
                    break;
                case Key.S:
                    _moveDown = true;
                    break;
                case Key.A:
                    _moveLeft = true;
                    break;
                case Key.D:
                    _moveRight = true;
                    break;
            }
        }

        public void KeyUp(KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.C:
                    _action = false;
                    break;
                case Key.Z:
                    _turnLeft = false;
                    break;
                case Key.X:
                    _turnRight = false;
                    break;
                case Key.W:
                    _moveUp = false;
                    break;
                case Key.S:
                    _moveDown = false;
                    break;
                case Key.A:
                    _moveLeft = false;
                    break;
                case Key.D:
                    _moveRight = false;
                    break;
            }
        }

        public void KeyDown_2(KeyEventArgs e)
        {
            if (e.IsRepeat)
            {
                return;
            }
            switch (e.Key)
            {
                case Key.NumPad3:
                    _action = true;
                    break;
                case Key.NumPad1:
                    _turnLeft = true;
                    break;
                case Key.NumPad2:
                    _turnRight = true;
                    break;
                case Key.Up:
                    _moveUp = true;
                    break;
                case Key.Down:
                    _moveDown = true;
                    break;
                case Key.Left:
                    _moveLeft = true;
                    break;
                case Key.Right:
                    _moveRight = true;
                    break;
            }
        }

        public void KeyUp_2(KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.NumPad3:
                    _action = false;
                    break;
                case Key.NumPad1:
                    _turnLeft = false;
                    break;
                case Key.NumPad2:
                    _turnRight = false;
                    break;
                case Key.Up:
                    _moveUp = false;
                    break;
                case Key.Down:
                    _moveDown = false;
                    break;
                case Key.Left:
                    _moveLeft = false;
                    break;
                case Key.Right:
                    _moveRight = false;
                    break;
            }
        }
    }
}
