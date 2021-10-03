using Snake.Configurations;
using Snake.Files;
using Snake.Game.Menu;
using Snake.Extensions;
using Snake.Game.Settings;

namespace Snake.Game.Render
{
    public class MenuRender
    {
        private readonly ConsoleRender render = new ConsoleRender();
        private readonly ConsoleConfig consoleConfig = new ConsoleConfig();
        private readonly MenuConfig menuConfig = new MenuConfig();

        public void MainMenuRender()
        {
            render.Clear();
            MenuManager menuManager = new MenuManager();
            Frame();

            int widht = consoleConfig.CenterX;
            int height = consoleConfig.CenterY - menuConfig.MenuOptions.HalfLength();
            for (int i = 0; i < menuConfig.MenuOptions.Length; i++)
            {
                string text = menuConfig.MenuOptions[i];
                int textHeigth = height + 2 * i;
                if (i == menuManager.GetActiveOption())
                    text = "> " + text + " <";
                RenderText(text, widht - text.HalfLength(), textHeigth);
            }
        }

        public void MenuSnakeRender()
        {
            render.Clear();
            MenuManager menuManager = new MenuManager();
            GameConfig gameConfig = new GameConfig();
            GameSettings gameSettings = new GameSettings();
            Frame();
            int widht = consoleConfig.CenterX;
            int height = consoleConfig.CenterY - menuConfig.MenuCustomsSnakeOptions.HalfLength();
            int option = menuManager.GetActiveOption();
            int selectedColor = gameSettings.GetNumberColor();
            int selectedSkin = gameSettings.GetNumberSkin();
            for (int i = 0; i < menuConfig.MenuCustomsSnakeOptions.Length; i++)
            {
                string text = menuConfig.MenuCustomsSnakeOptions[i];
                int textHeigth = height + 2 * i;
                if (option == i && i != 0 && i != 1)
                    text = "> " + text + " <";

                if (i == 0)
                    text = GetTextAdvencedOptions(text, option, i, gameConfig.Colors, selectedColor);
                else if (i == 1)
                    text = GetTextAdvencedOptions(text, option, i, gameConfig.Skins, selectedSkin);
                RenderText(text, widht - text.HalfLength(), textHeigth);
            }
        }

        public void MenuGameRender()
        {
            render.Clear();
            MenuManager menuManager = new MenuManager();
            GameConfig gameConfig = new GameConfig();
            GameSettings gameSettings = new GameSettings();
            Frame();
            int widht = consoleConfig.CenterX;
            int height = consoleConfig.CenterY - menuConfig.MenuGameOptions.HalfLength();
            int option = menuManager.GetActiveOption();
            int selectedMap = gameSettings.GetNumberMap();
            int seletcedDifficulti = gameSettings.GetNumberDifficulti();
            for (int i = 0; i < menuConfig.MenuGameOptions.Length; i++)
            {
                string text = menuConfig.MenuGameOptions[i];
                int textHeigth = height + 2 * i;
                if (option == i && i != 0 && i != 1)
                    text = "> " + text + " <";

                if (i == 0)
                    text = GetTextAdvencedOptions(text, option, i, gameConfig.Maps, selectedMap);
                else if (i == 1)
                    text = GetTextAdvencedOptions(text, option, i, gameConfig.Difficulti, seletcedDifficulti);
                RenderText(text, widht - text.HalfLength(), textHeigth);
            }
        }

        public void MenuScoresRender()
        {
            render.Clear();
            Frame();
            FileManager file = new FileManager();
            ScoresFile scoresFile = file.GetScores();
            if (scoresFile != null)
            {
                for (int i = 0; i < scoresFile.MaxSlots; i++)
                {
                    if (scoresFile.Scores.Count > i)
                    {
                        string text = (i + 1) + ": ";
                        int textHeigth = 2 + 2 * i;
                        text += scoresFile.Scores[i].Name + " " + scoresFile.Scores[i].Scores;
                        RenderText(text, 2, textHeigth);
                    }
                }
            }

            RenderText("> "+menuConfig.MenuScoreOptions+" <", 2, 36);
        }

        public void Frame()
        {
            for (int x = 0; x < consoleConfig.Widht; x++)
            {
                render.Write("#", x, 0);
                render.Write("#", x, consoleConfig.Height - 2);
            }

            for (int y = 0; y < consoleConfig.Height - 1; y++)
            {
                render.Write("#", 0, y);
                render.Write("#", consoleConfig.Widht - 1, y);
            }
        }

        private string GetTextAdvencedOptions<T>(string text, int option, int i, T[] objects, int selectedOption)
        {
            if (option != i)
                text += " " + objects[selectedOption].ToString();
            else
                text = AdvancedOptions(selectedOption, objects);
            return text;
        }

        private string AdvancedOptions<T>(int selectedOption, T[] objects)
        {
            int index = selectedOption - 1;
            if (index < 0)
                index = objects.Length - 1;
            string text = objects[index].ToString() + " > " + objects[selectedOption].ToString() + " < ";

            index = selectedOption + 1;
            if (index > objects.Length - 1)
                index = 0;
            text += objects[index].ToString();

            return text;
        }

        private void RenderText(string text, int x, int y)
            => render.Write(text, x, y);
    }
}
