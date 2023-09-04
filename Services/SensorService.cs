// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
using System.Device.Gpio;
using Avalonia.Threading;
using Iot.Device.OneWire;

namespace VibeOne.Services;

public class SensorService
{
    private OneWireBus _oneWireBus { get; set; }
    private OneWireThermometerDevice _wireThermometerDevice { get; set; }
    private DispatcherTimer _timer;


    public SensorService()
    {
        if (DeviceInstance.IsRPI == false) return;
        var busIds = OneWireBus.EnumerateBusIds();
        foreach (var id in busIds)
        {
            Console.WriteLine($"BUS ID: {id}");
        }

    }
}
