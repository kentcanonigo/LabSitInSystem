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
using System.Windows.Threading;

namespace LabSitInSystem
{
    /// <summary>
    /// Interaction logic for SitInWindow.xaml
    /// </summary>
    public partial class SitInWindow : Window
    {
        private DispatcherTimer _timer;
        private TimeSpan _remainingTime;

        public string LoggedInStudentId { get; }

        public SitInWindow(string loggedInStudentId, string studentName)
        {
            InitializeComponent();

            NameLabel.Content = studentName;
            IDLabel.Content = loggedInStudentId;

            // Get the screen's working area (excluding taskbar)
            var workingArea = SystemParameters.WorkArea;

            // Position the window in the top-right corner
            this.Left = workingArea.Right - this.Width; // Align to the right
            this.Top = workingArea.Top;                // Align to the top

            // Ensure always on top
            this.Topmost = true;

            LoggedInStudentId = loggedInStudentId;
            StartSessionRequest();
        }

        private void StartSessionRequest() {
            StatusLabel.Content = "Waiting for approval";
            using (var context = new AppDbContext()) {
                // Check if there's already an active session
                var activeSession = context.SitIns.FirstOrDefault(s => s.StudentId == LoggedInStudentId && s.TimeOut == null);
                if (activeSession != null) {
                    MessageBox.Show("You already have an active session.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Create a new session request
                var newSitIn = new SitIn {
                    StudentId = LoggedInStudentId,
                    ApprovedByAdmin = false // Not approved yet
                };

                context.SitIns.Add(newSitIn);
                context.SaveChanges();

                // Start polling for approval
                StartPollingForApproval();

                MessageBox.Show("Session request sent. Please wait for admin approval.", "Request Sent", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private DispatcherTimer _pollingTimer;

        private void StartPollingForApproval() {
            _pollingTimer = new DispatcherTimer {
                Interval = TimeSpan.FromSeconds(1) // Check every 1 second
            };

            _pollingTimer.Tick += CheckApprovalStatus;
            _pollingTimer.Start();
        }

        private void CheckApprovalStatus(object sender, EventArgs e) {
            using (var context = new AppDbContext()) {
                var pendingSession = context.SitIns.FirstOrDefault(s => s.StudentId == LoggedInStudentId && s.TimeOut == null);

                if (pendingSession != null && pendingSession.ApprovedByAdmin) {
                    _pollingTimer.Stop(); // Stop polling
                    StatusLabel.Content = "Lab Session Active";
                    StatusBar.Fill = new SolidColorBrush(Colors.Green);
                    StartTimer(); // Start the session timer
                }
            }
        }


        private void StartTimer() {
            using (var context = new AppDbContext()) {
                // Fetch the existing session request that is not yet approved
                var activeSession = context.SitIns.FirstOrDefault(s => s.StudentId == LoggedInStudentId && s.TimeOut == null && s.ApprovedByAdmin);
                if (activeSession == null) {
                    MessageBox.Show("No approved session found. Please wait for admin approval.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Update the session with the start time (if necessary)
                if (activeSession.TimeIn == default) {
                    activeSession.TimeIn = DateTime.Now;
                    context.SaveChanges();
                }

                // Initialize remaining time
                _remainingTime = TimeSpan.FromMinutes(59).Add(TimeSpan.FromSeconds(59)); // 59:59

                // Start the timer
                _timer = new DispatcherTimer {
                    Interval = TimeSpan.FromSeconds(1) // Update every second
                };
                _timer.Tick += Timer_Tick;
                _timer.Start();
            }
        }


        private void EndSessionButton_Click(object sender, RoutedEventArgs e) {
            EndSession();
            TimeLeftLabel.Content = "Session ended.";
            this.Close();
        }

        private void Timer_Tick(object sender, EventArgs e) {
            if (_remainingTime > TimeSpan.Zero) {
                _remainingTime = _remainingTime.Subtract(TimeSpan.FromSeconds(1));
                TimerLabel.Content = $"{_remainingTime.Minutes:D2}:{_remainingTime.Seconds:D2}";

                // Notify the user when 20 minutes remain
                if (_remainingTime == TimeSpan.FromMinutes(20)) {
                    MessageBox.Show("20 minutes remaining in your session.", "Time Alert", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                // Notify the user when 10 minutes remain
                if (_remainingTime == TimeSpan.FromMinutes(10)) {
                    MessageBox.Show("10 minutes remaining in your session.", "Time Alert", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else {
                // Time is up
                _timer.Stop();
                TimeLeftLabel.Content = "Time is up!";
                EndSession(); // Automatically end the session
            }
        }



        private void EndSession() {
            using (var context = new AppDbContext()) {
                var activeSession = context.SitIns.FirstOrDefault(s => s.StudentId == LoggedInStudentId && s.TimeOut == null);

                if (activeSession != null) {
                    if (!activeSession.ApprovedByAdmin) {
                        // Delete the pending session if it's not approved
                        context.SitIns.Remove(activeSession);
                        MessageBox.Show("Session request canceled.", "Session Canceled", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else {
                        // End the approved session
                        activeSession.TimeOut = DateTime.Now;
                        activeSession.Duration = (int)(activeSession.TimeOut - activeSession.TimeIn)?.TotalMinutes;
                        MessageBox.Show("Session ended successfully.", "Session Ended", MessageBoxButton.OK, MessageBoxImage.Information);
                    }

                    context.SaveChanges();
                }
            }

            // Stop the timer if it's running
            _timer?.Stop();
            _timer = null;
        }


    }
}
