using G1ANT.Language;
using System;
using System.Net;

namespace G1ANT.Addon.Ftp
{
    [Command(Name = "ftp.filetimestamp", Tooltip = "Get file last modification timestamp")]
    public class GetFileTimestampCommand : Command
    {

        private FtpWebRequest ftpRequest = null;

        public class Arguments : CommandArguments
        {

            [Argument(Required = true, Tooltip = "Path to remote file")]
            public TextStructure remotefile { get; set; } = new TextStructure(string.Empty);

            [Argument]
            public VariableStructure Result { get; set; } = new VariableStructure("result");

        }

        public GetFileTimestampCommand(AbstractScripter scripter) : base(scripter)
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
                ftpRequest.Method = WebRequestMethods.Ftp.GetDateTimestamp;

                using (FtpWebResponse response = (FtpWebResponse)ftpRequest.GetResponse())
                {
                    Scripter.Variables.SetVariableValue(arguments.Result.Value, new DateTimeStructure(response.LastModified));
                }

                ftpRequest = null;
                return;
            }
            catch (Exception exc)
            {
                throw new ApplicationException($"Error occured while get file timestamp: " + exc.Message);
            }
        }
    }
}
