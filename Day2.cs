using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace AdventOfCode {
    class Day2 {
        private static string inputFilePath = "day-2-input.txt";

        private class PasswordPolicy {

            public int LowerBound { get; }
            public int UpperBound { get; }
            public char Letter { get; }
            public string Password { get; }

            public PasswordPolicy(int lowerBound, int upperBound, char letter, string password) {
                LowerBound = lowerBound;
                UpperBound = upperBound;
                Letter = letter;
                Password = password;
            }
        }

        static Dictionary<char, int> GetCharacterCount(string stringValue) {
            Dictionary<char, int> table = new Dictionary<char, int>();
            char[] chars = stringValue.ToCharArray();
            foreach(char c in chars) {
                if (!table.ContainsKey(c)) {
                    table.Add(c, 1);
                } else {
                    table[c] = table[c] + 1;
                }
            }

            return table;
        }

        static void GetFieldsInRow(string[] db, Action<PasswordPolicy> callback) {
            foreach (string row in db) {
                string[] fields = row.Split(new char[] { '-', ' ', ':' }, StringSplitOptions.RemoveEmptyEntries);

                callback(new PasswordPolicy(
                    Convert.ToInt32(fields[0]), // lower bound
                    Convert.ToInt32(fields[1]),  // upper bound
                    Convert.ToChar(fields[2]), // letter
                    fields[3] // password
                ));
            }
        }

        static int Part1(string[] db) {
            int validPasswordCount = 0;

            GetFieldsInRow(db, policy => {
                Dictionary<char, int> characterCount = GetCharacterCount(policy.Password);

                if (!characterCount.ContainsKey(policy.Letter))
                    return;

                if (policy.LowerBound > characterCount[policy.Letter] || policy.UpperBound < characterCount[policy.Letter])
                    return;

                validPasswordCount += 1;
            });

            return validPasswordCount;
        }

        static int Part2(string[] db) {
            int validPasswordCount = 0;

            GetFieldsInRow(db, policy => {
                char charAtLowerRange = policy.Password[policy.LowerBound - 1];
                char charAtUpperRange = policy.Password[policy.UpperBound - 1];

                if (policy.Letter == charAtLowerRange && policy.Letter == charAtUpperRange)
                    return;

                if (policy.Letter != charAtLowerRange && policy.Letter != charAtUpperRange)
                    return;


                validPasswordCount += 1;

            });

            return validPasswordCount;
        }

        static void Main(string[] args) {
            string[] db = File.ReadAllLines(inputFilePath);

            Console.WriteLine($"Part 1 -> Answer: {Part1(db)}");
            Console.WriteLine($"Part 2 -> Answer: {Part2(db)}");

            Console.ReadLine();
        }
    }
}
