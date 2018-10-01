using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using NLog;
using NLog.Targets;

namespace WpfApp1
{
    // From http://dotnetsolutionsbytomi.blogspot.com/2011/06/creating-awesome-logging-control-with.html
    public class MemoryEventTarget : Target
    {
        public event Action<LogEventInfo> EventReceived;

        /// <summary>
        /// Notifies listeners about new event
        /// </summary>
        /// <param name="logEvent">The logging event.</param>
        protected override void Write(LogEventInfo logEvent) =>
            EventReceived?.Invoke(logEvent);
    }

    public class LogItemBgColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ("Warn" == value.ToString()) return Brushes.Yellow;
            if ("Error" == value.ToString()) return Brushes.Tomato;
            return Brushes.White;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            throw new NotImplementedException();
    }

    public class LogItemFgColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ("Error" == value.ToString())
                return Brushes.Black;
            return Brushes.Black;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            throw new NotImplementedException();
    }
}
