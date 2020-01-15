using Snake.Game;
using System.Collections.Generic;

namespace Snake.Files
{
    public class ScoresFile
    {
        public int MaxSlots { get; private set; } = 17;
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
            VeryficationLenghtScores();

            if (Scores[0].Scores < score.Scores)
                AddScoreToTable(score, 0);
            else
            {
                for (int i = MaxSlots - 2; i > 0; i--)
                {
                    if (Scores[i].Scores >= score.Scores)
                    {
                        AddScoreToTable(score, i + 1);
                        return;
                    }
                }
            }
        }

        private void VeryficationLenghtScores()
        {
            int missingScore = MaxSlots - Scores.Count;
            if (missingScore > 0)
            {
                for (int i = 0; i < missingScore; i++)
                    Scores.Add(new Score(0, ""));
            }
        }
        private void AddScoreToTable(Score score, int index)
        {
                int countScores = Scores.Count;
                for (int i = countScores - 1; i > index; i--)
                    Scores[i] = Scores[i - 1];
                Scores[index] = score;
        }
    }
}
