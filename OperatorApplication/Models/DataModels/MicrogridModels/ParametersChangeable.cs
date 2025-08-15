using System;
using System.Collections.Generic;
using OperatorApplication.Models.DataModels.Base;
namespace OperatorApplication.Models.DataModels.MicrogridModels;

public partial class ParametersChangeable : Data
{
    public int? Id { get; set; }

    public int? BlockId { get; set; }

    public double? ActiveLoadPower { get; set; }

    public double? InductiveLoadPower { get; set; }

    public double? Inductance { get; set; }

    public double? ActiveResistace { get; set; }

    public double? MicrogridFrequency { get; set; }

    public string? Time { get; set; }

    public virtual MicrogridParametersChangeable? Block { get; set; }
}
