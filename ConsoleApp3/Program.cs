using System.Collections.Generic;
using System.Diagnostics.Metrics;

namespace Calculator2
{
    public class Program
    {
        static void Main(string[] args)
        {
            string polynomial = "";
            string input = "";
            List<string> polynomialParts;
            string finalPolynomial = "";
            Console.WriteLine("Give a polynomial:");
            input = Console.ReadLine();

            while (input != "0")
            {
                if (input.Contains("="))
                {
                    input.Replace(" ", "");
                    int indexOfVariableLetter = input.IndexOf('=') - 1;
                    char letter = input[indexOfVariableLetter];
                    if (finalPolynomial.Contains(letter) == false)
                    {
                        Console.WriteLine("You gave wrong variable!");
                        polynomial = finalPolynomial;
                    }
                    else
                    { 
                        polynomial = ProcessAndOrganize.AssignValueToVariable(input, finalPolynomial);
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
                usedParts = ProcessAndOrganize.CleanFinalPolynomial(usedParts);

                finalPolynomial = string.Join("", usedParts);

                Console.WriteLine(finalPolynomial);
                Console.WriteLine("Give a polynomial or assign a value to a variable. Give 0 to quit.");
                input = Console.ReadLine();
            }
        }
    }
}
