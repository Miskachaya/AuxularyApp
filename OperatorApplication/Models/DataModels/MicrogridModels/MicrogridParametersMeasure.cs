using System;
using System.Collections.Generic;
using OperatorApplication.Models.DataModels.Base;
namespace OperatorApplication.Models.DataModels.MicrogridModels;

public partial class MicrogridParametersMeasure : Data
{
    public int? BlockId { get; set; }

    public string? BlockType { get; set; }

    public int? StandId { get; set; }

    public virtual ICollection<ParametersMeasure>? ParametersMeasures { get; set; } = new List<ParametersMeasure>();

    public virtual StandPart? Stand { get; set; }
}
