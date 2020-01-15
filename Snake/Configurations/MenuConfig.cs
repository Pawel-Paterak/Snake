namespace Snake.Configurations
{
    public class MenuConfig
    {
        public int CountMenuOptions { get => MenuOptions.Length; }
        public int CountCustomsSnakeOptions { get => CustomsSnakeOptions.Length; }
        public int CountLevelOptions { get => LevelsOptions.Length; }
        public int CountScoreOptions { get; private set; } = 1;
        public string[] MenuOptions { get; private set; }
        public string[] CustomsSnakeOptions { get; private set; }
        public string[] LevelsOptions { get; private set; }
        public string ScoreOption { get; private set; } = "Back";

        public MenuConfig()
        {
            InitializeMenu();
            InitializeCustomsSnake();
            InitializeLevels();
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
            CustomsSnakeOptions = new string[4];
            CustomsSnakeOptions[0] = "Color";
            CustomsSnakeOptions[1] = "Skin";
            CustomsSnakeOptions[2] = "Play";
            CustomsSnakeOptions[3] = "Back";
        }
        private void InitializeLevels()
        {
            LevelsOptions = new string[4];
            LevelsOptions[0] = "Easy";
            LevelsOptions[1] = "Medium";
            LevelsOptions[2] = "Hard";
            LevelsOptions[3] = "Back";
        }
    }
}
