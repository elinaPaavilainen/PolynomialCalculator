using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            return letters;
        }
    }
}
