// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Subjects;
using System.Runtime.CompilerServices;
using System.Threading;
using Iot.Device.OneWire;

namespace VibeOne.Services;

public class SensorService : INotifyPropertyChanged
{
    private const string Device1Id = "28-03139794691e";
    private const string Device2Id = "28-031397944f32";

    public BehaviorSubject<bool> IsMonitorRunning { get; } = new BehaviorSubject<bool>(false);
    public BehaviorSubject<double> MainTankTemperature { get; } = new BehaviorSubject<double>(9999.99);

    private OneWireThermometerDevice? TemperatureDevice1 { get; }
    private OneWireThermometerDevice? TemperatureDevice2 { get; }

    private double _temp1;

    public double TemperatureOne
    {
        get
        {
            return _temp1;
        }
        set
        {
            SetField(ref _temp1, value);
        }
    }

    private double _temp2;

    public double TemperatureTwo
    {
        get
        {
            return _temp2;
        }
        set
        {
            SetField(ref _temp2, value);
        }
    }

    private readonly CancellationTokenSource _token = new CancellationTokenSource();

    public SensorService()
    {
        if (DeviceInstance.IsRPI == false) return; // Dont load the sensors if we arent on a raspberry pi

        TemperatureDevice1 = OneWireThermometerDevice.EnumerateDevices().First(ed => ed.DeviceId == Device1Id);
        TemperatureDevice2 = OneWireThermometerDevice.EnumerateDevices().First(ed => ed.DeviceId == Device2Id);
    }

    public async Task StartTemperatureMonitorAsync()
    {
        Console.Write("Starting the Sensor Service");
        MainTankTemperature.OnNext(9999.99);
        while (!_token.IsCancellationRequested)
        {
            var sensor1 = await TemperatureDevice1?.ReadTemperatureAsync()!;
            var sensor2 = await TemperatureDevice2?.ReadTemperatureAsync()!;
            TemperatureOne = sensor1.DegreesFahrenheit;
            TemperatureTwo = sensor2.DegreesFahrenheit;
            MainTankTemperature.OnNext(sensor1.DegreesFahrenheit);

            await Task.Delay(
                1000); // TEMPERATURE CHECK RESOLUTION, DEFAULT IS 10 SECONDS, // SETTING TO LOWER THAN 1 SECOND MAY HAVE BAD RESULTS
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}
