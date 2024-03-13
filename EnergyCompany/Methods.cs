using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace EnergyCompany
{
    public static class Methods
    {
        public static void Startup(string appName, string appVersion, string appAuthor)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("{0}: Version {1} by {2}", appName, appVersion, appAuthor);
            Console.WriteLine();

            Console.ResetColor();

            Console.WriteLine("Choose an option by typing their corresponding number.");
        }

        public static void ShowOptions()
        {
            Console.WriteLine("(1) Insert a new endpoint.");
            Console.WriteLine("(2) Edit an existing endpoint.");
            Console.WriteLine("(3) Delete an existing endpoint.");
            Console.WriteLine("(4) List all endpoints.");
            Console.WriteLine("(5) Find an endpoint by Endpoint Serial Number.");
            Console.WriteLine("(6) Exit.");
        }

        public static string Validate(string input, int option)
        {
            //option 0 = string
            //option 1 = int
            //option 2 = meter model id
            //option 3 = switch state
            //option 4 = delete

            int? inputInt = null;
                
            switch (option)
            {
                case 0:
                    if (string.IsNullOrWhiteSpace(input))
                    {
                        Console.WriteLine("\nYour input is empty or invalid! Please, try again.");
                        input = Console.ReadLine();
                        input = Validate(input, option);
                    }
                    break;
                case 1:
                    if (!string.IsNullOrWhiteSpace(input))
                    {
                        if (!int.TryParse(input, out int value))
                        {
                            Console.WriteLine("\nYour input must be an integer! Please, try again.");
                            input = Console.ReadLine();
                            input = Validate(input, option);
                        }
                        else
                        {
                            inputInt = Int32.Parse(input);
                        }
                    }
                    else
                    {
                        Console.WriteLine("\nYour input is empty or invalid! Please, try again.");
                        input = Console.ReadLine();
                        input = Validate(input, option);
                    }
                    break;
                case 2:
                    if (!string.IsNullOrWhiteSpace(input))
                    {
                        if (!int.TryParse(input, out int value))
                        {
                            Console.WriteLine("\nYour input must be an integer! Please, try again.");
                            input = Console.ReadLine();
                            input = Validate(input, option);
                        }
                        else
                        {
                            inputInt = Int32.Parse(input);
                            if (Array.BinarySearch(new int[] { 16, 17, 18, 19 }, inputInt) < 0)
                            {
                                Console.WriteLine("Your input is invalid! Please, type a number between 16 and 19 for the Meter Model Id.");
                                input = Console.ReadLine();
                                input = Validate(input, option);
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("\nYour input is empty or invalid! Please, try again.");
                        input = Console.ReadLine();
                        input = Validate(input, option);
                    }
                    break;
                case 3:
                    if (!string.IsNullOrWhiteSpace(input))
                    {
                        if (!int.TryParse(input, out int value))
                        {
                            Console.WriteLine("\nYour input must be an integer! Please, try again.");
                            input = Console.ReadLine();
                            input = Validate(input, option);
                        }
                        else
                        {
                            inputInt = Int32.Parse(input);
                            if (Array.BinarySearch(new int[] { 0, 1, 2 }, inputInt) < 0)
                            {
                                Console.WriteLine("Your input is invalid! Please, type a number between 0 and 2 for the Switch State.");
                                input = Console.ReadLine();
                                input = Validate(input, option);
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("\nYour input is empty or invalid! Please, try again.");
                        input = Console.ReadLine();
                        input = Validate(input, option);
                    }
                    break;
                case 4:
                    if (!string.IsNullOrWhiteSpace(input))
                    {
                        if (!int.TryParse(input, out int value))
                        {
                            Console.WriteLine("\nYour input must be an integer! Please, try again.");
                            input = Console.ReadLine();
                            input = Validate(input, option);
                        }
                        else
                        {
                            if (Array.BinarySearch(new int[] { 0, 1 }, Int32.Parse(input)) < 0)
                            {
                                Console.WriteLine("Your option must be valid! Choose between (0) for NO or (1) for YES.");
                                input = Console.ReadLine();
                                input = Validate(input, option);
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("\nYour input is empty or invalid! Please, try again.");
                        input = Console.ReadLine();
                        input = Validate(input, option);
                    }
                    break;
            }

            if (string.IsNullOrWhiteSpace(input))
                return inputInt.ToString();
            else
                return input;
        }

        public static void Print(string input, ConsoleColor colour)
        {
            Console.ForegroundColor = colour;
            Console.WriteLine(input);
            Console.ResetColor();
        }

        public static Meter Insert(List<Meter> MeterList)
        {
            string input;

            Console.Write("Insert the Endpoint Serial Number you want to add: ");
            input = Console.ReadLine();
            input = Validate(input, 0);

            Meter meter = FindBySerialNumber(MeterList, input);

            if (!string.IsNullOrEmpty(meter.EndpointSerialNumber))
            {
                Print("\nERROR: There is already an endpoint with that Serial Number!", ConsoleColor.Red);

                return new Meter() { };
            }
            else
            {
                meter.EndpointSerialNumber = input;

                Console.WriteLine("Insert the Meter Model Id.");
                Console.WriteLine("Options: (16) for NSX1P2W, (17) for NSX1P3W, (18) for NSX2P3W or (19) for NSX3P4W");
                input = Console.ReadLine();
                meter.MeterModelId = (EnumMeterModel)Int32.Parse(Validate(input, 2));

                Console.Write("Insert the Meter Number: ");
                input = Console.ReadLine();
                meter.MeterNumber = Int32.Parse(Validate(input, 1));

                Console.Write("Insert the Meter Firmware Version: ");
                input = Console.ReadLine();
                meter.MeterFirmwareVersion = Validate(input, 0);

                Console.Write("Insert the Switch State of the Meter: ");
                Console.WriteLine("Options: (0) for Disconnected, (1) for Connected or (2) for Armed.");
                input = Console.ReadLine();
                meter.SwitchState = (EnumSwitchState)Int32.Parse(Validate(input, 3));

                Print("\nThe following meter was added!", ConsoleColor.Blue);

                Console.WriteLine(meter);

                return meter;
            }
        }
        
        public static List<Meter> Edit(List<Meter> MeterList)
        {
            string input;

            Console.Write("\nInsert the Endpoint Serial Number of the meter you want to edit: ");
            input = Console.ReadLine();
            input = Validate(input, 0);

            Meter meter = FindBySerialNumber(MeterList, input);

            if (string.IsNullOrEmpty(meter.EndpointSerialNumber))
            {
                Print("\nERROR: There is no endpoint with that Serial Number!", ConsoleColor.Red);

                return new List<Meter>() { };
            }
            else
            {
                Print("\nMeter found!", ConsoleColor.Red);

                Console.WriteLine(meter);

                Console.WriteLine("To which Switch State do you want to change?");
                Console.WriteLine("Type (0) for Disconnected, (1) for Connected and (2) for Armed.");

                input = Console.ReadLine();
                meter.SwitchState = (EnumSwitchState)Int32.Parse(Validate(input, 3));

                Print("\nThe following meter was changed!", ConsoleColor.Blue);

                Console.WriteLine(meter);

                return MeterList;
            }
        }

        public static List<Meter> Delete(List<Meter> MeterList)
        {
            string input;

            Console.Write("\nInsert the Endpoint Serial Number you want to delete: ");
            input = Console.ReadLine();
            input = Validate(input, 0);

            Meter meter = FindBySerialNumber(MeterList, input);

            if (string.IsNullOrEmpty(meter.EndpointSerialNumber))
            {
                Print("\nERROR: There is no endpoint with that Serial Number!", ConsoleColor.Red);

                return new List<Meter>() { };
            }
            else 
            {
                Print("\nAre you SURE you want to DELETE this Meter? This action cannot be undone.", ConsoleColor.Red);
                Console.WriteLine("Type (0) for NO or (1) for YES.");
                input = Console.ReadLine();
                input = Validate(input, 4);

                if (input == "1")
                {
                    MeterList.Remove(meter);

                    Print("\nThe meter was deleted!", ConsoleColor.Blue);
                }

                return MeterList;
            }
        }

        public static void ListAll(List<Meter> MeterList)
        {
            //MeterList.ForEach(i => Console.Write("{0}\t", i));
            foreach (var item in MeterList.Select((value, i )=> new { i, value}))
            {
                var value = item.value;
                var index = item.i;

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nEndpoint #{0}", index+1);
                Console.ResetColor();

                Console.Write("{0}\t", value);
            }
        }

        public static void ListById(List<Meter> MeterList)
        {
            string input;
            
            Console.Write("\nInsert the Endpoint Serial Number you want to search: ");
            input = Console.ReadLine();
            input = Validate(input, 0);

            Meter meter = FindBySerialNumber(MeterList, input);

            if (string.IsNullOrEmpty(meter.EndpointSerialNumber))
            {
                Print("\nERROR: There is no endpoint with that Serial Number!", ConsoleColor.Red);
            }
            else
            {
                Print("\nMeter found!", ConsoleColor.Blue);

                Console.WriteLine(meter);
            }
        }

        public static Meter FindBySerialNumber(List<Meter> MeterList, string Id)
        {
            if (MeterList.Exists(x => x.EndpointSerialNumber == Id))
                return MeterList.Find(x => x.EndpointSerialNumber == Id);
            else
                return new Meter() { };
        }
    }
}
