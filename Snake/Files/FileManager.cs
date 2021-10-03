using Snake.Configurations;
using Snake.Files.Json;
using Snake.Game;
using System.Collections.Generic;
using System.IO;

namespace Snake.Files
{
    public class FileManager
    {
        public void NewScore(Score score)
        {
            JsonManager json = new JsonManager();
            ScoresFile scoresFile = json.Read<ScoresFile>(GameConfig.ScoresFile);
            if (scoresFile != null)
            {
                scoresFile.AddScores(score);
                json.Write(GameConfig.ScoresFile, scoresFile);
            }
        }

        public ScoresFile GetScores()
        {
            JsonManager json = new JsonManager();
            return json.Read<ScoresFile>(GameConfig.ScoresFile);
        }

        public MapFile[] GetMaps()
        {
            if (!ExistsMapsDirectory())
                return null;

            JsonManager jsonManager = new JsonManager();
            List<MapFile> maps = new List<MapFile>();
            string[] jsonFiles = jsonManager.GetJsonFiles(GameConfig.PathMaps);
            foreach (string file in jsonFiles)
            {
                MapFile map = jsonManager.Read<MapFile>(file);
                if (map != null)
                    maps.Add(map);
            }

            return maps.ToArray();
        }

        public bool ExistsScoresFile(bool create = false)
        {
            JsonManager jManager = new JsonManager();
            bool exists = jManager.FileExists(GameConfig.ScoresFile);
            if (create && !exists)
                jManager.Write(GameConfig.ScoresFile, new ScoresFile(new List<Score>()));
            return exists;
        }

        public bool ExistsMapsDirectory(bool create = false)
        {
            bool exists = Directory.Exists(GameConfig.PathMaps);
            if (create && !exists)
                Directory.CreateDirectory(GameConfig.PathMaps);
            return exists;
        }
    }
}
