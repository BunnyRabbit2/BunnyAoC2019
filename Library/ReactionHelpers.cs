using System.Collections.Generic;

namespace AdventOfCode2019
{
    public class ChemReaction
    {
        public static Dictionary<string, ChemReaction> Reactions;
        public static List<ChemReaction>[] Stages;

        public Dictionary<string,int> Input {get;}
        public KeyValuePair<string,int> Output {get;}
        public int Stage = 0;

        public ChemReaction(Dictionary<string,int> i, KeyValuePair<string,int> o)
        {
            Input = i;
            Output = o;
        }

        void calculateStagesHelp()
        {
            Stage = 1;
            foreach( var kvp in Input)
            {
                if(kvp.Key != "ORE")
                {
                    var r = Reactions[kvp.Key];
                    if(r.Stage == 0)
                        r.calculateStagesHelp();
                    if(Stage <= r.Stage)
                    {
                        Stage = r.Stage + 1;
                    }
                }
            }
        }

        public static void CalculateStages()
        {
            ChemReaction finalR = Reactions["FUEL"];
            finalR.calculateStagesHelp();
            int lastStage = finalR.Stage;

            Stages = new List<ChemReaction>[lastStage];
            for(int i=0; i < lastStage; i++)
            {
                Stages[i] = new List<ChemReaction>();
                foreach(ChemReaction r in Reactions.Values)
                {
                    Stages[r.Stage-1].Add(r);
                }
            }
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