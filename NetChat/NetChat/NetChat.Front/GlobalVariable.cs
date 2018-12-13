using System;
using System.IO;

namespace NetChat.Front {
    public static class GlobalVariable
    {

        private static string path = System.IO.Path.GetTempPath() + "NetChat.config";
        public static String UserName = "User" + new Random().Next();
        public static String IP = "127.0.0.1";
        public static int Port = 4308;
        public static String PW = "1234";

        public static void SafeToTemp()
        {
            string[] content =
            {
                "User:" + UserName,
                "IP:" + IP,
                "Port:" + Port,
                "PW:" + PW
            };
            File.WriteAllLines(path, content);
        }

        public static void LoadFromTemp()
        {
            if(File.Exists(path)) {
                string[] content = File.ReadAllLines(path);
                UserName = content[0].Replace("User:", "");
                IP = content[1].Replace("IP:", "");
                Port = int.Parse(content[2].Replace("Port:", ""));
                PW = content[3].Replace("PW:", "");
            }
        }
    }
}
