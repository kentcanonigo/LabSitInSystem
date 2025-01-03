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
    /// Interaction logic for AddStudentDialog.xaml
    /// </summary>
    public partial class AddStudentDialog : Window
    {
        private bool _isEditMode = false;
        private Student _editingStudentId;

        public AddStudentDialog() {
            InitializeComponent();
        }

        public AddStudentDialog(Student studentToEdit) {
            InitializeComponent();
            _isEditMode = true;
            _editingStudentId = studentToEdit;
            FillEditFields(studentToEdit);
        }

        private void FillEditFields(Student student) {
            FullName.Text = student.FullName;
            Course.Text = student.Program;
            YearLevel.Text = student.Year;
            Schedule.Text = student.Section;
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e) {
            // Validate the inputs
            if (string.IsNullOrWhiteSpace(FullName.Text) ||
                string.IsNullOrWhiteSpace(Course.Text) ||
                string.IsNullOrWhiteSpace(YearLevel.Text) ||
                string.IsNullOrWhiteSpace(Schedule.Text)) {
                MessageBox.Show("All fields are required.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            using (var context = new AppDbContext()) {
                if (_isEditMode && _editingStudentId != null) {
                    // Update existing student
                    var student = context.Students.FirstOrDefault(s => s.StudentId == _editingStudentId.StudentId);
                    if (student != null) {
                        student.StudentId = IDNumber.Text;
                        student.FullName = FullName.Text;
                        student.Program = Course.Text;
                        student.Year = YearLevel.Text;
                        student.Section = Schedule.Text;

                        context.SaveChanges();
                        MessageBox.Show("Student updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else {
                    // Add new student
                    if (!int.TryParse(IDNumber.Text, out int parsedId)) {
                        MessageBox.Show("Invalid ID Number. Please enter a valid integer.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    var defaultPassword = parsedId.ToString(); // Default password is the student ID number
                    var passwordHash = BCrypt.Net.BCrypt.HashPassword(defaultPassword);

                    var newStudent = new Student {
                        StudentId = IDNumber.Text,
                        FullName = FullName.Text,
                        Program = Course.Text,
                        Year = YearLevel.Text,
                        Section = Schedule.Text,
                        PasswordHash = passwordHash // Set hashed password
                    };

                    context.Students.Add(newStudent);
                    context.SaveChanges();
                    MessageBox.Show($"Student added successfully! Default password is their ID number ({defaultPassword}).", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            this.Close();
        }



        private void CancelButton_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }
    }
}
