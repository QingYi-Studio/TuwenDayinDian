using MySql.Data.MySqlClient;
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
using TuwenDayinDian.Models;

namespace TuwenDayinDian.windows
{
    /// <summary>
    /// CustomerInformation.xaml 的交互逻辑
    /// </summary>
    public partial class CustomerInformation : Window
    {
        string CustomersName = "";
        string id = "";
        public CustomerInformation(string customersName1)
        {
            
            InitializeComponent();
            if (customersName1 != "")
            {
                CustomersName = customersName1;
                CustomerName.Text = customersName1;
                MySqlDataReader reader = CRUD.getIns().QueryAccordingtoKeyValye("customers", customersName1);
                while (reader.Read())
                {
                    if (reader["id"] != null)
                    {
                        id = reader["id"].ToString();
                    }

                    if (reader["address"]!=null)
                    {
                        CustomerAddress.Text= reader["address"].ToString();
                    }
                    if (reader["contact_info"] != null)
                    {
                        CustomerPhone.Text = reader["contact_info"].ToString();
                    }
                    if (reader["company_type"] != null)
                    {
                        CompantType.Text = reader["company_type"].ToString();
                    }
                    if (reader["level"] != null)
                    {
                        Level.Text = reader["level"].ToString();
                    }
                    if (reader["we_name"] != null)
                    {
                        WeName.Text = reader["we_name"].ToString();
                    }
                    if (reader["contact_person"] != null)
                    {
                        ContactPersonString = reader["contact_person"].ToString();
                    }             

                }


            }
            Console.WriteLine("CustomersName1"+CustomersName);
        }

        string ContactPersonString = "";

        private void close_setPrintParam(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public event EventHandler<string> CustomerNameSaved;

        private void Open_AddContactPersonWin(object sender, RoutedEventArgs e)
        {
            var  contactPerson= new ContactPerson(ContactPersonString);
            // selectionWindow.Owner = this.Parent as Window;
            contactPerson.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            contactPerson.ContactPersonSaved += OnContactPersonSaved;
            contactPerson.ShowDialog();        
           
        }
       

        private void OnContactPersonSaved(object sender, List<string> content)
        {
            ContactPersonString = "";
            foreach (string item in content)
            {
               // Console.WriteLine("OnContactPersonSaved" + item);
                ContactPersonString = ContactPersonString+ item +",";
            }
            ContactPersonString= ContactPersonString.Remove( ContactPersonString.Length-1);

           // Console.WriteLine("ContactPersonString" + ContactPersonString);
            // 处理子窗体传递过来的内容
            // 可以将内容显示在父级窗口中的某个控件中，或者保存内容到数据库中等操作
        }




        private void Customer_Save(object sender, RoutedEventArgs e)
        {
            //Console.WriteLine("CustomersName2" + CustomersName);

            if (CustomerName.Text == "")
            {

                MessageBox.Show("请填写有效信息");                
            }

            if (CustomerName.Text != ""&& CustomersName=="")
            {
                Console.WriteLine("1");
                string sqlVule = "'" + CustomerName.Text + "','" + CustomerAddress.Text + "','" + CustomerPhone.Text + "','" + ContactPersonString + "','" + CompantType.Text + "','" + Level.Text + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" +  WeName.Text+"'";
                string sql = "INSERT INTO customers (name, address,contact_info,contact_person,company_type, level,registration_date,we_name) VALUES (" + sqlVule + ");";
                CRUD.getIns().InsertRecord(sql);
                CustomerNameSaved?.Invoke(this, CustomerName.Text);
                this.Close();
            }
            if(CustomersName!="")
            {
              
                CRUD.getIns().UpdateCustomersRecord(id,CustomerName.Text, CustomerPhone.Text, CustomerAddress.Text, CompantType.Text, ContactPersonString, Level.Text, WeName.Text);
                CustomerNameSaved?.Invoke(this, CustomerName.Text);
                this.Close();
            }
            
        }

       
    }
}
