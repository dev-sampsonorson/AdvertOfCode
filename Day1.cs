using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace AdventOfCode {
    class Day1 {
        private static string inputFile = "day-1-input.txt";


        private static int Part1(int[] db, int target) {
            Dictionary<int, bool> table = new Dictionary<int, bool>();

            int product = 0;
            foreach (int number in db) {
                int toFind = target - number;
                if (table.ContainsKey(toFind)) {
                    product = number * toFind;
                    break;
                }

                table[number] = true;
            }

            return product;
        }
        private static int Part2(int[] db, int target) {
            Dictionary<int, bool> table = new Dictionary<int, bool>();

            int product = 0;
            for (int i = 0; i < db.Length; i++) {
                int newTarget = target - db[i];
                for (int j = i; j < db.Length; j++) {
                    int toFind = newTarget - db[j];
                    if (table.ContainsKey(toFind)) {
                        product = db[i] * db[j] * toFind;
                        break;
                    }

                    table[db[j]] = true;

                }
            }

            return product;
        }


        public static void Main(string[] args) {
            int[] db = File.ReadAllLines(inputFile).Select(int.Parse).ToArray();
            int[] sample = { 1721, 979, 366, 299, 675, 1456 };

            Console.WriteLine($"Part 1 -> Answer: {Part1(db, 2020)}");
            Console.WriteLine($"Part 2 -> Answer: {Part2(db, 2020)}");
        }
    }
}
