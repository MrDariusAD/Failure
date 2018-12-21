using System;
using System.IO;
using System.Threading;

namespace NetChat.Front {
    public static class GlobalVariable
    {

        private static string PATH = System.IO.Path.GetTempPath() + "NetChat.config";
        public static string USERNAME = "User" + new Random().Next();
        public static string IP = "127.0.0.1";
        public static int PORT = 4308;
        public static string PW = "1234";

        public static void SafeToTemp()
        {
            string[] content =
            {
                "User:" + USERNAME,
                "IP:" + IP,
                "Port:" + PORT,
                "PW:" + PW
            };
            File.WriteAllLines(PATH, content);
        }

        public static void LoadFromTemp()
        {
            if(File.Exists(PATH)) {
                string[] content = File.ReadAllLines(PATH);
                USERNAME = content[0].Replace("User:", "");
                IP = content[1].Replace("IP:", "");
                PORT = int.Parse(content[2].Replace("Port:", ""));
                PW = content[3].Replace("PW:", "");
            }
        }
    }
}
