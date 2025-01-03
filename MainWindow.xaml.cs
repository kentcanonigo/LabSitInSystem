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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void StudentRecordsClick(object sender, MouseButtonEventArgs e) {
            Main.Content = new StudentRecordPage();
            Box1.Fill = new SolidColorBrush(Colors.White);
            StudentRecordsButton.Foreground = new SolidColorBrush(Colors.Black);
            StudentLogButton.Foreground = new SolidColorBrush(Colors.White);
            Box2.Fill = new SolidColorBrush(Colors.Transparent);
        }

        private void StudentLogClick(object sender, MouseButtonEventArgs e) {
            Main.Content = new StudentLogPage();
            Box1.Fill = new SolidColorBrush(Colors.Transparent);
            StudentRecordsButton.Foreground = new SolidColorBrush(Colors.White);
            StudentLogButton.Foreground = new SolidColorBrush(Colors.Black);
            Box2.Fill = new SolidColorBrush(Colors.White);
        }

        private void LogOutButton_Click(object sender, RoutedEventArgs e) {
            // TODO: Handle logging out
            var studentLogInWindow = new StudentLoginWindow();
            studentLogInWindow.Show();
            this.Close();
        }
    }
}
