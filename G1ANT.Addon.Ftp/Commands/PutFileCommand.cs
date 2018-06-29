using G1ANT.Language;
using System;
using System.IO;
using System.Net;

namespace G1ANT.Addon.Ftp
{
    [Command(Name = "ftp.put", Tooltip = "Put file to FTP server")]
    public class PutFileCommand : Command
    {

        private FtpWebRequest ftpRequest = null;
        private Stream ftpStream = null;
        private readonly int bufferSize = 4096;

        public class Arguments : CommandArguments
        {

            [Argument(Required = true, Tooltip = "Local file name")]
            public TextStructure localfile { get; set; } = new TextStructure(string.Empty);

            [Argument(Required = true, Tooltip = "Path to remote file")]
            public TextStructure remotefile { get; set; } = new TextStructure(string.Empty);

        }

        public PutFileCommand(AbstractScripter scripter) : base(scripter)
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
                ftpRequest.Method = WebRequestMethods.Ftp.UploadFile;
                ftpStream = ftpRequest.GetRequestStream();
                FileStream localFileStream = new FileStream(arguments.localfile.Value, FileMode.Open);
                byte[] byteBuffer = new byte[bufferSize];
                int bytesSent = localFileStream.Read(byteBuffer, 0, bufferSize);

                while (bytesSent != 0)
                {
                    ftpStream.Write(byteBuffer, 0, bytesSent);
                    bytesSent = localFileStream.Read(byteBuffer, 0, bufferSize);
                }

                localFileStream.Close();
                ftpStream.Close();
                ftpRequest = null;
            }
            catch (Exception exc)
            {
                throw new ApplicationException($"Error occured while sending file to FTP server", exc);
            }
            return;
        }
    }
}
