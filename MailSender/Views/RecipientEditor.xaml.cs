using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfMailSender.Views
{
    /// <summary>
    /// Логика взаимодействия для RecipientEditor.xaml
    /// </summary>
    public partial class RecipientEditor : UserControl
    {
        public RecipientEditor()
        {
            InitializeComponent();
        }
        private void OnDataValidationError(object? Sender, ValidationErrorEventArgs E)
        {
            //var control = (Control)E.OriginalSource;
            //if (E.Action == ValidationErrorEventAction.Added)
            //    control.ToolTip = E.Error.ErrorContent.ToString();
            //else
            //    control.ClearValue(ToolTipProperty);
        }
    }
}
