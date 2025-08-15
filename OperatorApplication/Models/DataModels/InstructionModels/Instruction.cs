using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;

namespace OperatorApplication.Models.DataModels.InstructionModels;

public partial class Instruction
{
    [JsonIgnore]
    public int InstructionId { get; set; }

    public string Name { get; set; } = null!;

    public DateTime DateCreation { get; set; } = DateTime.UtcNow;

    public string? State { get; set; }

    public virtual ICollection<InstructionStep>? InstructionSteps { get; set; } = new List<InstructionStep>();
}
