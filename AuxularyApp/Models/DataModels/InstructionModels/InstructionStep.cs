using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
using AuxularyApp.ViewModels.Base;
namespace AuxularyApp.Models.DataModels.InstructionModels;

public partial class InstructionStep 
{
    public int StepId { get; set; }

    public int StepNumber { get; set; } = 0;

    public int InstructionId { get; set; }

    public string Time { get; set; } = null!;

    public int BlockId { get; set; }
    //[JsonIgnore]
    public virtual Instruction? Instruction { get; set; } 

    public virtual ICollection<ParametersChange>? ParametersChanges { get; set; } = new List<ParametersChange>();

    public virtual ICollection<StateChange>? StateChanges { get; set; } = new List<StateChange>();
}
