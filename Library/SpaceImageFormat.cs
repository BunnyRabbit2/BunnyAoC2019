using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019
{
    class SpaceImageFormat // Only does black (0) or white (1)
    {
        int width, height;
        int[] imageData;
        List<int[]> layers;
        int[] colourData;

        public SpaceImageFormat(int[] data, int wIn, int hIn)
        {
            imageData = data;
            layers = new List<int[]>();
            width = wIn;
            height = hIn;
            colourData = new int[width * height];
            turnDataIntoLayers();
            createColourData();
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

        void createColourData()
        {
            for (int i = 0; i < width * height; i++)
            {
                colourData[i] = layers[0][i]; // copy first layer into colourData
            }
            for (int l = 1; l < layers.Count; l++)
            {
                for (int p = 0; p < colourData.Length; p++)
                {
                    if (colourData[p] == 2)
                        colourData[p] = layers[l][p];
                }
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

        public void drawImage()
        {
            for (int h = 0; h < colourData.Length; h += width)
            {
                string line = "";
                for (int w = 0; w < width; w++)
                {
                    if (colourData[h + w] == 0)
                        line += "-";
                    else if (colourData[h + w] == 1)
                        line += "#";
                }
                Console.WriteLine(line);
            }
        }
    }
}