using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TuwenDayinDian.Models
{


    public class ThesisParam : INotifyPropertyChanged
    {

        public static ObservableCollection<ThesisParam> ThesisParams { get; set; } = new ObservableCollection<ThesisParam>();
        private int _orderId;   //序号
        private string _date;   //日期
        private int _fileId;   //序号
        private string _fileType;   //文件类型
        private string _pages;   //范围
        private string _duplex;   //正反
        private string _size;   //纸张大小
        private string _fileName;   //文件名
        private short _copies;        //份数
        private string _a3Page;    //
        private string _singlePage;
        private string _flyleafPage;
        private string _colorStyle;
        private string _bindingStyle;
        private string _spine;
        private string _coverType;
        private string _coverName;
        private string _orderStatus;  //订单状态
        private string _receivingInfo;
        private string _path;
        private int _numPage;   //页数


        public int OrderId
        {
            get { return _orderId; }
            set
            {
                if (_orderId != value)
                {
                    _orderId = value;
                    OnPropertyChanged(nameof(OrderId));
                }
            }
        }
        public int FileId
        {
            get { return _fileId; }
            set
            {
                if (_fileId != value)
                {
                    _fileId = value;
                    OnPropertyChanged(nameof(FileId));
                }
            }
        }
        public int NumPage
        {
            get { return _numPage; }
            set
            {
                if (_numPage != value)
                {
                    _numPage = value;
                    OnPropertyChanged(nameof(NumPage));
                }
            }
        }

        public string Date
        {
            get { return _date; }
            set
            {
                if (_date != value)
                {
                    _date = value;
                    OnPropertyChanged(nameof(Date));
                }
            }
        }

        public string FileType
        {
            get { return _fileType; }
            set
            {
                if (_fileType != value)
                {
                    _fileType = value;
                    OnPropertyChanged(nameof(FileType));
                }
            }
        }

        public string Pages
        {
            get { return _pages; }
            set
            {
                if (_pages != value)
                {
                    _pages = value;
                    OnPropertyChanged(nameof(Pages));
                }
            }
        }

        public string Duplex
        {
            get { return _duplex; }
            set
            {
                if (_duplex != value)
                {
                    _duplex = value;
                    OnPropertyChanged(nameof(Duplex));
                }
            }
        }
        public string Path
        {
            get { return _path; }
            set
            {
                if (_path != value)
                {
                    _path = value;
                    OnPropertyChanged(nameof(Path));
                }
            }
        }

        public string Size
        {
            get { return _size; }
            set
            {
                if (_size != value)
                {
                    _size = value;
                    OnPropertyChanged(nameof(Size));
                }
            }
        }



        public string FileName
        {
            get { return _fileName; }
            set
            {
                if (_fileName != value)
                {
                    _fileName = value;
                    OnPropertyChanged(nameof(FileName));
                }
            }
        }

        public string CoverType
        {
            get { return _coverType; }
            set
            {
                if (_coverType != value)
                {
                    _coverType = value;
                    OnPropertyChanged(nameof(CoverType));
                }
            }
        }
        public string CoverName
        {
            get { return _coverName; }
            set
            {
                if (_coverName != value)
                {
                    _coverName = value;
                    OnPropertyChanged(nameof(CoverName));
                }
            }
        }


        public string BindingStyle
        {
            get { return _bindingStyle; }
            set
            {
                if (_bindingStyle != value)
                {
                    _bindingStyle = value;
                    OnPropertyChanged(nameof(BindingStyle));
                }
            }
        }

        public short Copies
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

        public string A3Page
        {
            get { return _a3Page; }
            set
            {
                if (_a3Page != value)
                {
                    _a3Page = value;
                    OnPropertyChanged(nameof(A3Page));
                }
            }
        }

        public string SinglePage
        {
            get { return _singlePage; }
            set
            {
                if (_singlePage != value)
                {
                    _singlePage = value;
                    OnPropertyChanged(nameof(SinglePage));
                }
            }
        }

        public string FlyleafPage
        {
            get { return _flyleafPage; }
            set
            {
                if (_flyleafPage != value)
                {
                    _flyleafPage = value;
                    OnPropertyChanged(nameof(FlyleafPage));
                }
            }
        }

        public string ColorStyle
        {
            get { return _colorStyle; }
            set
            {
                if (_colorStyle != value)
                {
                    _colorStyle = value;
                    OnPropertyChanged(nameof(ColorStyle));
                }
            }
        }

        public string OrderStatus
        {
            get { return _orderStatus; }
            set
            {
                if (_orderStatus != value)
                {
                    _orderStatus = value;
                    OnPropertyChanged(nameof(OrderStatus));
                }
            }
        }

        public string Spine
        {
            get { return _spine; }
            set
            {
                if (_spine != value)
                {
                    _spine = value;
                    OnPropertyChanged(nameof(Spine));
                }
            }
        }

        public string ReceivingInfo
        {
            get { return _receivingInfo; }
            set
            {
                if (_receivingInfo != value)
                {
                    _receivingInfo = value;
                    OnPropertyChanged(nameof(ReceivingInfo));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }


    public class ThesisOrder
    {        
        public string OrderSN { get; set; }
        public string ReceivingInfo { get; set; }
        public string Created_at { get; set; }

        [JsonProperty("ThesisParam")]
        public List<ThesisParam> ThesisParam { get; set; }


    }

}
