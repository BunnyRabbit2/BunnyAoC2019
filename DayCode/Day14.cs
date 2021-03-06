using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019
{
    public class Day14
    {
        public static void solveDay14()
        {
            Day14 d14 = new Day14();
            d14.loadInputs("inputs/Day14.txt");
            d14.solvePuzzle1();
            d14.solvePuzzle2();
        }

        bool inputsLoaded;
        Dictionary<string, ChemReaction> reactions;
        ChemStocks stocks;

        public Day14()
        {
            inputsLoaded = false;
            stocks = new ChemStocks();
        }

        public void loadInputs(string fileLocation)
        {
            if (File.Exists(fileLocation))
            {
                string[] lines = File.ReadAllText(fileLocation).Split(Environment.NewLine);

                reactions = lines
                .Select(l => l.Split(new[] { " => " }, 0))
                .Select(a => new { Inputs = a[0].Split(new[] { ", " }, 0), Output = a[1].Split(' ') })
                .Select(t => new { Inputs = t.Inputs.Select(i => i.Split(' ')).ToDictionary(a => a[1], a => int.Parse(a[0])), Output = new KeyValuePair<string, int>(t.Output[1], int.Parse(t.Output[0])) })
                .ToDictionary(r => r.Output.Key, r => new ChemReaction(r.Inputs, r.Output));

                inputsLoaded = true;
            }
            else
            {
                Console.WriteLine("Day14: Invalid File Location");
            }
        }

        public void solvePuzzle1()
        {
            if (!inputsLoaded)
                return;

            long result = oreForFuel(1);

            Console.WriteLine("Day14: Puzzle 1 solution - " + result);
        }

        // Only provides a solution if puzzle 1 has been run first to find the station placement
        public void solvePuzzle2()
        {
            if (!inputsLoaded)
                return;

            stocks.stocks = new Dictionary<string, long> { { "ORE", 1000000000000 } };

            Func<string, long, bool> tryMake = null;
            tryMake = (target, amount) =>
            {
                var reaction = reactions[target];
                var runs = (long)Math.Ceiling(amount / (double)reaction.Output.Value);
                if (reaction.Inputs.Any(input => stocks.getStock(input.Key) < runs * input.Value && input.Key == "ORE"))
                {
                    return false;
                }

                var stockBackup = stocks.stocks.ToDictionary(a => a.Key, a => a.Value);
                while (reaction.Inputs.Any(input => stocks.getStock(input.Key) < runs * input.Value))
                {
                    var mustMake = reaction.Inputs.First(input => stocks.getStock(input.Key) < runs * input.Value);
                    var need = runs * mustMake.Value - stocks.getStock(mustMake.Key);
                    if (!tryMake(mustMake.Key, need))
                    {
                        stocks.stocks = stockBackup;
                        return false;
                    }
                }

                foreach (var input in reaction.Inputs)
                {
                    stocks.addStock(input.Key, -runs * input.Value);
                }

                stocks.addStock(target, runs * reaction.Output.Value);

                return true;
            };

            var mf = 1000000;
            while (mf > 0)
            {
                while (tryMake("FUEL", mf)) {}
                mf /= 10;
            }
           
            long result = stocks.stocks["FUEL"];

            Console.WriteLine("Day14: Puzzle 2 solution - " + result);
        }

        long oreForFuel(long fuelToMake)
        {
            Dictionary<string, long> deficits = new Dictionary<string, long> { { "FUEL", fuelToMake } };

            while (deficits.Any(kvp => kvp.Key != "ORE" && kvp.Value > 0))
            {
                var fill = deficits.First(kvp => kvp.Key != "ORE" && kvp.Value > 0);
                var reac = reactions[fill.Key];
                deficits[fill.Key] -= reac.Output.Value;
                foreach (var input in reac.Inputs)
                {
                    if (deficits.ContainsKey(input.Key))
                        deficits[input.Key] += input.Value;
                    else
                        deficits.Add(input.Key, input.Value);
                }
            }

            return deficits["ORE"];
        }
    }
}