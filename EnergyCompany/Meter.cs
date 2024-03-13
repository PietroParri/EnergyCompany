﻿using System;
using System.IO.Ports;

namespace EnergyCompany
{
    public enum EnumSwitchState
    {
        Disconnected = 0,
        Connected = 1,
        Armed = 2
    }

    public enum EnumMeterModel
    {
        NSX1P2W = 16,
        NSX1P3W = 17,
        NSX2P3W = 18,
        NSX3P4W = 19
    }

    public class Meter
    {
        public string EndpointSerialNumber { get; set; }

        public EnumMeterModel MeterModelId { get; set; }

        public int MeterNumber { get; set; }

        public string MeterFirmwareVersion { get; set; }

        public EnumSwitchState SwitchState { get; set; }

        public override string ToString()
        {
            return "\nEndpoint Serial Number: " + EndpointSerialNumber + "\nMeter Model Id: " + MeterModelId + 
                "\nMeter Number: " + MeterNumber + "\nMeter Firmware Version: " + MeterFirmwareVersion + "\nSwitch State: " + SwitchState + "\n";
        }
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            Meter objAsPart = obj as Meter;
            if (objAsPart == null) return false;
            else return Equals(objAsPart);
        }
        public override int GetHashCode()
        {
            return EndpointSerialNumber.GetHashCode();
        }
        public bool Equals(Meter other)
        {
            if (other == null) return false;
            return (this.EndpointSerialNumber.Equals(other.EndpointSerialNumber));
        }
    }
}
