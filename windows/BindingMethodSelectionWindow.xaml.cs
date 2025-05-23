using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TuwenDayinDian
{
    /// <summary>
    /// BindingMethodSelectionWindow.xaml 的交互逻辑
    /// </summary>
    public partial class BindingMethodSelectionWindow : Window
    {
        public BindingMethodSelectionWindow()
        {
            InitializeComponent();
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            // 这将关闭窗口，并将 DialogResult 设置为 true
            this.DialogResult = true;
        }
    }
}
