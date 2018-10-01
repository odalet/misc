using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using NLog;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for LoggingControl.xaml
    /// </summary>
    public partial class LoggingControl : UserControl
    {
        private readonly MemoryEventTarget _logTarget;  // My new custom Target (code is attached here MemoryQueue.cs)
        public static ObservableCollection<LogEventInfo> LogCollection { get; set; }


        public LoggingControl()
        {
            LogCollection = new ObservableCollection<LogEventInfo>();

            InitializeComponent();

            // init memory queue
            _logTarget = new MemoryEventTarget();
            _logTarget.EventReceived += EventReceived;
            NLog.Config.SimpleConfigurator.ConfigureForTargetLogging(_logTarget, LogLevel.Debug);
        }

        private void EventReceived(LogEventInfo message)
        {
            Dispatcher.Invoke(new Action(() => {
                if (LogCollection.Count >= 50) LogCollection.RemoveAt(LogCollection.Count - 1);
                LogCollection.Add(message);
            }));
        }
    }
}
