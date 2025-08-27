using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.Drawing;
using LiveChartsCore.Kernel;
using LiveChartsCore.Kernel.Events;
using LiveChartsCore.Kernel.Sketches;
using LiveChartsCore.Measure;
using LiveChartsCore.Motion;
using LiveChartsCore.Painting;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.VisualElements;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuxularyApp.Infrastructure.Graphics
{
    public class Chart 
    {
        public List<DateTimePoint> Voltagevalues { get; set; } = [];
        public List<DateTimePoint> ActiveLPvalues { get; set; } = [];
        private List<DateTimePoint> ReactiveLPvalues { get; set; } = [];
        public List<DateTimePoint> FullLPvalues { get; set; } = [];
        public List<DateTimePoint> MicrogridFr { get; set; } = [];
        public List<DateTimePoint> CurrentValue { get; set; } = [];
        public List<DateTimePoint> LPF { get; set; } = [];
        public ObservableCollection<ISeries> Series { get; set; }
        public Chart()
        {
            Series= [
                new LineSeries<DateTimePoint>
                {
                    Name = "Напряжение",
                    IsVisible = true,
                    Values = Voltagevalues,
                    Fill = null,
                    GeometryFill = null,
                    GeometryStroke = null,
                    LineSmoothness = 0
                },
                new LineSeries<DateTimePoint>
                {
                    Name = "Ток",
                    IsVisible = true,
                    Values = CurrentValue,
                    Fill = null,
                    GeometryFill = null,
                    GeometryStroke = null,
                    LineSmoothness = 0
                },
                new LineSeries<DateTimePoint>
                {
                    Name = "Ативная МН",
                    IsVisible = true,
                    Values = ActiveLPvalues,
                    Fill = null,
                    GeometryFill = null,
                    GeometryStroke = null,
                    LineSmoothness = 0
                },
                new LineSeries<DateTimePoint>
                {
                    Name = "Реактивная МН",
                    IsVisible = true,
                    Values = ReactiveLPvalues,
                    Fill = null,
                    GeometryFill = null,
                    GeometryStroke = null,
                    LineSmoothness = 0
                },
                new LineSeries<DateTimePoint>
                {
                    Name = "Полная МН",
                    IsVisible = true,
                    Values = FullLPvalues,
                    Fill = null,
                    GeometryFill = null,
                    GeometryStroke = null,
                    LineSmoothness = 0
                },
                new LineSeries<DateTimePoint>
                {
                    Name = "Коэф. мощности нагрузки",
                    IsVisible = true,
                    Values = LPF,
                    Fill = null,
                    GeometryFill = null,
                    GeometryStroke = null,
                    LineSmoothness = 0
                },
                new LineSeries<DateTimePoint>
                {
                    Name = "Частота эл.сети",
                    IsVisible = true,
                    Values = MicrogridFr,
                    Fill = null,
                    GeometryFill = null,
                    GeometryStroke = null,
                    LineSmoothness = 0
                }
            ];
        }
        public void PushChartData(int maxVal,DateTime time, double voltageValue, double activeLPvalue, double reactiveLPvalue, double fullLPvalue, double microgridFr, double currentValue, double lPF )
        {
            Voltagevalues.Add(new DateTimePoint(time, voltageValue));
            ActiveLPvalues.Add(new DateTimePoint(time, activeLPvalue));
            ReactiveLPvalues.Add(new DateTimePoint (time, reactiveLPvalue));
            FullLPvalues.Add(new DateTimePoint(time, fullLPvalue));
            MicrogridFr.Add(new DateTimePoint(time, microgridFr));
            CurrentValue.Add(new DateTimePoint(time, currentValue));
            LPF.Add(new DateTimePoint(time, lPF));

            if (Voltagevalues.Count > maxVal) Voltagevalues.Remove(0);
            if (ActiveLPvalues.Count > maxVal) ActiveLPvalues.Remove(0);
            if (ReactiveLPvalues.Count > maxVal) ReactiveLPvalues.Remove(0);
            if (FullLPvalues.Count > maxVal) FullLPvalues.Remove(0);
            if (MicrogridFr.Count > maxVal) MicrogridFr.Remove(0);
            if (CurrentValue.Count > maxVal) CurrentValue.Remove(0);
            if (LPF.Count > maxVal) LPF.Remove(0);
        }
    }
}
