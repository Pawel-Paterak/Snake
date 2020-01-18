namespace Snake.Configurations
{
    public class MenuConfig
    {
        public int CountMenuOptions { get => MenuOptions.Length; }
        public int CountCustomsSnakeOptions { get => CustomsSnakeOptions.Length; }
        public int CountLevelOptions { get => GameSettingsOptions.Length; }
        public int CountScoreOptions { get; private set; } = 1;
        public string[] MenuOptions { get; private set; }
        public string[] CustomsSnakeOptions { get; private set; }
        public string[] GameSettingsOptions { get; private set; }
        public string ScoreOption { get; private set; } = "Back";

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
            CustomsSnakeOptions = new string[4];
            CustomsSnakeOptions[0] = "Color";
            CustomsSnakeOptions[1] = "Skin";
            CustomsSnakeOptions[2] = "Next";
            CustomsSnakeOptions[3] = "Back";
        }
        private void InitializeGameSettings()
        {
            GameSettingsOptions = new string[4];
            GameSettingsOptions[0] = "Map";
            GameSettingsOptions[1] = "Difficulti";
            GameSettingsOptions[2] = "Play";
            GameSettingsOptions[3] = "Back";
        }
    }
}
