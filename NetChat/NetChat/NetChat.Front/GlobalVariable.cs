using System;
using System.IO;

namespace NetChat.Front {
    public static class GlobalVariable
    {

        private static readonly string Path = System.IO.Path.GetTempPath() + "NetChat.config";
        public static string Username = "User" + new Random().Next();
        public static string Ip = "127.0.0.1";
        public static int Port = 4308;
        public static string Pw = "1234";

        public static void SafeToTemp()
        {
            string[] content =
            {
                "User:" + Username,
                "IP:" + Ip,
                "Port:" + Port,
                "PW:" + Pw
            };
            File.WriteAllLines(Path, content);
        }

        public static void LoadFromTemp()
        {
            if (!File.Exists(Path)) return;
            var content = File.ReadAllLines(Path);
            Username = content[0].Replace("User:", "");
            Ip = content[1].Replace("IP:", "");
            Port = int.Parse(content[2].Replace("Port:", ""));
            Pw = content[3].Replace("PW:", "");
        }
    }
}
