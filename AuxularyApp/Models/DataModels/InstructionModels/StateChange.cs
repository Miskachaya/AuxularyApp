using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace AuxularyApp.Models.DataModels.InstructionModels;

public partial class StateChange : INotifyPropertyChanged
{
    [JsonIgnore]
    private string _Block = "";
    [JsonIgnore]
    public string Block { get => _Block; set { _Block = value; OnPropertyChanged(); } }
    [JsonIgnore]
    public int ChangeId { get; set; }
    [JsonIgnore]
    public int StepId { get; set; }
    [JsonIgnore]
    private int _SwitchL;
    public int SwitchL { get=> _SwitchL; set { _SwitchL = value; OnPropertyChanged(); } }
    [JsonIgnore]
    private int _SwitchR;
    public int SwitchR { get=> _SwitchR; set { _SwitchR = value; OnPropertyChanged(); } }
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
