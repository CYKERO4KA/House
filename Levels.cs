namespace House
{
    internal class Levels
    {
        private bool _key1;
        private bool _key2;
        private bool _hummer;
        private bool _katana;
        private bool _bathroom;
        private bool _bedSideTable;
        private bool _washingMachine;
        private bool _fridge;
        private bool _blockedDoor;
        private bool _maniac = true;

        private readonly Random _random = new();
        private readonly Game _game = new();
        private static void Room(string art, string[] options, IReadOnlyList<Action> rooms)
        {
            var buttons = new Buttons(art, options);
            var selectedIndex = buttons.ButtonAction();
            rooms[selectedIndex]();
        }
        public void LevelsRun() => Room(Text.Instruction(), new[] { "Continue" }, new Action[] { Prologue });

        private void Prologue()
        {
            Console.Clear();
            Console.WriteLine(Text.Prologue1());
            Console.ReadLine();
            Console.WriteLine(Text.Prologue2());
            Console.ReadLine();
            Console.WriteLine(Text.Prologue3());
            Console.ReadLine();
            Console.WriteLine(Text.Prologue4());
            Console.ReadLine();
            Console.WriteLine(Text.Prologue5());
            Console.ReadLine();
            Console.WriteLine(Text.Prologue6());
            Console.ReadLine();
            Console.WriteLine(Text.Prologue7());
            Console.ReadLine();
            Console.WriteLine(Text.Prologue8());
            Console.ReadLine();
            Console.Clear();
            Console.WriteLine(Text.Prologue9());
            Console.ReadLine();
            Console.WriteLine(Text.Prologue10());
            Console.ReadLine();
            Console.WriteLine(Text.Prologue11());
            Console.ReadLine();
            Celling();
        } 
            
        private void Celling() => Room(Graphics.CellingArt(), new [] { "Go to the corridor" }, new Action[] { CellingToCorridorDoor });
        private void Corridor() => Room(Graphics.CorridorArt(), new [] { "Go to the bathroom", "Go to the baby's room", "Go to the bedroom", "Go to the celling" }, new Action[] { CorridorToBathroomDoor, CorridorToBabyRoomDoor, CorridorToBedroomDoor, CorridorToCellingDoor });
        private void Bathroom() => Room(Graphics.BathroomArt(), new [] { "Go to the pantry", "Go to the corridor", "Loot the washing machine" }, new Action[] { BathroomToPantryDoor, BathroomToCorridorDoor, ActionWashingMachine });
        private void BabyRoom() => Room(Graphics.BabyRoomArt(), new [] { "Go to the kitchen" , "Go to the corridor" , "Loot the shelf"}, new Action[] { BabyRoomToKitchenDoor, BabyRoomToCorridorDoor, ActionShelf });
        private void Bedroom() => Room(Graphics.BedroomArt(), new [] { "Go to the corridor", "Go to the pantry" ,"Loot the bedside table", "Loot the shelf"}, new Action[] { BedroomToCorridorDoor, BedroomToPantryDoor, BedSideTableAction, ActionBigShelf });
        private void Kitchen() => Room(Graphics.KitchenArt(), new [] { "Go to the hall" , "Go to the baby's room" ,"Loot the fridge", "Loot the kitchen shelf"}, new Action[] { KitchenToHallDoor, KitchenToBabyRoomDoor, ActionFridge, ActionKitchenShelf });
        private void Pantry() => Room(Graphics.PantryArt(), new [] { "Go to the bathroom", "Go to the bedroom", "Loot the shelf" }, new Action[] { PantryToBathroomDoor, PantryToBedroomDoor, ActionPantryShelf });
        private void Hall() => Room(Graphics.HallArt(), new [] { "Go to the exit", "Go to the kitchen", "Check dark angle" }, new Action[] { HallToExitDoor, HallToKitchenDoor, Maniac });

        private void Exit()
        {
            Console.Clear();
            Console.WriteLine(Text.Ending1());
            Console.ReadLine();
            Console.WriteLine(Text.Ending2());
            Console.ReadLine();
            Console.WriteLine(Text.Ending3());
            Console.ReadLine();
            Console.Clear();
            Console.WriteLine(Text.Ending4());
            Console.ReadLine();
            _game.RunMainMenu();

        }

        //--------------------------------DOORS-Celling--------------------------------
        private void CellingToCorridorDoor() => Room(Graphics.DoorArt(), new [] { "Enter", "Back" }, new Action[] { Corridor, Celling  });
        //--------------------------------DOORS-Corridor--------------------------------
        private void CorridorToCellingDoor() => Room(Graphics.DoorArt(), new [] { "Enter", "Back" }, new Action[] { Celling, Corridor });
        private void CorridorToBedroomDoor() => Room(Graphics.DoorArt(), new [] { "Enter", "Back" }, new Action[] { Bedroom, Corridor });
        private void CorridorToBabyRoomDoor()
        {
            switch (_key1)
            {
                case true:
                    Room(Graphics.LockedDoorArt(), new [] { "Enter", "Back" }, new Action[] { BabyRoom, Corridor });
                    break;
                case false:
                    Room(Graphics.LockedDoorArt(), new [] { "Enter", "Back" }, new Action[] {ShowKeyText1, Corridor });
                    break;
            }
        }
        private void CorridorToBathroomDoor()
        {
            switch (_bathroom)
            {
                case true:
                    Room(Graphics.DoorArt(), new [] { "Enter", "Back" }, new Action[] { Bathroom, Corridor });
                    break;
                case false:
                    Room(Graphics.DoorArt(), new [] { "Enter", "Back" }, new Action[] { ShowBlockedText2, Corridor });
                    break;
            }
        }
        //--------------------------------DOORS-Bedroom--------------------------------
        private void BedroomToPantryDoor()
        {
            switch (_hummer)
            {
                case true:
                    switch (_blockedDoor)
                    {
                        case true:
                            Room(Graphics.DoorArt(), new [] { "Enter", "Back" }, new Action[] { Pantry, Bedroom });
                            break;
                        case false:
                            _blockedDoor = true;
                            Room(Graphics.BlockedDoorArt(), new [] { "Enter", "Back" }, new Action[] { Pantry, Bedroom });
                            break;
                    }

                    break;
                case false:
                    Room(Graphics.BlockedDoorArt(), new [] { "Enter", "Back" }, new Action[] {ShowBlockedText1, Bedroom });
                    break;
            }
        }
        private void BedroomToCorridorDoor() => Room(Graphics.DoorArt(), new [] { "Enter", "Back" }, new Action[] { Corridor, Bedroom });
        
        //--------------------------------DOORS-Bathroom--------------------------------
        private void BathroomToCorridorDoor()
        {
            switch (_bathroom)
            {
                case true:
                    Room(Graphics.DoorArt(), new [] { "Enter", "Back" }, new Action[] { Corridor, Bathroom });
                    break;
                case false:
                    _bathroom = true;
                    Room(Graphics.DoorArt(), new [] { "Enter", "Back" }, new Action[] { ShowUnBlockedText, Bathroom });
                    break;
            }
        }
        private void BathroomToPantryDoor() => Room(Graphics.DoorArt(), new [] { "Enter", "Back" }, new Action[] { Pantry ,Bathroom });
        //--------------------------------DOORS-Pantry--------------------------------
        private void PantryToBathroomDoor() => Room(Graphics.DoorArt(), new [] { "Enter", "Back" }, new Action[] { Bathroom, Pantry });
        private void PantryToBedroomDoor() => Room(Graphics.DoorArt(), new [] { "Enter", "Back" }, new Action[] { Bedroom, Pantry });
        //--------------------------------DOORS-BabyRoom--------------------------------
        private void BabyRoomToCorridorDoor() => Room(Graphics.LockedDoorArt(), new [] { "Enter", "Back" }, new Action[] { Corridor, BabyRoom });
        private void BabyRoomToKitchenDoor() => Room(Graphics.DoorArt(), new [] { "Enter", "Back" }, new Action[] { Kitchen, BabyRoom });
        //--------------------------------DOORS-Kitchen--------------------------------
        private void KitchenToBabyRoomDoor() => Room(Graphics.DoorArt(), new [] { "Enter", "Back" }, new Action[] { BabyRoom, Kitchen });
        private void KitchenToHallDoor() => Room(Graphics.DoorArt(), new [] { "Enter", "Back" }, new Action[] { CodeCalculator, Kitchen });
        //--------------------------------DOORS-Hall--------------------------------
        private void HallToKitchenDoor() => Room(Graphics.DoorArt(), new [] { "Enter", "Back" }, new Action[] { Kitchen, Hall });
        private void HallToExitDoor()
        {
            switch (_key2)
            {
                case true:
                    Room(Graphics.LockedDoorArt(), new [] { "Enter", "Back" }, new Action[] { Exit, Hall });
                    break;
                case false:
                    Room(Graphics.LockedDoorArt(), new [] { "Enter", "Back" }, new Action[] { ShowKeyText2, Hall });
                    break;
            }
        }
        //--------------------------------Keys--------------------------------
        private void Key1()
        {
            switch (_key1)
            {
                case true:
                    Bedroom();
                    break;
                case false:
                    _key1 = true;
                    Room(Graphics.Key1Art(), new [] { "Continue" }, new Action[] { Bedroom });
                    break;
            }
        }
        private void Hummer()
        {
            switch (_hummer)
            {
                case true:
                    Kitchen();
                    break;
                case false:
                    _hummer = true;
                    Room(Graphics.HummerArt(), new [] { "Continue" }, new Action[] { Kitchen });
                    break;
            }
        }
        private void Katana()
        {
            switch (_katana)
            {
                case true:
                    Bathroom();
                    break;
                case false:
                    _katana = true;
                    Room(Graphics.KatanaArt(), new [] { "Continue" }, new Action[] { Bathroom });
                    break;
            }
        }
        private void Maniac()
        {
            switch (_maniac)
            {
                case true:
                    switch (_katana)
                    {
                        case true:
                            _key2 = true;
                            _maniac = false;
                            Room(Graphics.ManiacArt(), new[] { "Kill the maniac" }, new Action[] { ShowKey2 });
                            break;
                        case false:
                            Room(Graphics.ManiacArt(), new [] { "You died" }, new Action[] { _game.RunMainMenu });
                            break;
                    }

                    break;
                case false:
                    Room(Text.ManiacText(), new [] { "Continue" }, new Action[] { Hall });
                    break;
            }
        }
        private void BedSideTableAction()
        {
            switch (_bedSideTable)
            {
                case true:
                    Room(Graphics.BedSideTableArt(), new [] { "Loot", "Back" }, new Action[] { ShowLootingText1, Bedroom });
                    break;
                case false:
                    _bedSideTable = true;
                    Room(Graphics.BedSideTableArt(), new [] { "Loot", "Back" }, new Action[] { Key1, Bedroom });
                    break;
            }
        }
        private void ActionWashingMachine()
        {
            switch (_washingMachine)
            {
                case true:
                    Room(Graphics.WashingMachineArt(), new [] { "Loot", "Back" }, new Action[] { ShowLootingText2, Bathroom });
                    break;
                case false:
                    _washingMachine = true;
                    Room(Graphics.WashingMachineArt(), new [] { "Loot", "Back" }, new Action[] { Katana, Bathroom });
                    break;
            }
        }
        private void ActionFridge()
        {
            switch (_fridge)
            {
                case true:
                    Room(Graphics.FridgeArt(), new [] { "Loot", "Back" }, new Action[] {  ShowLootingText3, Kitchen });
                    break;
                case false:
                    _fridge = true;
                    Room(Graphics.FridgeArt(), new [] { "Loot", "Back" }, new Action[] { Hummer, Kitchen });
                    break;
            }
        }
        private void ActionShelf()
        {
            Room(Graphics.ShelfArt(), new [] { "Loot", "Back" }, new Action[] {  ShowLootingText4, BabyRoom });
        }
        private void ActionBigShelf()
        {
            Room(Graphics.BigShelfArt(), new [] { "Loot", "Back" }, new Action[] {  ShowLootingText5, Bedroom });
        }
        private void ActionKitchenShelf()
        {
            Room(Graphics.KitchenShelfArt(), new [] { "Loot", "Back" }, new Action[] {  ShowLootingText6, Kitchen });
        }
        private void ActionPantryShelf()
        {
            Room(Graphics.PantryShelfArt(), new [] { "Loot", "Back" }, new Action[] {  ShowLootingText7, Pantry });
        }
        //--------------------------------Show--------------------------------
        private void ShowKeyText1() => Room(Text.KeyFalse(), new [] { "Back" }, new Action[] { CorridorToBabyRoomDoor });
        private void ShowKeyText2() => Room(Text.KeyFalse(), new [] { "Back" }, new Action[] { HallToExitDoor});
        private void ShowBlockedText1() => Room(Text.Blocked(), new [] { "Back" }, new Action[] { BedroomToPantryDoor});
        private void ShowBlockedText2() => Room(Text.BlockedBathroom(), new [] { "Back" }, new Action[] { CorridorToBathroomDoor});
        private void ShowUnBlockedText() => Room(Text.UnBlocked(), new [] { "Continue" }, new Action[] { BathroomToCorridorDoor});
        private void ShowKey2() => Room(Graphics.Key2Art(), new [] { "Continue" }, new Action[] {Hall});
        private void ShowLootingText1() => Room(Text.EmptyText(), new [] { "Continue" }, new Action[] { BedSideTableAction });
        private void ShowLootingText2() => Room(Text.EmptyText(), new [] { "Continue" }, new Action[] { ActionWashingMachine });
        private void ShowLootingText3() => Room(Text.EmptyText(), new [] { "Continue" }, new Action[] { ActionFridge });
        private void ShowLootingText4() => Room(Text.EmptyText(), new [] { "Continue" }, new Action[] { ActionShelf });
        private void ShowLootingText5() => Room(Text.EmptyText(), new [] { "Continue" }, new Action[] { ActionBigShelf });
        private void ShowLootingText6() => Room(Text.EmptyText(), new [] { "Continue" }, new Action[] { ActionKitchenShelf });
        private void ShowLootingText7() => Room(Text.EmptyText(), new [] { "Continue" }, new Action[] { ActionPantryShelf });
        //--------------------------------Code--------------------------------
        private void CodeCalculator()
        {
            try
            {
                
                Console.Clear();
                Console.WriteLine("                                          YOU NEED TO SOLVE THESE EXAMPLES:");
                Console.WriteLine("                       ===========================================================================");
                const string accession = "                                                    YOU CAN ENTER:";
                for (var i = 0; i < 3; i++)
                {
                    var firstNumber = _random.Next(10);
                    var secondNumber = _random.Next(10);
                    Console.Write($"\t\t\t\t\t\t\t{firstNumber} * {secondNumber} = ");
                    var inputResult = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
                    var randomResult = firstNumber * secondNumber;
                    if (randomResult == inputResult)
                    {
                        const string correct = "                                                         Correct\n";
                        Console.WriteLine(correct);
                    }
                    else if (randomResult != inputResult)
                    {
                        switch (_maniac)
                        {
                            case true:
                                Console.Clear();
                                Console.WriteLine("                                                        Mistake");
                                Console.ReadLine();
                                Console.WriteLine("                               Unfortunately for you maniac dont forget these stupid mistakes");
                                Console.ReadLine();
                                Maniac();
                                break;
                            case false:
                                Console.Clear();
                                Console.WriteLine("                                                        Mistake");
                                Console.ReadLine();
                                Console.WriteLine("                                          Fortunately for you maniac is dead!");
                                Console.ReadLine();
                                Room("", new [] { "Try again", "Back" }, new Action[] { CodeCalculator, Kitchen });
                                break;
                        }
                    }
                }
                Console.WriteLine("                       ===========================================================================");
                Console.ReadLine();
                Room(accession, new [] { "Enter", "Back" }, new Action[] { Hall, Kitchen });
            }
            catch
            {
                switch (_maniac)
                {
                    case true:
                        Console.Clear();
                        Console.WriteLine("                                      Are you stupid? Write numbers, not letters!");
                        Console.ReadLine();
                        Console.WriteLine("                               Unfortunately for you maniac dont forget these stupid mistakes");
                        Console.ReadLine();
                        Maniac();
                        break;
                    case false:
                        Console.Clear();
                        Console.WriteLine("                                      Are you stupid? Write numbers, not letters!");
                        Console.ReadLine();
                        Console.WriteLine("                                          Fortunately for you maniac is dead!");
                        Console.ReadLine();
                        Room("", new [] { "Try again", "Back" }, new Action[] { CodeCalculator, Kitchen });
                        break;
                }
            }
            
        }
    }
}