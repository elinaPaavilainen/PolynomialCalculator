using System.Collections.Generic;

namespace Calculator2
{
    public class Program
    {
        static void Main(string[] args)
        {
            string? polynomial = "-23 + 2x + 9a - 4a - 23x - 18";
            string input = "";
            List<string> polynomialParts;
            string finalPolynomial = "";

            // koita tällä 3a+2a*3a+4+3*12+2a
            while (input != "0")
            {
                Console.WriteLine("Give a polynomial or assign a value to a variable:");
                input = Console.ReadLine();

                if (input.Contains("="))
                {
                    input.Replace(" ", "");
                    int indexOfVariableLetter = input.IndexOf('=') - 1;
                    //int index = input.IndexOf("=");
                    int indexOfVariableNumber = input.IndexOf("=") + 1;

                    char letter = input[indexOfVariableLetter];

                    int indexOfLetterInPolynomial = finalPolynomial.IndexOf(letter);
                    string variableNumber = "";

                    if (finalPolynomial[indexOfLetterInPolynomial - 1] == '/')
                    {
                        variableNumber = input.Substring(indexOfVariableNumber);
                    }
                    else
                    {
                        variableNumber = "*" + input.Substring(indexOfVariableNumber);
                    }
                    polynomial = finalPolynomial.Replace(Convert.ToString(input[indexOfVariableLetter]), variableNumber);

                    polynomialParts = ProcessAndOrganize.FromStringToList(polynomial);

                    for (int i = 0; i < polynomialParts.Count; i++)
                    {
                        if (polynomialParts[i].Contains('^') && !polynomialParts[i].Any(c => char.IsLetter(c)))
                        {
                            int indexOfPower = polynomialParts[i].IndexOf('^');
                            int baseNum = Convert.ToInt32(polynomialParts[i].Substring(0, indexOfPower));
                            int power = Convert.ToInt32(polynomialParts[i].Substring(indexOfPower + 1));
                            string powerResult = Convert.ToString(Math.Pow(baseNum, power));

                            polynomialParts[i] = powerResult;
                            polynomial = string.Join("", polynomialParts);
                        }
                    }
                }
                else
                {
                    polynomial = input;
                }

                List<string> usedParts = [];
                List<string> middlePolynomialParts = [];
                bool newRound;

                if (polynomial.Contains('*') || polynomial.Contains('/'))
                {
                    newRound = true;
                }
                else
                {
                    newRound = false;
                }
                polynomialParts = ProcessAndOrganize.FromStringToList(polynomial);
                polynomial = string.Join("", polynomialParts);

                while (newRound)
                {
                    var middlePolynomialPartsAndNewRound = ProcessAndOrganize.PolynomialProcessMultiplyOrDivide(polynomial, usedParts);
                    newRound = middlePolynomialPartsAndNewRound.newRound;
                    middlePolynomialParts = middlePolynomialPartsAndNewRound.newPolynomialParts;
                    usedParts = middlePolynomialPartsAndNewRound.usedParts;

                    polynomial = string.Join("", middlePolynomialParts);
                }

                usedParts = ProcessAndOrganize.PolynomialProcessAddOrSubtract(polynomial, usedParts);

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
                            usedParts[i-1] = "";
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

                finalPolynomial = string.Join("", usedParts);

                Console.WriteLine(finalPolynomial);

            }
        }
    }
}
