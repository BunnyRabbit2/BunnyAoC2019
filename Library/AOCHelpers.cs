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

        public static float GetDistanceBetweenTwoPoints(float x1, float y1, float x2, float y2)
        {
            return (float)Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));
        }

        public static float GetBearingBetweenTwoPoints(float x1, float y1, float x2, float y2, bool returnDegrees = true)
        {
            double a, o;

            if (y1 < y2)
                a = y2 - y1;
            else
                a = y1 - y2;

            if (x1 < x2)
                o = x2 - x1;
            else
                o = x1 - x2;

            float theta = (float)Math.Atan(o / a);

            theta = theta * (180.0f / 3.142f);

            if (y1 < y2)
            {
                // south of player
                if (x1 > x2)
                    // east of player
                    theta = 180.0f + theta;
                else
                    // west of player
                    theta = 180.0f - theta;
            }
            else
            {
                // north of player
                if (x1 > x2)
                {
                    theta = 360.0f - theta;
                }
            }

            if (returnDegrees)
                return theta;
            else
                return theta * (3.142f / 180.0f);
        }
    }
}