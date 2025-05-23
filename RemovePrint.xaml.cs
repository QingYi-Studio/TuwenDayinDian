using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
//using System.Windows.Shapes;
using FormApp16.Zhy.MCI;
using MySqlX.XDevAPI;
using Newtonsoft.Json;
using PdfEdit;
using PdfEdit.Pdf.Content.Objects;
using PdfPrintingNet;
using Spire.Pdf;
using Spire.Pdf.Graphics;
using TuwenDayinDian.Helpers;
using TuwenDayinDian.Models;
using TuwenDayinDian.windows;
using MessageBox = System.Windows.MessageBox;

namespace TuwenDayinDian
{

    /// <summary>
    /// RemovePrint.xaml 的交互逻辑
    /// </summary>
    public partial class RemovePrint : Page
    {
        private ClientWebSocket _webSocket;
        public static Logger log = new Logger();
        private readonly static string path = @"D:\ProgramWork\" + DateTime.Now.ToString("yyyyMMdd");
        int Idi = 0;
      
        string Black_Print = "";
        string InkColor_Print = "";
        string LaserColor_Print = "";
        string Cove_Print = "";
        string BlackBinding_Print = "";
        string OrderCoverPath = ".\\" + "1.pdf";
        string[] PrintName = new string[] { };
        public RemovePrint()
        {
            InitializeComponent();         
            _webSocket = new ClientWebSocket();          
            ConnectWebsocket("25107671");     
            GetToday_ThesisOrder();
            LoadData();
            log.Write("[Info] Remove Print Page init");
        }
        List<ThesisOrder> ThesisOrderAll = new List<ThesisOrder>();
        

        private void CreatDat(string path)
        {
            if (Directory.Exists(path)) //该方法是判断该路径下有没有该文件夹，注意并不能判断某个文件是否存在
            {
                string filePath = path + "\\" + "param.dat";
                if (!File.Exists(filePath))
                {
                    FileStream fs = File.Create(filePath);//创建文件
                    fs.Close();
                    fs.Dispose();
                    return;
                }
            }
            else//如果不存在
            {
                Directory.CreateDirectory(path);//创建这个文件夹
                string jsonFile = "param.dat";
                string filePath = path + "\\" + jsonFile;
                FileStream fs = File.Create(filePath);//创建文件
                fs.Close();
                fs.Dispose();
                return;
            }
        }

        // 数据的保存
        private void SaveData(string path)
        {

            //AfTextFile.Write(path + "\\" + "param.dat", jsonStr, AfTextFile.UTF8);

        }

        // 数据的加载
        private void LoadData()
        {
            string jsonFile =  ".\\" + "printName.dat";
            if (!File.Exists(jsonFile)) return;

            // 从文件中读出文本
            string printNamestr = AfTextFile.Read(jsonFile, AfTextFile.UTF8);
            if (printNamestr == "") return;
            // 将jsonStr 转成 List<Param>

             PrintName = printNamestr.Split(new char[] { ',' });
             Black_Print = PrintName[0];
             InkColor_Print = PrintName[1];
             LaserColor_Print = PrintName[2];
             Cove_Print = PrintName[3];
             BlackBinding_Print = PrintName[4];

        }



        private static int[] ToIntArray(string[] Content)
        {
            int[] c = new int[Content.Length];
            for (int i = 0; i < Content.Length; i++)
            {
                c[i] = Convert.ToInt32(Content[i].ToString());
            }
            return c;
        }
        private async void ConnectWebsocket(string userId)
        {
            try
            {
                Uri serverUri = new Uri("ws://www.xuanzhuanmumatuwen.com/websocket?userId=" + userId); // WebSocket服务器地址
                await _webSocket.ConnectAsync(serverUri, CancellationToken.None);
                //StatusTextBlock.Text = "Connected";
                //ConnectButton.IsEnabled = false;
                //SendButton.IsEnabled = true;
                //ReceiveButton.IsEnabled = true;
                //RemovePrintLogin.Visibility = Visibility.Collapsed;
                //RemovePrintListView.Visibility = Visibility.Visible;
                await ReceiveMessages(); // 启动接收消息的异步任务

            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
            
        }

        //private async void SendButton_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        string message = MessageTextBox.Text;
        //        byte[] buffer = System.Text.Encoding.UTF8.GetBytes(message);
        //        await _webSocket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);
        //        StatusTextBlock.Text = "Message sent";
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        //private async void ReceiveButton_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        byte[] buffer = new byte[1024];
        //        WebSocketReceiveResult result = await _webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
        //        string message = System.Text.Encoding.UTF8.GetString(buffer, 0, result.Count);
        //        System.Windows.MessageBox.Show("Received message: " + message);
        //    }
        //    catch (Exception ex)
        //    {
        //        System.Windows.MessageBox.Show(ex.Message);
        //    }
        //}



        private async Task ReceiveMessages()
        {

            var buffer = new byte[50 * 1024]; // 4KB 缓冲区
            var messageBuffer = new ArraySegment<byte>(buffer);
            var stringBuilder = new StringBuilder();

            while (_webSocket.State == WebSocketState.Open)
            {
                WebSocketReceiveResult result = null;
                stringBuilder.Clear();

                do
                {
                    result = await _webSocket.ReceiveAsync(messageBuffer, CancellationToken.None);

                    if (result.MessageType == WebSocketMessageType.Close)
                    {
                        await _webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
                        return;
                    }
                    else if (result.MessageType == WebSocketMessageType.Binary)
                    {
                        // 如果需要，处理二进制消息
                    }
                    else if (result.MessageType == WebSocketMessageType.Text)
                    {
                        var messageChunk = Encoding.UTF8.GetString(buffer, 0, result.Count);
                        stringBuilder.Append(messageChunk);
                    }
                }
                while (!result.EndOfMessage);

                var message = stringBuilder.ToString();
               
                // 根据需要处理完整的消息
                Models.ThesisOrder Orders = JsonConvert.DeserializeObject<Models.ThesisOrder>(message);            



                new MCI().Play(@"E:\mp3\order.mp3", 1);

                Idi++;
                upNowdata_ThesisOrder(Orders);

              // await print_ThesisOrder(Orders);

                ThesisOrderAll.Add(Orders);

            }
        }

        //添加至表格
        private async void upNowdata_ThesisOrder(Models.ThesisOrder Orders)
        {
       
            string ReceivingInfo = Orders.ReceivingInfo;
            string date1 = DateTime.Now.ToString("HH:mm:ss");
            string BindingStyleText = "";
            string ColorStyleText = "";
            string SizeStyleText = "";
            int j = 0;
            string filePath2 = path + "\\" + Orders.OrderSN.ToString();
            foreach (var param in Orders.ThesisParam)
            {
                
                string filePath = await downloadFile(param.FileName, filePath2, j.ToString());
                Orders.ThesisParam[j].Path = filePath;
                j++;
                switch (param.BindingStyle)
                {
                    case "0":
                        BindingStyleText = "不装订";
                        break;
                    case "1":
                        BindingStyleText = "订书机";
                        break;
                    case "2":
                        BindingStyleText = "抽杆夹";

                        break;
                    case "3":
                        BindingStyleText = "胶装";
                        break;

                    case "4":
                        Console.WriteLine("Invalid param");
                        break;

                };
                switch (param.ColorStyle)
                {
                    case "1":
                        ColorStyleText = "黑白";
                        break;
                    case "2":
                        ColorStyleText = "彩色";
                        break;
                    case "3":
                        ColorStyleText = "激光彩色";
                        break;
                    case "4":
                        ColorStyleText = "证件照";
                        break;
                    case "5":
                        Console.WriteLine("Invalid param");
                        break;

                };
                string SinglePagexx = "";
                if (param.Duplex == "")
                {
                    SinglePagexx = param.SinglePage;
                }
                else
                {
                    switch (param.Duplex)
                    {
                        case "0":
                            SinglePagexx = "请自查";
                            break;
                        case "1":
                            SinglePagexx = "单面打印";
                            break;
                        case "2":
                            SinglePagexx = "双面长边";
                            break;
                        case "3":
                            SinglePagexx = "双面短边";
                            break;
                        case "4":
                            Console.WriteLine("Invalid param");
                            break;

                    };

                }

                if (param.Size == "")
                {
                    SizeStyleText = param.A3Page;
                }
                else
                {
                    switch (param.Size)
                    {
                        case "8":
                            SizeStyleText = "A3";
                            break;
                        case "9":
                            SizeStyleText = "A4";
                            break;
                        case "4":
                            Console.WriteLine("Invalid param");
                            break;
                    };
                }
                Console.WriteLine("j=" + j);
                Models.ThesisParam.ThesisParams.Add(new Models.ThesisParam
                {
                    OrderId = Idi,
                    FileId=j,
                    FileName = param.FileName,
                    Date = date1,
                    CoverType = param.CoverType,
                    Copies = param.Copies,
                    BindingStyle = BindingStyleText,
                    SinglePage = SinglePagexx+"范围-"+param.Pages,
                    A3Page = SizeStyleText,
                    ColorStyle = ColorStyleText,
                    ReceivingInfo = ReceivingInfo,
                    OrderStatus=""

                });

                ReceivingInfo = "";
                date1 = "";
                

            }
            RemovePrintListView.ItemsSource = Models.ThesisParam.ThesisParams;
        }

        private void updata_ThesisOrder(Models.ThesisOrder Orders)
        {
            DateTime dateTime = DateTime.Parse(Orders.Created_at);
            string timeOnly = dateTime.ToString("HH:mm:ss");
            string OrderStatu1 = "";
            int j = 0;
            string ReceivingInfo = Orders.ReceivingInfo;
            string date1 = timeOnly;
            string BindingStyleText = "";
            string ColorStyleText = "";
            string SizeStyleText = "";
            string filePath2 = path + "\\" + Orders.OrderSN.ToString();
            string orderSate = filePath2 + "\\" + "OrderStatus.dat";
            if (File.Exists(orderSate))
            {
                OrderStatu1 = "完成";
            }
            foreach (var param in Orders.ThesisParam)
            {

                Orders.ThesisParam[j].Path = filePath2+ "\\" +j +".pdf";
             

                j++;
                switch (param.BindingStyle)
                {
                    case "0":
                        BindingStyleText = "不装订";
                        break;
                    case "1":
                        BindingStyleText = "订书机";
                        break;
                    case "2":
                        BindingStyleText = "抽杆夹";

                        break;
                    case "3":
                        BindingStyleText = "胶装";
                        break;

                    case "4":
                        Console.WriteLine("Invalid param");
                        break;

                };
                switch (param.ColorStyle)
                {
                    case "1":
                        ColorStyleText = "黑白";
                        break;
                    case "2":
                        ColorStyleText = "彩色";
                        break;
                    case "3":
                        ColorStyleText = "激光彩色";
                        break;
                    case "4":
                        ColorStyleText = "证件照";
                        break;
                    case "5":
                        Console.WriteLine("Invalid param");
                        break;

                };
                string SinglePagexx = "";
                if (param.Duplex == "")
                {
                    SinglePagexx = param.SinglePage;
                }
                else
                {
                    switch (param.Duplex)
                    {
                        case "0":
                            SinglePagexx = "请自查";
                            break;
                        case "1":
                            SinglePagexx = "单面打印";
                            break;
                        case "2":
                            SinglePagexx = "双面长边";
                            break;
                        case "3":
                            SinglePagexx = "双面短边";
                            break;
                        case "4":
                            Console.WriteLine("Invalid param");
                            break;

                    };

                }

                if (param.Size == "")
                {
                    SizeStyleText = param.A3Page;
                }
                else
                {
                    switch (param.Size)
                    {
                        case "8":
                            SizeStyleText = "A3";
                            break;
                        case "9":
                            SizeStyleText = "A4";
                            break;
                        case "4":
                            Console.WriteLine("Invalid param");
                            break;
                    };
                }
                Console.WriteLine("j=" + j);
              
               
               
                Models.ThesisParam.ThesisParams.Add(new Models.ThesisParam
                {
                    OrderId = Idi,
                    FileName = param.FileName,
                    FileId=j,
                    Date = date1,
                    CoverType = param.CoverType,
                    Copies = param.Copies,
                    BindingStyle = BindingStyleText,
                    SinglePage = SinglePagexx + "范围-" + param.Pages,
                    A3Page = SizeStyleText,
                    ColorStyle = ColorStyleText,
                    ReceivingInfo = ReceivingInfo,
                    OrderStatus= OrderStatu1

                });
                ReceivingInfo = "";
                date1 = "";


            }
            RemovePrintListView.ItemsSource = Models.ThesisParam.ThesisParams;
        }

        //操作文件
        private async Task<string> print_ThesisOrder(Models.ThesisOrder Orders,string i)
        {
            int coveId = 0;
            string printName = InkColor_Print;
            PrintHelper printhelper = new PrintHelper();
            if (Orders.ThesisParam[0].ColorStyle == "1")
            {
                printName = Black_Print;
            }

            if (Orders.ThesisParam[0].ColorStyle == "3")
            {
                printName = LaserColor_Print;
            }
            if (Orders.ThesisParam[0].ColorStyle != "4")
            {
               
                string orderPath1 =await printhelper.MakeOrder(OrderCoverPath, Orders.ReceivingInfo,Orders.OrderSN,i, Orders.ThesisParam, path + "\\" + Orders.OrderSN.ToString() + "\\order.pdf");
                //Console.WriteLine("orderPath1"+orderPath1);
                //await printhelper.Print_PDFfile(orderPath1, printName, 1, 9, "1-1", 1);

                await Task.Run(() =>
                {
                    try
                    {
                        PdfPrint pdfprint = new PdfPrint("", "");   
                        pdfprint.PrinterName = printName;                   
                        Duplex dx = (Duplex)1;
                        pdfprint.DuplexType = dx;
                        PaperSource paperSource = new PaperSource();
                        // paperSource.SourceName = "纸盘 2";
                        paperSource.RawKind = 261;
                        pdfprint.PaperSource = paperSource;
                        //pdfprint.PrintInColor = false;                
                        PaperSize ps = new PaperSize("A4", 210, 297);
                        //ps.RawKind = 9;
                        ps.RawKind = 9;
                        pdfprint.PaperSize = ps;
                        pdfprint.Collate = true;
                        // pdfprint.Scale = PdfPrint.ScaleTypes.FitToMargins;
                        pdfprint.IsAutoRotate = true;
                        var fole = pdfprint.Print(orderPath1);
                        return 1;
                    }
                    catch (Exception ex)
                    {
                        System.Windows.MessageBox.Show(ex.Message, "错误信息");
                        return 0;

                    }
                });
                await Task.Delay(600);
                //await printhelper.PrintString(printName, Orders.ReceivingInfo, Orders.ThesisParam);
            }



            DirectoryInfo root = new DirectoryInfo(path);
            if (!Directory.Exists(root.ToString()))
            {
                Directory.CreateDirectory(root.ToString());
            }
            int length = root.GetDirectories().Length;
            string filePath2 = path + "\\" + Orders.OrderSN.ToString();

            //遍历文件
            foreach (var param in Orders.ThesisParam)
            {
              //  Console.WriteLine("foreach111");
                
                if (param.ColorStyle == "4")
                {
                    //string logoPath = "E:\\mp3\\1.jpg";
                    string logoPath = "";
                    FileHelper fileHelper = new FileHelper();
                    string photoPath = fileHelper.MakePhoto(param.Path, filePath2, param.ReceivingInfo, logoPath);
                    //printhelper.PrintPicture(photoPath, param.ColorStyle, param.Copies,7);

                }
                if (param.FileType == "图片")
                {
                    switch (param.ColorStyle)
                    {
                        case "1":
                            printName = Black_Print;//"Canon iR-ADV 8205 UFR II";
                            break;
                        case "2":
                            printName = InkColor_Print;//"Canon iR-ADV 8205 UFR II";HP PageWide Pro 452dw Printer
                            break;
                        case "3":
                            printName = LaserColor_Print;//"Canon iR-ADV 8205 UFR II";EPSON L1800 Series (网络 USB1)
                            break;
                        //case "4":
                        //    printName = "EPSON L1800 Series (网络 USB1)";//"Canon iR-ADV 8205 UFR II";
                        //    break;

                    }



                    FileHelper fileHelper = new FileHelper();
                    //string photoPath = fileHelper.MakePhoto(filePath, filePath2, param.ReceivingInfo, logoPath);
                    int size = int.Parse(param.Size);
                    printhelper.PrintPicture(param.Path, printName, param.Copies, size);

                }
                if (param.FileType == "文档" || param.FileType == "压缩包")
                {

                    switch (param.ColorStyle)
                    {
                        case "1":
                            printName = Black_Print;
                            if (param.BindingStyle == "1")
                            {
                                printName = BlackBinding_Print;
                            }
                            break;
                        case "2":
                            printName = InkColor_Print;
                            break;
                        case "3":
                            printName = LaserColor_Print;
                            break;
                        case "4":
                            break;
                        case "5":
                            Console.WriteLine("Invalid param");
                            break;

                    };
                    PdfPrint pdfprint = new PdfPrint("", "");
                    
                    // 转换->图片打印

                    //await printhelper.Print_PDFfile(param.Path, printName, param.Copies, int.Parse(param.Size), param.Pages, int.Parse(param.Duplex));

                }
                if (param.FileType == "论文")
                {
                    coveId++;
                    if (param.BindingStyle == "3")
                    {
                        //没有封皮，用第一页做封皮
                        FileHelper fileHelper = new FileHelper();
                        param.CoverName = await fileHelper.MakeCover(param.Path, param.Spine, coveId);
                        print_Cover(param.CoverName, param.Copies, param.CoverType);

                    }

                    //全部单or双面打印 A4
                    // string printName = "EPSON WF-C21000 Series";
                    printName = InkColor_Print;
                    if (param.ColorStyle == "1")
                    {
                        printName = Black_Print;
                    }

                    if (param.ColorStyle == "3")
                    {
                        printName = LaserColor_Print;
                    }


                    // EPSON WF-C21000 Series
                    if (param.SinglePage == "全部双面打印" && param.A3Page == "" && param.FlyleafPage == "")
                    {

                        await printhelper.Print_PDFfile(param.Path, printName, param.Copies, 9, "", 2);
                    }
                    if (param.SinglePage == "全部单面打印" && param.A3Page == "" && param.FlyleafPage == "")
                    {

                        await printhelper.Print_PDFfile(param.Path, printName, param.Copies, 9, "", 1);
                    }
                    if (param.SinglePage == "" && param.A3Page == "" && param.FlyleafPage == "")
                    {
                        await printhelper.Print_PDFfile(param.Path, printName, param.Copies, 9, "", 1);
                    }


                    //部分单面 有A3
                    if (param.SinglePage != "全部双面打印" && param.SinglePage != "全部单面打印" && param.SinglePage != "")
                    {
                        PdfPrint pdfprint = new PdfPrint("", "");

                        string[] stringA3Page = new string[] { };
                        string[] stringFlyleafPage = new string[] { };
                        string[] stringSinglePage = param.SinglePage.Split(new char[] { ',' });
                        //string[] stringSinglePage0 = new string[] { };
                        // string[] stringSinglePage = new string[] { };


                        if (param.A3Page != "")
                        {
                            stringA3Page = param.A3Page.Split(new char[] { ',' });
                            stringSinglePage = stringSinglePage.Union(stringA3Page).ToArray();
                        }


                        if (param.FlyleafPage != "")
                        {
                            stringFlyleafPage = param.FlyleafPage.Split(new char[] { ',' });
                            stringSinglePage = stringSinglePage.Union(stringFlyleafPage).ToArray();
                        }

                        //Console.WriteLine("stringA3Page" + stringA3Page.Length);
                        //Console.WriteLine("stringFlyleafPage" + stringFlyleafPage.Length);
                        //Console.WriteLine("stringSinglePage1" + stringSinglePage.Length);

                        int[] allPage = newIntArr(param.NumPage);
                        int[] intA3Page = ToIntArray(stringA3Page);
                        int[] intSinglePage = ToIntArray(stringSinglePage);
                        int[] intFlyfealPage = ToIntArray(stringFlyleafPage);
                        int[] duplexPage = allPage.Except(intSinglePage).ToArray();
                        intSinglePage = intSinglePage.Except(intFlyfealPage).ToArray();
                        intSinglePage = intSinglePage.Except(intA3Page).ToArray();



                        string[] paramA3Page = ProcessArray(intA3Page);
                        string[] paramFlyleafPafe = ProcessArray(intFlyfealPage);
                        string[] paramSinglePage = ProcessArray(intSinglePage);
                        string[] paramDuplexPage = ProcessArray(duplexPage);

                        List<Tuple<string, int>> tupleList = ArrangeParam(paramSinglePage, paramDuplexPage, paramA3Page, paramFlyleafPafe);

                        int size = 9;
                       
                        int duplex = 0;
                        if (param.ColorStyle == "1")
                        {
                            printName = Black_Print;
                        }
                        if (param.ColorStyle == "2")
                        {
                            printName = InkColor_Print;
                        }
                        if (param.ColorStyle == "3")
                        {
                            printName = LaserColor_Print;
                        }

                        for (int c = 0; c < param.Copies; c++)
                        {
                            foreach (var tuple in tupleList)
                            {
                                int cardBorad = 0;
                                if (param.ColorStyle == "1")
                                {
                                    printName = Black_Print;
                                }
                                if (param.ColorStyle == "2")
                                {
                                    printName = InkColor_Print;
                                }
                                if (param.ColorStyle == "3")
                                {
                                    printName = LaserColor_Print;
                                }
                                switch (tuple.Item2)
                                {
                                    case 1:
                                        size = 9;
                                        duplex = 1;                                        
                                        break;
                                    case 2:
                                        size = 9;
                                        duplex = 2;                                        
                                        break;
                                    case 3:
                                        size = 8;
                                        duplex = 1;
                                        break;
                                    case 4:
                                        size = 9;
                                        duplex = 1;
                                        cardBorad = 261;
                                        break;

                                    case 5:
                                        Console.WriteLine("Invalid param");
                                        break;

                                };
                                //Console.WriteLine($"{tuple.Item1} : {tuple.Item2}");
                                // Console.WriteLine($"{tuple.Item1} : {tuple.Item2}" + "  size" + size + "  cardBorad" + cardBorad + "  duplex" + duplex);


                                await printhelper.Print_PDFcontinuity(pdfprint, param.Path, printName, cardBorad, size, tuple.Item1, duplex, 1);
                            }
                        }
                    }
                }
                       

            }

            return "ok";
        }

        private async Task print_File(Models.ThesisParam param)
        {
            PrintHelper printhelper = new PrintHelper();
            string printName = InkColor_Print;
            

                //if (param.ColorStyle == "4")
                //{
                //    //string logoPath = "E:\\mp3\\1.jpg";
                //    string logoPath = "";
                //    FileHelper fileHelper = new FileHelper();
                //    string photoPath = fileHelper.MakePhoto(param.Path, filePath2, param.ReceivingInfo, logoPath);
                //    //printhelper.PrintPicture(photoPath, param.ColorStyle, param.Copies,7);
                //}
                if (param.FileType == "图片")
                {
                    switch (param.ColorStyle)
                    {
                        case "1":
                            printName = Black_Print;//"Canon iR-ADV 8205 UFR II";
                            break;
                        case "2":
                            printName = InkColor_Print;//"Canon iR-ADV 8205 UFR II";HP PageWide Pro 452dw Printer
                            break;
                        case "3":
                            printName = LaserColor_Print;//"Canon iR-ADV 8205 UFR II";EPSON L1800 Series (网络 USB1)
                            break;
                            //case "4":
                            //    printName = "EPSON L1800 Series (网络 USB1)";//"Canon iR-ADV 8205 UFR II";
                            //    break;
                    }



                    FileHelper fileHelper = new FileHelper();
                    //string photoPath = fileHelper.MakePhoto(filePath, filePath2, param.ReceivingInfo, logoPath);
                    int size = int.Parse(param.Size);
                    printhelper.PrintPicture(param.Path, printName, param.Copies, size);

                }
                if (param.FileType == "文档" || param.FileType == "压缩包")
                {

                switch (param.ColorStyle)
                {
                    case "1":
                        printName = Black_Print;
                        if (param.BindingStyle == "1")
                        {
                            printName = BlackBinding_Print;
                        }
                        break;
                    case "2":
                        printName = InkColor_Print;
                        break;
                    case "3":
                        printName = LaserColor_Print;
                        break;
                    default:
                        Console.WriteLine("Invalid param");
                        break;
                };
                    //PdfPrint pdfprint = new PdfPrint("", "");

                    await printhelper.Print_PDFfile(param.Path, printName, param.Copies, int.Parse(param.Size), param.Pages, int.Parse(param.Duplex));

            }
            if (param.FileType == "论文")
                {

                    if (param.BindingStyle == "3")
                    {
                        //没有封皮，用第一页做封皮
                        FileHelper fileHelper = new FileHelper();
                        param.CoverName = await fileHelper.MakeCover(param.Path, param.Spine,1);
                        print_Cover(param.CoverName, param.Copies, param.CoverType);

                    }

                    //全部单or双面打印 A4
                    // string printName = "EPSON WF-C21000 Series";
                    printName = InkColor_Print;
                    if (param.ColorStyle == "1")
                    {
                        printName = Black_Print;
                    }

                    if (param.ColorStyle == "3")
                    {
                        printName = LaserColor_Print;
                    }


                    // EPSON WF-C21000 Series
                    if (param.SinglePage == "全部双面打印" && param.A3Page == "" && param.FlyleafPage == "")
                    {

                        await printhelper.Print_PDFfile(param.Path, printName, param.Copies, 9, "", 2);
                    }
                    if (param.SinglePage == "全部单面打印" && param.A3Page == "" && param.FlyleafPage == "")
                    {

                        await printhelper.Print_PDFfile(param.Path, printName, param.Copies, 9, "", 1);
                    }
                    if (param.SinglePage == "" && param.A3Page == "" && param.FlyleafPage == "")
                    {

                        await printhelper.Print_PDFfile(param.Path, printName, param.Copies, 9, "", 1);
                    }


                    //部分单面 有A3
                    if (param.SinglePage != "全部双面打印" && param.SinglePage != "全部单面打印" && param.SinglePage != "")
                    {
                        PdfPrint pdfprint = new PdfPrint("", "");

                        string[] stringA3Page = new string[] { };
                        string[] stringFlyleafPage = new string[] { };
                        string[] stringSinglePage = param.SinglePage.Split(new char[] { ',' });
                        //string[] stringSinglePage0 = new string[] { };
                        // string[] stringSinglePage = new string[] { };


                        if (param.A3Page != "")
                        {
                            stringA3Page = param.A3Page.Split(new char[] { ',' });
                            stringSinglePage = stringSinglePage.Union(stringA3Page).ToArray();
                        }


                        if (param.FlyleafPage != "")
                        {
                            stringFlyleafPage = param.FlyleafPage.Split(new char[] { ',' });
                            stringSinglePage = stringSinglePage.Union(stringFlyleafPage).ToArray();
                        }

                        //Console.WriteLine("stringA3Page" + stringA3Page.Length);
                        //Console.WriteLine("stringFlyleafPage" + stringFlyleafPage.Length);
                        //Console.WriteLine("stringSinglePage1" + stringSinglePage.Length);

                        int[] allPage = newIntArr(param.NumPage);
                        int[] intA3Page = ToIntArray(stringA3Page);
                        int[] intSinglePage = ToIntArray(stringSinglePage);
                        int[] intFlyfealPage = ToIntArray(stringFlyleafPage);
                        int[] duplexPage = allPage.Except(intSinglePage).ToArray();
                        intSinglePage = intSinglePage.Except(intFlyfealPage).ToArray();
                        intSinglePage = intSinglePage.Except(intA3Page).ToArray();



                        string[] paramA3Page = ProcessArray(intA3Page);
                        string[] paramFlyleafPafe = ProcessArray(intFlyfealPage);
                        string[] paramSinglePage = ProcessArray(intSinglePage);
                        string[] paramDuplexPage = ProcessArray(duplexPage);

                        List<Tuple<string, int>> tupleList = ArrangeParam(paramSinglePage, paramDuplexPage, paramA3Page, paramFlyleafPafe);

                        int size = 9;

                        int duplex = 0;
                        if (param.ColorStyle == "1")
                        {
                            printName = Black_Print;
                        }
                        if (param.ColorStyle == "2")
                        {
                            printName = InkColor_Print;
                        }
                        if (param.ColorStyle == "3")
                        {
                            printName = LaserColor_Print;
                        }

                        for (int c = 0; c < param.Copies; c++)
                        {
                            foreach (var tuple in tupleList)
                            {
                                int cardBorad = 0;
                                if (param.ColorStyle == "1")
                                {
                                    printName = Black_Print;
                                }
                                if (param.ColorStyle == "2")
                                {
                                    printName = InkColor_Print;
                                }
                                if (param.ColorStyle == "3")
                                {
                                    printName = LaserColor_Print;
                                }
                                switch (tuple.Item2)
                                {
                                    case 1:
                                        size = 9;
                                        duplex = 1;
                                        break;
                                    case 2:
                                        size = 9;
                                        duplex = 2;
                                        break;
                                    case 3:
                                        size = 8;
                                        duplex = 1;
                                        break;
                                    case 4:
                                        size = 9;
                                        duplex = 1;
                                        cardBorad = 261;
                                        break;

                                    case 5:
                                        Console.WriteLine("Invalid param");
                                        break;

                                };
                                //Console.WriteLine($"{tuple.Item1} : {tuple.Item2}");
                                // Console.WriteLine($"{tuple.Item1} : {tuple.Item2}" + "  size" + size + "  cardBorad" + cardBorad + "  duplex" + duplex);


                                await printhelper.Print_PDFcontinuity(pdfprint, param.Path, printName, cardBorad, size, tuple.Item1, duplex, 1);
                            }
                        }
                    }
                }

        }
        private async void GetToday_ThesisOrder()
        {
            
        HttpClient client = new HttpClient();
            // 发送GET请求到指定URL
            HttpResponseMessage response = await client.GetAsync("https://www.xuanzhuanmumatuwen.com/getpayorderbytime?DeviceName=25107671");
            response.EnsureSuccessStatusCode();  // 确保请求成功

            // 读取响应内容
            string responseContent = await response.Content.ReadAsStringAsync();        
         

            ThesisOrderAll = JsonConvert.DeserializeObject<List<Models.ThesisOrder>>(responseContent);
            foreach (var thesisOrder in ThesisOrderAll)
            {
                Idi++;
                updata_ThesisOrder(thesisOrder);
            }


        }






        public async Task<string> downloadFile(string url, string filePath2, string name)
        {
           
            if (!Directory.Exists(filePath2))
            {
               
                Directory.CreateDirectory(filePath2);
            }
            try
            {
                string fileExt = url.Substring(url.LastIndexOf(".")).Trim().ToLower();
                if (fileExt.Length >5)
                {
                    fileExt = ".pdf";
                }
                string fileName = name + fileExt;
                Console.WriteLine("fileName",$"{fileName}");
                string pathFile = filePath2 + "\\" + fileName;
                await Task.Run(() => {     
                    System.Net.WebClient client = new System.Net.WebClient();                    
                    client.DownloadFile(url, pathFile);
                    
                });
                return pathFile;
                //创建文件名称

            }
            catch
            {
                System.Windows.MessageBox.Show("下载文件失败");
                return "";
            }
        }


        //制作封皮
        private async Task<string>  makeCover(string filePath, string spine)
        {
            // 当前行关联的数据
            // int rowIndex = grid.SelectedRows[0].Index;
            //Param tag = (Param)grid.Rows[rowIndex].Tag;
            string directoryName = System.IO.Path.GetDirectoryName(filePath);
            string coverPath = directoryName + "\\封面.pdf";

            await Task.Run(() =>
            {

                PdfDocument pdf = new PdfDocument();
                pdf.LoadFromFile(filePath);
                PdfDocument pdf1 = new PdfDocument();
                PdfPageBase page;
                SizeF size = new SizeF();
                size.Width = 1246.7f;
                size.Height = 842f;
                page = pdf1.Pages.Add(size, new PdfMargins(0));
                //为现有文档的页面创建模板并将模板画到新建文档的页面上
                pdf.Pages[0].CreateTemplate().Draw(page, new PointF(637, 0));

                //添加书脊
                if (spine != "")
                {
                    PdfTrueTypeFont trueTypeFont = new PdfTrueTypeFont(new Font("仿宋", 11f), true);
                    int spineLength;
                    spineLength = spine.Length;
                    int a = 600 / spineLength;
                    for (int i = 0; i < spineLength; i++)
                    {
                        int X = 626;
                        if (spine[i] < 0x4E00 || spine[i] > 0x9FA5) //汉字
                        {
                            X = 629;
                        }
                        page.Canvas.DrawString(spine[i].ToString(), trueTypeFont, PdfBrushes.Black, new PointF(X, 130 + i * a));
                    }
                }
                pdf1.SaveToFile(coverPath);
            });
            


            //保存文件返回路径 
            
            return coverPath;
        }

     

        private void print_Cover(string path, short Copies, string coverType)
        {


            string filemp3 = "E:\\C#\\Biaoge\\" + coverType + ".mp3";
            System.Windows.MessageBox.Show("请放" + coverType + "封皮");
            new MCI().Play(filemp3, 1);
            //语音播报
            short coverColor = 4;
            //if (coverType == "黄色")
            //{
            //    coverColor = 3;
            //}
            //if (coverType == "绿色")
            //{
            //    coverColor = 2;
            //}

            //if(coverType != "黄色"&&coverType!= "蓝绿色")
            //{
            //    coverColor = 6;
            //}

            PdfPrint pdfprint = new PdfPrint("", "");
            pdfprint.PrinterName = Cove_Print; //Xerox Global Print Driver PCL6 Adobe PDF
            pdfprint.Copies = Copies;
            pdfprint.PrintInColor = true;
            PaperSource paperSource = new PaperSource();
            paperSource.RawKind = coverColor;
            pdfprint.PaperSource = paperSource;

            PaperSize ps = new PaperSize("440", 440, 297);
            ps.Height = 1169;
            ps.Width = 1732;
            pdfprint.PaperSize = ps;

            pdfprint.Scale = PdfPrint.ScaleTypes.None;
            // pdfprint.Scale = PdfPrint.ScaleTypes.FitToMargins;
            pdfprint.IsAutoRotate = true;
            var fole = pdfprint.Print(path);

        }

        public async void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (_webSocket.State == WebSocketState.Open)
            {
                await _webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None);
            }
        }


        private static bool IsConsecutive(int[] arr)
        {
            Array.Sort(arr); // 对数组进行排序

            for (int i = 1; i < arr.Length; i++)
            {
                if (arr[i] != arr[i - 1] + 1)
                {
                    return false;
                }
            }
            return true;
        }
      

        static string[] ProcessArray(int[] arr)
        {
            Array.Sort(arr); // 对数组进行排序

            string[] result = new string[arr.Length];
            int resultIndex = 0;

            for (int i = 0; i < arr.Length; i++)
            {
                int start = arr[i];
                while (i + 1 < arr.Length && arr[i + 1] == arr[i] + 1)
                {
                    i++;
                }
                int end = arr[i];

                if (start == end)
                {
                    result[resultIndex] = start.ToString();
                }
                else
                {
                    result[resultIndex] = start + "-" + end;
                }

                resultIndex++;
            }

            Array.Resize(ref result, resultIndex);

            return result;
        }
        static int[] newIntArr(int num)
        {
            int[] numbers = new int[num];

            for (int i = 0; i < num; i++)
            {
                numbers[i] = i + 1;
            }          
          
            return numbers;
        }

        static List<Tuple<string, int>> ArrangeParam(string[] singlePages, string[] duplexPages, string[] A3Pages, string[] flyleafPages)
        {
       

            List<string> mergedArray = new List<string>();
            List<int> sourceIndex = new List<int>();
            //foreach(string page in singlePages)
            //{
            //    Console.WriteLine("singlePages-"+page);
            //}

            //foreach (string page in duplexPages)
            //{
            //    Console.WriteLine("duplexPages-" + page);
            //}

            //foreach (string page in A3Pages)
            //{
            //    Console.WriteLine("A3Pages-" + page);
            //}

            // 合并三个数组
            mergedArray.AddRange(singlePages);
            mergedArray.AddRange(duplexPages);
            if (A3Pages.Length > 0)
            {
                mergedArray.AddRange(A3Pages);
            }
            if (flyleafPages.Length > 0)
            {
                mergedArray.AddRange(flyleafPages);
            }


            // 自定义排序方法，按照“-”前的数字大小排序
            mergedArray.Sort((a, b) =>
            {
                int numA = int.Parse(a.Split('-')[0]);
                int numB = int.Parse(b.Split('-')[0]);
                return numA.CompareTo(numB);
            });
            //去除重复
            List<string> uniqueNumbers = mergedArray.Distinct().ToList();

            // 标记元素来自哪个数组
            for (int i = 0; i < uniqueNumbers.Count; i++)
            {
                if (A3Pages.Contains(uniqueNumbers[i]))
                {
                    sourceIndex.Add(3);
                    continue;
                }
                else if (flyleafPages.Contains(uniqueNumbers[i]))
                {
                    sourceIndex.Add(4);
                    continue;
                }
                else if (duplexPages.Contains(uniqueNumbers[i]))
                {
                    sourceIndex.Add(2);
                    continue;
                }
                else if (singlePages.Contains(uniqueNumbers[i]))
                {
                    sourceIndex.Add(1);
                    continue;
                }
            }

            List<Tuple<string, int>> zippedList = uniqueNumbers.Zip(sourceIndex, (s, i) => new Tuple<string, int>(s, i)).ToList();
            return zippedList;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }
        
        private async void PrintOrderAgain_Click(object sender, RoutedEventArgs e)
        {
            if (RemovePrintListView.SelectedItem is Models.ThesisParam selectedItem)
            {
                //Console.WriteLine("111");
                int i = selectedItem.OrderId - 1;

                try
                {
                    log.Write("[Info] Start printing...");
                    await print_ThesisOrder(ThesisOrderAll[i], selectedItem.OrderId.ToString());
                    log.Write("[Info] Print over.");
                }
                catch (Exception printEx)
                {
                    log.Write("[Error] " + printEx.Message);
                    MessageBox.Show(printEx.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error); 
                }
                selectedItem.OrderStatus = "完成";
                // 刷新ListView以显示更新
                RemovePrintListView.Items.Refresh();

                // TODO: 这里需要使用一个类似updata_ThesisOrder来刷新OrderStatus，不要硬改，不然有概率出现问题

                string filePath2 = path + "\\" + ThesisOrderAll[i].OrderSN.ToString();
                string jsonFile = filePath2 + "\\" + "OrderStatus.dat";
                log.Write("[Info] Start adding the status file...");
                AfTextFile.Write(jsonFile);
                log.Write("[Info] Add the status file over.");
            }
        }
        private async void PrintFileAgain_Click(object sender, RoutedEventArgs e)
        {
            if (RemovePrintListView.SelectedItem is Models.ThesisParam selectedItem)
            {
                int i = selectedItem.OrderId - 1;
                //foreach(var param in ThesisOrderAll[i].ThesisParam)
                //{
                //    Console.WriteLine(param.FileId);
                //    Console.WriteLine(param.FileName);
                //}
                //Console.WriteLine(ThesisOrderAll[i].ThesisParam[selectedItem.FileId - 1].FileId);
                //Console.WriteLine(ThesisOrderAll[i].ThesisParam[selectedItem.FileId - 1].Path);
                //Console.WriteLine(ThesisOrderAll[i].ThesisParam[selectedItem.FileId - 1].Copies);
                //Console.WriteLine(ThesisOrderAll[i].ThesisParam[selectedItem.FileId - 1].Pages);


                log.Write("[Info] 打印调用前");
                await print_File(ThesisOrderAll[i].ThesisParam[selectedItem.FileId-1]);
                log.Write("[Info] 打印调用后");
                selectedItem.OrderStatus = "完成";

                // 刷新ListView以显示更新
                RemovePrintListView.Items.Refresh();
            }
        }

        private async void DownloadAgain_Click(object sender, RoutedEventArgs e)
        {
            if (RemovePrintListView.SelectedItem is Models.ThesisParam selectedItem)
            {
                int i = selectedItem.OrderId - 1;     

               int j = 0;
                string filePath2 = path + "\\" + ThesisOrderAll[i].OrderSN.ToString();
                foreach (var param in ThesisOrderAll[i].ThesisParam)
                {


                    string filePath = await downloadFile(param.FileName, filePath2, j.ToString());
                    ThesisOrderAll[i].ThesisParam[j].Path = filePath;
                    ThesisOrderAll[i].ThesisParam[j].FileId = j+1;
                    j++;   

                }
                RemovePrintListView.ItemsSource = Models.ThesisParam.ThesisParams;

            }
        }
     
        private void ToFile_Click(object sender, RoutedEventArgs e)
        {    
            if (RemovePrintListView.SelectedItem is Models.ThesisParam selectedItem)
            {
                
                int i = selectedItem.OrderId - 1;
                string folderPath = path + "\\"+ThesisOrderAll[i].OrderSN + "\\";  
                ProcessStartInfo processInfo = new ProcessStartInfo();
                processInfo.FileName = "explorer.exe";
                processInfo.Arguments = folderPath;
                try
                {
                    Process.Start(processInfo);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }
        }

        private async void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            PrintHelper printhelper = new PrintHelper();
            if (RemovePrintListView.SelectedItem is Models.ThesisParam selectedItem)
            {
                int i = selectedItem.OrderId - 1;
                await printhelper.Open_PDFfile(ThesisOrderAll[i].ThesisParam[selectedItem.FileId - 1].Path);
             
            }
        }

        private void ChangePrint_Click(object sender, RoutedEventArgs e)
        {
            var changePrint = new ChangePrint(PrintName);
            // selectionWindow.Owner = this.Parent as Window;
            changePrint.WindowStartupLocation = WindowStartupLocation.CenterScreen;
           changePrint.UpdataPrintName += OnUpdataPrintName;
            changePrint.ShowDialog();
        }

        private void OnUpdataPrintName(object sender, string[] e)
        {
            PrintName = e;
             Black_Print = e[0];
             InkColor_Print = e[1];
             LaserColor_Print = e[2];
             Cove_Print = e[3] ;
             BlackBinding_Print = e[4];

            //Console.WriteLine("Black_Print" + Black_Print);
            //Console.WriteLine("InkColor_Print" + InkColor_Print);
            //Console.WriteLine("LaserColor_Print" + LaserColor_Print);
            //Console.WriteLine("Cove_Print" + Cove_Print);
            //Console.WriteLine("1BlackBinding_Print" + BlackBinding_Print);


        }

        //private void OnUpdataPrintName(object sender, Bill e)
        //{

        //    e.SerialNumber = BillId;
        //    Bill.Bills.Add(e);
        //    UpdateTotalPrice();
        //    BillId++;
        //}

        private void DetailButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as System.Windows.Controls.Button;
            if (button?.CommandParameter is Models.ThesisParam selectedItem) // 请替换YourDataType为实际数据模型类型
            {
                Console.WriteLine(selectedItem.FileName+"|"+ selectedItem.OrderId+ "|" + selectedItem.Pages + "|" + selectedItem.BindingStyle);
            }
        }
    }
}
