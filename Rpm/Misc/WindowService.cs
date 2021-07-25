using System;
using System.Globalization;
using System.Management;
using System.ServiceProcess;
using System.Threading.Tasks;

namespace Rpm.Misc
{
    public static class WindowService
    {
        public static async Task<bool> StartService(string serviceName, int timeoutMilliseconds)
        {
            return await Task<bool>.Factory.StartNew(() =>
            {
                try
                {
                    var service = new ServiceController(serviceName);
                    var valid = true;
                    if (service.StartType != ServiceStartMode.Manual)
                        valid = ChangeStartupType("NetTcpPortSharing", "Manual").Result;
                    if (valid && service.Status != ServiceControllerStatus.Running)
                    {
                        var timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds);

                        service.Start();
                        service.WaitForStatus(ServiceControllerStatus.Running, timeout);
                    }

                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            });
        }

        public static bool IsRunning(string name)
        {
            try
            {
                var service = new ServiceController(name);
                return service.Status == ServiceControllerStatus.Running;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static async Task<bool> StopService(string serviceName, int timeoutMilliseconds)
        {
            return await Task<bool>.Factory.StartNew(() =>
            {
                try
                {
                    var service = new ServiceController(serviceName);
                    var timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds);

                    service.Stop();
                    service.WaitForStatus(ServiceControllerStatus.Stopped, timeout);
                    return true;
                }
                catch
                {
                    return false;
                }
            });
        }

        public static async Task<bool> RestartService(string serviceName, int timeoutMilliseconds)
        {
            return await Task<bool>.Factory.StartNew(() =>
            {
                try
                {
                    var service = new ServiceController(serviceName);
                    var millisec1 = Environment.TickCount;
                    var timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds);

                    service.Stop();
                    service.WaitForStatus(ServiceControllerStatus.Stopped, timeout);

                    // count the rest of the timeout
                    var millisec2 = Environment.TickCount;
                    timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds - (millisec2 - millisec1));

                    service.Start();
                    service.WaitForStatus(ServiceControllerStatus.Running, timeout);
                    return true;
                }
                catch
                {
                    return false;
                }
            });
        }

        public static async Task<bool> ChangeStartupType(string serviceName, string startupType)
        {
            return await Task.Factory.StartNew(() =>
            {
                const string MethodName = "ChangeStartMode";
                var path = new ManagementPath
                {
                    Server = ".",
                    NamespacePath = @"root\CIMV2",
                    RelativePath = string.Format(
                        CultureInfo.InvariantCulture,
                        "Win32_Service.Name='{0}'",
                        serviceName)
                };
                using var serviceObject = new ManagementObject(path);
                var inputParameters = serviceObject.GetMethodParameters(MethodName);
                inputParameters["startmode"] = startupType;
                var outputParameters =
                    serviceObject.InvokeMethod(MethodName, inputParameters, null);
                return outputParameters != null && (uint) outputParameters.Properties["ReturnValue"].Value == 0;
            });
        }
    }
}