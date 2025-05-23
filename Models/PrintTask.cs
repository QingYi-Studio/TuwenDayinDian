using System.ComponentModel;

namespace TuwenDayinDian.Models
{
    public class PrintTask : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private int _序号;
        public int 序号
        {
            get { return _序号; }
            set
            {
                if (_序号 != value)
                {
                    _序号 = value;
                    OnPropertyChanged(nameof(序号));
                }
            }
        }


        private string _图标;
        public string 图标
        {
            get { return _图标; }
            set
            {
                if (_图标 != value)
                {
                    _图标 = value;
                    OnPropertyChanged(nameof(图标));
                }
            }
        }

        private string _文件名称;
        public string 文件名称
        {
            get { return _文件名称; }
            set
            {
                if (_文件名称 != value)
                {
                    _文件名称 = value;
                    OnPropertyChanged(nameof(文件名称));
                }
            }
        }

        private int _页数;
        public int 页数
        {
            get { return _页数; }
            set
            {
                if (_页数 != value)
                {
                    _页数 = value;
                    OnPropertyChanged(nameof(页数));
                }
            }
        }

        private string _大小;
        public string 大小
        {
            get { return _大小; }
            set
            {
                if (_大小 != value)
                {
                    _大小 = value;
                    OnPropertyChanged(nameof(大小));
                }
            }
        }

        private string _方向;
        public string 方向
        {
            get { return _方向; }
            set
            {
                if (_方向 != value)
                {
                    _方向 = value;
                    OnPropertyChanged(nameof(方向));
                }
            }
        }

        private int _份数;
        public int 份数
        {
            get { return _份数; }
            set
            {
                if (_份数 != value)
                {
                    _份数 = value;
                    OnPropertyChanged(nameof(份数));
                }
            }
        }

        private string _状态;
        public string 状态
        {
            get { return _状态; }
            set
            {
                if (_状态 != value)
                {
                    _状态 = value;
                    OnPropertyChanged(nameof(状态));
                }
            }
        }

        private string _双面;
        public string 双面
        {
            get { return _双面; }
            set
            {
                if (_双面 != value)
                {
                    _双面 = value;
                    OnPropertyChanged(nameof(双面));
                }
            }
        }

        private string _缩放;
        public string 缩放
        {
            get { return _缩放; }
            set
            {
                if (_缩放 != value)
                {
                    _缩放 = value;
                    OnPropertyChanged(nameof(缩放));
                }
            }
        }

        private string _打印机;
        public string 打印机
        {
            get { return _打印机; }
            set
            {
                if (_打印机 != value)
                {
                    _打印机 = value;
                    OnPropertyChanged(nameof(打印机));
                }
            }
        }

        private string _文件路径;
        public string 文件路径
        {
            get { return _文件路径; }
            set
            {
                if (_文件路径 != value)
                {
                    _文件路径 = value;
                    OnPropertyChanged(nameof(文件路径));
                }
            }
        }

        private string _打印状态;
        public string 打印状态
        {
            get { return _打印状态; }
            set
            {
                if (_打印状态 != value)
                {
                    _打印状态 = value;
                    OnPropertyChanged(nameof(打印状态));
                }
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}