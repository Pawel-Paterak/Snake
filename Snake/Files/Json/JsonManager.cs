using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Snake.Game.Render;

namespace Snake.Files.Json
{
    class JsonManager
    {
        public void Write(string path, object obj)
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.Converters.Add(new JavaScriptDateTimeConverter());
            serializer.NullValueHandling = NullValueHandling.Ignore;
            try
            {
                using (StreamWriter sw = new StreamWriter(path))
                using (JsonWriter writer = new JsonTextWriter(sw))
                    serializer.Serialize(writer, obj);
            }
            catch (Exception e)
            {
                ConsoleRender render = new ConsoleRender();
                render.Write(e.Message, 0, 0);
                Console.ReadKey();
            }
        }

        public T Read<T>(string path)
        {
            T temp = default(T);
            JsonSerializer serializer = new JsonSerializer();
            serializer.Converters.Add(new JavaScriptDateTimeConverter());
            serializer.NullValueHandling = NullValueHandling.Ignore;

            try
            {
                using (StreamReader sw = new StreamReader(path))
                using (JsonReader reader = new JsonTextReader(sw))
                    temp = (T)serializer.Deserialize(reader, typeof(T));
            }
            catch(Exception e)
            {
                ConsoleRender render = new ConsoleRender();
                render.Write(e.Message, 0, 0);
                Console.ReadKey();
            }
            return temp;
        }

        public bool Exists(string path)
         => File.Exists(path);
    }
}
