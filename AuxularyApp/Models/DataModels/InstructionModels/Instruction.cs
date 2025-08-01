using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace AuxularyApp.Models.DataModels.InstructionModels;

public partial class Instruction
{
    public int InstructionId { get; set; }

    public string Name { get; set; } = null!;

    public DateTime DateCreation { get; set; }

    public string? State { get; set; }

    public virtual ICollection<InstructionStep>? InstructionSteps { get; set; } = new List<InstructionStep>();
}
