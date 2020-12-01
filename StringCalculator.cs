using System;
using System.Collections.Generic;
using System.Linq;

namespace ClassStringCalculator
{
    public class StringCalculator
    {
        
        public int Add(string numbers)
        {
            
            if (string.IsNullOrEmpty(numbers))
            {
                AddOccured?.Invoke(numbers, 0);
                return 0;
            }
            string[] delimiters = ExtractDelimiters(ref numbers);

            int sum = 0;
            var negativeNumbers = new List<int>();
            var parsedNumbers = numbers.Split(delimiters, StringSplitOptions.None).Select(int.Parse);
            foreach (int number in parsedNumbers.Where(n => n <= 1000))
            {
                if (number < 0)
                    negativeNumbers.Add(number);
                sum += number;
            }

            if (negativeNumbers.Any())
                throw new Exception("negatives not allowed: " + string.Join(",", negativeNumbers));


            AddOccured?.Invoke(numbers, sum);

            return sum;
        }

        private static string[] ExtractDelimiters(ref string numbers)
        {

            if (!numbers.StartsWith("//"))
                return new[] {",", "\n" };

            string[] delimiters;
            if (numbers[2] == '[' )
            {
                int delimiterEndIndex = numbers.LastIndexOf(']');
                delimiters = numbers.Substring(3, delimiterEndIndex - 3)
                    .Split(new[] { "][" },StringSplitOptions.None);
                numbers = numbers.Substring(delimiterEndIndex + 1);
            }
            else
            {
                delimiters = new[] { numbers[2].ToString() };
                numbers = numbers.Substring(4);                
            }

            return delimiters;
        }

        public delegate void AddOccurredEventHandler(string input, int result);

        public event AddOccurredEventHandler AddOccured;


    }
}
