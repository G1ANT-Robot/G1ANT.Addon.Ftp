using G1ANT.Language;
using System;
using System.Net;

namespace G1ANT.Addon.Ftp
{
    [Command(Name = "ftp.createdir", Tooltip = "Create directory on FTP server")]
    public class CreateDirectoryCommand : Command
    {

        private FtpWebRequest ftpRequest = null;
        private FtpWebResponse ftpResponse = null;


        public class Arguments : CommandArguments
        {

            [Argument(Required = true, Tooltip = "Directory name")]
            public TextStructure directory { get; set; } = new TextStructure(string.Empty);


        }

        public CreateDirectoryCommand(AbstractScripter scripter) : base(scripter)
        {
        }


        public void Execute(Arguments arguments)
        {
            try
            {
                ftpRequest = (FtpWebRequest)FtpWebRequest.Create("ftp://" + FtpSettings.GetInstance().Host + "/" + arguments.directory.Value);
                ftpRequest.Credentials = new NetworkCredential(FtpSettings.GetInstance().User, FtpSettings.GetInstance().Password);
                ftpRequest.UseBinary = FtpSettings.GetInstance().UseBinary;
                ftpRequest.UsePassive = FtpSettings.GetInstance().UsePassive;
                ftpRequest.KeepAlive = FtpSettings.GetInstance().KeepAlive;
                ftpRequest.Method = WebRequestMethods.Ftp.MakeDirectory;
                ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
                ftpResponse.Close();
                ftpRequest = null;
            }
            catch (Exception exc) {
                throw new ApplicationException($"Error occured while create directory", exc);
            }
            return;
        }
    }
}
