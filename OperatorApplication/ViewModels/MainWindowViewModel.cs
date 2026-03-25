using Microsoft.Extensions.DependencyInjection;
using OperatorApplication.Models.DataModels.InstructionModels;
using OperatorApplication.Services;
using OperatorApplication.ViewModels.Base;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Collections.ObjectModel;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Threading;
using static Confluent.Kafka.ConfigPropertyNames;
namespace OperatorApplication.ViewModels
{
    public partial class MainWindowViewModel : ViewModel
    {
        public IServiceProvider _serviceProvider; public IRabbitMQService _rabbitMQService; public IKafkaService _kafkaService; public INatsService _natsService;
        public List<int> AddedPanelsValue { get; } = new List<int>();
        private Instruction _SelectedInstruction;
        public Instruction SelectedInstruction
        {
            get => _SelectedInstruction;
            set {
                    AddedPanelsValue.Clear();
                    InstructionStepsCollection.Clear(); 
                    Set(ref _SelectedInstruction, value);
                    foreach (var instructionStep in _SelectedInstruction.InstructionSteps)
                    {
                        if (instructionStep.ParametersChanges.Count == 0)
                        {
                            AddedPanelsValue.Add(0);
                            instructionStep.StateChanges.FirstOrDefault().StepNumber = AddedPanelsValue.Count;
                            instructionStep.StateChanges.FirstOrDefault().Block=instructionStep.Block;
                            instructionStep.StateChanges.FirstOrDefault().Time=instructionStep.Time;
                            InstructionStepsCollection.Add(instructionStep.StateChanges.FirstOrDefault());   
                        }
                        else
                        {
                            AddedPanelsValue.Add(0);
                            instructionStep.ParametersChanges.FirstOrDefault().StepNumber = AddedPanelsValue.Count;
                            instructionStep.ParametersChanges.FirstOrDefault().Block = instructionStep.Block;
                            instructionStep.ParametersChanges.FirstOrDefault().Time = instructionStep.Time;
                        InstructionStepsCollection.Add(instructionStep.ParametersChanges.FirstOrDefault());
                        }
                    }
            }
        }
        public ObservableCollection<Instruction> PlannedInstructionCollection { get; } = [];
        public ObservableCollection<Instruction> CompleteInstructionCollection { get; } = [];
        //Dispatcher dispatcher = Application.Current.Dispatcher;
        public ObservableCollection<Instruction> InstructionCollection { get; } = [];
        public ObservableCollection<object> InstructionStepsCollection { get; } =[];
        public MainWindowViewModel(IServiceProvider serviseP) : base(serviseP)
        {
            _serviceProvider = serviseP;
            //_rabbitMQService = _serviceProvider.GetRequiredService<IRabbitMQService>();
            //_kafkaService = _serviceProvider.GetRequiredService<IKafkaService>();
            //_kafkaService.SetCollectionUpdater(AddInstructionToCollections);
            //_rabbitMQService.SetCollectionUpdater(AddInstructionToCollections);
            //_natsService=_serviceProvider.GetRequiredService<INatsService>();
            
            //_kafkaService.SetCollectionUpdater(AddInstructionToCollections);
            Task.Run(async()=>Recieve());
           // MessageBox.Show(InstructionCollection.Count.ToString());

        }

        public async Task Recieve()
        {
            try
            {
                if (_serviceProvider == null) MessageBox.Show("serviceProviderisnull");
                //await _rabbitMQService.DataReceivedEventArgs(InstructionCollection, PlannedInstructionCollection);
                await _kafkaService.ExecuteAsync( new CancellationToken());
                //await _natsService.ReceiveAsync();
            }
            catch (Exception ex) { MessageBox.Show("При попытке вызова метода startconsumingasync возникла ошибка: "+ex.Message); }
        }

        private void AddInstructionToCollections(Instruction instruction)
        {
            // Убеждаемся, что работаем в UI потоке
            if (Application.Current.Dispatcher.CheckAccess())
            {
                InstructionCollection.Add(instruction);
                PlannedInstructionCollection.Add(instruction);
            }
            else
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    InstructionCollection.Add(instruction);
                    PlannedInstructionCollection.Add(instruction);
                });
            }
        }

        private string _Text=$"";
        public string Text
        {
            get => _Text;
            set => Set(ref _Text, value);    
        }

    }
}
