using static System.Console;

namespace House
{
    internal class Game
    {
        public void Start()
        {
            RunMainMenu();
        }
        public void RunMainMenu()
        {
            const string prompt = @"
                               ▄█    █▄     ▄██████▄  ███    █▄     ▄████████    ▄████████ 
                              ███    ███   ███    ███ ███    ███   ███    ███   ███    ███ 
                              ███    ███   ███    ███ ███    ███   ███    █▀    ███    █▀  
                             ▄███▄▄▄▄███▄▄ ███    ███ ███    ███   ███         ▄███▄▄▄     
                            ▀▀███▀▀▀▀███▀  ███    ███ ███    ███ ▀███████████ ▀▀███▀▀▀     
                              ███    ███   ███    ███ ███    ███          ███   ███    █▄  
                              ███    ███   ███    ███ ███    ███    ▄█    ███   ███    ███ 
                              ███    █▀     ▀██████▀  ████████▀   ▄████████▀    ██████████ 
";
            string[] options = { "Play", "Options", "Exit" };
            var mainMenu = new Menu(prompt, options);
            var selectedIndex = mainMenu.Run();

            switch (selectedIndex)
            {
                case 0:
                    GameRun();
                    break;
                case 1:
                    Option();
                    break;
                case 2:
                    ExitGame();
                    break;
            }
        }
        private void ExitGame()
        {
            Clear();
            const string prompt = "                                                You want to leave?";
            string[] options = { "Yes", "No" };
            var buttons = new Buttons(prompt, options);
            var selectedIndex = buttons.ButtonAction();
            switch (selectedIndex)
            {
                case 0:
                    Environment.Exit(0); 
                    break;
                case 1:
                    RunMainMenu();
                    break;
            }
        }
        private void Option()
        {
            Clear();
            WriteLine(Text.GameOptions());
            WriteLine("                                        Press any key to return... ");
            ReadKey(true);
            RunMainMenu();
        }
        private void GameRun()
        {
            Clear();
            var levels = new Levels();
            levels.LevelsRun();
            ReadKey(true);
            RunMainMenu();
        }
    }
}