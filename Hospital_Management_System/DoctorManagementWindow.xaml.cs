using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Hospital_MamSys_LIB.DAL;
using Hospital_MamSys_LIB.Model;

namespace Hospital_MamSys_GUI
{
    public partial class DoctorManagementWindow : Window
    {
        private DALDoctor _dalDoctor;
        private List<Doctor> _allDoctors;

        public DoctorManagementWindow()
        {
            InitializeComponent();
            _dalDoctor = new DALDoctor();
            LoadDoctors();
        }

        private void LoadDoctors()
        {
            try
            {
                _allDoctors = _dalDoctor.GetAllDoctors();
                dgDoctors.ItemsSource = _allDoctors;
                txtCount.Text = $"Total {_allDoctors.Count} records";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load doctor list: {ex.Message}", "Error", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DgDoctors_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgDoctors.SelectedItem is Doctor doctor)
            {
                DisplayDoctor(doctor);
            }
        }

        private void DisplayDoctor(Doctor doctor)
        {
            txtDoctorID.Text = doctor.DoctorID.ToString();
            txtName.Text = doctor.Name;
            txtSpecialization.Text = doctor.Specialization;
            txtContact.Text = doctor.Contact;
            txtDepartmentID.Text = doctor.DepartmentID.ToString();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Validate input
                if (string.IsNullOrWhiteSpace(txtName.Text))
                {
                    MessageBox.Show("Please enter doctor name", "Validation Failed", 
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtDepartmentID.Text) || !int.TryParse(txtDepartmentID.Text, out int deptId))
                {
                    MessageBox.Show("Please enter a valid Department ID", "Validation Failed", 
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Create new doctor
                Doctor newDoctor = new Doctor
                {
                    Name = txtName.Text.Trim(),
                    Specialization = txtSpecialization.Text.Trim(),
                    Contact = txtContact.Text.Trim(),
                    DepartmentID = deptId
                };

                _dalDoctor.AddDoctor(newDoctor);
                MessageBox.Show("Doctor added successfully!", "Success", 
                    MessageBoxButton.OK, MessageBoxImage.Information);
                
                ClearForm();
                LoadDoctors();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to add doctor: {ex.Message}", "Error", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtDoctorID.Text))
                {
                    MessageBox.Show("Please select a doctor to update", "Information", 
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                // Validate input
                if (string.IsNullOrWhiteSpace(txtName.Text))
                {
                    MessageBox.Show("Please enter doctor name", "Validation Failed", 
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtDepartmentID.Text) || !int.TryParse(txtDepartmentID.Text, out int deptId))
                {
                    MessageBox.Show("Please enter a valid Department ID", "Validation Failed", 
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Update doctor information
                Doctor updatedDoctor = new Doctor
                {
                    DoctorID = int.Parse(txtDoctorID.Text),
                    Name = txtName.Text.Trim(),
                    Specialization = txtSpecialization.Text.Trim(),
                    Contact = txtContact.Text.Trim(),
                    DepartmentID = deptId
                };

                int rowsAffected = _dalDoctor.UpdateDoctor(updatedDoctor);
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Doctor information updated successfully!", "Success", 
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadDoctors();
                }
                else
                {
                    MessageBox.Show("Update failed, doctor not found", "Warning", 
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to update doctor information: {ex.Message}", "Error", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtDoctorID.Text))
                {
                    MessageBox.Show("Please select a doctor to delete", "Information", 
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                var result = MessageBox.Show(
                    $"Are you sure you want to delete doctor [{txtName.Text}]?\nThis action cannot be undone!", 
                    "Confirm Delete", 
                    MessageBoxButton.YesNo, 
                    MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    int doctorId = int.Parse(txtDoctorID.Text);
                    int rowsAffected = _dalDoctor.DeleteById(doctorId);
                    
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Doctor deleted successfully!", "Success", 
                            MessageBoxButton.OK, MessageBoxImage.Information);
                        ClearForm();
                        LoadDoctors();
                    }
                    else
                    {
                        MessageBox.Show("Delete failed, doctor not found", "Warning", 
                            MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to delete doctor: {ex.Message}", "Error", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            txtDoctorID.Text = string.Empty;
            txtName.Text = string.Empty;
            txtSpecialization.Text = string.Empty;
            txtContact.Text = string.Empty;
            txtDepartmentID.Text = string.Empty;
            dgDoctors.SelectedItem = null;
        }

        private void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
            LoadDoctors();
            MessageBox.Show("List refreshed", "Information", 
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = txtSearch.Text.ToLower();
            
            if (string.IsNullOrWhiteSpace(searchText))
            {
                dgDoctors.ItemsSource = _allDoctors;
            }
            else
            {
                var filtered = _allDoctors.Where(d =>
                    (d.Name != null && d.Name.ToLower().Contains(searchText)) ||
                    (d.Specialization != null && d.Specialization.ToLower().Contains(searchText)) ||
                    (d.Contact != null && d.Contact.ToLower().Contains(searchText)) ||
                    d.DoctorID.ToString().Contains(searchText)
                ).ToList();
                
                dgDoctors.ItemsSource = filtered;
            }
        }
    }
}

