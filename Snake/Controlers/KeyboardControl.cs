using System;
using System.Threading.Tasks;

namespace Snake.Controlers
{
    public static class KeyboardControl
    {
        public static Action<char> PressKeyEvent { get; set; }
        public static Action<bool> KeyboardCloseEvent { get; set; }

        private static bool isRunning { get; set; } = false;
        private static Task LoopTask { get; set; }

        public static void Start()
        {
            if(!isRunning)
            {
                isRunning = true;
                LoopTask = new Task(Loop);
                LoopTask.Start();
            }
        }

        private static void Loop()
        {
            do
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                PressKeyEvent?.Invoke(keyInfo.KeyChar);
            } while (isRunning);
            KeyboardCloseEvent?.Invoke(true);
        }

        public static void Close()
        {
            isRunning = false;
            LoopTask.Wait();
            LoopTask.Dispose();
        }
    }
}
