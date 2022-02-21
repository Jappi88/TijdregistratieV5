using System.Reflection;

namespace FolderSync.Reflection
{
    public class ReflectionObject
    {
        public void SetProperty(string propertyName, object value)
        {
            var pi = GetType().GetProperty(propertyName);
            pi.SetValue(this, value, BindingFlags.SetProperty, null, new object[] { }, null);
        }

        public object GetProperty(string propertyName)
        {
            var pi = GetType().GetProperty(propertyName);
            var value = pi.GetValue(this, null);
            return value;
        }
    }
}