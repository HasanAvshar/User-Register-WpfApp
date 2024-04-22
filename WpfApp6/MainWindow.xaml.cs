using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp6
{
    public partial class MainWindow : Window
    {
        private List<User> users = new List<User>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            User user = new User();
            user.Name = NameTextBox.Text;
            user.Surname = SurnameTextBox.Text;
            user.Gender = MaleRadioButton.IsChecked == true ? "Male" : "Female";
            user.Country = (CountryComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
            user.IsRead = ReadCheckBox.IsChecked == true;

            if (string.IsNullOrEmpty(user.Name) || string.IsNullOrEmpty(user.Surname) || string.IsNullOrEmpty(user.Country))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            users.Add(user);

            UserListBox.Items.Add(user.Name + " " + user.Surname);

            NameTextBox.Clear();
            SurnameTextBox.Clear();
            MaleRadioButton.IsChecked = true;
            CountryComboBox.SelectedIndex = -1;
            ReadCheckBox.IsChecked = false;
        }


        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (UserListBox.SelectedItem == null)
            {
                MessageBox.Show("Please select a user.");
                return;
            }

            User selectedUser = users[UserListBox.SelectedIndex];

            string json = JsonConvert.SerializeObject(selectedUser);

            File.WriteAllText(selectedUser.Name + ".json", json);
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            if (UserListBox.SelectedItem == null)
            {
                MessageBox.Show("Please select a user.");
                return;
            }

            User selectedUser = users[UserListBox.SelectedIndex];
            File.Delete(selectedUser.Name + ".json");
            users.Remove(selectedUser);
            UserListBox.Items.Remove(UserListBox.SelectedItem);
        }
    }

    public class User
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Gender { get; set; }
        public string Country { get; set; }
        public bool IsRead { get; set; }
    }
}
