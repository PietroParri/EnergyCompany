using EnergyCompany;
using System;
using System.Collections.Generic;

public class Program
{
    public static Meter inputMeter = new Meter();

    public static List<Meter> MeterList = new List<Meter>();

    public static string input;

    private static void Main(string[] args)
    {
        Methods _methods = new Methods();
        Validation _validation = new Validation();

        //DISCLAIMER: no AI were used on the conception of this project

        //app vars
        string appName = "EnergyCompany";
        string appVersion = "1.0.0";
        string appAuthor = "Pietro Parri"; //https://github.com/PietroParri

        /* If you want to manually insert an endpoint before runtime, use this;
            * MeterList.Add(new Meter() 
            * {
            *  EndpointSerialNumber = "*STRING*",
            *  MeterModelId = (EnumMeterModel)*INT*,
            *  MeterNumber = *INT*,
            *  MeterFirmwareVersion = "*STRING",
            *  SwitchState = (EnumSwitchState)*INT*
            * }); 
        */

        _methods.Startup(appName, appVersion, appAuthor);

        _methods.ShowOptions();

        input = Console.ReadLine();

        _validation.Verify();

        _validation.ValidateOptions();

        _methods.CallSwitch();
    }
}