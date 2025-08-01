using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace AuxularyApp.Models.DataModels.InstructionModels;

public partial class ParametersChange : INotifyPropertyChanged
{
    private int _ChangeID;
    public int ChangeId { get=>_ChangeID; set { _ChangeID = value;OnPropertyChanged(); } }

    public int _StepID;
    public int StepId { get=> _StepID; set { _StepID = value; OnPropertyChanged(); } }

    public string _Parameter = null!;
    public string Parameter { get=> _Parameter; set { _Parameter = value;OnPropertyChanged(); } } 

    private double _ParameterValue;
    public double ParameterValue { get=> _ParameterValue; set { _ParameterValue = value;OnPropertyChanged(); } }
    //[JsonIgnore]
    public virtual InstructionStep? Step { get; set; }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

}
