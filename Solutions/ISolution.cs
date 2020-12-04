using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Solutions {
    public interface ISolution {
        Task ReadInput(string file);
        void Part1();
        void Part2();
    }
}
