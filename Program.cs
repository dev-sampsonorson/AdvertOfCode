using AdventOfCode.Solutions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode {
    class Program {

        public static async Task Main(string[] args) {
            // What day
            int day = 11; // DateTime.Now.Day;
            if (args.Length > 0) {
                day = Convert.ToInt32(args[0]);
            }

            // What input file
            string file = $"Inputs/day-{day:00}.txt";
            if (!File.Exists(file)) {
                Console.WriteLine($"Input file not found => {file}");
                return;
            }

            // Create solution instance
            ISolution solution = CreateSolution(day);

            if (solution == null) {
                Console.WriteLine($"Solution not found => {solution}");
                return;
            }

            // Read input
            await solution.ReadInput(file);

            // Execute part1
            Console.WriteLine($"Part 1 ->");
            solution.Part1();
            Console.WriteLine(Environment.NewLine);

            // Execute part2
            Console.WriteLine($"Part 2 ->");
            solution.Part2();
        }

        private static ISolution CreateSolution(int day) {
            var className = $"Day{day:00}";
            var assembly = Assembly.GetExecutingAssembly();
            Type type = assembly.GetTypes().SingleOrDefault(x => x.Name == className);

            return type != null ? Activator.CreateInstance(type) as ISolution : null;
        }
    }
}
