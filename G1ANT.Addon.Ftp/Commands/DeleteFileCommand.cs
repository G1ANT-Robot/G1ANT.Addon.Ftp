using G1ANT.Language;
using System;
using System.Net;

namespace G1ANT.Addon.Ftp
{
    [Command(Name = "ftp.delete", Tooltip = "Delete file from FTP server")]
    public class DeleteFileCommand : Command
    {

        private FtpWebRequest ftpRequest = null;
        private FtpWebResponse ftpResponse = null;

        public class Arguments : CommandArguments
        {

            [Argument(Required = true, Tooltip = "Path to remote file to delete")]
            public TextStructure remotefile { get; set; } = new TextStructure(string.Empty);

        }

        public DeleteFileCommand(AbstractScripter scripter) : base(scripter)
        {
        }


        public void Execute(Arguments arguments)
        {
            try
            {
                ftpRequest = (FtpWebRequest)FtpWebRequest.Create("ftp://" + FtpSettings.GetInstance().Host + "/" + arguments.remotefile.Value);
                ftpRequest.Credentials = new NetworkCredential(FtpSettings.GetInstance().User, FtpSettings.GetInstance().Password);
                ftpRequest.UseBinary = FtpSettings.GetInstance().UseBinary;
                ftpRequest.UsePassive = FtpSettings.GetInstance().UsePassive;
                ftpRequest.KeepAlive = FtpSettings.GetInstance().KeepAlive;
                ftpRequest.Method = WebRequestMethods.Ftp.DeleteFile;
                ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
                ftpResponse.Close();
                ftpRequest = null;
            }
            catch (Exception exc) {
                throw new ApplicationException($"Error occured while delete ftp file", exc);
            }
            return;
        }
    }
}
