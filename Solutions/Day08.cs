using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AdventOfCode.Solutions {
    class Day08 : ISolution {

        Host Runtime;

        private class Instruction {
            public string Command { get; set; }
            public int Argument { get; set; }
            public int VisitCount { get; set; }
        }

        private class Host {

            public List<Instruction> Instructions { get; set; }
            public int Accumulator { get; set; }

            public Host() {
                Instructions = new List<Instruction>();
            }

            public int Run() {
                int instructionIndex = 0;

                while(instructionIndex < Instructions.Count) {
                    var instruction = Instructions[instructionIndex];
                    instruction.VisitCount += 1;

                    if (instruction.VisitCount > 1) {
                        return instruction.VisitCount;
                    }


                    switch (instruction.Command) {
                        case "acc": {
                            Accumulator += instruction.Argument;
                            break;
                        }
                        case "jmp": {
                            instructionIndex += instruction.Argument;
                            continue;
                        }
                        case "nop": {
                            break;
                        }
                    }

                    instructionIndex += 1;

                }


                return 0;
            }

            public void Reset() {
                Accumulator = 0;
                Instructions.ForEach(x => x.VisitCount = 0);
            }
        }

        public async Task ReadInput(string file) {
            Runtime = new Host();
            string[] input = await File.ReadAllLinesAsync(file);
            foreach(string line in input) {
                string[] parts = line.Split(" ");
                Runtime.Instructions.Add(new Instruction { Command = parts[0], Argument = Convert.ToInt32(parts[1]) });
            }
        }
        public void Part1() {
            Runtime.Run();
            Console.WriteLine("Answer -> {0}", Runtime.Accumulator);

        }

        public void Part2() {
            foreach(var instruction in Runtime.Instructions) {
                if (SwapJmpNop(instruction)) {
                    if (Runtime.Run() == 0) {
                        Console.WriteLine("Answer -> {0}", Runtime.Accumulator);
                        break;
                    }

                    SwapJmpNop(instruction);
                }

                Runtime.Reset();
            }
        }

        private bool SwapJmpNop(Instruction instruction) {
            if (instruction.Command == "jmp" || instruction.Command == "nop") {
                instruction.Command = instruction.Command == "jmp" ? "nop" : "jmp";
                return true;
            }

            return false;
        }
    }
}
