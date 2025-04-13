using System.Collections.Generic;

namespace Calculator2
{
    public static class GoThroughPolynomial
    {


        public static List<string> GoThroughPartsForMultiplyOrDivide(List<string> polynomialParts)
        {
            if (polynomialParts.Any(item => item.Contains('*')) || polynomialParts.Any(item => item.Contains('/')))
            {
                return CheckOperation.CheckIfMultiplyOrDivide(polynomialParts);
            }
            else
            {
                return polynomialParts;
            }
        }

        public static List<string> GoThroughPartsForAddOrSubtract(List<string> polynomialParts)
        {
            return CheckOperation.CheckIfAddOrSubtract(polynomialParts);

        }
    }
}

