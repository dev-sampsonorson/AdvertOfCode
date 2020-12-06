using System;
using System.Threading.Tasks;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode.Solutions {
    public class Day05 : ISolution {

        private List<Seat> Inputs;

        private class Seat {
            public string BoardingPass { get; set; }
            public int Row { get; set; }
            public int Column { get; set; }
            public int SeatId { get; set; }
        }

        private int GetRow(int min, int max, int index, string seatNumber, char front) {
            if (min == max) return min;

            char character = seatNumber[index];
            int middle = character == front
                ? (int)Math.Floor((min + max) / 2d)
                : (int)Math.Ceiling((min + max) / 2d);

            return character == front
                ? GetRow(min, middle, index + 1, seatNumber, front)
                : GetRow(middle, max, index + 1, seatNumber, front);


        }

        private void SortSeats() {
            bool swap = false;
            for (int i = 0; i < Inputs.Count; i++) {
                for (int j = i + 1; j < Inputs.Count; j++) {
                    if (Inputs[j].SeatId < Inputs[i].SeatId) {
                        Seat temp = Inputs[i];
                        Inputs[i] = Inputs[j];
                        Inputs[j] = temp;

                        swap = true;
                    }
                }

                if (!swap) break;
            }

        }

        public async Task ReadInput(string file) {
            Inputs = (from item in (await File.ReadAllLinesAsync(file))
                      select new Seat { BoardingPass = item }).ToList();
        }

        public void Part1() {
            int highestSeatId = 0;
            for (int i = 0; i < Inputs.Count; i++) {
                Inputs[i].Row = GetRow(0, 127, 0, Inputs[i].BoardingPass[0..7], 'F');
                Inputs[i].Column = GetRow(0, 7, 0, Inputs[i].BoardingPass[7..], 'L');
                Inputs[i].SeatId = (Inputs[i].Row * 8) + Inputs[i].Column;

                if (Inputs[i].SeatId > highestSeatId)
                    highestSeatId = Inputs[i].SeatId;
            }

            Console.WriteLine("Answer -> {0}", highestSeatId);
        }

        public void Part2() {
            SortSeats();

            int mySeatId = 0;
            for (int i = 0; i < Inputs.Count; i++) {
                int diff = Inputs[i + 1].SeatId - Inputs[i].SeatId;
                if (diff == 2) {
                    mySeatId = Inputs[i].SeatId + 1;
                    break;
                }
            }

            Console.WriteLine("Answer -> {0}", mySeatId);
        }
    }
}
