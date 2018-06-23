using G1ANT.Language;
using System;
using System.Net;

namespace G1ANT.Addon.Ftp
{
    [Command(Name = "ftp.filesize", Tooltip = "Get file size on FTP server")]
    public class GetFileSizeCommand : Command
    {

        private FtpWebRequest ftpRequest = null;
        private FtpWebResponse ftpResponse = null;

        public class Arguments : CommandArguments
        {

            [Argument(Required = true, Tooltip = "Path to remote file")]
            public TextStructure remotefile { get; set; } = new TextStructure(string.Empty);

            [Argument]
            public VariableStructure Result { get; set; } = new VariableStructure("result");

        }

        public GetFileSizeCommand(AbstractScripter scripter) : base(scripter)
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
                ftpRequest.Method = WebRequestMethods.Ftp.GetFileSize;
                ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
                long size = ftpResponse.ContentLength;
                if (ftpResponse != null)
                {
                    ftpResponse.Close();
                }
                ftpRequest = null;
                Scripter.Variables.SetVariableValue(arguments.Result.Value, new FloatStructure(size));
                return;
            }
            catch (Exception exc)
            {
                throw new ApplicationException($"Error occured while get file size: " + exc.Message);
            }
        }
    }
}
