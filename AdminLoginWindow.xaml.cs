using LaboratorySitInSystem;
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
using System.Windows.Shapes;

namespace LabSitInSystem
{
    /// <summary>
    /// Interaction logic for AdminLoginWindow.xaml
    /// </summary>
    public partial class AdminLoginWindow : Window
    {
        public AdminLoginWindow()
        {
            InitializeComponent();
        }

        private void LogInButton_Click(object sender, RoutedEventArgs e) {
            var email = EmailAddress.Text; // Updated to use EmailAddress field
            var password = Password.Password; // Updated to use Password field

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password)) {
                MessageBox.Show("Please enter both email and password.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Validate credentials
            using (var context = new AppDbContext()) {

                var user = context.Users.FirstOrDefault(u => u.Username == email); // Updated to match by email
                if (user != null && BCrypt.Net.BCrypt.Verify(password, user.PasswordHash)) {
                    var mainWindow = new MainWindow();
                    mainWindow.Show();
                    this.Close(); // Close the login window
                }
                else {
                    MessageBox.Show("Invalid email or password.", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void LoginAsStudentButton_Click(object sender, RoutedEventArgs e) {
            var studentLoginWindow = new StudentLoginWindow();
            studentLoginWindow.Show();
            this.Close();
        }
    }
}
