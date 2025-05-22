using System;
using System.Text.RegularExpressions;

namespace ChmodConverterApp
{
    public static class ChmodConverter
    {
        private static readonly string[] NumericToSymbolicMap = new string[]
        {
            "---",
            "--x", 
            "-w-", 
            "-wx", 
            "r--", 
            "r-x", 
            "rw-", 
            "rwx"  
        };

        public static string SymbolicToNumeric(string symbolic)
        {
            if (string.IsNullOrEmpty(symbolic) || symbolic.Length != 9)
                throw new ArgumentException("Symbolic notation must be exactly 9 characters long (e.g., 'rwxr-xr-x').");

            if (!Regex.IsMatch(symbolic, "^[rwx-]{9}$"))
                throw new ArgumentException("Symbolic notation must contain only 'r', 'w', 'x', or '-' characters.");

            string userPerms = symbolic.Substring(0, 3);
            string groupPerms = symbolic.Substring(3, 3);
            string othersPerms = symbolic.Substring(6, 3);

            int userValue = CalculateNumericValue(userPerms);
            int groupValue = CalculateNumericValue(groupPerms);
            int othersValue = CalculateNumericValue(othersPerms);

            return $"{userValue}{groupValue}{othersValue}";
        }

        private static int CalculateNumericValue(string perms)
        {
            int value = 0;
            if (perms[0] == 'r') value += 4;
            if (perms[1] == 'w') value += 2;
            if (perms[2] == 'x') value += 1;
            return value;
        }

        public static string NumericToSymbolic(string numeric)
        {
            if (string.IsNullOrEmpty(numeric) || numeric.Length != 3)
                throw new ArgumentException("Numeric notation must be exactly 3 digits long (e.g., '755').");

            if (!Regex.IsMatch(numeric, "^[0-7]{3}$"))
                throw new ArgumentException("Numeric notation must contain only digits between 0 and 7.");

            int userValue = int.Parse(numeric[0].ToString());
            int groupValue = int.Parse(numeric[1].ToString());
            int othersValue = int.Parse(numeric[2].ToString());

            string userPerms = NumericToSymbolicMap[userValue];
            string groupPerms = NumericToSymbolicMap[groupValue];
            string othersPerms = NumericToSymbolicMap[othersValue];

            return $"{userPerms}{groupPerms}{othersPerms}";
        }
    }
}