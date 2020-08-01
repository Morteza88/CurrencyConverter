using CurrencyConverterLibrary.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace CurrencyConverterLibrary
{
    public class CurrencyConverter : ICurrencyConverter
    {
        private static readonly object lockObject = new object();
        private static CurrencyConverter instance = null;

        private Dictionary<string, Dictionary<string, double>> ConversionRateDictionaris = new Dictionary<string, Dictionary<string, double>>();
        private List<Tuple<string, string, double>> ConversionRates = new List<Tuple<string, string, double>>();

        private CurrencyConverter() { }
        public static CurrencyConverter GetInstance()
        {
            if (instance == null)
            {
                lock (lockObject)
                {
                    if (instance == null)
                        instance = new CurrencyConverter();
                }
            }
            return instance;
        }

        public void ClearConfiguration()
        {
            lock (ConversionRateDictionaris)
            {
                ConversionRates.Clear();
                ConversionRateDictionaris.Clear();
            }
        }

        public double Convert(string fromCurrency, string toCurrency, double amount)
        {
            if (!ConversionRateDictionaris[fromCurrency].ContainsKey(toCurrency))
                return 0;
            return amount * ConversionRateDictionaris[fromCurrency][toCurrency];
        }

        public void UpdateConfiguration(IEnumerable<Tuple<string, string, double>> conversionRates)
        {
            lock (ConversionRateDictionaris)
            {
                updateConversionRates(conversionRates);
                ConversionRateDictionaris.Clear();

                var currencyGraph = Graph<string>.CreateGraph(ConversionRates);
                foreach (var startNode in currencyGraph.Nodes)
                {
                    ConversionRateDictionaris.Add(startNode, new Dictionary<string, double>());
                    var shortestPathFunc = currencyGraph.GetShortestPathFunction(startNode);
                    foreach (var endNode in currencyGraph.Nodes)
                    {
                        if (endNode.Equals(startNode))
                            continue;
                        var shortestPath = shortestPathFunc(endNode);
                        if (shortestPath.Count() > 1)
                        {
                            double rate = 1;
                            for (int i = 0; i < shortestPath.Count() - 1; i++)
                            {
                                rate *= getRate(shortestPath[i], shortestPath[i + 1]);
                            }
                            ConversionRateDictionaris[startNode].Add(endNode, rate);
                        }
                    }
                }
            }
        }

        private void updateConversionRates(IEnumerable<Tuple<string, string, double>> conversionRates)
        {
            foreach (var item in conversionRates)
            {
                ConversionRates.RemoveAll(x => (x.Item1 == item.Item1 && x.Item2 == item.Item2)
                                            || (x.Item1 == item.Item2 && x.Item2 == item.Item1));
                ConversionRates.Add(item);
            }
        }

        private double getRate(string currencyFrom, string currencyTo)
        {
            double? result = ConversionRates.Find(x => x.Item1 == currencyFrom && x.Item2 == currencyTo)?.Item3;
            if (result == null)
                result = 1 / ConversionRates.Find(x => x.Item1 == currencyTo && x.Item2 == currencyFrom).Item3;
            return (double)result;
        }
    }
}
