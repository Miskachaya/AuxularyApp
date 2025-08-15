using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
using OperatorApplication.ViewModels.Base;
namespace OperatorApplication.Models.DataModels.InstructionModels;

public partial class InstructionStep : INotifyPropertyChanged
{
    [JsonIgnore]
    public int StepId { get; set; }

    public int StepNumber { get; set; } = 0;

    [JsonIgnore]
    public int InstructionId { get; set; }

    public string Time { get; set; } = null!;
    public string Block {  get; set; } = null!;
    [JsonIgnore]
    public virtual Instruction? Instruction { get; set; } 

    public virtual ICollection<ParametersChange>? ParametersChanges { get; set; } = new List<ParametersChange>();

    public virtual ICollection<StateChange>? StateChanges { get; set; } = new List<StateChange>();

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
