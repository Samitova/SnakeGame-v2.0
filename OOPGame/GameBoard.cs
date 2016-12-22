using System;
using NConsoleGraphics;
using System.Xml;

namespace OOPGame
{    
    public class GameBoard : IGameObject
    {
        private int _width;
        private int _height;
        private GameAria _gameAria;
        private StatusAria _status;      

        public event Action GameOver;

        public GameBoard(int width, int height, int size)
        {
            _width = width;
            _height = height;
         
            _gameAria = new GameAria(0, 0, width, _height - 20, size);

            _gameAria.SnakeEatApple += SnakeEatApple;
            _gameAria.SnakeCrashed += SnakeCkreshed;

            _status = new StatusAria(0, _height - 30, width, 20, size);
        }

        public void SetStatusAria(int score, int lives)
        {
            _status.Score = score;
            _status.Lives = lives;
        }

        /// <summary>
        /// Load board
        /// </summary>
        /// <param name="xmlNode"></param>
        public void Load(XmlNode xmlNode)
        {
            XmlNode statusNode = xmlNode.SelectSingleNode("//status");
            int score = int.Parse(statusNode.Attributes["score"].Value);
            int lives = int.Parse(statusNode.Attributes["lives"].Value);

            SetStatusAria(score, lives);

            _gameAria.Load(xmlNode);
        }

        /// <summary>
        /// Save board
        /// </summary>
        /// <param name="xmlDoc"></param>
        public void Save(XmlDocument  xmlDoc)
        {          
            _status.Save(xmlDoc);
            _gameAria.Save(xmlDoc);
        }

        /// <summary>
        /// Decreese lifes when snake crashed
        /// </summary>
        private void SnakeCkreshed()
        {
            _status.DecreaseLifes();
        }

        /// <summary>
        /// Increese score when snake eats apple
        /// </summary>
        private void SnakeEatApple()
        {
            _status.IncreaseScore();
        }

        /// <summary>
        /// Check is game over
        /// </summary>
        private void CheckLivesEnded()
        {
            if (_status.IsLivesEnded() && GameOver != null)
            {
                GameOver();
            }
        }

        public void Render(ConsoleGraphics graphics)
        {
            _status.Render(graphics);
            _gameAria.Render(graphics);
        }

        public void Update(GameEngine engine)
        {
            _status.Update(engine);
            _gameAria.Update(engine);
            CheckLivesEnded();
        }
    }
}
