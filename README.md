# G1ANT.Addon.Ftp

Some FTP commands to use with G1ANT.Robot

Example usage in G1ANT Robot:

```
ftp.init host ‴ftp.cs.brown.edu‴ user ‴anonymous‴ password ‴user@host.pl‴ 

ftp.get remotefile ‴pub/pscover-1.0.tar.Z‴ localfile  ‴c:\test.txt‴ 

ftp.filesize remotefile ‴pub/pscover-1.0.tar.Z‴ result ♥result
dialog message ♥result

ftp.filetimestamp remotefile ‴pub/pscover-1.0.tar.Z‴ result ♥result
dialog message ♥result
```

