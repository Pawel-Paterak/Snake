using Snake.Configurations;
using Snake.Game.Render;
using System;
using System.Threading.Tasks;

namespace Snake.Controlers
{
    public class KeyboardControl
    {
        public static Action<ConsoleKey> PressKeyEvent { get; set; }
        public static Action KeyboardCloseEvent { get; set; }

        private bool WaitForKey { get; set; } = false;
        private bool IsRunning { get; set; } = false;
        private Task LoopTask { get; set; }

        public void Start()
        {
            if(!IsRunning)
            {
                IsRunning = true;
                LoopTask = new Task(Loop);
                LoopTask.Start();
            }
        }
        public void Close()
        {
            IsRunning = false;

            ConsoleRender render = new ConsoleRender();
            ConsoleConfig config = new ConsoleConfig();
            string text = "Press key to close";
            int offsetText = text.Length / 2;
            render.Clear();
            render.Write(text, config.Widht / 2 - offsetText, config.Height / 2);

            LoopTask.Wait(-1);
            LoopTask.Dispose();
        }

        private void Loop()
        {
            do
            {
                WaitForKey = true;
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                PressKeyEvent?.Invoke(keyInfo.Key);
                WaitForKey = false;
            } while (IsRunning);
            KeyboardCloseEvent?.Invoke();
        }
    }
}
