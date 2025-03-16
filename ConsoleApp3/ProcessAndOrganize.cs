using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Calculator2
{
    public static class ProcessAndOrganize
    {
        private static char[] charsToCheck = { '*', '/', '+', '-' };
        public static string ProcessPolynomial(string polynomial)
        {
            polynomial = polynomial.Replace(" ", "");
            string newPolynomial = "";

            if (char.IsAsciiLetterOrDigit(polynomial[0]) && polynomial[0] != '+' && polynomial[0] != '-' && polynomial[0] != '(')
            {
                polynomial = "+" + polynomial;
            }

            foreach (char character in polynomial)
            {
                if (charsToCheck.Contains(character))
                {
                    newPolynomial = newPolynomial + " " + character + " ";
                }
                else
                {
                    newPolynomial = newPolynomial + character;
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
        public static List<string> OrganizePolynomial(string polynomial, string differentLetters)
        {

            //tee kerto ja jakolaskulle oma tarkistus

            string[] polynomialPartsArray = polynomial.Split(' ');
            List<string> polynomialParts = polynomialPartsArray.ToList();

            int polynomialPartsCount = polynomialParts.Count;

            List<string> withLettersParts = new List<string>();
            List<string> noLetterParts = new List<string>();



            


           

            foreach (var letterCharacter in differentLetters)
            {
                for (int i = 0; i < polynomialPartsCount; i++)
                {
                    foreach (char character in charsToCheck)
                    {
                        if (!polynomialParts[i].Contains(character))
                        {
                            if (polynomialParts[i].Contains(letterCharacter))
                            {
                                withLettersParts.Add(polynomialParts[i-1]);
                                withLettersParts.Add(polynomialParts[i]);
                                break;
                            }
                        }
                    }
                }
            }
            for (int i = 0; i < polynomialPartsCount; i++)
            {
                foreach (char character in charsToCheck)
                {
                    if (!polynomialParts[i].Contains(character))
                    {
                        if (polynomialParts[i].All(char.IsDigit))
                        {
                            noLetterParts.Add(polynomialParts[i - 1]);
                            noLetterParts.Add(polynomialParts[i]);
                            break;
                        }
                    }
                }
            }
            List<string> newPolynomial = withLettersParts.Concat(noLetterParts).ToList();

            //foreach (string part in newPolynomial)
            //    Console.WriteLine(part);
            return newPolynomial;
        }
        public static List<string> ModifyPolynomialParts(List<string> polynomialParts, int indexOfOperator, string newPart)
        {
            for (int i = 0; i < 4; i++)
            {
                polynomialParts.RemoveAt(indexOfOperator - 2);
            }
            polynomialParts.Insert(indexOfOperator - 2, newPart);
            //polynomialParts.ForEach(Console.WriteLine);
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
                    if ((polynomialParts[i] == "+" || polynomialParts[i] == "-") && (polynomialParts[i + 2] != "*" || polynomialParts[i + 2] != "/"))
                    {
                        partsToReturn.Add(polynomialParts[i]);
                        partsToReturn.Add(polynomialParts[i + 1]);
                        break;
                    }

                    if ((polynomialParts[i] == "+" || polynomialParts[i] == "-") && (polynomialParts[i + 2] == "*" || polynomialParts[i + 2] == "/") && (polynomialParts[i + 4] != "*" || polynomialParts[i + 4] != "/"))
                    {
                        partsToReturn.Add(polynomialParts[i]);
                        partsToReturn.Add(polynomialParts[i + 1]);
                        partsToReturn.Add(polynomialParts[i + 2]);
                        partsToReturn.Add(polynomialParts[i + 3]);
                        break;
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


        public static List<string> PolynomialMillMultiplyOrDivide(string polynomial, List<string> usedParts)
        {

            if (polynomial != "")
            {
                var polynomialParts = FromStringToList(polynomial).polynomialParts;

                List<string> copy = [];
                foreach (string part in polynomialParts)
                {
                    copy.Add(part);
                }

                string copyString = String.Join("", copy);


                var newPolynomialPartsBefore = GoThroughPolynomial.GoThroughPartsForMultiplyOrDivide(polynomialParts);

                string newPolynomialPartsStringBefore = String.Join("", newPolynomialPartsBefore);

                var newPolynomialParts = FromStringToList(newPolynomialPartsStringBefore).polynomialParts;

                string newPolynomialPartsString = String.Join("", newPolynomialParts);



                if (copyString != newPolynomialPartsString)
                {
                    PolynomialMillMultiplyOrDivide(newPolynomialPartsString, usedParts);
                }
                else
                {
                    var shorterPolynomialAndUsedParts = ChewOnPolynomial(newPolynomialParts, usedParts);
                    var shorterPolynomial = shorterPolynomialAndUsedParts.polynomialParts;
                    var usedPartsUpdated = shorterPolynomialAndUsedParts.usedParts;

                    string shorterPolynomialString = String.Join("", shorterPolynomial);
                    PolynomialMillMultiplyOrDivide(shorterPolynomialString, usedPartsUpdated);
                }
            }
            return usedParts;
        }







        public static List<string> PolynomialMillAddOrSubtract(string polynomial, List<string> usedParts)
        {

            if (polynomial != "")
            {
                var polynomialParts = FromStringToList(polynomial).polynomialParts;

                List<string> copy = [];
                foreach (string part in polynomialParts)
                {
                    copy.Add(part);
                }

                string copyString = String.Join("", copy);


                var newPolynomialPartsBefore = GoThroughPolynomial.GoThroughPartsForAddOrSubtract(polynomialParts);

                string newPolynomialPartsStringBefore = String.Join("", newPolynomialPartsBefore);

                var newPolynomialParts = FromStringToList(newPolynomialPartsStringBefore).polynomialParts;

                string newPolynomialPartsString = String.Join("", newPolynomialParts);



                if (copyString != newPolynomialPartsString)
                {
                    PolynomialMillAddOrSubtract(newPolynomialPartsString, usedParts);
                }
                else
                {
                    var shorterPolynomialAndUsedParts = ChewOnPolynomial(newPolynomialParts, usedParts);
                    var shorterPolynomial = shorterPolynomialAndUsedParts.polynomialParts;
                    var usedPartsUpdated = shorterPolynomialAndUsedParts.usedParts;

                    string shorterPolynomialString = String.Join("", shorterPolynomial);
                    PolynomialMillAddOrSubtract(shorterPolynomialString, usedPartsUpdated);
                }
            }
            return usedParts;
        }

        public static (List<string> polynomialParts, List<string> usedParts) ChewOnPolynomial(List<string> polynomialParts, List<string> usedParts)
        {

            var usedPartsAndTheirCount = ProcessAndOrganize.RemoveAndSaveFirstTerm(polynomialParts);

            usedParts.AddRange(usedPartsAndTheirCount.partsToReturn);
            int countOfReturnedParts = usedPartsAndTheirCount.countOfPartsToReturn;

            polynomialParts.RemoveRange(0, countOfReturnedParts);



            return (polynomialParts, usedParts);
        }


        public static (List<string> polynomialParts, string differentLetters) FromStringToList(string polynomial)
        {
            polynomial = ProcessAndOrganize.ProcessPolynomial(polynomial);

            string differentLetters = ProcessAndOrganize.CheckLetters(polynomial);

            List<string> polynomialParts = ProcessAndOrganize.OrganizePolynomial(polynomial, differentLetters);

            return (polynomialParts, differentLetters);
        }
    }
}



//var newPolynomialParts = GoThroughPolynomial.GoThroughParts(polynomialParts);



//var newerPolynomialParts = GoThroughPolynomial.GoThroughParts(newPolynomialParts);

//newerPolynomialParts.ForEach(Console.WriteLine);
//Console.WriteLine();

//var newerNewerPolynomialParts = GoThroughPolynomial.GoThroughParts(newerPolynomialParts);

//newerNewerPolynomialParts.ForEach(Console.WriteLine);
//Console.WriteLine();

//var newerNewerPolynomialParts2 = GoThroughPolynomial.GoThroughParts(newerNewerPolynomialParts);

//newerNewerPolynomialParts2.ForEach(Console.WriteLine);
//Console.WriteLine();