using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode.Solutions {
    class Day11 : ISolution {
        public char[,] Seats { get; set; }

        private void PrintSeats(char[,] seats) {
            int rows = seats.GetLength(0);
            int cols = seats.GetLength(1);

            for (int i = 0; i < rows; i++) {
                for (int j = 0; j < cols; j++) {
                    Console.Write(seats[i, j]);
                }
                Console.Write("\n");
            }
        }

        private int CountOccupiedSeats(char[,] seats) {
            int count = 0;
            int rows = seats.GetLength(0);
            int cols = seats.GetLength(1);

            for (int i = 0; i < rows; i++) {
                for (int j = 0; j < cols; j++) {
                    if (seats[i, j] == '#') count += 1;
                }
            }

            return count;
        }

        private int NumberOfOccupiedAdjacentSeats(char[,] seats, int row, int col) {
            int lastRowIndex = seats.GetLength(0) - 1;
            int lastColIndex = seats.GetLength(1) - 1;
            int count = 0;

            // top
            if (row - 1 >= 0 && col - 1 >= 0 && seats[row - 1, col - 1] == '#') count += 1;
            if (row - 1 >= 0 && seats[row - 1, col] == '#') count += 1;
            if (row - 1 >= 0 && col + 1 <= lastColIndex && seats[row - 1, col + 1] == '#') count += 1;

            // sides
            if (col - 1 >= 0 && seats[row, col - 1] == '#') count += 1;
            if (col + 1 <= lastColIndex && seats[row, col + 1] == '#') count += 1;

            // bottom
            if (row + 1 <= lastRowIndex && col - 1 >= 0 && seats[row + 1, col - 1] == '#') count += 1;
            if (row + 1 <= lastRowIndex && seats[row + 1, col] == '#') count += 1;
            if (row + 1 <= lastRowIndex && col + 1 <= lastColIndex && seats[row + 1, col + 1] == '#') count += 1;

            return count;
        }

        private int NumberOfOccupiedVisibleSeats(char[,] seats, int row, int col) {
            int lastRowIndex = seats.GetLength(0) - 1;
            int lastColIndex = seats.GetLength(1) - 1;
            int count = 0;
            int r, c;

            // horizontal
            for (c = col + 1; c <= lastColIndex; c++) {
                if (seats[row, c] == 'L') break;
                if (seats[row, c] == '#') {
                    count += 1;
                    break;
                }
            }

            for (c = col - 1; c >= 0; c--) {
                if (seats[row, c] == 'L') break;
                if (seats[row, c] == '#') {
                    count += 1;
                    break;
                }
            }

            // vertical
            for (r = row + 1; r <= lastRowIndex; r++) {
                if (seats[r, col] == 'L') break;
                if (seats[r, col] == '#') {
                    count += 1;
                    break;
                }
            }

            for (r = row - 1; r >= 0; r--) {
                if (seats[r, col] == 'L') break;
                if (seats[r, col] == '#') {
                    count += 1;
                    break;
                }
            }

            // top-left
            r = row - 1;
            c = col - 1;
            while (r >= 0 && c >= 0) {
                if (seats[r, c] == 'L') break;
                if (seats[r, c] == '#') {
                    count += 1;
                    break;
                }

                r -= 1;
                c -= 1;
            }

            // top-right
            r = row - 1;
            c = col + 1;
            while (r >= 0 && c <= lastColIndex) {
                if (seats[r, c] == 'L') break;
                if (seats[r, c] == '#') {
                    count += 1;
                    break;
                }

                r -= 1;
                c += 1;
            }

            // bottom-left
            r = row + 1;
            c = col - 1;
            while (r <= lastRowIndex && c >= 0) {
                if (seats[r, c] == 'L') break;
                if (seats[r, c] == '#') {
                    count += 1;
                    break;
                }

                r += 1;
                c -= 1;
            }

            // bottom-right
            r = row + 1;
            c = col + 1;
            while (r <= lastRowIndex && c <= lastColIndex) {
                if (seats[r, c] == 'L') break;
                if (seats[r, c] == '#') {
                    count += 1;
                    break;
                }

                r += 1;
                c += 1;
            }

            return count;
        }


        public async Task ReadInput(string file) {
            string[] lines = await File.ReadAllLinesAsync(file);
            Seats = new char[lines.Length, lines[0].Length];

            for(int i = 0; i < lines.Length; i++) {
                for (int j = 0; j < lines[0].Length; j++) {
                    Seats[i, j] = lines[i][j];
                }
            }
        }

        public void Part1() {
            char[,] seats = (char[,])Seats.Clone();
            int rows = Seats.GetLength(0);
            int cols = Seats.GetLength(1);
            int changes, iterations = 0;

            char[,] workingCopy;

            do {
                changes = 0;
                workingCopy = (char[,])seats.Clone();
                
                for (int i = 0; i < rows; i++) {
                    for (int j = 0; j < cols; j++) {
                        // check if adjacent seats
                        int nOccupiedAdjacent = NumberOfOccupiedAdjacentSeats(seats, i, j);
                        if (workingCopy[i, j] == 'L' && nOccupiedAdjacent == 0) {
                            workingCopy[i, j] = '#';
                            changes += 1;
                        }
                
                        if (workingCopy[i, j] == '#' && nOccupiedAdjacent >= 4) {
                            workingCopy[i, j] = 'L';
                            changes += 1;
                        }
                    }
                }

                seats = workingCopy;
                iterations += 1;
            } while (changes > 0);


            Console.WriteLine("Answer -> {0}", CountOccupiedSeats(seats));
        }

        public void Part2() {
            char[,] seats = (char[,])Seats.Clone();
            int rows = Seats.GetLength(0);
            int cols = Seats.GetLength(1);
            int changes, iterations = 0;

            char[,] workingCopy;

            do {
                changes = 0;
                workingCopy = (char[,])seats.Clone();

                for (int i = 0; i < rows; i++) {
                    for (int j = 0; j < cols; j++) {
                        // check if adjacent seats
                        int nOccupiedVisible = NumberOfOccupiedVisibleSeats(seats, i, j);
                        if (workingCopy[i, j] == 'L' && nOccupiedVisible == 0) {
                            workingCopy[i, j] = '#';
                            changes += 1;
                        }

                        if (workingCopy[i, j] == '#' && nOccupiedVisible >= 5) {
                            workingCopy[i, j] = 'L';
                            changes += 1;
                        }
                    }
                }

                seats = workingCopy;
                iterations += 1;
            } while (changes > 0);

            Console.WriteLine("Answer -> {0}", CountOccupiedSeats(seats));

        }
    }
}
