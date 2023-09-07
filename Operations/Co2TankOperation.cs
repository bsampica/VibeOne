// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

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
        _sensorService.IsMonitorRunning.Subscribe(_ => { }); // CREATES A HOT OBSERVABLE with SUBSCRIBE.
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
                if (_relayService.SolenoidOpen.Value)
                {
                    await _relayService.TriggerRelayAsync();
                }

                break;
            case < 39:
                // WE ARE NOW IN THE RANGE TO OPEN THE VALVE
                if (!_relayService.SolenoidOpen.Value)
                {
                    await _relayService.TriggerRelayAsync();
                }

                break;
        }
    }

    ~Co2TankOperation()
    {
        Console.WriteLine("CO2 Tank Service is self destructing");
    }
}
