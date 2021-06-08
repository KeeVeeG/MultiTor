using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace MultiTor
{
    class Program
    {
        static void OpenTor(int id)
        {
            foreach (var process in Process.GetProcessesByName("tor" + id))
            {
                process.Kill();
            }
            Thread.Sleep(1000);
            Process tor = new Process();
            tor.StartInfo.FileName = @"C:\Tor\bin\Tor\tor" + id + ".exe";
            tor.StartInfo.Arguments = @"-f C:\Tor\bin\Data\Tor\torrc GeoIPv6File bin\Data\Tor\geoip6 SOCKSPort 127.0.0.1:" + (10000 + id) + " HTTPTunnelPort 127.0.0.1:" + (21000 + id) + " CONTROLPort 127.0.0.1:" + (20000 + id) + @" DATADirectory C:\Tor\data\tor-" + id;
            tor.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            tor.StartInfo.CreateNoWindow = true;
            tor.Start();
            Console.WriteLine("Create tor" + id);
        }
        static void Main(string[] args)
        {
            switch (args[0])
            {
                case "run":
					int id = int.Parse(args[1]);
					if (!File.Exists(@"c:\Tor\bin\Tor\tor" + id + ".exe"))
						File.Copy(@"C:\Tor\bin\Tor\tor.exe", @"C:\Tor\bin\Tor\tor" + id + ".exe");
					OpenTor(id);
                break;

                case "kill":
                    Console.WriteLine("Kill");
                    foreach (string folder in Directory.GetDirectories(@"C:\Tor\data\"))
                    {
                        string ids = folder.Split("-")[1];
                        foreach (var process in Process.GetProcessesByName("tor" + ids))
                        {
                            process.Kill();
                        }
                    }
                break;

                case "cache":
                    Console.WriteLine("Cache");
                    foreach (string folder in Directory.GetDirectories(@"C:\Tor\data\"))
                    {
                        string ids = folder.Split("-")[1];
                        foreach (var process in Process.GetProcessesByName("tor" + ids))
                        {
                            process.Kill();
                        }
                        Thread.Sleep(2000);
                        Directory.Delete(folder, true);
                        File.Delete(@"C:\Tor\bin\Tor\tor" + ids + ".exe");
                    }
                break;
            }
        }
    }
}
