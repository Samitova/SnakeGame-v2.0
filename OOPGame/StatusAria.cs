using System;
using NConsoleGraphics;
using System.Xml;

namespace OOPGame
{
    class StatusAria : Aria, IGameObject
    {
        public int Score { get; set; }
        public int Lives { get; set; }
        private int _size;

        public StatusAria(int x, int y, int width, int height, int size): base (x, y, width, height)
        {
            Score = 0;
            Lives = 3;
            _size = size;
        }

        public void SetStatus(int score, int lives)
        {
            Score = score;
            Lives = lives;
        }

        /// <summary>
        /// Check amount of lifes
        /// </summary>
        /// <returns>bool</returns>
        public bool IsLivesEnded()
        {
            if (Lives < 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Increase score
        /// </summary>
        public void IncreaseScore()
        {
            Score += 10;
        }

        /// <summary>
        /// Decrease score
        /// </summary>
        public void DecreaseLifes()
        {
            --Lives;
        }      

        public override string ToString()
        {
            return String.Format("{0, 20} {1, 10} {2, -6} {3, 6} {4, -6}", "Cool game snake.", "Lifes:", Lives, "Score:", Score);
        }

        public void Render(ConsoleGraphics graphics)
        {
            graphics.DrawString(this.ToString(), "Arial", 0xFFBD00F0, 50, graphics.ClientHeight - _size, 10);
        }
        
        public void Save(XmlDocument xmlDoc)
        {
            XmlNode statusNode = xmlDoc.CreateElement("status");
            XmlAttribute attribute = xmlDoc.CreateAttribute("score");
            attribute.Value = Score.ToString();
            statusNode.Attributes.Append(attribute);
            attribute = xmlDoc.CreateAttribute("lives");
            attribute.Value = Lives.ToString();
            statusNode.Attributes.Append(attribute);
            xmlDoc.LastChild.AppendChild(statusNode);
        }

        public void Load(XmlNode xmlNode)
        { }

        public void Update(GameEngine engine)
        { }
    }
}
