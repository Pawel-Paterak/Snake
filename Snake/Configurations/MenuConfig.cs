namespace Snake.Configurations
{
    public class MenuConfig
    {
        public int CountMenuOptions { get => MenuOptions.Length; }
        public int CountMenuCustomsSnakeOptions { get => MenuCustomsSnakeOptions.Length; }
        public int CountMenuGameOptions { get => MenuGameOptions.Length; }
        public int CountScoreOptions { get; private set; } = 1;
        public string[] MenuOptions { get; private set; }
        public string[] MenuCustomsSnakeOptions { get; private set; }
        public string[] MenuGameOptions { get; private set; }
        public string MenuScoreOptions { get; private set; } = "Back";

        public MenuConfig()
        {
            InitializeMenu();
            InitializeCustomsSnake();
            InitializeGameSettings();
        }

        private void InitializeMenu()
        {
            MenuOptions = new string[4];
            MenuOptions[0] = "Start";
            MenuOptions[1] = "Multiplayer(disable)";
            MenuOptions[2] = "Scores";
            MenuOptions[3] = "Exit";
        }

        private void InitializeCustomsSnake()
        {
            MenuCustomsSnakeOptions = new string[4];
            MenuCustomsSnakeOptions[0] = "Color";
            MenuCustomsSnakeOptions[1] = "Skin";
            MenuCustomsSnakeOptions[2] = "Next";
            MenuCustomsSnakeOptions[3] = "Back";
        }

        private void InitializeGameSettings()
        {
            MenuGameOptions = new string[4];
            MenuGameOptions[0] = "Map";
            MenuGameOptions[1] = "Difficulti";
            MenuGameOptions[2] = "Play";
            MenuGameOptions[3] = "Back";
        }
    }
}
