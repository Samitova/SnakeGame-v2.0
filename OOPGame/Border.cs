using System.Xml;
using NConsoleGraphics;

namespace OOPGame
{
    class Border: IGameObject
    {
        public int Width { get; set; }
        public int Height { get; set; }
        private int _thickness;

        public Border(int width, int height, int size)
        {
            Width = width;
            Height = height;
            _thickness = size;
        }

        public void Render(ConsoleGraphics graphics)
        {           
            graphics.DrawLine(0xFFFFAB00, 0, 0, Width, 0, _thickness);
            graphics.DrawLine(0xFFFFAB00, 0, 0, 0, Height, _thickness);
            graphics.DrawLine(0xFFFFAB00, Width, 0, Width, Height, _thickness );
            graphics.DrawLine(0xFFFFAB00, 0, Height - _thickness/4, Width, Height - _thickness/4, _thickness/2);
        }

        public void Update(GameEngine engine)
        { }

        public void Save(XmlDocument xmlDoc)
        { }

        public void Load(XmlNode xmlNode)
        { }
    }
}
