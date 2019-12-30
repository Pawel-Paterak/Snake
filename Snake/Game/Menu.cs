using Snake.Controlers;
using Snake.Game.Enums;
using Snake.Game.Render;
using System;

namespace Snake.Game
{
    public class Menu
    {
        private readonly GameManager game = new GameManager();
        private readonly ConsoleRender render = new ConsoleRender();
        private readonly KeyboardControl keyboardControl = new KeyboardControl();
        private Func<char> getKey;
        private bool isNewKey = false;
        private char Key { get => key;
            set
            {
                key = value;
                isNewKey = true;
            }
        }
        private char key;
        public MenuEnum Canvas;

        public Menu(GameManager game)
        {
            this.game = game;
            getKey = GetKey;
            keyboardControl.PressKeyEvent += OnPressKey;
            keyboardControl.Start();
        }

        public void RenderCanvas()
        {
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
                            isLoop = !LevelsMenuRender(getKey());
                            break;
                        }
                }
            } while (isLoop);
        }

        private void MainMenuRender()
        {
            render.Write("This is Main menu");
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
                default:
                    {
                        return false;
                    }
            }
        }

        private void LevelsMenuRender()
        {
            render.Write("This is Levels menu");
        }

        private bool LevelsMenuRender(char key)
        {
            switch (key)
            {
                default:
                    {
                        return false;
                    }
            }
        }

        private void OnPressKey(char key)
            => Key = key;

        private char GetKey()
        {
            bool isLoop = true;
            char key = ' ';
            do
            {
                if (isNewKey)
                {
                    isLoop = false;
                    isNewKey = false;
                    key = Key;
                }

            } while (isLoop);
            return key;
        }

        public void Close()
        {
            keyboardControl.PressKeyEvent -= OnPressKey;
            keyboardControl.Close();
        }
    }
}
