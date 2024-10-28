using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace TextFormatter
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BoldButton_Click(object sender, RoutedEventArgs e)
        {
            ToggleTextFormatting(Inline.FontWeightProperty, FontWeights.Bold);
        }

        private void ItalicButton_Click(object sender, RoutedEventArgs e)
        {
            ToggleTextFormatting(Inline.FontStyleProperty, FontStyles.Italic);
        }

        private void UnderlineButton_Click(object sender, RoutedEventArgs e)
        {
            ToggleTextFormatting(Inline.TextDecorationsProperty, TextDecorations.Underline);
        }

        private void IncreaseFontSize_Click(object sender, RoutedEventArgs e)
        {
            ChangeFontSize(2);
        }

        private void DecreaseFontSize_Click(object sender, RoutedEventArgs e)
        {
            ChangeFontSize(-2);
        }

        private void FontColorComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FontColorComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                Color color = (Color)ColorConverter.ConvertFromString(selectedItem.Tag.ToString());
                RichTextEditor.Selection.ApplyPropertyValue(TextElement.ForegroundProperty, new SolidColorBrush(color));
            }
        }

        private void FontSizeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FontSizeComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                double fontSize = double.Parse(selectedItem.Tag.ToString());
                RichTextEditor.Selection.ApplyPropertyValue(Inline.FontSizeProperty, fontSize);
            }
        }

        private void ToggleTextFormatting(DependencyProperty property, object value)
        {
            if (RichTextEditor.Selection.IsEmpty)
            {
                RichTextEditor.Selection.ApplyPropertyValue(property, value);
            }
            else
            {
                var currentValue = RichTextEditor.Selection.GetPropertyValue(property);
                if (currentValue.Equals(value))
                {
                    RichTextEditor.Selection.ApplyPropertyValue(property, null);
                }
                else
                {
                    RichTextEditor.Selection.ApplyPropertyValue(property, value);
                }
            }
        }

        private void ChangeFontSize(double change)
        {
            if (RichTextEditor.Selection.IsEmpty)
            {
                return;
            }

            var currentSize = RichTextEditor.Selection.GetPropertyValue(Inline.FontSizeProperty);
            double newSize = currentSize is double size ? size + change : 12; // начальный размер шрифта
            RichTextEditor.Selection.ApplyPropertyValue(Inline.FontSizeProperty, newSize);
        }
    }
}