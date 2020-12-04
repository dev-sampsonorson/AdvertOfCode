using AdventOfCode.Solutions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode {

    // TODO: More improvements
    public class Day04 : ISolution {

        private static string[] requiredInfo = { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid" };
        private static List<string> Inputs;

        private static bool HasRequiredInfo(List<string[]> data) {
            return requiredInfo.All(x => data.Select(item => item[0]).Contains(x));
        }

        private static bool HasValidBirthYear(string value) {
            if (!Int32.TryParse(value, out int valueInt))
                return false;

            if (value.Length != 4)
                return false;

            if (valueInt < 1920 || valueInt > 2002)
                return false;

            return true;
        }

        private static bool HasValidIssueYear(string value) {
            if (!Int32.TryParse(value, out int valueInt))
                return false;

            if (value.Length != 4)
                return false;

            if (valueInt < 2010 || valueInt > 2020)
                return false;

            return true;
        }

        private static bool HasValidExpirationYear(string value) {
            if (!Int32.TryParse(value, out int valueInt))
                return false;

            if (value.Length != 4)
                return false;

            if (valueInt < 2020 || valueInt > 2030)
                return false;

            return true;
        }

        private static bool HasValidHeight(string value) {
            if (value.Length < 4)
                return false;

            string valueUnit = value.Substring(value.Length - 2, 2);

            if (!Int32.TryParse(value.Substring(0, value.Length - 2), out int valueNumber)) {
                return false;
            }

            if (valueUnit == "cm" && (valueNumber < 150 || valueNumber > 193))
                return false;

            if (valueUnit == "in" && (valueNumber < 59 || valueNumber > 76))
                return false;

            return true;
        }

        private static bool HasValidHairColor(string value) {
            char[] validCharactes = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f' };
            if (value.Length != 7)
                return false;

            for (int i = 0; i < value.Length; i++) {
                if (i == 0 && value[i] != '#')
                    return false;

                if (i > 0 && !validCharactes.Contains(value[i]))
                    return false;
                //Console.WriteLine(value);
            }
            return true;
        }

        private static bool HasValidEyeColor(string value) {
            string[] eyeColorOptions = new string[] { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };

            string result = eyeColorOptions.SingleOrDefault(x => x == value);

            if (result == null)
                return false;

            return true;
        }

        private static bool HasValidPassportId(string value) {
            if (value.Length != 9)
                return false;

            if (!Int32.TryParse(value, out int passportId))
                return false;

            return true;
        }

        private static int Part1(List<string> inputs) {
            int valid = 0;
            foreach (var input in inputs) {
                List<string[]> passportInfo = input.Split(" ").Select(x => x.Split(":")).ToList();

                if (HasRequiredInfo(passportInfo)) {
                    valid += 1;
                }
            }

            return valid;
        }

        private static int Part2(List<string> inputs) {
            int valid = 0;
            foreach (var input in inputs) {
                List<string[]> passportInfo = input.Split(" ").Select(x => x.Split(":")).ToList();

                // Check if the passport has the required info
                if (!HasRequiredInfo(passportInfo))
                    continue;

                string birthYear = passportInfo.Find(x => x[0] == "byr")[1];
                string issueYear = passportInfo.Find(x => x[0] == "iyr")[1];
                string expirationYear = passportInfo.Find(x => x[0] == "eyr")[1];
                string height = passportInfo.Find(x => x[0] == "hgt")[1];
                string hairColor = passportInfo.Find(x => x[0] == "hcl")[1];
                string eyeColor = passportInfo.Find(x => x[0] == "ecl")[1];
                string passwordId = passportInfo.Find(x => x[0] == "pid")[1];

                // Check if byr is valid
                if (!HasValidBirthYear(birthYear))
                    continue;

                // Check if iyr is valid
                if (!HasValidIssueYear(issueYear))
                    continue;

                // Check if eyr is valid
                if (!HasValidExpirationYear(expirationYear))
                    continue;

                // Check if hgt is valid
                if (!HasValidHeight(height))
                    continue;

                // Check if hcl is valid
                if (!HasValidHairColor(hairColor))
                    continue;

                // Check if ecl is valid
                if (!HasValidEyeColor(eyeColor))
                    continue;

                // Check if pid is valid
                if (!HasValidPassportId(passwordId))
                    continue;


                valid += 1;
            }

            return valid;
        }

        public async Task ReadInput(string file) {
            Inputs = new List<string>();
            using (StreamReader reader = new StreamReader(file)) {
                string line;
                string record = string.Empty;
                while (true) {
                    line = await reader.ReadLineAsync();

                    if (line == null && record.Length > 0) {
                        Inputs.Add(record);
                        break;
                    }

                    record += $" {line.Trim()}";

                    if (line != string.Empty)
                        continue;

                    Inputs.Add(record);
                    record = string.Empty;
                }
            }
        }

        public void Part1() {
            Console.WriteLine("Answer -> {0}", Part1(Inputs));
        }

        public void Part2() {
            Console.WriteLine("Answer -> {0}", Part2(Inputs));
        }
    }
}
