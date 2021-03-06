﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace TreeCounter
{
    public static class Program
    {
        public static async Task Main(string[] _)
        {
            var input = await File.ReadAllLinesAsync("../input.txt");
            var lineLength = input[0].Length;

            var treeSlices = Enumerable
                .Range(0, lineLength)
                .Select(columnIndex =>
                    // convert lines to columns
                    input // alle Zeilen
                    .Select(line => line[columnIndex]) // aus einer Zeile Character an Stelle X
                    .ToArray()
                ).ToArray();

            var r3d1 = EvaluateMovement(treeSlices, 3, 1);
            var r1d1 = EvaluateMovement(treeSlices, 1, 1);
            var r5d1 = EvaluateMovement(treeSlices, 5, 1);
            var r7d1 = EvaluateMovement(treeSlices, 7, 1);
            var r1d2 = EvaluateMovement(treeSlices, 1, 2);

            Console.WriteLine("Final Product: " + (r3d1 * r1d1 * r5d1 * r7d1 * r1d2));
        }

        private static int EvaluateMovement(char[][] treeSlices, int rightMoves, int downMoves) {
            var sliceGenerator = SliceGenerator(treeSlices);
            var sliceEnumerator = sliceGenerator.GetEnumerator();
            sliceEnumerator.MoveNext();

            var horPos = 0;
            var vertPos = 0;
            var treeCount = 0;

            while (vertPos < treeSlices[0].Length - 1)
            {
                for(var moveRight = 0; moveRight < rightMoves; moveRight++) {
                    sliceEnumerator.MoveNext();
                }
                horPos += rightMoves;

                vertPos += downMoves;

                if (sliceEnumerator.Current[vertPos] == '#')
                {
                    treeCount++;
                }
            }

            Console.WriteLine($"Sliding {rightMoves} right and {downMoves} down the forest you've encountered {treeCount} trees and moved {horPos} meters right!");

            return treeCount;
        }

        private static void PrintSlice(char[] slice, int horPos, int vertPos = -1)
        {
            for (var index = 0; index < slice.Length; index++)
            {
                Console.SetCursorPosition(horPos, index);
                if (vertPos == index)
                {
                    Console.Write(slice[index] == '#' ? 'X' : '0');
                }
                else
                {
                    Console.Write(slice[index]);
                }
            }
        }

        private static IEnumerable<char[]> SliceGenerator(char[][] slices)
        {
            while (true)
            {
                foreach (var slice in slices)
                {
                    yield return slice;
                }
            }
        }
    }
}
