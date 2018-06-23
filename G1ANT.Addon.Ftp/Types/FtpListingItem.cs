using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G1ANT.Addon.Ftp
{
    public class FtpListingItem
    {
        private String Name { get; }
        private bool IsDirectory { get; }

        public FtpListingItem(String name, bool isDirectory)
        {
            Name = name;
            IsDirectory = isDirectory;
        }

        public override string ToString()
        {
            return Name + "|" + (IsDirectory ? "D" : "F");
        }
    }
}
