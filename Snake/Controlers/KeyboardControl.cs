using Snake.Configurations;
using Snake.Extensions;
using Snake.Game.Render;
using System;
using System.Threading.Tasks;

namespace Snake.Controlers
{
    public class KeyboardControl
    {
        public static Action<ConsoleKey> PressKeyEvent { get; set; }
        public static Action KeyboardCloseEvent { get; set; }

        private bool isRunning;
        private Task loopTask;

        public void Start()
        {
            if(!isRunning)
            {
                isRunning = true;
                loopTask = new Task(Loop);
                loopTask.Start();
            }
        }

        public void Close()
        {
            isRunning = false;

            ConsoleRender render = new ConsoleRender();
            ConsoleConfig config = new ConsoleConfig();
            string text = "Press key to close";
            int offsetText = text.HalfLength();
            render.Clear();
            render.Write(text, config.CenterX - offsetText, config.CenterY);

            loopTask.Wait(-1);
            loopTask.Dispose();
        }

        private void Loop()
        {
            do
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                PressKeyEvent?.Invoke(keyInfo.Key);
            } while (isRunning);
            KeyboardCloseEvent?.Invoke();
        }
    }
}
