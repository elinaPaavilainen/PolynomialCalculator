using System.Collections.Generic;

namespace Calculator2
{
    public class Program
    {
        static void Main(string[] args)
        {
            string? polynomial = "-23 + 2x + 9a - 4a - 23x - 18";
            string input = "";
            string finalPolynomial = "";

            // koita tällä 2x-2x-2-x-2x*2x*2*x
            while (input != "0")
            {
                Console.WriteLine("Give a polynomial or assign a value to a variable:");
                input = Console.ReadLine();

                if (input.Contains("="))
                {
                    input.Replace(" ", "");
                    int indexOfVariableLetter = input.IndexOf('=') - 1;
                    int index = input.IndexOf("=");
                    int indexOfVariableNumber = input.IndexOf("=") + 1;

                    char letter = input[indexOfVariableLetter];

                    int indexOfLetterInPolynomial = finalPolynomial.IndexOf(letter);

                    string multiplyCharAndVariableNumber = "*" + input.Substring(indexOfVariableNumber);
                    polynomial = finalPolynomial.Replace(Convert.ToString(input[indexOfVariableLetter]), multiplyCharAndVariableNumber);
                    //Console.WriteLine(polynomial);
                    //Console.WriteLine();

                    var polynomialParts = ProcessAndOrganize.FromStringToList(polynomial);

                    //foreach ( var polynomialPart in polynomialParts )
                    //{
                    //    Console.WriteLine(polynomialPart);
                    //}                    

                    for (int i = 0; i < polynomialParts.Count; i++)
                    {
                        if (polynomialParts[i].Contains('^') && !polynomialParts[i].Any(c => char.IsLetter(c)))
                        {
                            int indexOfPower = polynomialParts[i].IndexOf('^');
                            int baseNum = Convert.ToInt32(polynomialParts[i].Substring(0, indexOfPower));
                            int power = Convert.ToInt32(polynomialParts[i].Substring(indexOfPower + 1));
                            string powerResult = Convert.ToString(Math.Pow(baseNum, power));

                            //polynomial = polynomial.Remove(indexOfLetterInPolynomial + 1);
                            //polynomial = polynomial.Remove(indexOfLetterInPolynomial + 1);
                            //polynomial = polynomial.Remove(indexOfLetterInPolynomial + 1);
                            //polynomial = polynomial + powerResult;
                            polynomialParts[i] = powerResult;
                            polynomial = string.Join("", polynomialParts);
                            Console.WriteLine(polynomial);

                        }
                    }
                }
                else
                {
                    polynomial = input;
                }



                List<string> middlePolynomialParts = [];

                bool newRound = true;
                while (newRound)
                {
                    var middlePolynomialPartsAndNewRound = ProcessAndOrganize.PolynomialMillMultiplyOrDivide(polynomial);
                    newRound = middlePolynomialPartsAndNewRound.newRound;
                    middlePolynomialParts = middlePolynomialPartsAndNewRound.newPolynomialParts;
                    polynomial = string.Join("", middlePolynomialParts);
                }



                //string middlePolynomial = string.Join("", middlePolynomialParts);

                List<string> usedParts = [];

                usedParts = ProcessAndOrganize.PolynomialMillAddOrSubtract(polynomial, usedParts);

                finalPolynomial = string.Join("", usedParts);

                Console.WriteLine(finalPolynomial);

            }
        }
    }
}
