using CurrencyConverterLibrary;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CurrencyConverterlibrary.NUnitTests
{
    public class CurrencyConverterUnitTest
    {
        [Test]
        public void CurrencyConverter_Simple_ReturnConversionRate()
        {
            List<Tuple<string, string, double>> rates = new List<Tuple<string, string, double>>();
            rates.Add(Tuple.Create("USD", "CAD", 1.34));
            rates.Add(Tuple.Create("CAD", "GBP", 0.58));
            rates.Add(Tuple.Create("USD", "EUR", 0.86));
            CurrencyConverter currencyConverter = new CurrencyConverter();
            currencyConverter.UpdateConfiguration(rates);

            double result = currencyConverter.Convert("CAD", "EUR", 1);
            Assert.AreEqual(0.86 / 1.34, result);
        }

        [Test]
        public void CurrencyConverter_ConnectedGraph_ReturnShortestPathConversionRate()
        {
            //Arrenge
            List<Tuple<string, string, double>> rates = new List<Tuple<string, string, double>>();
            rates.Add(Tuple.Create("a", "b", 1.32));
            rates.Add(Tuple.Create("a", "d", 0.58));
            rates.Add(Tuple.Create("b", "c", 1.46));
            rates.Add(Tuple.Create("d", "e", 1.56));
            rates.Add(Tuple.Create("d", "h", 2.86));
            rates.Add(Tuple.Create("e", "f", 0.76));
            rates.Add(Tuple.Create("e", "g", 0.80));
            rates.Add(Tuple.Create("e", "h", 0.65));
            rates.Add(Tuple.Create("f", "g", 1.25));
            rates.Add(Tuple.Create("g", "h", 0.92));
            CurrencyConverter currencyConverter = new CurrencyConverter();
            currencyConverter.UpdateConfiguration(rates);

            double amount = 100;
            double delta = 0.0001;

            //Action
            var ConvertaToa = currencyConverter.Convert("a", "a", amount);
            var ConvertaTob = currencyConverter.Convert("a", "b", amount);
            var ConvertbToa = currencyConverter.Convert("b", "a", amount);
            var ConvertaToc = currencyConverter.Convert("a", "c", amount);
            var ConvertcToa = currencyConverter.Convert("c", "a", amount);
            var ConvertaTod = currencyConverter.Convert("a", "d", amount);
            var ConvertdToa = currencyConverter.Convert("d", "a", amount);
            var ConvertaToe = currencyConverter.Convert("a", "e", amount);
            var ConverteToa = currencyConverter.Convert("e", "a", amount);
            var ConvertaTof = currencyConverter.Convert("a", "f", amount);
            var ConvertfToa = currencyConverter.Convert("f", "a", amount);
            var ConvertaTog = currencyConverter.Convert("a", "g", amount);
            var ConvertgToa = currencyConverter.Convert("g", "a", amount);
            var ConvertaToh = currencyConverter.Convert("a", "h", amount);
            var ConverthToa = currencyConverter.Convert("h", "a", amount);

            //Assert
            //Assert.AreEqual(1, ConvertaToa);
            Assert.AreEqual(amount * 1.32, ConvertaTob, delta);
            Assert.AreEqual(amount / 1.32, ConvertbToa, delta);
            Assert.AreEqual(amount * (1.32 * 1.46), ConvertaToc, delta);
            Assert.AreEqual(amount / (1.32 * 1.46), ConvertcToa, delta);
            Assert.AreEqual(amount * 0.58, ConvertaTod, delta);
            Assert.AreEqual(amount / 0.58, ConvertdToa, delta);
            Assert.AreEqual(amount * (0.58 * 1.56), ConvertaToe, delta);
            Assert.AreEqual(amount / (0.58 * 1.56), ConverteToa, delta);
            Assert.AreEqual(amount * (0.58 * 1.56 * 0.76), ConvertaTof, delta);
            Assert.AreEqual(amount / (0.58 * 1.56 * 0.76), ConvertfToa, delta);
            Assert.That(ConvertaTog, Is.InRange(amount * (0.58 * 1.56 * 0.80) - delta, amount * (0.58 * 1.56 * 0.80) + delta)
                                   | Is.InRange(amount * (0.58 * 2.86 * (1 / 0.92)) - delta, amount * (0.58 * 2.86 * (1 / 0.92)) + delta));
            Assert.That(ConvertgToa, Is.InRange((amount / (0.58 * 1.56 * 0.80)) - delta, (amount / (0.58 * 1.56 * 0.80)) + delta)
                                   | Is.InRange((amount / (0.58 * 2.86 * (1 / 0.92))) - delta, (amount / (0.58 * 2.86 * (1 / 0.92))) + delta));
            Assert.AreEqual(amount * (0.58 * 2.86), ConvertaToh, delta);
            Assert.AreEqual(amount / (0.58 * 2.86), ConverthToa, delta);
        }
    }
}
