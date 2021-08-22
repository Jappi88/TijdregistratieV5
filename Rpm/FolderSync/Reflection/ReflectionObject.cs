using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Reflection;

namespace FolderSync.Reflection
{
    public class ReflectionObject
    {
        public void SetProperty(string propertyName, object value)
        {
            PropertyInfo pi = this.GetType().GetProperty(propertyName);
            pi.SetValue(this, value, System.Reflection.BindingFlags.SetProperty, null, new object[] { }, null);
        }

        public object GetProperty(string propertyName)
        {
            PropertyInfo pi = this.GetType().GetProperty(propertyName);
            object value = pi.GetValue(this, null);
            return value;
        }
    }
}
