using System;
using System.Collections.Generic;
using AuxularyApp.Models.DataModels.Base;
namespace AuxularyApp.Models.DataModels;

public partial class ParametersMeasure : Data
{
    public int? Id { get; set; }

    public int? BlockId { get; set; }

    public double? VoltageValue { get; set; }

    public double? CurrentValue { get; set; }

    public double? ActiveLoadPower { get; set; }

    public double? ReactiveLoadPower { get; set; }

    public double? FullLoadPower { get; set; }

    public double? LoadPowerFactor { get; set; }

    public double? MicrogridFrequency { get; set; }

    public DateTime? Time { get; set; }

    public virtual MicrogridParametersMeasure? Block { get; set; }
}
