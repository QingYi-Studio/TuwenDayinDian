using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.WebSockets;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TuwenDayinDian.Helpers;



namespace TuwenDayinDian
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        TasksPage tasksPage;
        BillPage billPage;
       // WebSocket webSocket;
        RemovePrint removePrint;


        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
            MyLeftMenu.MenuList.SelectionChanged += MenuList_SelectionChanged;
            Closing += MainWindow_Closing;
            //  InitialseCurrentSetup();


        }

        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }

        //关闭主窗口时，关闭websocket连接

        string url = "https://www.xuanzhuanmumatuwen.com/websocketInsertingCoil?DeviceId=25107671";

        private async void SendHttpRequestAsync()
        {
            // 发送 GET 请求到指定 URL            
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);
                MessageBox.Show("线上接单已下线");

                // 检查返回的状态码
                if (!response.IsSuccessStatusCode)
                {
                    MessageBox.Show("请求失败，无法关闭程序。", "错误", MessageBoxButton.OK, MessageBoxImage.Question);
                }
            }
        }
        private async Task<bool> SendHttpRequestButton_Click()
        {
            HttpClient client = new HttpClient();
           
                HttpResponseMessage response = await client.GetAsync("http://www.xuanzhuanmumatuwen.com/websocketInsertingCoil?DeviceId=25107671");

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                   
                }
           
            
        }

        //左侧菜单监听
        private void MenuList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (MyLeftMenu.MenuList.SelectedIndex == 0)
            {
                if (tasksPage == null)
                {
                    tasksPage = new TasksPage();
                    ContentFrame.Navigate(tasksPage);
                }
                else
                {
                    ContentFrame.Navigate(tasksPage);
                }
            }

             if (MyLeftMenu.MenuList.SelectedIndex == 1)
            {
                if (billPage == null)
                {
                    billPage = new BillPage();
                    ContentFrame.Navigate(billPage);
                }
                else
                {
                    ContentFrame.Navigate(billPage);
                }
            }


            //if (MyLeftMenu.MenuList.SelectedIndex == 2)
            //{
            //    if (webSocket == null)
            //    {
            //        webSocket = new WebSocket();
            //        ContentFrame.Navigate(webSocket);
            //    }
            //    else
            //    {
            //        ContentFrame.Navigate(webSocket);
            //    }
            //}

            if (MyLeftMenu.MenuList.SelectedIndex ==2)
            {
                if (removePrint == null)
                {
                    removePrint = new RemovePrint();
                    ContentFrame.Navigate(removePrint);
                }
                else
                {
                    ContentFrame.Navigate(removePrint);
                }
            }

        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (tasksPage == null)
            {
                tasksPage = new TasksPage();
                ContentFrame.Navigate(tasksPage);
            }
            else
            {
                ContentFrame.Navigate(tasksPage);
            }

            MyLeftMenu.MenuList.SelectedIndex = 0;
            // this.DataContext
        }

        private void MyLeftMenu_Loaded(object sender, RoutedEventArgs e)
        {

        }


        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            SendHttpRequestAsync();
            /*
            MessageBoxResult result = MessageBox.Show("确定要退出程序吗？", "提示", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                
                Application.Current.Shutdown();
            }
            else
            {
                e.Cancel = true;
            }
            */
            Application.Current.Shutdown();
        }

     






        //PrinterSettings CurrentPrintSettings = new PrinterSettings();
        //PrintDocument pd = new PrintDocument();
        //PrintSettings objPrinterSetup = new PrintSettings();
        //PrinterSetup CurrentSetup = new PrinterSetup();
        //PrinterSetup DefaultSetup = new PrinterSetup();
        //BinaryFormatter formatter = new BinaryFormatter();
        //bool nuInhibit = false;
        //byte[] Comparator1;
        //byte[] Comparator2;
        //byte[] DevModeArray;



        ////-------------------------------Button event handlers------------------------------------


        ////-------------------------------Combo box event handlers------------------------------------

        ////-------------------------------Radio Button event handlers------------------------------------


        ////-------------------------------Platform Invokes-----------------------------------------

        //[DllImport("winspool.Drv", EntryPoint = "DocumentPropertiesW", SetLastError = true,
        //ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        //private static extern int DocumentProperties(IntPtr hwnd, IntPtr hPrinter,
        //[MarshalAs(UnmanagedType.LPWStr)] string pDeviceNameg,
        //IntPtr pDevModeOutput, IntPtr pDevModeInput, int fMode);

        //[DllImport("kernel32.dll", ExactSpelling = true)]
        //public static extern IntPtr GlobalFree(IntPtr handle);

        //[DllImport("kernel32.dll", ExactSpelling = true)]
        //public static extern IntPtr GlobalLock(IntPtr handle);

        //[DllImport("kernel32.dll", ExactSpelling = true)]
        //public static extern IntPtr GlobalUnlock(IntPtr handle);

        ////-------------------------------Methods------------------------------------------
        //private void InitialseCurrentSetup()//Clears the printer setup arraylist and puts 10 blank items in it
        //{
        //    objPrinterSetup.alPrinterSetup.Clear();
        //    for (int Iterator = 0; Iterator <= 9; Iterator++)
        //    {
        //        PrinterSetup temp = new PrinterSetup();
        //        temp = null;
        //        objPrinterSetup.alPrinterSetup.Add(temp);
        //    }
        //}
        //private void Store(IntPtr hwnd)
        //{
        //    GetDevmode(CurrentPrintSettings, 2, "", hwnd);
        //    CurrentSetup.NameOfPrinter = CurrentPrintSettings.PrinterName;
        //    CurrentSetup.PaperSize = CurrentPrintSettings.DefaultPageSettings.PaperSize;
        //    CurrentSetup.PrintQuality = CurrentPrintSettings.DefaultPageSettings.PrinterResolution;
        //    CurrentSetup.PaperSource = CurrentPrintSettings.DefaultPageSettings.PaperSource;
        //    CurrentSetup.CanDuplex = CurrentPrintSettings.CanDuplex;
        //    CurrentSetup.DSided = CurrentPrintSettings.Duplex;
        //    //CurrentSetup.rbmm = rbmm.Checked;
        //   // CurrentSetup.Landscape = rbLandscape.Checked;
        //    //CurrentSetup.rbInches = rbInch.Checked;
        //    //CurrentSetup.rbInches100 = rbInch100.Checked;
        //    //CurrentSetup.PrinterNotes = tbxPrinterNotes.Text;
        //    //CurrentSetup.Copies = nuCopies.Value;
        //    //CurrentSetup.TopMargin = nuTopMargin.Value;
        //    //CurrentSetup.BottomMargin = nuBottomMargin.Value;
        //    //CurrentSetup.LeftMargin = nuLeftMargin.Value;
        //    //CurrentSetup.RightMargin = nuRightMargin.Value;
        //    CurrentSetup.Devmodearray = DevModeArray;
        //}
        //private void Recall()
        //{
        //    CurrentPrintSettings.PrinterName = CurrentSetup.NameOfPrinter;
        //    UpdatePrintForm(CurrentPrintSettings, CurrentSetup);
        //    //if (rbDevmodeYes.Checked == true)
        //    //{
        //    //    SetDevmode(CurrentPrintSettings, 2, "");
        //    //}
        //    //if (rbDevmodeYes.Checked == false)
        //    //{
        //    //    ChangeSetup();
        //    //}
        //   // UpdatePrintForm(CurrentPrintSettings, CurrentSetup);
        //}

        //private void GetDevmode(PrinterSettings printerSettings, int mode, String Filename, IntPtr hwnd)//Grabs the devmode data in memory and stores in arraylist
        //{
        //    ///int mode
        //    ///1 = Save devmode structure to file
        //    ///2 = Save devmode structure to Byte array and arraylist
        //    IntPtr hDevMode = IntPtr.Zero;                        // handle to the DEVMODE
        //    IntPtr pDevMode = IntPtr.Zero;                          // pointer to the DEVMODE

        //    try
        //    {
        //        // Get a handle to a DEVMODE for the default printer settings
        //        hDevMode = printerSettings.GetHdevmode(printerSettings.DefaultPageSettings);

        //        // Obtain a lock on the handle and get an actual pointer so Windows won't
        //        // move it around while we're futzing with it
        //        pDevMode = GlobalLock(hDevMode);
        //        int sizeNeeded = DocumentProperties(hwnd, IntPtr.Zero, printerSettings.PrinterName, IntPtr.Zero, pDevMode, 0);
        //        if (sizeNeeded <= 0)
        //        {
        //            System.Windows.MessageBox.Show("Devmode Bummer, Cant get size of devmode structure");
        //            GlobalUnlock(hDevMode);
        //            GlobalFree(hDevMode);
        //            return;
        //        }
        //        DevModeArray = new byte[sizeNeeded];    //Copies the buffer into a byte array
        //        if (mode == 1)  //Save devmode structure to file
        //        {
        //            FileStream fs = new FileStream(Filename, FileMode.Create);
        //            for (int i = 0; i < sizeNeeded; ++i)
        //            {
        //                fs.WriteByte(Marshal.ReadByte(pDevMode, i));
        //            }
        //            fs.Close();
        //            fs.Dispose();
        //        }
        //        if (mode == 2)  //Save devmode structure to Byte array and arraylist
        //        {
        //            for (int i = 0; i < sizeNeeded; ++i)
        //            {
        //                DevModeArray[i] = (byte)(Marshal.ReadByte(pDevMode, i));    //Copies the array to an arraylist where it can be recalled
        //            }
        //        }

        //        // Unlock the handle, we're done futzing around with memory
        //        GlobalUnlock(hDevMode);
        //        // And to boot, we don't need that DEVMODE anymore, either
        //        GlobalFree(hDevMode);
        //        hDevMode = IntPtr.Zero;
        //    }
        //    catch (Exception ex)
        //    {
        //        if (hDevMode != IntPtr.Zero)
        //        {
        //            System.Windows.MessageBox.Show("BUGGER");
        //            GlobalUnlock(hDevMode);
        //            // And to boot, we don't need that DEVMODE anymore, either
        //            GlobalFree(hDevMode);
        //            hDevMode = IntPtr.Zero;
        //        }
        //    }
        //}
        //private void SetDevmode(PrinterSettings printerSettings, int mode, String Filename)//Grabs the data in arraylist and chucks it back into memory "Crank the suckers out"
        //{
        //    ///int mode
        //    ///1 = Load devmode structure from file
        //    ///2 = Load devmode structure from arraylist
        //    IntPtr hDevMode = IntPtr.Zero;                        // a handle to our current DEVMODE
        //    IntPtr pDevMode = IntPtr.Zero;                          // a pointer to our current DEVMODE
        //    Byte[] Temparray;
        //    try
        //    {
        //        DevModeArray = CurrentSetup.Devmodearray;
        //        // Obtain the current DEVMODE position in memory
        //        hDevMode = printerSettings.GetHdevmode(printerSettings.DefaultPageSettings);

        //        // Obtain a lock on the handle and get an actual pointer so Windows won't move
        //        // it around while we're futzing with it
        //        pDevMode = GlobalLock(hDevMode);

        //        // Overwrite our current DEVMODE in memory with the one we saved.
        //        // They should be the same size since we haven't like upgraded the OS
        //        // or anything.


        //        if (mode == 1)  //Load devmode structure from file
        //        {
        //            FileStream fs = new FileStream(Filename, FileMode.Open, FileAccess.Read);
        //            Temparray = new byte[fs.Length];
        //            fs.Read(Temparray, 0, Temparray.Length);
        //            fs.Close();
        //            fs.Dispose();
        //            for (int i = 0; i < Temparray.Length; ++i)
        //            {
        //                Marshal.WriteByte(pDevMode, i, Temparray[i]);
        //            }
        //        }
        //        if (mode == 2)  //Load devmode structure from arraylist
        //        {
        //            for (int i = 0; i < DevModeArray.Length; ++i)
        //            {
        //                Marshal.WriteByte(pDevMode, i, DevModeArray[i]);
        //            }
        //        }
        //        // We're done futzing
        //        GlobalUnlock(hDevMode);

        //        // Tell our printer settings to use the one we just overwrote
        //        printerSettings.SetHdevmode(hDevMode);
        //        printerSettings.DefaultPageSettings.SetHdevmode(hDevMode);

        //        // It's copied to our printer settings, so we can free the OS-level one
        //        GlobalFree(hDevMode);
        //    }
        //    catch (Exception ex)
        //    {
        //        if (hDevMode != IntPtr.Zero)
        //        {
        //            System.Windows.MessageBox.Show("BUGGER");
        //            GlobalUnlock(hDevMode);
        //            // And to boot, we don't need that DEVMODE anymore, either
        //            GlobalFree(hDevMode);
        //            hDevMode = IntPtr.Zero;
        //        }
        //    }

        //}
        //private void ChangeSetup()//Does not use the devmode structure
        //{
        //    CurrentPrintSettings.DefaultPageSettings.Landscape = CurrentSetup.Landscape;
        //    CurrentPrintSettings.DefaultPageSettings.PaperSize = CurrentSetup.PaperSize;
        //    //CurrentPrintSettings.DefaultPageSettings.PrinterResolution = CurrentSetup.PrintQuality;
        //    CurrentPrintSettings.DefaultPageSettings.PaperSource = CurrentSetup.PaperSource;
        //    //CurrentPrintSettings.CanDuplex = CurrentSetup.CanDuplex;
        //    CurrentPrintSettings.Duplex = CurrentSetup.DSided;
        //}
        //private PrinterSettings OpenPrinterPropertiesDialog(PrinterSettings printerSettings, IntPtr hwnd)//Shows the printer settings dialogue that comes with the printer driver
        //{
        //    IntPtr hDevMode = IntPtr.Zero;
        //    IntPtr devModeData = IntPtr.Zero;
        //    IntPtr hPrinter = IntPtr.Zero;
        //    String pName = printerSettings.PrinterName;
        //    try
        //    {
        //        hDevMode = printerSettings.GetHdevmode(printerSettings.DefaultPageSettings);
        //        IntPtr pDevMode = GlobalLock(hDevMode);
        //        int sizeNeeded = DocumentProperties(hwnd, IntPtr.Zero, pName, pDevMode, pDevMode, 0);//get needed size and allocate memory 

        //        if (sizeNeeded < 0)
        //        {
        //            System.Windows.MessageBox.Show("Bummer, Cant get size of devmode structure");
        //            Marshal.FreeHGlobal(devModeData);
        //            Marshal.FreeHGlobal(hDevMode);
        //            devModeData = IntPtr.Zero;
        //            hDevMode = IntPtr.Zero;
        //            return printerSettings;
        //        }
        //        devModeData = Marshal.AllocHGlobal(sizeNeeded);
        //        //show the native dialog 
        //        int returncode = DocumentProperties(hwnd, IntPtr.Zero, pName, devModeData, pDevMode, 14);
        //        if (returncode < 0) //Failure to display native dialogue
        //        {
        //            System.Windows.MessageBox.Show("Dialogue Bummer, Got me a devmode, but the dialogue got stuck");
        //            Marshal.FreeHGlobal(devModeData);
        //            Marshal.FreeHGlobal(hDevMode);
        //            devModeData = IntPtr.Zero;
        //            hDevMode = IntPtr.Zero;
        //            return printerSettings;
        //        }
        //        if (returncode == 2) //User clicked "Cancel"
        //        {
        //            GlobalUnlock(hDevMode);//unlocks the memory
        //            if (hDevMode != IntPtr.Zero)
        //            {
        //                Marshal.FreeHGlobal(hDevMode); //Frees the memory
        //                hDevMode = IntPtr.Zero;
        //            }
        //            if (devModeData != IntPtr.Zero)
        //            {
        //                GlobalFree(devModeData);
        //                devModeData = IntPtr.Zero;
        //            }
        //        }
        //        GlobalUnlock(hDevMode);//unlocks the memory

        //        if (hDevMode != IntPtr.Zero)
        //        {
        //            Marshal.FreeHGlobal(hDevMode); //Frees the memory
        //            hDevMode = IntPtr.Zero;
        //        }
        //        if (devModeData != IntPtr.Zero)
        //        {
        //            printerSettings.SetHdevmode(devModeData);
        //            printerSettings.DefaultPageSettings.SetHdevmode(devModeData);
        //            GlobalFree(devModeData);
        //            devModeData = IntPtr.Zero;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        System.Windows.MessageBox.Show("An error has occurred, caught and chucked back\n" + ex.Message);
        //    }
        //    finally
        //    {
        //        if (hDevMode != IntPtr.Zero)
        //        {
        //            Marshal.FreeHGlobal(hDevMode);
        //        }
        //        if (devModeData != IntPtr.Zero)
        //        {
        //            Marshal.FreeHGlobal(devModeData);
        //        }
        //    }
        //    return printerSettings;
        //}
        //private void UpdatePrintForm(PrinterSettings TempSettings, PrinterSetup TempSetup)
        //{
        //    int i = 0;  //used as an iterator to index combo lists

        //    ///These settings are updated from printer settings
        //    ///Possibly after a devmode change from changing properties from printer driver

        //    Console.WriteLine("TempSettings.PrinterName"+TempSettings.PrinterName);
        //    if (TempSettings.IsValid == false)
        //    {
        //        System.Windows.MessageBox.Show("No such Printer on your system");
        //        return;
        //    }
        //    //if (TempSettings.DefaultPageSettings.Landscape == true)
        //    //{
        //    //    rbLandscape.Checked = true;
        //    //    ///No need to set portrait to false as mutually exclusive.
        //    //    ///both can be false but not true.
        //    //}
        //    //if (TempSettings.DefaultPageSettings.Landscape == false)
        //    //{
        //    //    rbPortrait.Checked = true;
        //    //}
        //    //foreach (PaperSource psc in cbxPaperSource.Items)
        //    //{
        //    //    i++;
        //    //    if (psc.SourceName == CurrentPrintSettings.DefaultPageSettings.PaperSource.SourceName)
        //    //    {
        //    //        cbxPaperSource.SelectedIndex = i - 1;
        //    //    }
        //    //}
        //    // i = 0;
        //    //foreach (PaperSize ps in cbxPaperSize.Items)
        //    //{
        //    //    i++;
        //    //    if (ps.PaperName == CurrentPrintSettings.DefaultPageSettings.PaperSize.PaperName)
        //    //    {
        //    //        cbxPaperSize.SelectedIndex = i - 1;
        //    //        if (ps.Kind.ToString() == "Custom")
        //    //        {
        //    //            if (CurrentPrintSettings.DefaultPageSettings.PaperSize.Height <= 0)
        //    //            {
        //    //                string Name = CurrentPrintSettings.DefaultPageSettings.PaperSize.PaperName;
        //    //                int RawKind = CurrentPrintSettings.DefaultPageSettings.PaperSize.RawKind;
        //    //                CurrentPrintSettings.DefaultPageSettings.PaperSize = SetCustomPaper(CustomWidth, CustomHeight, RawKind, Name);
        //    //            }
        //    //            if (CurrentPrintSettings.DefaultPageSettings.PaperSize.Width <= 0)
        //    //            {
        //    //                string Name = CurrentPrintSettings.DefaultPageSettings.PaperSize.PaperName;
        //    //                int RawKind = CurrentPrintSettings.DefaultPageSettings.PaperSize.RawKind;
        //    //                CurrentPrintSettings.DefaultPageSettings.PaperSize = SetCustomPaper(CustomWidth, CustomHeight, RawKind, Name);
        //    //            }
        //    //            tbxPageHeight.ReadOnly = false;
        //    //            tbxPageWidth.ReadOnly = false;
        //    //            tbxPageWidth.Text = CurrentPrintSettings.DefaultPageSettings.PaperSize.Width.ToString();
        //    //            tbxPageHeight.Text = CurrentPrintSettings.DefaultPageSettings.PaperSize.Height.ToString();
        //    //        }
        //    //        else
        //    //        {
        //    //            tbxPageHeight.ReadOnly = true;
        //    //            tbxPageWidth.ReadOnly = true;
        //    //        }
        //    //        tbxPAHeight.Text = CurrentPrintSettings.DefaultPageSettings.PrintableArea.Height.ToString();
        //    //        tbxPAWidth.Text = CurrentPrintSettings.DefaultPageSettings.PrintableArea.Width.ToString();
        //    //    }
        //    //}
        //    //i = 0;
        //    //foreach (PrinterResolution pr in cbxPrintQuality.Items)
        //    //{
        //    //    i++;
        //    //    if (pr.X == CurrentPrintSettings.DefaultPageSettings.PrinterResolution.X)
        //    //    {
        //    //        cbxPrintQuality.SelectedIndex = i - 1;
        //    //    }
        //    //}
        //    // i = 0;
        //}
        //private PaperSize SetCustomPaper(int CustomWidth, int CustomHeight, int RawKind, string Name)
        //{
        //    if (CustomWidth <= 0)
        //    {
        //        CustomWidth = 100;
        //    }
        //    if (CustomHeight <= 0)
        //    {
        //        CustomHeight = 100;
        //    }
        //    PaperSize myPapersize = new PaperSize(Name, CustomWidth, CustomHeight);
        //    myPapersize.RawKind = RawKind;
        //    return myPapersize;
        //}




        //private void LoadPrinterSettings() //Loads the settings currently in the printer for the selected printer
        //{
        //    try
        //    {
        //        nuInhibit = true;
        //        pd.PrinterSettings.PrinterName = "FUJI XEROX ApeosPort-VI C5571";
        //        CurrentPrintSettings = pd.PrinterSettings;
        //        Console.WriteLine("CurrentPrintSettings.PrinterName"+CurrentPrintSettings.PrinterName);

        //        foreach (PaperSize PS in CurrentPrintSettings.PaperSizes)  //Gets the Paper sizes the printer can use
        //        {
        //            if (Enum.IsDefined(PS.Kind.GetType(), PS.Kind))
        //            {
        //                Console.WriteLine(PS);

        //            }
        //        }

        //        Console.WriteLine("cbxPaperSize.Text =" + CurrentPrintSettings.DefaultPageSettings.PaperSize.PaperName);


        //        foreach (PrinterResolution PR in CurrentPrintSettings.PrinterResolutions)  //Gets the printer resolutions
        //        {
        //            if (Enum.IsDefined(PR.Kind.GetType(), PR.Kind))
        //            {
        //                Console.WriteLine(PR);
        //            }
        //        }
        //        {
        //            Console.WriteLine("cbxPrintQuality.Text = " + CurrentPrintSettings.DefaultPageSettings.PrinterResolution.Kind.ToString());
        //            Console.WriteLine("tbxHRes.Text = " + CurrentPrintSettings.DefaultPageSettings.PrinterResolution.X.ToString());
        //            Console.WriteLine("tbxVres.Text = " + CurrentPrintSettings.DefaultPageSettings.PrinterResolution.Y.ToString());

        //            foreach (PaperSource PSC in CurrentPrintSettings.PaperSources)  //Gets the Paper sources the printer has
        //            {
        //                if (Enum.IsDefined(PSC.Kind.GetType(), PSC.Kind))
        //                {
        //                    Console.WriteLine("cbxPaperSource.Items.Add" + PSC);

        //                }
        //            }

        //            //if (CurrentPrintSettings.CanDuplex == true)
        //            //{
        //            //    cbBothsides.Enabled = true;
        //            //}
        //            //if (CurrentPrintSettings.CanDuplex == false)
        //            //{
        //            //    cbBothsides.Enabled = false;
        //            //}
        //            //if (CurrentPrintSettings.SupportsColor == true)
        //            //{
        //            //    cbxColour.SelectedIndex = 0;
        //            //}
        //            //if (CurrentPrintSettings.SupportsColor == false)
        //            //{
        //            //    cbxColour.SelectedIndex = 1;
        //            //}
        //            //if (nuCopies.Value > 1)
        //            //{
        //            //    cbCollate.Enabled = true;
        //            //}
        //            //if (nuCopies.Value == 1)
        //            //{
        //            //    cbCollate.Enabled = false;
        //            //}
        //            //if (CurrentPrintSettings.DefaultPageSettings.Landscape == true)
        //            //{
        //            //    rbLandscape.Checked = true;
        //            //}
        //            //if (CurrentPrintSettings.DefaultPageSettings.Landscape == false)
        //            //{
        //            //    rbPortrait.Checked = true;
        //            //}

        //            //DefaultSetup = SetPrinterDefault(CurrentPrintSettings);
        //            //MaximisePrintableArea();
        //            nuInhibit = false;
        //            UpdatePrintForm(CurrentPrintSettings, CurrentSetup);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        System.Windows.MessageBox.Show(ex.Message);
        //        nuInhibit = false;
        //    }
        //}

        ////-------------------------------Structures and classes----------------------------



        ///// <summary>
        ///// This class is the printer settings item. contains notes, printername, source, copies etc
        ///// </summary>
        //[Serializable]
        //public class PrinterSetup
        //{
        //    ///There are some variables in here which are not used in this solution
        //    ///These have been commented out
        //    ///They are left over from a different solution that this will become part of.
        //    public string PrinterNotes = "";
        //    public PaperSize PaperSize;
        //    public PaperSource PaperSource;
        //    public PrinterResolution PrintQuality;
        //    public string NameOfPrinter = "";
        //    public bool Landscape = false;
        //    //public bool rbmm = false;
        //    //public bool rbInches = false;
        //    //public bool rbInches100 = true;
        //    //public bool Collate = false;
        //    public bool CanDuplex = false;
        //    public Duplex DSided;
        //    public decimal Copies = 1;
        //    //public decimal TopMargin;
        //    //public decimal BottomMargin;
        //    //public decimal LeftMargin;
        //    //public decimal RightMargin;
        //    public byte[] Devmodearray;
        //}
        ///// <summary>
        ///// This class is the printer settings arraylist that can be saved to a file
        ///// </summary>
        //[Serializable]
        //public class PrintSettings
        //{
        //    public String strVersion = "";
        //    public ArrayList alPrinterSetup = new ArrayList();
        //}





    }
}
