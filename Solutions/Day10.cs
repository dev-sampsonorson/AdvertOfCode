using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AdventOfCode.Solutions {
    class Day10 : ISolution {

        private Bag MyBag;
        private int FirstAdapter;
        private int LastAdapter;

        private class Bag {
            public List<int> Adapters { get; set; } = new List<int>();

            public List<int> Sort() {
                List<int> clone = Adapters.GetRange(0, Adapters.Count); // Adapters.Select(x => x).ToList();
                for (int i = 0; i < clone.Count; i++) {
                    for (int j = i + 1; j < clone.Count; j++) {
                        if (clone[j] < clone[i]) {
                            int temp = clone[i];
                            clone[i] = clone[j];
                            clone[j] = temp;
                        }
                    }
                }

                return clone;
            }
        }

        private List<int> GetPossibleNodes(int n) {
            List<int> r = new List<int>();

            for (int i = 1; i <= 3; i++)
                r.Add(n + i);

            return r;
        }

        private void FindPaths(List<List<int>> paths, List<int> path) {
            int lastNode = path.Last();

            // determine possible landing nodes
            List<int> possibleNodes = GetPossibleNodes(lastNode);

            // filter out invalid landing nodes
            possibleNodes = possibleNodes.Where(x => MyBag.Adapters.Contains(x)).OrderBy(x => x).ToList();

            if (possibleNodes.Count == 0)
                return;

            List<int> dup = path.GetRange(0, path.Count);

            // add the first node in landing nodes to path and call FindPath
            path.Add(possibleNodes.First());
            FindPaths(paths, path);

            // iterate through the remaining landing nodes
            // duplicate path
            // add node to path
            // call FindPath
            for (int i = 1; i < possibleNodes.Count; i++) {
                List<int> newPath = dup.GetRange(0, dup.Count);
                newPath.Add(possibleNodes[i]);
                paths.Add(newPath);
                FindPaths(paths, newPath);
            }
        }

        public async Task ReadInput(string file) {
            MyBag = new Bag();
            FirstAdapter = 0;

            MyBag.Adapters = (from x in await File.ReadAllLinesAsync(file)
                              select Convert.ToInt32(x)).OrderBy(x => x).ToList();
            MyBag.Adapters.Insert(0, 0);

            LastAdapter = MyBag.Adapters.Max() + 3;
            MyBag.Adapters.Add(LastAdapter);
        }

        public void Part1() {
            int differenceOfOne = 0;
            int differenceOfThree = 0;
            // List<int> sorted = MyBag.Sort();

            int chargingOutletJoltage = 0;

            for (int i = 0; i < MyBag.Adapters.Count - 1; i++) {
                List<int> joltChoices = new List<int>();

                for (int j = chargingOutletJoltage + 1; j <= chargingOutletJoltage + 3; j++)
                    joltChoices.Add(j);

                int choosenAdapterJolts = MyBag.Adapters.First(x => joltChoices.Contains(x));
                int diff = choosenAdapterJolts - chargingOutletJoltage;

                chargingOutletJoltage = choosenAdapterJolts;

                if (diff == 1)
                    differenceOfOne += 1;

                if (diff == 3)
                    differenceOfThree += 1;
            }

            Console.WriteLine("Answer -> {0}", differenceOfOne * differenceOfThree);
        }

        public void Part2() {
            //List<List<int>> paths = new List<List<int>>();
            //paths.Add(new List<int> { FirstAdapter });

            //FindPaths(paths, paths.Last());
            // paths.Where(x => x.Last() == LastAdapter).Count()

            int len = MyBag.Adapters.Count;
            var paths = new long[len];

            paths[0] = 1;
            for (int i = 1; i < MyBag.Adapters.Count; i++) {
                for (int j = 0; j < i; j++) {
                    // is there a valid path from i -> j
                    if (MyBag.Adapters[i] - MyBag.Adapters[j] <= 3) {
                        // add all the paths from the j node
                        paths[i] += paths[j];
                    }
                }
            }

            Console.WriteLine("Answer -> {0}", paths[len - 1]);

        }
    }
}
