using System.Windows;

namespace RecipeManager
{
    public partial class PromptWindow : Window
    {
        public string ResponseText => ResponseTextBox.Text;

        public PromptWindow(string prompt)
        {
            InitializeComponent();
            PromptTextBlock.Text = prompt;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
