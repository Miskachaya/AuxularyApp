using OperatorApplication.Models.DataModels.InstructionModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace OperatorApplication.Models.DataModels.ViewComponentModels
{
    internal class InstructionStepComponent
    {
        public Grid GeneralGrid { get; set; }
        public TextBlock StepNumberTextBlock { get; set; } 
        public InstructionStepComponent() 
        {
            GeneralGrid = new Grid();
            GeneralGrid.ColumnDefinitions.Add(new ColumnDefinition());
            GeneralGrid.ColumnDefinitions.Add(new ColumnDefinition());
            GeneralGrid.ColumnDefinitions.Add(new ColumnDefinition());
            GeneralGrid.ColumnDefinitions.Add(new ColumnDefinition());
            GeneralGrid.ColumnDefinitions.Add(new ColumnDefinition());

            StepNumberTextBlock = new TextBlock();
            StepNumberTextBlock.Text = "Binding Children.Count, ElementName=StepGrid";

            GeneralGrid.Children.Add(StepNumberTextBlock);
        }
    }
}
