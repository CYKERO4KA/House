using static System.Console;

namespace House
{
    internal class Buttons
    {
        private int _selectedIndex;
        private readonly string[] _options;
        private readonly string _prompt;

        public Buttons(string prompt, string[] options)
        {
            _prompt = prompt;
            _options = options;
            _selectedIndex = 0;
        }
        private void ShowButtons()
        {
            WriteLine(_prompt);
            for (var i = 0; i < _options.Length; i++)
            {
                var currentOption = _options[i];
                char prefix;
                if (i == _selectedIndex)
                {
                    prefix = '*';
                    ForegroundColor = ConsoleColor.Cyan;
                }
                else
                {
                    prefix = ' ';
                    ForegroundColor = ConsoleColor.White;
                }
                WriteLine($"                                                   {prefix} << {currentOption} >>");
               
            }
            ResetColor();
        }
        public int ButtonAction()
        {
            ConsoleKey keyPressed;
            do
            {
                Clear();
                ShowButtons();
                var keyInfo = ReadKey(true);
                keyPressed = keyInfo.Key;

                switch (keyPressed)
                {
                    case ConsoleKey.UpArrow:
                    {
                        _selectedIndex--;
                        if (_selectedIndex == -1)
                        {
                            _selectedIndex = _options.Length - 1;
                        }
                        Beep();
                        break;
                    }
                    case ConsoleKey.DownArrow:
                    {
                        _selectedIndex++;
                        if (_selectedIndex == _options.Length)
                        {
                            _selectedIndex = 0;
                        }
                        Beep();
                        break;
                    }
                    case ConsoleKey.Escape:
                    {
                        Clear();
                        var game = new Game();
                        const string prompt1 = "                                                You want to leave?";
                        string[] options1 = { "Yes", "No" };
                        var buttons = new Buttons(prompt1, options1);
                        var selectedIndex = buttons.ButtonAction();
                        switch (selectedIndex)
                        {
                            case 0:
                                game.RunMainMenu();
                                break;
                            case 1:
                                ReadKey(true);
                                break;
                        }
                        Beep();
                        break;
                    }
                }
            } while (keyPressed != ConsoleKey.Enter);
            return _selectedIndex;
        }
    }
}
