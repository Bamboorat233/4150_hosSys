using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Hospital_MamSys_LIB.DAL;
using Hospital_MamSys_LIB.Model;

namespace Hospital_MamSys_GUI
{
    public partial class PatientManagementWindow : Window
    {
        private DALPatient _dalPatient;
        private List<Patient> _allPatients;

        public PatientManagementWindow()
        {
            InitializeComponent();
            _dalPatient = new DALPatient();
            LoadPatients();
        }

        private void LoadPatients()
        {
            try
            {
                _allPatients = _dalPatient.GetAllPatients();
                dgPatients.ItemsSource = _allPatients;
                txtCount.Text = $"Total {_allPatients.Count} records";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load patient list: {ex.Message}", "Error", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DgPatients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgPatients.SelectedItem is Patient patient)
            {
                DisplayPatient(patient);
            }
        }

        private void DisplayPatient(Patient patient)
        {
            txtPatientID.Text = patient.PatientID.ToString();
            txtName.Text = patient.Name;
            dpDOB.SelectedDate = patient.DOB;
            
            // Set gender
            foreach (ComboBoxItem item in cmbGender.Items)
            {
                if (item.Content.ToString() == patient.Gender)
                {
                    cmbGender.SelectedItem = item;
                    break;
                }
            }
            
            txtContact.Text = patient.Contact;
            txtAddress.Text = patient.Address;
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Validate input
                if (string.IsNullOrWhiteSpace(txtName.Text))
                {
                    MessageBox.Show("Please enter patient name", "Validation Failed", 
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!dpDOB.SelectedDate.HasValue)
                {
                    MessageBox.Show("Please select date of birth", "Validation Failed", 
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (cmbGender.SelectedItem == null)
                {
                    MessageBox.Show("Please select gender", "Validation Failed", 
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Create new patient
                Patient newPatient = new Patient
                {
                    Name = txtName.Text.Trim(),
                    DOB = dpDOB.SelectedDate.Value,
                    Gender = (cmbGender.SelectedItem as ComboBoxItem).Content.ToString(),
                    Contact = txtContact.Text.Trim(),
                    Address = txtAddress.Text.Trim()
                };

                _dalPatient.AddPatient(newPatient);
                MessageBox.Show("Patient added successfully!", "Success", 
                    MessageBoxButton.OK, MessageBoxImage.Information);
                
                ClearForm();
                LoadPatients();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to add patient: {ex.Message}", "Error", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtPatientID.Text))
                {
                    MessageBox.Show("Please select a patient to update", "Information", 
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                // Validate input
                if (string.IsNullOrWhiteSpace(txtName.Text))
                {
                    MessageBox.Show("Please enter patient name", "Validation Failed", 
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!dpDOB.SelectedDate.HasValue)
                {
                    MessageBox.Show("Please select date of birth", "Validation Failed", 
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (cmbGender.SelectedItem == null)
                {
                    MessageBox.Show("Please select gender", "Validation Failed", 
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Update patient information
                Patient updatedPatient = new Patient
                {
                    PatientID = int.Parse(txtPatientID.Text),
                    Name = txtName.Text.Trim(),
                    DOB = dpDOB.SelectedDate.Value,
                    Gender = (cmbGender.SelectedItem as ComboBoxItem).Content.ToString(),
                    Contact = txtContact.Text.Trim(),
                    Address = txtAddress.Text.Trim()
                };

                int rowsAffected = _dalPatient.UpdatePatient(updatedPatient);
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Patient information updated successfully!", "Success", 
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadPatients();
                }
                else
                {
                    MessageBox.Show("Update failed, patient not found", "Warning", 
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to update patient information: {ex.Message}", "Error", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtPatientID.Text))
                {
                    MessageBox.Show("Please select a patient to delete", "Information", 
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                var result = MessageBox.Show(
                    $"Are you sure you want to delete patient [{txtName.Text}]?\nThis action cannot be undone!", 
                    "Confirm Delete", 
                    MessageBoxButton.YesNo, 
                    MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    int patientId = int.Parse(txtPatientID.Text);
                    int rowsAffected = _dalPatient.DeleteById(patientId);
                    
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Patient deleted successfully!", "Success", 
                            MessageBoxButton.OK, MessageBoxImage.Information);
                        ClearForm();
                        LoadPatients();
                    }
                    else
                    {
                        MessageBox.Show("Delete failed, patient not found", "Warning", 
                            MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to delete patient: {ex.Message}", "Error", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            txtPatientID.Text = string.Empty;
            txtName.Text = string.Empty;
            dpDOB.SelectedDate = null;
            cmbGender.SelectedItem = null;
            txtContact.Text = string.Empty;
            txtAddress.Text = string.Empty;
            dgPatients.SelectedItem = null;
        }

        private void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
            LoadPatients();
            MessageBox.Show("List refreshed", "Information", 
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = txtSearch.Text.ToLower();
            
            if (string.IsNullOrWhiteSpace(searchText))
            {
                dgPatients.ItemsSource = _allPatients;
            }
            else
            {
                var filtered = _allPatients.Where(p =>
                    (p.Name != null && p.Name.ToLower().Contains(searchText)) ||
                    (p.Contact != null && p.Contact.ToLower().Contains(searchText)) ||
                    p.PatientID.ToString().Contains(searchText)
                ).ToList();
                
                dgPatients.ItemsSource = filtered;
            }
        }
    }
}

