using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuxularyApp.ViewModels.Base;
using AuxularyApp.Models;
using OxyPlot;
using OxyPlot.Series;
namespace AuxularyApp.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        

        private Chart MyModel { get; private set; }
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
