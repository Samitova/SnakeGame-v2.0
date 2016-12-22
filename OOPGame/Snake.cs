using System;
using NConsoleGraphics;
using System.Collections.Generic;
using System.Xml;

namespace OOPGame
{
    internal class Snake : IGameObject
    {
        private int _speed;
        private List<SnakeItem> _snakeItems = new List<SnakeItem>();

        public Snake(int speed)
        {
            _speed = speed;
            _snakeItems.Add(new SnakeItem(speed / 2, speed / 2, speed, 0));
        }

        public void SetSnakeItems(List<SnakeItem> items)
        {
            _snakeItems = items;
        }

        /// <summary>
        /// Save snake state 
        /// </summary>
        /// <param name="xmlDoc"></param>
        public void Save(XmlDocument xmlDoc)
        {
            XmlNode snakeNode = xmlDoc.CreateElement("snake");
            xmlDoc.LastChild.AppendChild(snakeNode);
            foreach (var item in _snakeItems)
            {
                item.Save(xmlDoc);
            }
        }

        /// <summary>
        /// Chack is snake crashed
        /// </summary>
        /// <param name="border">border</param>
        /// <returns>bool</returns>
        public bool IsCrashed(Border border)
        {
            if (IsMeetTail() || IsMeetBoard(border))
            {
                CreateNewSnake((border.Width - _speed) / 2, border.Height / 2);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Chack is snake meet border
        /// </summary>
        /// <param name="border">border</param>
        /// <returns>bool</returns>
        private bool IsMeetBoard(Border border)
        {
            if (_snakeItems[0].X < _speed / 2 || _snakeItems[0].X > border.Width - _speed * 1.5
                || _snakeItems[0].Y < _speed / 2 || _snakeItems[0].Y > border.Height - _speed * 1.5)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Chack is snake meet its tail
        /// </summary>       
        /// <returns>bool</returns>
        private bool IsMeetTail()
        {
            for (int i = 1; i < _snakeItems.Count; i++)
            {
                if (_snakeItems[0].X == _snakeItems[i].X && _snakeItems[0].Y == _snakeItems[i].Y)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Chack is snake meet apple
        /// </summary>
        /// <param name="x"> x of apple</param>
        /// <param name="y">y of apple</param>
        /// <returns>bool</returns>
        public bool IsMeetApple(int x, int y)
        {
            if (_snakeItems[0].X == x && _snakeItems[0].Y == y)
            {
                IncreaseSnake();
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Check is place taken by snake for apple creation 
        /// </summary>
        /// <param name="x"> x apple</param>
        /// <param name="y"> y apple</param>
        /// <returns>bool</returns>
        public bool IsPlaceTaken(int x, int y)
        {
            bool isTaken = false;
            _snakeItems.ForEach(item =>
            {
                if (item.X == x && item.Y == y)
                {
                    isTaken = true;
                }
            });
            return isTaken;
        }

        /// <summary>
        /// Change snake direction
        /// </summary>
        private void ChangeDirection()
        {
            int lastXSpeed = _snakeItems[0].XSpeed;
            int lastYSpeed = _snakeItems[0].YSpeed;

            for (int i = 0; i < _snakeItems.Count - 1; i++)
            {
                if (lastXSpeed != _snakeItems[i + 1].XSpeed || lastYSpeed != _snakeItems[i + 1].YSpeed)
                {
                    _snakeItems[i + 1].XSpeed = _snakeItems[i + 1].XSpeed + lastXSpeed;
                    lastXSpeed = _snakeItems[i + 1].XSpeed - lastXSpeed;
                    _snakeItems[i + 1].XSpeed = _snakeItems[i + 1].XSpeed - lastXSpeed;

                    _snakeItems[i + 1].YSpeed = _snakeItems[i + 1].YSpeed + lastYSpeed;
                    lastYSpeed = _snakeItems[i + 1].YSpeed - lastYSpeed;
                    _snakeItems[i + 1].YSpeed = _snakeItems[i + 1].YSpeed - lastYSpeed;
                }
            }
        }

        /// <summary>
        /// Increase snake
        /// </summary>
        private void IncreaseSnake()
        {
            int lastIndex = _snakeItems.Count - 1;
            int x = _snakeItems[lastIndex].X - _snakeItems[lastIndex].XSpeed;
            int y = _snakeItems[lastIndex].Y - _snakeItems[lastIndex].YSpeed;
            _snakeItems.Add(new SnakeItem(x, y, _snakeItems[lastIndex].XSpeed, _snakeItems[lastIndex].YSpeed));
        }

        /// <summary>
        /// Ceate new snake afte crashing
        /// </summary>
        /// <param name="x">x of snake</param>
        /// <param name="y">y of snake</param>
        private void CreateNewSnake(int x, int y)
        {
            _snakeItems.Clear();
            _snakeItems.Add(new SnakeItem(x, y, _speed, 0));
        }

        public void Render(ConsoleGraphics graphics)
        {
            _snakeItems.ForEach(item => item.Render(graphics));
        }

        /// <summary>
        /// Return count of snake items
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            return _snakeItems.Count;
        }

        /// <summary>
        /// return snake move dirrection along x axis
        /// </summary>
        /// <returns></returns>
        public int GetXSpeed()
        {
            return _snakeItems[0].XSpeed;
        }

        /// <summary>
        /// return snake move dirrection along y axis
        /// </summary>
        /// <returns></returns>
        public int GetYSpeed()
        {
            return _snakeItems[0].YSpeed;
        }

        public void TurnToLeft()
        {
            _snakeItems[0].XSpeed = Math.Abs(_snakeItems[0].YSpeed) * -1;
            _snakeItems[0].YSpeed = 0;
        }

        public void TurnToRight()
        {
            _snakeItems[0].XSpeed = Math.Abs(_snakeItems[0].YSpeed);
            _snakeItems[0].YSpeed = 0;
        }

        public void TurnUp()
        {
            _snakeItems[0].YSpeed = Math.Abs(_snakeItems[0].XSpeed) * -1;
            _snakeItems[0].XSpeed = 0;
        }

        public void TurnDown()
        {
            _snakeItems[0].YSpeed = Math.Abs(_snakeItems[0].XSpeed);
            _snakeItems[0].XSpeed = 0;
        }

        public void Update(GameEngine engine)
        {
            _snakeItems.ForEach(item =>
            {
                item.Update(engine);
            });

            ChangeDirection();
        }

        public void Load(XmlNode xmlNode)
        { }
    }
}
