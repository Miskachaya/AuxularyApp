using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace OperatorApplication.Models.DataModels.InstructionModels;

public partial class ParametersChange : INotifyPropertyChanged
{
    [JsonIgnore]
    public int ChangeId { get; set; }
    [JsonIgnore]
    public int StepId { get; set; }
    [JsonIgnore]
    public string _Parameter = null!;
    public string Parameter { get=> _Parameter; set { _Parameter = value;OnPropertyChanged(); } }
    [JsonIgnore]
    private double _ParameterValue;
    public double ParameterValue { get=> _ParameterValue; set { _ParameterValue = value;OnPropertyChanged(); } }
    [JsonIgnore]
    private string _Block = "";
    [JsonIgnore]
    public string Block { get => _Block; set { _Block = value; OnPropertyChanged(); } }
    [JsonIgnore]
    private string _Time = DateTime.Now.ToString("HH:mm:ss dd.MM.yyyy");
    [JsonIgnore]
    public string Time { get => _Time; set { _Block = Time; OnPropertyChanged(); } }
    [JsonIgnore]
    public virtual InstructionStep? Step { get; set; }
    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
