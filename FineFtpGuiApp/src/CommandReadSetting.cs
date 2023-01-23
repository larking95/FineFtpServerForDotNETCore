using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Win32;

namespace FineFtpGuiApp.src
{
    class CommandReadSetting : ICommand
    {
        public CommandReadSetting(ref SettingFileHandler handler)
        {
            this.handler = handler;
        }

        #region ICommand の実装
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            var dialog = new OpenFileDialog()
            {
                Title = "読み込む設定ファイルの選択",
                InitialDirectory = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location),
                FileName = "setting.xml",
                DefaultExt = ".xml",
                Filter = "Extensible Markup Language | *.xml"
            };

            bool? result = dialog.ShowDialog();
            if(result == true)
            {
                this.handler.ReadSettingFile(dialog.FileName);
            }
        }

        #endregion

        private SettingFileHandler handler;
    }
}
