// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Device.Gpio;
using System.Linq;
using Avalonia.Threading;
using Iot.Device.OneWire;

namespace VibeOne.Services;

public class SensorService
{
    private const string Device1Id = "28-03139794691e";
    private const string Device2Id = "28-031397944f32";


    public SensorService()
    {
        if (DeviceInstance.IsRPI == false) return;
        var sensor1 = OneWireThermometerDevice.EnumerateDevices().First(ed => ed.DeviceId == Device1Id);
        var sensor2 = OneWireThermometerDevice.EnumerateDevices().First(ed => ed.DeviceId == Device2Id);

        Console.WriteLine($"Sensor 1: {sensor1.ReadTemperature().DegreesFahrenheit}");
        Console.WriteLine($"SEnsor 2: {sensor2.ReadTemperature().DegreesFahrenheit}");
    }
}
