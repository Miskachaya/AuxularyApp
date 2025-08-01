using System;
using System.Collections.Generic;
using AuxularyApp.Models.DataModels.Base;

namespace AuxularyApp.Models.DataModels.MicrogridModels;

public partial class MicrogridParametersChangeable : Data
{
    public int? BlockId { get; set; }

    public string? BlockType { get; set; }

    public int? StandId { get; set; }

    public virtual ICollection<ParametersChangeable>? ParametersChangeables { get; set; } = new List<ParametersChangeable>();

    public virtual StandPart? Stand { get; set; }
}
