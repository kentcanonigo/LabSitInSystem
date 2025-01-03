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
    /// Interaction logic for StudentDashboard.xaml
    /// </summary>
    public partial class StudentDashboard : Window
    {
        public string LoggedInStudentId { get; set; } // Store the logged-in student's ID
        public string StudentName { get; set; }

        public StudentDashboard(string studentId, string studentName) {
            InitializeComponent();
            StudentName = studentName;
            var firstName = studentName?.Split(' ')[0] ?? "Student";
            NameLabel.Content = $"Hello, {firstName}";
            LoggedInStudentId = studentId; // Set the logged-in student's ID
        }

        private void ChangePassButton_Click(object sender, RoutedEventArgs e) {
            // Ask the user for their old password and new password
            var oldPassword = OldPassword.Password; // Assume you have a PasswordBox named OldPasswordBox
            var newPassword = NewPassword.Password; // Assume you have a PasswordBox named NewPasswordBox

            if (string.IsNullOrWhiteSpace(oldPassword) || string.IsNullOrWhiteSpace(newPassword)) {
                MessageBox.Show("Both old and new passwords are required.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            using (var context = new AppDbContext()) {
                var currentStudent = context.Students.FirstOrDefault(s => s.StudentId == LoggedInStudentId); // Replace with actual logged-in student's ID

                if (currentStudent != null) {
                    // Verify the old password
                    if (!BCrypt.Net.BCrypt.Verify(oldPassword, currentStudent.PasswordHash)) {
                        MessageBox.Show("Incorrect old password.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    // Update the password
                    currentStudent.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
                    context.SaveChanges();

                    MessageBox.Show("Password changed successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else {
                    MessageBox.Show("Student not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }


        private void StartButton_Click(object sender, RoutedEventArgs e) {
            var sitInWindow = new SitInWindow(LoggedInStudentId, StudentName);
            this.Hide();
            sitInWindow.ShowDialog();
            this.Show();
        }


        private void LogOutButton_Click(object sender, RoutedEventArgs e) {
            var result = MessageBox.Show("Are you sure you want to log out?", "Confirm Logout", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes) {
                // Navigate back to the login window
                var loginWindow = new StudentLoginWindow(); // Replace with your login window class
                loginWindow.Show();

                // Close the current dashboard
                Window.GetWindow(this)?.Close();
            }
        }

    }
}
