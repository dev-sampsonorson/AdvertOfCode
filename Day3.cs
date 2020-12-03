using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AdventOfCode {


    class Day3 {

        private static string inputFile = "day-3-input.txt";

        private class SquareReader : IDisposable {

            private StreamReader fileReader = null;
            private List<List<char>> db = new List<List<char>>();
            private int currentRow = 0;
            private int currentColumn = 0;
            private int lastRowIndex;
            private int lastColumnIndex;
            private int columnCount;

            public SquareReader(string filePath) {
                fileReader = new StreamReader(filePath);
            }

            public void PopulateDb() {
                string line;
                List<char> row;
                while ((line = fileReader.ReadLine()) != null) {
                    row = new List<char>();
                    foreach (char character in line) {
                        row.Add(character);
                    }

                    db.Add(row);
                }

                lastRowIndex = db.Count - 1;
                lastColumnIndex = db[0].Count - 1;
                columnCount = db[0].Count;

                fileReader.Close();
                fileReader.Dispose();
            }

            public char? NextCharacter(Tuple<int, int> slope) {

                // Increament column
                currentColumn = (currentColumn + slope.Item1) % columnCount;
                currentRow += slope.Item2;

                // Valid row
                // Valid column
                if (currentRow > lastRowIndex) return null;
                if (currentColumn > lastColumnIndex) return null;

                // Get character
                char currentChar = db[currentRow][currentColumn];

                return currentChar;
            }

            public void Reset() {
                currentRow = 0;
                currentColumn = 0;
            }

            public void Dispose() {
                fileReader.Dispose();
            }
        }

        private static int Traverse(SquareReader reader, Tuple<int, int> slope) {
            char? character;
            int counter = 0;
            while ((character = reader.NextCharacter(slope)) != null) {
                if (character == '#') counter++;
                //Console.WriteLine(character);
            }

            return counter;
        }

        public static void Main(string[] args) {
            Tuple<int, int> slope = new Tuple<int, int>(3, 1);

            SquareReader reader = new SquareReader(inputFile);
            reader.PopulateDb();
            Console.WriteLine($"Part 1 -> Answer: {Traverse(reader, slope)}");

            Tuple<int, int>[] slopes = new Tuple<int, int>[] {
                new Tuple<int, int>(1, 1),
                new Tuple<int, int>(3, 1),
                new Tuple<int, int>(5, 1),
                new Tuple<int, int>(7, 1),
                new Tuple<int, int>(1, 2)
            };

            long product = 1;
            foreach (Tuple<int, int> item in slopes) {
                reader.Reset();
                product *= Traverse(reader, item);
            }
            Console.WriteLine($"Part 2 -> Answer: {product}");
        }
    }
}
