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

        public static Meter Insert(List<Meter> MeterList)
        {
            string Id;

            Console.Write("Insert the Endpoint Serial Number you want to add: ");
            Id = Console.ReadLine();

            Meter meter = FindBySerialNumber(MeterList, Id);

            if (!string.IsNullOrEmpty(meter.EndpointSerialNumber))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nERROR: There is already an endpoint with that Serial Number!\n");
                Console.ResetColor();

                return new Meter() { };
            }
            else
            {
                meter.EndpointSerialNumber = Id;

                Console.Write("Insert the Meter Model Id: ");
                meter.MeterModelId = Int32.Parse(Console.ReadLine());

                Console.Write("Insert the Meter Number: ");
                meter.MeterNumber = Int32.Parse(Console.ReadLine());

                Console.Write("Insert the Meter Firmware Version: ");
                meter.MeterFirmwareVersion = Console.ReadLine();

                Console.Write("Insert the Switch State of the Meter: ");
                meter.SwitchState = Int32.Parse(Console.ReadLine());

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("\nThe following meter was added!"); 
                Console.ResetColor();

                Console.WriteLine(meter);

                return meter;
            }
        }
        
        public static List<Meter> Edit(List<Meter> MeterList)
        {
            string Id;
            int switchState;

            Console.Write("\nInsert the Endpoint Serial Number you want to edit: ");
            Id = Console.ReadLine();

            Meter meter = FindBySerialNumber(MeterList, Id);

            if (string.IsNullOrEmpty(meter.EndpointSerialNumber))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nERROR: There is no endpoint with that Serial Number!");
                Console.ResetColor();

                return new List<Meter>() { };
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nMeter found!");
                Console.ResetColor();

                Console.WriteLine(meter);

                Console.WriteLine("To which Switch State do you want to change?");
                Console.WriteLine("Type (0) for Disconnected, (1) for Connected and (2) for Armed.");

                switchState = Int32.Parse(Console.ReadLine());

                meter.SwitchState = switchState; 

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("\nThe following meter was changed!");
                Console.ResetColor();

                Console.WriteLine(meter);

                return MeterList;
            }
        }

        public static List<Meter> Delete(List<Meter> MeterList)
        {
            string Id;

            Console.Write("\nInsert the Endpoint Serial Number you want to delete: ");
            Id = Console.ReadLine();

            Meter meter = FindBySerialNumber(MeterList, Id);

            if (string.IsNullOrEmpty(meter.EndpointSerialNumber))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nERROR: There is no endpoint with that Serial Number!");
                Console.ResetColor();

                return new List<Meter>() { };
            }
            else 
            {
                MeterList.Remove(meter);

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("\nThe meter was deleted!");
                Console.ResetColor();

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
            string Id;

            Console.Write("\nInsert the Endpoint Serial Number you want to search: ");
            Id = Console.ReadLine();

            Meter meter = FindBySerialNumber(MeterList, Id);

            if (string.IsNullOrEmpty(meter.EndpointSerialNumber))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nERROR: There is no endpoint with that Serial Number!");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("\nMeter found!");
                Console.ResetColor();

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
