using AuxularyApp.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace AuxularyApp.Models
{
    public class ButtonOfState : INotifyPropertyChanged
    {
        string title { get; set; }
        string side { get; set; }
        bool stateChange;
        public Brush _Color;

        public Brush Color
        {
            get => _Color;
            set
            {
                _Color = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ButtonOfState(string title, string side, Brush color)
        {
            this.title = title;
            this.side = side;
            this.stateChange = false;
            this.Color = color;
        }
        public string getTitle()
        {
            return title;
        }
        public void setTitle(string title)
        {
            this.title = title;
        }

        public bool getState()
        {
            return stateChange;
        }
        public void setState(bool stateChange)
        {
            this.stateChange = stateChange;
        }

        public string getSide()
        {
            return side;
        }
        public void setSide(string side)
        {
            this.side = side;
        }
        public void setStateChange()
        {
            if (stateChange)
            {
                stateChange = !stateChange;
                Color = Brushes.Green;
            }
            else
            {
                stateChange = !stateChange;
                Color = Brushes.Red;
            }
        }
    }
    class ButtonOfStateChanger
    {
        IEquipmentService EquipmentService { get; set; }
        ObservableCollection<ButtonOfState> ButtonList = new();
        HttpClient client = new();
        public ButtonOfStateChanger(IEquipmentService equipmentService, ObservableCollection<ButtonOfState> List, HttpClient httpClient)
        {
            this.EquipmentService = equipmentService;
            this.client = httpClient;
            this.ButtonList = List;
        }
        public void SetNewValue(string title, string side)
        {
            for (int i=0;i<ButtonList.Count;i++) {
                var button = ButtonList[i];
                if ((title == button.getTitle()) && (side == button.getSide()))
                {
                    button.setStateChange();
                    string value="";
                        switch (button.getTitle())
                        {
                            case ("7"):
                                switch (button.getSide(),button.getState())
                                {
                                    case ("R", false):
                                        if ((ButtonList[i+1].getSide()=="L") && (ButtonList[i + 1].getState() == false))
                                        {
                                            value = "0";
                                        } else if ((ButtonList[i + 1].getSide() == "L") && (ButtonList[i + 1].getState() == true))
                                        {
                                            value = "3";
                                        }
                                        break;
                                    case ("R", true):
                                        if ((ButtonList[i + 1].getSide() == "L") && (ButtonList[i + 1].getState() == false))
                                        {
                                            value = "48";
                                        }
                                        else if ((ButtonList[i + 1].getSide() == "L") && (ButtonList[i + 1].getState() == true))
                                        {
                                            value = "51";
                                        }
                                        break;
                                    case ("L", false):
                                        if ((ButtonList[i - 1].getSide() == "R") && (ButtonList[i - 1].getState() == false))
                                        {
                                            value = "0";
                                        }
                                        else if ((ButtonList[i - 1].getSide() == "R") && (ButtonList[i - 1].getState() == true))
                                        {
                                            value = "48";
                                        }
                                        break;
                                    case ("L", true):
                                        if ((ButtonList[i - 1].getSide() == "R") && (ButtonList[i - 1].getState() == false))
                                        {
                                            value = "3";
                                        }
                                        else if ((ButtonList[i - 1].getSide() == "R") && (ButtonList[i - 1].getState() == true))
                                        {
                                            value = "51";
                                        }
                                        break;
                                }
                                break;
                            case ("8"):
                                switch (button.getSide(), button.getState())
                                {
                                    case ("R", false):
                                        if ((ButtonList[i + 1].getSide() == "L") && (ButtonList[i + 1].getState() == false))
                                        {
                                            value = "0";
                                        }
                                        else if ((ButtonList[i + 1].getSide() == "L") && (ButtonList[i + 1].getState() == true))
                                        {
                                            value = "3";
                                        }
                                        break;
                                    case ("R", true):
                                        if ((ButtonList[i + 1].getSide() == "L") && (ButtonList[i + 1].getState() == false))
                                        {
                                            value = "48";
                                        }
                                        else if ((ButtonList[i + 1].getSide() == "L") && (ButtonList[i + 1].getState() == true))
                                        {
                                            value = "51";
                                        }
                                        break;
                                    case ("L", false):
                                        if ((ButtonList[i - 1].getSide() == "R") && (ButtonList[i - 1].getState() == false))
                                        {
                                            value = "0";
                                        }
                                        else if ((ButtonList[i - 1].getSide() == "R") && (ButtonList[i - 1].getState() == true))
                                        {
                                            value = "48";
                                        }
                                        break;
                                    case ("L", true):
                                        if ((ButtonList[i - 1].getSide() == "R") && (ButtonList[i - 1].getState() == false))
                                        {
                                            value = "3";
                                        }
                                        else if ((ButtonList[i - 1].getSide() == "R") && (ButtonList[i - 1].getState() == true))
                                        {
                                            value = "51";
                                        }
                                        break;
                                }
                                break;
                            case ("9"):
                                switch (button.getSide(), button.getState())
                                {
                                    case ("R", false):
                                        if ((ButtonList[i + 1].getSide() == "L") && (ButtonList[i + 1].getState() == false))
                                        {
                                            value = "0";
                                        }
                                        else if ((ButtonList[i + 1].getSide() == "L") && (ButtonList[i + 1].getState() == true))
                                        {
                                            value = "3";
                                        }
                                        break;
                                    case ("R", true):
                                        if ((ButtonList[i + 1].getSide() == "L") && (ButtonList[i + 1].getState() == false))
                                        {
                                            value = "48";
                                        }
                                        else if ((ButtonList[i + 1].getSide() == "L") && (ButtonList[i + 1].getState() == true))
                                        {
                                            value = "51";
                                        }
                                        break;
                                    case ("L", false):
                                        if ((ButtonList[i - 1].getSide() == "R") && (ButtonList[i - 1].getState() == false))
                                        {
                                            value = "0";
                                        }
                                        else if ((ButtonList[i - 1].getSide() == "R") && (ButtonList[i - 1].getState() == true))
                                        {
                                            value = "48";
                                        }
                                        break;
                                    case ("L", true):
                                        if ((ButtonList[i - 1].getSide() == "R") && (ButtonList[i - 1].getState() == false))
                                        {
                                            value = "3";
                                        }
                                        else if ((ButtonList[i - 1].getSide() == "R") && (ButtonList[i - 1].getState() == true))
                                        {
                                            value = "51";
                                        }
                                        break;
                                }
                                break;
                            case ("10"):
                                switch (button.getSide(), button.getState())
                                {
                                    case ("R", false):
                                        if ((ButtonList[i + 1].getSide() == "L") && (ButtonList[i + 1].getState() == false))
                                        {
                                            value = "72";
                                        }
                                        else if ((ButtonList[i + 1].getSide() == "L") && (ButtonList[i + 1].getState() == true))
                                        {
                                            value = "75";
                                        }
                                        break;
                                    case ("R", true):
                                        if ((ButtonList[i + 1].getSide() == "L") && (ButtonList[i + 1].getState() == false))
                                        {
                                            value = "120";
                                        }
                                        else if ((ButtonList[i + 1].getSide() == "L") && (ButtonList[i + 1].getState() == true))
                                        {
                                            value = "123";
                                        }
                                        break;
                                    case ("L", false):
                                        if ((ButtonList[i - 1].getSide() == "R") && (ButtonList[i - 1].getState() == false))
                                        {
                                            value = "72";
                                        }
                                        else if ((ButtonList[i - 1].getSide() == "R") && (ButtonList[i - 1].getState() == true))
                                        {
                                            value = "120";
                                        }
                                        break;
                                    case ("L", true):
                                        if ((ButtonList[i - 1].getSide() == "R") && (ButtonList[i - 1].getState() == false))
                                        {
                                            value = "75";
                                        }
                                        else if ((ButtonList[i - 1].getSide() == "R") && (ButtonList[i - 1].getState() == true))
                                        {
                                            value = "123";
                                        }
                                        break;
                                }
                                break;
                        }
                        try
                        {
                            HttpContent cont = new StringContent($"{title}-{value}");
                            var response = client.PostAsync($"https://localhost:7029/api/Equipment/{title}-{value}", null);
                            //var response = client.GetAsync($"https://localhost:7029/api/Equipment/{title}-{value}");
                        }
                        catch (Exception e)
                        {
                            button.setStateChange();
                        }
                        finally
                        {
                            CommandRequest command = new CommandRequest();
                            command.BlockId = Convert.ToByte(title);
                            ushort[] values = new UInt16[3];
                            for (int j = 0; i < values.Length; i++) 
                            {
                                values[i] = Convert.ToUInt16(values[i]);
                            }
                            command.Value = values;
                            EquipmentService.ExecuteCommandAsync(command);
                        }
                    

                }
            }
        }
    }
}
