using Snake.Configurations;
using Snake.Files;
using Snake.Files.Json;
using Snake.Game.Menu;
using Snake.Extensions;
using System;

namespace Snake.Game.Render
{
    public class MenuRender
    {
        private ConsoleRender Render { get; set; }  = new ConsoleRender();
        private MenuConfig Config { get; set; } = new MenuConfig();

        public void MainMenuRender()
        {
            Render.Clear();
            ConsoleConfig config = new ConsoleConfig();
            MenuManager menuManager = new MenuManager();
            Frame();

            int widht = config.CenterX;
            int height = config.CenterY - Config.MenuOptions.HalfLength();
            for (int i = 0; i < Config.MenuOptions.Length; i++)
            {
                string text = Config.MenuOptions[i];
                if (i == menuManager.GetOptionChoose())
                    text = "> " + text + " <";
                Render.Write(text, widht - text.HalfLength(), height + 2 * i);
            }
        }
        public void CustomsSnakeMenuRender()
        {
            Render.Clear();
            ConsoleConfig config = new ConsoleConfig();
            GameConfig gameConfig = new GameConfig();
            MenuManager menuManager = new MenuManager();
            Frame();
            int widht = config.CenterX;
            int height = config.CenterY - (Config.CustomsSnakeOptions.Length + 2) / 2;
            int option = menuManager.GetOptionChoose();
            int chooseColor = menuManager.GetChooseColor();
            int chooseSkin = menuManager.GetChooseSkins();
            for (int i = 0; i < Config.CustomsSnakeOptions.Length; i++)
            {
                string text = Config.CustomsSnakeOptions[i];
                int textHeigth = height + 2 * i;
                if (option == i && i != 0 && i != 1)
                    text = "> " + text + " <";

                if (i == 0)
                {
                    if (option != 0)
                        text += " " + gameConfig.ColorsSnake[chooseColor];
                    else
                        text = RenderCustomsColorOption(chooseColor);
                }
                else if (i == 1)
                {
                    if (option != 1)
                        text += " " + gameConfig.SkinsSnake[chooseSkin];
                    else
                        text = RenderCustomsSkinsOption(chooseSkin);
                }
                Render.Write(text, widht - text.HalfLength(), textHeigth);
            }
        }
        public void LevelsMenuRender()
        {
            Render.Clear();
            ConsoleConfig config = new ConsoleConfig();
            MenuManager menuManager = new MenuManager();
            Frame();
            int widht = config.CenterX;
            int height = config.CenterY - Config.LevelsOptions.HalfLength();
            for (int i = 0; i < Config.LevelsOptions.Length; i++)
            {
                string text = Config.LevelsOptions[i];
                if (i == menuManager.GetOptionChoose())
                    text = "> " + text + " <";
                Render.Write(text, widht - text.HalfLength(), height + 2 * i);
            }
        }
        public void ScoresMenuRender()
        {
            Render.Clear();
            Frame();
            JsonManager json = new JsonManager();
            ScoresFile scoresFile = json.Read<ScoresFile>(GameConfig.ScoresFile);
            if (scoresFile != null)
            {
                for (int i = 0; i < scoresFile.MaxSlots; i++)
                {
                    string text = (i + 1) + ": ";
                    text += scoresFile.Scores[i].Name + " " + scoresFile.Scores[i].Scores;
                    Render.Write(text, 2, 2 + 2 * i);
                }
            }

            Render.Write("> "+Config.ScoreOption+" <", 2, 36);
        }
        public void Frame()
        {
            ConsoleConfig console = new ConsoleConfig();
            for (int x = 0; x < console.Widht; x++)
            {
                Render.Write("#", x, 0);
                Render.Write("#", x, console.Height - 2);
            }

            for (int y = 0; y < console.Height - 1; y++)
            {
                Render.Write("#", 0, y);
                Render.Write("#", console.Widht - 1, y);
            }
        }

        private string RenderCustomsSkinsOption(int choose)
        {
            ConsoleConfig config = new ConsoleConfig();
            GameConfig gameConfig = new GameConfig();
            char[] skins = gameConfig.SkinsSnake;
            int widht = config.CenterX;

            int index = choose - 1;
            if (index < 0)
                index = skins.Length - 1;
            string text = skins[index] + " > " + skins[choose] + " < ";

            index = choose + 1;
            if (index > skins.Length - 1)
                index = 0;
            text += skins[index];

            return text;
        }
        private string RenderCustomsColorOption(int choose)
        {
            ConsoleConfig config = new ConsoleConfig();
            GameConfig gameConfig = new GameConfig();
            ConsoleColor[] colors = gameConfig.ColorsSnake;

            int index = choose - 1;
            if (index < 0)
                index = colors.Length - 1;
            string text = colors[index] + " > " + colors[choose] + " < ";

            index = choose + 1;
            if (index > colors.Length - 1)
                index = 0;
            text += colors[index];

            return text;
        }
    }
}
