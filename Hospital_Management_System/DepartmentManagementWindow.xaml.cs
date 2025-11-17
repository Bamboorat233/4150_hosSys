using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Hospital_MamSys_LIB.DAL;
using Hospital_MamSys_LIB.Model;

namespace Hospital_MamSys_GUI
{
    public partial class DepartmentManagementWindow : Window
    {
        private DALDepartment dalDepartment = new DALDepartment();
        private List<Department> departments = new List<Department>();

        public DepartmentManagementWindow()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                departments = dalDepartment.GetAll();
                dgDepartments.ItemsSource = departments;
                txtCount.Text = $"Total: {departments.Count}";
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show($"Failed to load departments: {ex.Message}", "Error",
                    CustomMessageBox.MessageBoxButton.OK, CustomMessageBox.MessageBoxIcon.Error);
            }
        }

        private void DgDepartments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgDepartments.SelectedItem is Department dept)
            {
                txtDepartmentID.Text = dept.DepartmentID.ToString();
                txtName.Text = dept.Name;
                txtLocation.Text = dept.Location;
            }
        }

        private void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
            ClearForm();
            CustomMessageBox.Show("Data refreshed successfully!", "Success",
                CustomMessageBox.MessageBoxButton.OK, CustomMessageBox.MessageBoxIcon.Information);
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            ClearForm();
            txtName.Focus();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Validation
                if (string.IsNullOrWhiteSpace(txtName.Text))
                {
                    CustomMessageBox.Show("Please enter department name!", "Validation Error",
                        CustomMessageBox.MessageBoxButton.OK, CustomMessageBox.MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtLocation.Text))
                {
                    CustomMessageBox.Show("Please enter location!", "Validation Error",
                        CustomMessageBox.MessageBoxButton.OK, CustomMessageBox.MessageBoxIcon.Warning);
                    return;
                }

                Department dept = new Department
                {
                    Name = txtName.Text.Trim(),
                    Location = txtLocation.Text.Trim()
                };

                // Check if updating or inserting
                if (string.IsNullOrWhiteSpace(txtDepartmentID.Text))
                {
                    // Insert new department
                    int newId = dalDepartment.Insert(dept);
                    CustomMessageBox.Show($"Department added successfully! (ID: {newId})", "Success",
                        CustomMessageBox.MessageBoxButton.OK, CustomMessageBox.MessageBoxIcon.Information);
                }
                else
                {
                    // Update existing department
                    dept.DepartmentID = int.Parse(txtDepartmentID.Text);
                    dalDepartment.Update(dept);
                    CustomMessageBox.Show("Department updated successfully!", "Success",
                        CustomMessageBox.MessageBoxButton.OK, CustomMessageBox.MessageBoxIcon.Information);
                }

                LoadData();
                ClearForm();
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show($"Failed to save department: {ex.Message}", "Error",
                    CustomMessageBox.MessageBoxButton.OK, CustomMessageBox.MessageBoxIcon.Error);
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtDepartmentID.Text))
                {
                    CustomMessageBox.Show("Please select a department to delete!", "Validation Error",
                        CustomMessageBox.MessageBoxButton.OK, CustomMessageBox.MessageBoxIcon.Warning);
                    return;
                }

                var result = CustomMessageBox.Show(
                    $"Are you sure you want to delete department '{txtName.Text}'?",
                    "Confirm Delete",
                    CustomMessageBox.MessageBoxButton.YesNo,
                    CustomMessageBox.MessageBoxIcon.Question);

                if (result == CustomMessageBox.MessageBoxResult.Yes)
                {
                    int id = int.Parse(txtDepartmentID.Text);
                    dalDepartment.Delete(id);
                    CustomMessageBox.Show("Department deleted successfully!", "Success",
                        CustomMessageBox.MessageBoxButton.OK, CustomMessageBox.MessageBoxIcon.Information);
                    LoadData();
                    ClearForm();
                }
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show($"Failed to delete department: {ex.Message}", "Error",
                    CustomMessageBox.MessageBoxButton.OK, CustomMessageBox.MessageBoxIcon.Error);
            }
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            txtDepartmentID.Clear();
            txtName.Clear();
            txtLocation.Clear();
            dgDepartments.SelectedItem = null;
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

