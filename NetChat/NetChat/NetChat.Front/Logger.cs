using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace NetChat.Front
{
    public static class Logger
    {
        public static string path = Path.GetTempPath() + "/" + AppDomain.CurrentDomain.FriendlyName + " Debug.log";

        public static void Start()
        {
            File.AppendAllText(path, "\n >>>Starting Program<<< \n\n");
        }

        public static void Debug(string message)
        {
            WriteToFile(message, "Debug");
        }

        public static void Error(string message)
        {
            WriteToFile(message, "Error");
        }

        public static void Info(string message)
        {
            WriteToFile(message, "Info");
        }

        private static void WriteToFile(string message, string msgType)
        {
            if (!File.Exists(path))
                File.Create(path).Close();
            File.AppendAllText(path, "[" + DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss") + "][" + msgType + "] >>> " + message + "\n");
        }

        public static void Error(Exception ex)
        {
            Error(ex.ToString());
        }
    }
}
