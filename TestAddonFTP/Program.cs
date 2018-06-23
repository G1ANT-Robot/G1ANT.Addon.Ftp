using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using G1ANT.Addon.Ftp;
using G1ANT.Language;

namespace TestAddonFTP
{
    class Program
    {
        static void Main(string[] args)
        {
            FtpSettings.GetInstance().Host = "ftp.cs.brown.edu";
            FtpSettings.GetInstance().User = "anonymous";
            FtpSettings.GetInstance().Password = "test@test.com";


            /*
            GetFileTimestampCommand.Arguments arg = new GetFileTimestampCommand.Arguments();
            arg.remotefile = new G1ANT.Language.TextStructure("pub/pscover-1.0.tar.Z");
            GetFileTimestampCommand gpzc = new GetFileTimestampCommand(null);
            gpzc.Execute(arg);
            */

            ListCommand.Arguments arg = new ListCommand.Arguments();
            arg.directory = new TextStructure("pub/");
            ListCommand lc = new ListCommand(null);
            lc.Execute(arg);
        }
    }
}
