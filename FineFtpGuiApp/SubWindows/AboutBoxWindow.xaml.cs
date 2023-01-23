using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Shapes;

namespace FineFtpGuiApp
{
    /// <summary>
    /// AboutBoxWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class AboutBoxWindow : Window
    {
        public AboutBoxWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            this.FileVersionInfo =
                FileVersionInfo.GetVersionInfo(Environment.GetCommandLineArgs()[0]);
        }

        public FileVersionInfo FileVersionInfo
        {
            get { return (FileVersionInfo)GetValue(FileVersionInfoProperty); }
            set { SetValue(FileVersionInfoProperty, value); }
        }

        public static readonly DependencyProperty FileVersionInfoProperty =
            DependencyProperty.Register("FileVersionInfo", typeof(FileVersionInfo),
                typeof(AboutBoxWindow), new UIPropertyMetadata(null));
    }
}
