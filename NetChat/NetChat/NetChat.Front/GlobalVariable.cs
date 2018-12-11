using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetChat.Front
{
    public static class GlobalVariable
    {

        public static String UserName = "User" + new Random().Next();
        public static bool allowSelfHost = true;
        public static String IP = "127.0.0.1";
        public static int Port = 4308;
        public static String PW = "1234";
    }
}
