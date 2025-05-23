using FormApp16.Zhy.MCI;
using Spire.Pdf.Graphics;
using Spire.Pdf;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Drawing;
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
using System.Diagnostics;
using TuwenDayinDian.Components;
using TuwenDayinDian.Helpers;
using TuwenDayinDian.Models;
using Page = System.Windows.Controls.Page;
using Application = Word.Application;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using Word;

using System.IO;
using System.Data;
using Excel;
using System.Security.Policy;
using Newtonsoft.Json;
using System.Windows.Forms;
using static TuwenDayinDian.Helpers.DuplexSettings;
using System.ComponentModel;
using PdfPrintingNet;


namespace TuwenDayinDian
{
    /// <summary>
    /// TasksPage.xaml 的交互逻辑
    /// </summary>
    public partial class TasksPage : Page
    {

        public TasksPage()
        {
            InitializeComponent();
        }

        private void btnQingKong_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = System.Windows.MessageBox.Show("是否确认清空列表？", "确认", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                TaskListDataGrid.PrintTasks.Clear();
            }
        }

        private void btnJiSuanZhongJia_Click(object sender, RoutedEventArgs e)
        {
            tbkZhongJia.Text = PriceListDataGrid.UpdateTotalPrice();
        }

        private void PriceListDataGrid_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void setPrintParam(object sender, RoutedEventArgs e)
        {
            
                var printParam = new PrintParam();
            // selectionWindow.Owner = this.Parent as Window;
                printParam.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                printParam.ShowDialog();
            
            //if (printParam.ShowDialog() == true)
           // {
                // 获取所有选中的装订方式并连接它们
                //var selectedMethods = printParam.BindingMethodListBox.SelectedItems.Cast<string>();
                //string combinedMethods = string.Join(" | ", selectedMethods);

                //printParam.BindingMethod = combinedMethods;

                // 如果ObservableCollection<Pricing>不会自动更新UI，您可能需要手动更新
                // PriceListView.Items.Refresh();
            //}

        }

        #region 开始打印示例
        private void Start_Print(object sender, RoutedEventArgs e)
        {

            // string pdfFilePath = @"C:\Users\Administrator\Desktop\99\6.pdf";




            //PrintHelper printhelper = new PrintHelper();
            //await printhelper.Print_PDFfile1(pdfFilePath, "导出为WPS PDF", 1, 9, "", 1);
            //System.Windows.MessageBox.Show("kaish");





            ////  string wordFile = "C:\\Users\\Administrator\\Desktop\\价格\\1.docx";

            //  //Application wpsWordApp = (Application)wps;
            //  //wpsWordApp.Visible = false;
            //  // dynamic doc = wpsWordApp.Documents.Open("C:\\Users\\Administrator\\Desktop\\价格\\1.docx");
            //  //  int num = doc.ComputeStatistics(WdStatistic.wdStatisticPages);
            //  // Console.WriteLine("num" + num);

            //  Application wordApp = new Application();
            //  wordApp.Visible = false;
            //  wordApp.ActivePrinter = "FUJI XEROX ApeosPort-VI C5571";
            //  Document doc = wordApp.Documents.Open(pdfFilePath);
            // doc.PrintOut(Copies: 1);
            //  //int pageCount = doc.ComputeStatistics(WdStatistic.wdStatisticPages);
            //  //  Console.WriteLine($"Word 文件 '{wordFile}' 的页数为：{pageCount}");
            //  doc.Close();






            //            wordApp.ActivePrinter = "FUJI XEROX ApeosPort-VI C5571";
            //            //wordApp.Visible = false;
            //            Document doc = wordApp.Documents.Open(wordFile);                    
            //            doc.PrintOut(Copies: Copies);











            //            try { 
            //            string pdfFilePath = @"C:\Users\Administrator\Desktop\2-8\30.pdf";

            //            string wpsPdfReaderPath = @"D:\Program Files (x86)\Kingsoft\WPS Office\12.8.2.17149\office6\wpspdf.exe";
            //            // 创建一个 ProcessStartInfo 对象
            //            ProcessStartInfo startInfo = new ProcessStartInfo();
            //            startInfo.FileName = wpsPdfReaderPath;
            //            // 尝试传递文件路径作为参数
            //            startInfo.Arguments = pdfFilePath;
            //            // 隐藏窗口，让其在后台运行
            //            //startInfo.CreateNoWindow = true;
            //            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            //            // 创建并启动进程
            //            Process process = new Process();
            //            process.StartInfo = startInfo;
            //            process.Start();
            //            // 等待一段时间，确保 WPS 完全打开文件
            //            System.Threading.Thread.Sleep(5000);
            //            // 模拟按下 Ctrl + P 进行打印
            //            System.Windows.Forms.SendKeys.SendWait("^p");
            //            System.Threading.Thread.Sleep(2000);
            //            // 模拟按下 Enter 键确认打印
            //            System.Windows.Forms.SendKeys.SendWait("{ENTER}");
            //            Console.WriteLine("已发送打印任务。");
            //        }
            //catch (Exception ex)
            //{
            //Console.WriteLine($"打印过程中出现错误: {ex.Message}");
            //}


















            //// 替换为你的 PDF 文件路径
            //string pdfFilePath = @"C:\Users\Administrator\Desktop\2-8\30.pdf";


            //try
            //{

            //    // 替换为你的 PDF 文件路径


            //    // 创建一个 ProcessStartInfo 对象
            //    ProcessStartInfo startInfo = new ProcessStartInfo();
            //    startInfo.FileName = pdfFilePath;
            //    startInfo.Verb = "Print"; // 指定操作是打印

            //    // 创建并启动进程
            //    Process process = new Process();
            //    process.StartInfo = startInfo;
            //    process.Start();

            //    Console.WriteLine("已发送打印任务。");


            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine($"发生错误: {ex.Message}");
            //}












            //  PrintHelper printhelper = new PrintHelper();


            //await  printhelper.Print_PDFfile("C:\\Users\\Administrator\\Desktop\\打印-165\\2.pdf", "导出为WPS PDF", 1, 9, "", 2);





            // PrintHelper printhelper = new PrintHelper();

            //   printhelper.PrintWordFile("C:\\Users\\Administrator\\Desktop\\test.docx");
            //PrintDocument pd = new PrintDocument();
            //Duplex dx = (Duplex)1;
            //pd.PrinterSettings.Duplex = dx;
            //  PrintHelper printhelper = new PrintHelper();
            //printhelper.PrintWord("C:\\Users\\Administrator\\Desktop\\打印\\1.doc", pd);
            //  PdfPrint pdfprint = new PdfPrint("", "");
            //  printhelper.Print_PDFcontinuity(pdfprint, "D:\\ProgramWork\\20240326\\7\\1.pdf", "HP PageWide Pro 452dw Printer PCL-6", 259, 9, "1", 2);




            //  string wordFile = "C:\\Users\\Administrator\\Desktop\\价格\\1.docx";
            //  var wpsWordApp = Marshal.GetActiveObject("KWps.Application") as Application;
            //  //Application wpsWordApp = (Application)wps;
            //  //wpsWordApp.Visible = false;
            // // dynamic doc = wpsWordApp.Documents.Open("C:\\Users\\Administrator\\Desktop\\价格\\1.docx");
            ////  int num = doc.ComputeStatistics(WdStatistic.wdStatisticPages);
            // // Console.WriteLine("num" + num);

            //  Application wordApp = new Application();
            //  wordApp.Visible = false;
            //  Document doc = wordApp.Documents.Open(wordFile);

            //  int pageCount = doc.ComputeStatistics(WdStatistic.wdStatisticPages);
            //  Console.WriteLine($"Word 文件 '{wordFile}' 的页数为：{pageCount}");
            //  doc.Close();
            //  wordApp.Quit();



            //if (TaskListDataGrid.PrintTasks.Any())
            //{
            //    var groupedTasks = TaskListDataGrid.PrintTasks;   

            //    foreach (var group in groupedTasks)
            //    {
            //        Console.WriteLine(group.文件名称);
            //        Console.WriteLine(group.文件路径);
            //        int duole=0;
            //        switch (group.双面)
            //        {
            //            case "单面打印":
            //                duole = 1;
            //                Console.WriteLine("单面打印");
            //                break;
            //            case "长边翻页":
            //                duole = 2;
            //                Console.WriteLine("长边翻页");
            //                break;
            //            case "短边翻页":
            //                duole = 3;
            //                Console.WriteLine("短边翻页");
            //                break;

            //        }
            //        Console.WriteLine(duole);

            // await new PrintHelper().Print_Wordfile(wordFile, 1,1, "", 2);

            //await new PrintHelper().Print_PDFfile(group.文件路径,1 , ((short)group.份数), "", duole);
            //    }
            //}

        }
        #endregion

















        private void Bulk_operation(object sender, RoutedEventArgs e)
        {

            int[] nums=new int[] { 25,24,23,22,21,20,19,18,17,16,14,13,12,11,10,9,8,7,6,5,1,};
            FileHelper fileHelper = new FileHelper();

            string folderPath = @"\\Chinami-0eljj9t\工作\2024年\6月\29\test"; // 文件夹路径

            try
            {
                string[] files = Directory.GetFiles(folderPath); // 获取文件夹中的所有文件

                foreach (string file in files)
                {
                    fileHelper.Extract_PDFPageNum(file, nums); ; // 打印文件路径
                }
            }
            catch (Exception)
            {
                Console.WriteLine("An error occurred: ");
            }

           // fileHelper.Extract_PDFPageNum("\\\\Chinami-0eljj9t\\工作\\2024年\\6月\\29\\test\\1.pdf", nums);

        }

        private void Make_Cover(object sender, RoutedEventArgs e)
        {
            FileHelper fileHelper = new FileHelper();

            // string folderPath = @"\\Chinami-0eljj9t\工作\2024年\6月\29\test\"; // 文件夹路径


           
            //拆分文档


            //PdfDocument doc = new PdfDocument();

            //try
            //{
            //    string[] files = Directory.GetFiles(folderPath); // 获取文件夹中的所有文件

            //    foreach (string file in files)
            //    {


            //        doc.LoadFromFile(file);                   
            //        doc.Pages.RemoveAt(0);
            //        doc.SaveToFile(file);
            //        doc.Close();
            //    }
            //}
            //catch (Exception)
            //{
            //    Console.WriteLine("An error occurred: ");
            //}
            //PdfDocument pdf1 = new PdfDocument();
            //PdfPageBase page;

            //try
            //{
            //    string[] files = Directory.GetFiles(folderPath); // 获取文件夹中的所有文件

            //    foreach (string file in files)
            //    {


            //        doc.LoadFromFile(file);
            //        page = pdf1.Pages.Add(doc.Pages[0].Size, new PdfMargins(0));
            //        //为现有文档的页面创建模板并将模板画到新建文档的页面上
            //        doc.Pages[0].CreateTemplate().Draw(page, new PointF(0, 0));

            //    }
            //}
            //catch (Exception)
            //{
            //    Console.WriteLine("An error occurred: ");
            //}

            //pdf1.SaveToFile(@"\\Chinami-0eljj9t\工作\2024年\6月\29\cove.pdf", FileFormat.PDF);
            //pdf1.Close();
            //doc.Close();
        }

        private void fileopen(object sender, RoutedEventArgs e)
        {

            //FileHelper filehelper = new FileHelper();
            //filehelper.OpenWpsDocument("C:\\Users\\Administrator\\Desktop\\pdf\\常见问题与解决方法.docx");

        }

        private void Insert_Blankpage(object sender, RoutedEventArgs e)
        {
            FileHelper fileHelper = new FileHelper();

            string folderPath = @"\\Chinami-0eljj9t\工作\2024年\8月\28\test\2023针推班打印"; // 文件夹路径

            PdfDocument doc = new PdfDocument();

            //try
            //{
            //    string[] files = Directory.GetFiles(folderPath); // 获取文件夹中的所有文件

            //    foreach (string file in files)
            //    {


            //        doc.LoadFromFile(file);                   
            //        doc.Pages.RemoveAt(0);
            //        doc.SaveToFile(file);
            //        doc.Close();
            //    }
            //}
            //catch (Exception)
            //{
            //    Console.WriteLine("An error occurred: ");
            //}
          
         

            try
            {
                string[] files = Directory.GetFiles(folderPath); // 获取文件夹中的所有文件

                foreach (string file in files)
                {
                    Console.WriteLine("file"+ file);
                    doc.LoadFromFile(file);
                    doc.Pages.Insert(1);
                    doc.SaveToFile(file);
                }
            }
            catch (Exception)
            {
                Console.WriteLine("An error occurred: ");
            }
                       
          
            doc.Close();

        }


        //private void mps(object sender, RoutedEventArgs e)
        //{
        //    new MCI().Play(@"G:\BaiduSyncdisk\Code\C#\Biaoge\order.mp3", 1);
        //}
    }











    //public class PrinterSettings
    //{
    //    #region "Private Variables"
    //    private IntPtr hPrinter = new System.IntPtr();
    //    private PRINTER_DEFAULTS PrinterValues = new PRINTER_DEFAULTS();
    //    private PRINTER_INFO_2 pinfo = new PRINTER_INFO_2();
    //    private DEVMODE dm;
    //    private IntPtr ptrDM;
    //    private IntPtr ptrPrinterInfo;
    //    private int sizeOfDevMode = 0;
    //    private int lastError;
    //    private int nBytesNeeded;
    //    private long nRet;
    //    private int intError;
    //    private System.Int32 nJunk;
    //    private IntPtr yDevModeData;

    //    #endregion
    //    #region "Win API Def"
    //    [DllImport("kernel32.dll", EntryPoint = "GetLastError", SetLastError = false,
    //    ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
    //    private static extern Int32 GetLastError();
    //    [DllImport("winspool.Drv", EntryPoint = "ClosePrinter", SetLastError = true,
    //    ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
    //    private static extern bool ClosePrinter(IntPtr hPrinter);
    //    [DllImport("winspool.Drv", EntryPoint = "DocumentPropertiesA", SetLastError = true,
    //    ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
    //    private static extern int DocumentProperties(IntPtr hwnd, IntPtr hPrinter,
    //    [MarshalAs(UnmanagedType.LPStr)] string pDeviceNameg,
    //    IntPtr pDevModeOutput, ref IntPtr pDevModeInput, int fMode);
    //    [DllImport("winspool.Drv", EntryPoint = "GetPrinterA", SetLastError = true,
    //        CharSet = CharSet.Ansi, ExactSpelling = true,
    //        CallingConvention = CallingConvention.StdCall)]
    //    private static extern bool GetPrinter(IntPtr hPrinter, Int32 dwLevel,
    //    IntPtr pPrinter, Int32 dwBuf, out Int32 dwNeeded);
    //    /*[DllImport("winspool.Drv", EntryPoint="OpenPrinterA", 
    //        SetLastError=true, CharSet=CharSet.Ansi, 
    //        ExactSpelling=true, CallingConvention=CallingConvention.StdCall)]
    //    static extern bool OpenPrinter([MarshalAs(UnmanagedType.LPStr)] string szPrinter, 
    //    out IntPtr hPrinter, ref PRINTER_DEFAULTS pd)

    //    [ DllImport( "winspool.drv",CharSet=CharSet.Unicode,ExactSpelling=false,
    //    CallingConvention=CallingConvention.StdCall )]
    //    public static extern long OpenPrinter(string pPrinterName, 
    //             ref IntPtr phPrinter, int pDefault);*/

    //    /*[DllImport("winspool.Drv", EntryPoint="OpenPrinterA", 
    //        SetLastError=true, CharSet=CharSet.Ansi, 
    //        ExactSpelling=true, CallingConvention=CallingConvention.StdCall)]
    //    static extern bool OpenPrinter([MarshalAs(UnmanagedType.LPStr)] string szPrinter, 
    //    out IntPtr hPrinter, ref PRINTER_DEFAULTS pd);
    //    */
    //    [DllImport("winspool.Drv", EntryPoint = "OpenPrinterA",
    //        SetLastError = true, CharSet = CharSet.Ansi,
    //        ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
    //    private static extern bool
    //        OpenPrinter([MarshalAs(UnmanagedType.LPStr)] string szPrinter,
    //        out IntPtr hPrinter, ref PRINTER_DEFAULTS pd);
    //    [DllImport("winspool.drv", CharSet = CharSet.Ansi, SetLastError = true)]
    //    private static extern bool SetPrinter(IntPtr hPrinter, int Level, IntPtr
    //    pPrinter, int Command);

    //    /*[DllImport("winspool.drv", CharSet=CharSet.Ansi, SetLastError=true)]
    //    private static extern bool SetPrinter(IntPtr hPrinter, int Level, IntPtr 
    //    pPrinter, int Command);*/

    //    // Wrapper for Win32 message formatter.
    //    /*[DllImport("kernel32.dll", CharSet=System.Runtime.InteropServices.CharSet.Auto)]
    //    private unsafe static extern int FormatMessage( int dwFlags,
    //    ref IntPtr pMessageSource,
    //    int dwMessageID,
    //    int dwLanguageID,
    //    ref string lpBuffer,
    //    int nSize,
    //    IntPtr* pArguments);*/
    //    #endregion
    //    #region "Data structure"
    //    [StructLayout(LayoutKind.Sequential)]
    //    public struct PRINTER_DEFAULTS
    //    {
    //        public int pDatatype;
    //        public int pDevMode;
    //        public int DesiredAccess;
    //    }
    //    /*[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto)]
    //    public struct PRINTER_DEFAULTS
    //    {
    //    public int pDataType;
    //    public IntPtr pDevMode;
    //    public ACCESS_MASK DesiredAccess;
    //    }*/

    //    [StructLayout(LayoutKind.Sequential)]
    //    private struct PRINTER_INFO_2
    //    {
    //        [MarshalAs(UnmanagedType.LPStr)] public string pServerName;
    //        [MarshalAs(UnmanagedType.LPStr)] public string pPrinterName;
    //        [MarshalAs(UnmanagedType.LPStr)] public string pShareName;
    //        [MarshalAs(UnmanagedType.LPStr)] public string pPortName;
    //        [MarshalAs(UnmanagedType.LPStr)] public string pDriverName;
    //        [MarshalAs(UnmanagedType.LPStr)] public string pComment;
    //        [MarshalAs(UnmanagedType.LPStr)] public string pLocation;
    //        public IntPtr pDevMode;
    //        [MarshalAs(UnmanagedType.LPStr)] public string pSepFile;
    //        [MarshalAs(UnmanagedType.LPStr)] public string pPrintProcessor;
    //        [MarshalAs(UnmanagedType.LPStr)] public string pDatatype;
    //        [MarshalAs(UnmanagedType.LPStr)] public string pParameters;
    //        public IntPtr pSecurityDescriptor;
    //        public Int32 Attributes;
    //        public Int32 Priority;
    //        public Int32 DefaultPriority;
    //        public Int32 StartTime;
    //        public Int32 UntilTime;
    //        public Int32 Status;
    //        public Int32 cJobs;
    //        public Int32 AveragePPM;
    //    }
    //    private const short CCDEVICENAME = 32;
    //    private const short CCFORMNAME = 32;
    //    [StructLayout(LayoutKind.Sequential)]
    //    public struct DEVMODE
    //    {
    //        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = CCDEVICENAME)]
    //        public string dmDeviceName;
    //        public short dmSpecVersion;
    //        public short dmDriverVersion;
    //        public short dmSize;
    //        public short dmDriverExtra;
    //        public int dmFields;
    //        public short dmOrientation;
    //        public short dmPaperSize;
    //        public short dmPaperLength;
    //        public short dmPaperWidth;
    //        public short dmScale;
    //        public short dmCopies;
    //        public short dmDefaultSource;
    //        public short dmPrintQuality;
    //        public short dmColor;
    //        public short dmDuplex;
    //        public short dmYResolution;
    //        public short dmTTOption;
    //        public short dmCollate;
    //        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = CCFORMNAME)]
    //        public string dmFormName;
    //        public short dmUnusedPadding;
    //        public short dmBitsPerPel;
    //        public int dmPelsWidth;
    //        public int dmPelsHeight;
    //        public int dmDisplayFlags;
    //        public int dmDisplayFrequency;
    //    }
    //    #endregion
    //    #region "Constants"
    //    private const int DM_DUPLEX = 0x1000;
    //    private const int DM_IN_BUFFER = 8;
    //    private const int DM_OUT_BUFFER = 2;
    //    private const int PRINTER_ACCESS_ADMINISTER = 0x4;
    //    private const int PRINTER_ACCESS_USE = 0x8;
    //    private const int STANDARD_RIGHTS_REQUIRED = 0xF0000;
    //    private const int PRINTER_ALL_ACCESS =
    //        (STANDARD_RIGHTS_REQUIRED | PRINTER_ACCESS_ADMINISTER
    //        | PRINTER_ACCESS_USE);
    //    #endregion
    //    #region "Function to change printer settings" 
    //    public bool ChangePrinterSetting(string PrinterName, PrinterData PS)

    //    {

    //        if (((int)PS.Duplex < 1) || ((int)PS.Duplex > 3))
    //        {
    //            throw new ArgumentOutOfRangeException("nDuplexSetting",
    //                                     "nDuplexSetting is incorrect.");
    //        }
    //        else
    //        {
    //            dm = this.GetPrinterSettings(PrinterName);
    //            dm.dmDefaultSource = (short)PS.source;
    //            dm.dmOrientation = (short)PS.Orientation;
    //            dm.dmPaperSize = (short)PS.Size;
    //            dm.dmDuplex = (short)PS.Duplex;
    //            Marshal.StructureToPtr(dm, yDevModeData, true);
    //            pinfo.pDevMode = yDevModeData;
    //            pinfo.pSecurityDescriptor = IntPtr.Zero;
    //            /*update driver dependent part of the DEVMODE
    //            1 = DocumentProperties(IntPtr.Zero, hPrinter, sPrinterName, yDevModeData
    //            , ref pinfo.pDevMode, (DM_IN_BUFFER | DM_OUT_BUFFER));*/
    //            Marshal.StructureToPtr(pinfo, ptrPrinterInfo, true);
    //            lastError = Marshal.GetLastWin32Error();
    //            nRet = Convert.ToInt16(SetPrinter(hPrinter, 2, ptrPrinterInfo, 0));
    //            if (nRet == 0)
    //            {
    //                //Unable to set shared printer settings.
    //                lastError = Marshal.GetLastWin32Error();
    //                //string myErrMsg = GetErrorMessage(lastError);
    //                throw new Win32Exception(Marshal.GetLastWin32Error());

    //            }
    //            if (hPrinter != IntPtr.Zero)
    //                ClosePrinter(hPrinter);
    //            return Convert.ToBoolean(nRet);
    //        }
    //    }
    //    //private DEVMODE GetPrinterSettings(string PrinterName)
    //    //{
    //    //    PrinterData PData = new PrinterData();
    //    //    DEVMODE dm;
    //    //    const int PRINTER_ACCESS_ADMINISTER = 0x4;
    //    //    const int PRINTER_ACCESS_USE = 0x8;
    //    //    const int PRINTER_ALL_ACCESS = (STANDARD_RIGHTS_REQUIRED |
    //    //               PRINTER_ACCESS_ADMINISTER | PRINTER_ACCESS_USE);


    //    //    PrinterValues.pDatatype = 0;
    //    //    PrinterValues.pDevMode = 0;
    //    //    PrinterValues.DesiredAccess = PRINTER_ALL_ACCESS;
    //    //    nRet = Convert.ToInt32(OpenPrinter(PrinterName,
    //    //                   out hPrinter, ref PrinterValues));
    //    //    if (nRet == 0)
    //    //    {
    //    //        lastError = Marshal.GetLastWin32Error();
    //    //        throw new Win32Exception(Marshal.GetLastWin32Error());
    //    //    }
    //    //    GetPrinter(hPrinter, 2, IntPtr.Zero, 0, out nBytesNeeded);
    //    //    if (nBytesNeeded <= 0)
    //    //    {
    //    //        throw new System.Exception("Unable to allocate memory");
    //    //    }
    //    //    else
    //    //    {
    //    //        // Allocate enough space for PRINTER_INFO_2... 
    //    //        { ptrPrinterIn fo = Marshal.AllocCoTaskMem(nBytesNeeded)};
    //    //        ptrPrinterInfo = Marshal.AllocHGlobal(nBytesNeeded);
    //    //        // The second GetPrinter fills in all the current settings, so all you 
    //    //        // need to do is modify what you're interested in...
    //    //        nRet = Convert.ToInt32(GetPrinter(hPrinter, 2,
    //    //            ptrPrinterInfo, nBytesNeeded, out nJunk));
    //    //        if (nRet == 0)
    //    //        {
    //    //            lastError = Marshal.GetLastWin32Error();
    //    //            throw new Win32Exception(Marshal.GetLastWin32Error());
    //    //        }
    //    //        pinfo = (PRINTER_INFO_2)Marshal.PtrToStructure(ptrPrinterInfo,
    //    //                                              typeof(PRINTER_INFO_2));
    //    //        IntPtr Temp = new IntPtr();
    //    //        if (pinfo.pDevMode == IntPtr.Zero)
    //    //        {
    //    //            // If GetPrinter didn't fill in the DEVMODE, try to get it by calling
    //    //            // DocumentProperties...
    //    //            IntPtr ptrZero = IntPtr.Zero;
    //    //            //get the size of the devmode structure
    //    //            sizeOfDevMode = DocumentProperties(IntPtr.Zero, hPrinter,
    //    //                               PrinterName, ptrZero, ref ptrZero, 0);

    //    //            ptrDM = Marshal.AllocCoTaskMem(sizeOfDevMode);
    //    //            int i;
    //    //            i = DocumentProperties(IntPtr.Zero, hPrinter, PrinterName, ptrDM,
    //    //            ref ptrZero, DM_OUT_BUFFER);
    //    //            if ((i < 0) || (ptrDM == IntPtr.Zero))
    //    //            {
    //    //                //Cannot get the DEVMODE structure.
    //    //                throw new System.Exception("Cannot get DEVMODE data");
    //    //            }
    //    //            pinfo.pDevMode = ptrDM;
    //    //        }
    //    //        intError = DocumentProperties(IntPtr.Zero, hPrinter,
    //    //                  PrinterName, IntPtr.Zero, ref Temp, 0);
    //    //        //IntPtr yDevModeData = Marshal.AllocCoTaskMem(i1);
    //    //        yDevModeData = Marshal.AllocHGlobal(intError);
    //    //        intError = DocumentProperties(IntPtr.Zero, hPrinter,
    //    //                 PrinterName, yDevModeData, ref Temp, 2);
    //    //        dm = (DEVMODE)Marshal.PtrToStructure(yDevModeData, typeof(DEVMODE));
    //    //        //nRet = DocumentProperties(IntPtr.Zero, hPrinter, sPrinterName, yDevModeData
    //    //        // , ref yDevModeData, (DM_IN_BUFFER | DM_OUT_BUFFER));
    //    //        if ((nRet == 0) || (hPrinter == IntPtr.Zero))
    //    //        {
    //    //            lastError = Marshal.GetLastWin32Error();
    //    //            //string myErrMsg = GetErrorMessage(lastError);
    //    //            throw new Win32Exception(Marshal.GetLastWin32Error());
    //    //        }
    //    //        return dm;
    //    //    }
    //    //    #endregion
    //    //}
    //}

}



//using System;
//using System.IO;
//using Microsoft.Office.Interop.Word;
//using iText.Kernel.Pdf;

//class Program
//{
//    static void Main()
//    {
//        string folderPath = @"C:\path\to\your\folder";

//        // 获取所有的 Word 文件
//        string[] wordFiles = Directory.GetFiles(folderPath, "*.docx");

//        // 获取所有的 PDF 文件
//        string[] pdfFiles = Directory.GetFiles(folderPath, "*.pdf");

//        // 处理 Word 文件
//        foreach (string wordFile in wordFiles)
//        {
//            Application wordApp = new Application();
//            Document doc = wordApp.Documents.Open(wordFile);
//            int pageCount = doc.ComputeStatistics(WdStatistic.wdStatisticPages);
//            Console.WriteLine($"Word 文件 '{wordFile}' 的页数为：{pageCount}");
//            doc.Close();
//            wordApp.Quit();
//        }

//        // 处理 PDF 文件
//        foreach (string pdfFile in pdfFiles)
//        {
//            PdfReader pdfReader = new PdfReader(pdfFile);
//            PdfDocument pdfDoc = new PdfDocument(pdfReader);
//            int pageCount = pdfDoc.GetNumberOfPages();
//            Console.WriteLine($"PDF 文件 '{pdfFile}' 的页数为：{pageCount}");
//            pdfDoc.Close();
//            pdfReader.Close();
//        }
//    }
//}