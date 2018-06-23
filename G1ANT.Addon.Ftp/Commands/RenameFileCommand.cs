using G1ANT.Language;
using System;
using System.Net;

namespace G1ANT.Addon.Ftp
{
    [Command(Name = "ftp.rename", Tooltip = "Rename file on FTP server")]
    public class RenameFileCommand : Command
    {

        private FtpWebRequest ftpRequest = null;
        private FtpWebResponse ftpResponse = null;

        public class Arguments : CommandArguments
        {

            [Argument(Required = true, Tooltip = "Path to current file")]
            public TextStructure currentFile { get; set; } = new TextStructure(string.Empty);

            [Argument(Required = true, Tooltip = "Path to new file")]
            public TextStructure newFile { get; set; } = new TextStructure(string.Empty);

        }

        public RenameFileCommand(AbstractScripter scripter) : base(scripter)
        {
        }


        public void Execute(Arguments arguments)
        {
            try
            {
                ftpRequest = (FtpWebRequest)FtpWebRequest.Create("ftp://" + FtpSettings.GetInstance().Host + "/" + arguments.currentFile.Value);
                ftpRequest.Credentials = new NetworkCredential(FtpSettings.GetInstance().User, FtpSettings.GetInstance().Password);
                ftpRequest.UseBinary = FtpSettings.GetInstance().UseBinary;
                ftpRequest.UsePassive = FtpSettings.GetInstance().UsePassive;
                ftpRequest.KeepAlive = FtpSettings.GetInstance().KeepAlive;
                ftpRequest.Method = WebRequestMethods.Ftp.Rename;
                ftpRequest.RenameTo = arguments.newFile.Value;
                ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
                ftpResponse.Close();
                ftpRequest = null;
            }
            catch (Exception exc) {
                throw new ApplicationException($"Error occured while rename remote file name", exc);
            }
            return;
        }
    }
}
