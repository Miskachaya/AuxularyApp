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
using AuxularyApp.Models.DataModels.Base;
using System.Collections.Specialized;
using OxyPlot.Axes;
namespace AuxularyApp.Models
{
    internal class Chart
    {
        ObservableCollection<Data> DataCollection = new ObservableCollection<Data>();
        PlotModel chart;
        LineSeries ls = new LineSeries();
        public Chart()
        {
            chart.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom });
            chart.Axes.Add(new LinearAxis { Position = AxisPosition.Left });

            chart.Series.Add(ls); 

            DataCollection.CollectionChanged += Data_CollectionChanged;
        }
        void Data_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            //switch (e.Action)
            //{
            //    case NotifyCollectionChangedAction.Add: // если добавление
            //        if (e.NewItems?[0] is Data newPerson)
            //            //chart.
            //        break;
            //}
        }
    }
}
