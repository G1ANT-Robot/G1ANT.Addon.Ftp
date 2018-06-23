using G1ANT.Language;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace G1ANT.Addon.Ftp
{
    [Command(Name = "ftp.list", Tooltip = "List FTP directory")]
    public class ListCommand : Command
    {

        private FtpWebRequest ftpRequest = null;
        private FtpWebResponse ftpResponse = null;
        private Stream ftpStream = null;


        public class Arguments : CommandArguments
        {

            [Argument(Required = false, Tooltip = "Directory name")]
            public TextStructure directory { get; set; } = new TextStructure(string.Empty);

            [Argument]
            public VariableStructure Result { get; set; } = new  VariableStructure("result");
        }

        public ListCommand(AbstractScripter scripter) : base(scripter)
        {
        }


        public void Execute(Arguments arguments)
        {
            ListStructure cmdresult = new ListStructure();
            try
            {
                if (arguments.directory.Value == null)
                {
                    arguments.directory.Value = String.Empty;
                }
                ftpRequest = (FtpWebRequest)FtpWebRequest.Create("ftp://" + FtpSettings.GetInstance().Host + "/" + arguments.directory.Value);
                ftpRequest.Credentials = new NetworkCredential(FtpSettings.GetInstance().User, FtpSettings.GetInstance().Password);
                ftpRequest.UseBinary = FtpSettings.GetInstance().UseBinary;
                ftpRequest.UsePassive = FtpSettings.GetInstance().UsePassive;
                ftpRequest.KeepAlive = FtpSettings.GetInstance().KeepAlive;

                ftpRequest.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
                ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
                ftpStream = ftpResponse.GetResponseStream();
                StreamReader ftpReader = new StreamReader(ftpStream);
                string directoryRaw = ftpReader.ReadToEnd();

                ftpReader.Close();
                ftpStream.Close();
                ftpResponse.Close();
                ftpRequest = null;

                string[] list = directoryRaw.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                foreach (String line in list)
                {
                    string[] items = line.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                    string fn = items.Last();
                    bool isDir = items[0].StartsWith("d", StringComparison.InvariantCultureIgnoreCase);
                    String s = new FtpListingItem(fn, isDir).ToString();
                    cmdresult.Value.Add(new TextStructure(s));
                }
                Scripter.Variables.SetVariableValue(arguments.Result.Value, new ListStructure(cmdresult));
            }
            catch (Exception exc)
            {
                throw new ApplicationException($"Error occured while list directory", exc);
            }
            return;
        }
    }
}
