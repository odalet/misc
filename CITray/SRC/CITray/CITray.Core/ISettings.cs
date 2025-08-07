using System;
using CITray.Settings;

namespace CITray
{
    public interface ISettings
    {
        GlobalSettings Global { get; }
    }
}
