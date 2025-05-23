using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuwenDayinDian.Models
{
    public class Pricing : INotifyPropertyChanged
    {
        private string _name;
        private int _bookCount;
        private int _pageCount;
        private double _unitPrice;
        private int _copies;
        private string _bindingMethod;
        private double _bindingUnitPrice;

        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }
        public int BookCount
        {
            get { return _bookCount; }
            set
            {
                if (_bookCount != value)
                {
                    _bookCount = value;
                    OnPropertyChanged(nameof(BookCount));
                }
            }
        }

        public int PageCount
        {
            get { return _pageCount; }
            set
            {
                if (_pageCount != value)
                {
                    _pageCount = value;
                    OnPropertyChanged(nameof(PageCount));
                }
            }
        }

        public double UnitPrice
        {
            get { return _unitPrice; }
            set
            {
                if (_unitPrice != value)
                {
                    _unitPrice = value;
                    OnPropertyChanged(nameof(UnitPrice));
                }
            }
        }

        public int Copies
        {
            get { return _copies; }
            set
            {
                if (_copies != value)
                {
                    _copies = value;
                    OnPropertyChanged(nameof(Copies));
                }
            }
        }

        public string BindingMethod
        {
            get { return _bindingMethod; }
            set
            {
                if (_bindingMethod != value)
                {
                    _bindingMethod = value;
                    OnPropertyChanged(nameof(BindingMethod));
                }
            }
        }

        public double BindingUnitPrice
        {
            get { return _bindingUnitPrice; }
            set
            {
                if (_bindingUnitPrice != value)
                {
                    _bindingUnitPrice = value;
                    OnPropertyChanged(nameof(BindingUnitPrice));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class AddBillInfoPricing : INotifyPropertyChanged
    {
  
        private int _pageCount; 
        private int _copies;      
        private double _sizeUnitPrice;
        private double _printUnitPrice;
        private double _pageUnitPrice;
        private double _coveUnitPrice;
        private double _bindingUnitPrice;
        private double _tecprintUnitPrice;
        private double _penhuibuUnitPrice;
        private double _acrylicUnitPrice;

        public int PageCount
        {
            get { return _pageCount; }
            set
            {
                if (_pageCount != value)
                {
                    _pageCount = value;
                    OnPropertyChanged(nameof(PageCount));
                }
            }
        }

        public double SizeUnitPrice
        {
            get { return _sizeUnitPrice; }
            set
            {
                if (_sizeUnitPrice != value)
                {
                    _sizeUnitPrice = value;
                    OnPropertyChanged(nameof(SizeUnitPrice));
                }
            }
        }

        public int Copies
        {
            get { return _copies; }
            set
            {
                if (_copies != value)
                {
                    _copies = value;
                    OnPropertyChanged(nameof(Copies));
                }
            }
        }

        public double PrintUnitPrice
        {
            get { return _printUnitPrice; }
            set
            {
                if (_printUnitPrice != value)
                {
                    _printUnitPrice = value;
                    OnPropertyChanged(nameof(PrintUnitPrice));
                }
            }
        }

        public double PageUnitPrice
        {
            get { return _pageUnitPrice; }
            set
            {
                if (_pageUnitPrice != value)
                {
                    _pageUnitPrice = value;
                    OnPropertyChanged(nameof(PageUnitPrice));
                }
            }
        }

        public double BindingUnitPrice
        {
            get { return _bindingUnitPrice; }
            set
            {
                if (_bindingUnitPrice != value)
                {
                    _bindingUnitPrice = value;
                    OnPropertyChanged(nameof(BindingUnitPrice));
                }
            }
        }

        public double CoveUnitPrice
        {
            get { return _coveUnitPrice; }
            set
            {
                if (_coveUnitPrice != value)
                {
                    _coveUnitPrice = value;
                    OnPropertyChanged(nameof(CoveUnitPrice));
                }
            }
        }

        public double TecprintUnitPrice
        {
            get { return _tecprintUnitPrice; }
            set
            {
                if (_tecprintUnitPrice != value)
                {
                    _tecprintUnitPrice = value;
                    OnPropertyChanged(nameof(TecprintUnitPrice));
                }
            }
        }

        public double PenhuibuUnitPrice
        {
            get { return _penhuibuUnitPrice; }
            set
            {
                if (_penhuibuUnitPrice != value)
                {
                    _penhuibuUnitPrice = value;
                    OnPropertyChanged(nameof(PenhuibuUnitPrice));
                }
            }
        }

        public double AcrylicUnitPrice
        {
            get { return _acrylicUnitPrice; }
            set
            {
                if (_acrylicUnitPrice != value)
                {
                    _acrylicUnitPrice = value;
                    OnPropertyChanged(nameof(AcrylicUnitPrice));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
