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
                Console.WriteLine("TEMP IS ABOVE OR EQUAL TO 39.  Closing or maintaining closed relay.");
                await _relayService.ResetRelayAsync();  // Reset the relay to default
                break;
            case < 39:
                // WE ARE NOW IN THE RANGE TO OPEN THE VALVE
                Console.WriteLine($"OPENING CO2 RELAY - WILL MONITOR TEMP.");
                Console.WriteLine($"AT 39F THE RELAY OPENS.  ABOVE 40:  IT WILL CLOSE AGAIN");
                await _relayService.TriggerRelayAsync();
                break;
            default:
                // SOMETHING WENT WRONG, CLOSE THE RELAY
                await _relayService.ResetRelayAsync();
                break;
        }
    }

    ~Co2TankOperation()
    {
        Console.WriteLine("CO2 Tank Service is self destructing");
    }
}
