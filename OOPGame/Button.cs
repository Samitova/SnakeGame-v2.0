using System;
using NConsoleGraphics;
using System.Xml;

namespace OOPGame
{
    class Button : IGameObject
    {
        private string _text;
        private int _width;
        private int _height;
        private int _x;
        private int _y;       
       
        public event Action OnClick;

        public Button(int x, int y, int width, int height, string text)
        {           
            _x = x;
            _y = y;
            _width = width;
            _height = height;
            _text = text;
        }

        public void Render(ConsoleGraphics graphics)
        {
            graphics.DrawRectangle(0xFFFFAB00, _x, _y, _width, _height, 2);
            graphics.DrawString(_text, "Arial", 0xFFFF0000, _x + (_width - (int)GetTextInPixel()) /2, _y + 5, 14);            
        }

        public void Update(GameEngine engine)
        {
            CheckClick();
        }

        /// <summary>
        /// Get offset for next button
        /// </summary>
        /// <returns></returns>
        public int GetOffset()
        {
            return _y + _height + 10;
        }

        private double GetTextInPixel()
        {
            return _text.Length * 12.5;
        }

        public void CheckClick()
        {
            if (Input.IsMouseLeftButtonDown && Input.MouseX >= _x && Input.MouseX <= _x + _width && Input.MouseY >= _y && Input.MouseY <= _y + _height)
            {
                OnClick();
            }
        }

        public void Save(XmlDocument xmlDoc)
        { }

        public void Load(XmlNode xmlNode)
        { }
    }
}
