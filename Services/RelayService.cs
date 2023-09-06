﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Device.Gpio;
using System.Threading;

namespace VibeOne.Services;

public class RelayService : IDisposable
{
    private const int PinNumber = 21;
    private readonly GpioController _gpioController = new GpioController();

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
        _gpioController.Write(PinNumber, PinValue.High);
        Thread.Sleep(1000);
        _gpioController.Write(PinNumber, PinValue.Low);
    }
    
    public async Task<bool> TriggerRelayAsync()
    {
        _gpioController.Write(PinNumber, PinValue.High);
        await Task.Delay(1000);
        _gpioController.Write(PinNumber, PinValue.Low);
        return true;
    }

    public void ResetRelay()
    {
        //TODO: Figure out how to track and make sure the relay will be closed.
        Thread.Sleep(1000);
    }

    public async Task<bool> ResetRelayAsync()
    {
        //TODO: Figure out how to track and make sure the relay will be closed
        await Task.Delay(1000);
        return true;
    }

    public void Dispose()
    {
        _gpioController.Dispose();
    }
}
