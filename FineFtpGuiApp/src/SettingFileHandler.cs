using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace FineFtpGuiApp
{
    class SettingFileHandler
    {
        public SettingFileHandler()
        {
        }

        public ref ObservableCollection<FtpServerInformation> GetConnectionList()
        {
            return ref this.ftpserverInfoList;
        }

        public void ReadSettingFile(string settingFilePath)
        {
            if (!System.IO.File.Exists(settingFilePath))
            {
                return;
            }

            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(ObservableCollection<FtpServerInformation>));
            System.IO.StreamReader sr = new System.IO.StreamReader(settingFilePath, new System.Text.UTF8Encoding(false));

            var temp = serializer.Deserialize(sr);
            if (temp != null )
            {
                // そのまま代入すると違うオブジェクトになってしまう。
                // 同じオブジェクトのままaddするようにしないといけない
                ftpserverInfoList.Clear();
                foreach (FtpServerInformation ftpServerInfo in (ObservableCollection<FtpServerInformation>)temp) { 
                    ftpserverInfoList.Add(ftpServerInfo);
                }
            }
            sr.Close();
        }

        public void SaveSettingFile(string settingFilePath)
        {
            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(ObservableCollection<FtpServerInformation>));
            System.IO.StreamWriter sw = new System.IO.StreamWriter(settingFilePath, false, new System.Text.UTF8Encoding(false));
            serializer.Serialize(sw, this.ftpserverInfoList);
            sw.Close();
        }

        private ObservableCollection<FtpServerInformation> ftpserverInfoList= new ObservableCollection<FtpServerInformation>();
    }
}
