using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using AuxularyApp.Models.DataModels.Base;
namespace AuxularyApp.Models.DataModels.MicrogridModels;

public partial class ParametersMeasure : Data
{
    public ParametersMeasure() { }
    [JsonPropertyName("id")]
    public int? Id { get; set; }
    [JsonPropertyName("blockId")]
    public int? BlockId { get; set; }
    [JsonPropertyName("voltageValue")]
    public double? VoltageValue { get; set; }
    [JsonPropertyName("currentValue")]
    public double? CurrentValue { get; set; }
    [JsonPropertyName("activeLoadPower")]
    public double? ActiveLoadPower { get; set; }
    [JsonPropertyName("reactiveLoadPower")]
    public double? ReactiveLoadPower { get; set; }
    [JsonPropertyName("fullLoadPower")]
    public double? FullLoadPower { get; set; }
    [JsonPropertyName("loadPowerFactor")]
    public double? LoadPowerFactor { get; set; }
    [JsonPropertyName("microgridFrequency")]
    public double? MicrogridFrequency { get; set; }
    [JsonPropertyName("time")]
    public DateTime Time { get; set; }

    public virtual MicrogridParametersMeasure? Block { get; set; }
}
