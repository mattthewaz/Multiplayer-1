using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultiplayerDedicatedServer.Util;

namespace MultiplayerDedicatedServer
{
    class Program
    {
        static void Main(string[] args)
        {
            // Step: Read in settings from INI file
            var settings = SettingsReader.ReadServerSettings();

            if (settings == null)
            {
                Logger.Write("Server settings could not be read", Logger.MessageType.Fatal);
                return;
            }

            // Step: Allow user to select a file (or quit)

            // Step: Start server on different thread

            // Step: Listen for console commands
        }
    }
}
