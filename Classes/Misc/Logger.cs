using Polenter.Serialization;
using System;
using System.Collections.Generic;
using System.IO;

namespace ProductieManager.Misc
{
    public class Logger
    {
        private readonly string _filepath;
        public List<LogFile> Logs { get; private set; }

        public Logger(string filepath)
        {
            _filepath = filepath;
            Logs = new List<LogFile> { };
        }

        public bool Load()
        {
            try
            {
                using (FileStream fs = new FileStream(_filepath, FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read))
                {
                    SharpSerializer sh = new SharpSerializer(true);
                    Logs = sh.Deserialize(fs) as List<LogFile>;
                    fs.Close();
                }
                return true;
            }
            catch { return false; }
        }

        public bool Save()
        {
            try
            {
                using (FileStream fs = new FileStream(_filepath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read))
                {
                    SharpSerializer sh = new SharpSerializer(true);
                    sh.Serialize(Logs, fs);
                    fs.Close();
                }
                return true;
            }
            catch { return false; }
        }

        public void AddLog(LogFile log)
        {
            log.Added = DateTime.Now;
            Logs.Add(log);
            Save();
            NewlogAdded(log);
        }

        public event NewLogHandler OnNewLogAdded;

        public void NewlogAdded(LogFile log)
        {
            OnNewLogAdded?.Invoke(log, this);
        }
    }
}