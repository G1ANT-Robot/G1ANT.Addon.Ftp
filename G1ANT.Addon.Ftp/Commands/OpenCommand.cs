using G1ANT.Language;

namespace G1ANT.Addon.Ftp
{
    [Command(Name = "ftp.init", Tooltip = "Set parameters for FTP connection")]
    public class OpenCommand : Command
    {
  
        public class Arguments : CommandArguments
        {
            [Argument(Required = true, Tooltip = "FTP hostname or IP")]
            public TextStructure host { get; set; } = new TextStructure(string.Empty);

            [Argument(Required = true, Tooltip = "FTP user")]
            public TextStructure user { get; set; } = new TextStructure(string.Empty);

            [Argument(Required = true, Tooltip = "FTP password")]
            public TextStructure password { get; set; } = new TextStructure(string.Empty);

            [Argument(Required = false, Tooltip = "FTP server port")]
            public IntegerStructure port { get; set; } = new IntegerStructure(21);

            [Argument(Required = false, Tooltip = "FTP binary/text transfering")]
            public BooleanStructure binary { get; set; } = new BooleanStructure(true);

            [Argument(Required = false, Tooltip = "FTP keep alive option")]
            public BooleanStructure keepalive { get; set; } = new BooleanStructure(true);

            [Argument(Required = false, Tooltip = "FTP connection option")]
            public BooleanStructure passive { get; set; } = new BooleanStructure(true);
        }

        public OpenCommand(AbstractScripter scripter) : base(scripter)
        {
        }

        public void Execute(Arguments arguments)
        {
            FtpSettings.GetInstance().Host = arguments.host.Value;
            FtpSettings.GetInstance().User = arguments.user.Value;
            FtpSettings.GetInstance().Port = arguments.port.Value;
            FtpSettings.GetInstance().KeepAlive = arguments.keepalive.Value;
            FtpSettings.GetInstance().UseBinary = arguments.binary.Value;
            FtpSettings.GetInstance().UsePassive = arguments.passive.Value;
        }
    }
}
