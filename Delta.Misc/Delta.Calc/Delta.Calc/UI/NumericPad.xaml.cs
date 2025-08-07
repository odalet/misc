using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Delta.Calc.UI
{
    /// <summary>
    /// Interaction logic for NumericPad.xaml
    /// </summary>
    public partial class NumericPad : UserControl
    {
        public NumericPad()
        {
            InitializeComponent();
            CreateControls();
        }

        private void CreateControls()
        {
            for (int i = 0; i < 5; i++)
                rootGrid.RowDefinitions.Add(new RowDefinition());
            for (int i = 0; i < 5; i++)
                rootGrid.ColumnDefinitions.Add(new ColumnDefinition());

            // Create buttons
            CreateButton(SpecialStrings.Back, 0, 0);
            CreateButton("CE", 0, 1);
            CreateButton("C", 0, 2);
            CreateButton(SpecialStrings.PlusOrMinus, 0, 3);
            CreateButton(SpecialStrings.Sqrt, 0, 4);

            CreateButton("7", 1, 0);
            CreateButton("8", 1, 1);
            CreateButton("9", 1, 2);
            CreateButton("/", 1, 3);
            CreateButton("%", 1, 4);

            CreateButton("4", 2, 0);
            CreateButton("5", 2, 1);
            CreateButton("6", 2, 2);
            CreateButton("*", 2, 3);
            CreateButton("1/x", 2, 4);

            CreateButton("1", 3, 0);
            CreateButton("2", 3, 1);
            CreateButton("3", 3, 2);
            CreateButton("-", 3, 3);
            CreateButton("=", 3, 4, 2, 0);

            CreateButton("0", 4, 0, 0, 2);
            CreateButton(".", 4, 2);
            CreateButton("+", 4, 3);
        }

        private void CreateButton(string label, int row, int column, int rowspan = 0, int colspan = 0)
        {
            // 1 & 0 have the same effect, but we want the span value be 1 for margins computation
            if (rowspan == 0) rowspan = 1;
            if (colspan == 0) colspan = 1;

            var button = new Button() { Content = label };
            
            button.SetValue(Grid.RowProperty, row);
            button.SetValue(Grid.ColumnProperty, column);
            if (rowspan > 1)
                button.SetValue(Grid.RowSpanProperty, rowspan);
            if (colspan > 1)
                button.SetValue(Grid.ColumnSpanProperty, colspan);

            // Margins
            const double margin = 2.0;
            const int maxr = 4; // max row number
            const int maxc = 4; // max column number

            double l = margin, r = margin, t = margin, b = margin;
            
            if (row == 0) t = 0.0;
            if (row + rowspan == maxr + 1) b = 0.0;
            if (column == 0) l = 0.0;
            if (column + colspan == maxc + 1) r = 0.0;

            button.Margin = new Thickness(l, t, r, b);
            

            rootGrid.Children.Add(button);
        }
    }
}
