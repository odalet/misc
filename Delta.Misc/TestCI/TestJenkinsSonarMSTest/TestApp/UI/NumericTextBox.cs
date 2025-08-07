using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace TestApp.UI
{
    public class NumericTextBox : TextBox
    {
        public NumericTextBox() : base()
        {
            // See https://karlhulme.wordpress.com/2007/02/15/masking-input-to-a-wpf-textbox/
            PreviewTextInput += (s, e) => e.Handled = !IsTextValid(e.Text);
            AddHandler(DataObject.PastingEvent, new DataObjectPastingEventHandler((s, e) =>
            {
                if (e.DataObject.GetDataPresent(typeof(string)))
                {
                    var text = (string)e.DataObject.GetData(typeof(string));
                    if (!IsTextValid(text)) e.CancelCommand();
                }
                else e.CancelCommand();

            }));
        }

        private static bool IsTextValid(string text)
        {
            var regex = new Regex("[^0-9.-]+"); // regex that matches disallowed text
            return !regex.IsMatch(text);
        }
    }
}
