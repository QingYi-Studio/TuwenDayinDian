using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TuwenDayinDian.Helpers
{
    internal class AfTextFile
    {
        public static string Read(string filePath, Encoding encoding)
        {
            using (StreamReader sr = new StreamReader(filePath, encoding))
            {
                return sr.ReadToEnd();
            }
        }

        public static void Write(string filePath, string content, Encoding encoding)
        {
            using (StreamWriter sw = new StreamWriter(filePath, false, encoding))
            {
                sw.Write(content);
            }
        }

        public static bool Write(string filePath)
        {
            try
            {
                if (string.IsNullOrEmpty(filePath))
                {
                    MessageBox.Show("完成状态文件路径为空", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }

                string dir = Path.GetDirectoryName(filePath);

                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                using (FileStream fs = File.Create(filePath)) { }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        // 两个静态属性
        public static Encoding UTF8
        {
            get
            {
                return UTF8Encoding.UTF8;
            }
        }
        public static Encoding GBK
        {
            get
            {
                return Encoding.GetEncoding("GBK");
            }
        }
    }
}
