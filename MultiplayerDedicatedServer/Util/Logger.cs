using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiplayerDedicatedServer.Util
{
    // Class for logging to be hooked up. Just a stub for now.
    public static class Logger
    {
        public enum MessageType
        {
            Message = 0,
            Info = 1,
            Warning = 2,
            Error = 3,
            Fatal = 4
        };


        public static void Write(string message, MessageType type)
        {
            switch (type)
            {
                case MessageType.Info:
                    Console.WriteLine($"[Info] - {message}");
                    break;
                case MessageType.Warning:
                    Console.WriteLine($"[Warn] - {message}");
                    break;
                case MessageType.Error:
                    Console.WriteLine($"[Error] - {message}");
                    break;
                case MessageType.Fatal:
                    Console.WriteLine($"[FATAL] - {message}");
                    break;
                default:
                    Console.WriteLine(message);
                    break;
            }
        }
    }
}
