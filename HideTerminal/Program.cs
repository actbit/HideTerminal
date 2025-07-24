using ConsoleAppFramework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HideTerminal
{
    class Program
    {
        public static SettingModel SettingModel { get; set; }
        public const string SettingFileName = "HideTerminalSetting.json";
        static async Task Main(string[] args)
        {
            if (File.Exists(SettingFileName))
            {
                SettingModel = JsonSerializer.Deserialize<SettingModel>(System.IO.File.ReadAllText(SettingFileName));
            }
            else
            {
                SettingModel = new SettingModel();
                using (FileStream fs = new FileStream(SettingFileName, FileMode.OpenOrCreate))
                {
                    JsonSerializer.Serialize(fs, SettingModel);
                    await fs.FlushAsync();
                }
            }
            if (args.Length == 0)
            {
                ProcessStartInfo processStartInfo = new ProcessStartInfo(SettingModel.ExecutePath, SettingModel.Arguments);
                processStartInfo.CreateNoWindow = true;
                processStartInfo.WorkingDirectory = SettingModel.CurrentDirectory;
                processStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                try
                {
                    Process process = Process.Start(processStartInfo);

                }
                catch
                {

                }
                return;
            }
            var consoleApp = ConsoleApp.Create();
            consoleApp.Add<Set>("set");
            consoleApp.Run(args);
        }
    }
    class SettingModel
    {
        public string CurrentDirectory { get; set; } = "";
        public string ExecutePath { get; set; } = "";
        public string Arguments { get; set; } = "";
    }
    class Set
    {
        /// <summary>
        /// Sets the current directory, executable path, and arguments for the application.
        /// </summary>
        /// <param name="current">-c, Current Directory</param>
        /// <param name="execute">-e, Execute File</param>
        /// <param name="args">-a, args</param>
        [Command("")]
        public void Run( string? current = null,string? execute = null, string? args = null)
        {
            if (!string.IsNullOrEmpty(current))
            {
                Program.SettingModel.CurrentDirectory = current.Trim('\"').Trim('\'');
            }
            if (!string.IsNullOrEmpty(execute))
            {
                Program.SettingModel.ExecutePath = execute.Trim('\"').Trim('\'');
            }
            if (!string.IsNullOrEmpty(args))
            {
                Program.SettingModel.Arguments = args.Trim('\"').Trim('\'');
            }
            using (FileStream fs = new FileStream(Program.SettingFileName, FileMode.Open))
            {
                JsonSerializer.Serialize(fs, Program.SettingModel);
            }
        }
    }

}
