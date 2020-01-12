using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.Game.Menu
{
    public class OptionChoose
    {
        public int Option {
            get => option;
            set
            {
                option = value;
                if (option < 0)
                    option = CountOption - 1;
                else if (option >= CountOption)
                    option = 0;
            }
        }
        public int CountOption { get; set; }

        private int option = 0;

        public OptionChoose()
        {

        }
        public OptionChoose(int option, int countOption)
        {
            Option = option;
            this.option = option;
            CountOption = countOption;
        }
    }
}
