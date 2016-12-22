using NConsoleGraphics;
using System;
using System.IO;
using System.Collections.Generic;
using System.Threading;
using System.Xml;

namespace OOPGame
{
    public class SnakeGameEngine : GameEngine
    {
        private const int _size = 20;       
        private bool _isGameOver = false;
        private bool _isGameActive = false;
        private List<Menu> _menus;

        public SnakeGameEngine(ConsoleGraphics graphics) : base(graphics)
        {           
            SetStartState();
        }    

        /// <summary>
        /// Initialize start state of game
        /// </summary>
        public override void SetStartState()
        {
            _menus = new List<Menu>();
            GameBoard board = new GameBoard(_graphics.ClientWidth, _graphics.ClientHeight, _size);
            board.GameOver += SetGameOverState;
            AddObject(board);

            SetStartMenu();
            SetGameMenu();
        }

        private void SetStartMenu()
        {
            Menu startMenu = new Menu(140, 100, 120, 30, new string[] { "New Game", "Load Game", "Exit Game" });
            startMenu._isActive = true;
            startMenu._buttons[0].OnClick += NewGame;
            startMenu._buttons[1].OnClick += LoadGame;
            startMenu._buttons[2].OnClick += ExitGame;
            _menus.Add(startMenu);
        }

        private void SetGameMenu()
        {
            Menu gameMenu = new Menu(140, 100, 120, 30, new string[] { "Continue", "New Game", "Save game", "Load Game", "Exit Game" });
            gameMenu._buttons[0].OnClick += ContinueGame;
            gameMenu._buttons[1].OnClick += NewGame;
            gameMenu._buttons[2].OnClick += SaveGame;
            gameMenu._buttons[3].OnClick += LoadGame;
            gameMenu._buttons[4].OnClick += ExitGame;
            _menus.Add(gameMenu);
        }

        /// <summary>
        /// Changed Flag for game over situation
        /// </summary>
        private void SetGameOverState()
        {
            _isGameOver = true;
        }

        private void ContinueGame()
        {
            _menus[1]._isActive = false;
            _isGameActive = true;
        }

        private void NewGame()
        {
            ResetState();
            _menus[0]._isActive = false;
            _isGameActive = true;
        }

        private void SaveGame()
        {
            SaveToXml();
            _menus[1]._isActive = false;
            _isGameActive = true;
        }

        private void LoadGame()
        {
            XmlDocument xmlDoc = new XmlDocument();
            try
            {
                xmlDoc.Load("game.xml");
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.Message);
            }

            XmlNode gameNode = xmlDoc.DocumentElement;

            ResetState();
            foreach (var obj in _tempObjects)
                obj.Load(gameNode);

            foreach (var menu in _menus)
            {
                menu._isActive = false;
            }
            _isGameActive = true;
        }

        private void ExitGame()
        {
            Environment.Exit(0);
        }

        /// <summary>
        /// Save game to xml file
        /// </summary>
        private void SaveToXml()
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlDeclaration xmlDeclaration = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
            XmlElement root = xmlDoc.DocumentElement;
            xmlDoc.InsertBefore(xmlDeclaration, root);

            XmlNode gameNode = xmlDoc.CreateElement("game");
            xmlDoc.AppendChild(gameNode);
            foreach (var obj in _objects)
                obj.Save(xmlDoc);
            try
            {
                xmlDoc.Save("game.xml");
            }
            catch(IOException ex)
            {
                Console.WriteLine(ex.Message);
            }
           
        }      

        private void CheckGameOverState()
        {
            if (_isGameOver)
            {
                _menus[0]._isActive = true;
                _isGameActive = false;
                _isGameOver = false;
            }
        }

        /// <summary>
        /// Check users input
        /// </summary>
        private void CheckInputKey()
        {
            if (Input.IsKeyDown(Keys.ESCAPE))
            {
                _isGameActive = false;
                _menus[1]._isActive = true;
            }
        }

        /// <summary>
        /// Reset game state
        /// </summary>
        public override void ResetState()
        {
            base.ResetState();
            SetStartState();
        }       

        public override void Start()
        {
            while (true)
            {
                _graphics.FillRectangle(0xFFFFFFFF, 0, 0, _graphics.ClientWidth, _graphics.ClientHeight);

                if (_isGameActive)
                {
                    foreach (var obj in _objects)
                        obj.Update(this);

                    _objects.AddRange(_tempObjects);
                    _tempObjects.Clear();
                
                    foreach (var obj in _objects)
                        obj.Render(_graphics);    
                }

                foreach (var menu in _menus)
                {
                    if (menu._isActive)
                    {
                        menu.Update(this);
                        menu.Render(_graphics);
                    }
                } 

                _graphics.FlipPages();

                CheckGameOverState();
                CheckInputKey();

                Thread.Sleep(200);
            }
        }
    }
}
