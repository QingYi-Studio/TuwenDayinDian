using FormApp16.Zhy.MCI;
using PdfPrintingNet;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Threading.Tasks;
using Spire.Pdf;
using Spire.Pdf.Graphics;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows;
using System;
using Word;
using Excel;
using PowerPoint;
using Page = System.Windows.Controls.Page;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using Task = System.Threading.Tasks.Task;
using System.Windows.Media.Media3D;
using System.IO;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using System.Windows.Input;
using System.Diagnostics;
using System.Runtime.Remoting.Contexts;
using TuwenDayinDian.Models;
using Spire.Pdf.Print;
using PdfEdit;




namespace TuwenDayinDian.Helpers
{
    public static class PrinterHelper
    {
        public static List<string> GetInstalledPrinters()
        {
            List<string> printers = new List<string>();

            foreach (string printer in PrinterSettings.InstalledPrinters)
            {
                printers.Add(printer);
            }

            return printers;
        }

        public static string GetDefaultPrinter()
        {
            var PrintName = GetDefaultPrinter().ToString();
            return PrintName;
        }
    }

    public  class PrintHelper
    {








//制作封皮
public async Task<string> MakeOrder(string orderCoverPath, string receivingInfo, string orderSn, string printID, List<ThesisParam> thesisParam,string filePath)
        {
            // 当前行关联的数据
            

            await System.Threading.Tasks.Task.Run(() =>
            {

                Spire.Pdf.PdfDocument pdf = new Spire.Pdf.PdfDocument();
                pdf.LoadFromFile(orderCoverPath);

                PdfPageBase page = pdf.Pages[0];
               
                PdfTrueTypeFont trueTypeFont = new PdfTrueTypeFont(new System.Drawing.Font("宋体", 11f), true);
                page.Canvas.DrawString("订单号：" + orderSn+ " | " + printID, trueTypeFont, PdfBrushes.Black, new PointF(30, 20));
                page.Canvas.DrawString("送货信息：" + receivingInfo, trueTypeFont, PdfBrushes.Black, new PointF(30, 32));
                page.Canvas.DrawString("件数：" + thesisParam.Count, trueTypeFont, PdfBrushes.Black, new PointF(30, 65));
                
                int y = 75;
                foreach (var param in thesisParam)
                {
                    string BindingStyleText = "";
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
                    string ColorText = "";
                    switch (param.ColorStyle)
                    {

                        case "1":
                            ColorText = "黑白";
                            break;
                        case "2":
                            ColorText = "彩色";
                            break;
                        case "3":
                            ColorText = "激光彩色";
                            break;

                        case "4":
                            Console.WriteLine("Invalid param");
                            break;
                    };
                    page.Canvas.DrawString("内容：" + ColorText + "打印"+ " | " + BindingStyleText + " | " + param.Copies + "份| " + param.NumPage + "页| " + param.Pages, trueTypeFont, PdfBrushes.Black, new PointF(30, y));     
                    y = y + 12;
                }
              
                pdf.SaveToFile(filePath);

                pdf.Close();
                

            });
            return filePath;
        }

        public async Task Print_Cover(string path, short Copies, string coverType)
        {
           string filemp3 = "E:\\C#\\Biaoge\\" + coverType + ".mp3";
            System.Windows.MessageBox.Show("请放" + coverType + "封皮");
            new MCI().Play(filemp3, 1);

            //语音播报
            short coverColor = 4;
            if (coverType == "黄色")
            {
                coverColor = 3;
            }
            if (coverType == "绿色")
            {
                coverColor = 2;
            }

            //if(coverType != "黄色"&&coverType!= "蓝绿色")
            //{
            //    coverColor = 6;
            //}
            await Task.Run(() =>
            {
                PdfPrint pdfprint = new PdfPrint("", "");
                pdfprint.PrinterName = "FUJI XEROX ApeosPort-VI C5571"; //Xerox Global Print Driver PCL6 Adobe PDF
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
            });

        }
        public async Task Print_PDFcontinuity(PdfPrint pdfprint, string path, string printerName, int cardBoard, int size, string Pages, int duplex,short copies)
        {


            await Task.Run(() =>
            {
                try
                {                    
                    pdfprint.PrinterName = printerName;
                    int a = duplex;
                    Duplex dx = (Duplex)a;
                    pdfprint.DuplexType = dx;
                    //pdfprint.PrintInColor = false;
                    pdfprint.Copies = copies;



                    PaperSource paperSource = new PaperSource();
                   // paperSource.SourceName = "纸盘 2";
                    paperSource.RawKind = cardBoard;
                   
                    pdfprint.PaperSource = paperSource;  
                    pdfprint.Pages = Pages;
                    PaperSize ps = new PaperSize("A4", 210, 297);
                    //ps.RawKind = 9;
                    ps.RawKind = size;
                    pdfprint.PaperSize = ps;
                    pdfprint.Scale = PdfPrint.ScaleTypes.None;
                    // pdfprint.Scale = PdfPrint.ScaleTypes.FitToMargins;
                    pdfprint.IsAutoRotate = true;
                    var fole = pdfprint.Print(path);
                    return 1;
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.Message, "错误信息");
                    return 0;

                }
            });

        }



        public async Task Open_PDFfile(string pdfFilePath)
        {
            await Task.Run(() =>
            {
                try
                {
                  
                    string wpsPdfReaderPath = @"C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe";
                    // 创建一个 ProcessStartInfo 对象
                    ProcessStartInfo startInfo = new ProcessStartInfo();
                    startInfo.FileName = wpsPdfReaderPath;
                    // 尝试传递文件路径作为参数
                    startInfo.Arguments = pdfFilePath;
                    // 隐藏窗口，让其在后台运行
                    //startInfo.CreateNoWindow = true;
                    startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    // 创建并启动进程
                    Process process = new Process();
                    process.StartInfo = startInfo;
                    process.Start();
                    // 等待一段时间，确保 WPS 完全打开文件
                   // System.Threading.Thread.Sleep(3000);
                    // 模拟按下 Ctrl + P 进行打印
                    //System.Windows.Forms.SendKeys.SendWait("^p");
                    //System.Threading.Thread.Sleep(2000);
                    //// 模拟按下 Enter 键确认打印
                    //System.Windows.Forms.SendKeys.SendWait("{ENTER}");
                    Console.WriteLine("已发送打印任务。");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"打印过程中出现错误: {ex.Message}");
                }

            });

        }

        //public async Task Print_PDFfile1(string path, string printerName, short copies, int size, string pages, int duplex)
        //{
        //     await Task.Run(() =>
        //    {
        //        try
        //        {

        //            PdfDocument doc = new PdfDocument();
        //            doc.LoadFromFile(path);
        //            doc.PrintSettings.PrinterName = printerName;
        //            doc.PrintSettings.Copies = copies;

        //            // 设置双面打印
        //            doc.PrintSettings.Duplex = (Duplex)duplex;

        //            if (!string.IsNullOrEmpty(pages))
        //            {
        //                if (pages.Contains("-"))
        //                {
        //                    // 处理页面范围如"1-5"
        //                    var range = pages.Split('-');
        //                    doc.PrintSettings.SelectPageRange(int.Parse(range[0]), int.Parse(range[1]));
        //                }                 
        //            }
                                       
                 
        //            //获取原文档第一页的纸张大小,这里的单位是Point
        //            SizeF pageSize = doc.Pages[0].Size;
        //            double width= pageSize.Width * 2.54;
        //            double height = pageSize.Height * 2.54;
        //            double a4Width = 21.0;
        //            double a4Height = 29.7;
        //            double a3Width = 29.7;
        //            double a3Height = 42.0;
                    
            
        //            if (size == 9)
        //            {
        //                if (height > width)
        //                {
        //                    if (Math.Abs(width - a4Width) >= 0.5 || Math.Abs(height - a4Height) >= 0.5)
        //                    {
        //                        // Console.WriteLine("1");

        //                       // pdfprint.Scale = PdfPrint.ScaleTypes.FitToMargins;
        //                        doc.PrintSettings.SelectSinglePageLayout(PdfSinglePageScalingMode.FitSize, true);
        //                    }
        //                    else
        //                    {
        //                        // Console.WriteLine("2");
        //                        // 如果页面大小接近A4，使用 FitToMargins
        //                        doc.PrintSettings.SelectSinglePageLayout(PdfSinglePageScalingMode.ActualSize, true);
        //                       // pdfprint.Scale = PdfPrint.ScaleTypes.None;
        //                    }

        //                }
        //                else
        //                {
        //                    if (Math.Abs(height - a4Width) >= 0.5 || Math.Abs(width - a4Height) >= 0.5)
        //                    {
        //                        // Console.WriteLine("3");
        //                       // pdfprint.Scale = PdfPrint.ScaleTypes.FitToMargins;
        //                        doc.PrintSettings.SelectSinglePageLayout(PdfSinglePageScalingMode.FitSize, true);
        //                    }
        //                    else
        //                    {
        //                        // Console.WriteLine("4");
        //                        // 如果页面大小接近A4，使用 FitToMargins
        //                        //pdfprint.Scale = PdfPrint.ScaleTypes.None;
        //                        doc.PrintSettings.SelectSinglePageLayout(PdfSinglePageScalingMode.ActualSize, true);
        //                    }
        //                }
        //            }
        //            if (size == 8)
        //            {
        //                doc.PrintSettings.SelectSinglePageLayout(PdfSinglePageScalingMode.FitSize, true);

        //            }
        //            doc.Print();

        //        }
        //        catch (Exception ex)
        //        {
        //            System.Windows.MessageBox.Show(ex.Message, "错误信息");
            
        //        }
        //    });
        //}
        public async Task Print_PDFfile(string path, string printerName, short Copies, int size, string Pages, int duplex)
        {

            await Task.Run(() =>
            {
                try
                {
                    RemovePrint.log.Write("[Info] 准备实例化PdfPrint");
                    PdfPrint pdfprint = new PdfPrint("", "");
                    RemovePrint.log.Write("[Info] 已实例化PdfPrint");
                    var pageSize = pdfprint.GetPdfPageSize(path, 1);
                    RemovePrint.log.Write("[Info] 成功获取pdf页大小");
                    // Console.WriteLine("pageSize" + pageSize);
                    double width = pageSize.Width * 2.54;
                    double height = pageSize.Height * 2.54;
                    double a4Width = 21.0;
                    double a4Height = 29.7;
                    double a3Width = 29.7;
                    double a3Height = 42.0;

                    if (size == 9)
                    {
                        if (height > width)
                        {
                            if (Math.Abs(width - a4Width) >= 0.5 || Math.Abs(height - a4Height) >= 0.5)
                            {
                                // Console.WriteLine("1");

                                pdfprint.Scale = PdfPrint.ScaleTypes.FitToMargins;
                            }
                            else
                            {
                                // Console.WriteLine("2");
                                // 如果页面大小接近A4，使用 FitToMargins
                                pdfprint.Scale = PdfPrint.ScaleTypes.None;
                            }

                        }
                        else
                        {
                            if (Math.Abs(height - a4Width) >= 0.5 || Math.Abs(width - a4Height) >= 0.5)
                            {
                                // Console.WriteLine("3");
                                pdfprint.Scale = PdfPrint.ScaleTypes.FitToMargins;
                            }
                            else
                            {
                                // Console.WriteLine("4");
                                // 如果页面大小接近A4，使用 FitToMargins
                                pdfprint.Scale = PdfPrint.ScaleTypes.None;
                            }
                        }
                    }
                    if (size == 8)
                    {
                        pdfprint.Scale = PdfPrint.ScaleTypes.FitToMargins;

                    }


                    // 定义标准页面大小（例如，A4纸）                              


                    pdfprint.PrinterName = printerName;
                    int a = duplex;
                    Duplex dx = (Duplex)a;
                    pdfprint.DuplexType = dx;
                    //pdfprint.PrintInColor = false;
                    pdfprint.Copies = Copies;
                    pdfprint.Pages = Pages;
                    PaperSize ps = new PaperSize("A4", 210, 297);
                    //ps.RawKind = 9;
                    ps.RawKind = size;
                    pdfprint.PaperSize = ps;
                    pdfprint.Collate = true;
                    // pdfprint.Scale = PdfPrint.ScaleTypes.FitToMargins;
                    pdfprint.IsAutoRotate = true;
                    RemovePrint.log.Write("[Info] 准备进行pdfprint.Print");
                    var fole = pdfprint.Print(path);
                    RemovePrint.log.Write("[Info] pdfprint.Print完成");
                    return 1;
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.Message, "错误信息");
                    return 0;

                }
            });


        }

        public async Task PrintString(string PrinterName,string receivingInfo, List<ThesisParam> thesisParam)
        {
            await Task.Run(() =>
            {
                PrintDocument pd = new PrintDocument();
                PaperSize ps = new PaperSize("A4", 72, 297);
                ps.RawKind = 9;
                pd.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(pd_PrintPage);


                pd.PrinterSettings.PrinterName = PrinterName;// "Canon iR-ADV 8205 UFR II";
                pd.DefaultPageSettings.PaperSize = ps;

               
                // MessageBox.Show("Status = " + pd.ToString());


                void pd_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs ev)
                {

                   // System.Drawing.Font titleFont = new System.Drawing.Font("黑体", 14, System.Drawing.FontStyle.Bold);//标题字体           
                   System.Drawing.Font fntTxt = new System.Drawing.Font("宋体", 12, System.Drawing.FontStyle.Regular);//正文文字
                   System.Drawing.Font fntTxt1 = new System.Drawing.Font("宋体", 10, System.Drawing.FontStyle.Regular);
                    System.Drawing.Brush brush = new SolidBrush(System.Drawing.Color.Black);//画刷           
                   // System.Drawing.Pen pen = new System.Drawing.Pen(System.Drawing.Color.Black);           //线条颜色           
                    System.Drawing.Point po = new System.Drawing.Point(10, 10);
                    ev.Graphics.DrawString("送货信息：" + receivingInfo, fntTxt1, brush, new System.Drawing.Point(10, 30));
                    ev.Graphics.DrawString("件数：" + thesisParam.Count, fntTxt, brush, new System.Drawing.Point(10, 52));
                    int y = 74;
                    foreach (var param in thesisParam)
                    {
                        string BindingStyleText = "";
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
                        string ColorText = "";
                        switch (param.ColorStyle)
                        {
                           
                            case "1":
                                ColorText = "黑白";
                                break;
                            case "2":
                                ColorText = "彩色";
                                break;
                            case "3":
                                ColorText = "激光彩色";
                                break;

                            case "4":
                                Console.WriteLine("Invalid param");
                                break;

                        };


                        ev.Graphics.DrawString("内容：" + ColorText + "打印"+ BindingStyleText + "|"+ param.Copies + "份"+param.Pages+"页", fntTxt, brush, new System.Drawing.Point(10, y));
                        y = y + 20;
                    }
                    
                    
                }
                pd.Print();

            });
            

        }
        public void PrintPicture(string path,string PrinterName,  short Copies, int size)
        {

            Image img = Image.FromFile(path);
            int width = img.Width;
            int height = img.Height;

            PrintDocument pd = new PrintDocument();
            pd.PrinterSettings.Copies = Copies;
            Console.WriteLine("3");

            pd.PrinterSettings.PrinterName = PrinterName;

         


            if (size != 7)
            {
                PaperSize ps = new PaperSize("A4", 300, 297);
                ps.RawKind = size;
                pd.DefaultPageSettings.PaperSize = ps;
            }
            else
            {
                PaperSize pswucun = new PaperSize("3R", 89, 127);
                pswucun.Height = 500;
                pswucun.Width = 350;
                pd.DefaultPageSettings.PaperSize = pswucun;
            }





            if (size == 9)
            {

                if (height >= width)
                {
                    pd.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(pd_PrintPageVertical);
                }
                else
                {
                    pd.DefaultPageSettings.Landscape = true;
                    pd.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(pd_PrintPageLandscape);
                }
            }
            else
            {
                if (size == 8)
                {
                    // MessageBox.Show("size:8");
                    if (height >= width)
                    {
                        pd.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(pd_PrintPage2Vertical);
                    }
                    else
                    {
                        pd.DefaultPageSettings.Landscape = true;
                        pd.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(pd_PrintPage2Landscape);
                    }
                }
                else
                {
                    // MessageBox.Show("size:other");
                    pd.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(pd_PrintPhoto);

                }

            }

            try
            {
                pd.Print();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "打印出错");
                pd.PrintController.OnEndPrint(pd, new PrintEventArgs());
            }



            void pd_PrintPageVertical(object sender, System.Drawing.Printing.PrintPageEventArgs ev)
            {


                int x = width * 1139 / height;

                if (x >= 797)
                {
                    x = 797;
                    int y = 797 * height / width;
                    int my = (1169 - y) / 2;
                    System.Drawing.Rectangle destRect = new System.Drawing.Rectangle(15, my, x, y);
                    ev.Graphics.DrawImage(img, destRect, 0, 0, width, height, System.Drawing.GraphicsUnit.Pixel);
                }
                else
                {
                    int y = 1139;
                    int mx = (827 - x) / 2;
                    System.Drawing.Rectangle destRect = new System.Drawing.Rectangle(mx, 15, x, y);
                    ev.Graphics.DrawImage(img, destRect, 0, 0, width, height, System.Drawing.GraphicsUnit.Pixel);
                }


            }

            void pd_PrintPageLandscape(object sender, System.Drawing.Printing.PrintPageEventArgs ev)
            {
                int y = height * 1139 / width;
                if (y >= 797)
                {
                    y = 797;
                    int x = 797 * width / height;
                    int mx = (1169 - x) / 2;
                    System.Drawing.Rectangle destRect = new System.Drawing.Rectangle(mx, 15, x, y);
                    ev.Graphics.DrawImage(img, destRect, 0, 0, width, height, System.Drawing.GraphicsUnit.Pixel);
                }
                else
                {
                    int x = 1139;
                    int my = (827 - y) / 2;
                    System.Drawing.Rectangle destRect = new System.Drawing.Rectangle(15, my, x, y);
                    ev.Graphics.DrawImage(img, destRect, 0, 0, width, height, System.Drawing.GraphicsUnit.Pixel);
                }
            }


            void pd_PrintPage2Vertical(object sender, System.Drawing.Printing.PrintPageEventArgs ev)
            {
                int x = width * 1624 / height;
                if (x >= 1139)
                {
                    x = 1139;
                    int y = 1139 * height / width;
                    int my = (1654 - y) / 2;
                    System.Drawing.Rectangle destRect = new System.Drawing.Rectangle(15, my, x, y);
                    ev.Graphics.DrawImage(img, destRect, 0, 0, width, height, System.Drawing.GraphicsUnit.Pixel);
                }
                else
                {
                    int y = 1624;
                    int mx = (1169 - x) / 2;
                    System.Drawing.Rectangle destRect = new System.Drawing.Rectangle(mx, 15, x, y);
                    ev.Graphics.DrawImage(img, destRect, 0, 0, width, height, System.Drawing.GraphicsUnit.Pixel);
                }


            }

            void pd_PrintPage2Landscape(object sender, System.Drawing.Printing.PrintPageEventArgs ev)
            {
                int y = height * 1624 / width;
                if (y >= 1139)
                {
                    y = 1139;
                    int x = 1139 * width / height;
                    int mx = (1654 - x) / 2;
                    System.Drawing.Rectangle destRect = new System.Drawing.Rectangle(mx, 15, x, y);
                    ev.Graphics.DrawImage(img, destRect, 0, 0, width, height, System.Drawing.GraphicsUnit.Pixel);
                }
                else
                {
                    int x = 1624;
                    int my = (1169 - y) / 2;
                    System.Drawing.Rectangle destRect = new System.Drawing.Rectangle(15, my, x, y);
                    ev.Graphics.DrawImage(img, destRect, 0, 0, width, height, System.Drawing.GraphicsUnit.Pixel);
                }


            }

            void pd_PrintPhoto(object sender, System.Drawing.Printing.PrintPageEventArgs ev)
            {

                System.Drawing.Rectangle destRect = new System.Drawing.Rectangle(0, 0, 350, 500);
                PrinterResolutionKind px = (PrinterResolutionKind)(-4);
                //ev.PageSettings.PaperSize = pswucun;
                //MessageBox.Show("pswucun:"+ ev.PageSettings.PaperSize);
                ev.PageSettings.PrinterResolution.Kind = px;
                ev.Graphics.DrawImage(img, destRect, 0, 0, width, height, System.Drawing.GraphicsUnit.Pixel);

            }
        }


        public void PrintWordFile(string wordFilePath)
        {
            // 初始化打印文档
            PrintDocument printDoc = new PrintDocument();

            // 设置默认打印机（可根据需要更改）
            printDoc.PrinterSettings.PrinterName = "FUJI XEROX ApeosPort-VI C5571";

            // 检查打印机是否支持双面打印
            if (printDoc.PrinterSettings.CanDuplex)
            {
                // 设置为自动双面打印
                Console.WriteLine("可以双面打印");
                printDoc.PrinterSettings.Duplex = Duplex.Vertical;
            }

            // 设置打印页面事件（这里我们直接通过 Word 打印）
            printDoc.PrintPage += (sender, e) =>
            {
                Word.Application wordApp = new Word.Application();
                Word.Document doc = null;
                
                try
                {
                    // 打开 Word 文档
                    doc = wordApp.Documents.Open(wordFilePath);
                 

                    // 打印文档
                    doc.PrintOut();
                    doc.Close(SaveChanges: false);
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message, "错误信息");
                }
                finally
                {
                    if (doc != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(doc);
                    if (wordApp != null)
                    {
                        wordApp.Quit();
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(wordApp);
                    }

                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                }
            };

            // 执行打印
            try
            {
                printDoc.Print();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "打印失败");
            }
        }
        //public async Task Print_Wordfile(string wordFile, int Color, short Copies, string Pages)
        //{

        //    await Task.Run(() =>
        //    {
        //        try
        //        {
        //            Word.Application wordApp = new Word.Application();
        //            wordApp.ActivePrinter = "FUJI XEROX ApeosPort-VI C5571";
        //            //wordApp.Visible = false;
        //            Document doc = wordApp.Documents.Open(wordFile);                    
        //            doc.PrintOut(Copies: Copies);

        //        }
        //        catch (Exception ex)
        //        {

        //            System.Windows.MessageBox.Show(ex.Message, "错误信息");
        //            //return 0;

        //        }
        //    });

        //}



        public void PrintWord(string FileName, PrintDocument pd)
        {
            //0 check if there are any winword process exist
            //if is,kill it
            Process[] wordProcess = Process.GetProcessesByName("WINWORD");
            for (int i = 0; i < wordProcess.Length; i++)
            {
                wordProcess[i].Kill();
            }
            object missing = System.Reflection.Missing.Value;
            object objFileName = FileName;
            object objPrintName = pd.PrinterSettings.PrinterName;
            Word.Application objApp = new Word.Application();
           
            Word.Document objDoc = null;
            try
            {

                objDoc = objApp.Documents.Open(FileName);
                objDoc.Activate();
                object copies = "1";
                object pages = "";
                object range = Word.WdPrintOutRange.wdPrintAllDocument;
                object items = Word.WdPrintOutItem.wdPrintDocumentContent;
                object pageType = Word.WdPrintOutPages.wdPrintAllPages;
                object oTrue = true;
                object oFalse = false;
                objApp.Options.PrintOddPagesInAscendingOrder = true;
                objApp.Options.PrintEvenPagesInAscendingOrder = true;
                objDoc.PrintOut(
                ref oTrue, ref oFalse, ref range, ref missing, ref missing, ref missing,
                ref items, ref copies, ref pages, ref pageType, ref oFalse, ref oTrue,
                ref missing, ref oFalse, ref missing, ref missing, ref missing, ref missing);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (objDoc != null)
                {
                    Marshal.ReleaseComObject(objDoc);
                    Marshal.FinalReleaseComObject(objDoc);
                    objDoc = null;
                }
                if (objApp != null)
                {
                    objApp.Quit(ref missing, ref missing, ref missing);
                    Marshal.ReleaseComObject(objApp);
                    Marshal.FinalReleaseComObject(objApp);
                    objApp = null;
                }
            }
        }

    }

    public class FileHelper
    {


        //获取word文件的页数
        public async Task<(int, string, string)> GetWordPageNum(string wordFile,string pdfFile)
        {
            int pageCount = 0;
            string size = "";
            string direc = "";
            Word.Application wordApp = new Word.Application();
            wordApp.Visible = false;
            await Task.Run(() =>
            {
                Document doc = wordApp.Documents.Open(wordFile);
            pageCount = doc.ComputeStatistics(WdStatistic.wdStatisticPages);
              //  doc.ExportAsFixedFormat(pdfFile, WdExportFormat.wdExportFormatPDF);
                size =doc.PageSetup.PaperSize.ToString();
                if (doc.PageSetup.PageWidth> doc.PageSetup.PageHeight)
                {
                    direc = "横向";
                }
                else
                {
                    direc = "纵向";
                }
                
                //Console.WriteLine($"Word 文件 '{wordFile}' 的页数为：{pageCount}");                
            doc.Close();
            });
            wordApp.Quit();               
            return (pageCount, size, direc);    
        }
  
        //获取Excel文件的页数
        public async Task<(int,string,string)> GetExcelPageNum(Excel.Application excelApp,string excelFile)
        {
            int pageCount = 0;
            await Task.Run(() =>
            {
               
                Workbook workbook = excelApp.Workbooks.Open(excelFile);
                Worksheet worksheet = workbook.Sheets[1];
                pageCount = worksheet.PageSetup.Pages.Count;
                Console.WriteLine($"Excel 文件 '{excelFile}' 的页数为：{pageCount}");

                if (workbook != null)
                {
                    workbook.Close(SaveChanges: false);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                }
                
               // excelApp.Quit();
            });
            return (pageCount,"","");

        }
        //获取PPT文件的页数
        public async Task<int> GetPPTPageNum(PowerPoint.Application pptApp,string pptFile)
        {
            int pageCount = 0;
            await Task.Run(() =>
            {
              
                Presentation ppt = pptApp.Presentations.Open(pptFile, MsoTriState.msoFalse, MsoTriState.msoFalse, MsoTriState.msoFalse);
                pageCount = ppt.Slides.Count;
                Console.WriteLine($"PPT 文件 '{pptFile}' 的页数为：{pageCount}");
               
                if (ppt != null)
                {
                    ppt.Close();
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(ppt);
                }
                // pptApp.Quit();
            });
            return pageCount;

        }
    

        //获取PDF文件的页数
        public async Task<(int, string, string)> GetPDFPageNum(Spire.Pdf.PdfDocument doc,string pdfFile)
        {
            int pageCount = 0;
            string size = "";
            string direc = "";
            await Task.Run(() =>
            {
               
                doc.LoadFromFile(pdfFile);
                pageCount = doc.Pages.Count;
                size = doc.Pages[0].Size.ToString();

                if (doc.Pages[0].MediaBox.Width > doc.Pages[0].MediaBox.Height)
                {
                    direc = "横向";
                }
                else
                {
                    direc = "纵向";
                }
                Console.WriteLine($"PDF 文件 '{size}' 的size为：{size}");
                doc.Close();
            });
            return (pageCount, size, direc);

        }



        public string  MakePhoto(string path,string filePath2, string data ,string logoPath)
        {
            Bitmap bitmapPic = new Bitmap(1051, 1500);// 新建一个Bitmap 位图 
            bitmapPic.SetResolution(300, 300);
            Graphics g = Graphics.FromImage(bitmapPic);// 根据新建的 Bitmap 位图，创建画布
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.Clear(Color.White);// 使用白色重置画布

           
            Image img = Image.FromFile(path);//图片路径
            if (logoPath != "")
            {

                Image imgLogo = Image.FromFile(logoPath);//图片路径
                g.DrawImage(imgLogo, 8, 8, 285, 75);
            }

            if (img.Height==413)
            {
               // System.Windows.MessageBox.Show("请放照片" +"一寸");
                g.DrawImage(img, 30, 70, 319, 437);
                g.DrawImage(img, 353, 70, 319, 437);
                g.DrawImage(img, 673, 70, 319, 437);
                g.DrawImage(img, 30, 514, 319, 437);
                g.DrawImage(img, 353, 514, 319, 437);
                g.DrawImage(img, 673, 514, 319, 437);
                g.DrawImage(img, 30, 952, 319, 437);
                g.DrawImage(img, 353, 952, 319, 437);
                g.DrawImage(img, 673, 952, 319, 437);

            }
            else
            {
               // System.Windows.MessageBox.Show("请放照片" + "二寸");
                g.DrawImage(img, 73, 129, 437, 591);
                g.DrawImage(img, 522, 129, 437, 591);
                g.DrawImage(img, 73, 742, 437, 591);
                g.DrawImage(img, 522, 742, 437, 591);


            }

            
            //在背景照片上添加文字  
            PointF drawPoint = new PointF(40F, 1400F);
            AddFont(g, drawPoint, data);
            string newname = filePath2 + "\\1" + ".jpeg";
            bitmapPic.Save(newname, System.Drawing.Imaging.ImageFormat.Jpeg);
            return newname;


        }


        private void AddFont(Graphics grap, PointF drawPoint, string data)
        {
            SolidBrush mybrush = new SolidBrush(Color.Black);    //设置默认画笔颜色
            FontFamily fontFamily = new FontFamily("宋体");
            System.Drawing.Font myfont = new System.Drawing.Font(fontFamily, 9, System.Drawing.FontStyle.Regular);   //设置默认字体格式   
            grap.DrawString(data, myfont, mybrush, drawPoint);  //图片上添加文字;
        }

        public async Task<string> MakeCover(string filePath, string spine,int coverId)
        {
            // 当前行关联的数据
            // int rowIndex = grid.SelectedRows[0].Index;
            //Param tag = (Param)grid.Rows[rowIndex].Tag;
            string directoryName = System.IO.Path.GetDirectoryName(filePath);
            string coverPath = directoryName + "\\封面"+ coverId + ".pdf";

            await System.Threading.Tasks.Task.Run(() =>
            {

                Spire.Pdf.PdfDocument pdf = new Spire.Pdf.PdfDocument();
                pdf.LoadFromFile(filePath);
                Spire.Pdf.PdfDocument pdf1 = new Spire.Pdf.PdfDocument();
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
                    PdfTrueTypeFont trueTypeFont = new PdfTrueTypeFont(new System.Drawing.Font("仿宋", 11f), true);
                    int spineLength;
                    spineLength = spine.Length;
                    int a = 600 / spineLength;
                    PdfTemplate template = new PdfTemplate(649, 626);
                    template.Graphics.TranslateTransform(649, 100);
                    template.Graphics.RotateTransform(90);
                    for (int i = 0; i < spineLength; i++)
                    {
                        int X = 629;
                        if (spine[i] < 0x4E00 || spine[i] > 0x9FA5) //汉字
                        {
                            X = 626;

                            template.Graphics.DrawString(spine[i].ToString(), trueTypeFont, PdfBrushes.Black, new PointF(i * a, 0));
                            page.Canvas.DrawTemplate(template, new PointF(0, 0));
                        }
                        else
                        {
                            page.Canvas.DrawString(spine[i].ToString(), trueTypeFont, PdfBrushes.Black, new PointF(636, 100 + i * a));
                        }

                    }

                    //        PdfTemplate template = new PdfTemplate(629, 450);
                    //        template.Graphics.TranslateTransform(629, 10);
                    //        template.Graphics.RotateTransform(90);
                    //template.Graphics.DrawString(spine, trueTypeFont, PdfBrushes.Black, 0, 0);
                    //page.Canvas.DrawTemplate(template, new PointF(0, 0));



                }

                pdf1.SaveToFile(coverPath);
            });



            //保存文件返回路径 

            return coverPath;
        }


        //提取固定页数
        public async void Extract_PDFPageNum(string pdfFile, int[] pagesnum)
        {

            await Task.Run(() =>
            {
                Spire.Pdf.PdfDocument doc = new Spire.Pdf.PdfDocument();
                doc.LoadFromFile(pdfFile);
                if (doc.Pages.Count != 26)
                {
                    Console.WriteLine(pdfFile);
                    return;

                }
                foreach (var page in pagesnum)
                {
                   
                    doc.Pages.RemoveAt(page);

                }
                doc.SaveToFile(pdfFile);


                doc.Close();
            });
           

        }

        public async void Extract_PDFCove(string pdfFile)
        {

            await Task.Run(() =>
            {
                
            });


        }


    }

    
    public class PrintParamSet
    {



       

    }








    class DuplexSettings
    {
        #region Win32 API Declaration


        [DllImport("kernel32.dll", EntryPoint = "GetLastError", SetLastError = false, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 GetLastError();


        [DllImport("winspool.Drv", EntryPoint = "ClosePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool ClosePrinter(IntPtr hPrinter);


        [DllImport("winspool.Drv", EntryPoint = "DocumentPropertiesA", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern int DocumentProperties(IntPtr hwnd, IntPtr hPrinter, [MarshalAs(UnmanagedType.LPStr)]
string pDeviceNameg, IntPtr pDevModeOutput, IntPtr pDevModeInput, int fMode);


        [DllImport("winspool.Drv", EntryPoint = "GetPrinterA", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool GetPrinter(IntPtr hPrinter, Int32 dwLevel, IntPtr pPrinter, Int32 dwBuf, ref Int32 dwNeeded);


        [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        static extern int OpenPrinter(string pPrinterName, out IntPtr phPrinter, ref PRINTER_DEFAULTS pDefault);


        [DllImport("winspool.Drv", EntryPoint = "SetPrinterA", ExactSpelling = true, SetLastError = true)]
        public static extern bool SetPrinter(IntPtr hPrinter, int Level, IntPtr pPrinter, int Command);


        [StructLayout(LayoutKind.Sequential)]
        public struct PRINTER_DEFAULTS
        {
            public IntPtr pDatatype;
            public IntPtr pDevMode;
            public int DesiredAccess;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct PRINTER_INFO_9
        {

            public IntPtr pDevMode;
            // Pointer to SECURITY_DESCRIPTOR
            public int pSecurityDescriptor;
        }

        public const short CCDEVICENAME = 32;

        public const short CCFORMNAME = 32;

        [StructLayout(LayoutKind.Sequential)]
        public struct DEVMODE
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = CCDEVICENAME)]
            public string dmDeviceName;
            public short dmSpecVersion;
            public short dmDriverVersion;
            public short dmSize;
            public short dmDriverExtra;
            public int dmFields;
            public short dmOrientation;
            public short dmPaperSize;
            public short dmPaperLength;
            public short dmPaperWidth;
            public short dmScale;
            public short dmCopies;
            public short dmDefaultSource;
            public short dmPrintQuality;
            public short dmColor;
            public short dmDuplex;
            public short dmYResolution;
            public short dmTTOption;
            public short dmCollate;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = CCFORMNAME)]
            public string dmFormName;
            public short dmUnusedPadding;
            public short dmBitsPerPel;
            public int dmPelsWidth;
            public int dmPelsHeight;
            public int dmDisplayFlags;
            public int dmDisplayFrequency;
        }


        public const Int64 DM_DUPLEX = 0x1000L;
        public const Int64 DM_ORIENTATION = 0x1L;
        public const Int64 DM_SCALE = 0x10L;
        public const Int64 DMORIENT_PORTRAIT = 0x1L;
        public const Int64 DMORIENT_LANDSCAPE = 0x2L;
        public const Int32 DM_MODIFY = 8;
        public const Int32 DM_COPY = 2;
        public const Int32 DM_IN_BUFFER = 8;
        public const Int32 DM_OUT_BUFFER = 2;
        public const Int32 PRINTER_ACCESS_ADMINISTER = 0x4;
        public const Int32 PRINTER_ACCESS_USE = 0x8;
        public const Int32 STANDARD_RIGHTS_REQUIRED = 0xf0000;
        public const int PRINTER_ALL_ACCESS = (STANDARD_RIGHTS_REQUIRED | PRINTER_ACCESS_ADMINISTER | PRINTER_ACCESS_USE);
        //added this 
        public const int CCHDEVICENAME = 32;
        //added this 
        public const int CCHFORMNAME = 32;

        #endregion

        #region Public Methods


        /// <summary>
        /// Method Name : GetPrinterDuplex 
        /// Programmatically get the Duplex flag for the specified printer 
        /// driver's default properties. 
        /// </summary>
        /// <param name="sPrinterName"> The name of the printer to be used. </param>
        /// <param name="errorMessage"> this will contain error messsage if any. </param>
        /// <returns> 
        /// nDuplexSetting - One of the following standard settings: 
        /// 0 = Error
        /// 1 = None (Simplex)
        /// 2 = Duplex on long edge (book) 
        /// 3 = Duplex on short edge (legal) 
        /// </returns>
        /// <remarks>
        /// </remarks>
        public short GetPrinterDuplex(string sPrinterName, out string errorMessage)
        {
            errorMessage = string.Empty;
            short functionReturnValue = 0;
            IntPtr hPrinter = default(IntPtr);
            PRINTER_DEFAULTS pd = default(PRINTER_DEFAULTS);
            DEVMODE dm = new DEVMODE();
            int nRet = 0;
            pd.DesiredAccess = PRINTER_ACCESS_USE;
            nRet = OpenPrinter(sPrinterName, out hPrinter, ref pd);
            if ((nRet == 0) | (hPrinter.ToInt32() == 0))
            {
                if (GetLastError() == 5)
                {
                    errorMessage = "Access denied -- See the article for more info.";
                }
                else
                {
                    errorMessage = "Cannot open the printer specified " + "(make sure the printer name is correct).";
                }
                return functionReturnValue;
            }
            nRet = DocumentProperties(IntPtr.Zero, hPrinter, sPrinterName, IntPtr.Zero, IntPtr.Zero, 0);
            if ((nRet < 0))
            {
                errorMessage = "Cannot get the size of the DEVMODE structure.";
                goto cleanup;
            }
            IntPtr iparg = Marshal.AllocCoTaskMem(nRet + 100);
            nRet = DocumentProperties(IntPtr.Zero, hPrinter, sPrinterName, iparg, IntPtr.Zero, DM_OUT_BUFFER);
            if ((nRet < 0))
            {
                errorMessage = "Cannot get the DEVMODE structure.";
                goto cleanup;
            }
            dm = (DEVMODE)Marshal.PtrToStructure(iparg, dm.GetType());
            if (!Convert.ToBoolean(dm.dmFields & DM_DUPLEX))
            {
                errorMessage = "You cannot modify the duplex flag for this printer " + "because it does not support duplex or the driver " + "does not support setting it from the Windows API.";
                goto cleanup;
            }
            functionReturnValue = dm.dmDuplex;

        cleanup:
            if ((hPrinter.ToInt32() != 0))
                ClosePrinter(hPrinter);
            return functionReturnValue;
        }


        /// <summary>
        /// Method Name : SetPrinterDuplex     
        /// Programmatically set the Duplex flag for the specified printer driver's default properties. 
        /// </summary>
        /// <param name="sPrinterName"> sPrinterName - The name of the printer to be used. </param>
        /// <param name="nDuplexSetting"> 
        /// nDuplexSetting - One of the following standard settings: 
        /// 1 = None 
        /// 2 = Duplex on long edge (book) 
        /// 3 = Duplex on short edge (legal) 
        /// </param>
        ///  <param name="errorMessage"> this will contain error messsage if any. </param>
        /// <returns>
        /// Returns: True on success, False on error.
        /// </returns>
        /// <remarks>
        /// 
        /// </remarks>
        public bool SetPrinterDuplex(string sPrinterName, int nDuplexSetting, out string errorMessage)
        {
            errorMessage = string.Empty;
            bool functionReturnValue = false;
            IntPtr hPrinter = default(IntPtr);
            PRINTER_DEFAULTS pd = default(PRINTER_DEFAULTS);
            PRINTER_INFO_9 pinfo = new PRINTER_INFO_9();
            DEVMODE dm = new DEVMODE();
            IntPtr ptrPrinterInfo = default(IntPtr);
            int nBytesNeeded = 0;
            int nRet = 0;
            Int32 nJunk = default(Int32);
            if ((nDuplexSetting < 1) | (nDuplexSetting > 3))
            {
                errorMessage = "Error: dwDuplexSetting is incorrect.";
                return functionReturnValue;
            }
            pd.DesiredAccess = PRINTER_ACCESS_USE;
            nRet = OpenPrinter(sPrinterName, out hPrinter, ref pd);
            if ((nRet == 0) | (hPrinter.ToInt32() == 0))
            {
                if (GetLastError() == 5)
                {
                    errorMessage = "Access denied -- See the article for more info.";
                }
                else
                {
                    errorMessage = "Cannot open the printer specified " + "(make sure the printer name is correct).";
                }
                return functionReturnValue;
            }
            nRet = DocumentProperties(IntPtr.Zero, hPrinter, sPrinterName, IntPtr.Zero, IntPtr.Zero, 0);
            if ((nRet < 0))
            {
                errorMessage = "Cannot get the size of the DEVMODE structure.";
                goto cleanup;
            }
            IntPtr iparg = Marshal.AllocCoTaskMem(nRet + 100);
            nRet = DocumentProperties(IntPtr.Zero, hPrinter, sPrinterName, iparg, IntPtr.Zero, DM_OUT_BUFFER);
            if ((nRet < 0))
            {
                errorMessage = "Cannot get the DEVMODE structure.";
                goto cleanup;
            }
            dm = (DEVMODE)Marshal.PtrToStructure(iparg, dm.GetType());
            if (!Convert.ToBoolean(dm.dmFields & DM_DUPLEX))
            {
                errorMessage = "You cannot modify the duplex flag for this printer " + "because it does not support duplex or the driver " + "does not support setting it from the Windows API.";
                goto cleanup;
            }
            dm.dmDuplex = (short)nDuplexSetting;
            Marshal.StructureToPtr(dm, iparg, true);
            nRet = DocumentProperties(IntPtr.Zero, hPrinter, sPrinterName, pinfo.pDevMode, pinfo.pDevMode, DM_IN_BUFFER | DM_OUT_BUFFER);
            if ((nRet < 0))
            {
                errorMessage = "Unable to set duplex setting to this printer.";
                goto cleanup;
            }
            GetPrinter(hPrinter, 9, IntPtr.Zero, 0, ref nBytesNeeded);
            if ((nBytesNeeded == 0))
            {
                errorMessage = "GetPrinter failed.";
                goto cleanup;
            }
            ptrPrinterInfo = Marshal.AllocCoTaskMem(nBytesNeeded + 100);
            nRet = GetPrinter(hPrinter, 9, ptrPrinterInfo, nBytesNeeded, ref nJunk) ? 1 : 0;
            if ((nRet == 0))
            {
                errorMessage = "Unable to get shared printer settings.";
                goto cleanup;
            }
            pinfo = (PRINTER_INFO_9)Marshal.PtrToStructure(ptrPrinterInfo, pinfo.GetType());
            pinfo.pDevMode = iparg;
            pinfo.pSecurityDescriptor = 0;
            Marshal.StructureToPtr(pinfo, ptrPrinterInfo, true);
            nRet = SetPrinter(hPrinter, 9, ptrPrinterInfo, 0) ? 1 : 0;
            if ((nRet == 0))
            {
                errorMessage = "Unable to set shared printer settings.";
            }
            functionReturnValue = Convert.ToBoolean(nRet);
        cleanup:
            if ((hPrinter.ToInt32() != 0))
                ClosePrinter(hPrinter);
            return functionReturnValue;
        }
        #endregion

    }

}


