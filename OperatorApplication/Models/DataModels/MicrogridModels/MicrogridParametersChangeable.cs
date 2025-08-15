using System;
using System.Collections.Generic;
using OperatorApplication.Models.DataModels.Base;

namespace OperatorApplication.Models.DataModels.MicrogridModels;

public partial class MicrogridParametersChangeable : Data
{
    public int? BlockId { get; set; }

    public string? BlockType { get; set; }

    public int? StandId { get; set; }

    public virtual ICollection<ParametersChangeable>? ParametersChangeables { get; set; } = new List<ParametersChangeable>();

    public virtual StandPart? Stand { get; set; }
}
