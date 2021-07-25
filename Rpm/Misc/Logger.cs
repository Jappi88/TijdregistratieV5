using System.Collections.Generic;
using Rpm.Various;

namespace Rpm.Misc
{
    public class Logger
    {
        public Logger()
        {
            Logs = new List<LogEntry>();
        }

        public List<LogEntry> Logs { get; }

        public bool Load()
        {
            try
            {
                //using (FileStream fs = new FileStream(_filepath, FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read))
                //{
                //    SharpSerializer sh = new SharpSerializer(true);
                //    Logs = sh.Deserialize(fs) as List<LogFile>;
                //    fs.Close();
                //}
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Save()
        {
            try
            {
                //    using (FileStream fs = new FileStream(_filepath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read))
                //    {
                //        SharpSerializer sh = new SharpSerializer(true);
                //        sh.Serialize(Logs, fs);
                //        fs.Close();
                //    }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void AddLog(LogEntry log)
        {
            //log.Added = DateTime.Now;
            //Logs.Add(log);
            //Save();
            //NewlogAdded(log);
        }

        public event NewLogHandler OnNewLogAdded;

        public void NewlogAdded(LogEntry log)
        {
            OnNewLogAdded?.Invoke(log, this);
        }
    }
}