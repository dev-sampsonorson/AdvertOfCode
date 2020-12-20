using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AdventOfCode.Solutions {
    class Day09 : ISolution {

        List<long> Input;
        long InvalidNumber;

        public async Task ReadInput(string file) {
            Input = (from x in await File.ReadAllLinesAsync(file)
                     select Convert.ToInt64(x)).ToList();

        }

        private bool HasSumOfTwo(List<long> list, long number) {
            Dictionary<long, bool> db = new Dictionary<long, bool>();

            foreach(long n in list) {
                long target = number - n;
                if (db.ContainsKey(target)) {
                    return true;
                }

                db.Add(n, true);
            }

            return false;
        }

        private long FindInvalidNumber() {
            for (int i = 25; i < Input.Count; i++) {
                if (!HasSumOfTwo(Input.GetRange(i - 25, 25), Input[i])) {
                    return Input[i];
                }
            }

            return -1;
        }

        private List<long> Sort(List<long> unsorted) {
            bool swap = false;

            for (int i = 0; i < unsorted.Count; i++) {
                for (int j = i + 1; j < unsorted.Count; j++) {
                    if (unsorted[j] < unsorted[i]) {
                        long temp = unsorted[i];
                        unsorted[i] = unsorted[j];
                        unsorted[j] = temp;

                        swap = true;
                    }
                }

                if (!swap) break;
            }

            return unsorted;
        }

        private long SumOfSmallestAndLargest() {
            bool found = false;
            List<long> contiguousNumbers = new List<long>();

            for (int i = 0; i < Input.Count; i++) {
                long sum = 0;
                for (int j = i; j < Input.Count; j++) {
                    sum += Input[j];
                    contiguousNumbers.Add(Input[j]);

                    if (sum > InvalidNumber) {
                        contiguousNumbers.Clear();
                        break;
                    }

                    if (sum == InvalidNumber) {
                        found = true;
                        break;
                    }
                }

                if (found) break;
            }

            List<long> sortedList = Sort(contiguousNumbers);

            return sortedList[0] + sortedList[sortedList.Count - 1];
        }


        public void Part1() {
            InvalidNumber = FindInvalidNumber();
            Console.WriteLine("Answer -> {0}", InvalidNumber);
        }

        public void Part2() {
            Console.WriteLine("Answer -> {0}", SumOfSmallestAndLargest());
        }
    }
}
