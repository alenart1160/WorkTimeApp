using WorkTimeApp.Shared.Services;
using WorkTime.Abstractions;
namespace WorkTimeApp.Services;



public class PlatformService : IPlatformInfo
{
    public string GetBaseUrl()
    {
        if (DeviceInfo.Platform == DevicePlatform.Android)
        {
            return "http://10.0.2.2:5076";
        }
        else if (DeviceInfo.Platform == DevicePlatform.WinUI)
        {
            return "http://localhost:5076";
        }
        else
        {
            return "http://localhost:5076";
        }
    }
}