using EnergyCompany.ENUMs;
using EnergyCompany;
using System.Collections.Generic;
using System;
using System.Linq;

public class MethodsServices
{
    public Meter InsertService(List<Meter> MeterList, string EndpointSerialNumber, EnumMeterModel MeterModelId,
        int MeterNumber, string MeterFirmwareVersion, EnumSwitchState SwitchState)
    {
        Meter meterInsert = new Meter();

        try
        {
            meterInsert.EndpointSerialNumber = EndpointSerialNumber;
            meterInsert.MeterModelId = MeterModelId;
            meterInsert.MeterNumber = MeterNumber;
            meterInsert.MeterFirmwareVersion = MeterFirmwareVersion;
            meterInsert.SwitchState = SwitchState;

            MeterList.Add(meterInsert);

            return meterInsert;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error inserting Endpoint", ex);
            throw;
        }
    }

    public Meter EditService(Meter meter, EnumSwitchState switchState)
    {
        meter.SwitchState = switchState;

        return meter;
    }

    public List<Meter> DeleteService(List<Meter> MeterList, Meter meter)
    {
        MeterList.Remove(meter);

        return MeterList;
    }
}