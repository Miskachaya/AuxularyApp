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
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.VisualElements;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace AuxularyApp.Infrastructure.Graphics
{
    public class Chart 
    {
        private  ObservableCollection<ObservablePoint> _values = [];
        public ObservableCollection<ObservablePoint> GetValues() { return _values; }
        private int count=0;
        public void SetValues(int i)
        {
            count = i;
        }

        private List<DateTimePoint> _points = new List<DateTimePoint>();
        public List<DateTimePoint > GetPoints() { return _points; }
        private List<DateTimePoint> Voltagevalues { get; set; } = [];
        private List<DateTimePoint> ActiveLPvalues { get; set; } = [];
        private List<DateTimePoint> ReactiveLPvalues { get; set; } = [];
        private List<DateTimePoint> FullLPvalues { get; set; } = [];
        private List<DateTimePoint> MicrogridFr { get; set; } = [];
        private List<DateTimePoint> CurrentValue { get; set; } = [];
        private List<DateTimePoint> LPF { get; set; } = [];
        public ObservableCollection<ISeries> Series { get; set; } = [];

        public Chart()
        {
            Series = [
                new LineSeries<DateTimePoint>
                {
                    Name = "Напряжение",
                    Stroke = new SolidColorPaint(SKColors.Red) { StrokeThickness = 2 },
                    IsVisible = true,
                    Values = Voltagevalues,
                    Fill = null,
                    GeometryFill = null,
                    GeometryStroke = null,
                    LineSmoothness = 0
                },
                new LineSeries<DateTimePoint>
                {
                    Stroke = new SolidColorPaint(SKColors.Blue) { StrokeThickness = 2 },
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
                    Stroke = new SolidColorPaint(SKColors.Orange) { StrokeThickness = 2 },
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
                    Stroke = new SolidColorPaint(SKColors.Purple) { StrokeThickness = 2 },
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
                    Stroke = new SolidColorPaint(SKColors.Pink) { StrokeThickness = 2 },
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
                    Stroke = new SolidColorPaint(SKColors.Green) { StrokeThickness = 2 },
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
                    Stroke = new SolidColorPaint(SKColors.Brown) { StrokeThickness = 2 },
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

            if (Voltagevalues.Count > maxVal) Voltagevalues.RemoveAt(0);
            if (ActiveLPvalues.Count > maxVal) ActiveLPvalues.RemoveAt(0);
            if (ReactiveLPvalues.Count > maxVal) ReactiveLPvalues.RemoveAt(0);
            if (FullLPvalues.Count > maxVal) FullLPvalues.RemoveAt(0);
            if (MicrogridFr.Count > maxVal) MicrogridFr.RemoveAt(0);
            if (CurrentValue.Count > maxVal) CurrentValue.RemoveAt(0);
            if (LPF.Count > maxVal) LPF.RemoveAt(0);
        }

        public void PushRetrospectiveChartData( DateTime time,string parameter, double value)
        {
            switch (parameter)
            {
                case "VoltageValue":
                    Voltagevalues.Add(new DateTimePoint(time, value));
                    _values.Add(new ObservablePoint(count++, value));
                    _points.Add(new DateTimePoint(time, value));
                    break;
                case "ActiveLPValues":
                    ActiveLPvalues.Add(new DateTimePoint(time, value));
                    _values.Add(new ObservablePoint(count++, value));
                    _points.Add(new DateTimePoint(time, value));
                    break;
                case "ReactiveLPvalues":
                    ReactiveLPvalues.Add(new DateTimePoint(time, value));
                    _values.Add(new ObservablePoint(count++, value));
                    _points.Add(new DateTimePoint(time, value));
                    break;
                case "FullLPvalues":
                    FullLPvalues.Add(new DateTimePoint(time, value));
                    _values.Add(new ObservablePoint(count++, value));
                    _points.Add(new DateTimePoint(time, value));
                    break ;
                case "MicrogridFr":
                    MicrogridFr.Add(new DateTimePoint(time, value));
                    _values.Add(new ObservablePoint(count++, value));
                    _points.Add(new DateTimePoint(time, value));
                    break;
                case "CurrentValue":
                    CurrentValue.Add(new DateTimePoint(time, value));
                    _values.Add(new ObservablePoint(count++, value));
                    _points.Add(new DateTimePoint(time, value));
                    break ; 
                case "LPF":
                    LPF.Add(new DateTimePoint(time, value));
                    _values.Add(new ObservablePoint(count++, value));
                    _points.Add(new DateTimePoint(time, value));
                    break;
            }
        }
    }
}
