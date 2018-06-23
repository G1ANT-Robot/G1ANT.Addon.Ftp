using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G1ANT.Addon.Ftp
{
    public sealed class FtpSettings
    {
        public string Host { get; set; }
        public string User { get; set; }
        public int Port { get; set; }
        public string Password { get; set; }
        public bool UseBinary { get; set; }
        public bool UsePassive { get; set; }
        public bool KeepAlive { get; set; }

        private FtpSettings()
        {
            Port = 21;
            UseBinary = true;
            UsePassive = true;
            KeepAlive = true;
        }

        private static readonly FtpSettings instance = null;
        static FtpSettings()
        {
            instance = new FtpSettings();
        }

        public static FtpSettings GetInstance()
        {
            return instance;
        }

    }


}
