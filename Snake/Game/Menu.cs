﻿using Snake.Configurations;
using Snake.Controlers;
using Snake.Game.Enums;
using Snake.Game.Render;
using System;

namespace Snake.Game
{
    public class Menu
    {
        public MenuEnum Canvas { get; set; }

        private readonly GameManager game;
        private readonly ConsoleRender render = new ConsoleRender();
        private readonly Func<char> getKey;
        private bool isNewKey = false;
        private char Key { get => key;
            set
            {
                key = value;
                isNewKey = true;
            }
        }
        private char key;

        public Menu(GameManager game)
        {
            this.game = game;
            KeyboardControl.Start();
            getKey = GetKey;
        }

        public void RenderCanvas()
        {
            KeyboardControl.PressKeyEvent += OnPressKey;

            bool isLoop = true;
            do
            {
                switch (Canvas)
                {
                    case MenuEnum.MainMenu:
                        {
                            MainMenuRender();
                            isLoop = !MainMenu(getKey());
                            break;
                        }
                    case MenuEnum.Levels:
                        {
                            LevelsMenuRender();
                            isLoop = !LevelsMenu(getKey());
                            break;
                        }
                }
            } while (isLoop);
            KeyboardControl.PressKeyEvent -= OnPressKey;
        }

        private void MainMenuRender()
        {
            render.Clear();
            ConsoleConfiguration console = new ConsoleConfiguration();
            Frame();
            int offsetXText = 5;
            int widht = console.widht / 2;
            int height = console.height / 2;
            string text = "1) Start";
            render.Write(text, widht - offsetXText, height - 3);
            text = "2) Multiplayer(disable)";
            render.Write(text, widht- offsetXText, height-1);
            text = "3) Scores(disable)";
            render.Write(text, widht - offsetXText, height+1);
            text = "4) Exit";
            render.Write(text, widht - offsetXText, height+3);
        }

        private void Frame()
        {
            ConsoleConfiguration console = new ConsoleConfiguration();
            for (int x = 0; x < console.widht; x++)
            {
                render.Write("#", x, 0);
                render.Write("#", x, console.height - 2);
            }

            for (int y = 0; y < console.height - 1; y++)
            {
                render.Write("#", 0, y);
                render.Write("#", console.widht - 1, y);
            }
        }

        private bool MainMenu(char key)
        {
            switch(key)
            {
                case '1':
                    {
                        Canvas = MenuEnum.Levels;
                        return false;
                    }
                case '4':
                    {
                        CloseConsole();
                        return true;
                    }
                default:
                    {
                        return false;
                    }
            }
        }

        private void LevelsMenuRender()
        {
            render.Clear();
            ConsoleConfiguration console = new ConsoleConfiguration();
            Frame();
            int offsetXText = 5;
            int widht = console.widht / 2;
            int height = console.height / 2;
            string text = "1) Easy";
            render.Write(text, widht - offsetXText, height - 3);
            text = "2) Medium";
            render.Write(text, widht - offsetXText, height - 1);
            text = "3) Hard";
            render.Write(text, widht - offsetXText, height + 1);
            text = "4) Back";
            render.Write(text, widht - offsetXText, height + 3);
        }

        private bool LevelsMenu(char key)
        {
            switch (key)
            {
                case '1':
                    {
                        game.RefreshTime = 100;
                        break;
                    }
                case '2':
                    {
                        game.RefreshTime = 60;
                        break;
                    }
                case '3':
                    {
                        game.RefreshTime = 30;
                        break;
                    }
                case '4':
                    {
                        Canvas = MenuEnum.MainMenu;
                        return false;
                    }
                default:
                    {
                        return false;
                    }
            }
            return true;
        }

        private void OnPressKey(char key)
            => Key = key;

        private char GetKey()
        {
            bool isLoop = true;
            char myKey = ' ';
            do
            {
                if (isNewKey)
                {
                    isLoop = false;
                    isNewKey = false;
                    myKey = Key;
                }

            } while (isLoop);
            return myKey;
        }

        private void CloseConsole()
        {
            KeyboardControl.Close();
            Environment.Exit(0);
        }
    }
}
