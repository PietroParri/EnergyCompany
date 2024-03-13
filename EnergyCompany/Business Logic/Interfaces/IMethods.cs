using EnergyCompany;
using System.Collections.Generic;

interface IMethods
{
    void Startup(string appName, string appVersion, string appAuthor);
    void ShowOptions();
    string Validate(string input, int option);
    void Recall();

    void CallSwitch();
        
    Meter Insert(List<Meter> MeterList);

    List<Meter> Edit(List<Meter> MeterList);

    List<Meter> Delete(List<Meter> MeterList);

    void ListAll(List<Meter> MeterList);

    void ListById(List<Meter> MeterList);

    Meter FindBySerialNumber(List<Meter> MeterList, string Id);

}