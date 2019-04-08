﻿using System.Text;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Multiplayer.Common.Util
{
    /* Original author: https://stackoverflow.com/users/1563422/danny-beckett
     * Modified by MattTheWaz
     * Posted to stackoverflow: https://stackoverflow.com/questions/217902/reading-writing-an-ini-file
     * Note from author:
     * Firstly, read this MSDN blog post on the limitations of INI files: https://blogs.msdn.microsoft.com/oldnewthing/20071126-00/?p=24383/. 
     * If it suits your needs, read on.
     * This is a concise implementation I wrote, utilising the original Windows P/Invoke, so it is supported by all versions of Windows with .NET installed, 
     * (i.e. Windows 98 - Windows 10). I hereby release it into the public domain - you're free to use it commercially without attribution.
     */
    public class IniFile   // revision 11
    {
        public FileInfo File;
        string Path;
        string EXE = Assembly.GetExecutingAssembly().GetName().Name;

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern long WritePrivateProfileString(string Section, string Key, string Value, string FilePath);

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern int GetPrivateProfileString(string Section, string Key, string Default, StringBuilder RetVal, int Size, string FilePath);

        public IniFile(string IniPath = null)
        {
            File = new FileInfo(IniPath ?? EXE + ".ini");
            Path = File.FullName.ToString();
        }

        public string Read(string Key, string Section = null)
        {
            var RetVal = new StringBuilder(255);
            GetPrivateProfileString(Section ?? EXE, Key, "", RetVal, 255, Path);
            return RetVal.ToString();
        }

        public void Write(string Key, string Value, string Section = null)
        {
            WritePrivateProfileString(Section ?? EXE, Key, Value, Path);
        }

        public void DeleteKey(string Key, string Section = null)
        {
            Write(Key, null, Section ?? EXE);
        }

        public void DeleteSection(string Section = null)
        {
            Write(null, null, Section ?? EXE);
        }

        public bool KeyExists(string Key, string Section = null)
        {
            return Read(Key, Section).Length > 0;
        }
    }
}