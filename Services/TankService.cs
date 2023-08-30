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
                Name = "Tank 1",
                Id = 1,
                Temperature = 21.2,
                TemperatureHistory = new[] { 90, 84, 71.2, 65.1, 40, 21.2 }
            },
            new TankModel()
            {
                Name = "Tank 2",
                Id = 2,
                Temperature = 21.2,
                TemperatureHistory = new[] { 90, 84, 71.2, 65.1, 40, 21.2 }
            },
            new TankModel()
            {
                Name = "Tank 3",
                Id = 3,
                Temperature = 21.2,
                TemperatureHistory = new[] { 90, 84, 71.2, 65.1, 40, 21.2 }
            },
            new TankModel()
            {
                Name = "Tank 4",
                Id = 4,
                Temperature = 21.2,
                TemperatureHistory = new[] { 90, 84, 71.2, 65.1, 40, 21.2 }
            },
        };
    }
}
