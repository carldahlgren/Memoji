namespace Memoji
{
    class Coordinates
    {
        public (int, int) Grid;
        public string[] CoordArr { get; private set; }

        public Coordinates(int difficulty)
        {
            this.Grid = GridCalc(difficulty);
            CoordArr = CoordMapper(Grid);

        }

        (int, int) GridCalc(int difficulty)
        {
            (int, int) output;
            return output = difficulty switch
            {
                //Easy/Medium/Hard/Default
                0 => (2, 2),
                1 => (4, 2),
                2 => (4, 3),
                _ => (0, 0)
            };

        }
        string[] CoordMapper((int, int) grid)
        {

            (int, int)[] output = new (int, int)[grid.Item1 * grid.Item2];
            int x = 0;
            for (int i = 0; i < grid.Item2; i++)
            {
                for (int j = 1; j <= grid.Item1; j++)
                {
                    output[x].Item1 = i;
                    output[x].Item2 = j;
                    x++;
                };
            };

            return LetterSub(output);

        }
        string[] LetterSub((int, int)[] input)
        {
            string[] output = new string[input.Length];
            char[] alph = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

            for (int j = 0; j < output.Length; j++)
            {
                for (int i = 0; i < alph.Length; i++)
                {
                    if (input[j].Item1 == i)
                        output[j] = $"{alph[i]}{input[j].Item2}";
                }
            }
            return output;
        }
    }

}