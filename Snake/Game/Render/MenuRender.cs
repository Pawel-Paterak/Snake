using Snake.Configurations;
using Snake.Files;
using Snake.Files.Json;
using Snake.Game.Menu;

namespace Snake.Game.Render
{
    public class MenuRender
    {
        private readonly ConsoleRender render = new ConsoleRender();

        public void MainMenuRender()
        {
            render.Clear();
            ConsoleConfig console = new ConsoleConfig();
            Frame();
            string[] options = new string[4];
            options[0] = "Start";
            options[1] = "Multiplayer(disable)";
            options[2] = "Scores";
            options[3] = "Exit";

            int widht = console.Widht / 2;
            int height = console.Height / 2 - options.Length / 2;
            for (int i = 0; i < options.Length; i++)
            {
                string text = options[i];
                if (i == MenuManager.singleton.OptionChoose)
                    text = "> " + text + " <";
                int offsetXText = text.Length / 2;
                render.Write(text, widht - offsetXText, height + 2 * i);
            }
        }

        public void LevelsMenuRender()
        {
            render.Clear();
            ConsoleConfig console = new ConsoleConfig();
            Frame();
            string[] options = new string[4];
            options[0] = "Easy";
            options[1] = "Medium";
            options[2] = "Hard";
            options[3] = "Back";

            int widht = console.Widht / 2;
            int height = console.Height / 2 - options.Length / 2;
            for (int i = 0; i < options.Length; i++)
            {
                string text = options[i];
                if (i == MenuManager.singleton.OptionChoose)
                    text = "> " + text + " <";
                int offsetXText = text.Length / 2;
                render.Write(text, widht - offsetXText, height + 2 * i);
            }
        }

        public void CustomsSnakeMenuRender()
        {
            render.Clear();
            ConsoleConfig console = new ConsoleConfig();
            GameConfig gameConfig = new GameConfig();
            Frame();
            string[] options = new string[4];
            options[0] = "Color";
            options[1] = "Skin";
            options[2] = "Play";
            options[3] = "Back";
            int widht = console.Widht / 2;
            int height = console.Height / 2 - (options.Length + 2) / 2;
            int option = MenuManager.singleton.OptionChoose;
            for (int i = 0; i < options.Length; i++)
            {
                string text = options[i];
                if (option == i && i != 0 && i != 1)
                    text = "> " + text + " <";

                if (option != 0 && i == 0)
                    text += " " + gameConfig.ColorsSnake[MenuManager.singleton.GetChooseColor()];

                if (option != 1 && i == 1)
                    text += " " + gameConfig.SkinsSnake[MenuManager.singleton.GetChooseSkins()];

                int offsetXText = text.Length / 2;
                render.Write(text, widht - offsetXText, height + 2 * i);

                if (option == 0 && i == 0)
                {
                    int index = MenuManager.singleton.GetChooseColor() - 1;
                    if (index < 0)
                        index = gameConfig.ColorsSnake.Length - 1;
                    text = gameConfig.ColorsSnake[index] + " ";

                    index = MenuManager.singleton.GetChooseColor();
                    if (index < 0 || index > gameConfig.ColorsSnake.Length - 1)
                        index = 0;
                    text += "> " + gameConfig.ColorsSnake[index] + " < ";

                    index = MenuManager.singleton.GetChooseColor() + 1;
                    if (index > gameConfig.ColorsSnake.Length - 1)
                        index = 0;
                    text += gameConfig.ColorsSnake[index];

                    offsetXText = text.Length / 2;
                    render.Write(text, widht - offsetXText, height + 2 * i);
                }

                if (option == 1 && i == 1)
                {
                    int index = MenuManager.singleton.GetChooseSkins() - 1;
                    if (index < 0)
                        index = gameConfig.SkinsSnake.Length - 1;
                    text = gameConfig.SkinsSnake[index] + " ";

                    index = MenuManager.singleton.GetChooseSkins();
                    if (index < 0 || index > gameConfig.SkinsSnake.Length - 1)
                        index = 0;
                    text += "> " + gameConfig.SkinsSnake[index] + " < ";

                    index = MenuManager.singleton.GetChooseSkins() + 1;
                    if (index > gameConfig.SkinsSnake.Length - 1)
                        index = 0;
                    text += gameConfig.SkinsSnake[index];

                    offsetXText = text.Length / 2;
                    render.Write(text, widht - offsetXText, height + 2 * i);
                }
            }
        }

        public void ScoresMenuRender()
        {
            render.Clear();
            Frame();
            JsonManager json = new JsonManager();
            ScoresFile scoresFile = json.Read<ScoresFile>(GameConfig.ScoresFile);
            for (int i = 0; i < 17; i++)
            {
                string text = (i + 1) + ": ";
                if (scoresFile != null && i < scoresFile.Scores.Count)
                    text += scoresFile.Scores[i].Name + " " + scoresFile.Scores[i].Scores;
                render.Write(text, 2, 2 + 2 * i);
            }

            render.Write("> back <", 2, 36);
        }

        public void Frame()
        {
            ConsoleConfig console = new ConsoleConfig();
            for (int x = 0; x < console.Widht; x++)
            {
                render.Write("#", x, 0);
                render.Write("#", x, console.Height - 2);
            }

            for (int y = 0; y < console.Height - 1; y++)
            {
                render.Write("#", 0, y);
                render.Write("#", console.Widht - 1, y);
            }
        }
    }
}
