namespace Calculator2
{
    internal class CheckLettersAndNumbers
    {
        public static string CheckNumbers(string term)
        {
            var numbers = "";

            foreach (char character in term)
            {
                if (char.IsDigit(character))
                {
                    numbers += character;
                }
                else
                {
                    break;
                }
            }

            if (numbers == "")
            {
                return "1";
            }
            return numbers;
        }
        public static string CheckLetters(string term)
        {
            var letters = "";

            foreach (char character in term)
            {
                if (char.IsLetter(character))
                {
                    letters += character;
                }
            }
            string sortedLetters = new(letters.OrderBy(c => c).ToArray());
            return sortedLetters;
        }
    }
}
