using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace TuwenDayinDian.Components
{
    /// <summary>
    /// PriceListDataGrid.xaml 的交互逻辑
    /// </summary>
    public partial class PriceListDataGrid : UserControl
    {
        public static ObservableCollection<Pricing> PricingList { get; set; } = new ObservableCollection<Pricing>();

        public static double TotalPrice { get; private set; }


        public PriceListDataGrid()
        {
            InitializeComponent();

            // 添加监听器以便在PrintTasks集合更改时重新计算PricingList
            TaskListDataGrid.PrintTasks.CollectionChanged += PrintTasks_CollectionChanged;
            this.Loaded += PriceListDataGrid_Loaded;
        }

        private void PriceListDataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            PriceListView.ItemsSource = PricingList;
        }

        private void PrintTasks_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            // 如果有新项目添加到PrintTasks，则为每个新项目添加PropertyChanged监听器
            if (e.NewItems != null)
            {
                foreach (PrintTask newTask in e.NewItems)
                {
                    newTask.PropertyChanged += PrintTask_PropertyChanged;
                }
            }

            // 如果PrintTasks中的项目已被移除，则从每个已移除的项目中删除PropertyChanged监听器
            if (e.OldItems != null)
            {
                foreach (PrintTask oldTask in e.OldItems)
                {
                    oldTask.PropertyChanged -= PrintTask_PropertyChanged;
                }
            }

            // UpdatePricingList();
        }

        public static void UpdatePricingList()
        {
            if ( TaskListDataGrid.PrintTasks .Any())
            {
                
                var groupedTasks = TaskListDataGrid.PrintTasks
                    .GroupBy(t => new
                    {
                        Size = t.大小,
                        DoubleSided = t.双面.Contains("单面") ? "单面" : "双面"

                    });

                PricingList.Clear();
              
                foreach (var group in groupedTasks)
                {
                    Pricing pricing = new Pricing
                    {
                        Name = group.Key.Size + group.Key.DoubleSided,
                        PageCount = group.Sum(t => t.页数),
                        UnitPrice = 0.2,
                        Copies = 1,
                        //BookCount = ,
                        
                    };

                    PricingList.Add(pricing);
                }
            }
        }


        private void PrintTask_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // 当PrintTask的任何属性发生变化时更新PricingList
            UpdatePricingList();
        }


        private void BindingMethodCell_Click(object sender, MouseButtonEventArgs e)
        {
            if (PriceListView.SelectedItem is Pricing selectedPricing)
            {
                var selectionWindow = new BindingMethodSelectionWindow();
               // selectionWindow.Owner = this.Parent as Window;
                selectionWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;  

                if (selectionWindow.ShowDialog() == true)
                {
                    // 获取所有选中的装订方式并连接它们
                    var selectedMethods = selectionWindow.BindingMethodListBox.SelectedItems.Cast<string>();
                    string combinedMethods = string.Join(" | ", selectedMethods);

                    selectedPricing.BindingMethod = combinedMethods;

                    // 如果ObservableCollection<Pricing>不会自动更新UI，您可能需要手动更新
                   // PriceListView.Items.Refresh();
                }
            }
        }

        /// <summary>
        /// 计算总价
        /// </summary>
        public static string UpdateTotalPrice()
        {
            
            TotalPrice = 0;
            
            foreach (var pricing in PricingList)
            {
               
                TotalPrice += (pricing.PageCount * pricing.UnitPrice* pricing.Copies) + (pricing.Copies * pricing.BindingUnitPrice* pricing.BookCount);
            }             
            return TotalPrice.ToString();
        }

        private void PriceListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

    }
}
