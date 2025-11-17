using System;
using System.Windows;
using System.Windows.Threading;
using Hospital_MamSys_LIB.Model;

namespace Hospital_MamSys_GUI
{
    public partial class MainWindow : Window
    {
        private User _currentUser;
        private DispatcherTimer _timer;

        public MainWindow(User user)
        {
            InitializeComponent();
            _currentUser = user;
            InitializeUI();
            StartClock();
        }

        private void InitializeUI()
        {
            // Set user information
            txtWelcome.Text = $"Welcome, {_currentUser.Username}";
            txtUserRole.Text = $"Role: {GetRoleDisplayName(_currentUser.Role)}";
            txtCurrentUser.Text = $"Current User: {_currentUser.Username} ({GetRoleDisplayName(_currentUser.Role)})";

            // Set permissions based on role
            SetPermissionsByRole(_currentUser.Role);
        }

        private string GetRoleDisplayName(string role)
        {
            switch (role)
            {
                case "Admin":
                    return "Administrator";
                case "Doctor":
                    return "Doctor";
                case "Nurse":
                    return "Nurse";
                case "Receptionist":
                    return "Receptionist";
                default:
                    return "User";
            }
        }

        private void SetPermissionsByRole(string role)
        {
            // Admin has all permissions
            if (role == "Admin")
            {
                btnUserManagement.Visibility = Visibility.Visible;
                btnDepartmentManagement.Visibility = Visibility.Visible;
            }
            else
            {
                // Regular users can only access basic modules
                btnUserManagement.Visibility = Visibility.Collapsed;
                btnDepartmentManagement.Visibility = Visibility.Collapsed;
            }
        }

        private void StartClock()
        {
            // Start clock display
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += (s, e) =>
            {
                txtDateTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            };
            _timer.Start();
            txtDateTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        private void BtnPatientManagement_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                PatientManagementWindow window = new PatientManagementWindow();
                window.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to open Patient Management window: {ex.Message}", "Error", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnDoctorManagement_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DoctorManagementWindow window = new DoctorManagementWindow();
                window.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to open Doctor Management window: {ex.Message}", "Error", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnAppointmentManagement_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AppointmentManagementWindow window = new AppointmentManagementWindow();
                window.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to open Appointment Management window: {ex.Message}", "Error", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnDepartmentManagement_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DepartmentManagementWindow window = new DepartmentManagementWindow();
                window.ShowDialog();
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show($"Failed to open Department Management window: {ex.Message}", "Error",
                    CustomMessageBox.MessageBoxButton.OK, CustomMessageBox.MessageBoxIcon.Error);
            }
        }

        private void BtnUserManagement_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                UserManagementWindow window = new UserManagementWindow();
                window.ShowDialog();
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show($"Failed to open User Management window: {ex.Message}", "Error",
                    CustomMessageBox.MessageBoxButton.OK, CustomMessageBox.MessageBoxIcon.Error);
            }
        }

        private void BtnLogout_Click(object sender, RoutedEventArgs e)
        {
            var result = CustomMessageBox.Show("Are you sure you want to logout?", "Confirm", 
                CustomMessageBox.MessageBoxButton.YesNo, CustomMessageBox.MessageBoxIcon.Question);
            
            if (result == CustomMessageBox.MessageBoxResult.Yes)
            {
                _timer?.Stop();
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.Show();
                this.Close();
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            _timer?.Stop();
        }
    }
}
