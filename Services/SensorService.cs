// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Device.Gpio;
using Avalonia.Threading;
using Iot.Device.OneWire;

namespace VibeOne.Services;

public class SensorService
{
    private const string BusId1 = "w1_bus_master1";
    private const string BusId2 = "w1_bus_master2";
    private const string TempSensorOneId = "28-031397944f32"; //28-03139794691e
    private const string TempSensorTwoId = "28-03139794691e";
    private OneWireBus _oneWireBus1 { get; set; }
    private OneWireBus _oneWireBus2 { get; set; }
    private OneWireThermometerDevice _wireThermometerDevice1 { get; set; }
    private OneWireThermometerDevice _wireThermometerDevice2 { get; set; }
    private DispatcherTimer _timer;


    public SensorService()
    {
        if (DeviceInstance.IsRPI == false) return;

        _oneWireBus1 = new OneWireBus(BusId1);
        _oneWireBus2 = new OneWireBus(BusId2);

        foreach (var dev in _oneWireBus1.EnumerateDeviceIds())
        {
            Console.WriteLine($"Device Id {dev}");
        }

        foreach (var dev in _oneWireBus2.EnumerateDeviceIds())
        {
            Console.WriteLine($"Device Id {dev}");
        }

        _wireThermometerDevice1 = new OneWireThermometerDevice(BusId1, TempSensorOneId);
        _wireThermometerDevice2 = new OneWireThermometerDevice(BusId2, TempSensorTwoId);

        Console.WriteLine($"Device One:  {_wireThermometerDevice1.DeviceId}");
        Console.WriteLine($"Device Two: {_wireThermometerDevice2.DeviceId}");
        // Console.WriteLine($"Temp 1 Temp  : {_wireThermometerDevice1.ReadTemperature().DegreesFahrenheit.ToString()}");
        // Console.WriteLine($"Temp 2 Temp : {_wireThermometerDevice2.ReadTemperature().DegreesFahrenheit.ToString()}");
    }
}
