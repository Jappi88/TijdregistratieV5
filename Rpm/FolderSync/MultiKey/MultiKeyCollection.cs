﻿using System;
using System.Collections.Generic;
using FolderSync.Reflection;

namespace FolderSync.MultiKey
{
    public class MultiKeyCollection<T>
        where T : ReflectionObject
    {
        protected string _Delimiter = "#";
        protected Dictionary<string, T> _Keys = new();
        protected string[] _Properties = { };
        public List<FileOperation> Operations = new();

        public MultiKeyCollection(string[] properties)
        {
            _Properties = properties;
        }

        public string GetKey(T obj)
        {
            var key = string.Empty;
            foreach (var property in _Properties)
            {
                var propValue = obj.GetProperty(property).ToString();
                if (propValue.IndexOf(_Delimiter, StringComparison.Ordinal) >= 0)
                    throw new Exception(
                        $"Cannot create key from this object, property value of this object '{propValue}' contains '#'.");
                key += propValue + _Delimiter;
            }

            if (key.EndsWith(_Delimiter) && key.Length != 1) key = key.Substring(0, key.Length - 1);
            return key;
        }

        public T GetAddObject(T obj)
        {
            var key = GetKey(obj);
            if (_Keys.ContainsKey(key))
                return _Keys[key];
            _Keys.Add(key, obj);
            return obj;
        }

        public FileOperation GetAddFileSyncEntry(FolderSynchronizationItemFile item)
        {
            try
            {
                if (item == null || string.IsNullOrEmpty(item.SourceFileName) ||
                    string.IsNullOrEmpty(item.DestinationFileName)) return null;
                if (Operations == null) Operations = new List<FileOperation>();
                var index = -1;
                if ((index = Operations.IndexOf(item)) > -1)
                    return Operations[index];
                Operations.Add(item);
                return item;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public FileOperation GetAddFolderSyncEntry(FolderSynchronizationItemFolder item)
        {
            try
            {
                if (item == null || string.IsNullOrEmpty(item.SourceFileName)) return null;
                if (Operations == null) Operations = new List<FileOperation>();
                var index = -1;
                if ((index = Operations.IndexOf(item)) > -1)
                    return Operations[index];
                Operations.Add(item);
                return item;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public T GetObjectByKey(string key)
        {
            if (_Keys.ContainsKey(key))
                return _Keys[key];
            return null;
        }

        #region Properties

        //public IList<string> Keys => _Keys.Keys.ToList<string>();

        //public IList<T> Objects => _Keys.Values.ToList<T>();

        #endregion
    }
}