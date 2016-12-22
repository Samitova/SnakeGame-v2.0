using System;
using NConsoleGraphics;
using System.Xml;

namespace OOPGame
{
    class SnakeItem:IGameObject
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int XSpeed { get; set; }
        public int YSpeed { get; set; }
        private int Size
        {
            get
            {
                return Math.Abs(XSpeed + YSpeed);
            }
        }

        public SnakeItem(int x, int y, int xspeed, int yspeed)
        {
            X = x;
            Y = y;
            XSpeed = xspeed;
            YSpeed = yspeed;           
        }

        /// <summary>
        /// Save to xml file
        /// </summary>
        /// <param name="xmlDoc"></param>
        public void Save(XmlDocument xmlDoc)
        {
            XmlNode snakeItemNode = xmlDoc.CreateElement("snakeItem");
            XmlAttribute attribute = xmlDoc.CreateAttribute("x");
            attribute.Value = X.ToString();
            snakeItemNode.Attributes.Append(attribute);
            attribute = xmlDoc.CreateAttribute("y");
            attribute.Value = Y.ToString();
            snakeItemNode.Attributes.Append(attribute);
            attribute = xmlDoc.CreateAttribute("xSpeed");
            attribute.Value = XSpeed.ToString();
            snakeItemNode.Attributes.Append(attribute);
            attribute = xmlDoc.CreateAttribute("ySpeed");
            attribute.Value = YSpeed.ToString();
            snakeItemNode.Attributes.Append(attribute);
            xmlDoc.LastChild.LastChild.AppendChild(snakeItemNode);
        }

        public void Render(ConsoleGraphics graphics)
        {
            graphics.FillRectangle(0xFFFF0000, X, Y, Size, Size);
        }

        public void Update(GameEngine engine)
        {                         
                X += XSpeed;
                Y += YSpeed;           
        }

        public void Load(XmlNode xmlNode)
        { }
    }
}
