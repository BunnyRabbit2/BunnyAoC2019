using System.Collections.Generic;

namespace AdventOfCode2019
{
    public class ChemReaction
    {
        public Dictionary<string,int> Inputs {get;}
        public KeyValuePair<string,int> Output {get;}

        public ChemReaction(Dictionary<string,int> i, KeyValuePair<string,int> o)
        {
            Inputs = i;
            Output = o;
        }
    }

    public class ChemStocks
    {
        public Dictionary<string,long> stocks;

        public ChemStocks()
        {
            stocks = new Dictionary<string, long>();
        }

        public long getStock(string s)
        {
            return stocks.ContainsKey(s) ? stocks[s] : 0;
        }

        public void addStock(string s,long a)
        {
            if (stocks.ContainsKey(s)) stocks[s] += a;
                else stocks.Add(s,a);
        }
    }
}