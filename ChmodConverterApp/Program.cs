using System;

namespace ChmodConverterApp
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Usage: chmodconverter <permission>");
                Console.WriteLine("Example: chmodconverter rwxrwxrwx");
                Console.WriteLine("Example: chmodconverter 777");
                return;
            }

            string input = args[0];

            try
            {
                if (input.Length == 9 && IsSymbolic(input))
                {
                    string numeric = ChmodConverter.SymbolicToNumeric(input);
                    Console.WriteLine(numeric);
                }
                else if (input.Length == 3 && IsNumeric(input))
                {
                    string symbolic = ChmodConverter.NumericToSymbolic(input);
                    Console.WriteLine(symbolic);
                }
                else
                {
                    Console.WriteLine("Invalid input. Use symbolic (e.g., 'rwxrwxrwx') or numeric (e.g., '777') format.");
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private static bool IsSymbolic(string input)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(input, "^[rwx-]{9}$");
        }

        private static bool IsNumeric(string input)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(input, "^[0-7]{3}$");
        }
    }
}