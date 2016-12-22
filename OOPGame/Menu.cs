using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using NConsoleGraphics;

namespace OOPGame
{
    class Menu : IGameObject
    {
        public List<Button> _buttons { get; set; }
        public bool _isActive { get; set; }

        public Menu(int x, int y, int width, int height, string[] buttonName)
        {
            _buttons = new List<Button>();
            SetButtons(x, y, width, height, buttonName);
            _isActive = false;
        }

        private void SetButtons(int x, int y, int width, int height, string[] buttonName)
        {
            if (buttonName.Length > 0)
            {
                _buttons.Add(new Button(x, y, width, height, buttonName[0]));
                for (int i = 1; i < buttonName.Length; i++)
                {
                    _buttons.Add(new Button(x, 10 + _buttons[i - 1].GetOffset(), width, height, buttonName[i]));
                }
            }
        }

        public void Render(ConsoleGraphics graphics)
        {
            foreach (var button in _buttons)
            {
                button.Render(graphics);
            }
        }

        public void Update(GameEngine engine)
        {
            foreach (var button in _buttons)
            {
                button.Update(engine);
            }
        }

        public void Save(XmlDocument xmlDoc)
        { }

        public void Load(XmlNode xmlNode)
        { }
    }
}
