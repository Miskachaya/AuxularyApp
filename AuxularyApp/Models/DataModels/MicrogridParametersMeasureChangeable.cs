using System;
using System.Collections.Generic;

namespace AuxularyApp.Models.DataModels;

public partial class MicrogridParametersMeasureChangeable
{
    public int? BlockId { get; set; }

    public string? BlockType { get; set; }

    public int? StandId { get; set; }

    public virtual StandPart? Stand { get; set; }
}
