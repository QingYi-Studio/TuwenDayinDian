using MySql.Data.MySqlClient;
using Mysqlx.Crud;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TuwenDayinDian.Components;
using TuwenDayinDian.Models;
using TuwenDayinDian.windows;
using static TuwenDayinDian.windows.AddBillInfo;


namespace TuwenDayinDian
{
    
   
    public class ItemToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return Brushes.Black; // 默认颜色

            string item = value.ToString();
            switch (item)
            {
                case "未结账":
                    return Brushes.Red;
                case "已结账":
                    return Brushes.Green;
                case "已开票":
                    return Brushes.Blue;
                default:
                    return Brushes.Black; // 备选默认颜色
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// BillPage.xaml 的交互逻辑
    /// </summary>
    public partial class BillPage : Page
    {
       
        List<PrintpicesModel> PrintpicesList = new List<PrintpicesModel>();
        int BillId = 1;
        public BillPage()
        {
            InitializeComponent();
            
            SecondMenuList.MenuList.SelectionChanged += SecondMenuList_SelectionChanged;          

            MySqlDataReader reader = CRUD.getIns().QueryAllRecords("printprices");
            while (reader.Read())
            {
                PrintpicesModel printpices = new PrintpicesModel();
                printpices.JobName = reader.GetString("job_name");
                printpices.JobType = reader.GetString("job_type");
                printpices.L1 = reader.GetDecimal("L1");
                printpices.L2 = reader.GetDecimal("L2");
                printpices.L3 = reader.GetDecimal("L3");
                printpices.L4 = reader.GetDecimal("L4");
                printpices.L5 = reader.GetDecimal("L5");
                printpices.L6 = reader.GetDecimal("L6");
                printpices.L7 = reader.GetDecimal("L7");
                // 添加其他需要的字段
                PrintpicesList.Add(printpices);
            }
            
        }
     


        private void Open_AddCustomerWin(object sender, RoutedEventArgs e)
        {
            var customerInformation = new CustomerInformation("");
            // selectionWindow.Owner = this.Parent as Window;
            customerInformation.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            customerInformation.CustomerNameSaved += OnCustomerNameSaved;
            customerInformation.ShowDialog();
            
        }
        private void Open_AddBillIfonWin(object sender, RoutedEventArgs e)
        {
            Models.MenuItem selectedData = (Models.MenuItem)SecondMenuList.MenuList.SelectedItem;
            if (selectedData == null)
            {
                System.Windows.MessageBox.Show("请选择客户");
                return;
            }
            var addBillInfo = new AddBillInfo(selectedData.MenuText, PrintpicesList);            
            // selectionWindow.Owner = this.Parent as Window;
            addBillInfo.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            addBillInfo.AddBillInfoSaved += OnAddBillInfoSaved;
            addBillInfo.ShowDialog();


        }

        private void OnAddBillInfoSaved(object sender, Bill e)
        {
           
            e.SerialNumber = BillId;
            Bill.Bills.Add(e);
            UpdateTotalPrice();
            BillId++;
        }

        private void OnCustomerNameSaved(object sender, string content)
        {
           
            var Menulist = (List<Models.MenuItem>)SecondMenuList.MenuList.ItemsSource;
            Menulist.Add(new Models.MenuItem() { MenuText =content });            
            SecondMenuList.MenuList.Items.Refresh();
  
        }


        




        private void SecondMenuList_SelectionChanged(object sender, RoutedEventArgs e)
        {
            //BillListView.SelectionChanged -= ComboBox_SelectionChanged;
            if (isSelectionChangedEnabled)
            {
                isSelectionChangedEnabled = false;
            }
           
            Bill.Bills.Clear();
            
            //获取客户名称传入Page
            Models.MenuItem selectedData = (Models.MenuItem)SecondMenuList.MenuList.SelectedItem;
            if (selectedData == null)
            {
                return;
            }
            BillId = 1;
           
            
            string sql= "select * from bills JOIN customers ON bills.customer_id = customers.id JOIN prints ON bills.print_id = prints.id where customers.name =@name ";
            MySqlDataReader reader = CRUD.getIns().QuerySentence(sql, selectedData.MenuText);
           // var myArray = FindResource("OrderStatus") as string[];

           //var color= FindName("comboBox1") as string;
           // Console.WriteLine("color" + color);
           

            while (reader.Read())
            {
               // Console.WriteLine("reader.Read(", reader.GetValue(2));
                Bill.Bills.Add(new Bill
                {
                    Id = reader.GetInt32("id"),
                    SerialNumber = BillId,
                    Date = reader.IsDBNull(0) ? "" : reader.GetDateTime("generation_date").ToString("yyyyMMdd"),
                    FileName = reader.IsDBNull(0) ? "" : reader.GetString("bill_name"),
                    FileDetail = reader.IsDBNull(0) ? "" : reader.GetString("material"),
                    Copies = reader.GetInt32("copies"),
                    Technique = reader.IsDBNull(0) ? "" : reader.GetString("technique"),
                    Amount = reader.GetDouble("amount"),
                    OrderBy = reader.IsDBNull(0) ? "" : reader.GetString("order_by"),
                    Order_Status = reader.IsDBNull(0) ? "" : reader.GetString("order_status"),                    
                    Operator = reader.IsDBNull(0) ? "" : reader.GetString("operator"),

                });
                BillId ++;
            }
            BillListView.ItemsSource = Models.Bill.Bills;
            BillListView.Items.Refresh();
            UpdateTotalPrice();
        }

        //字体颜色
        public class MyViewModel : INotifyPropertyChanged
        {
            private Brush _textColor;

            
            public Brush TextColor
            {
                get { return _textColor; }
                set
                {
                    _textColor = value;
                    OnPropertyChanged(nameof(TextColor));
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;

            protected virtual void OnPropertyChanged(string propertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }



        private bool isSelectionChangedEnabled = false;

     

        private void MenuList_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                // 获取正在编辑的项目
                var editedItem = e.Row.Item as Bill;

                if (editedItem != null)
                {
                    Console.WriteLine("MenuList_CellEditEnding id" + editedItem.Id);
                }
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (isSelectionChangedEnabled==true)
            {

                if (e.AddedItems.Count > 0)
                {
                    var comboBox = sender as System.Windows.Controls.ComboBox;
                    if (comboBox != null)
                    {
                        //var selectedCategory = comboBox.SelectedItem as string;
                        var dataGridRow = FindParent<DataGridRow>(comboBox);
                        if (dataGridRow != null)
                        {
                            var menuItem = dataGridRow.Item as Bill;
                            if (menuItem != null)
                            {
                                // 更新数据库中的相应数据
                              //  Console.WriteLine("已更改" + menuItem.Order_Status);
                                // string query = "UPDATE bills SET order_status ="+ menuItem.Order_Status+" where id=" + menuItem.Id;
                                string query = "UPDATE bills SET order_status = '" + menuItem.Order_Status + "' where id=" + menuItem.Id;
                                CRUD.getIns().UpdateRecord(query);
                                UpdateTotalPrice();
                                //MessageBox.Show(menuItem.Order_Status);
                                // 提示用户
                                //  MessageBox.Show($"Updated Category to {selectedCategory} for MenuItem {menuItem.MenuText}");
                            }
                        }
                    }
                }
            }
        }

        // Helper method to find parent of a specific type
        private static T FindParent<T>(DependencyObject child) where T : DependencyObject
        {
            DependencyObject parentObject = VisualTreeHelper.GetParent(child);
            if (parentObject == null) return null;

            T parent = parentObject as T;
            if (parent != null)
            {
                return parent;
            }
            else
            {
                return FindParent<T>(parentObject);
            }
        }

        private void MenuList_CurrentCellChanged(object sender, EventArgs e)
        {
            if (!isSelectionChangedEnabled)
            {
                isSelectionChangedEnabled = true;
                             
            }
          // Console.WriteLine("MenuList_CurrentCellChanged");
        }

        private void DataGrid_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            var dataGrid = sender as System.Windows.Controls.DataGrid;
            if (dataGrid.SelectedItem == null)
            {
                e.Handled = true;
            }
        }

        private void UpdateMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (BillListView.SelectedItem is Bill selectedItem)
            {
                // 更新操作逻辑，例如弹出一个对话框修改数据
                // Console.WriteLine($"更新: {selectedItem.Id}");


                var addBillInfo = new AddBillInfo(selectedItem.Id);
                addBillInfo.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                addBillInfo.ShowDialog();

                // 以下是尝试使用本地缓存传入的一套代码，当然，我还是觉得从数据库获取更加准确
                // 同时建议加一个每分钟/每五分钟从数据库获取一次当前页面数据
                // 这个的实例化没写，没注释我不知道你的这些变量是什么
                /*
                var addBillInfo = new AddBillInfo(selectedItem.Id, selectedItem.Date, selectedItem.Amount, selectedItem.FileDetail, selectedItem.OrderBy);
                addBillInfo.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                addBillInfo.ShowDialog();
                */
            }
        }

        private void DeleteMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (BillListView.SelectedItem is Bill selectedItem)
            {
                //  Console.WriteLine("删除:"+ selectedItem.FileName);
                // 删除操作逻辑，例如从数据源中移除数据

                CRUD.getIns().DeleteByIdRecord("bills", selectedItem.Id);
                Bill.Bills.Remove(selectedItem);
                UpdateTotalPrice();
                isSelectionChangedEnabled = false;
               // BillListView.Items.Refresh();
               // Console.WriteLine($"删除: {selectedItem.Id}");
            }
        }
        public void UpdateTotalPrice()
        {
            double totalPrice = 0;
           foreach (var item in Bill.Bills)
            {
                
                if (item.Order_Status!="已结账")
                {
                    //Console.WriteLine(item.Order_Status);
                    // Console.WriteLine(item.Amount);
                    totalPrice = totalPrice + item.Amount;
                }               
            }

            tbkZhongJia.Text = totalPrice.ToString();
        }


    }
}
