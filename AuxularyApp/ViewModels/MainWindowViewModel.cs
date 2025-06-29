﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuxularyApp.ViewModels.Base;
using AuxularyApp.Models;
using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Input;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using System.Windows;
using System.Collections.ObjectModel;
using AuxularyApp.Models.DataModels;
using System.Text.Json;
using System.Windows.Media;
using System.Runtime.Serialization;
using System.Net.Http.Json;
using AuxularyApp.Models.DataModels.Base;
using LiveChartsCore.Kernel.Sketches;
using LiveChartsCore.SkiaSharpView.VisualElements;
using GalaSoft.MvvmLight.Command;
using AuxularyApp.Infrastructure.Commands;
using LiveChartsCore.Kernel.Events;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.Input;
using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.Kernel.Events;
using LiveChartsCore.Kernel.Sketches;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using System.Xml.Linq;
namespace AuxularyApp.ViewModels
{
    internal partial class MainWindowViewModel : ViewModel
    {
        int maxVal =18;
        public ObservableCollection<ObservableCollection<ParametersMeasure>> _Measurements = new ObservableCollection<ObservableCollection<ParametersMeasure>>();
        private readonly DateTimeAxis _customAxis;
        private readonly DateTimeAxis _customAxis2;
        private bool _isDown = false;
        private readonly ObservableCollection<DateTimePoint> _values1 = [];
        private readonly ObservableCollection<DateTimePoint> _values2 = [];
        private readonly ObservableCollection<DateTimePoint> _values3 = [];

        public ISeries[] Series { get; set; }
        public Axis[] ScrollableAxes { get; set; }
        public ISeries[] ScrollbarSeries { get; set; }
        public Axis[] InvisibleX { get; set; }
        public Axis[] InvisibleY { get; set; }
        public LiveChartsCore.Measure.Margin Margin { get; set; }
        public RectangularSection[] Thumbs { get; set; }
  
        #region Series
        public ObservableCollection<ISeries> Series1 { get; set; }
        private readonly List<DateTimePoint> _Voltagevalues1 = [];
        private readonly List<DateTimePoint> _ActiveLPvalues1 = [];
        private readonly List<DateTimePoint> _ReactiveLPvalues1 = [];
        private readonly List<DateTimePoint> _FullLPvalues1 = [];
        private readonly List<DateTimePoint> _MicrogridFr1 = [];
        private readonly List<DateTimePoint> _CurrentValue1 = [];
        private readonly List<DateTimePoint> _LPF1 = [];

        public ObservableCollection<ISeries> Series2 { get; set; }
        private readonly List<DateTimePoint> _Voltagevalues2 = [];
        private readonly List<DateTimePoint> _ActiveLPvalues2 = [];
        private readonly List<DateTimePoint> _ReactiveLPvalues2 = [];
        private readonly List<DateTimePoint> _FullLPvalues2 = [];
        private readonly List<DateTimePoint> _MicrogridFr2 = [];
        private readonly List<DateTimePoint> _CurrentValue2 = [];
        private readonly List<DateTimePoint> _LPF2 = [];

        public ObservableCollection<ISeries> Series3 { get; set; }
        private readonly List<DateTimePoint> _Voltagevalues3 = [];
        private readonly List<DateTimePoint> _ActiveLPvalues3 = [];
        private readonly List<DateTimePoint> _ReactiveLPvalues3 = [];
        private readonly List<DateTimePoint> _FullLPvalues3 = [];
        private readonly List<DateTimePoint> _MicrogridFr3 = [];
        private readonly List<DateTimePoint> _CurrentValue3 = [];
        private readonly List<DateTimePoint> _LPF3 = [];

        public ObservableCollection<ISeries> Series4 { get; set; }
        private readonly List<DateTimePoint> _Voltagevalues4 = [];
        private readonly List<DateTimePoint> _ActiveLPvalues4 = [];
        private readonly List<DateTimePoint> _ReactiveLPvalues4 = [];
        private readonly List<DateTimePoint> _FullLPvalues4 = [];
        private readonly List<DateTimePoint> _MicrogridFr4 = [];
        private readonly List<DateTimePoint> _CurrentValue4 = [];
        private readonly List<DateTimePoint> _LPF4 = [];

        public ObservableCollection<ISeries> Series5 { get; set; }
        private readonly List<DateTimePoint> _Voltagevalues5 = [];
        private readonly List<DateTimePoint> _ActiveLPvalues5 = [];
        private readonly List<DateTimePoint> _ReactiveLPvalues5 = [];
        private readonly List<DateTimePoint> _FullLPvalues5 = [];
        private readonly List<DateTimePoint> _MicrogridFr5 = [];
        private readonly List<DateTimePoint> _CurrentValue5 = [];
        private readonly List<DateTimePoint> _LPF5 = [];

        public ObservableCollection<ISeries> Series6 { get; set; }
        private readonly List<DateTimePoint> _Voltagevalues6 = [];
        private readonly List<DateTimePoint> _ActiveLPvalues6 = [];
        private readonly List<DateTimePoint> _ReactiveLPvalues6= [];
        private readonly List<DateTimePoint> _FullLPvalues6 = [];
        private readonly List<DateTimePoint> _MicrogridFr6 = [];
        private readonly List<DateTimePoint> _CurrentValue6 = [];
        private readonly List<DateTimePoint> _LPF6 = [];

        public ObservableCollection<ISeries> Series7 { get; set; }
        private readonly List<DateTimePoint> _Voltagevalues7 = [];
        private readonly List<DateTimePoint> _ActiveLPvalues7 = [];
        private readonly List<DateTimePoint> _ReactiveLPvalues7 = [];
        private readonly List<DateTimePoint> _FullLPvalues7 = [];
        private readonly List<DateTimePoint> _MicrogridFr7 = [];
        private readonly List<DateTimePoint> _CurrentValue7 = [];
        private readonly List<DateTimePoint> _LPF7 = [];

        #endregion
        public Axis[] XAxes { get; set; }
        public ICartesianAxis[] YAxes { get; set; } = [
        new Axis
            {
                TextSize = 13,
                // We can specify a custom separator collection
                // the library will use this separators instead of
                // calculating them based on the date of the chart
                CustomSeparators = [0,  1000, 2000,3000,4000, 5000],
                MinLimit = 0, // forces the axis to start at 0
                MaxLimit = 5000, // forces the axis to end at 100
                SeparatorsPaint = new SolidColorPaint(SKColors.Black.WithAlpha(100))
            }
        ];
        public ICartesianAxis[] YAxes2 { get; set; } = [
        new Axis
            {
                TextSize = 13,
                // We can specify a custom separator collection
                // the library will use this separators instead of
                // calculating them based on the date of the chart
                CustomSeparators = [0,  100, 200,300,400, 500],
                MinLimit = 0, // forces the axis to start at 0
                MaxLimit = 500, // forces the axis to end at 100
                SeparatorsPaint = new SolidColorPaint(SKColors.Black.WithAlpha(100))
            }
        ];

        public object Sync { get; } = new object();

        public bool IsReading { get; set; } = true;

        public MainWindowViewModel(){
            UpdateClock();
            var trend1 = 350;
            var trend2 = 318;
            var trend3 = 252;
            var r = new Random();

            for (var i = 0; i < 500; i++)
            {
                _values1.Add(new DateTimePoint(DateTime.Now.AddSeconds(i), trend1 += r.Next(-3, 3)));
                _values2.Add(new DateTimePoint(DateTime.Now.AddSeconds(i), trend2 + r.Next(-1, 1)));
                _values3.Add(new DateTimePoint(DateTime.Now.AddSeconds(i), trend3 + r.Next(-5, 5)));
            }
                

            Series = [
                new LineSeries<DateTimePoint>
            {
                Name = "МТПН1-А3",
                IsVisible = true,
                Values = _values1,
                Fill = null,
                GeometryFill = null,
                GeometryStroke = null,
                LineSmoothness = 0,
                DataPadding = new(0, 1)
            },
            //new LineSeries<DateTimePoint>
            //{
            //    Name = "МТПН1-А4",
            //    IsVisible = true,
            //    Values = _values2,
            //    Fill = null,
            //    GeometryFill = null,
            //    GeometryStroke = null,
            //    LineSmoothness = 0,
            //    DataPadding = new(0, 1)
            //},
            //    new LineSeries<DateTimePoint>
            //{
            //    Name = "ИПОС1-Р1",
            //    IsVisible = true,
            //    Values = _values3,
            //    Fill = null,
            //    GeometryFill = null,
            //    GeometryStroke = null,
            //    LineSmoothness = 0,
            //    DataPadding = new (0, 1)
            //}
            ];

            ScrollbarSeries = [
                new LineSeries<DateTimePoint>
            {
                Values = _values1,
                GeometryStroke = null,
                GeometryFill = null,
                DataPadding = new(0, 1)
            },
            //new LineSeries<DateTimePoint>
            //{
            //    Values = _values2,
            //    GeometryStroke = null,
            //    GeometryFill = null,
            //    DataPadding = new(0, 1)
            //},
            //new LineSeries<DateTimePoint>
            //{
            //    Values = _values3,
            //    GeometryStroke = null,
            //    GeometryFill = null,
            //    DataPadding = new(0, 1)
            //}
            ];


            _customAxis2 = new DateTimeAxis(TimeSpan.FromSeconds(1), Formatter)
            {
                
                //TextSize = 13,
                LabelsRotation = 90,
                //MaxLimit = DateTime.Now.AddSeconds(-20).Ticks,
                AnimationsSpeed = TimeSpan.FromMilliseconds(0),
                SeparatorsPaint = new SolidColorPaint(SKColors.Black.WithAlpha(100))
            };
            ScrollableAxes = [_customAxis2];
            Thumbs = [
                new RectangularSection
            {
                Fill = new SolidColorPaint(new SKColor(255, 205, 210, 100))
            }
            ];

            InvisibleX = [new Axis { IsVisible = false }];
            InvisibleY = [new Axis { IsVisible = false }];

            // force the left margin to be 100 and the right margin 50 in both charts, this will
            // align the start and end point of the "draw margin",
            // no matter the size of the labels in the Y axis of both chart.
            var auto = LiveChartsCore.Measure.Margin.Auto;
            Margin = new(100, auto, 50, auto);

            SwitchVisiableCommand1 = new LambdaCommand(OnSwitchVisibleCommandExecuted1, CanSwitchVisiableCommandExecuted);
            SwitchVisiableCommand2 = new LambdaCommand(OnSwitchVisibleCommandExecuted2, CanSwitchVisiableCommandExecuted);
            SwitchVisiableCommand3 = new LambdaCommand(OnSwitchVisibleCommandExecuted3, CanSwitchVisiableCommandExecuted);
            SwitchVisiableCommand4 = new LambdaCommand(OnSwitchVisibleCommandExecuted4, CanSwitchVisiableCommandExecuted);
            SwitchVisiableCommand5 = new LambdaCommand(OnSwitchVisibleCommandExecuted5, CanSwitchVisiableCommandExecuted);
            SwitchVisiableCommand6 = new LambdaCommand(OnSwitchVisibleCommandExecuted6, CanSwitchVisiableCommandExecuted);
            Series1 = [

            new LineSeries<DateTimePoint>
            {
                Name = "Напряжение",
                IsVisible = true,
                Values = _Voltagevalues1,
                Fill = null,
                GeometryFill = null,
                GeometryStroke = null,
                LineSmoothness = 0
            },
            new LineSeries<DateTimePoint>
            {
                Name = "Ток",
                IsVisible = true,
                Values = _CurrentValue1,
                Fill = null,
                GeometryFill = null,
                GeometryStroke = null,
                LineSmoothness = 0
            },
            new LineSeries<DateTimePoint>
            {
                Name = "Ативная МН",
                IsVisible = true,
                Values = _ActiveLPvalues1,
                Fill = null,
                GeometryFill = null,
                GeometryStroke = null,
                LineSmoothness = 0
            },
            new LineSeries<DateTimePoint>
            {
                Name = "Реактивная МН",
                IsVisible = true,
                Values = _ReactiveLPvalues1,
                Fill = null,
                GeometryFill = null,
                GeometryStroke = null,
                LineSmoothness = 0
            },
            new LineSeries<DateTimePoint>
            {
                Name = "Полная МН",
                IsVisible = true,
                Values = _FullLPvalues1,
                Fill = null,
                GeometryFill = null,
                GeometryStroke = null,
                LineSmoothness = 0
            },
            new LineSeries<DateTimePoint>
            {
                Name = "Коэф. мощности нагрузки",
                IsVisible = true,
                Values = _LPF1,
                Fill = null,
                GeometryFill = null,
                GeometryStroke = null,
                LineSmoothness = 0
            },
            new LineSeries<DateTimePoint>
            {
                Name = "Частота эл.сети",
                IsVisible = true,
                Values = _MicrogridFr1,
                Fill = null,
                GeometryFill = null,
                GeometryStroke = null,
                LineSmoothness = 0
            }
        ];
            Series2 = [
            new LineSeries<DateTimePoint>
            {
                Name = "Напряжение",
                IsVisible = true,
                Values = _Voltagevalues2,
                Fill = null,
                GeometryFill = null,
                GeometryStroke = null,
                LineSmoothness = 0
            },
            new LineSeries<DateTimePoint>
            {
                Name = "Ток",
                IsVisible = true,
                Values = _CurrentValue2,
                Fill = null,
                GeometryFill = null,
                GeometryStroke = null,
                LineSmoothness = 0
            },
            new LineSeries<DateTimePoint>
            {
                Name = "Активная МН",
                IsVisible = true,
                Values = _ActiveLPvalues2,
                Fill = null,
                GeometryFill = null,
                GeometryStroke = null,
                LineSmoothness = 1
            },
            new LineSeries<DateTimePoint>
            {
                Name = "Реактивная МН",
                IsVisible = true,
                Values = _ReactiveLPvalues2,
                Fill = null,
                GeometryFill = null,
                GeometryStroke = null
            },
            new LineSeries<DateTimePoint>
            {
                Name = "Полная МН",
                IsVisible = true,
                Values = _FullLPvalues2,
                Fill = null,
                GeometryFill = null,
                GeometryStroke = null
            },
            new LineSeries<DateTimePoint>
            {
                Name = "Коэф. мощности нагрузки",
                IsVisible = true,
                Values = _LPF2,
                Fill = null,
                GeometryFill = null,
                GeometryStroke = null,
                LineSmoothness = 0
            },
            new LineSeries<DateTimePoint>
            {
                Name = "Частота эл.сети",
                IsVisible = true,
                Values = _MicrogridFr2,
                Fill = null,
                GeometryFill = null,
                GeometryStroke = null,
                LineSmoothness = 0
            }
        ];
            Series3 = [
            new LineSeries<DateTimePoint>
            {
                Name = "Напряжение",
                IsVisible = true,
                Values = _Voltagevalues3,
                Fill = null,
                GeometryFill = null,
                GeometryStroke = null,
                LineSmoothness = 0
            },
            new LineSeries<DateTimePoint>
            {
                Name = "Ток",
                IsVisible = true,
                Values = _CurrentValue3,
                Fill = null,
                GeometryFill = null,
                GeometryStroke = null,
                LineSmoothness = 0
            },
            new LineSeries<DateTimePoint>
            {
                Name = "Активная МН",
                IsVisible = true,
                Values = _ActiveLPvalues3,
                Fill = null,
                GeometryFill = null,
                GeometryStroke = null,
                LineSmoothness = 1
            },
            new LineSeries<DateTimePoint>
            {
                Name = "Реактивная МН",
                IsVisible = true,
                Values = _ReactiveLPvalues3,
                Fill = null,
                GeometryFill = null,
                GeometryStroke = null
            },
            new LineSeries<DateTimePoint>
            {
                Name = "Полная МН",
                IsVisible = true,
                Values = _FullLPvalues3,
                Fill = null,
                GeometryFill = null,
                GeometryStroke = null
            },
            new LineSeries<DateTimePoint>
            {
                Name = "Коэф. мощности нагрузки",
                IsVisible = true,
                Values = _LPF3,
                Fill = null,
                GeometryFill = null,
                GeometryStroke = null,
                LineSmoothness = 0
            },
            new LineSeries<DateTimePoint>
            {
                Name = "Частота эл.сети",
                IsVisible = true,
                Values = _MicrogridFr3,
                Fill = null,
                GeometryFill = null,
                GeometryStroke = null,
                LineSmoothness = 0
            }
        ];
            Series4 = [
            new LineSeries<DateTimePoint>
            {
                Name = "Напряжение",
                IsVisible = true,
                Values = _Voltagevalues4,
                Fill = null,
                GeometryFill = null,
                GeometryStroke = null,
                LineSmoothness = 0
            },
            new LineSeries<DateTimePoint>
            {
                Name = "Ток",
                IsVisible = true,
                Values = _CurrentValue4,
                Fill = null,
                GeometryFill = null,
                GeometryStroke = null,
                LineSmoothness = 0
            },
            new LineSeries<DateTimePoint>
            {
                Name = "Активная МН",
                IsVisible = true,
                Values = _ActiveLPvalues4,
                Fill = null,
                GeometryFill = null,
                GeometryStroke = null,
                LineSmoothness = 1
            },
            new LineSeries<DateTimePoint>
            {
                Name = "Реактивная МН",
                IsVisible = true,
                Values = _ReactiveLPvalues4,
                Fill = null,
                GeometryFill = null,
                GeometryStroke = null
            },
            new LineSeries<DateTimePoint>
            {
                Name = "Полная Мн",
                IsVisible = true,
                Values = _FullLPvalues4,
                Fill = null,
                GeometryFill = null,
                GeometryStroke = null
            },
            new LineSeries<DateTimePoint>
            {
                Name = "Коэф. мощности нагрузки",
                IsVisible = true,
                Values = _LPF4,
                Fill = null,
                GeometryFill = null,
                GeometryStroke = null,
                LineSmoothness = 0
            },
            new LineSeries<DateTimePoint>
            {
                Name = "Частота эл.сети",
                IsVisible = true,
                Values = _MicrogridFr4,
                Fill = null,
                GeometryFill = null,
                GeometryStroke = null,
                LineSmoothness = 0
            }
        ];
            Series5 = [
            new LineSeries<DateTimePoint>
            {
                Name = "Напряжение",
                IsVisible = true,
                Values = _Voltagevalues5,
                Fill = null,
                GeometryFill = null,
                GeometryStroke = null,
                LineSmoothness = 0
            },
            new LineSeries<DateTimePoint>
            {
                Name = "Ток",
                IsVisible = true,
                Values = _CurrentValue5,
                Fill = null,
                GeometryFill = null,
                GeometryStroke = null,
                LineSmoothness = 0
            },
            new LineSeries<DateTimePoint>
            {
                Name = "Активная МН",
                IsVisible = true,
                Values = _ActiveLPvalues5,
                Fill = null,
                GeometryFill = null,
                GeometryStroke = null,
                LineSmoothness = 1
            },
            new LineSeries<DateTimePoint>
            {
                Name = "Реактивная МН",
                IsVisible = true,
                Values = _ReactiveLPvalues5,
                Fill = null,
                GeometryFill = null,
                GeometryStroke = null
            },
            new LineSeries<DateTimePoint>
            {
                Name = "Полная МН",
                IsVisible = true,
                Values = _FullLPvalues5,
                Fill = null,
                GeometryFill = null,
                GeometryStroke = null
            },
            new LineSeries<DateTimePoint>
            {
                Name = "Коэф. мощности нагрузки",
                IsVisible = true,
                Values = _LPF5,
                Fill = null,
                GeometryFill = null,
                GeometryStroke = null,
                LineSmoothness = 0
            },
            new LineSeries<DateTimePoint>
            {
                Name = "Частота эл.сети",
                IsVisible = true,
                Values = _MicrogridFr5,
                Fill = null,
                GeometryFill = null,
                GeometryStroke = null,
                LineSmoothness = 0
            }
        ];
            Series6 = [
            new LineSeries<DateTimePoint>
            {
                Name = "Нпряжение",
                IsVisible = true,
                Values = _Voltagevalues6,
                Fill = null,
                GeometryFill = null,
                GeometryStroke = null,
                LineSmoothness = 0
            },
            new LineSeries<DateTimePoint>
            {
                Name = "Ток",
                IsVisible = true,
                Values = _CurrentValue6,
                Fill = null,
                GeometryFill = null,
                GeometryStroke = null,
                LineSmoothness = 0
            },
            new LineSeries<DateTimePoint>
            {
                Name = "Активная МН",
                IsVisible = true,
                Values = _ActiveLPvalues6,
                Fill = null,
                GeometryFill = null,
                GeometryStroke = null,
                LineSmoothness = 1
            },
            new LineSeries<DateTimePoint>
            {
                Name = "Реактивная МН",
                IsVisible = true,
                Values = _ReactiveLPvalues6,
                Fill = null,
                GeometryFill = null,
                GeometryStroke = null
            },
            new LineSeries<DateTimePoint>
            {
                Name = "Полная МН",
                IsVisible = true,
                Values = _FullLPvalues6,
                Fill = null,
                GeometryFill = null,
                GeometryStroke = null
            },
            new LineSeries<DateTimePoint>
            {
                Name = "Коэф. мощности нагрузки",
                IsVisible = true,
                Values = _LPF6,
                Fill = null,
                GeometryFill = null,
                GeometryStroke = null,
                LineSmoothness = 0
            },
            new LineSeries<DateTimePoint>
            {
                Name = "Частота эл.сети",
                IsVisible = true,
                Values = _MicrogridFr6,
                Fill = null,
                GeometryFill = null,
                GeometryStroke = null,
                LineSmoothness = 0
            }
        ];
            Series7 = [
            new LineSeries<DateTimePoint>
            {
                Name = "Напряжение",
                IsVisible = true,
                Values = _Voltagevalues7,
                Fill = null,
                GeometryFill = null,
                GeometryStroke = null,
                LineSmoothness = 0
            },
            new LineSeries<DateTimePoint>
            {
                Name = "Ток",
                IsVisible = true,
                Values = _CurrentValue7,
                Fill = null,
                GeometryFill = null,
                GeometryStroke = null,
                LineSmoothness = 0
            },
            new LineSeries<DateTimePoint>
            {
                Name = "Активная МН",
                IsVisible = true,
                Values = _ActiveLPvalues7,
                Fill = null,
                GeometryFill = null,
                GeometryStroke = null,
                LineSmoothness = 1
            },
            new LineSeries<DateTimePoint>
            {
                Name = "Реактивная МН",
                IsVisible = true,
                Values = _ReactiveLPvalues7,
                Fill = null,
                GeometryFill = null,
                GeometryStroke = null
            },
            new LineSeries<DateTimePoint>
            {
                Name = "Полная МН",
                IsVisible = true,
                Values = _FullLPvalues7,
                Fill = null,
                GeometryFill = null,
                GeometryStroke = null
            },
            new LineSeries<DateTimePoint>
            {
                Name = "Коэф. мощности нагрузки",
                IsVisible = true,
                Values = _LPF7,
                Fill = null,
                GeometryFill = null,
                GeometryStroke = null,
                LineSmoothness = 0
            },
            new LineSeries<DateTimePoint>
            {
                Name = "Частота эл.сети",
                IsVisible = true,
                Values = _MicrogridFr7,
                Fill = null,
                GeometryFill = null,
                GeometryStroke = null,
                LineSmoothness = 0
            }
        ];
            _customAxis = new DateTimeAxis(TimeSpan.FromSeconds(1), Formatter)
            {
                TextSize = 13,
                LabelsRotation = 90,
                //MaxLimit = DateTime.Now.AddSeconds(-20).Ticks,
                CustomSeparators = GetSeparators(),
                AnimationsSpeed = TimeSpan.FromMilliseconds(0),
                SeparatorsPaint = new SolidColorPaint(SKColors.Black.WithAlpha(100))
            };
            XAxes = [_customAxis];
            GetResponse();
        }
        public LabelVisual[] Title { get; set; } =
        [
            new LabelVisual
            {
                Text = "МТПН1-А3",
                TextSize = 25,
                Padding = new LiveChartsCore.Drawing.Padding(15)
            },
            new LabelVisual
            {
                Text = "МТПН1-А4",
                TextSize = 25,
                Padding = new LiveChartsCore.Drawing.Padding(15)
            },
                new LabelVisual
            {
                Text = "МТПН1-А5",
                TextSize = 25,
                Padding = new LiveChartsCore.Drawing.Padding(15)
            },
                new LabelVisual
            {
                Text = "МТПН1-А6",
                TextSize = 25,
                Padding = new LiveChartsCore.Drawing.Padding(15)
            },
                new LabelVisual
            {
                Text = "ИПОС1-Р1",
                TextSize = 25,
                Padding = new LiveChartsCore.Drawing.Padding(15)
            },
                new LabelVisual
            {
                Text = "ИПОС1-Р2",
                TextSize = 25,
                Padding = new LiveChartsCore.Drawing.Padding(15)
            }
        ];
        private static double[] GetSeparators()
        {

            var now = DateTime.Now;
            return
            [
                now.AddSeconds(-8).Ticks,
                now.AddSeconds(-7).Ticks,
                now.AddSeconds(-6).Ticks,
                now.AddSeconds(-5).Ticks,
                now.AddSeconds(-4).Ticks,
                now.AddSeconds(-3).Ticks,
                now.AddSeconds(-2).Ticks,
            ];
        }

        private static string Formatter(DateTime date)
        {
           // var secsAgo = (DateTime.Now - date).TotalSeconds;

            return $"{date.ToString("HH:mm:ss")}";
               
        }
        #region Команды
        [RelayCommand]
        public void ChartUpdated(ChartCommandArgs args)
        {
            var cartesianChart = (ICartesianChartView)args.Chart;

            var x = cartesianChart.XAxes.First();

            // update the scroll bar thumb when the chart is updated (zoom/pan)
            // this will let the user know the current visible range
            var thumb = Thumbs[0];

            thumb.Xi = x.MinLimit;
            thumb.Xj = x.MaxLimit;
        }

        [RelayCommand]
        public void PointerDown(PointerCommandArgs args) =>
            _isDown = true;

        [RelayCommand]
        public void PointerMove(PointerCommandArgs args)
        {
            if (!_isDown) return;

            var chart = (ICartesianChartView)args.Chart;
            var positionInData = chart.ScalePixelsToData(args.PointerPosition);

            var thumb = Thumbs[0];
            var currentRange = thumb.Xj - thumb.Xi;

            // update the scroll bar thumb when the user is dragging the chart
            thumb.Xi = positionInData.X - currentRange / 2;
            thumb.Xj = positionInData.X + currentRange / 2;

            // update the chart visible range
            ScrollableAxes[0].MinLimit = thumb.Xi;
            ScrollableAxes[0].MaxLimit = thumb.Xj;
        }

        [RelayCommand]
        public void PointerUp(PointerCommandArgs args) =>
            _isDown = false;
        public ICommand SwitchVisiableCommand1 { get; }
        private void OnSwitchVisibleCommandExecuted1(object p)
        {
            int i = int.Parse(p.ToString());
            
            Series1[i].IsVisible = !Series1[i].IsVisible;
        }
        public ICommand SwitchVisiableCommand2 { get; }
        private void OnSwitchVisibleCommandExecuted2(object p)
        {
            int i = int.Parse(p.ToString());

            Series2[i].IsVisible = !Series2[i].IsVisible;
        }
        public ICommand SwitchVisiableCommand3{ get; }
        private void OnSwitchVisibleCommandExecuted3(object p)
        {
            int i = int.Parse(p.ToString());

            Series3[i].IsVisible = !Series3[i].IsVisible;
        }
        public ICommand SwitchVisiableCommand4 { get; }
        private void OnSwitchVisibleCommandExecuted4(object p)
        {
            int i = int.Parse(p.ToString());

            Series4[i].IsVisible = !Series4[i].IsVisible;
        }
        public ICommand SwitchVisiableCommand5 { get; }
        private void OnSwitchVisibleCommandExecuted5(object p)
        {
            int i = int.Parse(p.ToString());

            Series5[i].IsVisible = !Series5[i].IsVisible;
        }
        public ICommand SwitchVisiableCommand6 { get; }
        private void OnSwitchVisibleCommandExecuted6(object p)
        {
            int i = int.Parse(p.ToString());

            Series6[i].IsVisible = !Series6[i].IsVisible;
        }
        private bool CanSwitchVisiableCommandExecuted(object p) => true;
        public ICommand CanGetDataList { get; }
        private async void OnGetDataList(object p)
        {
            
            HttpClientHandler handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
                {
                    return true;
                }
            };
            HttpClient httpClient = new HttpClient(handler);
            using var response = await httpClient.GetAsync("https://localhost:7133/api/ParametersMeasures/lv");
            string content = await response.Content.ReadAsStringAsync();
            ParametersMeasure[] collection = JsonSerializer.Deserialize<ParametersMeasure[]>(content);
        }
        #endregion
        #region Properties
        #region WindowTitle
        private string _Title;
        public string Title2
            {
                get => _Title; 
                set => Set(ref _Title,value);
            }
        #endregion
        #region ParametersData
        public class ParametersData
        {
            public string ParameterName { get; set; }
            public string ParameterSubName { get; set; }
        }

        private List<ParametersData> _ParamData = new List<ParametersData>()
        {
            new ParametersData() { ParameterSubName = "Напряжение", ParameterName="VoltageValue" },
            new ParametersData() { ParameterSubName = "Ток", ParameterName="CurrentValue"},
            new ParametersData() { ParameterSubName = "Активная мощность", ParameterName="ActiveLoadPower" },
            new ParametersData() { ParameterSubName = "Реактивная мощность", ParameterName="ReactiveLoadPower"},
            new ParametersData() { ParameterSubName = "Полная мощность", ParameterName="FullLoadPower" },
            new ParametersData() { ParameterSubName = "Коэф. мощности нагрузки", ParameterName="LoadPowerFactor" },
            new ParametersData() { ParameterSubName = "Частота эл. сети", ParameterName="MicrogridFrequency" },
        };
        public List<ParametersData> ParamData { get => _ParamData; set => _ParamData = value; }
        #endregion
        #region DateTime
        private string _Date = DateTime.Now.ToString("dd.MM.yyyy\nHH:mm:ss");
        public string Date
        {
            get => _Date;
            set => Set(ref _Date, value);
        }
        private void UpdateClock()
        {
            var timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1); // Обновлять каждую секунду
            timer.Tick += (sender, e) =>
            {
                Date = DateTime.Now.ToString("dd.MM.yyyy\nHH:mm:ss");
            };
            timer.Start();
        }
        #endregion
        #endregion
        public async void GetResponse()
        {
            while (true)
            {
                await Task.Delay(500);
                HttpClientHandler handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
                    {
                        return true;
                    }
                };
                HttpClient httpClient = new HttpClient(handler);
                using var response = await httpClient.GetAsync("https://localhost:7133/api/ParametersMeasures/lv");
                string content = await response.Content.ReadAsStringAsync();
                ParametersMeasure[] collection = JsonSerializer.Deserialize<ParametersMeasure[]>(content);
                foreach (ParametersMeasure d in collection)
                {
                    d.Time = DateTime.Now;
                    switch (d.BlockId)
                    {
                        
                        case 1:
                            _Voltagevalues1.Add(new DateTimePoint(d.Time, d.VoltageValue));
                            if (_Voltagevalues1.Count > maxVal) _Voltagevalues1.RemoveAt(0);
                            _ReactiveLPvalues1.Add(new DateTimePoint(d.Time, d.ReactiveLoadPower));
                            if (_ReactiveLPvalues1.Count > maxVal) _ReactiveLPvalues1.RemoveAt(0);
                            _FullLPvalues1.Add(new DateTimePoint(d.Time, d.FullLoadPower));
                            if (_FullLPvalues1.Count > maxVal) _FullLPvalues1.RemoveAt(0);
                            _ActiveLPvalues1.Add(new DateTimePoint(d.Time, d.ActiveLoadPower));
                            if (_ActiveLPvalues1.Count > maxVal) _ActiveLPvalues1.RemoveAt(0);

                            _CurrentValue1.Add(new DateTimePoint(d.Time, d.CurrentValue));
                            if (_CurrentValue1.Count > maxVal) _CurrentValue1.RemoveAt(0);
                            _LPF1.Add(new DateTimePoint(d.Time, d.LoadPowerFactor));
                            if (_LPF1.Count > maxVal) _LPF1.RemoveAt(0);
                            _MicrogridFr1.Add(new DateTimePoint(d.Time, d.MicrogridFrequency));
                            if (_MicrogridFr1.Count > maxVal) _MicrogridFr1.RemoveAt(0);
                            break;
                        case 2:
                            _Voltagevalues2.Add(new DateTimePoint(d.Time, d.VoltageValue));
                            if (_Voltagevalues2.Count > maxVal) _Voltagevalues2.RemoveAt(0);
                            _ReactiveLPvalues2.Add(new DateTimePoint(d.Time, d.ReactiveLoadPower));
                            if (_ReactiveLPvalues2.Count > maxVal) _ReactiveLPvalues2.RemoveAt(0);
                            _FullLPvalues2.Add(new DateTimePoint(d.Time, d.FullLoadPower));
                            if (_FullLPvalues2.Count > maxVal) _FullLPvalues2.RemoveAt(0);
                            _ActiveLPvalues2.Add(new DateTimePoint(d.Time, d.ActiveLoadPower));
                            if (_ActiveLPvalues2.Count > maxVal) _ActiveLPvalues2.RemoveAt(0);

                            _CurrentValue2.Add(new DateTimePoint(d.Time, d.CurrentValue));
                            if (_CurrentValue2.Count > maxVal) _CurrentValue2.RemoveAt(0);
                            _LPF2.Add(new DateTimePoint(d.Time, d.LoadPowerFactor));
                            if (_LPF2.Count > maxVal) _LPF2.RemoveAt(0);
                            _MicrogridFr2.Add(new DateTimePoint(d.Time, d.MicrogridFrequency));
                            if (_MicrogridFr2.Count > maxVal) _MicrogridFr2.RemoveAt(0);
                            break;
                        case 3:
                            _Voltagevalues3.Add(new DateTimePoint(d.Time, d.VoltageValue));
                            if (_Voltagevalues3.Count > maxVal) _Voltagevalues3.RemoveAt(0);
                            _ReactiveLPvalues3.Add(new DateTimePoint(d.Time, d.ReactiveLoadPower));
                            if (_ReactiveLPvalues3.Count > maxVal) _ReactiveLPvalues3.RemoveAt(0);
                            _FullLPvalues3.Add(new DateTimePoint(d.Time, d.FullLoadPower));
                            if (_FullLPvalues3.Count > maxVal) _FullLPvalues3.RemoveAt(0);
                            _ActiveLPvalues3.Add(new DateTimePoint(d.Time, d.ActiveLoadPower));
                            if (_ActiveLPvalues3.Count > maxVal) _ActiveLPvalues3.RemoveAt(0);

                            _CurrentValue3.Add(new DateTimePoint(d.Time, d.CurrentValue));
                            if (_CurrentValue3.Count > maxVal) _CurrentValue3.RemoveAt(0);
                            _LPF3.Add(new DateTimePoint(d.Time, d.LoadPowerFactor));
                            if (_LPF3.Count > maxVal) _LPF3.RemoveAt(0);
                            _MicrogridFr3.Add(new DateTimePoint(d.Time, d.MicrogridFrequency));
                            if (_MicrogridFr3.Count > maxVal) _MicrogridFr3.RemoveAt(0);
                            break;
                        case 4:
                            _Voltagevalues4.Add(new DateTimePoint(d.Time, d.VoltageValue));
                            if (_Voltagevalues4.Count > maxVal) _Voltagevalues4.RemoveAt(0);
                            _ReactiveLPvalues4.Add(new DateTimePoint(d.Time, d.ReactiveLoadPower));
                            if (_ReactiveLPvalues4.Count > maxVal) _ReactiveLPvalues4.RemoveAt(0);
                            _FullLPvalues4.Add(new DateTimePoint(d.Time, d.FullLoadPower));
                            if (_FullLPvalues4.Count > maxVal) _FullLPvalues4.RemoveAt(0);
                            _ActiveLPvalues4.Add(new DateTimePoint(d.Time, d.ActiveLoadPower));
                            if (_ActiveLPvalues4.Count > maxVal) _ActiveLPvalues4.RemoveAt(0);

                            _CurrentValue4.Add(new DateTimePoint(d.Time, d.CurrentValue));
                            if (_CurrentValue4.Count > maxVal) _CurrentValue4.RemoveAt(0);
                            _LPF4.Add(new DateTimePoint(d.Time, d.LoadPowerFactor));
                            if (_LPF4.Count > maxVal) _LPF4.RemoveAt(0);
                            _MicrogridFr4.Add(new DateTimePoint(d.Time, d.MicrogridFrequency));
                            if (_MicrogridFr4.Count > maxVal) _MicrogridFr4.RemoveAt(0);
                            break;
                        case 5:
                            _Voltagevalues5.Add(new DateTimePoint(d.Time, d.VoltageValue));
                            if (_Voltagevalues5.Count > maxVal) _Voltagevalues5.RemoveAt(0);
                            _ReactiveLPvalues5.Add(new DateTimePoint(d.Time, d.ReactiveLoadPower));
                            if (_ReactiveLPvalues5.Count > maxVal) _ReactiveLPvalues5.RemoveAt(0);
                            _FullLPvalues5.Add(new DateTimePoint(d.Time, d.FullLoadPower));
                            if (_FullLPvalues5.Count > maxVal) _FullLPvalues5.RemoveAt(0);
                            _ActiveLPvalues5.Add(new DateTimePoint(d.Time, d.ActiveLoadPower));
                            if (_ActiveLPvalues5.Count > maxVal) _ActiveLPvalues5.RemoveAt(0);

                            _CurrentValue5.Add(new DateTimePoint(d.Time, d.CurrentValue));
                            if (_CurrentValue5.Count > maxVal) _CurrentValue5.RemoveAt(0);
                            _LPF5.Add(new DateTimePoint(d.Time, d.LoadPowerFactor));
                            if (_LPF5.Count > maxVal) _LPF5.RemoveAt(0);
                            _MicrogridFr5.Add(new DateTimePoint(d.Time, d.MicrogridFrequency));
                            if (_MicrogridFr5.Count > maxVal) _MicrogridFr5.RemoveAt(0);
                            break;
                        case 6:
                            _Voltagevalues6.Add(new DateTimePoint(d.Time, d.VoltageValue));
                            if (_Voltagevalues6.Count > maxVal) _Voltagevalues6.RemoveAt(0);
                            _ReactiveLPvalues6.Add(new DateTimePoint(d.Time, d.ReactiveLoadPower));
                            if (_ReactiveLPvalues6.Count > maxVal) _ReactiveLPvalues6.RemoveAt(0);
                            _FullLPvalues6.Add(new DateTimePoint(d.Time, d.FullLoadPower));
                            if (_FullLPvalues6.Count > maxVal) _FullLPvalues6.RemoveAt(0);
                            _ActiveLPvalues6.Add(new DateTimePoint(d.Time, d.ActiveLoadPower));
                            if (_ActiveLPvalues6.Count > maxVal) _ActiveLPvalues6.RemoveAt(0);

                            _CurrentValue6.Add(new DateTimePoint(d.Time, d.CurrentValue));
                            if (_CurrentValue6.Count > maxVal) _CurrentValue6.RemoveAt(0);
                            _LPF6.Add(new DateTimePoint(d.Time, d.LoadPowerFactor));
                            if (_LPF6.Count > maxVal) _LPF6.RemoveAt(0);
                            _MicrogridFr6.Add(new DateTimePoint(d.Time, d.MicrogridFrequency));
                            if (_MicrogridFr6.Count > maxVal) _MicrogridFr6.RemoveAt(0);
                            break;
                    }
                }
                _customAxis.MaxLimit = DateTime.Now.AddSeconds(-3).Ticks;
                _customAxis.MinLimit = DateTime.Now.AddSeconds(-8).Ticks;
                _customAxis.CustomSeparators = GetSeparators();
            }
        }

    }
}
