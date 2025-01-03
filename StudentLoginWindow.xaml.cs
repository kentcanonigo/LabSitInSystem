using LaboratorySitInSystem;
using System.Net.Mail;
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

namespace LabSitInSystem {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class StudentLoginWindow : Window {
        public StudentLoginWindow() {
            InitializeComponent();
        }

        private void LogInButton_Click(object sender, RoutedEventArgs e) {
            var idNumber = IDNumberBox.Text; // Updated to use EmailAddress field
            var password = PassBox.Password; // Updated to use Password field

            if (string.IsNullOrWhiteSpace(idNumber) || string.IsNullOrWhiteSpace(password)) {
                MessageBox.Show("Please enter both ID Number and Password.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Validate credentials
            using (var context = new AppDbContext()) {

                var user = context.Students.FirstOrDefault(u => u.StudentId == idNumber); // Updated to match by email
                if (user != null && BCrypt.Net.BCrypt.Verify(password, user.PasswordHash)) {
                    var mainWindow = new StudentDashboard(user.StudentId, user.FullName);
                    mainWindow.Show();
                    this.Close(); // Close the login window
                }
                else {
                    MessageBox.Show("Invalid ID Number or password.", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void LoginAsAdminButton_Click(object sender, RoutedEventArgs e) {
            var adminLoginWindow = new AdminLoginWindow();
            adminLoginWindow.Show();
            this.Close();
        }
    }
}