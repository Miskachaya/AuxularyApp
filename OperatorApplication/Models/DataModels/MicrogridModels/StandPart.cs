using System;
using System.Collections.Generic;
using OperatorApplication.Models.DataModels.Base;
namespace OperatorApplication.Models.DataModels.MicrogridModels;

public partial class StandPart : Data
{
    public int? StandId { get; set; }

    public string StandType { get; set; } = null!;

    public virtual ICollection<MicrogridParametersChangeable>? MicrogridParametersChangeables { get; set; } = new List<MicrogridParametersChangeable>();

    public virtual ICollection<MicrogridParametersMeasureChangeable>? MicrogridParametersMeasureChangeables { get; set; } = new List<MicrogridParametersMeasureChangeable>();

    public virtual ICollection<MicrogridParametersMeasure>? MicrogridParametersMeasures { get; set; } = new List<MicrogridParametersMeasure>();
}
