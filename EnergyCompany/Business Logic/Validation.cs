using System;

internal class Validation : Methods, IValidation
{
    private Methods _methods = new Methods();

    //vars
    private readonly int[] options = { 1, 2, 3, 4, 5, 6 };
    internal static bool exists;
    internal static bool close = false;

    public void Verify()
    {
        if (string.IsNullOrEmpty(input))
        {
            Console.WriteLine("\nYour input is empty or invalid! Please, try again.");
            input = Console.ReadLine();
            Verify();
        }

        if (!int.TryParse(input, out int value))
        {
            Console.WriteLine("Your input must be a valid number! Please, try again.");
            input = Console.ReadLine();
            Verify();
        }

        if (close)
        {
            if (input == "0")
            {
                _methods.Print("\nChoose another option then.", ConsoleColor.White);
                _methods.ShowOptions();
                close = false;
                input = Console.ReadLine();
                _methods.CallSwitch();
            }

            else if (input == "1")
            {
                _methods.Print("The application has been closed", ConsoleColor.Red);
                Environment.Exit(0);
            }

            else
            {
                Console.WriteLine("Your input must be either (0) for NO or (1) for YES.");
                input = Console.ReadLine();
                Verify();
            }
        }
    }
    public void ValidateOptions()
        {
            exists = Array.BinarySearch(options, Int32.Parse(input)) >= 0;

            while (!exists)
            {
                _methods.Print("\nPlease, type a valid option!\n", ConsoleColor.White);

                input = Console.ReadLine();

                Verify();

                exists = Array.BinarySearch(options, Int32.Parse(input)) >= 0;
            }
        }
}
