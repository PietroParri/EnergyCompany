using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading;

namespace EnergyCompany
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            //DISCLAIMER: no AI were used on the conception of this project
            //app vars
            string appName = "EnergyCompany";
            string appVersion = "1.0.0";
            string appAuthor = "Pietro Parri"; //https://github.com/PietroParri

            string input;
            int[] options = { 1, 2, 3, 4, 5, 6 };
            bool exists;
            bool close = false;

            Meter inputMeter = new Meter();
            List<Meter> MeterList = new List<Meter>();

            void Verify()
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
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("\nChoose another option then.");
                        Console.ResetColor();
                        Methods.ShowOptions();
                        close = false;
                        input = Console.ReadLine();
                        CallSwitch();
                    }

                    else if (input == "1")
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("The application has been closed.");
                        Console.ResetColor();
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

            void ValidateOptions()
            {
                exists = Array.BinarySearch(options, Int32.Parse(input)) >= 0;

                while (!exists)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("\nPlease, type a valid option!\n");
                    Console.ResetColor();

                    input = Console.ReadLine();

                    Verify();

                    exists = Array.BinarySearch(options, Int32.Parse(input)) >= 0;
                }
            }

            void Recall()
            {
                input = "0";
                CallSwitch();
            }

            void CallSwitch()
            {
                switch (input)
                {
                    case "1":
                        inputMeter = Methods.Insert(MeterList);
                        if (!string.IsNullOrEmpty(inputMeter.EndpointSerialNumber))
                            MeterList.Add(new Meter()
                            {
                                EndpointSerialNumber = inputMeter.EndpointSerialNumber,
                                MeterModelId = inputMeter.MeterModelId,
                                MeterNumber = inputMeter.MeterNumber,
                                MeterFirmwareVersion = inputMeter.MeterFirmwareVersion,
                                SwitchState = inputMeter.SwitchState
                            });
                        Recall();
                        break;
                    case "2":
                        List<Meter> temp = MeterList;
                        MeterList = Methods.Edit(MeterList);
                        if (!MeterList.Any())
                            MeterList = temp;
                        Recall();
                        break;
                    case "3":
                        MeterList = Methods.Delete(MeterList);
                        Recall();
                        break;
                    case "4":
                        Methods.ListAll(MeterList);
                        Recall();
                        break;
                    case "5":
                        Methods.ListById(MeterList);
                        Recall();
                        break;
                    case "6":
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("\nAre you sure you want to close the application? Type (0) for NO and (1) for YES.");
                        Console.ResetColor();
                        input = Console.ReadLine();
                        close = true;
                        Verify();
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("\nPlease, choose an option to continue.");
                        Console.ResetColor();
                        Methods.ShowOptions();
                        input = Console.ReadLine();
                        Verify();
                        ValidateOptions();
                        CallSwitch();
                        break;
                }
            }

            //Console.WriteLine("\nMeter which serial number is " + Id + ": \n{0}", meters.Find(x => x.EndpointSerialNumber.Contains(Id)));
            //Console.WriteLine("\nThere is no meter with that serial number!\n");
            Methods.Startup(appName, appVersion, appAuthor);

            MeterList.Add(new Meter() { EndpointSerialNumber = "a", MeterModelId = 0, MeterNumber = 0, MeterFirmwareVersion = "a", SwitchState = 0 });
            MeterList.Add(new Meter() { EndpointSerialNumber = "b", MeterModelId = 1, MeterNumber = 1, MeterFirmwareVersion = "b", SwitchState = 1 });

            Methods.ShowOptions();

            input = Console.ReadLine();

            Verify();

            ValidateOptions();

            CallSwitch();
        }
    }
}