using OxyPlot.Series;
using OxyPlot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OxyPlot;
using System.Collections.ObjectModel;
using AuxularyApp.Models.DataModels;
namespace AuxularyApp.Models
{
    internal class Chart
    {
        ObservableCollection<ParametersMeasure> Data = new ObservableCollection<ParametersMeasure>();

        public Chart()
        {
            PlotModel Chart;
            Chart = new PlotModel { Title = "Example 1" };
        }
        
    }
}
