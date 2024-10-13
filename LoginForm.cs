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
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void linkLabel2_Click(object sender, EventArgs e)
        {
            this.Close();
            RegisterForm registerForm = new RegisterForm();
            registerForm.Show();
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            // Get form data
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            // Check if all fields are filled
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please fill in both username and password.");
                return;
            }

            // Hash the entered password
            string hashedPassword = HashPassword(password);

            // Define the PostgreSQL connection string
            string connectionString = "Host=localhost;Username=postgres;Password=Jrch-120423;Database=testStudentPaymentKioskDB";

            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();

                // Query the database to validate the user
                var cmd = new NpgsqlCommand("SELECT role FROM users WHERE username = @username AND password_hash = @password_hash", conn);
                cmd.Parameters.AddWithValue("username", username);
                cmd.Parameters.AddWithValue("password_hash", hashedPassword);

                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    string role = reader["role"].ToString();

                    // Navigate to the appropriate form based on the role
                    if (role == "Student")
                    {
                        StudentForm studentForm = new StudentForm();
                        studentForm.Show();
                        this.Hide();
                    }
                    else if (role == "Cashier")
                    {
                        CashierForm cashierForm = new CashierForm();
                        cashierForm.Show();
                        this.Hide();
                    }
                    else if (role == "Admin")
                    {
                        AdminForm adminForm = new AdminForm();
                        adminForm.Show();
                        this.Hide();
                    }
                }
                else
                {
                    MessageBox.Show("Invalid credentials. Please try again.");
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
