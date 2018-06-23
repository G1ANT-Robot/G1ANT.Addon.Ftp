using G1ANT.Language;
using System;
using System.IO;
using System.Net;

namespace G1ANT.Addon.Ftp
{
    [Command(Name = "ftp.get", Tooltip = "Get file from FTP server")]
    public class GetFileCommand : Command
    {

        private FtpWebRequest ftpRequest = null;
        private FtpWebResponse ftpResponse = null;
        private Stream ftpStream = null;
        private int bufferSize = 4096;

        public class Arguments : CommandArguments
        {

            [Argument(Required = true, Tooltip = "Path to remote file")]
            public TextStructure remotefile { get; set; } = new TextStructure(string.Empty);

            [Argument(Required = true, Tooltip = "Local file name, if exists will be overwritten")]
            public TextStructure localfile { get; set; } = new TextStructure(string.Empty);

        }

        public GetFileCommand(AbstractScripter scripter) : base(scripter)
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
                ftpRequest.Method = WebRequestMethods.Ftp.DownloadFile;
                ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
                ftpStream = ftpResponse.GetResponseStream();
                FileStream localFileStream = new FileStream(arguments.localfile.Value, FileMode.Create);
                byte[] byteBuffer = new byte[bufferSize];
                int bytesRead = ftpStream.Read(byteBuffer, 0, bufferSize);

                while (bytesRead > 0)
                {
                    localFileStream.Write(byteBuffer, 0, bytesRead);
                    bytesRead = ftpStream.Read(byteBuffer, 0, bufferSize);
                }

                localFileStream.Close();
                ftpStream.Close();
                ftpResponse.Close();
                ftpRequest = null;
            }
            catch (Exception exc)
            {
                throw new ApplicationException($"Error occured while download ftp file", exc);
            }
            return;
        }
    }
}
