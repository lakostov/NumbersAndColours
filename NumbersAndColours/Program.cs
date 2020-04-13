using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumbersAndColours
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] digits = new int[10];
            Dictionary<string, int> ColoursCount = new Dictionary<string, int>();

            Dictionary<string, int> ColourLinesCount = new Dictionary<string, int>();

            int greenNotBlueCount = 0;
            int sameColourCount = 0;
            int differentColourCount = 0;
            int alphabeticalPairCount = 0;

            string[] numbers = System.IO.File.ReadAllLines(@"Data\numbers.txt");
            string[] colours = System.IO.File.ReadAllLines(@"Data\colours.txt");

            foreach (var number in numbers)
            {
                char[] digitsInNumber = number.ToCharArray();

                foreach (var digitInNumber in digitsInNumber)
                {
                    digits[(int)char.GetNumericValue(digitInNumber)]++;
                }
            }

            foreach (var colorLine in colours)
            {
                var coloursInLine = colorLine.Split(',').ToList();

                if (AllElementsEqual(coloursInLine))
                {
                    sameColourCount++;
                }

                if (AllElementsDifferent(coloursInLine))
                {
                    differentColourCount++;
                }

                if (coloursInLine.Contains("GREEN") && !coloursInLine.Contains("BLUE"))
                {
                    greenNotBlueCount++;
                }

                if (ContainAplhabeticalPair(coloursInLine))
                {
                    alphabeticalPairCount++;
                }

                foreach (var colorName in coloursInLine)
                {
                    if (ColoursCount.ContainsKey(colorName))
                    {
                        ColoursCount[colorName]++;
                    }
                    else
                    {
                        ColoursCount.Add(colorName, 1);
                    }
                }

                foreach (var key in ColoursCount.Keys.ToList())
                {
                    if (coloursInLine.Contains(key))
                    {
                        if (ColourLinesCount.ContainsKey(key))
                        {
                            ColourLinesCount[key]++;
                        }
                        else
                        {
                            ColourLinesCount.Add(key, 1);
                        }
                    }
                }
            }

            DisplayMostFriquentDigit(digits);
            DisplayMissingNumber(numbers);
            DisplayMostFriquentColour(ColoursCount);
            DisplayColorInLeastLines(ColourLinesCount);
            DisplayNumberGreenNotBlueLines(greenNotBlueCount);
            DisplayNumberOfLinesWithAllSameColour(sameColourCount);
            DisplayNumberOfLinesWithAllDifferentColour(differentColourCount);
            DisplayNumberOfLinesWithAlphabeticalOrder(alphabeticalPairCount);
            Console.Read();
        }

        public static bool AllElementsEqual(List<string> names)
        {
            bool result = true;
            for (var i = 0; i < names.Count - 1; i++)
            {
                if (names[i] != names[i+1])
                {
                    result = false;
                }
            }

            return result;
        }

        public static bool AllElementsDifferent(List<string> names)
        {
            List<string> Colours = new List<string>();

            foreach (var name in names)
            {
                if (Colours.Contains(name))
                {
                    return false;
                }
                else
                {
                    Colours.Add(name);
                }
            }

            return true;
        }

        public static bool ContainAplhabeticalPair(List<string> names)
        {
            for (var i = 0; i < names.Count - 1; i++)
            {
                if (String.Compare(names[i], names[i+1]) < 0)
                {
                    return true;
                }
            }

            return false;
        }

        public static void DisplayMostFriquentDigit(int[] digits)
        {
            var max = 0;
            var index = 0;

            for (var i = 0; i < digits.Length; i++)
            {
                if (digits[i] > max)
                {
                    max = digits[i];
                    index = i;
                }
            }

            Console.WriteLine($"The most friquent digit is: {index}");
        }

        public static void DisplayMissingNumber(string[] numbers)
        {
            var convertedNumbers = Array.ConvertAll(numbers, n => Int32.Parse(n));
            Array.Sort(convertedNumbers);

            int missingNumber = 0;

            for (var i = 0; i < convertedNumbers.Length - 1; i++)
            {
                if (convertedNumbers[i] != convertedNumbers[i+1] - 1)
                {
                    missingNumber = convertedNumbers[i] + 1;
                    break;
                }
            }

            Console.WriteLine($"The missing number is: {missingNumber}");
        }

        public static void DisplayMostFriquentColour(Dictionary<string,int> colours)
        {
            int maxColourCount = 0;
            string maxColour = String.Empty;

            foreach (KeyValuePair<string, int> colour in colours)
            {
                if (colour.Value > maxColourCount)
                {
                    maxColourCount = colour.Value;
                    maxColour = colour.Key;
                }
            }

            Console.WriteLine($"The most friquent colour is: {maxColour}");
        }

        public static void DisplayColorInLeastLines(Dictionary<string, int> colours)
        {
            int minCount = Int32.MaxValue;
            string minColour = String.Empty;

            foreach (KeyValuePair<string, int> colour in colours)
            {
                if (colour.Value < minCount)
                {
                    minCount = colour.Value;
                    minColour = colour.Key;
                }
            }

            Console.WriteLine($"The colour with fewest lines is: {minColour}");
        }

        public static void DisplayNumberGreenNotBlueLines(int count)
        {
            Console.WriteLine($"The number of lines with GREEN but not BLUE is: {count}");
        }

        public static void DisplayNumberOfLinesWithAllSameColour(int count)
        {
            Console.WriteLine($"The number of lines with all same colours is: {count}");
        }

        public static void DisplayNumberOfLinesWithAllDifferentColour(int count)
        {
            Console.WriteLine($"The number of lines with all different colours is: {count}");
        }

        public static void DisplayNumberOfLinesWithAlphabeticalOrder(int count)
        {
            Console.WriteLine($"The number of lines with alphabetical pairs is: {count}");
        }
    }
}
