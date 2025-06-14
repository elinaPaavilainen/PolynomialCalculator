﻿using System.Text.RegularExpressions;

namespace Calculator2
{
    public static class ProcessAndOrganize
    {
        private static readonly char[] charsToCheck = { '*', '/', '+', '-' };

        public static string ProcessPolynomial(string polynomial)
        {
            polynomial = polynomial.Replace(" ", "");
            string newPolynomial = "";

            if (polynomial != "")
            {
                if (char.IsAsciiLetterOrDigit(polynomial[0]) && polynomial[0] != '+' && polynomial[0] != '-')
                {
                    polynomial = "+" + polynomial;
                }
            }

            int polynomialLength = polynomial.Length;

            for (int i = 0; i < polynomialLength; i++)
            {
                if (polynomial[i] == '*' || polynomial[i] == '/' || polynomial[i] == '+')
                {
                    if (polynomial[i + 1] == '-')
                    {
                        newPolynomial = newPolynomial + polynomial[i];
                    }
                    else
                    {
                        newPolynomial = newPolynomial + polynomial[i] + " ";
                    }
                }
                else if (polynomial[i] == '-')
                {
                    if (i > 0)
                    {
                        if (polynomial[i - 1] == '*' || polynomial[i - 1] == '/' || polynomial[i - 1] == '+' || polynomial[i - 1] == '-')
                        {
                            newPolynomial = newPolynomial + " " + polynomial[i];
                        }
                        else
                        {
                            newPolynomial = newPolynomial + " " + polynomial[i] + " ";
                        }
                    }
                    else
                    {
                        newPolynomial = newPolynomial + " " + polynomial[i] + " ";
                    }
                }

                else
                {
                    try
                    {
                        if (char.IsAsciiLetterOrDigit(polynomial[i]) && char.IsAsciiLetterOrDigit(polynomial[i + 1]))
                        {
                            newPolynomial = newPolynomial + polynomial[i];
                        }
                        else if (polynomial[i] == ',')
                        {
                            newPolynomial = newPolynomial + polynomial[i];
                        }
                        else if (char.IsAsciiDigit(polynomial[i]) && polynomial[i + 1] == ',')
                        {
                            newPolynomial = newPolynomial + polynomial[i];
                        }
                        else if (char.IsAsciiLetterOrDigit(polynomial[i]) && polynomial[i + 1] == '^')
                        {
                            newPolynomial = newPolynomial + polynomial[i];
                        }
                        else if (polynomial[i] == '^')
                        {
                            newPolynomial = newPolynomial + polynomial[i];
                        }
                        else
                        {
                            newPolynomial = newPolynomial + polynomial[i] + " ";
                        }
                    }
                    catch
                    {
                        newPolynomial = newPolynomial + polynomial[i] + " ";
                    }
                }
            }
            return newPolynomial.Trim();
        }

        public static string CheckLetters(string polynomial)
        {
            HashSet<char> uniqueLetters = new HashSet<char>();

            foreach (char c in polynomial)
            {
                if (char.IsLetter(c))
                {
                    uniqueLetters.Add(c);
                }
            }
            return string.Join(" ", uniqueLetters);
        }
        public static List<string> OrganizePolynomial(string polynomial, string differentLettersNotInOrder)
        {
            string differentLettersWithWhiteSpace = new string(differentLettersNotInOrder.OrderBy(c => c).ToArray());
            string differentLetters = differentLettersWithWhiteSpace.Replace(" ", "");


            string[] polynomialPartsArray = polynomial.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            List<string> polynomialParts = polynomialPartsArray.ToList();

            int polynomialPartsCount = polynomialParts.Count;

            List<string> withLettersParts = new List<string>();
            List<string> noLetterParts = new List<string>();
            List<string> leaveBeParts = new List<string>();

            foreach (var letterCharacter in differentLetters)
            {
                for (int i = 1; i < polynomialPartsCount; i++)
                {
                    if (i < polynomialPartsCount - 1)
                    {
                        if (polynomialParts[i - 1] != "*" && polynomialParts[i - 1] != "/" && polynomialParts[i + 1] != "*" && polynomialParts[i + 1] != "/")
                        {
                            if (polynomialParts[i].Contains(letterCharacter) && !polynomialParts[i].Contains('^'))
                            {
                                int letterCount = polynomialParts[i].Count(char.IsLetter);
                                if (letterCount > 1)
                                {
                                    withLettersParts.Add(polynomialParts[i - 1] + "Duplicate" + Convert.ToString(i));
                                    withLettersParts.Add(polynomialParts[i] + "Duplicate" + Convert.ToString(i));
                                }
                                else
                                {
                                    withLettersParts.Add(polynomialParts[i - 1]);
                                    withLettersParts.Add(polynomialParts[i]);
                                }

                            }
                        }
                    }
                    else
                    {
                        if (polynomialParts[i - 1] != "*" && polynomialParts[i - 1] != "/")
                        {
                            if (polynomialParts[i].Contains(letterCharacter) && !polynomialParts[i].Contains('^'))
                            {
                                int letterCount = polynomialParts[i].Count(char.IsLetter);
                                if (letterCount > 1)
                                {
                                    withLettersParts.Add(polynomialParts[i - 1] + "Duplicate" + Convert.ToString(i));
                                    withLettersParts.Add(polynomialParts[i] + "Duplicate" + Convert.ToString(i));
                                }
                                else
                                {
                                    withLettersParts.Add(polynomialParts[i - 1]);
                                    withLettersParts.Add(polynomialParts[i]);
                                }

                            }
                        }
                    }
                }
            }

            for (int i = 1; i < polynomialPartsCount; i++)
            {
                if (i < polynomialPartsCount - 1 && charsToCheck.Any(polynomialParts[i].Contains) == false)
                {
                    if (polynomialParts[i - 1] != "*" && polynomialParts[i - 1] != "/" && polynomialParts[i + 1] != "*" && polynomialParts[i + 1] != "/")
                    {
                        if (polynomialParts[i].Any(char.IsLetter) == false)
                        {
                            noLetterParts.Add(polynomialParts[i - 1]);
                            noLetterParts.Add(polynomialParts[i]);
                        }
                        else if (polynomialParts[i].Contains('^'))
                        {
                            leaveBeParts.Add(polynomialParts[i - 1]);
                            leaveBeParts.Add(polynomialParts[i]);
                        }
                    }
                    else
                    {
                        leaveBeParts.Add(polynomialParts[i - 1]);
                        leaveBeParts.Add(polynomialParts[i]);
                    }
                }
                else
                {
                    if (polynomialParts[i - 1] != "*" && polynomialParts[i - 1] != "/" && charsToCheck.Any(polynomialParts[i].Contains) == false)
                    {
                        if (polynomialParts[i].Any(char.IsLetter) == false)
                        {
                            noLetterParts.Add(polynomialParts[i - 1]);
                            noLetterParts.Add(polynomialParts[i]);
                        }
                        else if (polynomialParts[i].Contains('^'))
                        {
                            leaveBeParts.Add(polynomialParts[i - 1]);
                            leaveBeParts.Add(polynomialParts[i]);
                        }
                    }
                    else
                    {
                        if (charsToCheck.Any(polynomialParts[i].Contains) == false)
                        {
                            leaveBeParts.Add(polynomialParts[i - 1]);
                            leaveBeParts.Add(polynomialParts[i]);
                        }
                        continue;
                    }
                }
            }

            int withLettersPartCount = withLettersParts.Count;
            var DuplicateSaved = false;
            string firstDuplicateNumber = "";

            string secondDuplicateNumber = "";
            int count = -1;

            for (int i = 0; i < withLettersPartCount; i++)
            {
                if (count == 0)
                {
                    DuplicateSaved = false;
                }
                if (withLettersParts[i].Contains("Duplicate")
                    && (withLettersParts[i].Contains('+') == false && withLettersParts[i].Contains('-') == false && withLettersParts[i].Contains('*') == false && withLettersParts[i].Contains('/') == false)
                    && DuplicateSaved == false)
                {
                    Match match = Regex.Match(withLettersParts[i], @"\d+$");
                    firstDuplicateNumber = match.Value;

                    count = withLettersParts.Count(item => item.Contains($"Duplicate{firstDuplicateNumber}"));

                    withLettersParts[i - 1] = withLettersParts[i - 1].Replace($"Duplicate{firstDuplicateNumber}", "");
                    count--;
                    withLettersParts[i] = withLettersParts[i].Replace($"Duplicate{firstDuplicateNumber}", "");
                    count--;
                    DuplicateSaved = true;
                }

                if (withLettersParts[i].Contains("Duplicate")
                    && (withLettersParts[i].Contains('+') == false && withLettersParts[i].Contains('-') == false && withLettersParts[i].Contains('*') == false && withLettersParts[i].Contains('/') == false)
                    && DuplicateSaved == true)
                {
                    Match match = Regex.Match(withLettersParts[i], @"\d+$");
                    secondDuplicateNumber = match.Value;
                    if (secondDuplicateNumber == firstDuplicateNumber)
                    {
                        withLettersParts[i - 1] = "DELETE";
                        count--;
                        withLettersParts[i] = "DELETE";
                        count--;
                    }
                }
            }

            while (withLettersParts.Contains("DELETE"))
            {
                withLettersParts.Remove("DELETE");
            }



            List<string> newPolynomialPart1 = leaveBeParts.Concat(withLettersParts).ToList();
            List<string> newPolynomial = newPolynomialPart1.Concat(noLetterParts).ToList();

            return newPolynomial;
        }
        public static List<string> ModifyPolynomialParts(List<string> polynomialParts, int indexOfOperator, string newPart)
        {
            for (int i = 0; i < 4; i++)
            {
                polynomialParts.RemoveAt(indexOfOperator - 2);
            }
            polynomialParts.Insert(indexOfOperator - 2, newPart);
            return polynomialParts;
        }

        public static (List<string> partsToReturn, int countOfPartsToReturn) RemoveAndSaveFirstTerm(List<string> polynomialParts)
        {
            List<string> partsToReturn = [];
            int polynomialPartsCount = polynomialParts.Count;

            if (polynomialPartsCount > 2)
            {
                for (int i = 0; i < polynomialPartsCount; i++)

                {
                    if ((polynomialParts[i] == "+" || polynomialParts[i] == "-") && (polynomialParts[i + 2] != "*" && polynomialParts[i + 2] != "/"))
                    {
                        partsToReturn.Add(polynomialParts[i]);
                        partsToReturn.Add(polynomialParts[i + 1]);
                        break;
                    }
                    if (polynomialPartsCount == 4)
                    {
                        if ((polynomialParts[i] == "+" || polynomialParts[i] == "-") && (polynomialParts[i + 2] == "*" || polynomialParts[i + 2] == "/"))
                        {
                            partsToReturn.Add(polynomialParts[i]);
                            partsToReturn.Add(polynomialParts[i + 1]);
                            partsToReturn.Add(polynomialParts[i + 2]);
                            partsToReturn.Add(polynomialParts[i + 3]);
                            break;
                        }
                    }
                    if (polynomialPartsCount > 4)
                    {
                        if ((polynomialParts[i] == "+" || polynomialParts[i] == "-") && (polynomialParts[i + 2] == "*" || polynomialParts[i + 2] == "/") && (polynomialParts[i + 4] != "*" && polynomialParts[i + 4] != "/"))
                        {
                            partsToReturn.Add(polynomialParts[i]);
                            partsToReturn.Add(polynomialParts[i + 1]);
                            partsToReturn.Add(polynomialParts[i + 2]);
                            partsToReturn.Add(polynomialParts[i + 3]);
                            break;
                        }
                        if ((polynomialParts[i] == "+" || polynomialParts[i] == "-") && (polynomialParts[i + 2] == "*" || polynomialParts[i + 2] == "/") && (polynomialParts[i + 4] == "*" || polynomialParts[i + 4] == "/"))
                        {
                            partsToReturn.Add(polynomialParts[i]);
                            partsToReturn.Add(polynomialParts[i + 1]);
                            partsToReturn.Add(polynomialParts[i + 2]);
                            //partsToReturn.Add(polynomialParts[i + 3]);
                            break;
                        }
                    }
                }


                int count = partsToReturn.Count;
                return (partsToReturn, count);
            }
            else
            {

                partsToReturn.Add(polynomialParts[0]);
                partsToReturn.Add(polynomialParts[1]);
                int count = partsToReturn.Count;
                return (partsToReturn, count);

            }
        }

        public static (List<string> newPolynomialParts, bool newRound, List<string> usedParts) PolynomialProcessMultiplyOrDivide(string polynomial, List<string> usedParts)
        {
            bool newRound = true;
            var polynomialParts = FromStringToList(polynomial);

            List<string> copy = [];
            foreach (string part in polynomialParts)
            {
                copy.Add(part);
            }

            string copyString = String.Join("", copy);

            var newPolynomialPartsBefore = GoThroughPolynomial.GoThroughPartsForMultiplyOrDivide(polynomialParts);

            string newPolynomialPartsStringBefore = String.Join("", newPolynomialPartsBefore);

            var newPolynomialParts = FromStringToList(newPolynomialPartsStringBefore);

            string newPolynomialPartsString = String.Join("", newPolynomialParts);

            if (copyString != newPolynomialPartsString)
            {
                return (newPolynomialParts, newRound, usedParts);
            }
            else
            {
                if (newPolynomialPartsString.Contains('*') || newPolynomialPartsString.Contains('/'))
                {
                    var shorterPolynomialAndUsedParts = RemoveUsedParts(newPolynomialParts, usedParts);
                    var shorterPolynomial = shorterPolynomialAndUsedParts.polynomialParts;
                    var usedPartsUpdated = shorterPolynomialAndUsedParts.usedParts;

                    string shorterPolynomialString = String.Join("", shorterPolynomial);

                    if (shorterPolynomialString.Contains('*') || shorterPolynomialString.Contains('/'))
                    {
                        newRound = true;
                        return (shorterPolynomial, newRound, usedParts);
                    }
                    else
                    {
                        newRound = false;
                        return (newPolynomialParts, newRound, usedParts);
                    }
                }
                else
                {
                    newRound = false;
                    return (newPolynomialParts, newRound, usedParts);
                }
            }
        }

        public static List<string> PolynomialProcessAddOrSubtract(string polynomial, List<string> usedParts)
        {
            if (polynomial != "")
            {
                var polynomialParts = FromStringToListOrganize(polynomial).polynomialParts;

                List<string> copy = [];
                foreach (string part in polynomialParts)
                {
                    copy.Add(part);
                }

                string copyString = String.Join("", copy);

                var newPolynomialPartsBefore = GoThroughPolynomial.GoThroughPartsForAddOrSubtract(polynomialParts);

                string newPolynomialPartsStringBefore = String.Join("", newPolynomialPartsBefore);

                var newPolynomialParts = FromStringToListOrganize(newPolynomialPartsStringBefore).polynomialParts;

                string newPolynomialPartsString = String.Join("", newPolynomialParts);

                if (copyString != newPolynomialPartsString)
                {
                    PolynomialProcessAddOrSubtract(newPolynomialPartsString, usedParts);
                }
                else
                {
                    var shorterPolynomialAndUsedParts = RemoveUsedParts(newPolynomialParts, usedParts);
                    var shorterPolynomial = shorterPolynomialAndUsedParts.polynomialParts;
                    var usedPartsUpdated = shorterPolynomialAndUsedParts.usedParts;

                    string shorterPolynomialString = String.Join("", shorterPolynomial);
                    PolynomialProcessAddOrSubtract(shorterPolynomialString, usedPartsUpdated);
                }
            }
            return usedParts;
        }

        public static (List<string> polynomialParts, List<string> usedParts) RemoveUsedParts(List<string> polynomialParts, List<string> usedParts)
        {
            if (polynomialParts.Count > 0)
            {
                var usedPartsAndTheirCount = ProcessAndOrganize.RemoveAndSaveFirstTerm(polynomialParts);

                usedParts.AddRange(usedPartsAndTheirCount.partsToReturn);
                int countOfReturnedParts = usedPartsAndTheirCount.countOfPartsToReturn;

                polynomialParts.RemoveRange(0, countOfReturnedParts);

                return (polynomialParts, usedParts);
            }
            else
            {
                return (polynomialParts, usedParts);

            }
        }

        public static (List<string> polynomialParts, string differentLetters) FromStringToListOrganize(string polynomial)
        {
            polynomial = ProcessPolynomial(polynomial);

            string differentLetters = CheckLetters(polynomial);

            List<string> polynomialParts = OrganizePolynomial(polynomial, differentLetters);

            return (polynomialParts, differentLetters);
        }

        public static List<string> FromStringToList(string polynomial)
        {
            polynomial = ProcessPolynomial(polynomial);
            string[] polynomialPartsArray = polynomial.Split(' ');
            List<string> polynomialParts = polynomialPartsArray.ToList();
            polynomialParts.RemoveAll(item => string.IsNullOrEmpty(item));
            return polynomialParts;
        }

        public static string AssignValueToVariable(string input, string polynomial)
        {

            List<string> polynomialParts = FromStringToList(polynomial);
            input.Replace(" ", "");


            var inputLetter = Convert.ToString(input[0]);
            var inputNumber = input.Substring(input.IndexOf("=") + 1);

            for (int i = 0; i < polynomialParts.Count; i++)
            {
                if (polynomialParts[i].Contains(inputLetter))
                {
                    if (polynomialParts[i].Contains('^'))
                    {
                        int indexOfLetter = polynomialParts[i].IndexOf(inputLetter);
                        int indexOfPowerSymbol = polynomialParts[i].IndexOf('^');

                        if (indexOfPowerSymbol - indexOfLetter == 1)
                        {
                            double baseNum = Convert.ToDouble(inputNumber);
                            double power = Convert.ToDouble(polynomialParts[i].Substring(indexOfPowerSymbol + 1));
                            string powerResult = Convert.ToString(Math.Pow(baseNum, power));

                            try
                            {
                                if (char.IsNumber(polynomialParts[i][indexOfLetter - 1]))
                                {
                                    polynomialParts[i] = polynomialParts[i].Replace(inputLetter, "*");
                                    polynomialParts[i] = polynomialParts[i].Replace("^" + power, powerResult);

                                }
                                else if (char.IsLetter(polynomialParts[i][indexOfLetter - 1]))
                                {
                                    polynomialParts[i] = polynomialParts[i].Replace(inputLetter, "");
                                    polynomialParts[i] = polynomialParts[i].Replace("^" + power, "");

                                    if (polynomialParts[i].Any(c => char.IsNumber(c)))
                                    {
                                        double numbers = Convert.ToDouble(new string(polynomialParts[i].Where(char.IsDigit).ToArray()));
                                        double result = numbers * Convert.ToDouble(powerResult);
                                        polynomialParts[i] = polynomialParts[i].Replace(Convert.ToString(numbers), Convert.ToString(result));
                                    }
                                    
                                }
                            }
                            catch
                            {
                                polynomialParts[i] = polynomialParts[i].Replace(inputLetter, "");
                                polynomialParts[i] = polynomialParts[i].Replace("^" + power, powerResult);
                            }
                        }
                    }

                    if (polynomialParts[i] == inputLetter)
                    {
                        polynomialParts[i] = inputNumber;
                    }

                    if (polynomialParts[i] != inputLetter && polynomialParts[i].Contains("^") == false)
                    {
                        polynomialParts[i] = polynomialParts[i].Replace(inputLetter, "*" + inputNumber);
                    }
                }
            }
            polynomial = string.Join("", polynomialParts);
            return polynomial;
        }


        public static List<string> CleanFinalPolynomial(List<string> usedParts)
        {
            for (int i = 0; i < usedParts.Count; i++)
            {
                if (usedParts[0] == "+")
                {
                    usedParts[0] = "";
                }
                if (usedParts[i] == "0")
                {
                    if (usedParts.Count > 2)
                    {
                        usedParts[i - 1] = "";
                        usedParts[i] = "";
                    }
                }
                if (usedParts[i].Length > 1)
                {
                    if (usedParts[i][0] == '1' && Char.IsLetter(usedParts[i][1]))
                    {
                        usedParts[i] = usedParts[i].Remove(0, 1);
                    }
                }
                if (i > 1)
                {
                    if ((usedParts[i - 1] == "*" || usedParts[i - 1] == "/") && usedParts[i] == "+")
                    {
                        usedParts.RemoveAt(i);
                    }
                }
            }
            return usedParts;
        }
    }
}