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
using System.Windows.Media.TextFormatting;
using System.Windows.Shapes;

namespace TuwenDayinDian.windows
{
    /// <summary>
    /// ContactPerson.xaml 的交互逻辑
    /// </summary>
    public partial class ContactPerson : Window
    {
        public ContactPerson(string contactPerson)
        {
            InitializeComponent();
            if (contactPerson != null)
            {
                string[] contactPersons = contactPerson.Split(new char[] { ',' });
                int i=0;
                foreach (UIElement control in textBoxList.Children)
                {
                    if (control is TextBox)
                    {
                        if(i<contactPersons.Length)
                        {
                            TextBox textBox = control as TextBox;
                            textBox.Text = contactPersons[i];
                            i++;
                        }                        
                    }
                }

            }
        }


        public event EventHandler<List<string>> ContactPersonSaved;


        private void close_setPrintParam(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void GetAllValueStr(object sender, RoutedEventArgs e)
        {
            List<string> textList = new List<string>();
            
            foreach (UIElement control in textBoxList.Children)
            {
                if (control is TextBox)
                {
                    TextBox textBox = control as TextBox;
                    if (textBox.Text != "")
                    {
                        textList.Add(textBox.Text);
                       //Console.WriteLine("textBox.Text" + textBox.Text);
                    }
                    
                }
            }

            ContactPersonSaved?.Invoke(this, textList);
            // MessageBox.Show(this, sb.ToString());
            // return sb.ToString();
            this.Close();

        }
    }
}
