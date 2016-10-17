using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace UniTrans
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public static string StringToUnicode(string s)
        {
            char[] charbuffers = s.ToCharArray();
            byte[] buffer;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < charbuffers.Length; i++)
            {
                buffer = Encoding.Unicode.GetBytes(charbuffers[i].ToString());
                sb.Append(string.Format("//u{0:X2}{1:X2}", buffer[1], buffer[0]));
            }
            return sb.ToString();
        }

        public static string UnicodeToString(string str)
        {
            string outStr = "";
            if (!string.IsNullOrEmpty(str))
            {
                string[] strlist = str.Replace("//", "").Split('u');
                    for (int i = 1; i < strlist.Length; i++)
                    {
                        //将unicode字符转为10进制整数，然后转为char中文字符  
                        outStr += (char)int.Parse(strlist[i], System.Globalization.NumberStyles.HexNumber);
                    }
            }
            return outStr;
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (chk1.IsChecked == false)
            {
                uniBox.Text = StringToUnicode(textBox.Text);
                info.Text = "状态:已翻译";
                copy(uniBox);
            }
        }

        private void uniBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (chk1.IsChecked == true)
                {
                    textBox.Text = UnicodeToString(uniBox.Text);
                    info.Text = "状态:已翻译";
                    copy(textBox);
                }
            }
            catch
            {
                info.Text = "翻译失败";
            }
        }

        private void copy(TextBox T)
        {
            if (cp.IsChecked == true)
            {
                Clipboard.SetDataObject(T.Text);
                info.Text = "状态:已复制已翻译结果";
            }
        }
    }
}
