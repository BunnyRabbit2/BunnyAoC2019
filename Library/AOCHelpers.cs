using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019
{
    class AOCHelpers
    {
        // Got this code from http://csharphelper.com/blog/2014/08/generate-all-of-the-permutations-of-a-set-of-objects-in-c/
        public static List<List<T>> GeneratePermutations<T>(List<T> items)
        {
            // Make an array to hold the permutation we are building.
            T[] current_permutation = new T[items.Count];

            // Make an array to tell whether an item is in the current selection.
            bool[] in_selection = new bool[items.Count];

            // Make a result list.
            List<List<T>> results = new List<List<T>>();

            // Build the combinations recursively.
            PermuteItems<T>(items, in_selection,
                current_permutation, results, 0);

            // Return the results.
            return results;
        }

        // Recursively permute the items that are not yet in the current selection.
        private static void PermuteItems<T>(List<T> items, bool[] in_selection,
            T[] current_permutation, List<List<T>> results,
            int next_position)
        {
            // See if all of the positions are filled.
            if (next_position == items.Count)
            {
                // All of the positioned are filled. Save this permutation.
                results.Add(current_permutation.ToList());
            }
            else
            {
                // Try options for the next position.
                for (int i = 0; i < items.Count; i++)
                {
                    if (!in_selection[i])
                    {
                        // Add this item to the current permutation.
                        in_selection[i] = true;
                        current_permutation[next_position] = items[i];

                        // Recursively fill the remaining positions.
                        PermuteItems<T>(items, in_selection,
                            current_permutation, results,
                            next_position + 1);

                        // Remove the item from the current permutation.
                        in_selection[i] = false;
                    }
                }
            }
        }

        // Code from http://www.rosettacode.org/wiki/Greatest_common_divisor#C.23
        public static int GCD(int a, int b)
        {
            while (b != 0) b = a % (a = b);
            return a;
        }
    }
}