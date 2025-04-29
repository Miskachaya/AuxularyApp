using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuxularyApp.ViewModels.Base;
using AuxularyApp.Models;
using OxyPlot;
using OxyPlot.Series;
using AuxularyApp.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Input;
using Microsoft.Extensions.DependencyInjection;
namespace AuxularyApp.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        private readonly IHttpService _httpService;
        private readonly System.Timers.Timer _timer;
        private string _data;

        public MainWindowViewModel(IHttpService httpService)
        {
            _httpService= httpService;
            _timer = new System.Timers.Timer(1000);
            _timer.Elapsed += OnTimerElapsed;
            _timer.Start();
        }
        public string Data
        {
            get => _data;
            set
            {
                _data= value;
                OnPropertyChanged();
            }
        }
        private async void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            string data = await _httpService.GetDataAsync();
            Data = data;
        }
        //private Chart MyModel { get; private set; }
        #region Properties
        #region WindowTitle
        private string oldTitle;
        public string newTitle
        {
            get => oldTitle; 
            set => Set(ref oldTitle,value);
        }
        #endregion

        #endregion
    }
}
