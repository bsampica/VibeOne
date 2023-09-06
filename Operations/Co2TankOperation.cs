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

    public bool IsAttachedAndRunning { get; } = false;

    public Co2TankOperation(SensorService sensorService, TankService tankService, RelayService relayService)
    {
        _sensorService = sensorService;
        _tankService = tankService;
        _relayService = relayService;
    }


    public Task<bool> BeginOperation()
    {
        Console.WriteLine("Starting the CO2 Tank Service: BeginOperation with callback");
        _sensorService.IsMonitorRunning.Subscribe(b =>
        {
            Console.WriteLine($"IsMonitorRunning Observer published value: {b}");
        });

        _sensorService.MainTankTemperature.Subscribe(MainTankTemp_OnNext);
        return Task.FromResult(true);
    }

    private async void MainTankTemp_OnNext(double temp)
    {
        // FOR EACH CHANGE THIS METHOD WILL BE CALLED
        switch (temp)
        {
            case >= 39:
                // DO NOTHING, TEMP IS ABOVE OPERATION
                Console.WriteLine("TEMP IS ABOVE OR EQUAL TO 39.");
                if (_relayService.SolenoidOpen.Value)
                {
                    Console.WriteLine("RELAY IS OPEN:  CLOSING GAS");
                    await _relayService.TriggerRelayAsync();
                    break;
                }

                Console.WriteLine("RELAY IS CLOSED: NO ACTION REQUIRED");
                break;
            case < 39:
                // WE ARE NOW IN THE RANGE TO OPEN THE VALVE
                Console.WriteLine("TEMPERATURE REACHED FOR C02: 39f");
                if (!_relayService.SolenoidOpen.Value)
                {
                    Console.WriteLine("VALUE IS CLOSED:  OPENING FOR C02");
                    await _relayService.TriggerRelayAsync();
                    break;
                }
                Console.WriteLine("VALVE IS OPEN:  NO ACTION REQUIRED");
                break;
        }
    }

    ~Co2TankOperation()
    {
        Console.WriteLine("CO2 Tank Service is self destructing");
    }
}
