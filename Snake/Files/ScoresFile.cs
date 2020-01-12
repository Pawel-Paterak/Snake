using Snake.Game;
using System.Collections.Generic;

namespace Snake.Files
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
        public void AddScores(Score score)
        {
            int countScores = 17;
            int missingScore = countScores - Scores.Count;
            if (missingScore > 0)
            {
                for (int i = 0; i < missingScore; i++)
                    Scores.Add(new Score(0, ""));
            }

            for (int i = countScores - 1; i >= 0; i--)
            {
                if (i == 0 && Scores[i].Scores < score.Scores)
                {
                    AddScoreToScores(score, i);
                    return;
                }
                else if (i != countScores - 1 && Scores[i].Scores >= score.Scores)
                {
                    AddScoreToScores(score, i + 1);
                    return;
                }
            }
        }

        private void AddScoreToScores(Score score, int index)
        {
                int countScores = Scores.Count;
                for (int i = countScores - 1; i > index; i--)
                    Scores[i] = Scores[i - 1];

                if (Scores[index].Scores != score.Scores)
                    Scores[index] = score;
        }
    }
}
