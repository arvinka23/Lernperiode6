using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace budgetplanner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        {
            private string _connectionString = "Data Source=budget.db;Version=3;";
            private double _total = 0;

            public MainWindow()
            {
                InitializeComponent();
                LoadTransactions();
            }

            // Event handler for adding a transaction
            private void AddTransaction_Click(object sender, RoutedEventArgs e)
            {
                // Get the input values
                string description = DescriptionTextBox.Text;
                if (double.TryParse(AmountTextBox.Text, out double amount))
                {
                    // Insert into the database
                    using (var connection = new SQLiteConnection(_connectionString))
                    {
                        connection.Open();
                        // var command = new SQLiteCommand("INSERT INTO Transactions (Description, Amount) VALUES (@description, @amount)", connection);
                        command.Parameters.AddWithValue("@description", description);
                        command.Parameters.AddWithValue("@amount", amount);
                        command.ExecuteNonQuery();
                    }

                    // Update UI
                    _total += amount;
                    TransactionListBox.Items.Add($"{description} - ${amount}");
                    TotalLabel.Content = $"Total: ${_total}";

                    // Clear inputs
                    AmountTextBox.Clear();
                    DescriptionTextBox.Clear();
                }
                else
                {
                    MessageBox.Show("Please enter a valid amount.");
                }
            }

            // Load existing transactions from the database
            private void LoadTransactions()
            {
                using (var connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();
                    var command = new SQLiteCommand("SELECT Description, Amount FROM Transactions", connection);
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        string description = reader.GetString(0);
                        double amount = reader.GetDouble(1);

                        // Update UI and total
                        TransactionListBox.Items.Add($"{description} - ${amount}");
                        _total += amount;
                    }
                    TotalLabel.Content = $"Total: ${_total}";
                }
            }
        }
    }

}