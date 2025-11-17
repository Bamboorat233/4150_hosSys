using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Hospital_MamSys_LIB.DAL;
using Hospital_MamSys_LIB.Model;

namespace Hospital_MamSys_GUI
{
    public partial class UserManagementWindow : Window
    {
        private DALUser dalUser = new DALUser();
        private List<User> users = new List<User>();

        public UserManagementWindow()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                users = dalUser.GetAllUsers();
                dgUsers.ItemsSource = users;
                txtCount.Text = $"Total: {users.Count}";
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show($"Failed to load users: {ex.Message}", "Error",
                    CustomMessageBox.MessageBoxButton.OK, CustomMessageBox.MessageBoxIcon.Error);
            }
        }

        private void DgUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgUsers.SelectedItem is User user)
            {
                txtUserID.Text = user.UserID.ToString();
                txtUsername.Text = user.Username;
                // Don't display password for security
                txtPassword.Password = "";
                txtEmail.Text = user.Email;
                chkIsActive.IsChecked = user.IsActive;

                // Set role
                switch (user.Role)
                {
                    case "Admin":
                        cmbRole.SelectedIndex = 0;
                        break;
                    case "Doctor":
                        cmbRole.SelectedIndex = 1;
                        break;
                    case "Nurse":
                        cmbRole.SelectedIndex = 2;
                        break;
                    case "Receptionist":
                        cmbRole.SelectedIndex = 3;
                        break;
                    default:
                        cmbRole.SelectedIndex = -1;
                        break;
                }
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
            txtUsername.Focus();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Validation
                if (string.IsNullOrWhiteSpace(txtUsername.Text))
                {
                    CustomMessageBox.Show("Please enter username!", "Validation Error",
                        CustomMessageBox.MessageBoxButton.OK, CustomMessageBox.MessageBoxIcon.Warning);
                    return;
                }

                if (cmbRole.SelectedIndex == -1)
                {
                    CustomMessageBox.Show("Please select a role!", "Validation Error",
                        CustomMessageBox.MessageBoxButton.OK, CustomMessageBox.MessageBoxIcon.Warning);
                    return;
                }

                // Check if updating or inserting
                bool isNewUser = string.IsNullOrWhiteSpace(txtUserID.Text);
                
                if (isNewUser && string.IsNullOrWhiteSpace(txtPassword.Password))
                {
                    CustomMessageBox.Show("Password is required for new users!", "Validation Error",
                        CustomMessageBox.MessageBoxButton.OK, CustomMessageBox.MessageBoxIcon.Warning);
                    return;
                }

                User user = new User
                {
                    Username = txtUsername.Text.Trim(),
                    Role = (cmbRole.SelectedItem as ComboBoxItem)?.Content.ToString(),
                    Email = txtEmail.Text.Trim(),
                    IsActive = chkIsActive.IsChecked ?? true
                };

                if (isNewUser)
                {
                    // Insert new user
                    user.Password = txtPassword.Password;
                    user.CreatedDate = DateTime.Now;
                    dalUser.AddUser(user);
                    CustomMessageBox.Show("User added successfully!", "Success",
                        CustomMessageBox.MessageBoxButton.OK, CustomMessageBox.MessageBoxIcon.Information);
                }
                else
                {
                    // Update existing user
                    user.UserID = int.Parse(txtUserID.Text);
                    
                    // Only update password if it's not empty
                    if (!string.IsNullOrWhiteSpace(txtPassword.Password))
                    {
                        user.Password = txtPassword.Password;
                    }
                    else
                    {
                        // Keep existing password (fetch from selected user)
                        if (dgUsers.SelectedItem is User selectedUser)
                        {
                            user.Password = selectedUser.Password;
                        }
                    }

                    dalUser.UpdateUser(user);
                    CustomMessageBox.Show("User updated successfully!", "Success",
                        CustomMessageBox.MessageBoxButton.OK, CustomMessageBox.MessageBoxIcon.Information);
                }

                LoadData();
                ClearForm();
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show($"Failed to save user: {ex.Message}", "Error",
                    CustomMessageBox.MessageBoxButton.OK, CustomMessageBox.MessageBoxIcon.Error);
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtUserID.Text))
                {
                    CustomMessageBox.Show("Please select a user to delete!", "Validation Error",
                        CustomMessageBox.MessageBoxButton.OK, CustomMessageBox.MessageBoxIcon.Warning);
                    return;
                }

                var result = CustomMessageBox.Show(
                    $"Are you sure you want to delete user '{txtUsername.Text}'?",
                    "Confirm Delete",
                    CustomMessageBox.MessageBoxButton.YesNo,
                    CustomMessageBox.MessageBoxIcon.Question);

                if (result == CustomMessageBox.MessageBoxResult.Yes)
                {
                    int id = int.Parse(txtUserID.Text);
                    dalUser.DeleteById(id);
                    CustomMessageBox.Show("User deleted successfully!", "Success",
                        CustomMessageBox.MessageBoxButton.OK, CustomMessageBox.MessageBoxIcon.Information);
                    LoadData();
                    ClearForm();
                }
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show($"Failed to delete user: {ex.Message}", "Error",
                    CustomMessageBox.MessageBoxButton.OK, CustomMessageBox.MessageBoxIcon.Error);
            }
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            txtUserID.Clear();
            txtUsername.Clear();
            txtPassword.Clear();
            txtEmail.Clear();
            cmbRole.SelectedIndex = -1;
            chkIsActive.IsChecked = true;
            dgUsers.SelectedItem = null;
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

