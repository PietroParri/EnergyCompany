using EnergyCompany;
using EnergyCompany.ENUMs;

public class MethodsTest
{
    Methods _methods = new Methods();
    MethodsServices _services= new MethodsServices();

    [Fact]
    public void Inserting_Endpoint()
    {
        // Arrange

        List<Meter> MeterList = new List<Meter>();

        Meter expected = new Meter()
        {
            EndpointSerialNumber = "Meter Test",
            MeterModelId = (EnumMeterModel)16,
            MeterNumber = 5,
            MeterFirmwareVersion = "v1.0.45",
            SwitchState = (EnumSwitchState)2
        };

        MeterList.Add(expected);

        // Act
        Meter actual = _services.InsertService(MeterList, expected.EndpointSerialNumber, expected.MeterModelId, expected.MeterNumber, expected.MeterFirmwareVersion, expected.SwitchState);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Editing_Endpoint()
    {
        // Arrange
        Meter expected = new Meter()
        {
            EndpointSerialNumber = "Meter Test",
            MeterModelId = (EnumMeterModel)16,
            MeterNumber = 5,
            MeterFirmwareVersion = "v1.0.45",
            SwitchState = (EnumSwitchState)2
        };

        // Act
        Meter actual = _services.EditService(expected, (EnumSwitchState)2);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Deleting_Endpoint()
    {
        // Arrange
        List<Meter> expected = new List<Meter>();

        Meter meter = new Meter()
        {
            EndpointSerialNumber = "Meter Test",
            MeterModelId = (EnumMeterModel)16,
            MeterNumber = 5,
            MeterFirmwareVersion = "v1.0.45",
            SwitchState = (EnumSwitchState)2
        };

        expected.Add(meter);

        // Act

        List<Meter> actual = _services.DeleteService(expected, meter);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Finding_Endpoint()
    {
        // Arrange
        List<Meter> MeterList = new List<Meter>();

        Meter expected = new Meter()
        {
            EndpointSerialNumber = "Meter Test",
            MeterModelId = (EnumMeterModel)16,
            MeterNumber = 5,
            MeterFirmwareVersion = "v1.0.45",
            SwitchState = (EnumSwitchState)2
        };

        MeterList.Add(expected);

        // Act

        Meter actual = _methods.FindBySerialNumber(MeterList, expected.EndpointSerialNumber);

        // Assert
        Assert.Equal(expected, actual);
    }
}