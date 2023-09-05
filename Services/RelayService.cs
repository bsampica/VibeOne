// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Device.Gpio;
using System.Threading;

namespace VibeOne.Services;

public class RelayService : IDisposable
{
    private const int PinNumber = 21;
    private readonly GpioController _gpioController = new GpioController();
    private const string Ready = "READY ✅";
    private const string Alert = "ALERT 🚨";

    private readonly PinValue _pinValue;

    public RelayService()
    {
        _gpioController.OpenPin(PinNumber, PinMode.Output, PinValue.Low);
        _gpioController.RegisterCallbackForPinValueChangedEvent(PinNumber, PinEventTypes.Falling | PinEventTypes.Rising,
            OnPinEvent);
        _pinValue = PinValue.Low;
    }

    private void OnPinEvent(object sender, PinValueChangedEventArgs eventargs)
    {
        Console.WriteLine($"PIN EVENT: {eventargs.PinNumber}:{eventargs.ChangeType}");
    }

    public void ToggleRelay(TimeSpan toggleDelay)
    {
        _gpioController.Write(PinNumber, PinValue.High);
        Thread.Sleep(toggleDelay);
        _gpioController.Write(PinNumber, PinValue.Low);
    }

    public async Task ToggleRelayAsync(TimeSpan toggleDelay)
    {
        _gpioController.Write(PinNumber, PinValue.High);
        await Task.Delay(toggleDelay);
        _gpioController.Write(PinNumber, PinValue.Low);
    }


    public void Dispose()
    {
        _gpioController.Dispose();
    }
}
