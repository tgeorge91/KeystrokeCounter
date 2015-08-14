using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace KeystrokeCounter
{
    class Program
    {
        private static int count = 0;
        private static Log _log;

        private static string LogLocation = @"today.json";
        private static string HistoryLocation = @"history.json";

        static void Main(string[] args)
        {
            OpenLog();
            SwitchToNewDay();
            //KeyListener.Start();
        }

        public static void Count(bool forceSave = false)
        {
            count++;
            if (count % 15 == 0 || forceSave) { Save(); }
        }

        private static void Save()
        {

        }

        private static void OpenLog()
        {
            var log = File.ReadAllText(LogLocation);
            _log = JsonConvert.DeserializeObject<Log>(log);
        }

        private static void SwitchToNewDay() {
            var today = DateTime.Now.Date.ToShortDateString();
            if (DateTime.Now.Date > _log.Date.Date)
            {
                var history = new History();
                var historyJson = File.ReadAllText(HistoryLocation);
                history = JsonConvert.DeserializeObject<History>(historyJson);
                history.HistoryLog.Add(_log);
                var newHistory = JsonConvert.SerializeObject(history);
                File.WriteAllText(HistoryLocation, newHistory);
            }
        }
    }
}