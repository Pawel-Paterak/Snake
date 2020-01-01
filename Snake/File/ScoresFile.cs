using System.Collections.Generic;

namespace Snake.File
{
    public class ScoresFile
    {
        public List<Score> Scores { get; set; } = new List<Score>();

        public ScoresFile()
        {

        }

        public ScoresFile(List<Score> scores)
        {
            Scores = scores;
        }
    }

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
