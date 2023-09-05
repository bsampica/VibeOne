// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Device.Gpio;
using System.Linq;
using System.Threading;
using Avalonia.Threading;
using Iot.Device.OneWire;

namespace VibeOne.Services;

public class SensorService
{
    private const string Device1Id = "28-03139794691e";
    private const string Device2Id = "28-031397944f32";

    public OneWireThermometerDevice? TemperatureDevice1 { get; init; }
    public OneWireThermometerDevice? TemperatureDevice2 { get; init; }

    public SensorService()
    {
        if (DeviceInstance.IsRPI == false) return; // Dont load the sensors if we arent on a raspberry pi

        TemperatureDevice1 = OneWireThermometerDevice.EnumerateDevices().First(ed => ed.DeviceId == Device1Id);
        TemperatureDevice2 = OneWireThermometerDevice.EnumerateDevices().First(ed => ed.DeviceId == Device2Id);
    }

    public async Task StartTemperatureMonitorAsync(CancellationTokenSource token = null)
    {
        while (!token.IsCancellationRequested)
        {
            var sensor1 = await TemperatureDevice1?.ReadTemperatureAsync()!;
            var sensor2 = await TemperatureDevice2?.ReadTemperatureAsync()!;
            //Console.WriteLine($"Sensor 1: {sensor1.ReadTemperature().DegreesFahrenheit}");
            //Console.WriteLine($"Sensor 2: {sensor2.ReadTemperature().DegreesFahrenheit}");
            await Task.Delay(new TimeSpan(0, 0, 0, 20));
        }
    }
}
