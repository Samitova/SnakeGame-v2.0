using NConsoleGraphics;
using System.Collections.Generic;
using System.Threading;
using System;

namespace OOPGame
{
    public abstract class GameEngine
    {
        protected ConsoleGraphics _graphics;
        protected List<IGameObject> _objects = new List<IGameObject>();
        protected List<IGameObject> _tempObjects = new List<IGameObject>();      

        public GameEngine(ConsoleGraphics graphics)
        {
            this._graphics = graphics;           
        }

        public abstract void SetStartState();
       
        public virtual void ResetState()
        {
            _objects.Clear();
            _tempObjects.Clear();
        }
       
        public void SetGraphics(ConsoleGraphics graph)
        {
            _graphics = graph;
        }

        public virtual  void AddObject(IGameObject obj)
        {
            _tempObjects.Add(obj);
        }       

        public virtual void Start()
        {
            while (true)
            {
                // Game Loop
                foreach (var obj in _objects)
                    obj.Update(this);

                _objects.AddRange(_tempObjects);
                _tempObjects.Clear();

                // clearing screen before painting new frame
                _graphics.FillRectangle(0xFFFFFFFF, 0, 0, _graphics.ClientWidth, _graphics.ClientHeight);
                foreach (var obj in _objects)
                    obj.Render(_graphics);

                // double buffering technique is used
                _graphics.FlipPages();

                Thread.Sleep(200);
            }
        }
    }
}
