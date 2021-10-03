namespace Snake.Game.Menu
{
    public class IntigerBox
    {
        public int Intiger {
            get => intiger;
            set
            {
                intiger = value;
                if (intiger < 0)
                    intiger = MaxIntiger - 1;
                else if (intiger >= MaxIntiger)
                    intiger = 0;
            }
        }
        public int MaxIntiger { get; set; }

        private int intiger = 0;

        public IntigerBox(int option)
        {
            Intiger = option;
            this.intiger = option;
        }

        public IntigerBox(int value, int maxIntiger)
        {
            Intiger = value;
            intiger = value;
            MaxIntiger = maxIntiger;
        }
    }
}
