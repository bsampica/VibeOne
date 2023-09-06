// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.ComponentModel;
using ReactiveUI;
using VibeOne.Services;

namespace VibeOne.Operations;

public class Co2TankOperation : IAutoOperation
{
    private readonly SensorService _sensorService;
    private readonly TankService _tankService;
    private readonly RelayService _relayService;

    public Co2TankOperation(SensorService sensorService, TankService tankService, RelayService relayService)
    {
        _sensorService = sensorService;
        _tankService = tankService;
        _relayService = relayService;
    }


    public Task<bool> BeginOperation(Action callbackWhenConditionMet)
    {
        Console.WriteLine("Starting the CO2 Tank Service: BeginOperation with callback");
        _sensorService.IsMonitorRunning.Subscribe(b =>
        {
            Console.WriteLine($"IsMonitorRunning Observer published value: {b}");
        });

        _sensorService.MainTankTemperature.Subscribe(temp =>
        {
            Console.WriteLine($"CO2 Service detected temp change on main tank: {temp}");
        });

        return Task.FromResult(true);
    }

    ~Co2TankOperation()
    {
        Console.WriteLine("CO2 Tank Service is self destructing");
    }
}
