using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
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
                var powerFirst = 0;
                var powerSecond = 0;
                if (first.Contains("^"))
                {
                    var firstBefore = first;
                    first = firstBefore.Substring(0, firstBefore.IndexOf("^"));
                    powerFirst = Convert.ToInt32(firstBefore.Substring(firstBefore.IndexOf("^") + 1));
                }

                if (second.Contains("^"))
                {
                    var secondBefore = second;
                    second = secondBefore.Substring(0, secondBefore.IndexOf("^"));
                    powerSecond = Convert.ToInt32(secondBefore.Substring(secondBefore.IndexOf("^") + 1));
                }

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

                    if (firstLetters != "" && secondLetters != "")
                    {
                        if (powerFirst == 0 && powerSecond == 0)
                        {
                            string finalResult = resultNumber + firstLetters + "^2";
                            return finalResult;
                        }
                        else
                        {
                            var power = powerFirst + powerSecond + 1;
                            string finalResult = resultNumber + firstLetters + "^" + Convert.ToString(power);
                            return finalResult;
                        }
                    }

                    if (firstLetters != "")
                    {
                        if (powerFirst > 0)
                        {
                            string finalResult = resultNumber + firstLetters + "^" + Convert.ToString(powerFirst);
                            return finalResult;
                        }
                        else
                        {
                            string finalResult = resultNumber + firstLetters;
                            return finalResult;
                        }
                    }
                    else if (secondLetters != "")
                    {
                        if (powerSecond > 0)
                        {
                            string finalResult = resultNumber + secondLetters + "^" + Convert.ToString(powerSecond);
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
                        string finalResult = resultNumber + secondLetters;
                        return finalResult;
                    }
                }
                else
                {
                    if(powerFirst > 0)
                    {
                        first = first + "^" + Convert.ToString(powerFirst);
                    }
                    if (powerSecond > 0)
                    {
                        second = second + "^" + Convert.ToString(powerSecond);
                    }
                    return $"{operate}{first} * {second}";
                }

            }
        }

        public static string Divide(string operate, string first, string second)
        {
            bool firstContainsLetters = first.Any(char.IsLetter);
            bool secondContainsLetters = second.Any(char.IsLetter);

            if (firstContainsLetters == false && secondContainsLetters == false)
            {
                first = operate + first;

                double resultDouble = Convert.ToDouble(first) / Convert.ToDouble(second);
                string result = Convert.ToString(resultDouble);

                if (resultDouble >= 0)
                {
                    result = "+" + result;
                }
                return result;

            }
            else
            {
                // Tätä pitää muokata niin että osaa laskea esim. 1/x !!!!

                var firstNumbers = CheckLettersAndNumbers.CheckNumbers(first);
                var firstLetters = CheckLettersAndNumbers.CheckLetters(first);

                var secondNumbers = CheckLettersAndNumbers.CheckNumbers(second);
                var secondLetters = CheckLettersAndNumbers.CheckLetters(second);

                if (firstNumbers == "")
                {
                    firstNumbers = "1";
                }

                if (secondNumbers == "")
                {
                    secondNumbers = "1";
                }

                //jaa tämä eri osiin
                if (firstLetters == secondLetters)// || firstLetters == "" || secondLetters == "")
                {
                    firstNumbers = operate + firstNumbers;

                    double result = Convert.ToDouble(firstNumbers) / Convert.ToDouble(secondNumbers);
                    string resultNumber = Convert.ToString(result);
                    if (result >= 0)
                    {
                        resultNumber = "+" + resultNumber;
                    }
                    string finalResult = resultNumber;

                    //if (firstLetters != "" && secondLetters == "")
                    //{
                    //    finalResult = resultNumber + firstLetters;
                    //}

                    //else if (firstLetters == "" && secondLetters != "")
                    //{
                    //    finalResult = resultNumber + secondLetters;
                    //}

                    //else if (firstLetters != "" && secondLetters != "")
                    //{
                    //    finalResult = resultNumber;
                    //}
                    return finalResult;
                }
                else if(secondLetters == "" && secondNumbers == "1") 
                {
                    string finalResult = firstNumbers + firstLetters;
                    if (Convert.ToDouble(firstNumbers) >= 0)
                    {
                        finalResult = "+" + finalResult;
                    }

                    return finalResult;

                }
                else if (firstLetters.Contains(secondLetters))
                {
                    firstNumbers = operate + firstNumbers;

                    int result = Convert.ToInt32(firstNumbers) / Convert.ToInt32(secondNumbers);
                    string resultNumber = Convert.ToString(result);
                    if (result >= 0)
                    {
                        resultNumber = "+" + resultNumber;
                    }
                    string finalResult = "";

                    string finalLetters = firstLetters.Replace(secondLetters, "");

                    finalResult = resultNumber + finalLetters;
                    return finalResult;
                }
                else if (secondLetters.Contains(firstLetters))
                {
                    firstNumbers = operate + firstNumbers;

                    int result = Convert.ToInt32(firstNumbers) / Convert.ToInt32(secondNumbers);
                    string resultNumber = Convert.ToString(result);
                    if (result >= 0)
                    {
                        resultNumber = "+" + resultNumber;
                    }
                    string finalResult = "";

                    string finalLetters = secondLetters.Replace(firstLetters, "");

                    finalResult = resultNumber + finalLetters;
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