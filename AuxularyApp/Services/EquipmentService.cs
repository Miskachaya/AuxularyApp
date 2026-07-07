using AuxularyApp.Models;
using AuxularyApp.Models.DataModels.MicrogridModels;
using Microsoft.Data.SqlClient;
using Modbus.Device;
using Modbus.Utility;
using NATS.Client.Internals.SimpleJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;


namespace AuxularyApp.Services
{
    class EquipmentService : IEquipmentService
    {
        private readonly string portName = "COM5"; // или "COM3" для Windows
        int baudRate = 9600;
        Parity parity = Parity.None;
        int dataBits = 8;
        StopBits stopBits = StopBits.One;
        SerialPort serialPort;
        private readonly Queue<CommandRequest> _commandQueue = new();
        private readonly object _queueLock = new();
        private EquipmentStatus _status = new();
        IModbusSerialMaster master;
        int count = 0;
        ushort[] registers = new ushort[14];
        public EquipmentService()
        {
            serialPort = new SerialPort(portName, baudRate, parity, dataBits, stopBits);
            // Открываем порт
            //serialPort.Open();

            serialPort.DataReceived += (sender, e) =>
            {
                Console.WriteLine($"DEBUG: Data received");
            };
            // Создаём Modbus RTU мастер
            master = ModbusSerialMaster.CreateRtu(serialPort);
        }

        // Метод А - чтение данных с оборудования
        async void MethodA()
        {
            try
            {
                //serialPort.Open();
                for (int k = 1; k <= 6; k++)
                {
                    //string VoltageValue = "1110.0";
                    //string ActiveLoadPower = "1110.0";
                    //string ReactiveLoadPower = "1110.0";
                    //string FullLoadPower = "1110.0";
                    //string CurrentValue = "1110.0";
                    //string LoadPowerFactor = "1110.0";
                    //string MicrogridFrequency = "1110.0";

                    var VoltageValue = 1110.0;
                    var ActiveLoadPower = 1110.0;
                    var ReactiveLoadPower = 1110.0;
                    var FullLoadPower = 1110.0;
                    var CurrentValue = 1110.0;
                    var LoadPowerFactor = 1110.0;
                    var MicrogridFrequency =1110.0;

                    byte slaveId = (byte)k;
                    //using SerialPort serialPort = new SerialPort(portName, baudRate, parity, dataBits, stopBits);

                    try
                    {
                        //// Открываем порт
                        //if (serialPort.IsOpen)
                        //{
                        //    serialPort.Close();
                        //}
                        //serialPort.Open();

                        //// Создаём Modbus RTU мастер
                        //IModbusSerialMaster master = ModbusSerialMaster.CreateRtu(serialPort);

                        // Включаем отладочный вывод (аналог modbus_set_debug)
                        // В NModbus нет прямого аналога, но можно подписаться на события

                        // Устанавливаем таймауты (по умолчанию в .NET они уже установлены)
                        serialPort.ReadTimeout = 2050;
                        serialPort.WriteTimeout = 2050;
                        
                        // Читаем 2 регистра начиная с адреса 0 (как в оригинальном коде)
                        try {
                            Debug.WriteLine($"{count}");
                            
                            registers = master.ReadInputRegisters(slaveId, 0, 14);
                            count++;
                            for (int i = 0; i < registers.Length; i += 2)
                            {
                                if (i + 1 < registers.Length)
                                {
                                    double val = ModbusUtility.GetSingle(registers[i], registers[i + 1]);

                                    Debug.WriteLine($"Регистр{slaveId} номер параметра{i / 2}: {val}");
                                    
                                    switch (i / 2)
                                    {
                                        case 0:
                                            VoltageValue = val;//.ToString(System.Globalization.CultureInfo.GetCultureInfo("en-US"));
                                            break;
                                        case 1:
                                            CurrentValue = val;//.ToString(System.Globalization.CultureInfo.GetCultureInfo("en-US"));
                                            break;
                                        case 2:
                                            MicrogridFrequency = val;//.ToString(System.Globalization.CultureInfo.GetCultureInfo("en-US"));
                                            break;
                                        case 3:
                                            FullLoadPower = val;//.ToString(System.Globalization.CultureInfo.GetCultureInfo("en-US"));
                                            break;
                                        case 4:
                                            ActiveLoadPower = val;//.ToString(System.Globalization.CultureInfo.GetCultureInfo("en-US"));
                                            break;
                                        case 5:
                                            ReactiveLoadPower = val;//.ToString(System.Globalization.CultureInfo.GetCultureInfo("en-US"));
                                            break;
                                        case 6:
                                            LoadPowerFactor = val;//.ToString(System.Globalization.CultureInfo.GetCultureInfo("en-US"));
                                            break;
                                    }
                                    Thread.Sleep(100);
                                    
                                }
                                
                            }
                            var measure = new ParametersMeasure
                            {
                                BlockId = slaveId,
                                VoltageValue = (VoltageValue),
                                CurrentValue = (CurrentValue),
                                ActiveLoadPower = (ActiveLoadPower),
                                ReactiveLoadPower = (ReactiveLoadPower),
                                FullLoadPower = (FullLoadPower),
                                LoadPowerFactor = (LoadPowerFactor),
                                MicrogridFrequency = (MicrogridFrequency),
                                Time = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"))
                            };

                            // Сериализация в JSON
                            var jsonString = JsonSerializer.Serialize(measure);
                            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                            Debug.Print(jsonString);
                            using var client = new HttpClient();
                            var response = await client.PostAsync("http://84.237.17.49:5225/api/ParametersMeasures", content);
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show($"EquipmentService {e}");
                        }



                        
                       //string query = $"insert into ParametersMeasure (BlockID, VoltageValue, ActiveLoadPower, ReactiveLoadPower, FullLoadPower, CurrentValue, LoadPowerFactor, MicrogridFrequency, Time) values ({k},{VoltageValue}, {ActiveLoadPower}, {ReactiveLoadPower}, {FullLoadPower}, {CurrentValue}, {LoadPowerFactor}, {MicrogridFrequency}, '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}')";
                        //try
                        //{
                        //    string localDBConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\maksb\\source\\repos\\Miskachaya\\AuxularyApp\\AuxularyApp\\Common\\localDB.mdf;Integrated Security=True";
                        //    SqlConnection sqlConnection = new SqlConnection(localDBConnectionString);
                        //    sqlConnection.Open();
                        //    var command = new SqlCommand(query, sqlConnection);
                        //    //command.ExecuteNonQuery();
                        //    command.ExecuteScalar();
                        //    sqlConnection.Close();
                        //}
                        //catch (Exception e)
                        //{
                        //    Console.WriteLine(e.Message);
                        //    MessageBox.Show($"147 EquipmentService {e}");
                        //}
                        //finally 
                        //{
                        //    using (var connection = new SQLiteConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\maksb\\source\\repos\\Miskachaya\\AuxularyApp\\AuxularyApp\\Common\\localDB.mdf;Integrated Security=True"))
                        //    {
                        //        connection.Open();
                        //        if (connection != null)
                        //        {
                        //            Console.WriteLine("Connection");
                        //        }
                        //        else Console.WriteLine("No connection");
                        //        using (SQLiteCommand command = new SQLiteCommand(query, connection))
                        //        {
                        //            command.ExecuteNonQuery();
                        //            Console.WriteLine("response");
                        //        }
                        //    }
                        //}
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Ошибка: {ex.Message}");
                    }
                    // Имитация чтения данных с оборудования
                    //Console.WriteLine($"[{DateTime.Now:HH:mm:ss.fff}] Method A: Reading equipment data...");

                    // Здесь реальная логика работы с оборудованием
                    //Thread.Sleep(10); // Имитация работы

                    _status.LastMethodAExecution = DateTime.UtcNow;
                    _status.MethodAExecutionCount++;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading: {ex.Message}");
            }
        }
        private void MethodB(CommandRequest request)
        {

            try
            {
                //serialPort.Open();
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss.fff}] Method B: Executing command '{request.Value}'");
                ushort[] value = { 125 };
                byte numberOfBlock = 10;
                Console.WriteLine($"Запись значения {value} в регистр {numberOfBlock}...");
                //master.WriteMultipleRegisters((byte)request.BlockId, address, (ushort[])request.Value);
                if (request.BlockId == 7)
                {
                    ushort[] values= { 0 };
                    master.WriteMultipleRegisters(request.BlockId, 50, values);
                }
                else
                {
                    master.WriteMultipleRegisters(request.BlockId, 50, request.Value);
                }
                

                Console.WriteLine($"Успешно записано значение {value} в регистр {numberOfBlock}");
                // 7!!3(л1п0)0(л0п0)48(л0п1)51(л1п1)
                // 8!!3(л1п0)0(л0п0)48(л0п1)51(л1п1)
                // 9!!3(л1п0)0(л0п0)48(л0п1)51(л1п1)
                //10!!75(л1п0)72(л0п0)120(л0п1)123(л1п1)
                // Здесь реальная логика выполнения команды


                ///////Thread.Sleep(50); // Имитация работы команды
                _status.MethodBExecutionCount++;
                 //serialPort.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"212Equpment {ex}");

            }

        }
        // Основной цикл обработки (будет вызываться из фонового сервиса)
        public async void ProcessCycleAsync(CancellationToken cancellationToken = default)
        {
            try
            {
               
                while (!cancellationToken.IsCancellationRequested)
                {
                    serialPort.Open();
                    // Выполняем метод А
                    await Task.Run(MethodA);
                    //serialPort.Close();
                    // Проверяем и выполняем команды из очереди
                    CommandRequest? nextCommand = null;
                    lock (_queueLock)
                    { 
                        if (_commandQueue.Count > 0)
                        {
                            nextCommand = _commandQueue.Dequeue();
                            _status.QueueLength = _commandQueue.Count;
                        }
                    }

                    if (nextCommand != null)
                    {
                        try
                        {
                            MethodB(nextCommand);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Failed to execute command {nextCommand.Value}: {ex.Message}");
                            MessageBox.Show("250 EquipmentService");
                        }
                    }
                    serialPort.Close();
                    //Task.Delay(250, cancellationToken); // 4 раза в секунду
                }
               
            }
            catch (Exception ex) 
            {
                MessageBox.Show("259 EquipmentService");
            }

            
            //return Task.CompletedTask;
        }

        // Метод для добавления команды в очередь (вызывается из контроллера)
        public async Task<CommandResponse> ExecuteCommandAsync(CommandRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.ToString()))
            {
                return new CommandResponse
                {
                    Success = false,
                    Message = "Command cannot be empty"
                };
            }

            try
            {
                lock (_queueLock)
                {
                    _commandQueue.Enqueue(request);
                    _status.QueueLength = _commandQueue.Count;
                }

                Console.WriteLine($"[{DateTime.Now:HH:mm:ss.fff}] Command '{request.Value}' queued. Queue length: {_status.QueueLength}");

                return new CommandResponse
                {
                    Success = true,
                    Message = $"Command '{request.Value}' accepted and queued for execution"
                };
            }
            catch (Exception ex)
            {
                return new CommandResponse
                {
                    Success = false,
                    Message = $"Failed to queue command: {ex.Message}"
                };
            }
        }

        public EquipmentStatus GetEquipmentStatus()
        {
            return _status;
        }
    }
    public interface IEquipmentService
    {
        void ProcessCycleAsync(CancellationToken cancellationToken = default);
        Task<CommandResponse> ExecuteCommandAsync(CommandRequest request);
        EquipmentStatus GetEquipmentStatus();
    }
    public class EquipmentStatus
    {
        public bool IsRunning { get; set; }
        public DateTime LastMethodAExecution { get; set; }
        public int MethodAExecutionCount { get; set; }
        public int MethodBExecutionCount { get; set; }
        public int QueueLength { get; set; }
    }
}
