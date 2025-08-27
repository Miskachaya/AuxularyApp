using OperatorApplication.Models.DataModels.InstructionModels;
using OperatorApplication.ViewModels.Base;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Collections.ObjectModel;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Threading;
namespace OperatorApplication.ViewModels
{
    public partial class MainWindowViewModel : ViewModel
    {
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
        Dispatcher dispatcher = Application.Current.Dispatcher;
        public ObservableCollection<Instruction> InstructionCollection { get; } = [];
        public ObservableCollection<object> InstructionStepsCollection { get; } =[];
        public MainWindowViewModel()
        {
            Task.Run(async () => { await Recieve(); });
        }

        public async Task Recieve()
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            var connection = await factory.CreateConnectionAsync();
            var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(queue: "instructionQueue",
                                        durable: false,
                                        exclusive: false,
                                        autoDelete: false,
                                        arguments: null);
            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.ReceivedAsync += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                //MessageBox.Show(message);
                dispatcher.Invoke(() =>
                {
                    Instruction? instruction = JsonSerializer.Deserialize<Instruction>(message);
                    InstructionCollection?.Add(instruction);
                    PlannedInstructionCollection?.Add(instruction);
                });
                return Task.CompletedTask;
            };
            await channel.BasicConsumeAsync("instructionQueue", autoAck: true, consumer: consumer);
        }
        private string _Text=$"";
        public string Text
        {
            get => _Text;
            set => Set(ref _Text, value);    
        }

    }
}
