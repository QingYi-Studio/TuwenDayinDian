using MySql.Data.MySqlClient;
using Mysqlx.Crud;
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
using TuwenDayinDian.Models;
using TuwenDayinDian.windows;

namespace TuwenDayinDian.Components
{
    /// <summary>
    /// SecondMenuList.xaml 的交互逻辑
    /// </summary>    
    public partial class SecondMenuList : UserControl
    {
     
        public SecondMenuList()
        {
            InitializeComponent();
            List<Models.MenuItem> Menulist = new List<Models.MenuItem>();
            MySqlDataReader reader = CRUD.getIns().QueryAllRecords("customers");
            int i = 0;
            while (reader.Read())
            {
                Menulist.Add(new Models.MenuItem() { MenuText= reader.GetString("name") });
                //Console.WriteLine(reader.GetString("name"));
                i++;
                
            }              
            MenuList.ItemsSource = Menulist;

        }

        private void CustomerListView_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is ListView listView)
            {
                var item = VisualTreeHelper.HitTest(listView, e.GetPosition(listView))?.VisualHit;
                while (item != null && item != listView)
                {
                    if (item is ListViewItem listViewItem)
                    {
                        listViewItem.IsSelected = true;
                        break;
                    }
                    item = VisualTreeHelper.GetParent(item);
                }
            }
        }

        private void UpdateInfo_Click(object sender, RoutedEventArgs e)
        {
            if (MenuList.SelectedItem is Models.MenuItem selectedCustomer)
            {
             
                var customerInformation = new CustomerInformation(selectedCustomer.MenuText);
                // selectionWindow.Owner = this.Parent as Window;
                customerInformation.WindowStartupLocation = WindowStartupLocation.CenterScreen;
               customerInformation.CustomerNameSaved += OnCustomerNameSaved;
                customerInformation.ShowDialog();

               // MessageBox.Show($"更新信息：{selectedCustomer.MenuText}");
                // 在这里添加更新信息的逻辑
            }
        }

        private void DeleteCustomer_Click(object sender, RoutedEventArgs e)
        {
           
            if (MenuList.SelectedItem is Models.MenuItem selectedCustomer)
            {

                if (MessageBox.Show("确认删除客户，账单将一起删除，无法恢复", "提示：", MessageBoxButton.OKCancel) == MessageBoxResult.OK) 
                {
                    CRUD.getIns().DeleteByNameRecord("customers", selectedCustomer.MenuText);
                    var Menulist = (List<Models.MenuItem>)MenuList.ItemsSource;
                    Menulist.Remove(selectedCustomer);
                    MenuList.Items.Refresh();
                }
                
                // 在这里添加删除客户的逻辑
            }
        }
         

        private void OnCustomerNameSaved(object sender, string content)
        {

            List<Models.MenuItem> Menulist = new List<Models.MenuItem>();
            MySqlDataReader reader = CRUD.getIns().QueryAllRecords("customers");
            int i = 0;
            while (reader.Read())
            {
                Menulist.Add(new Models.MenuItem() { MenuText = reader.GetString("name") });
                //Console.WriteLine(reader.GetString("name"));
                i++;

            }
            MenuList.ItemsSource = Menulist;
        }

    }
}
