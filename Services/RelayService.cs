// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Device.Gpio;
using System.Reactive.Subjects;
using System.Threading;
using UnitsNet;

namespace VibeOne.Services;

public class RelayService : IDisposable
{
    private const int PinNumber = 21;
    private readonly GpioController _gpioController = new GpioController();
    public readonly BehaviorSubject<bool> SolenoidOpen = new BehaviorSubject<bool>(false);

    public RelayService()
    {
        _gpioController.OpenPin(PinNumber, PinMode.Output, PinValue.Low);
        _gpioController.RegisterCallbackForPinValueChangedEvent(PinNumber, PinEventTypes.Falling | PinEventTypes.Rising,
            OnPinEvent);
    }

    private void OnPinEvent(object sender, PinValueChangedEventArgs eventargs)
    {
        Console.WriteLine($"PIN EVENT: {eventargs.PinNumber}:{eventargs.ChangeType}");
    }

    public void TriggerRelay()
    {
        _gpioController.Write(PinNumber, PinValue.High); // Trigger the relay signal wire
        Thread.Sleep(500);
        _gpioController.Write(PinNumber, PinValue.Low); // Return the pin to off state.
        TrackSolenoidState();
    }

    public async Task<bool> TriggerRelayAsync()
    {
        _gpioController.Write(PinNumber, PinValue.High); // trigger the relay signal wire
        await Task.Delay(500);
        _gpioController.Write(PinNumber, PinValue.Low); // return the pin to the off state
        TrackSolenoidState();
        return true;
    }

    private void TrackSolenoidState()
    {
        switch (SolenoidOpen.Value)
        {
            case false:
                SolenoidOpen.OnNext(true);
                break;
            case true:
                SolenoidOpen.OnNext(false);
                break;
        }
    }

    public void Dispose()
    {
        _gpioController.Dispose();
    }
}
