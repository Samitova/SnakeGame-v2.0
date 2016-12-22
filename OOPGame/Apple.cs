using System;
using System.Xml;
using NConsoleGraphics;

namespace OOPGame
{
    class Apple : Fruit, IGameObject
    {
        public Apple(int x, int y, int size) : base(x, y, size)
        { }

        public void SetApplePosition(int x, int y)
        {
            X = x;
            Y = y;
        }

        public void Render(ConsoleGraphics graphics)
        {
            graphics.FillRectangle(0xFF00FF00, X, Y, Size, Size);
        }

        public void Save(XmlDocument xmlDoc)
        {
            XmlNode appleNode = xmlDoc.CreateElement("apple");
            XmlAttribute attribute = xmlDoc.CreateAttribute("x");
            attribute.Value = X.ToString();
            appleNode.Attributes.Append(attribute);
            attribute = xmlDoc.CreateAttribute("y");
            attribute.Value = Y.ToString();           
            appleNode.Attributes.Append(attribute);           
            xmlDoc.LastChild.AppendChild(appleNode);
        }

        public void Load(XmlNode xmlNode)
        { }

        public void Update(GameEngine engine)
        { }
    }
}
