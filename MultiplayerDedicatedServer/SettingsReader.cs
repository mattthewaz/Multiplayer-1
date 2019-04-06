using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Multiplayer.Common;
using MultiplayerDedicatedServer.Util;


namespace MultiplayerDedicatedServer
{
    public static class SettingsReader
    {
        private const string _settingsFilename = "Settings.ini";

        // Reads ServerSettings in from ini file.
        // If ini file does not exist, a new ini file will be written with default values, 
        // an error will be issued to logger, and this method will return null to signify 
        // something went wrong.
        // If ini file exists but is missing required information, an error will be issued,
        // and this method will return null to signify something went wrong.
        public static ServerSettings ReadServerSettings()
        {
            var settingIni = new IniFile(_settingsFilename);

            if (!settingIni.File.Exists)
            {
                //TODO: Localize error string
                Logger.Write($"{_settingsFilename} does not exist. Creating one with default values. Please modify file and rerun.", Logger.MessageType.Error);
                WriteEmptyIniFile(settingIni);
                return null;
            }
            else
            {
                var settings = ReadIniFile(settingIni);
                return settings;
            }
        }

        // Reads in settings from ini file. If required property is missing 
        // will display an error message and then return null.
        // Otherwise, returns ServerSettings object populated with values
        // read in from file.
        public static ServerSettings ReadIniFile(IniFile settingIni)
        {
            var settings = new ServerSettings();
            var valid = true;

            //public string gameName;
            settings.gameName = ReadIniEntry(settingIni, "gameName", "MyGame", "ServerSettings");
            valid &= settings.gameName != null;

            //public string bindAddress;
            settings.bindAddress = ReadIniEntry(settingIni, "bindAddress", "127.0.0.1", "ServerSettings");
            valid &= settings.bindAddress != null;

            //public int bindPort;
            var bindPortString = ReadIniEntry(settingIni, "bindPort", MultiplayerServer.DefaultPort.ToString(), "ServerSettings");
            valid &= int.TryParse(bindPortString, out settings.bindPort);

            //public string lanAddress;
            settings.lanAddress = ReadIniEntry(settingIni, "lanAddress", Multiplayer.Client.MpUtil.GetLocalIpAddress() ?? "127.0.0.1", "ServerSettings");
            valid &= settings.lanAddress != null;

            //public int maxPlayers = 8;
            var maxPlayersString = ReadIniEntry(settingIni, "maxPlayers", "8", "ServerSettings");
            valid &= int.TryParse(maxPlayersString, out settings.maxPlayers);

            //public int autosaveInterval = 8;
            var autosaveIntervalString = ReadIniEntry(settingIni, "autosaveInterval", "8", "ServerSettings");
            valid &= int.TryParse(autosaveIntervalString, out settings.autosaveInterval);

            //public bool pauseOnAutosave = false;
            var pauseOnAutosaveString = ReadIniEntry(settingIni, "pauseOnAutosave", "false", "ServerSettings");
            valid &= bool.TryParse(pauseOnAutosaveString, out settings.pauseOnAutosave);

            //public bool steam;
            var steamString = ReadIniEntry(settingIni, "steam", "false", "ServerSettings");
            valid &= bool.TryParse(steamString, out settings.steam);

            //public bool arbiter;
            var arbiterString = ReadIniEntry(settingIni, "arbiter", "false", "ServerSettings");
            valid &= bool.TryParse(arbiterString, out settings.arbiter);

            if (!valid)
            {
                Logger.Write("Errors were discovered in ini file. Please correct errors before rerunning.", Logger.MessageType.Error);
                return null;
            }

            return settings;
        }

        public static string ReadIniEntry(IniFile settingIni, string keyName, string defaultValue, string sectionName = null)
        {
            if (!settingIni.KeyExists(keyName, sectionName))
            {
                settingIni.Write(keyName, defaultValue, sectionName);
                Logger.Write($"Property {keyName} was not found in ini file. An entry has been created with a defualt value. Please modify value and rerun.", Logger.MessageType.Error);
                return null;
            }

            var entry = settingIni.Read(keyName, sectionName);
            if (String.IsNullOrWhiteSpace(entry))
            {
                Logger.Write($"Property {keyName} was not defined. Please modify value and rerun.", Logger.MessageType.Error);
                return null;
            }

            return entry;
        }

        // Create an entry for each of the variables in the ServerSettings class
        public static void WriteEmptyIniFile(IniFile settingIni)
        {
            //public string gameName;
            settingIni.Write("gameName", "MyGame", "ServerSettings");

            //public string bindAddress;
            settingIni.Write("bindAddress", "127.0.0.1", "ServerSettings");

            //public int bindPort;
            settingIni.Write("bindPort", MultiplayerServer.DefaultPort.ToString(), "ServerSettings");

            //public string lanAddress;
            settingIni.Write("lanAddress", Multiplayer.Client.MpUtil.GetLocalIpAddress() ?? "127.0.0.1", "ServerSettings");

            //public int maxPlayers = 8;
            settingIni.Write("maxPlayers", "8", "ServerSettings");

            //public int autosaveInterval = 8;
            settingIni.Write("autosaveInterval", "8", "ServerSettings");

            //public bool pauseOnAutosave = false;
            settingIni.Write("pauseOnAutosave", "false", "ServerSettings");

            //public bool steam;
            settingIni.Write("steam", "false", "ServerSettings");

            //public bool arbiter;
            settingIni.Write("arbiter", "false", "ServerSettings");
        }
    }
}
