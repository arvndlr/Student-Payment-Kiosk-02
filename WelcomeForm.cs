using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Student_Payment_Kiosk_02
{
    public partial class WelcomeForm : Form
    {
        private Font originalFont;
        private Font maximizedFont;
        public WelcomeForm()
        {
            InitializeComponent();

            // Initialize the original and maximized fonts
            originalFont = new Font("Arial", 10); // Original font
            maximizedFont = new Font("Arial", 16, FontStyle.Bold); // Font for maximized state

            this.SizeChanged += MainForm_SizeChanged;
        }
        // Event handler to change the font when the form is maximized
        private void MainForm_SizeChanged(object sender, EventArgs e)
        {

            // Check if the form is maximized
            if (this.WindowState == FormWindowState.Maximized)
            {
                // Change the font to the maximized font
                label1.Font = maximizedFont;
                label2.Font = maximizedFont;
            }
            else
            {
                // Revert to the original font when not maximized
                label1.Font = originalFont;
                label2.Font = originalFont;
            }
        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {
            // Create and show the password entry form
            LoginForm passwordForm = new LoginForm();
            passwordForm.Show(); // Show as a modal dialog
        }
    }
}
