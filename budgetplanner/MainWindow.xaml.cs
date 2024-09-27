using System.Windows;

namespace BudgetTracker
{
    public partial class MainWindow : Window
    {
        private double _total = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        // Event handler for adding a transaction
        private void AddTransaction_Click(object sender, RoutedEventArgs e)
        {
            string description = DescriptionTextBox.Text;

            // Validate amount input
            if (double.TryParse(AmountTextBox.Text, out double amount))
            {
                // Simulate adding transaction to the UI
                _total += amount;
                TransactionListBox.Items.Add($"{description} - ${amount}");
                TotalLabel.Content = $"Total: ${_total}";

                // Clear input fields
                AmountTextBox.Clear();
                DescriptionTextBox.Clear();
            }
            else
            {
                MessageBox.Show("Please enter a valid amount.");
            }
        }
    }
}
