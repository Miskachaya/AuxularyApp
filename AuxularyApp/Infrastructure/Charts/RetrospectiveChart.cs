//using LiveChartsCore;
//using LiveChartsCore.Defaults;
//using LiveChartsCore.SkiaSharpView;
//using System;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace AuxularyApp.Infrastructure.Charts
//{
//    internal class RetrospectiveChart
//    {
//        private List<DateTimePoint> MTPIN1_A3 { get; set; } = [];
//        private List<DateTimePoint> MTPIN1_A4 { get; set; } = [];
//        private List<DateTimePoint> MTPIN1_A5 { get; set; } = [];
//        private List<DateTimePoint> MTPIN1_A6 { get; set; } = [];
//        private List<DateTimePoint> IPOS1_P2 { get; set; } = [];
//        private List<DateTimePoint> IPOS1_P1 { get; set; } = [];
//        public ObservableCollection<ISeries> Series { get; set; } = [];

//        public RetrospectiveChart()
//        {
//            Series = [
//                new LineSeries<DateTimePoint>
//                {
//                    Name = "МТПИН1-А3",
//                    IsVisible = true,
//                    Values = MTPIN1_A3,
//                    Fill = null,
//                    GeometryFill = null,
//                    GeometryStroke = null,
//                    LineSmoothness = 0
//                },
//                new LineSeries<DateTimePoint>
//                {
//                    Name = "МТПИН1-А4",
//                    IsVisible = true,
//                    Values = MTPIN1_A4,
//                    Fill = null,
//                    GeometryFill = null,
//                    GeometryStroke = null,
//                    LineSmoothness = 0
//                },
//                new LineSeries<DateTimePoint>
//                {
//                    Name = "МТПИН1-А5",
//                    IsVisible = true,
//                    Values = MTPIN1_A5,
//                    Fill = null,
//                    GeometryFill = null,
//                    GeometryStroke = null,
//                    LineSmoothness = 0
//                },
//                new LineSeries<DateTimePoint>
//                {
//                    Name = "МТПИН1-А6",
//                    IsVisible = true,
//                    Values = MTPIN1_A6,
//                    Fill = null,
//                    GeometryFill = null,
//                    GeometryStroke = null,
//                    LineSmoothness = 0
//                },
//                new LineSeries<DateTimePoint>
//                {
//                    Name = "ИПОС1-Р1",
//                    IsVisible = true,
//                    Values = MTPIN1_A6,
//                    Fill = null,
//                    GeometryFill = null,
//                    GeometryStroke = null,
//                    LineSmoothness = 0
//                },
//                new LineSeries<DateTimePoint>
//                {
//                    Name = "ИПОС1-Р2",
//                    IsVisible = true,
//                    Values = IPOS1_P1,
//                    Fill = null,
//                    GeometryFill = null,
//                    GeometryStroke = null,
//                    LineSmoothness = 0
//                },
//                new LineSeries<DateTimePoint>
//                {
//                    Name = "Коэф. мощности нагрузки",
//                    IsVisible = true,
//                    Values = IPOS1_P2,
//                    Fill = null,
//                    GeometryFill = null,
//                    GeometryStroke = null,
//                    LineSmoothness = 0
//                }
//            ];
//        }
//        public void PushChartData(int maxVal, DateTime time, double voltageValue, double activeLPvalue, double reactiveLPvalue, double fullLPvalue, double microgridFr, double currentValue, double lPF)
//        {
//            Voltagevalues.Add(new DateTimePoint(time, voltageValue));
//            ActiveLPvalues.Add(new DateTimePoint(time, activeLPvalue));
//            ReactiveLPvalues.Add(new DateTimePoint(time, reactiveLPvalue));
//            FullLPvalues.Add(new DateTimePoint(time, fullLPvalue));
//            MicrogridFr.Add(new DateTimePoint(time, microgridFr));
//            CurrentValue.Add(new DateTimePoint(time, currentValue));
//            LPF.Add(new DateTimePoint(time, lPF));

//            if (Voltagevalues.Count > maxVal) Voltagevalues.RemoveAt(0);
//            if (ActiveLPvalues.Count > maxVal) ActiveLPvalues.RemoveAt(0);
//            if (ReactiveLPvalues.Count > maxVal) ReactiveLPvalues.RemoveAt(0);
//            if (FullLPvalues.Count > maxVal) FullLPvalues.RemoveAt(0);
//            if (MicrogridFr.Count > maxVal) MicrogridFr.RemoveAt(0);
//            if (CurrentValue.Count > maxVal) CurrentValue.RemoveAt(0);
//            if (LPF.Count > maxVal) LPF.RemoveAt(0);
//        }
//        public void PushChartData(int maxVal, DateTime time, string parameter, double value)
//        {
//            switch (parameter)
//            {
//                case :
//                    MTPIN1_A3.Add(new DateTimePoint(time, value));
//                    MTPIN1_A4.Add(new DateTimePoint(time, value));
//                    MTPIN1_A5.Add(new DateTimePoint(time, value));
//                    MTPIN1_A6.Add(new DateTimePoint(time, value));
//                    IPOS1_P1.Add(new DateTimePoint(time, value));
//                    IPOS1_P2.Add(new DateTimePoint(time, value));
//                break;
//            }
            

//            //if (Voltagevalues.Count > maxVal) Voltagevalues.RemoveAt(0);
//            //if (ActiveLPvalues.Count > maxVal) ActiveLPvalues.RemoveAt(0);
//            //if (ReactiveLPvalues.Count > maxVal) ReactiveLPvalues.RemoveAt(0);
//            //if (FullLPvalues.Count > maxVal) FullLPvalues.RemoveAt(0);
//            //if (MicrogridFr.Count > maxVal) MicrogridFr.RemoveAt(0);
//            //if (CurrentValue.Count > maxVal) CurrentValue.RemoveAt(0);
//            //if (LPF.Count > maxVal) LPF.RemoveAt(0);
//        }
//    }
//}
