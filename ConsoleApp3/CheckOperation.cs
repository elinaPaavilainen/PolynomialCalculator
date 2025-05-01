namespace Calculator2
{
    internal class CheckOperation
    {
        public static List<string> CheckIfMultiplyOrDivide(List<string> polynomialParts)
        {
            int polynomialPartsCount = polynomialParts.Count;

            for (int i = 0; i < polynomialPartsCount; i++)

            {
                try
                {
                    if (polynomialParts[i].Contains('*'))
                    {
                        var newPart = MultiplyOrDivide.Multiply(polynomialParts[i - 2], polynomialParts[i - 1], polynomialParts[i + 1]);
                        var newPolynomialParts = ProcessAndOrganize.ModifyPolynomialParts(polynomialParts, i, newPart);
                        return newPolynomialParts;
                    }
                    if (polynomialParts[i].Contains('/'))
                    {
                        var newPart = MultiplyOrDivide.Divide(polynomialParts[i - 2], polynomialParts[i - 1], polynomialParts[i + 1]);
                        var newPolynomialParts = ProcessAndOrganize.ModifyPolynomialParts(polynomialParts, i, newPart);
                        return newPolynomialParts;
                    }
                }
                catch 
                {
                    continue;
                }
            }
            return polynomialParts;
        }

        public static List<string> CheckIfAddOrSubtract(List<string> polynomialParts)
        {
            int polynomialPartsCount = polynomialParts.Count;

            for (int i = 0; i < polynomialPartsCount; i++)
            {
                if (polynomialParts[i].Contains('+'))
                {
                    try
                    {
                        var newPart = AddOrSubtract.Add(polynomialParts[i - 2], polynomialParts[i - 1], polynomialParts[i + 1]);
                        var newPolynomialParts = ProcessAndOrganize.ModifyPolynomialParts(polynomialParts, i, newPart);
                        return newPolynomialParts;
                    }
                    catch
                    {
                        continue;
                    }
                }
                else if (polynomialParts[i].Contains('-'))
                {
                    try
                    {
                        var newPart = AddOrSubtract.Subtract(polynomialParts[i - 2], polynomialParts[i - 1], polynomialParts[i + 1]);
                        var newPolynomialParts = ProcessAndOrganize.ModifyPolynomialParts(polynomialParts, i, newPart);
                        return newPolynomialParts;
                    }
                    catch
                    {
                        continue;
                    }
                }
            }

            return polynomialParts;
        }

    }
}