using FineFtpGuiApp.src;
using FineFtpWrapper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace FineFtpGuiApp
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            bool isAutoStart = false;

            // コマンドラインで指定があれば設定ファイルを読み込む
            foreach(string cmd in System.Environment.GetCommandLineArgs())
            {
                // -f 設定ファイルの指定
                if (cmd.Contains("-f"))
                {
                    settingfileHandler.ReadSettingFile(cmd.Replace("-f", ""));
                }
                // -s 自動的に起動
                if (cmd.Contains("-s"))
                {
                    isAutoStart = true;
                }
            }

            // インスタンス構築
            this.Title = "FineFtp server GUI Manager";
            LoadSettingFileButton.Command = new CommandReadSetting(ref this.settingfileHandler);
            SaveSettingFileButton.Command = new CommandSaveSetting(ref this.settingfileHandler);

            // 起動直後は表を触れない状態にしておく
            connectionList.CanUserAddRows = false;
            connectionList.IsEnabled= false;
            StartButton.IsEnabled = false;
            StopButton.IsEnabled = false;

            // サーバー設定を取得する
            connectionList.ItemsSource = settingfileHandler.GetConnectionList();

            // 表を触れるようにする
            connectionList.CanUserAddRows = true;
            connectionList.IsEnabled = true;
            StartButton.IsEnabled = true;

            // 自動開始設定なら開始する
            if (isAutoStart)
            {
                // あまり良くないが無理やりボタンを押す
                RoutedEventArgs tempArg = new RoutedEventArgs();
                this.StartButton_Click(StartButton, tempArg);
            }
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            if (settingfileHandler.GetConnectionList().Count() <= 0)
            {
                // サーバ情報がないなら何もしない
                MessageBox.Show("サーバ設定を入力してください。", "サーバ接続設定なし", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            // 一部要素を無効化する
            connectionList.CanUserAddRows = false;
            connectionList.IsEnabled = false;
            StartButton.IsEnabled = false;
            SaveSettingFileButton.IsEnabled = false;
            LoadSettingFileButton.IsEnabled = false;
            StopButton.IsEnabled = true;

            // Ftpサーバーを開始する
            foreach(FtpServerInformation info in connectionList.ItemsSource)
            {
                // サーバーを構築する
                FineFtpWrapper.FtpServer server = new FineFtpWrapper.FtpServer(info.IPAddress, info.Port);
                bool isSuccessAdd      = server.addUser(info.UserName, info.Password, info.Directory, FineFtpWrapper.Permission.All);
                bool isSuccessStart    = server.start(USED_THREAD_COUNT);

                // サーバーリストに加える
                ftpServers.Add(server);

                // 通信状況を格納する
                if(isSuccessAdd && isSuccessStart)
                {
                    info.IsOnline = true;
                }
            }

        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            // Ftpサーバーを停止する
            foreach(FtpServer server in ftpServers)
            {
                server.stop();
            }

            // サーバーリストをクリアする
            ftpServers.Clear();

            // 通信フラグを全て落とす
            foreach(FtpServerInformation info in connectionList.ItemsSource)
            {
                info.IsOnline = false;
            }

            // 一部要素を有効化する
            connectionList.CanUserAddRows = true;
            connectionList.IsEnabled = true;
            StartButton.IsEnabled = true;
            SaveSettingFileButton.IsEnabled = true;
            LoadSettingFileButton.IsEnabled = true;
            StopButton.IsEnabled = false;

        }

        private void AboutButton_Click(object sender, RoutedEventArgs e) 
        {
            AboutBoxWindow aboutbox = new AboutBoxWindow();
            aboutbox.ShowDialog();
        }

        private List<FineFtpWrapper.FtpServer> ftpServers = new();
        private SettingFileHandler settingfileHandler = new();
        private const int USED_THREAD_COUNT = 1;
    }
}
