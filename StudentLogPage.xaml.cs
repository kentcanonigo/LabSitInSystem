using LaboratorySitInSystem;
using Microsoft.EntityFrameworkCore;
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
    /// Interaction logic for StudentLogPage.xaml
    /// </summary>
    public partial class StudentLogPage : Page
    {
        public StudentLogPage()
        {
            InitializeComponent();
            LoadSitInData();
            LoadPendingRequests();
        }

        private void LoadSitInData() {
            using (var context = new AppDbContext()) {
                // Fetch only approved sit-ins
                var approvedSitIns = context.SitIns
                                            .Include(s => s.Student) // Include related student details
                                            .Where(s => s.ApprovedByAdmin) // Filter by approved sessions
                                            .ToList();

                // Bind to the DataGrid
                SitInsDataGrid.ItemsSource = approvedSitIns;
            }
        }


        private void ApproveButton_Click(object sender, RoutedEventArgs e) {
            if (PendingRequestsDataGrid.SelectedItem is SitIn selectedSession) {
                ApproveSession(selectedSession.SitInId);
            }
            else {
                MessageBox.Show("Please select a session to approve.", "No Selection", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void ApproveSession(int sitInId) {
            using (var context = new AppDbContext()) {
                var session = context.SitIns.FirstOrDefault(s => s.SitInId == sitInId);
                if (session != null) {
                    session.ApprovedByAdmin = true;
                    context.SaveChanges();

                    MessageBox.Show("Session approved successfully.", "Approval Complete", MessageBoxButton.OK, MessageBoxImage.Information);

                    // Reload both DataGrids
                    LoadSitInData();
                    LoadPendingRequests();
                }
            }
        }

        private void LoadPendingRequests() {
            using (var context = new AppDbContext()) {
                // Fetch pending approval requests and include the related Student entity
                var pendingRequests = context.SitIns
                                             .Include(s => s.Student) // Ensure Student data is loaded
                                             .Where(s => !s.ApprovedByAdmin && s.TimeOut == null)
                                             .ToList();

                // Bind to the PendingRequestsDataGrid
                PendingRequestsDataGrid.ItemsSource = pendingRequests;
            }
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e) {
            // Reload both DataGrids
            LoadSitInData();
            LoadPendingRequests();
        }
    }
}
