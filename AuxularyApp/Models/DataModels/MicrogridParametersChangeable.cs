using System;
using System.Collections.Generic;

namespace AuxularyApp.Models.DataModels;

public partial class MicrogridParametersChangeable
{
    public int? BlockId { get; set; }

    public string? BlockType { get; set; }

    public int? StandId { get; set; }

    public virtual ICollection<ParametersChangeable>? ParametersChangeables { get; set; } = new List<ParametersChangeable>();

    public virtual StandPart? Stand { get; set; }
}
