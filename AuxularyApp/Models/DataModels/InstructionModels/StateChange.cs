using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace AuxularyApp.Models.DataModels.InstructionModels;

public partial class StateChange : INotifyPropertyChanged
{
    private int _ChageId;
    public int ChangeId { get => _ChageId; set { _ChageId = value; OnPropertyChanged(); } }

    
    public int StepId { get; set; }
    private int _SwitchL;
    public int SwitchL { get=> _SwitchL; set { _SwitchL = value; OnPropertyChanged(); } }
    private int _SwitchR;
    public int SwitchR { get=> _SwitchR; set { _SwitchR = value; OnPropertyChanged(); } }
    //[JsonIgnore]
    public virtual InstructionStep? Step { get; set; }
    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
