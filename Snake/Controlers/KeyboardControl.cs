using System;
using System.Threading.Tasks;

namespace Snake.Controlers
{
    public class KeyboardControl
    {
        public Action<char> KeyInputEvent { get; set; }
        public Action<bool> KeyboardCloseEvent { get; set; }

        private bool isRunning { get; set; } = false;
        private Task LoopTask { get; set; }

        public void Start()
        {
            if(!isRunning)
            {
                isRunning = true;
                LoopTask = new Task(Loop);
                LoopTask.Start();
            }
        }

        private void Loop()
        {
            do
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                KeyInputEvent?.Invoke(keyInfo.KeyChar);
            } while (isRunning);
            KeyboardCloseEvent?.Invoke(true);
        }

        public void Close()
        {
            isRunning = false;
            LoopTask.Wait();
            LoopTask.Dispose();
        }
    }
}
