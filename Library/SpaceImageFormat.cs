using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019
{
    class SpaceImageFormat
    {
        int width, height;
        int[] imageData;
        List<int[]> layers;

        public SpaceImageFormat(int[] data, int wIn, int hIn)
        {
            imageData = data;
            layers = new List<int[]>();
            width = wIn;
            height = hIn;

            turnDataIntoLayers();
        }
        void turnDataIntoLayers()
        {
            int pixelsPerLayer = width * height;
            int noOfLayers = imageData.Length / pixelsPerLayer;

            for (int i = 0; i < noOfLayers; i++)
            {
                int[] newLayer = new int[pixelsPerLayer];
                int startIndex = i * pixelsPerLayer;

                for (int j = 0; j < pixelsPerLayer; j++)
                {
                    newLayer[j] = imageData[startIndex + j];
                }

                layers.Add(newLayer);
            }
        }

        public int verifyData()
        {
            int[] noOfZeroes = new int[layers.Count];

            for (int i = 0; i < layers.Count; i++)
            {
                int zeroes = 0;

                foreach (int n in layers[i])
                {
                    if (n == 0)
                        zeroes++;
                }

                noOfZeroes[i] = zeroes;
            }

            int fewestZeroes = Array.IndexOf(noOfZeroes, noOfZeroes.Min());

            int noOfOnes = 0;
            int noOfTwos = 0;
            foreach (int n in layers[fewestZeroes])
            {
                if (n == 1)
                    noOfOnes++;
                else if (n == 2)
                    noOfTwos++;
            }

            return noOfOnes * noOfTwos;
        }
    }
}