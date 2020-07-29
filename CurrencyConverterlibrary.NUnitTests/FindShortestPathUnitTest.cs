using CurrencyConverterLibrary;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace CurrencyConverterlibrary.NUnitTests
{
    public class FindShortestPathUnitTest
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void Test1()
        {
            List<Tuple<string, string, double>> rates = new List<Tuple<string, string, double>>();
            rates.Add(Tuple.Create("USD", "CAD", 1.34));
            rates.Add(Tuple.Create("CAD", "GBP", 0.58));
            rates.Add(Tuple.Create("USD", "EUR", 0.86));
            CurrencyConverter currencyConverter = new CurrencyConverter();
            currencyConverter.UpdateConfiguration(rates);

            double result = currencyConverter.Convert("CAD", "EUR", 1);
            Assert.AreEqual(0.86/1.34,result);
        }
    }
}