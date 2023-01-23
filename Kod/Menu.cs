
namespace Memoji
{
    class Menu
    {
        //https://www.youtube.com/watch?v=qAWhGEPMlS8&t=928s&ab_channel=MichaelHadley
        private int SelectedIndex;
        private string[] Options;
        private string[] Prompts;

        public Menu(string[] prompts , string[] options)
        {
            Prompts = prompts;
            Options = options;
            SelectedIndex = 0;
        }

        private void DisplayOptions()
        {
            Console.CursorVisible=false;
            foreach (var prompt in Prompts)
            {
                Tools.CenterWrite(prompt);
            }
            
            for (int i = 0; i < Options.Length; i++)
            {
                string currentOption = Options[i];

                if (i == SelectedIndex)
                {
                    Console.ForegroundColor = Theme.Background;
                    Console.BackgroundColor = Theme.Foreground;
                }
                else
                {
                    Console.ForegroundColor = Theme.Foreground;
                    Console.BackgroundColor = Theme.Background;

                }
                Tools.CenterWrite($" << {currentOption} >> ");
            }
            Console.ForegroundColor = Theme.Foreground;
            Console.BackgroundColor = Theme.Background;
            
        }

        public int Run()
        {
            ConsoleKey keyPressed;
            do
            {
                Console.Clear();
                DisplayOptions();

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                keyPressed = keyInfo.Key;
                
                if (keyPressed == ConsoleKey.UpArrow)
                    SelectedIndex--;
                if (SelectedIndex == -1)
                    SelectedIndex = Options.Length - 1;
                else if (keyPressed == ConsoleKey.DownArrow)
                    SelectedIndex++;
                if (SelectedIndex == Options.Length)
                    SelectedIndex = 0;


            }
            while (keyPressed != ConsoleKey.Enter);
            return SelectedIndex;
        }
    }
}