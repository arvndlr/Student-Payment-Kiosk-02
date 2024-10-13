using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Student_Payment_Kiosk_02
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            // Get form data
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();
            string role = cmbRole.SelectedItem.ToString();

            // Check if all fields are filled
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(role))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            // Hash the password before saving
            string hashedPassword = HashPassword(password);

            // Define the PostgreSQL connection string
            string connectionString = "Host=localhost;Username=postgres;Password=Jrch-120423;Database=testStudentPaymentKioskDB";
            
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();

                // Check if the username already exists
                var checkUserCmd = new NpgsqlCommand("SELECT COUNT(*) FROM users WHERE username = @username", conn);
                checkUserCmd.Parameters.AddWithValue("username", username);

                object result = checkUserCmd.ExecuteScalar();
                int userExists = Convert.ToInt32(result);

                if (userExists > 0)
                {
                    MessageBox.Show("Username already exists. Please choose another.");
                    return;
                }

                // Insert the new user into the database
                var cmd = new NpgsqlCommand("INSERT INTO users (username, password_hash, role, created_at, updated_at) VALUES (@username, @password_hash, @role, @created_at, @updated_at)", conn);
                cmd.Parameters.AddWithValue("username", username);
                cmd.Parameters.AddWithValue("password_hash", hashedPassword);
                cmd.Parameters.AddWithValue("role", role);
                cmd.Parameters.AddWithValue("created_at", DateTime.Now);
                cmd.Parameters.AddWithValue("updated_at", DateTime.Now);

                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Registration successful!");

                    // Redirect to the login form
                    this.Hide();
                    LoginForm loginForm = new LoginForm();
                    loginForm.Show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        public static string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }


    }
}
