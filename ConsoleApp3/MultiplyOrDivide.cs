using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Calculator2
{
    public static class MultiplyOrDivide
    {
        public static string Multiply(string operate, string first, string second)
        {
            bool firstContainsLetters = first.Any(char.IsLetter);
            bool secondContainsLetters = second.Any(char.IsLetter);

            if (firstContainsLetters == false && secondContainsLetters == false)
            {

                first = operate + first;


                int resultInt = Convert.ToInt32(first) * Convert.ToInt32(second);

                string result = Convert.ToString(resultInt);
                if (resultInt >= 0)
                {
                    result = "+" + result;
                }

                return result;
            }


            else
            {
                var firstNumbers = CheckLettersAndNumbers.CheckNumbers(first);
                var firstLetters = CheckLettersAndNumbers.CheckLetters(first);

                var secondNumbers = CheckLettersAndNumbers.CheckNumbers(second);
                var secondLetters = CheckLettersAndNumbers.CheckLetters(second);

                if (firstLetters == secondLetters || firstLetters == "" || secondLetters == "")
                {

                    firstNumbers = operate + firstNumbers;

                    int resultInt = Convert.ToInt32(firstNumbers) * Convert.ToInt32(secondNumbers);

                    string resultNumber = Convert.ToString(resultInt);
                    if (resultInt >= 0)
                    {
                        resultNumber = "+" + resultNumber;
                    }

                    if (firstLetters != "")
                    {
                        string finalResult = resultNumber + firstLetters;
                        return finalResult;
                    }
                    else
                    {
                        string finalResult = resultNumber + secondLetters;
                        return finalResult;
                    }
                }
                else
                {
                    return $"{operate}{first} * {second}";
                }

            }
        }

        //MUOKKAA täMÄ samanlaiseksi kuin Multiplyssä!!!!
        public static string Divide(string operate, string first, string second)
        {
            bool firstContainsLetters = first.Any(char.IsLetter);
            bool secondContainsLetters = second.Any(char.IsLetter);

            if (firstContainsLetters == false && secondContainsLetters == false)
            {
                if (operate == "-")
                {
                    first = operate + first;
                }
                int result = Convert.ToInt32(first) / Convert.ToInt32(second);
                return Convert.ToString(result);
            }
            else
            {
                var firstNumbers = CheckLettersAndNumbers.CheckNumbers(first);
                var firstLetters = CheckLettersAndNumbers.CheckLetters(first);

                var secondNumbers = CheckLettersAndNumbers.CheckNumbers(second);
                var secondLetters = CheckLettersAndNumbers.CheckLetters(second);

                if (firstLetters == secondLetters)
                {
                    if (operate == "-")
                    {
                        firstNumbers = operate + firstNumbers;
                    }
                    int result = Convert.ToInt32(firstNumbers) / Convert.ToInt32(secondNumbers);
                    string resultNumber = Convert.ToString(result);
                    string finalResult = resultNumber + firstLetters;
                    return finalResult;
                }
                else
                {
                    return $"{operate}{first} / {second}";
                }

            }
        }
    }
}
