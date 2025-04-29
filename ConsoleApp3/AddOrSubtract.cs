using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator2
{
    public static class AddOrSubtract
    {
        public static string Add(string operate, string first, string second)
        { 

            if (first.Contains('^') || second.Contains('^'))
            {
                return $"{operate}{first} + {second}";
            }

            bool firstContainsLetters = first.Any(char.IsLetter);
            bool secondContainsLetters = second.Any(char.IsLetter);

            if (firstContainsLetters == false && secondContainsLetters == false)
            {
                if (operate == "-")
                {
                    first = operate + first;
                }
                double result = Convert.ToDouble(first) + Convert.ToDouble(second);
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
                    double result = Convert.ToDouble(firstNumbers) + Convert.ToDouble(secondNumbers);
                    string resultNumber = Convert.ToString(result);
                    string finalResult = resultNumber + firstLetters;
                    return finalResult;
                }
                else
                {
                    return $"{operate}{first} + {second}";
                }

            }
        }
        public static string Subtract(string operate, string first, string second)
        {
            if (first.Contains('^') || second.Contains('^'))
            {
                return $"{operate}{first} + {second}";
            }
            bool firstContainsLetters = first.Any(char.IsLetter);
            bool secondContainsLetters = second.Any(char.IsLetter);

            if (firstContainsLetters == false && secondContainsLetters == false)
            {
                if(operate == "-")
                {
                    first = operate + first;
                }
                double result = Convert.ToDouble(first) - Convert.ToDouble(second);
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
                    double result = Convert.ToDouble(firstNumbers) - Convert.ToDouble(secondNumbers);
                    string resultNumber = Convert.ToString(result);

                    if (result == 0)
                    {
                        string finalResult = "0";
                        return finalResult;
                    }
                    else
                    { 
                        string finalResult = resultNumber + firstLetters;
                        return finalResult;
                    }
                    
                }
                else
                {
                    return $"{operate}{first} - {second}";
                }

            }
        }
    }
}


