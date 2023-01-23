namespace Memoji
{
    static class Tools 
    {
       
        public static string ReadString()
        {
            string output="";
            bool validinput = false;
            while (!validinput)
            {
                string? input = Console.ReadLine();
                if (input is not null)
                {
                    validinput = ValidateString(input);
                    output = input;
                    
                }
            }

            return output;
        }
        static bool ValidateString(string input)
        {
            if (ContainsLetters(input)) //Kollar om strängen innehåller någon bokstav
                if(IsShortEnough(input))//Kollar om strängen är kortare än 12 bokstäver
                    return true;
            return false;

        }
        static bool ContainsLetters(string input)
        {
            if (!input.Any(x => char.IsLetter(x)))
            {
                System.Console.WriteLine("Name has to contain at least one letter");
                return false;
            }
            else
                return true;
            
        }
        static bool IsShortEnough(string input){
            if(input.Length<12){
                return true;
            }else{
                System.Console.WriteLine("Name must be shorter than 12 characters.");
                return false;
            }
        }
        ///////////////////////////////////////////////////////
        public static bool ValidateCoord(string input, (ICard, string)[] deck)
        {
            if (IsCoord(input, deck))
                if (IsValidCoord(input, deck))
                    if (IsCardAlreadyFlipped(input, deck))
                        return true;
            return false;

        }
        static bool IsCoord(string input, (ICard, string)[] deck)//Checks if string == Coord.
        {
            for (int i = 0; i < deck.Length; i++)
            {
                if (deck[i].Item2 == input)
                    return true;
            }
            System.Console.WriteLine($"'{input}' is not a coordinate. ");
            return false;
        }
        static bool IsValidCoord(string input, (ICard, string)[] deck)//Checks if string == valid coord(a coord that has NOT been removed)
        {
            for (int i = 0; i < deck.Length; i++)
            {
                if (input == deck[i].Item2)
                {
                    if (deck[i].Item1.Removed == false)
                        return true;
                    else
                        System.Console.WriteLine($"'{input}' can not be picked, because it has been removed.");
                }
            }
            return false;

        }
        static bool IsCardAlreadyFlipped(string input, (ICard, string)[] deck)// Checks if valid Card/coord already has been Flipped
        {
            for (int i = 0; i < deck.Length; i++)
            {
                if (input == deck[i].Item2)
                {
                    if (deck[i].Item1.Flipped == false)
                        return true;
                    else
                        System.Console.WriteLine($"'{input}' can not be picked, because it's already flipped. You have to pick two different cards.");
                }
            }
            return false;

        }
        ///////////////////////////////////////////////////////
        public static void CenterWrite(string input)
        {
            Console.SetCursorPosition((Console.WindowWidth - input.Length) / 2, Console.CursorTop);
            Console.WriteLine($"{input}");
            //https://www.w3schools.blog/c-center-text
        }
       
    }
}