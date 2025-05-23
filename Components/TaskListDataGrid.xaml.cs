using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
using System.Xml.Linq;
using TuwenDayinDian.Helpers;
using TuwenDayinDian.Models;
using Word;
using Excel;
using PowerPoint;
using Application = Word.Application;
using System.Collections;
using System.Runtime.InteropServices;
using Google.Protobuf.Collections;
using Spire.Pdf;

namespace TuwenDayinDian.Components
{
    /// <summary>
    /// TaskListDataGrid.xaml 的交互逻辑
    /// </summary>
    /// 
    public partial class TaskListDataGrid : UserControl
    {
        private PrintTask _draggedItem;
        private System.Windows.Point? _startPoint;

        public static ObservableCollection<PrintTask> PrintTasks { get; set; } = new ObservableCollection<PrintTask>();

       

        public TaskListDataGrid()
        {
            InitializeComponent();

            // 创建测试数据
            //for (int i = 1; i <= 10; i++)
            //{
            //    PrintTasks.Add(new PrintTask
            //    {
            //        序号 = i,
            //        文件名称 = $"文件{i}",
            //        页数 = i * 10,
            //        大小 = $"{i * 10}MB",
            //        方向 = i % 2 == 0 ? "横向" : "纵向",
            //        份数 = i,
            //        状态 = "待打印",
            //        双面 = i % 2 == 0 ? "是" : "否",
            //        缩放 = "100%",
            //        打印机 = $"打印机{i}",
            //        名称 = $"任务{i}",
            //        打印状态 = "等待"
            //    });
            //}

            // 将数据绑定到TaskListView
            TaskListView.ItemsSource = PrintTasks;

        }


        //  private PrintTask _draggedItem;

        private void TaskListView_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var source = e.OriginalSource as FrameworkElement;
                if (source != null && source.GetType() == typeof(TextBlock))
                {
                    // 获取被点击的按钮
                    TextBlock bt = (TextBlock)source;
                    if (bt.Text == "x")
                    {
                        PrintTasks.RemoveAt((int)bt.Tag - 1);
                    }
                }
            }
            catch (Exception)
            {

            }
            try
            {
                var row = GetDataGridRowUnderMouse(TaskListView, e);
                if (row != null)
                {
                    _draggedItem = row.DataContext as PrintTask;
                    DragDrop.DoDragDrop(row, _draggedItem, DragDropEffects.Move);
                }
            }
            catch (Exception)
            {

                // throw;
            }
        }

        private void TaskListView_Drop(object sender, DragEventArgs e)
        {
            if (_draggedItem == null)
                return;

            var droppedOn = GetDataGridRowUnderMouse(TaskListView, e)?.DataContext as PrintTask;

            if (droppedOn == null || ReferenceEquals(_draggedItem, droppedOn))
                return;

            var items = TaskListView.ItemsSource as ObservableCollection<PrintTask>;

            int oldIndex = items.IndexOf(_draggedItem);
            int newIndex = items.IndexOf(droppedOn);

            items.RemoveAt(oldIndex);
            items.Insert(newIndex, _draggedItem);

            // 更新序号
            UpdateTaskSequences();
        }

        private DataGridRow GetDataGridRowUnderMouse(DataGrid grid, RoutedEventArgs e)
        {
            try
            {
                var element = e.OriginalSource as UIElement;
                while (element != null && !(element is DataGridRow))
                {
                    element = VisualTreeHelper.GetParent(element) as UIElement;
                }
                return element as DataGridRow;

            }
            catch (Exception)
            {
                return null;
            }
        }


        private void TaskListView_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, true))
            {
                string[] filenames = e.Data.GetData(DataFormats.FileDrop, true) as string[];

                if (filenames != null && filenames.Length == 1 && Directory.Exists(filenames[0]))
                {
                    e.Effects = DragDropEffects.Copy;
                }
                else
                {
                    e.Effects = DragDropEffects.None;
                }
            }
            e.Handled = true;
        }


        //private async void TaskListView_DropOnDataGrid(object sender, DragEventArgs e)
        //{

        //    List<string> fileList = new List<string>();
        //    if (e.Data.GetDataPresent(DataFormats.FileDrop, true))
        //    {
        //        string[] filenames = e.Data.GetData(DataFormats.FileDrop, true) as string[];


        //        foreach (string file in filenames)
        //        {
        //            if (File.GetAttributes(file).HasFlag(FileAttributes.Directory))
        //            {
        //                string[] allFiles = Directory.GetFiles(file, "*.*", SearchOption.AllDirectories);
        //                foreach (string f in allFiles)
        //                {
        //                    fileList.Add(f);

        //                }
        //            }
        //            else
        //            {

        //                fileList.Add(file);
        //            }

        //        }
        //    }

        //    foreach(string fi in fileList)
        //    {
        //        PrintTasks.Add(new PrintTask
        //        {
        //            序号 = PrintTasks.Count + 1, // 新项目的序号应为列表的当前计数+1
        //            文件名称 = System.IO.Path.GetFileName(fi),
        //            文件路径 = System.IO.Path.GetFullPath(fi),
        //            图标 = System.IO.Path.GetFileName(fi),
        //            页数 = 3,
        //            大小 = "A4",
        //            方向 = "纵向",
        //            份数 = 1,
        //            状态 = "待打印",
        //            双面 = "双面",
        //            缩放 = "100%",
        //            打印机 = $"打印机",
        //            打印状态 = "等待"
        //        });
        //    }




        //}




        private async void TaskListView_DropOnDataGrid(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, true))
            {
                string[] filenames = e.Data.GetData(DataFormats.FileDrop, true) as string[];

                if (filenames != null && filenames.Length == 1 && Directory.Exists(filenames[0]))
                {

                   
                    Excel.Application excelApp = new Excel.Application();
                    excelApp.Visible = false;
                    PowerPoint.Application pptApp = new PowerPoint.Application();
                    //pptApp.Visible = false;
                    PdfDocument doc = new PdfDocument();



                    foreach (var file in Directory.EnumerateFiles(filenames[0]))
                    {

                        //判断file文件名前是否包含~$，如果包含则跳过
                        if (System.IO.Path.GetFileName(file).StartsWith(file))
                        {
                            MessageBox.Show("文件名不合法，请重新选择文件");
                            continue;

                        }
                        

                        FileInfo fi = new FileInfo(file);
                        string ext = fi.Extension;
                        if (ext != ".pdf" && ext != ".docx" && ext != ".doc" && ext != ".xls" && ext != ".xlsx" && ext != ".ppt" && ext != ".ppt" && ext != ".jpg" && ext != ".jpeg" && ext != ".png")
                        {
                            continue;
                        }

                        var (num, size, direc) = (0,"","");
                        FileHelper filehelper = new FileHelper();
                        //判断文件类型
                        if (fi.Extension == ".pdf")
                        {
                            (num, size, direc) = await filehelper.GetPDFPageNum(doc,file);
                        }
                        if (fi.Extension == ".doc" || fi.Extension == ".docx")
                        {

                            //(num, size, direc) = await filehelper.GetWordPageNum( file);
                           // Console.WriteLine("num"+ num);

                        }
                        if (fi.Extension == ".xls" || fi.Extension == ".xlsx")
                        {
                            (num, size, direc) = await filehelper.GetExcelPageNum(excelApp, file);
                        }
                        if (fi.Extension == ".ppt" || fi.Extension == ".pptx")
                        {
                            num = await filehelper.GetPPTPageNum(pptApp, file);
                        }



                        PrintTasks.Add(new PrintTask
                        {
                            序号 = PrintTasks.Count + 1, // 新项目的序号应为列表的当前计数+1
                            文件名称 = System.IO.Path.GetFileName(file),
                            文件路径 = System.IO.Path.GetFullPath(file),
                            图标 = System.IO.Path.GetFileName(file),
                            页数 = (num, size, direc).num,
                            大小 = (num, size, direc).size,
                            方向 = (num, size, direc).direc,
                            份数 = 1,
                            状态 = "待打印",
                            双面 = "双面",
                            缩放 = "100%",
                            打印机 = $"打印机",
                            打印状态 = "等待"
                        });
                    }
                    //wordApp.Quit();
                  
                    //wordApp.Quit();
                    excelApp.Quit();
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
                    pptApp.Quit();
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(pptApp);
                }
            }
        }




        /// <summary>
        /// 修改序号
        /// </summary>
        private void UpdateTaskSequences()
        {
            int index = 1; // 序号从1开始
            foreach (var task in PrintTasks)
            {
                task.序号 = index++;
            }
        }

        private void TaskListView_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            PrintTask item = e.Row.Item as PrintTask;
            if (item != null)
            {
                DataGridComboBoxColumn printerColumn = TaskListView.Columns
                    .FirstOrDefault(col => col.Header.ToString() == "打印机名称") as DataGridComboBoxColumn;

                if (printerColumn != null && printerColumn.ItemsSource is IList<string> printerList && printerList.Count > 0)
                {
                    item.打印机 = printerList[0];
                }

                // 大小设置
                DataGridComboBoxColumn sizeColumn = TaskListView.Columns
                    .FirstOrDefault(col => col.Header.ToString() == "大小") as DataGridComboBoxColumn;
                if (sizeColumn != null && sizeColumn.ItemsSource is IList<string> sizeList && sizeList.Count > 0)
                {
                    item.大小 = sizeList[0];
                }

                // 方向设置
                DataGridComboBoxColumn directionColumn = TaskListView.Columns.FirstOrDefault(col => col.Header.ToString() == "方向") as DataGridComboBoxColumn;
                if (directionColumn != null && directionColumn.ItemsSource is IList<string> directionList && directionList.Count > 0)
                {
                    item.方向 = directionList[0];
                }

                // 双面设置
                DataGridComboBoxColumn duplexColumn = TaskListView.Columns
                    .FirstOrDefault(col => col.Header.ToString() == "双面") as DataGridComboBoxColumn;
                if (duplexColumn != null && duplexColumn.ItemsSource is IList<string> duplexList && duplexList.Count > 0)
                {
                    item.双面 = duplexList[0];
                }
            }

            PriceListDataGrid.UpdatePricingList();
        }

        private void OuterGrid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //  TaskListView.Height = OuterGrid.Height;
        }

       
    }
}
