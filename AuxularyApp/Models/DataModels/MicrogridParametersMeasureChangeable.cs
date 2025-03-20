using System;
using System.Collections.Generic;
using AuxularyApp.Models.DataModels.Base;
namespace AuxularyApp.Models.DataModels;

public partial class MicrogridParametersMeasureChangeable : Data
{
    public int? BlockId { get; set; }

    public string? BlockType { get; set; }

    public int? StandId { get; set; }

    public virtual StandPart? Stand { get; set; }
}
