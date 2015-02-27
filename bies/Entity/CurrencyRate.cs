using System;
using System.Collections.Generic;
using System.Text;

namespace bies
{
    public class CurrencyRate
    {
        private int currencyRateID;
        private double rate;
        private DateTime date;
        private int currencyID;

        public int CurrencyRateID
        {
            get { return currencyRateID; }
            set { currencyRateID = value; }
        }

        

        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }

        public int CurrencyID
        {
            get { return currencyID; }
            set { currencyID = value; }
        }

        public double Rate
        {
            get { return rate; }
            set { rate = value; }
        }
    }

}
