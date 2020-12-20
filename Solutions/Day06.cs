using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Solutions {
    public class Day06 : ISolution {

        private string[] Input;
        private IList<Group> Groups = new List<Group>();

        private class Group {
            public HashSet<char> YesQuestion { get; set; } = new HashSet<char>();
            public Dictionary<char, int> YesQuestionCount { get; set; } = new Dictionary<char, int>();

            public int GroupSize { get; set; }

            public void AddQuestion(char q) {
                YesQuestion.Add(q); // For Part 1

                if (YesQuestionCount.ContainsKey(q)) {
                    YesQuestionCount[q] += 1;
                    return;
                }

                YesQuestionCount.Add(q, 1);
            }
        }

        private void CreateGroups() {
            Group g = new Group();
            for (int i = 0; i <= Input.Length; i++) {
                if (i == Input.Length) {
                    Groups.Add(g);
                    break;
                }

                if (String.IsNullOrEmpty(Input[i])) {
                    Groups.Add(g);
                    g = new Group();
                    continue;
                }

                foreach (char c in Input[i]) {
                    g.AddQuestion(c);
                }
                g.GroupSize += 1;
            }
        }

        public async Task ReadInput(string file) {
            /*
            var totalAnswers = 0;

            foreach (var group in groups) {
                totalAnswers += group
                    .SelectMany(person => person)
                    .ToHashSet()
                    .Count;
            }*/
            Input = await File.ReadAllLinesAsync(file);

            CreateGroups();
        }

        public void Part1() {
            int sum = 0;
            for (int i = 0; i < Groups.Count; i++) {
                sum += Groups[i].YesQuestion.Count;
            }


            Console.WriteLine("Answer -> {0}", sum);
        }

        public void Part2() {

            int sum = 0;
            foreach (var g in Groups) {

                /*
                 * Another way
                int sum = (from item in g.YesQuestionCount
                           where item.Value == g.GroupSize
                           select item).Count();
                */

                foreach(KeyValuePair<char, int> kvp in g.YesQuestionCount) {
                    if (kvp.Value == g.GroupSize)
                        sum += 1;
                }
            }

            Console.WriteLine("Answer -> {0}", sum);
        }
    }
}
