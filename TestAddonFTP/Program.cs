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
            FtpSettings.GetInstance().Host = "localhost";
            FtpSettings.GetInstance().User = "test";
            FtpSettings.GetInstance().Password = "test";


            /*
            GetFileCommand.Arguments arg = new GetFileCommand.Arguments();
            arg.remotefile = new TextStructure("README.txt");
            arg.localfile = new TextStructure(@"c:\tmp\ftp.txt");
            GetFileCommand gfc = new GetFileCommand(null);
            gfc.Execute(arg);
            */

            /*
            PutFileCommand.Arguments arg = new PutFileCommand.Arguments();
            arg.remotefile = new TextStructure("newfile.txt");
            arg.localfile = new TextStructure(@"c:\tmp\ftp1.txt");
            PutFileCommand pfc = new PutFileCommand(null);
            pfc.Execute(arg);
            */

            /*
            RenameFileCommand.Arguments arg = new RenameFileCommand.Arguments();
            arg.currentFile = new TextStructure("newfile.txt");
            arg.newFile = new TextStructure("ftp2.txt");
            RenameFileCommand rfc = new RenameFileCommand(null);
            rfc.Execute(arg);
            */

            DeleteFileCommand.Arguments arg = new DeleteFileCommand.Arguments();
            arg.remotefile = new TextStructure("ftp2.txt");
            DeleteFileCommand dfc = new DeleteFileCommand(null);
            dfc.Execute(arg);

            /*
            GetFileTimestampCommand.Arguments arg = new GetFileTimestampCommand.Arguments();
            arg.remotefile = new G1ANT.Language.TextStructure("README.txt");
            GetFileTimestampCommand gpzc = new GetFileTimestampCommand(null);
            gpzc.Execute(arg);
            */

            /*
            ListCommand.Arguments arg = new ListCommand.Arguments();
            arg.directory = new TextStructure("pub/");
            ListCommand lc = new ListCommand(null);
            lc.Execute(arg);
            */
        }
    }
}
