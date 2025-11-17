using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Hospital_MamSys_LIB.DAL;
using Hospital_MamSys_LIB.Model;

namespace Hospital_MamSys_GUI
{
    public partial class AppointmentManagementWindow : Window
    {
        private DALAppointment _dalAppointment;
        private List<Appointment> _allAppointments;

        public AppointmentManagementWindow()
        {
            InitializeComponent();
            _dalAppointment = new DALAppointment();
            LoadAppointments();
        }

        private void LoadAppointments()
        {
            try
            {
                _allAppointments = _dalAppointment.GetAllAppointments();
                dgAppointments.ItemsSource = _allAppointments;
                txtCount.Text = $"Total {_allAppointments.Count} records";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load appointment list: {ex.Message}", "Error", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DgAppointments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgAppointments.SelectedItem is Appointment appointment)
            {
                DisplayAppointment(appointment);
            }
        }

        private void DisplayAppointment(Appointment appointment)
        {
            txtAppointmentID.Text = appointment.AppointmentID.ToString();
            txtPatientID.Text = appointment.PatientID.ToString();
            txtDoctorID.Text = appointment.DoctorID.ToString();
            dpAppointmentDate.SelectedDate = appointment.AppointmentDate;
            txtAppointmentTime.Text = appointment.AppointmentTime.ToString(@"hh\:mm");
            
            // Set status
            foreach (ComboBoxItem item in cmbStatus.Items)
            {
                if (item.Content.ToString() == appointment.Status)
                {
                    cmbStatus.SelectedItem = item;
                    break;
                }
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Validate input
                if (string.IsNullOrWhiteSpace(txtPatientID.Text) || !int.TryParse(txtPatientID.Text, out int patientId))
                {
                    MessageBox.Show("Please enter a valid Patient ID", "Validation Failed", 
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtDoctorID.Text) || !int.TryParse(txtDoctorID.Text, out int doctorId))
                {
                    MessageBox.Show("Please enter a valid Doctor ID", "Validation Failed", 
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!dpAppointmentDate.SelectedDate.HasValue)
                {
                    MessageBox.Show("Please select appointment date", "Validation Failed", 
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!TimeSpan.TryParse(txtAppointmentTime.Text, out TimeSpan appointmentTime))
                {
                    MessageBox.Show("Please enter a valid time format (e.g. 09:00)", "Validation Failed", 
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (cmbStatus.SelectedItem == null)
                {
                    MessageBox.Show("Please select status", "Validation Failed", 
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Create new appointment
                Appointment newAppointment = new Appointment
                {
                    PatientID = patientId,
                    DoctorID = doctorId,
                    AppointmentDate = dpAppointmentDate.SelectedDate.Value,
                    AppointmentTime = appointmentTime,
                    Status = (cmbStatus.SelectedItem as ComboBoxItem).Content.ToString()
                };

                _dalAppointment.AddAppointment(newAppointment);
                MessageBox.Show("Appointment added successfully!", "Success", 
                    MessageBoxButton.OK, MessageBoxImage.Information);
                
                ClearForm();
                LoadAppointments();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to add appointment: {ex.Message}", "Error", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtAppointmentID.Text))
                {
                    MessageBox.Show("Please select an appointment to update", "Information", 
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                // Validate input
                if (string.IsNullOrWhiteSpace(txtPatientID.Text) || !int.TryParse(txtPatientID.Text, out int patientId))
                {
                    MessageBox.Show("Please enter a valid Patient ID", "Validation Failed", 
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtDoctorID.Text) || !int.TryParse(txtDoctorID.Text, out int doctorId))
                {
                    MessageBox.Show("Please enter a valid Doctor ID", "Validation Failed", 
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!dpAppointmentDate.SelectedDate.HasValue)
                {
                    MessageBox.Show("Please select appointment date", "Validation Failed", 
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!TimeSpan.TryParse(txtAppointmentTime.Text, out TimeSpan appointmentTime))
                {
                    MessageBox.Show("Please enter a valid time format (e.g. 09:00)", "Validation Failed", 
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (cmbStatus.SelectedItem == null)
                {
                    MessageBox.Show("Please select status", "Validation Failed", 
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Update appointment information
                Appointment updatedAppointment = new Appointment
                {
                    AppointmentID = int.Parse(txtAppointmentID.Text),
                    PatientID = patientId,
                    DoctorID = doctorId,
                    AppointmentDate = dpAppointmentDate.SelectedDate.Value,
                    AppointmentTime = appointmentTime,
                    Status = (cmbStatus.SelectedItem as ComboBoxItem).Content.ToString()
                };

                _dalAppointment.UpdateAppointment(updatedAppointment);
                MessageBox.Show("Appointment information updated successfully!", "Success", 
                    MessageBoxButton.OK, MessageBoxImage.Information);
                LoadAppointments();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to update appointment information: {ex.Message}", "Error", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtAppointmentID.Text))
                {
                    MessageBox.Show("Please select an appointment to delete", "Information", 
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                var result = MessageBox.Show(
                    $"Are you sure you want to delete appointment ID [{txtAppointmentID.Text}]?\nThis action cannot be undone!", 
                    "Confirm Delete", 
                    MessageBoxButton.YesNo, 
                    MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    int appointmentId = int.Parse(txtAppointmentID.Text);
                    _dalAppointment.DeleteAppointment(appointmentId);
                    
                    MessageBox.Show("Appointment deleted successfully!", "Success", 
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    ClearForm();
                    LoadAppointments();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to delete appointment: {ex.Message}", "Error", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            txtAppointmentID.Text = string.Empty;
            txtPatientID.Text = string.Empty;
            txtDoctorID.Text = string.Empty;
            dpAppointmentDate.SelectedDate = null;
            txtAppointmentTime.Text = "09:00";
            cmbStatus.SelectedItem = null;
            dgAppointments.SelectedItem = null;
        }

        private void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
            LoadAppointments();
            MessageBox.Show("List refreshed", "Information", 
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = txtSearch.Text.ToLower();
            
            if (string.IsNullOrWhiteSpace(searchText))
            {
                dgAppointments.ItemsSource = _allAppointments;
            }
            else
            {
                var filtered = _allAppointments.Where(a =>
                    a.AppointmentID.ToString().Contains(searchText) ||
                    a.PatientID.ToString().Contains(searchText) ||
                    a.DoctorID.ToString().Contains(searchText) ||
                    (a.Status != null && a.Status.ToLower().Contains(searchText))
                ).ToList();
                
                dgAppointments.ItemsSource = filtered;
            }
        }
    }
}

