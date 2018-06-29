using G1ANT.Language;

namespace G1ANT.Addon.Ftp
{
    [Addon(Name = "Ftp", Tooltip = "Addon for FTP operations")]
    [Copyright(Author = "Marian Witkowski", Copyright = "(c) 2018 Marian Witkowski", Email = "marian.witkowski@gmail.com")]
    [License(Type = "LGPL", ResourceName = "License.txt")]
    [CommandGroup(Name = "ftp", Tooltip = "Command connected with ftp operations")]

    public class Addon : Language.Addon
    {
        public override void Initialize()
        {
            base.Initialize();
        }
    }
}
