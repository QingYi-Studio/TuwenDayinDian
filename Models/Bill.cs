using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuwenDayinDian.Models
{
    public static class GlobalData
    {
        public static string Name { get; set; }
       
    }
    public class Bill : INotifyPropertyChanged
    {


        public static ObservableCollection<Bill> Bills { get; set; } = new ObservableCollection<Bill>();
        private int _id;
        private int _serialNumbe;   //序号
        private string _date;   //日期
        private string _fileName;   //文件名
        private string _technique; //工艺
        private int _copies;        //份数
        private double _amount;     //金额
        private string _orderBy;    //下单人——联系人
        private string _orderStatus;  //订单状态
        private string _operator;     //操作员
        private string _fileDetail;     //操作员

        public int Id
        {
            get { return _id; }
            set
            {
                if (_id != value)
                {
                    _id = value;
                    OnPropertyChanged(nameof(Id));
                }
            }
        }

        public int SerialNumber
        {
            get { return _serialNumbe; }
            set
            {
                if (_serialNumbe != value)
                {
                    _serialNumbe = value;
                    OnPropertyChanged(nameof(SerialNumber));
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

        public string FileDetail
        {
            get { return _fileDetail; }
            set
            {
                if (_fileDetail != value)
                {
                    _fileDetail = value;
                    OnPropertyChanged(nameof(FileDetail));
                }
            }
        }


        public string Technique
        {
            get { return _technique; }
            set
            {
                if (_technique != value)
                {
                    _technique = value;
                    OnPropertyChanged(nameof(Technique));
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

        public double Amount
        {
            get { return _amount; }
            set
            {
                if (_amount != value)
                {
                    _amount = value;
                    OnPropertyChanged(nameof(Amount));
                }
            }
        }

        public string OrderBy
        {
            get { return _orderBy; }
            set
            {
                if (_orderBy != value)
                {
                    _orderBy = value;
                    OnPropertyChanged(nameof(OrderBy));
                }
            }
        }

        public string Order_Status
        {
            get { return _orderStatus; }
            set
            {
                if (_orderStatus != value)
                {
                    _orderStatus = value;
                    OnPropertyChanged(nameof(Order_Status));
                   
                }
            }
        }

        public string Operator
        {
            get { return _operator; }
            set
            {
                if (_operator != value)
                {
                    _operator = value;
                    OnPropertyChanged(nameof(Operator));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    internal class ContactPerson : INotifyPropertyChanged
    {

        private string _contactName;
        public static ObservableCollection<ContactPerson> ContactPersons { get; set; } = new ObservableCollection<ContactPerson>();
        public string ContactName
        {
            get { return _contactName; }
            set
            {
                if (_contactName != value)
                {
                    _contactName = value;
                    OnPropertyChanged(nameof(ContactName));
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
