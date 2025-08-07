using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using TestLib;

namespace TestApp.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}

            AddCommand = new RelayCommand(() => Result = Operations.Add(X, Y));
            SubtractCommand = new RelayCommand(() => Result = Operations.Subtract(X, Y));
            MultiplyCommand = new RelayCommand(() => Result = Operations.Multiply(X, Y));
            DivideCommand = new RelayCommand(() => Result = Operations.Divide(X, Y));

        }

        public ICommand AddCommand { get; private set; } 
        public ICommand SubtractCommand { get; private set; } 
        public ICommand MultiplyCommand { get; private set; } 
        public ICommand DivideCommand { get; private set; } 

        private double x;
        public double X
        {
            get { return x; }
            set { Set(ref x, value); }
        }

        private double y;
        public double Y
        {
            get { return y; }
            set { Set(ref y, value); }
        }

        private double result;

        public double Result
        {
            get { return result; }
            set { Set(ref result, value); }
        }

    }
}