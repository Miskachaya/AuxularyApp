using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using OperatorApplication.Models.DataModels.InstructionModels;

namespace OperatorApplication.ViewModels
{
    public class PanelTemplateSelector : DataTemplateSelector
    {
        public DataTemplate StateTemplate { get; set; }
        public DataTemplate ParameterTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item == null) return StateTemplate;
            
            //MessageBox.Show("селектор работает");
            //dynamic panelItem = item; // Используем dynamic для доступа к Type
            //string panelType = panelItem.Type;

            return item switch
            {
                StateChange => StateTemplate,
                ParametersChange => ParameterTemplate,
                _=> ParameterTemplate
                //_ => base.SelectTemplate(item, container)
            };
        }
    }
}
