using System.ComponentModel;

namespace FineFtpGuiApp
{
    public class FtpServerInformation:INotifyPropertyChanged
    {
        public FtpServerInformation() {
            _IPAddress   = "0.0.0.0";
            _Port        = 21;
            _UserName    = "User";
            _Password    = "";
            _Directory   = "C://";
            _IsOnline    = false;
        }

        private string _IPAddress;
        public string IPAddress {
            get { return this._IPAddress; }
            set
            {
                if ( value != this._IPAddress)
                {
                    this._IPAddress = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private int _Port;
        public int Port {
            get { return this._Port; }
            set
            {
                if( value != this._Port)
                {
                    this._Port = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string _UserName;
        public string UserName {
            get { return this._UserName; }
            set {
                if(value != this._UserName)
                {
                    this._UserName = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string _Password;
        public string Password {
            get { return this._Password; }
            set {
                if (value != this._Password)
                {
                    this._Password = value;
                    NotifyPropertyChanged();
                }
            } 
        }

        private string _Directory;
        public string Directory {
            get { return this._Directory; }
            set
            {
                if(value != this._Directory)
                {
                    this._Directory = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private bool _IsOnline;
        public bool IsOnline {
            get { return this._IsOnline; }
            set { 
                if(value != this._IsOnline)
                {
                    this._IsOnline = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string OnlineStatus { get { return !IsOnline ? "通信停止中" : "通信中"; } }

        #region INotifyPropertyChangedの実装
        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged(string propertyName = "")
        {
            if( PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
