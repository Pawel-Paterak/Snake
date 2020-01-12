namespace Snake.Game
{
    public class Score
    {
        public int Scores { get; set; }
        public string Name { get; set; }

        public Score()
        {

        }
        public Score(int scores, string name)
        {
            Scores = scores;
            Name = name;
        }
    }
}
