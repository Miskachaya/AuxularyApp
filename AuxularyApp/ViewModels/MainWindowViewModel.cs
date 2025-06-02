using System;
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
namespace AuxularyApp.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        int maxVal =18;
        public ObservableCollection<ObservableCollection<ParametersMeasure>> _Measurements = new ObservableCollection<ObservableCollection<ParametersMeasure>>();
        private readonly DateTimeAxis _customAxis;
        #region
        public ObservableCollection<ISeries> Series1 { get; set; }
        private readonly List<DateTimePoint> _Voltagevalues1 = [];
        private readonly List<DateTimePoint> _ActiveLPvalues1 = [];
        private readonly List<DateTimePoint> _ReactiveLPvalues1 = [];
        private readonly List<DateTimePoint> _FullLPvalues1 = [];

        public ObservableCollection<ISeries> Series2 { get; set; }
        private readonly List<DateTimePoint> _Voltagevalues2 = [];
        private readonly List<DateTimePoint> _ActiveLPvalues2 = [];
        private readonly List<DateTimePoint> _ReactiveLPvalues2 = [];
        private readonly List<DateTimePoint> _FullLPvalues2 = [];

        public ObservableCollection<ISeries> Series3 { get; set; }
        private readonly List<DateTimePoint> _Voltagevalues3 = [];
        private readonly List<DateTimePoint> _ActiveLPvalues3 = [];
        private readonly List<DateTimePoint> _ReactiveLPvalues3 = [];
        private readonly List<DateTimePoint> _FullLPvalues3 = [];

        public ObservableCollection<ISeries> Series4 { get; set; }
        private readonly List<DateTimePoint> _Voltagevalues4 = [];
        private readonly List<DateTimePoint> _ActiveLPvalues4 = [];
        private readonly List<DateTimePoint> _ReactiveLPvalues4 = [];
        private readonly List<DateTimePoint> _FullLPvalues4 = [];

        public ObservableCollection<ISeries> Series5 { get; set; }
        private readonly List<DateTimePoint> _Voltagevalues5 = [];
        private readonly List<DateTimePoint> _ActiveLPvalues5 = [];
        private readonly List<DateTimePoint> _ReactiveLPvalues5 = [];
        private readonly List<DateTimePoint> _FullLPvalues5 = [];

        public ObservableCollection<ISeries> Series6 { get; set; }
        private readonly List<DateTimePoint> _Voltagevalues6 = [];
        private readonly List<DateTimePoint> _ActiveLPvalues6 = [];
        private readonly List<DateTimePoint> _ReactiveLPvalues6= [];
        private readonly List<DateTimePoint> _FullLPvalues6 = [];

        #endregion
        public Axis[] XAxes { get; set; }
        public ICartesianAxis[] YAxes { get; set; } = [
        new Axis
            {
                // We can specify a custom separator collection
                // the library will use this separators instead of
                // calculating them based on the date of the chart
                CustomSeparators = [0, 100, 200, 300,400,500],
                MinLimit = 0, // forces the axis to start at 0
                MaxLimit = 500, // forces the axis to end at 100
                SeparatorsPaint = new SolidColorPaint(SKColors.Black.WithAlpha(100))
            }
        ];
      
        public object Sync { get; } = new object();

        public bool IsReading { get; set; } = true;

        public MainWindowViewModel(){
            SwitchVisiableCommand1 = new LambdaCommand(OnSwitchVisibleCommandExecuted1, CanSwitchVisiableCommandExecuted);
            SwitchVisiableCommand2 = new LambdaCommand(OnSwitchVisibleCommandExecuted2, CanSwitchVisiableCommandExecuted);
            SwitchVisiableCommand3 = new LambdaCommand(OnSwitchVisibleCommandExecuted3, CanSwitchVisiableCommandExecuted);
            SwitchVisiableCommand4 = new LambdaCommand(OnSwitchVisibleCommandExecuted4, CanSwitchVisiableCommandExecuted);
            SwitchVisiableCommand5 = new LambdaCommand(OnSwitchVisibleCommandExecuted5, CanSwitchVisiableCommandExecuted);
            SwitchVisiableCommand6 = new LambdaCommand(OnSwitchVisibleCommandExecuted6, CanSwitchVisiableCommandExecuted);
            Series1 = [

            new LineSeries<DateTimePoint>
            {
                Name = "Voltage",
                IsVisible = true,
                Values = _Voltagevalues1,
                Fill = null,
                GeometryFill = null,
                GeometryStroke = null,
                LineSmoothness = 0
            },
            new LineSeries<DateTimePoint>
            {
                Name = "ActiveLP",
                IsVisible = true,
                Values = _ActiveLPvalues1,
                Fill = null,
                GeometryFill = null,
                GeometryStroke = null,
                LineSmoothness = 0
            },
            new LineSeries<DateTimePoint>
            {
                Name = "ReactiveLP",
                IsVisible = true,
                Values = _ReactiveLPvalues1,
                Fill = null,
                GeometryFill = null,
                GeometryStroke = null,
                LineSmoothness = 0
            },
            new LineSeries<DateTimePoint>
            {
                Name = "FullLP",
                IsVisible = true,
                Values = _FullLPvalues1,
                Fill = null,
                GeometryFill = null,
                GeometryStroke = null,
                LineSmoothness = 0
            }
        ];
            Series2 = [
            new LineSeries<DateTimePoint>
            {
                Name = "Voltage",
                IsVisible = true,
                Values = _Voltagevalues2,
                Fill = null,
                GeometryFill = null,
                GeometryStroke = null,
                LineSmoothness = 0
            },
            new LineSeries<DateTimePoint>
            {
                Name = "ActiveLP",
                IsVisible = true,
                Values = _ActiveLPvalues2,
                Fill = null,
                GeometryFill = null,
                GeometryStroke = null,
                LineSmoothness = 1
            },
            new LineSeries<DateTimePoint>
            {
                Name = "ReactiveLP",
                IsVisible = true,
                Values = _ReactiveLPvalues2,
                Fill = null,
                GeometryFill = null,
                GeometryStroke = null
            },
            new LineSeries<DateTimePoint>
            {
                Name = "FullLP",
                IsVisible = true,
                Values = _FullLPvalues2,
                Fill = null,
                GeometryFill = null,
                GeometryStroke = null
            }
        ];
            Series3 = [
            new LineSeries<DateTimePoint>
            {
                Name = "Voltage",
                IsVisible = true,
                Values = _Voltagevalues3,
                Fill = null,
                GeometryFill = null,
                GeometryStroke = null,
                LineSmoothness = 0
            },
            new LineSeries<DateTimePoint>
            {
                Name = "ActiveLP",
                IsVisible = true,
                Values = _ActiveLPvalues3,
                Fill = null,
                GeometryFill = null,
                GeometryStroke = null,
                LineSmoothness = 1
            },
            new LineSeries<DateTimePoint>
            {
                Name = "ReactiveLP",
                IsVisible = true,
                Values = _ReactiveLPvalues3,
                Fill = null,
                GeometryFill = null,
                GeometryStroke = null
            },
            new LineSeries<DateTimePoint>
            {
                Name = "FullLP",
                IsVisible = true,
                Values = _FullLPvalues3,
                Fill = null,
                GeometryFill = null,
                GeometryStroke = null
            }
        ];
            Series4 = [
            new LineSeries<DateTimePoint>
            {
                Name = "Voltage",
                IsVisible = true,
                Values = _Voltagevalues4,
                Fill = null,
                GeometryFill = null,
                GeometryStroke = null,
                LineSmoothness = 0
            },
            new LineSeries<DateTimePoint>
            {
                Name = "ActiveLP",
                IsVisible = true,
                Values = _ActiveLPvalues4,
                Fill = null,
                GeometryFill = null,
                GeometryStroke = null,
                LineSmoothness = 1
            },
            new LineSeries<DateTimePoint>
            {
                Name = "ReactiveLP",
                IsVisible = true,
                Values = _ReactiveLPvalues4,
                Fill = null,
                GeometryFill = null,
                GeometryStroke = null
            },
            new LineSeries<DateTimePoint>
            {
                Name = "FullLP",
                IsVisible = true,
                Values = _FullLPvalues4,
                Fill = null,
                GeometryFill = null,
                GeometryStroke = null
            }
        ];
            Series5 = [
            new LineSeries<DateTimePoint>
            {
                Name = "Voltage",
                IsVisible = true,
                Values = _Voltagevalues5,
                Fill = null,
                GeometryFill = null,
                GeometryStroke = null,
                LineSmoothness = 0
            },
            new LineSeries<DateTimePoint>
            {
                Name = "ActiveLP",
                IsVisible = true,
                Values = _ActiveLPvalues5,
                Fill = null,
                GeometryFill = null,
                GeometryStroke = null,
                LineSmoothness = 1
            },
            new LineSeries<DateTimePoint>
            {
                Name = "ReactiveLP",
                IsVisible = true,
                Values = _ReactiveLPvalues5,
                Fill = null,
                GeometryFill = null,
                GeometryStroke = null
            },
            new LineSeries<DateTimePoint>
            {
                Name = "FullLP",
                IsVisible = true,
                Values = _FullLPvalues5,
                Fill = null,
                GeometryFill = null,
                GeometryStroke = null
            }
        ];
            Series6 = [
            new LineSeries<DateTimePoint>
            {
                Name = "Voltage",
                IsVisible = true,
                Values = _Voltagevalues6,
                Fill = null,
                GeometryFill = null,
                GeometryStroke = null,
                LineSmoothness = 0
            },
            new LineSeries<DateTimePoint>
            {
                Name = "ActiveLP",
                IsVisible = true,
                Values = _ActiveLPvalues6,
                Fill = null,
                GeometryFill = null,
                GeometryStroke = null,
                LineSmoothness = 1
            },
            new LineSeries<DateTimePoint>
            {
                Name = "ReactiveLP",
                IsVisible = true,
                Values = _ReactiveLPvalues6,
                Fill = null,
                GeometryFill = null,
                GeometryStroke = null
            },
            new LineSeries<DateTimePoint>
            {
                Name = "FullLP",
                IsVisible = true,
                Values = _FullLPvalues6,
                Fill = null,
                GeometryFill = null,
                GeometryStroke = null
            }
        ];
            _customAxis = new DateTimeAxis(TimeSpan.FromSeconds(1), Formatter)
            {
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
        #region
        private int _Int;
        public int Int
        {
            get => _Int;
            set => Set(ref _Int, value);
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
