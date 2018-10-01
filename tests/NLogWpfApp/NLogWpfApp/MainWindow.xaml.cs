using System.Windows;
using Common.Logging;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static int counter = 0;
        private static readonly ILog log = LogManager.GetLogger<MainWindow>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void FatalButton_Click(object sender, RoutedEventArgs e)
        {
            counter++;
            log.Fatal($"Fatal #{counter}");
        }

        private void ErrorButton_Click(object sender, RoutedEventArgs e)
        {
            counter++;
            log.Error($"Error #{counter}");
        }

        private void WarningButton_Click(object sender, RoutedEventArgs e)
        {
            counter++;
            log.Warn($"Warning #{counter}");
        }

        private void InfoButton_Click(object sender, RoutedEventArgs e)
        {
            counter++;
            log.Info($"Info #{counter}");
        }

        private void DebugButton_Click(object sender, RoutedEventArgs e)
        {
            counter++;
            log.Debug($"Debug #{counter}");
        }

        private void TraceButton_Click(object sender, RoutedEventArgs e)
        {
            counter++;
            log.Trace($"Trace #{counter}");
        }
    }
}
