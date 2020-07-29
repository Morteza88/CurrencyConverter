using System;
using System.Collections.Generic;

namespace CurrencyConverterLibrary
{
    public class CurrencyConverter : ICurrencyConverter
    {
        public void ClearConfiguration()
        {
            throw new NotImplementedException();
        }

        public double Convert(string fromCurrency, string toCurrency, double amount)
        {
            throw new NotImplementedException();
        }

        public void UpdateConfiguration(IEnumerable<Tuple<string, string, double>> conversionRates)
        {
            throw new NotImplementedException();
        }
    }
}
