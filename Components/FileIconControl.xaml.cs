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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TuwenDayinDian.Components
{
    /// <summary>
    /// FileIconControl.xaml 的交互逻辑
    /// </summary>
    public partial class FileIconControl : UserControl
    {
        public FileIconControl()
        {
            InitializeComponent();
        }

        public string FileName
        {
            get { return (string)GetValue(FileNameProperty); }
            set { SetValue(FileNameProperty, value); }
        }

        public static readonly DependencyProperty FileNameProperty =
            DependencyProperty.Register("FileName", typeof(string), typeof(FileIconControl), new PropertyMetadata(OnFileNameChanged));

        private static void OnFileNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (FileIconControl)d;
            var fileName = (string)e.NewValue;

            string extension = System.IO. Path.GetExtension(fileName)?.ToLower();

            var iconText = extension.Trim('.').ToUpper();

            if (iconText.Length >3)
            {
                iconText = iconText.Substring(0, 3);    

            }

            control.textBlock.Text = iconText;


            switch (extension)
            {
                case ".txt":
                    control.border.Background = Brushes.LightGray;
                    break;
                case ".doc":
                case ".docx":
                    control.border.Background = Brushes.DeepSkyBlue;
                    break;
                case ".xls":
                case ".xlsx":
                    control.border.Background = Brushes.Green;
                    break;
                case ".ppt":
                case ".pptx":
                    control.border.Background = Brushes.DarkOrange;
                    break;
                case ".pdf":
                    control.border.Background = Brushes.DeepPink;
                    break;
                case ".rtf":
                    control.border.Background = Brushes.Brown;
                    break;
                case ".csv":
                    control.border.Background = Brushes.Yellow;
                    break;
                case ".xml":
                    control.border.Background = Brushes.LightPink;
                    break;
                default:
                    control.border.Background = Brushes.LightGray; // Default color for unknown file types
                    break;
            }

        }
    }
}
