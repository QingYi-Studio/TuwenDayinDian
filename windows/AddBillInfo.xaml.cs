using MySql.Data.MySqlClient;
using Panuon.UI.Silver;
using PdfEdit.Pdf.Content.Objects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.TextFormatting;
using System.Windows.Shapes;
using TuwenDayinDian.Models;
using static Mysqlx.Notice.Warning.Types;

using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;
using RadioButton = System.Windows.Controls.RadioButton;

namespace TuwenDayinDian.windows
{


    /// <summary>
    /// AddBillInfo.xaml 的交互逻辑
    /// </summary>
    public partial class AddBillInfo : System.Windows.Window
    {



        public static class Calculate
        {           
            public static string level { get; set; }
            public static double pageCount { get; set; }
            public static int copies { get; set; }
            public static double sizeUnitPrice { get; set; }
            public static double colorUnitPrice { get; set; }
            public static double pageUnitPrice { get; set; }
            public static double duplexUnitPrice { get; set; }
            public static double coveUnitPrice { get; set; }
            public static double bindingUnitPrice { get; set; }
            public static double tecprintUnitPrice { get; set; }
            public static double penhuibuUnitPrice { get; set; }
            public static double pvcUnitPrice { get; set; }
            public static double acrylicUnitPrice { get; set; }
            public static double materialUnitPrice { get; set; }
            public static double tecAdUnitPrice { get; set; }

        }
        

        private string  CustomerName;       
        private string Size_radiotext;
        private string Color_radiotext;
        private string Duplex_radiotext;
        private string Admaterial_radiotext;
        private List<PrintpicesModel> PrintpicesList = new List<PrintpicesModel>();
       // private AddBillInfoPricing addBillInfoPricing = new AddBillInfoPricing();

        //private void UnitPrice_Loaded(object sender, RoutedEventArgs e)
        //{
        //    placeholderTextBlock.Text = addBillInfoPricing.PageUnitPrice.ToString();
        //}



        // public string Data { get; set; }
        public AddBillInfo(string name,List<PrintpicesModel> printpicesList)
        {

            InitializeComponent();

            
            CustomerName = name;
            PrintpicesList=printpicesList;
    
            string sql = "select IFNULL(contact_person, '') as contact_person, IFNULL(we_name, '') AS we_name, IFNULL(level, '') AS level from customers  where name = @name";
            MySqlDataReader reader = CRUD.getIns().QuerySentence(sql, name);
            //List<string> contactPersons = new List<string>();
            string name1 = "";

            while (reader.Read())
            {
                if (reader.GetString("contact_person") == "")
                {
                    name1 = reader.GetString("we_name");

                }
                else
                {
                    name1 = reader.GetString("contact_person");
                }

            }
            string[] contactPerson = name1.Split(new char[] { ',' });
            comboBox_OrderBy.ItemsSource = contactPerson;
            //string level=reader.GetString("level");
            //Calculate.level = "L" + level;


            List<string> page = new List<string>();
            List<string> cove = new List<string>();
            List<string> binding = new List<string>();
            List<string> penhuibu = new List<string>();
            List<string> acrylic = new List<string>();
            List<string> PVC = new List<string>();   

            foreach (var pices in printpicesList)
            {
                Console.WriteLine("pices -- "+pices.L7);

                if (pices.JobType == "page")
                {
                    page.Add(pices.JobName);

                }
                if (pices.JobType == "cove")
                {
                    cove.Add(pices.JobName);

                }
                if (pices.JobType == "binding")
                {
                    binding.Add(pices.JobName);

                }
                if (pices.JobType == "penhuibu")
                {
                    penhuibu.Add(pices.JobName);

                }
                if (pices.JobType == "acrylic")
                {
                    acrylic.Add(pices.JobName);

                }
                if (pices.JobType == "PVC")
                {
                    PVC.Add(pices.JobName);
                }      

            }   

            comboBox_page.ItemsSource = page;
            comboBox_cover.ItemsSource = cove;
            comboBox_binding.ItemsSource = binding;
            comboBox_penhuibu.ItemsSource = penhuibu;
            comboBox_acrylic.ItemsSource = acrylic;
            comboBox_PVC.ItemsSource = PVC;
            Calculate.copies = 1;


            sizeA4.IsChecked = true;
            colorBlack.IsChecked = true;
            duplex.IsChecked = true;
            comboBox_printType.SelectedIndex = 0;
        }

        //public AddBillInfo1(string name, List<PrintpicesModel> printpicesList, Bill selectedItem)
        //{

        //    InitializeComponent();


        //    CustomerName = name;
        //    PrintpicesList = printpicesList;

        //    string sql = "select IFNULL(contact_person, '') as contact_person, IFNULL(we_name, '') AS we_name, IFNULL(level, '') AS level from customers  where name = @name";
        //    MySqlDataReader reader = CRUD.getIns().QuerySentence(sql, name);
        //    //List<string> contactPersons = new List<string>();
        //    string name1 = "";

        //    while (reader.Read())
        //    {
        //        if (reader.GetString("contact_person") == "")
        //        {
        //            name1 = reader.GetString("we_name");

        //        }
        //        else
        //        {
        //            name1 = reader.GetString("contact_person");
        //        }

        //    }
        //    string[] contactPerson = name1.Split(new char[] { ',' });
        //    comboBox_OrderBy.ItemsSource = contactPerson;
        //    string level = reader.GetString("level");
        //    Calculate.level = "L" + level;


        //    List<string> page = new List<string>();
        //    List<string> cove = new List<string>();
        //    List<string> binding = new List<string>();
        //    List<string> penhuibu = new List<string>();
        //    List<string> acrylic = new List<string>();
        //    List<string> PVC = new List<string>();

        //    foreach (var pices in printpicesList)
        //    {
        //        Console.WriteLine("pices -- " + pices.L7);

        //        if (pices.JobType == "page")
        //        {
        //            page.Add(pices.JobName);

        //        }
        //        if (pices.JobType == "cove")
        //        {
        //            cove.Add(pices.JobName);

        //        }
        //        if (pices.JobType == "binding")
        //        {
        //            binding.Add(pices.JobName);

        //        }
        //        if (pices.JobType == "penhuibu")
        //        {
        //            penhuibu.Add(pices.JobName);

        //        }
        //        if (pices.JobType == "acrylic")
        //        {
        //            acrylic.Add(pices.JobName);

        //        }
        //        if (pices.JobType == "PVC")
        //        {
        //            PVC.Add(pices.JobName);
        //        }

        //    }

        //    comboBox_page.ItemsSource = page;
        //    comboBox_cover.ItemsSource = cove;
        //    comboBox_binding.ItemsSource = binding;
        //    comboBox_penhuibu.ItemsSource = penhuibu;
        //    comboBox_acrylic.ItemsSource = acrylic;
        //    comboBox_PVC.ItemsSource = PVC;
        //    Calculate.copies = 1;


        //    sizeA4.IsChecked = true;
        //    colorBlack.IsChecked = true;
        //    duplex.IsChecked = true;
        //    comboBox_printType.SelectedIndex = 0;
        //}


        public event EventHandler<Bill> AddBillInfoSaved;

        private void close_thisWindon(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


    

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
            {
            Console.WriteLine("void ComboBox_SelectionChanged"+ comboBox_printType.SelectedIndex);

                if (comboBox_printType.SelectedIndex == 0)
                {
                Print_StackPanel.Visibility = Visibility.Visible;
                Ad_StackPanel.Visibility = Visibility.Collapsed;
                Other_StackPanel.Visibility = Visibility.Collapsed;
                
                //Console.WriteLine("stackPanel.Visibility "+ stackPanel.Visibility);
                }
                if (comboBox_printType.SelectedIndex == 1)
                {
                Ad_StackPanel.Visibility = Visibility.Visible;
                Other_StackPanel.Visibility = Visibility.Collapsed;
                Print_StackPanel.Visibility = Visibility.Collapsed;
                }
                if (comboBox_printType.SelectedIndex == 2)
            {
                Ad_StackPanel.Visibility = Visibility.Collapsed;
                Other_StackPanel.Visibility = Visibility.Visible;
                Print_StackPanel.Visibility = Visibility.Collapsed;
            }
            
        }


 


        class Dat_input_box
        {
            //限制非法字符输入，只允许输入数字和小数点
            public static void TextBox_PreviewTextInput(object sender,
                TextCompositionEventArgs e)
            {
                var textBox = sender as System.Windows.Controls.TextBox;
                //这是对中文值的判断
                if (textBox == null) return;
                //if (Regex.IsMatch(textBox.Text, @"[\u4e00-\u9fbb]+$"))
                //{
                //   textBox.Text = "";
                //   e.Handled = true;
                //  return;
                //}
                //匹配只能输入一个小数点的浮点数
                Regex numbeRegex = new Regex("^[-]?\\d+[.]?\\d*$");
                Regex zeroRegex = new Regex("^[0]+[0-9]+$");
                e.Handled =
                        !numbeRegex.IsMatch(
                            textBox.Text.Insert(
                                textBox.SelectionStart, e.Text))
                                || zeroRegex.IsMatch(
                            textBox.Text.Insert(
                                textBox.SelectionStart, e.Text));
                textBox.Text = textBox.Text.Trim();
            }

            /// <summary>
            /// 键盘输入值事件(可以小键盘上的数字键和英文输入状态下的句点键盘)
            /// </summary>
            /// <param name="e"></param>
            public static void TextBox_OnPreviewKeyDown(KeyEventArgs e)
            {
                if ((e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 &&
                     e.KeyboardDevice.Modifiers != ModifierKeys.Shift)
                    ||
                    (e.Key >= Key.D0 && e.Key <= Key.D9 &&
                     e.KeyboardDevice.Modifiers != ModifierKeys.Shift) ||
                    e.Key == Key.Back || e.Key == Key.Left || e.Key == Key.Right ||
                    e.Key == Key.Enter || e.Key == Key.Decimal ||
                    e.Key == Key.OemPeriod)
                {
                    if (e.KeyboardDevice.Modifiers != ModifierKeys.None)
                    {
                        e.Handled = false;
                    }
                }
                else
                {
                    e.Handled = true;
                }
            }

            /// <summary>
            ///  禁止粘贴
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            public static void TextBox_Pasting(object sender, DataObjectPastingEventArgs e)
            {
                if (e.DataObject.GetDataPresent(typeof(String)))
                {
                    String text = (String)e.DataObject.GetData(typeof(String));
                    if (!isNumberic(text))
                    { e.CancelCommand(); }
                }
                else { e.CancelCommand(); }
            }

            public static bool isNumberic(string _string)
            {
                if (string.IsNullOrEmpty(_string))
                    return false;
                foreach (char c in _string)
                {
                    if (!char.IsDigit(c))
                        //if(c<'0' c="">'9')//最好的方法,在下面测试数据中再加一个0，然后这种方法效率会搞10毫秒左右
                        return false;
                }
                return true;
            }
        }

        private void Volt_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            Dat_input_box.TextBox_Pasting(sender, e);
           // Console.WriteLine("Volt_Pasting");
        }

        private void Volt_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            Dat_input_box.TextBox_OnPreviewKeyDown(e);
           // Console.WriteLine("Volt_PreviewKeyDown");
        }

        private void Volt_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Dat_input_box.TextBox_PreviewTextInput(sender, e);
            Calculate.copies = Convert.ToInt32(IntegerTextBox.Text);
            UpdateTotalPrice();


        }

        private void IncreaseButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(IntegerTextBox.Text, out int number))
            {
                number++;
                IntegerTextBox.Text = number.ToString();
                Calculate.copies=number;
                UpdateTotalPrice();

            }
        }

        private void DecreaseButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(IntegerTextBox.Text, out int number))
            {
                number--;
                IntegerTextBox.Text = number.ToString();
                Calculate.copies = number;
                UpdateTotalPrice();
            }
        }

        private void TextBox_PrintTextChanged(object sender, TextChangedEventArgs e)
        {
            placeholderTextBlock.Visibility = string.IsNullOrEmpty(unitPriceTextbox.Text) ? Visibility.Visible : Visibility.Collapsed;
            UpdateTotalPrice();
            //Console.WriteLine(unitPriceTextbox.Text.ToString());
        }

        private void TextBox_PagecountTextChanged(object sender, TextChangedEventArgs e)
        {
            placePagecountTextBlock.Visibility = string.IsNullOrEmpty(textBox_PagesNum.Text) ? Visibility.Visible : Visibility.Collapsed;
            Calculate.pageCount = int.Parse(textBox_PagesNum.Text);
            UpdateTotalPrice();
            //Console.WriteLine(unitPriceTextbox.Text.ToString());
        }

        private void TextBox_AdTextChanged(object sender, TextChangedEventArgs e)
        {
            placeAdTextBlock.Visibility = string.IsNullOrEmpty(textBox_adUnitPrice.Text) ? Visibility.Visible : Visibility.Collapsed;
            UpdateTotalPrice();
            //Console.WriteLine(unitPriceTextbox.Text.ToString());
        }

        private void TextBox_OtherTextChanged(object sender, TextChangedEventArgs e)
        {
            placeAdTextBlock.Visibility = string.IsNullOrEmpty(textBox_OtherUnitPrice.Text) ? Visibility.Visible : Visibility.Collapsed;
            UpdateTotalPrice();
            //Console.WriteLine(unitPriceTextbox.Text.ToString());
        }

        private void RadioButton_CheckedSize(object sender, RoutedEventArgs e)
        {
           
            RadioButton radioButton = (RadioButton)sender;
            Size_radiotext = radioButton.Content.ToString();
          
            Calculate.sizeUnitPrice = GetUnitPriceList("size", Size_radiotext, Calculate.level);
            UpdateTotalPrice();          
                
        }

        private void RadioButton_CheckedColor(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = (RadioButton)sender;
            Color_radiotext = radioButton.Content.ToString();
            
            Calculate.colorUnitPrice = GetUnitPriceList("color", Color_radiotext, Calculate.level);
            UpdateTotalPrice();

        }
        private void RadioButton_CheckedDuplex(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = (RadioButton)sender;
            Duplex_radiotext = radioButton.Content.ToString();            
            Calculate.duplexUnitPrice = GetUnitPriceList("duplex", Duplex_radiotext, Calculate.level);
            UpdateTotalPrice();

        }

        private void RadioButton_CheckedMaterial(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = (RadioButton)sender;
            Admaterial_radiotext = radioButton.Content.ToString();
            Calculate.materialUnitPrice = GetUnitPriceList("ad_material", Admaterial_radiotext, Calculate.level);
            UpdateTotalPrice();
          
           
          

        }

        public void  UpdateTotalPrice()
        {

            //Console.WriteLine("Calculate.pageUnitPrice" + Calculate.pageUnitPrice);
            //Console.WriteLine("Calculate.sizeUnitPrice" + Calculate.sizeUnitPrice);
            //Console.WriteLine("Calculate.colorUnitPrice" + Calculate.colorUnitPrice);
            //Console.WriteLine("Calculate.duplexUnitPrice" + Calculate.duplexUnitPrice);
            //Console.WriteLine("Calculate.coveUnitPrice" + Calculate.coveUnitPrice);
            //Console.WriteLine("Calculate.bindingUnitPrice" + Calculate.bindingUnitPrice);
            //Console.WriteLine("Calculate.tecprintUnitPrice" + Calculate.tecprintUnitPrice);

            //Console.WriteLine("Calculate.penhuibuUnitPrice" + Calculate.penhuibuUnitPrice);
            //Console.WriteLine("Calculate.pvcUnitPrice" + Calculate.pvcUnitPrice);
            //Console.WriteLine("Calculate.acrylicUnitPrice" + Calculate.acrylicUnitPrice);
            //Console.WriteLine("Calculate.materialUnitPrice" + Calculate.materialUnitPrice);
            //Console.WriteLine("Calculate.tecAdUnitPrice" + Calculate.tecAdUnitPrice);



            if (comboBox_printType.SelectedIndex == 0)
            {
                double TotalPrice = 0;
                if (unitPriceTextbox.Text.Length > 0)
                {
                    TotalPrice = double.Parse(unitPriceTextbox.Text);
                    //Console.WriteLine("unitPriceTextbox.Text" + unitPriceTextbox.Text);
                }
                else
                {
                    TotalPrice = (Calculate.pageUnitPrice + Calculate.sizeUnitPrice + Calculate.duplexUnitPrice  + Calculate.colorUnitPrice )* Calculate.pageCount + Calculate.bindingUnitPrice + Calculate.tecprintUnitPrice + Calculate.coveUnitPrice;

                }


                placeholderTextBlock.Text = TotalPrice.ToString();
                Amount_text.Text = (TotalPrice * Calculate.copies).ToString();
            }
            if (comboBox_printType.SelectedIndex == 1)
            {

                Console.WriteLine("Admaterial_radiotext" + Admaterial_radiotext);
                double TotalPrice = 0;
                double layoutFee = 0.00;           


                if (textBox_adUnitPrice.Text.Length > 0)
                {
                    TotalPrice = double.Parse(textBox_adUnitPrice.Text);
                    //Console.WriteLine("unitPriceTextbox.Text" + unitPriceTextbox.Text);
                }
                else
                {
                    if (textBox_adverWidth.Text.Length == 0|| textBox_adverHeight.Text.Length == 0)
                    {
                        System.Windows.MessageBox.Show("请填写有效尺寸");
                        return;
                    }
                    if (Admaterial_radiotext == "条幅")
                    {

                        TotalPrice = double.Parse(textBox_adverWidth.Text) / 100 * Calculate.materialUnitPrice;

                    }
                    else
                    {
                        double Width = double.Parse(textBox_adverWidth.Text) / 100;
                        double Height = double.Parse(textBox_adverHeight.Text) / 100;
                        double square = Width * Height;
                        TotalPrice = square * (Calculate.materialUnitPrice + Calculate.tecAdUnitPrice + Calculate.pvcUnitPrice + Calculate.acrylicUnitPrice + Calculate.penhuibuUnitPrice);

                    }
                 

                }
                placeAdTextBlock.Text = TotalPrice.ToString();                               
                if (textBox_LayoutFee.Text.Length> 0)
                {
                    layoutFee = double.Parse(textBox_LayoutFee.Text);                    
                }

                Amount_text.Text = (TotalPrice * Calculate.copies + layoutFee).ToString();
            }
            if (comboBox_printType.SelectedIndex == 2)
            {
                double TotalPrice = 0;
                if (textBox_OtherUnitPrice.Text.Length > 0)
                {
                    TotalPrice = double.Parse(textBox_OtherUnitPrice.Text);
                    //Console.WriteLine("unitPriceTextbox.Text" + unitPriceTextbox.Text);
                }
                else
                {
                    System.Windows.MessageBox.Show("请填写单价");
                    return;

                }
               // placeOtherTextBlock.Text = TotalPrice.ToString();                
                Amount_text.Text = (TotalPrice * Calculate.copies ).ToString();
            }

        }


        private void SaveBills_Click(object sender, RoutedEventArgs e)
        {
            if (comboBox_printType.SelectedIndex == 0)
            {
                string pagesNum = "1";
                // Console.WriteLine("textBox_PagesNum" + textBox_PagesNum.Text);
                if (textBox_PagesNum.Text != "")
                {
                    pagesNum = textBox_PagesNum.Text;
                }
                string technique = comboBox_binding.Text;
                var selectedListbox = Print_Technique.SelectedItems.Cast<string>();
                foreach (var item in selectedListbox)
                {
                    technique = technique + "|" + item;
                }

                string material = Size_radiotext + "内页" + pagesNum + "页、" + comboBox_page.Text + "、封面" + comboBox_cover.Text;
                if (textBox_printName.Text == "" && textBox_adverName.Text == "" && textBox_otherName.Text == "")
                {
                    System.Windows.MessageBox.Show("请填写有效信息");
                    return;
                }
                double numDouble = double.Parse(Amount_text.Text);
                Bill billList = new Bill()
                {
                    Date = DateTime.Now.ToString("yyyy-MM-dd"),
                    FileName = textBox_printName.Text,
                    FileDetail = Size_radiotext + "、" + Color_radiotext + "、" + Duplex_radiotext + "、" + material + pagesNum + "页",
                    Copies = Convert.ToInt32(IntegerTextBox.Text),
                    Technique = technique,
                    Amount = numDouble,
                    OrderBy = comboBox_OrderBy.Text,
                    Order_Status = "未结账",//reader.GetBoolean("order_status").ToString(),
                    Operator = GlobalData.Name,
                };


                // List<Models.Bill> billList = new List<Models.Bill>();
                AddBillInfoSaved?.Invoke(this, billList);
                if (textBox_printName.Text != "")
                {
                    int copies = Convert.ToInt32(IntegerTextBox.Text);
                    int InsertBills = CRUD.getIns().InsertPrints(Size_radiotext, copies, Convert.ToInt32(pagesNum), material, technique);
                    CRUD.getIns().InsertBills(CustomerName, InsertBills, billList);
                    this.Close();
                }

            }
            if (comboBox_printType.SelectedIndex == 1)
            {
                string layoutFee = "0";
                // Console.WriteLine("textBox_PagesNum" + textBox_PagesNum.Text);
                if (textBox_LayoutFee.Text != "")
                {
                    layoutFee = textBox_PagesNum.Text;
                }
                string technique = "";
                var selectedListbox = Ad_Technique.SelectedItems.Cast<string>();
                foreach (var item in selectedListbox)
                {
                    technique = technique + "|" + item;
                }
                string materialSize = textBox_adverWidth.Text + "x" + textBox_adverHeight.Text+"cm";
                string material = materialSize + Admaterial_radiotext+comboBox_acrylic.Text + comboBox_penhuibu.Text + comboBox_PVC.Text;
                if (textBox_adverName.Text == "")
                {
                    System.Windows.MessageBox.Show("请填写账目名称");
                    return;
                }
                double numDouble = double.Parse(Amount_text.Text);
                Bill billList = new Bill()
                {
                    Date = DateTime.Now.ToString("yyyy-MM-dd"),
                    FileName = textBox_adverName.Text,
                    FileDetail = material,
                    Copies = Convert.ToInt32(IntegerTextBox.Text),
                    Technique = technique,
                    Amount = numDouble,
                    OrderBy = comboBox_OrderBy.Text,
                    Order_Status = "未结账",//reader.GetBoolean("order_status").ToString(),
                    Operator = GlobalData.Name,
                };


                // List<Models.Bill> billList = new List<Models.Bill>();
                AddBillInfoSaved?.Invoke(this, billList);
                if ( textBox_adverName.Text != "")
                {
                    int copies = Convert.ToInt32(IntegerTextBox.Text);
                    int InsertBills = CRUD.getIns().InsertPrints(materialSize, copies, 1, material, technique);
                    CRUD.getIns().InsertBills(CustomerName, InsertBills, billList);
                    this.Close();
                }
            }
            if (comboBox_printType.SelectedIndex == 2)
            {
             
               
                if (textBox_otherName.Text == "")
                {
                    System.Windows.MessageBox.Show("请填写有效信息");
                    return;
                }
                double numDouble = double.Parse(Amount_text.Text);
                Bill billList = new Bill()
                {
                    Date = DateTime.Now.ToString("yyyy-MM-dd"),
                    FileName = textBox_otherName.Text,
                    FileDetail = textBox_otherName.Text,
                    Copies = Convert.ToInt32(IntegerTextBox.Text),
                    Technique = "",
                    Amount = numDouble,
                    OrderBy = comboBox_OrderBy.Text,
                    Order_Status = "未结账",//reader.GetBoolean("order_status").ToString(),
                    Operator = GlobalData.Name,
                };


                // List<Models.Bill> billList = new List<Models.Bill>();
                AddBillInfoSaved?.Invoke(this, billList);
                if (textBox_printName.Text != "" || textBox_adverName.Text != "" || textBox_otherName.Text != "")
                {
                    int copies = Convert.ToInt32(IntegerTextBox.Text);
                    int InsertBills = CRUD.getIns().InsertPrints("", copies, 0 , "", "");
                    CRUD.getIns().InsertBills(CustomerName, InsertBills, billList);
                    this.Close();
                }
            }
               

        }

        private void PageComboBox_Changed(object sender, SelectionChangedEventArgs e)
        {
            double unitPricePage = GetUnitPriceList("page", comboBox_page.SelectedValue.ToString(), Calculate.level);
            //double unitPricePage = GetUnitPriceList("size", "A4","L1");
            Calculate.pageUnitPrice = unitPricePage;
            UpdateTotalPrice();
          //  Console.WriteLine("PageComboBox_Changed" + comboBox_page.SelectedValue + unitPricePage);
        }

        private void CoveComboBox_Changed(object sender, SelectionChangedEventArgs e)
        {
            double unitPricePage = GetUnitPriceList("cove", comboBox_cover.SelectedValue.ToString(), Calculate.level);
            Calculate.coveUnitPrice = unitPricePage;
            UpdateTotalPrice();

        }

        private void BindingComboBox_Changed(object sender, SelectionChangedEventArgs e)
        {

            double unitPricePage = GetUnitPriceList("binding", comboBox_binding.SelectedValue.ToString(), Calculate.level);
            Calculate.bindingUnitPrice = unitPricePage;
            UpdateTotalPrice();
        }

        private void PenhuibuComboBox_Changed(object sender, SelectionChangedEventArgs e)
        {

            double unitPricePage = GetUnitPriceList("penhuibu", comboBox_penhuibu.SelectedValue.ToString(), Calculate.level);
            Calculate.penhuibuUnitPrice = unitPricePage;
            UpdateTotalPrice();
        }
        private void AcrylicComboBox_Changed(object sender, SelectionChangedEventArgs e)
        {

            double unitPricePage = GetUnitPriceList("acrylic", comboBox_acrylic.SelectedValue.ToString(), Calculate.level);
            Calculate.acrylicUnitPrice = unitPricePage;
            UpdateTotalPrice();
        }

        private void PVCComboBox_Changed(object sender, SelectionChangedEventArgs e)
        {

            double unitPricePage = GetUnitPriceList("PVC", comboBox_PVC.SelectedValue.ToString(), Calculate.level);
            Calculate.pvcUnitPrice = unitPricePage;
            UpdateTotalPrice();
        }



        public double GetUnitPriceList(string jobType, string jobName, string propertyName)
        {
          
            PrintpicesModel item=new PrintpicesModel();
            Console.WriteLine("GetUnitPriceList"+ jobType+"--"+ jobName + "--"+ propertyName);
            if (jobName == "")
            {
                  item = PrintpicesList.First(p => p.JobType == jobType);
                if (item == null)
                {
                    return 0.00;
                }
            }
            else
            {
                item = PrintpicesList.FirstOrDefault(p => p.JobType == jobType && p.JobName == jobName);
               // Console.WriteLine("GetUnitPriceList item -  " + item);
                //if (item == null)
                //{
                //    return null;
                //}

            }
            PropertyInfo propertyInfo = item.GetType().GetProperty(propertyName);
            if (propertyInfo == null)
            {
                throw new ArgumentException($"Property '{propertyName}' not found on type '{item.GetType().Name}'");
            }
            double d = Convert.ToDouble(propertyInfo.GetValue(item));
            Console.WriteLine("d"+ d);
            return d;
        }




        private void Print_Listbox(object sender, SelectionChangedEventArgs e)
        {
           

            var selectedListbox = Print_Technique.SelectedItems.Cast<string>();
            double unitPricePage = 0;
            foreach (var item in selectedListbox)
            {
               double unitPricePage1 = GetUnitPriceList("tec_print", item, Calculate.level);

               unitPricePage = unitPricePage + unitPricePage1;
                //technique = technique + "|" + item;
               
            }
            Calculate.tecprintUnitPrice = unitPricePage;        
            UpdateTotalPrice();
        }

        private void Ad_Listbox(object sender, SelectionChangedEventArgs e)
        {


            var selectedListbox = Ad_Technique.SelectedItems.Cast<string>();
            double unitPricePage = 0;
            foreach (var item in selectedListbox)
            {
                double unitPricePage1 = GetUnitPriceList("tec_ad", item, Calculate.level);

                unitPricePage = unitPricePage + unitPricePage1;
                // technique = technique + "|" + item;
               // Console.WriteLine("Ad_Technique" + item);
            }
            Calculate.tecAdUnitPrice = unitPricePage;
          //  Console.WriteLine("AdunitPricePage" + unitPricePage);
            UpdateTotalPrice();
        }
    }
}
