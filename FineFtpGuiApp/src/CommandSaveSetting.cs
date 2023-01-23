using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Win32;

namespace FineFtpGuiApp.src
{
    class CommandSaveSetting : ICommand
    {
        public CommandSaveSetting(ref SettingFileHandler handler)
        {
            this.handler = handler;
        }
        private SettingFileHandler handler;

        #region ICommnad の実装
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            var dialog = new SaveFileDialog()
            {
                Title = "保存する設定ファイルの選択",
                InitialDirectory = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location),
                FileName = "setting.xml",
                DefaultExt = ".xml",
                Filter = "Extensible Markup Language | *.xml"
            };

            bool? result = dialog.ShowDialog();
            if(result == true)
            {
                this.handler.SaveSettingFile(dialog.FileName);
            }
        }
        #endregion
    }
}
