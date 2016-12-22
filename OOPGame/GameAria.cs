using System;
using System.Collections.Generic;
using System.Xml;
using NConsoleGraphics;

namespace OOPGame
{
    class GameAria : Aria, IGameObject
    {
        private int _objectsSize;
        private int _gameSpeed = 5;
        private int _counter = 0;
        private Border _border;
        private Snake _snake;
        private Apple _apple;
        private Random _rand = new Random();
        private bool _isSpeedIncreased = false;

        public event Action SnakeEatApple;
        public event Action SnakeCrashed;

        public GameAria(int x, int y, int width, int height, int size): base (x, y, width, height)
        {
            _objectsSize = size;
            _border = new Border(width, height, size);
            _snake = new Snake(size);
            CreateApple();
        }

        public void Load(XmlNode xmlNode)
        {
            SetGameSpeed(xmlNode);
            SetApple(xmlNode);
            SetSnake(xmlNode);
        }

        /// <summary>
        /// Set game speed from xml file
        /// </summary>
        /// <param name="xmlNode"></param>
        public void SetGameSpeed(XmlNode xmlNode)
        {
            XmlNode speedNode = xmlNode.SelectSingleNode("//gamespeed");
            _gameSpeed = int.Parse(speedNode.Attributes["speed"].Value);
            _counter = int.Parse(speedNode.Attributes["counter"].Value);
        }

        /// <summary>
        /// Set spple state from xml file
        /// </summary>
        /// <param name="xmlNode"></param>
        public void SetApple(XmlNode xmlNode)
        {
            XmlNode speedNode = xmlNode.SelectSingleNode("//apple");
            int x = int.Parse(speedNode.Attributes["x"].Value);
            int y = int.Parse(speedNode.Attributes["y"].Value);
            _apple.SetApplePosition(x, y);
        }

        /// <summary>
        /// Set snake state from xml file
        /// </summary>
        /// <param name="xmlNode"></param>
        public void SetSnake(XmlNode xmlNode)
        {
            List<SnakeItem> snakeItems = new List<SnakeItem>();
            int x, y, xSpeed, ySpeed;
            XmlNodeList snakeNodes = xmlNode.SelectNodes("//snake/snakeItem");
            foreach (XmlNode item in snakeNodes)
            {
                x = int.Parse(item.Attributes["x"].Value);
                y = int.Parse(item.Attributes["y"].Value);
                xSpeed = int.Parse(item.Attributes["xSpeed"].Value);
                ySpeed = int.Parse(item.Attributes["ySpeed"].Value);
                snakeItems.Add(new SnakeItem(x, y, xSpeed, ySpeed)); 
            }
            _snake.SetSnakeItems(snakeItems);
        }

        /// <summary>
        /// Create random apple
        /// </summary>
        private void CreateApple()
        {
            int x, y;
            do
            {
                x = (_rand.Next(1, Width / _objectsSize) * _objectsSize) - _objectsSize / 2;
                y = (_rand.Next(1, Height / _objectsSize) * _objectsSize) - _objectsSize / 2;

            }
            while (_snake.IsPlaceTaken(x, y));
            _apple = new Apple(x, y, _objectsSize);
        }

        /// <summary>
        /// Save game
        /// </summary>
        /// <param name="xmlDoc"></param>
        public void Save(XmlDocument xmlDoc)
        {
            SaveGameSpeed(xmlDoc);
            _apple.Save(xmlDoc);
            _snake.Save(xmlDoc);
       }

        private void SaveGameSpeed(XmlDocument xmlDoc)
        {
            XmlNode gameSpeedNode = xmlDoc.CreateElement("gamespeed");
            XmlAttribute attribute = xmlDoc.CreateAttribute("speed");
            attribute.Value = _gameSpeed.ToString();
            gameSpeedNode.Attributes.Append(attribute);
            attribute = xmlDoc.CreateAttribute("counter");
            attribute.Value = _counter.ToString();
            gameSpeedNode.Attributes.Append(attribute);
            xmlDoc.LastChild.AppendChild(gameSpeedNode);
        }

        /// <summary>
        /// Check if snake eat apple
        /// </summary>
        private void CheckIntersection()
        {
            if (_snake.IsMeetApple(_apple.X, _apple.Y))
            {
                if (SnakeEatApple != null)
                {
                    SnakeEatApple();
                }
                _isSpeedIncreased = false;
                CreateApple();
            }
        }

        /// <summary>
        /// Check if snake crashed
        /// </summary>
        private void CheckCrash()
        {
            if (_snake.IsCrashed(_border) && SnakeCrashed != null)
            {
                SnakeCrashed();                          
            }
        }

        /// <summary>
        /// Increase game speed
        /// </summary>
        private void IncreaseGameSpeed()
        {
            if (_snake.Count() % 5 == 0 && _gameSpeed>1 && !_isSpeedIncreased)
            {
                _gameSpeed--;
                _isSpeedIncreased = true;
            }
        }

        public void Render(ConsoleGraphics graphics)
        {            
            _border.Render(graphics);
            _snake.Render(graphics);
            _apple.Render(graphics);
        }

        public void Update(GameEngine engine)
        {
            if (_counter++ % _gameSpeed == 0)
            {
                _snake.Update(engine);
            }

            if (Input.IsKeyDown(Keys.LEFT) && _snake.GetXSpeed() == 0)
            {
                _snake.TurnToLeft();
            }

            else if (Input.IsKeyDown(Keys.RIGHT) && _snake.GetXSpeed() == 0)
            {
                _snake.TurnToRight();
            }

            if (Input.IsKeyDown(Keys.UP) && _snake.GetYSpeed() == 0)
            {
                _snake.TurnUp();
            }

            if (Input.IsKeyDown(Keys.DOWN) && _snake.GetYSpeed() == 0)
            {
                _snake.TurnDown();
            }            
            CheckCrash();
            CheckIntersection();
            IncreaseGameSpeed();
        }

    }
}
