using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TuwenDayinDian.Models
{
    //public class AddBillIPrice : INotifyPropertyChanged
    //{
    //    private string _unitPrice;

    //    public event PropertyChangedEventHandler PropertyChanged;

    //    public string UnitPrice
    //    {
    //        get => _unitPrice;
    //        set
    //        {
    //            _unitPrice = value;
    //            OnPropertyChanged();
    //        }
    //    }

    //    public AddBillIPrice()
    //    {
    //        // 初始化属性值
    //        UnitPrice = "0.00";
    //    }

    //    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    //    {
    //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    //    }
    //}



    //public class AddBillIPrice : INotifyPropertyChanged
    //{



    //    private string _textBoxContent;

    //    private string _colorRadiotext;
    //    private string _duplexRadiotext;


    //    private string _pageComboBoxItem;
    //    private string _coverComboBoxItem;
    //    private string _bindingComboBoxItem;
    //    private string _penhuibuComboBoxItem;
    //    private string _acrylicComboBoxItem;
    //    private string _PVCComboBoxItem;
    //    private string _selectedListBoxItem;





    //    public event PropertyChangedEventHandler PropertyChanged;

    //    public string TextBoxContent
    //    {
    //        get => _textBoxContent;
    //        set
    //        {
    //            if (_textBoxContent != value)
    //            {
    //                _textBoxContent = value;
    //                OnPropertyChanged();
    //            }
    //        }
    //    }

    //    public string ColorRadiotext
    //    {
    //        get => _colorRadiotext;
    //        set
    //        {
    //            if (_colorRadiotext != value)
    //            {
    //                _colorRadiotext = value;
    //                OnPropertyChanged();
    //                UpdateTextBoxContent();
    //            }
    //        }
    //    }

    //    public string DuplexRadiotext
    //    {
    //        get => _duplexRadiotext;
    //        set
    //        {
    //            if (_duplexRadiotext != value)
    //            {
    //                _duplexRadiotext = value;
    //                OnPropertyChanged();
    //                UpdateTextBoxContent();
    //            }
    //        }
    //    }

    //    public string PageComboBoxItem
    //    {
    //        get => _pageComboBoxItem;
    //        set
    //        {
    //            if (_pageComboBoxItem != value)
    //            {
    //                _pageComboBoxItem = value;
    //                OnPropertyChanged();
    //                UpdateTextBoxContent();
    //            }
    //        }
    //    }

    //    public string CoverComboBoxItem
    //    {
    //        get => _coverComboBoxItem;
    //        set
    //        {
    //            if (_coverComboBoxItem != value)
    //            {
    //                _coverComboBoxItem = value;
    //                OnPropertyChanged();
    //                UpdateTextBoxContent();
    //            }
    //        }
    //    }

    //    public string BindingComboBoxItem
    //    {
    //        get => _bindingComboBoxItem;
    //        set
    //        {
    //            if (_bindingComboBoxItem != value)
    //            {
    //                _bindingComboBoxItem = value;
    //                OnPropertyChanged();
    //                UpdateTextBoxContent();
    //            }
    //        }
    //    }

    //    public string PenhuibuComboBoxItem
    //    {
    //        get => _penhuibuComboBoxItem;
    //        set
    //        {
    //            if (_penhuibuComboBoxItem != value)
    //            {
    //                _penhuibuComboBoxItem = value;
    //                OnPropertyChanged();
    //                UpdateTextBoxContent();
    //            }
    //        }
    //    }

    //    public string AcrylicComboBoxItem
    //    {
    //        get => _acrylicComboBoxItem;
    //        set
    //        {
    //            if (_acrylicComboBoxItem != value)
    //            {
    //                _acrylicComboBoxItem = value;
    //                OnPropertyChanged();
    //                UpdateTextBoxContent();
    //            }
    //        }
    //    }

    //    public string PVCComboBoxItem
    //    {
    //        get => _PVCComboBoxItem;
    //        set
    //        {
    //            if (_PVCComboBoxItem != value)
    //            {
    //                _PVCComboBoxItem = value;
    //                OnPropertyChanged();
    //                UpdateTextBoxContent();
    //            }
    //        }
    //    }


    //    public string SelectedListBoxItem
    //    {
    //        get => _selectedListBoxItem;
    //        set
    //        {
    //            if (_selectedListBoxItem != value)
    //            {
    //                _selectedListBoxItem = value;
    //                OnPropertyChanged();
    //                UpdateTextBoxContent();
    //            }
    //        }
    //    }

    //    private void UpdateTextBoxContent()
    //    {
    //        TextBoxContent = $"RadioButton: {ColorRadiotext}, ComboBox: {PageComboBoxItem}, ListBox: {SelectedListBoxItem}";
    //    }

    //    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    //    {
    //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    //    }
    //}



    internal class PriceList
    {
        public int A4BackPrice=20;
        public int A3BackPrice = 20;
        public int A4ColorPrice = 100;
        public int A3ColorPrice = 100;
        public int PerfectBinding = 300;
        public int SaddleStitching = 50;
        public int CoilBinding = 800;
        public int TapeBinding = 800;
        
        //public int page = 10;
        //public int page = 10;
        //public int page = 10;
        //public int page = 10;
        //public int page = 10;
    }

    public class PrintpicesModel
    {
        
        public string JobType { get; set; }
        public string JobName { get; set; }
        public Decimal L1 { get; set; }
        public Decimal L2 { get; set; }
        public Decimal L3 { get; set; }
        public Decimal L4 { get; set; }
        public Decimal L5 { get; set; }
        public Decimal L6 { get; set; }
        public Decimal L7 { get; set; }

        // 添加其他需要的字段
    }




}
