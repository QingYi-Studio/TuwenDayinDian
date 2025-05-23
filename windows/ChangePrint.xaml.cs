using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using TuwenDayinDian.Helpers;
using TuwenDayinDian.Models;

namespace TuwenDayinDian
{
    /// <summary>
    /// PrintParam.xaml 的交互逻辑
    /// </summary>
    public partial class ChangePrint : Window
    {
        string OrderCoverPath = "";
        public ChangePrint(string[] printname)
        {
            InitializeComponent();
            changePrint_Black_Print.Text = printname[0];
            changePrint_InkColor_Print.Text = printname[1];
            changePrint_LaserColor_Print.Text = printname[2];
            changePrint_Cove_Print.Text = printname[3];
            changePrint_BlackBinding_Print.Text = printname[4];
            //OrderCoverPath= printname[5];
        }

        private void close_setChangePrint(object sender, RoutedEventArgs e)
        {
           this.Close();
        }

        public event EventHandler<string[]> UpdataPrintName;

        private void SavePrintName_Click(object sender, RoutedEventArgs e)
        {         

            string jsonFile = ".\\" + "printName.dat";
            string Black_Print = changePrint_Black_Print.SelectedValue== null ?  changePrint_Black_Print.Text:changePrint_Black_Print.SelectedValue.ToString();
            string InkColor_Print = changePrint_InkColor_Print.SelectedValue == null ? changePrint_InkColor_Print.Text : changePrint_InkColor_Print.SelectedValue.ToString();
            string LaserColor_Print = changePrint_LaserColor_Print.SelectedValue == null ? changePrint_LaserColor_Print.Text : changePrint_LaserColor_Print.SelectedValue.ToString();
            string Cove_Print = changePrint_Cove_Print.SelectedValue == null ? changePrint_Cove_Print.Text : changePrint_Cove_Print.SelectedValue.ToString();
            string BlackBinding_Print = changePrint_BlackBinding_Print.SelectedValue == null ? changePrint_BlackBinding_Print.Text : changePrint_BlackBinding_Print.SelectedValue.ToString();
         
            string printName = Black_Print + "," + InkColor_Print + "," + LaserColor_Print + "," + Cove_Print + "," + BlackBinding_Print+ "," + OrderCoverPath;           
            AfTextFile.Write(jsonFile, printName, AfTextFile.UTF8);


            string[] PrintName = new string[6];
            PrintName[0] = Black_Print;
            PrintName[1] = InkColor_Print;
            PrintName[2] = LaserColor_Print;
            PrintName[3] = Cove_Print;
            PrintName[4] = BlackBinding_Print;

            UpdataPrintName?.Invoke(this, PrintName);


            this.Close();
        }

    


    }
}
