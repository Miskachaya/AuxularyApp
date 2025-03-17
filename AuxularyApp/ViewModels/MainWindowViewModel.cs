using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuxularyApp.ViewModels.Base;
namespace AuxularyApp.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
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
