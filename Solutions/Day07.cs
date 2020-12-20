using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AdventOfCode.Solutions {
    public class Day07 : ISolution {

        List<Bag> Input;

        private class Bag {
            public string Color { get; set; }
            public Dictionary<Bag, int> Holds { get; set; }

            public Bag() {
                Holds = new Dictionary<Bag, int>();
            }

            public bool CanHaveGoldBag() {
                if (Holds.Keys.Any(x => x.Color == "shiny gold")) return true;

                foreach(Bag b in Holds.Keys) {
                    if (b.CanHaveGoldBag()) {
                        return true;
                    }
                }

                return false;
            }

            public int CountBags() {
                int sum = Holds.Sum(x => x.Value);

                foreach(KeyValuePair<Bag, int> pair in Holds) {
                    sum += pair.Value * pair.Key.CountBags();
                }

                return sum;
            }
        }

        public async Task ReadInput(string file) {
            Input = new List<Bag>();
            List<string> lines = (await File.ReadAllLinesAsync(file)).ToList();

            foreach(string line in lines) {
                string[] parts = line.Split(" bags contain ");
                string bagColor = parts[0];

                Bag bag = Input.SingleOrDefault(c => c.Color == bagColor);
                if (bag == null) {
                    bag = new Bag { Color = bagColor };
                    Input.Add(bag);
                }

                if (parts[1].EndsWith("no other bags."))
                    continue;

                string part1 = parts[1].Replace("bags", "").Replace("bag", "").Replace(".", "");
                foreach (string contentPart in part1.Split(",")) {
                    string cp = contentPart.Trim();
                    string c = cp[cp.IndexOf(" ")..].Trim();
                    int n = Convert.ToInt32(cp[0..cp.IndexOf(" ")]);

                    Bag innerBag = Input.SingleOrDefault(x => x.Color == c);
                    if (innerBag == null) {
                        innerBag = new Bag { Color = c };
                        Input.Add(innerBag);
                    }

                    bag.Holds.Add(innerBag, n);
                }
            }
        }

        public void Part1() {
            int count = 0;
            foreach(Bag b in Input) {
                if (b.CanHaveGoldBag()) {
                    count += 1;
                }
            }

            Console.WriteLine("Answer -> {0}", count);
        }

        public void Part2() {
            var bag = Input.SingleOrDefault(x => x.Color == "shiny gold");

            Console.WriteLine("Answer -> {0}", bag.CountBags());
        }
    }
}
