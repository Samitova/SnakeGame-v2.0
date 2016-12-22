using NConsoleGraphics;
using System.Xml;

namespace OOPGame
{
    public interface IGameObject
    {
        void Render(ConsoleGraphics graphics);

        void Update(GameEngine engine);

        void Save(XmlDocument xmlDoc);

        void Load(XmlNode xmlNode);

    }
}