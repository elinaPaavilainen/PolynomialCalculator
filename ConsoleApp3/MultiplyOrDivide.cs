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

                double resultInt = Convert.ToDouble(first) * Convert.ToDouble(second);

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

                    double resultInt = Convert.ToDouble(firstNumbers) * Convert.ToDouble(secondNumbers);

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
                    foreach (var letter in firstLetters)
                    {
                        if (secondLetters.Contains(letter))
                        {
                            var powerLetter = letter + "^" + "2";
                            secondLetters = secondLetters.Replace(Convert.ToString(letter), powerLetter);
                            firstLetters = firstLetters.Replace(Convert.ToString(letter), "");
                        }
                    }


                    if (powerFirst == 0 && powerSecond == 0)
                    {
                        double result = Convert.ToDouble(firstNumbers) * Convert.ToDouble(secondNumbers);
                        string finalResult = result.ToString() + firstLetters + secondLetters;
                        if (result > 0)
                        {
                            finalResult = "+" + finalResult;
                        }
                        return finalResult;
                    }
                    else
                    {
                        first = first + "^" + Convert.ToString(powerFirst);
                        second = second + "^" + Convert.ToString(powerSecond);
                        return $"{operate}{first} * {second}";
                    }
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

                if (firstLetters == secondLetters)
                {
                    firstNumbers = operate + firstNumbers;

                    double result = Convert.ToDouble(firstNumbers) / Convert.ToDouble(secondNumbers);
                    string resultNumber = Convert.ToString(result);
                    if (result >= 0)
                    {
                        resultNumber = "+" + resultNumber;
                    }
                    string finalResult = resultNumber;

                    return finalResult;
                }
                else if (secondLetters == "" && secondNumbers == "1")
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

                    double result = Convert.ToDouble(firstNumbers) / Convert.ToDouble(secondNumbers);
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

                    double result = Convert.ToDouble(firstNumbers) / Convert.ToDouble(secondNumbers);
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