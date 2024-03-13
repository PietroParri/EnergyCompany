using System;
using System.IO.Ports;

namespace EnergyCompany
{
    public class Meter
    {
        public string EndpointSerialNumber { get; set; }

        public int MeterModelId { get; set; }

        public int MeterNumber { get; set; }

        public string MeterFirmwareVersion { get; set; }

        public int SwitchState { get; set; }

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
