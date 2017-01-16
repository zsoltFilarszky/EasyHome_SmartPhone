using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyHome
{
    public enum Changable
    {
        True = 1,
        False = 0
    }

    public enum Endpoints
    {
        CreateSensor=1,
        DeleteSensor=2,
        ListSensors=3,
        GetlatestSensorData=4,
        ChangesensorValue=5
    }

    public enum SIUnits
    {
        Celsius,
        Motion,
        Switch,
        Other
    }
}
