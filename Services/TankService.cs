// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.Reactive.Linq;
using DynamicData;
using VibeOne.Models;

namespace VibeOne.Services;

public class TankService
{
    public IEnumerable<TankModel> Tanks { get; set; }


    public TankService()
    {
        Tanks = MockData();
    }

    public List<TankModel> MockData()
    {
        return new List<TankModel>()
        {
            new TankModel()
            {
                Name = "Tank 1", Id = 1, Temperature = 21.2, TemperatureHistory = GetMockTemperatureHistory()
            }
        };
    }

    private List<TemperatureHistory> GetMockTemperatureHistory()
    {
        var tempHistory = new List<TemperatureHistory>();
        for (var i = 100; i >= 0; i--)
        {
            var tempReading = new TemperatureHistory()
            {
                Temperature = i, Time = new DateTime(2022, 11, 03, 07, 01, 22, 00) - TimeSpan.FromMinutes(i)
            };
            tempHistory.Add(tempReading);
        }

        return tempHistory;
    }
}
