using Snake.Configurations;
using Snake.Files;
using Snake.Files.Json;
using Snake.Game.Menu;
using Snake.Extensions;
using System;
using Snake.Game.Enums;

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
            MenuManager menuManager = new MenuManager();
            GameConfig gameConfig = new GameConfig();
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

        public void GameSettingsMenuRender()
        {
            Render.Clear();
            ConsoleConfig config = new ConsoleConfig();
            MenuManager menuManager = new MenuManager();
            GameConfig gameConfig = new GameConfig();
            Frame();
            int widht = config.CenterX;
            int height = config.CenterY - Config.GameSettingsOptions.HalfLength();
            int option = menuManager.GetOptionChoose();
            int chooseMap = menuManager.GetChooseMap();
            int chooseDifficulti = menuManager.GetChooseDifficulti();
            for (int i = 0; i < Config.GameSettingsOptions.Length; i++)
            {
                string text = Config.GameSettingsOptions[i];
                int textHeigth = height + 2 * i;
                if (option == i && i != 0 && i != 1)
                    text = "> " + text + " <";

                if (i == 0)
                {
                    if (option != 0)
                        text += " " + gameConfig.Maps[chooseMap].Name;
                    else
                        text = RenderGameSettingsMapOption(chooseMap);
                }
                else if (i == 1)
                {
                    if (option != 1)
                        text += " " + gameConfig.Difficulti[chooseDifficulti].ToString();
                    else
                        text = RenderGameSettingsDifficultiOption(chooseDifficulti);
                }
                Render.Write(text, widht - text.HalfLength(), textHeigth);
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
                    if (scoresFile.Scores.Count > i)
                    {
                        string text = (i + 1) + ": ";
                        text += scoresFile.Scores[i].Name + " " + scoresFile.Scores[i].Scores;
                        Render.Write(text, 2, 2 + 2 * i);
                    }
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
            GameConfig gameConfig = new GameConfig();
            char[] skins = gameConfig.SkinsSnake;

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

        private string RenderGameSettingsDifficultiOption(int choose)
        {
            GameConfig gameConfig = new GameConfig();
            DifficultiGameEnum[] difficulti = gameConfig.Difficulti;

            int index = choose - 1;
            if (index < 0)
                index = difficulti.Length - 1;
            string text = difficulti[index].ToString() + " > " + difficulti[choose].ToString() + " < ";

            index = choose + 1;
            if (index > difficulti.Length - 1)
                index = 0;
            text += difficulti[index].ToString();

            return text;
        }

        private string RenderGameSettingsMapOption(int choose)
        {
            GameConfig gameConfig = new GameConfig();
            MapFile[] map = gameConfig.Maps;
            int index = choose - 1;
            if (index < 0)
                index = map.Length - 1;
            string text = map[index].Name + " > " + map[choose].Name + " < ";

            index = choose + 1;
            if (index > map.Length - 1)
                index = 0;
            text += map[index].Name;

            return text;
        }
    }
}
