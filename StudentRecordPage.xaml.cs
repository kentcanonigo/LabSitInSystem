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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LabSitInSystem
{
    /// <summary>
    /// Interaction logic for StudentRecordPage.xaml
    /// </summary>
    public partial class StudentRecordPage : Page
    {
        public StudentRecordPage() {
            InitializeComponent();
            LoadStudentData();
        }

        public void LoadStudentData() {
            using (var context = new AppDbContext()) {
                var students = context.Students.ToList();
                StudentDataGrid.ItemsSource = students;
            }
        }

        private void AddStudentButton_Click(object sender, RoutedEventArgs e) {
            var addStudentDialog = new AddStudentDialog(); // Pass context or required data if necessary
            addStudentDialog.ShowDialog();

            // Reload the DataGrid after adding a student
            LoadStudentData();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e) {
            var button = sender as Button;
            if (button?.Tag is string studentId) {
                using (var context = new AppDbContext()) {
                    var studentToEdit = context.Students.FirstOrDefault(s => s.StudentId == studentId);
                    if (studentToEdit != null) {
                        var editStudentDialog = new AddStudentDialog(studentToEdit); // Pass the student to edit
                        editStudentDialog.ShowDialog();

                        // Reload the DataGrid after editing
                        LoadStudentData();
                    }
                }
            }
        }


        private void DeleteButton_Click(object sender, RoutedEventArgs e) {
            var button = sender as Button;
            if (button?.Tag is string studentId) {
                var result = MessageBox.Show($"Are you sure you want to delete student ID {studentId}?",
                                             "Confirm Deletion", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes) {
                    using (var context = new AppDbContext()) {
                        var studentToDelete = context.Students.FirstOrDefault(s => s.StudentId == studentId);
                        if (studentToDelete != null) {
                            context.Students.Remove(studentToDelete);
                            context.SaveChanges();

                            // Reload the DataGrid after deletion
                            LoadStudentData();
                        }
                    }
                }
            }
        }


        // CRUD methods
        private void LoadStudents() {
            using (var context = new AppDbContext()) {
                var students = context.Students.ToList();
                StudentDataGrid.ItemsSource = students;
            }
        }
    }
}
