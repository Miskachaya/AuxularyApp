using AuxularyApp.Infrastructure.Commands;
using AuxularyApp.Models;
using AuxularyApp.Models.DataModels.Base;
using AuxularyApp.Models.DataModels.InstructionModels;
using AuxularyApp.Models.DataModels.MicrogridModels;
using AuxularyApp.Models.DataModels.ViewComponentModels;
using AuxularyApp.ViewModels.Base;
using AuxularyApp.Views;
using CommunityToolkit.Mvvm.Input;
using GalaSoft.MvvmLight.Command;
using LiveChartsCore;
using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.Defaults;
using LiveChartsCore.Kernel.Events;
using LiveChartsCore.Kernel.Events;
using LiveChartsCore.Kernel.Sketches;
using LiveChartsCore.Kernel.Sketches;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.VisualElements;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SkiaSharp;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;
using System.Threading.Tasks;
using System.Timers;
using System.Web;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml.Linq;
namespace AuxularyApp.ViewModels
{
    internal partial class MainWindowViewModel : ViewModel
    { 
        public ObservableCollection<AuxularyApp.Infrastructure.Graphics.Chart> ChartCollection { get;  } = [];
        private static HttpClient httpClient { get; set; }
        public ObservableCollection<Instruction> CompletedInstructions { get; } = [];
        public ObservableCollection<Instruction> PlannedInstructions { get; } = new ObservableCollection<Instruction>();
        int maxVal =18;
        public ObservableCollection<ObservableCollection<ParametersMeasure>> _Measurements = new ObservableCollection<ObservableCollection<ParametersMeasure>>();
        private readonly DateTimeAxis _customAxis;
        private readonly DateTimeAxis _customAxis2;
        private bool _isDown = false;
        public readonly ObservableCollection<DateTimePoint> _values1 = [];
        private readonly ObservableCollection<DateTimePoint> _values2 = [];
        private readonly ObservableCollection<DateTimePoint> _values3 = [];

        public Dictionary<int, (ParametersChange, StateChange)> GeneralDictionary = new();

        public ObservableCollection<AuxularyApp.Infrastructure.Graphics.Chart> RetrospectiveChartCollection { get; } = [];

        //public Instruction instruction = new Instruction();
        public List<InstructionStep> steps = new List<InstructionStep>();   
        public ObservableCollection<ParametersChange> ParameterPanelItems { get; } = new ObservableCollection<ParametersChange>();
        public ObservableCollection<StateChange> StatePanelItems { get; } = new ObservableCollection<StateChange>();
        public ICommand AddParameterPanelCommand { get; }
        public ICommand AddStatePanelCommand { get; }
        public ICommand AcceptCommand { get; }
        
        
        // Общий список всех добавленных панелей (для отображения)
        public ObservableCollection<object> AddedPanels { get; } = new ObservableCollection<object>();

        public List<int> AddedPanelsValue { get; } = new List<int>();
        private void AddStatePanel()
        {
            
            AddedPanelsValue.Add(0);
            InstructionStep step = new InstructionStep();
            steps.Add(step);
            step.StepNumber = steps.Count;
            StateChange newItem = new StateChange();
            newItem.Time = DateTime.Now.ToString("HH:mm:ss dd.MM.yyyy");
            AddedPanels.Add(newItem);
            StatePanelItems.Add(newItem);
            GeneralDictionary.Add(steps.Count - 1, (null, newItem));
            //MessageBox.Show($"{steps.Count} {StatePanelItems.Count}" );
            
            /*AddedPanels.Add(new { Type = "Parameter", Content = newItem });*/ // Можно передавать ViewModel вместо анонимного типа
        }
        private void AddParameterPanel()
        {
            AddedPanelsValue.Add(0);
            InstructionStep step = new InstructionStep();
            steps.Add(step);
            step.StepNumber = steps.Count;
            ParametersChange newItem = new ParametersChange();
            newItem.Time = DateTime.Now.ToString("HH:mm:ss dd.MM.yyyy");
            AddedPanels.Add(newItem);
            ParameterPanelItems.Add(newItem);
            GeneralDictionary.Add(steps.Count - 1, (newItem,null));
            //MessageBox.Show($"{steps.Count} {ParameterPanelItems.Count}");
        }
        public class RelayCommand : ICommand
        {
            private readonly Action _execute;
            public RelayCommand(Action execute) => _execute = execute;
            public bool CanExecute(object parameter) => true;
            public void Execute(object parameter) => _execute();
            public event EventHandler CanExecuteChanged;
        }
        public ISeries[] Series { get; set; }
        public Axis[] ScrollableAxes { get; set; }
        public ISeries[] ScrollbarSeries { get; set; }
        public Axis[] InvisibleX { get; set; }
        public Axis[] InvisibleY { get; set; }
        public LiveChartsCore.Measure.Margin Margin { get; set; }
        public RectangularSection[] Thumbs { get; set; }

        #region Series
        
        public void CreateCharts()
        {
            for (int i = 1; i <= 6; i++)
            {
               AuxularyApp.Infrastructure.Graphics.Chart chart = new();
                ChartCollection.Add(chart);
                AuxularyApp.Infrastructure.Graphics.Chart Rchart = new();
                RetrospectiveChartCollection.Add(Rchart);
            }
        }
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
            CreateCharts();
            
            HttpClientHandler handler = new()
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
                {
                    return true;
                }
            };
            httpClient = new HttpClient(handler);

            AddParameterPanelCommand = new RelayCommand(AddParameterPanel);
            AddStatePanelCommand = new RelayCommand(AddStatePanel);
            AcceptCommand = new AsyncLambdaCommand(Acept);
            UpdateClock();
            var trend1 = 350;
            var trend2 = 318;
            var trend3 = 252;
            var r = new Random();

            //for (var i = 0; i < 500; i++)
            //{
            //    _values1.Add(new DateTimePoint(DateTime.Now.AddSeconds(i), trend1 += r.Next(-3, 3)));
            //    _values2.Add(new DateTimePoint(DateTime.Now.AddSeconds(i), trend2 + r.Next(-1, 1)));
            //    _values3.Add(new DateTimePoint(DateTime.Now.AddSeconds(i), trend3 + r.Next(-5, 5)));
            //}
                

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
            //AddAcceptCommand = new GalaSoft.MvvmLight.CommandWpf.RelayCommand<string>(OnAccept);
            //AddStepStateCommand = new LambdaCommand(OnAddState, CanAddStepStateExecuted);
            //AddStepParameterCommand = new LambdaCommand(OnAddStepParameter, CanAddStepParameterExecuted);
            SwitchVisiableCommand = new LambdaCommand(OnSwitchVisibleCommandExecuted, CanSwitchVisiableCommandExecuted);
            CreateNewInstructionCommand = new LambdaCommand(OnCreateNewInstructionCommand, CanSwitchVisiableCommandExecuted);
            GetDataList=new LambdaCommand(OnGetDataList,CanSwitchVisiableCommandExecuted);

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

        public ICommand CreateNewInstructionCommand { get; }
        private void OnCreateNewInstructionCommand(object p)
        {
            if (AddedPanels.Count != 0)
            {
                MessageBoxResult result = MessageBox.Show("Подтвердив, все шаги текущей инструкции удалятся.\n\nВы хотите продолжить?", "Подтверждение", MessageBoxButton.YesNoCancel);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        AddedPanels.Clear();
                        AddedPanelsValue.Clear();
                        steps.Clear();
                        GeneralDictionary.Clear();
                        break;
                    case MessageBoxResult.No:
                        // Действия при выборе "Нет"
                        break;
                }
            }
        }
        public ICommand SwitchVisiableCommand { get; }
        private void OnSwitchVisibleCommandExecuted(object p)
        {
            
            string[] array = p.ToString().Split(' ');
            int chartNum = int.Parse(array[0]);
            int paramNum = int.Parse(array[1]);
            ChartCollection[chartNum].Series[paramNum].IsVisible=!ChartCollection[chartNum].Series[paramNum].IsVisible;

        }

        public ICommand SwitchVisiableRetrospectiveCommand { get; }
        private void OnSwitchVisibleRetrospectiveCommandExecuted(object p)
        {

            string[] array = p.ToString().Split(' ');
            int chartNum = int.Parse(array[0]);
            int paramNum = int.Parse(array[1]);
            ChartCollection[chartNum].Series[paramNum].IsVisible = !ChartCollection[chartNum].Series[paramNum].IsVisible;

        }

        private string _firstDate;
        public string FirstDate
        {
            get => _firstDate; set => _firstDate = value;
        }
        private string _seconfDate;
        public string SeconfDate
        {
            get => _seconfDate; set => _seconfDate = value;
        }
        private bool CanSwitchVisiableCommandExecuted(object p) => true;
        public ICommand GetDataList { get; }
        private async void OnGetDataList(object p)
        {
            //DateTime a = DateTime.Parse(FirstDate,f);
            using HttpResponseMessage response = await httpClient.GetAsync($"https://localhost:7133/api/ParametersMeasures/Retrospective{FirstDate}b{SeconfDate}");
            string content = await response.Content.ReadAsStringAsync();
            ParametersMeasure[] collection = JsonSerializer.Deserialize<ParametersMeasure[]>(content);
            foreach (ParametersMeasure d in collection)
            {
                RetrospectiveChartCollection[d.BlockId.Value - 1].PushRetrospectiveChartData(d.Time, SelectedKey,d.LoadPowerFactor.Value);
                _values1.Add(new DateTimePoint(d.Time,d.VoltageValue));
            }
            MessageBox.Show(RetrospectiveChartCollection[3].Series[1].Values.ToString());
        }
        private string _selectedKey="Действ. знач. напряжения";
        public string SelectedKey
        {
            get => _selectedKey;
            set => _selectedKey = value;
        }
        public async Task Acept()
        {
            Instruction instruction = new Instruction();
            instruction.State = "unaccess";
            instruction.Name = instructionNote;
            for (int i = 0; i < steps.Count; i++)
            {
                steps[i].StepNumber = i + 1;
                if (GeneralDictionary[i].Item1 == null)
                {
                    StateChange SC = GeneralDictionary[i].Item2;
                    steps[i].StateChanges.Add(SC);
                    steps[i].Time = GeneralDictionary[i].Item2.Time;
                    steps[i].Block = GeneralDictionary[i].Item2.Block;
                }
                else
                {
                    ParametersChange PC = GeneralDictionary[i].Item1;
                    steps[i].ParametersChanges.Add(PC);
                    steps[i].Time = GeneralDictionary[i].Item1.Time;
                    steps[i].Block = GeneralDictionary[i].Item1.Block;
                }
            }
            instruction.InstructionSteps = steps;
            var options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                WriteIndented = true
            };
            string json = JsonSerializer.Serialize<Instruction>(instruction, options);
            json = json.Replace("\r\n", "");
            // replace \" with "
            json = json.Replace('\"', '"');
            json = json.Replace("\'", "");
            //Clipboard.SetText(json);
            //MessageBox.Show(json);

            var factory = new ConnectionFactory() { HostName = "localhost" };
            using var connection = await factory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();
            {
                // Создание очереди (если её нет)
                await channel.QueueDeclareAsync(queue: "instructionQueue",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                // Сообщение для отправки
                string message = json;
                var body = Encoding.UTF8.GetBytes(message);

                // Отправка сообщения
                await channel.BasicPublishAsync(exchange: string.Empty, routingKey: "instructionQueue", body: body);
            }
            AddedPanels.Clear();
            OnCreateNewInstructionCommand(new object());
            PlannedInstructions.Add(instruction);
            steps.Clear();
            GeneralDictionary.Clear();
            AddedPanelsValue.Clear();
            instructionNote = DateTime.Now.ToString("yy/MM/dd/HH/mm");
            instruction = null;
        }

        private bool CanSetStateExecuted(object p) => true;
        public ICommand AddSetStateCommand { get; }
        public async void OnSetState(object p)
        {
            InstructionStep IS = instructionStepList.Last();
            IS.Time = DateStep;
            IS.StepNumber = instructionStepList.Count + 1;
            StateChange stateChange = new StateChange();
        }
        #endregion
        #region Properties 
        private ObservableCollection<ISeries> _retrospectiveSeries;
        public ObservableCollection<ISeries> RetrospectiveSeries
        {
            get => _retrospectiveSeries; set => Set(ref _retrospectiveSeries, value);
        }
        private Dictionary<string , string> _paramData= new Dictionary<string, string>{ {"VolatgeValue","Действ. знач. напржение"}, {"CurrentValue","Действ. знач. тока" },{ "ActiveLPValues","Активная МН" },{ "ReactiveLPvalues", "Реактивная МН"},{"FullLPvalues","Полная МН" },{ "MicrogridFr", "Коэф. мощности нагрузки" }, { "LPF", "Частота эл. сети" } };
        public Dictionary<string , string> ParamData { get => _paramData; set => _paramData = value; }
        private bool[] _blockId = { false,false,false,false,false,false,false};
        public bool[] BlockId
        {
            get => _blockId; set => _blockId = value;
        }
        #region Instruction
        private Instruction _Instruction;
        public Instruction Instruction
        {
            get => _Instruction;
            set => Set(ref _Instruction, value);
        }
        #endregion
        #region InstructionStepList
        private ObservableCollection<InstructionStep> _instructionStepList = new ObservableCollection<InstructionStep>();

        public ObservableCollection<InstructionStep> instructionStepList
        {
            get => _instructionStepList;
            set => Set(ref _instructionStepList, value);
        }
        #endregion
        #region parameterChangeList
        private ObservableCollection<ParametersChange> _parameterChangeList = new ObservableCollection<ParametersChange>();

        public ObservableCollection<ParametersChange> parameterChangeList
        {
            get => _parameterChangeList;
            set => Set(ref _parameterChangeList, value);
        }
        #endregion
        #region StateChangeList
        private ObservableCollection<StateChange> _stateChangeList = new ObservableCollection<StateChange>();

        public ObservableCollection<StateChange> stateChangeList
        {
            get => _stateChangeList;
            set => Set(ref _stateChangeList, value);
        }
        #endregion
        #region ParametersData
        public class ParametersData
        {
            public string ParameterName { get; set; }
            public string ParameterSubName { get; set; }
        }
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
        #region Date
        private string _DateStep;
        public string DateStep
        {
            get => _DateStep;
            set => Set(ref _DateStep, value);
        }
        #endregion
        #region LeftSwitchValue
        private int _LeftSwitchValue;
        public int LeftSwitchValue
        {
            get => _LeftSwitchValue;
            set => Set(ref _LeftSwitchValue, value);
        }
        #endregion
        #region LeftSwitchValue
        private int _RightSwitchValue;
        public int RightSwitchValue
        {
            get => _RightSwitchValue;
            set => Set(ref _RightSwitchValue, value);
        }
        #endregion
        #region Name of instruction
        private string _instructionNote = DateTime.Now.ToString("yy/MM/dd/HH/mm");
        public string instructionNote
        {
            get => _instructionNote;
            set => Set(ref _instructionNote, value);
        }
        #endregion
        #endregion
        public async void GetResponse()
        {
            while (true)
            {
                await Task.Delay(500);
                using var response = await httpClient.GetAsync("https://localhost:7133/api/ParametersMeasures/lv");
                string content = await response.Content.ReadAsStringAsync();
                ParametersMeasure[] collection = JsonSerializer.Deserialize<ParametersMeasure[]>(content);
                foreach (ParametersMeasure d in collection)
                {
                    d.Time = DateTime.Now;
                    ChartCollection[d.BlockId.Value-1].PushChartData(maxVal,d.Time,d.VoltageValue.Value,d.ActiveLoadPower.Value, d.ReactiveLoadPower.Value, d.FullLoadPower.Value, d.MicrogridFrequency.Value, d.CurrentValue.Value, d.LoadPowerFactor.Value);
                }
                _customAxis.MaxLimit = DateTime.Now.AddSeconds(-3).Ticks;
                _customAxis.MinLimit = DateTime.Now.AddSeconds(-8).Ticks;
                _customAxis.CustomSeparators = GetSeparators();
            }
        }
    }
}
