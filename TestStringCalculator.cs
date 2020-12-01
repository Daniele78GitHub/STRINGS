using ClassStringCalculator;
using NUnit.Framework;
using System;

namespace NUnitTestStringCalculator
{
    [TestFixture]
    public class Tests
    {
        private StringCalculator calculator;
        [SetUp]
        public void Setup()
        {
            this.calculator = new StringCalculator();
        }

        private int Number(string numbers)
        {
            var result = calculator.Add(numbers);

            return result;
        }

        // Test string null
        [Test]
        [TestCase("", ExpectedResult = 0)]
        [TestCase(null, ExpectedResult = 0)]
        public int StringNull(string numbers)
        {
            return Number(numbers);
        }

        [Test]
        [TestCase("2", ExpectedResult = 2)]
        [TestCase("200", ExpectedResult = 200)]
        public int SameNumber(string numbers)
        {
            return Number(numbers);
        }

        [Test]
        [TestCase("1,2", ExpectedResult = 3)]
        [TestCase("10,11", ExpectedResult = 21)]
        public int SommadiDueNumeri(string numbers)
        {
            return Number(numbers);
        }

        // ## Step 1  - Test OK
        //1. The method can take 0, 1 or 2 numbers, separated by commas, and will return their sum, examples: 
        //* **“”** should return **0**
        //* **“1”** should return **1**
        //* **“1,2”** should return **3**
        [Test]
        [TestCase(0,"")]
        [TestCase(1, "1")]
        [TestCase(3, "1,2")]
        [TestCase(6, "1\n2,3")]

        // Test Somma di due numeri
        public void TestStep1(int expected, string numbers)
        {
            Assert.AreEqual(expected, this.calculator.Add(numbers));
        }

        //## Step 3
        // Calling Add with a negative number will throw an exception **“negatives not allowed”** - and the negative that was passed.
        // If there are multiple negatives, show all of them in the exception message

        [Test]
        [TestCase("-2", "1,-2")]

        // Numeri Negativi
        public void TestStep3(string expectedNegativeNumbers, string numbers)
        {
            var exception = Assert.Throws<Exception>(() => this.calculator.Add(numbers));
            Assert.AreEqual("negatives not allowed: " + expectedNegativeNumbers, exception.Message);

        }

        //## Step 4 - Test OK
        //Numbers bigger than 1000 should be ignored, for example**“1000,2”** should return **2**
        [Test]
        [TestCase(2, "2,1001")]
        [TestCase(1002, "2,1000")]
        public void TestStep4(int expected, string numbers)
        {
            Assert.AreEqual(expected, this.calculator.Add(numbers));
        }


        //## Step 6 - Test OK

        [Test]
        [TestCase(4, "//;\n2;2")]
        [TestCase(6, "//[***]\n1***2***3")]
        [TestCase(6, "//[***][%]\n1***2%3")]
        public void TestStep6(int expected, string numbers)
        {
            Assert.AreEqual(expected, this.calculator.Add(numbers));
        }

        [Test]
        [TestCase(4, "2,2")]
        [TestCase(0, "")]
        public void TestAddInputResult(int expectedResult, string numbers)
        {
            string receivedInput = null; ;
            int receivedResult = 0; ;

            this.calculator.AddOccured += (input, result) =>
            {
                receivedInput = input;
                receivedResult = result;
            };

            this.calculator.Add(numbers);

            Assert.AreEqual(numbers, receivedInput);
            Assert.AreEqual(expectedResult, receivedResult);
        }

    }
}