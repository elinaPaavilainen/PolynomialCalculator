using System.Collections.Generic;

namespace Calculator2
{
    public class Program
    {
        static void Main(string[] args)
        {
            string? polynomial = "-23 + 2x + 9a - 4a - 23x - 18";

            while (polynomial != "0")
            {
                Console.WriteLine("Give a polynomial:");
                polynomial = Console.ReadLine();

                List<string> usedParts = [];

                usedParts = ProcessAndOrganize.PolynomialMillMultiplyOrDivide(polynomial, usedParts);

                string middlePolynomial = string.Join("", usedParts);

                usedParts = [];

                usedParts = ProcessAndOrganize.PolynomialMillAddOrSubtract(middlePolynomial, usedParts);

                string finalPolynomial = string.Join("", usedParts);
                Console.WriteLine(finalPolynomial);
            }
        }
    }
}
