using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Collections.Specialized.BitVector32;

namespace Student_Payment_Kiosk_02
{
    public partial class AdminForm : Form
    {
        private Font originalFont;
        private Font maximizedFont;
        public AdminForm()
        {
            InitializeComponent();
            // Initialize the original and maximized fonts
            originalFont = new Font("Arial", 12); // Original font
            maximizedFont = new Font("Arial", 16); // Font for maximized state

            this.SizeChanged += MainForm_SizeChanged;
        }
        // Event handler to change the font when the form is maximized
        private void MainForm_SizeChanged(object sender, EventArgs e)
        {

            // Check if the form is maximized
            if (this.WindowState == FormWindowState.Maximized)
            {
                // Change the font to the maximized font
                tabControl1.Font = maximizedFont;
                tabPage2.Font = maximizedFont;
                tableLayoutPanel1.Padding = new Padding(80, tableLayoutPanel1.Padding.Top, 80, tableLayoutPanel1.Padding.Bottom);

                tableLayoutPanel2.RowStyles[0].Height = tableLayoutPanel2.Height / 6; // Set first row to half of the height
                tableLayoutPanel2.RowStyles[1].Height = tableLayoutPanel2.Height / 6;
                tableLayoutPanel2.RowStyles[2].Height = tableLayoutPanel2.Height / 6; // Set first row to half of the height
                tableLayoutPanel2.RowStyles[3].Height = tableLayoutPanel2.Height / 6;
                tableLayoutPanel2.RowStyles[4].Height = tableLayoutPanel2.Height / 6; // Set first row to half of the height
                tableLayoutPanel2.RowStyles[5].Height = tableLayoutPanel2.Height / 6;

            }
            else
            {
                // Revert to the original font when not maximized
                tabControl1.Font = originalFont;
                tabPage2.Font = originalFont;
                tableLayoutPanel1.Padding = new Padding(20, tableLayoutPanel1.Padding.Top, 20, tableLayoutPanel1.Padding.Bottom);
            }
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string studentName = txtName.Text;
            string studentSection = txtSection.Text;
            string studentNumber = txtStudentNo.Text;
            string program = txtProgram.Text;
            string status = txtStatus.Text;
            int units = int.Parse(txtUnits.Text);
            string paymentPlan = txtPaymentPlan.Text; ;
            string email = txtEmail.Text;
            string username = txtUsername.Text;
            decimal netAssessment = decimal.Parse(txtNetassessment.Text);

            //tuitionfee data
            string ps1 = paymentSched1.Text;
            string ps2 = paymentSched2.Text;
            string ps3 = paymentSched3.Text;
            string ps4 = paymentSched4.Text;
            string ps5 = paymentSched5.Text;

            DateTime ps1DueDate = ps1Due.Value;
            DateTime ps2DueDate = ps2Due.Value;
            DateTime ps3DueDate = ps3Due.Value;
            DateTime ps4DueDate = ps4Due.Value;
            DateTime ps5DueDate = ps5Due.Value;

            decimal ps1Amnt = decimal.Parse(ps1Amount.Text);
            decimal ps2Amnt = decimal.Parse(ps1Amount.Text);
            decimal ps3Amnt = decimal.Parse(ps1Amount.Text);
            decimal ps4Amnt = decimal.Parse(ps1Amount.Text);
            decimal ps5Amnt = decimal.Parse(ps1Amount.Text);

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
                    MessageBox.Show("Username already exists");
                    var cmd = new NpgsqlCommand("INSERT INTO studentinfo (studentid, name, section, program, status, units, username, email, paymentplan, netassessment) VALUES (@studentid, @name, @section, @program, @status, @units, @username, @email, @paymentplan,@netassessment)", conn);

                    cmd.Parameters.AddWithValue("studentid", studentNumber);
                    cmd.Parameters.AddWithValue("name", studentName);
                    cmd.Parameters.AddWithValue("section", studentSection);
                    cmd.Parameters.AddWithValue("program", program);
                    cmd.Parameters.AddWithValue("status", status);
                    cmd.Parameters.AddWithValue("units", units);  // Assuming it's an integer
                    cmd.Parameters.AddWithValue("username", username);
                    cmd.Parameters.AddWithValue("email", email);
                    cmd.Parameters.AddWithValue("paymentplan", paymentPlan);
                    cmd.Parameters.AddWithValue("netassessment", netAssessment);

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

                var tfcmd = new NpgsqlCommand("INSERT INTO tuitionfee (student_number, payment_sched, duedate, amount) VALUES (@student_number, @payment_sched, @duedate, @amount)", conn);

                using (tfcmd)
                {
                    tfcmd.Parameters.AddWithValue("student_number", studentNumber);
                    tfcmd.Parameters.AddWithValue("payment_sched", ps1);
                    tfcmd.Parameters.AddWithValue("duedate", ps1DueDate);
                    tfcmd.Parameters.AddWithValue("amount", ps1Amnt);

                    tfcmd.Parameters.AddWithValue("student_number", studentNumber);
                    tfcmd.Parameters.AddWithValue("payment_sched", ps2);
                    tfcmd.Parameters.AddWithValue("duedate", ps2DueDate);
                    tfcmd.Parameters.AddWithValue("amount", ps2Amnt);

                    tfcmd.Parameters.AddWithValue("student_number", studentNumber);
                    tfcmd.Parameters.AddWithValue("payment_sched", ps3);
                    tfcmd.Parameters.AddWithValue("duedate", ps3DueDate);
                    tfcmd.Parameters.AddWithValue("amount", ps3Amnt);

                    tfcmd.Parameters.AddWithValue("student_number", studentNumber);
                    tfcmd.Parameters.AddWithValue("payment_sched", ps4);
                    tfcmd.Parameters.AddWithValue("duedate", ps4DueDate);
                    tfcmd.Parameters.AddWithValue("amount", ps4Amnt);

                    tfcmd.Parameters.AddWithValue("student_number", studentNumber);
                    tfcmd.Parameters.AddWithValue("payment_sched", ps5);
                    tfcmd.Parameters.AddWithValue("duedate", ps5DueDate);
                    tfcmd.Parameters.AddWithValue("amount", ps5Amnt);

                    // Execute the command
                    tfcmd.ExecuteNonQuery();
                }
                

                
            }

        }
    }
}
