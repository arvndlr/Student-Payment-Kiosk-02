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
    }
}
