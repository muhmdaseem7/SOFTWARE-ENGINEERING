using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Login_FORM
{
    public partial class ResetPasswordForm : Form
    {
        private Panel panel1;
        private TextBox EmailTextBox;
        private Button SendRequestButton;
        private Label label2;
        private Label label1;
        private Button ReturnButton;

        // Adjust your connection string as needed
        private readonly string connectionString = "Server=localhost;Database=Loginpage;User ID=root;Password=root;Port=3306;AllowPublicKeyRetrieval=True;SSL Mode=None;";

        public ResetPasswordForm()
        {
            InitializeComponent();
        }

        private void SendRequestButton_Click(object sender, EventArgs e)
        {
            string email = EmailTextBox.Text.Trim();

            if (string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Please enter your email.");
                return;
            }

            try
            {
                using (MySqlConnection con = new MySqlConnection(connectionString))
                {
                    con.Open();

                    // Check if email exists
                    using (MySqlCommand checkCmd = new MySqlCommand("SELECT id FROM LoginForm WHERE email=@email", con))
                    {
                        checkCmd.Parameters.AddWithValue("@email", email);
                        var userId = checkCmd.ExecuteScalar();

                        if (userId == null)
                        {
                            MessageBox.Show("No user found with that email.");
                            return;
                        }
                    }

                    // Set pending password reset
                    using (MySqlCommand updateCmd = new MySqlCommand("UPDATE LoginForm SET pending_password_reset=1 WHERE email=@email", con))
                    {
                        updateCmd.Parameters.AddWithValue("@email", email);
                        int rowsAffected = updateCmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Your password reset request has been submitted. An administrator will review and if approved, you will receive a new password by email.");
                        }
                        else
                        {
                            MessageBox.Show("No rows were updated. Ensure that the email is correct and the 'pending_password_reset' column exists in the database.");
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                // Show detailed database error
                MessageBox.Show($"Database Error: {ex.Message}\n\nCheck your database connection, table and column names.");
            }
            catch (Exception ex)
            {
                // Show generic error
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private void ReturnButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            using (LoginForm loginForm = new LoginForm())
            {
                loginForm.ShowDialog();
            }
            this.Close();
        }

        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.ReturnButton = new System.Windows.Forms.Button();
            this.SendRequestButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.EmailTextBox = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DarkRed;
            this.panel1.Controls.Add(this.ReturnButton);
            this.panel1.Controls.Add(this.SendRequestButton);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.EmailTextBox);
            this.panel1.Location = new System.Drawing.Point(217, 63);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(814, 609);
            this.panel1.TabIndex = 0;
            // 
            // ReturnButton
            // 
            this.ReturnButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold);
            this.ReturnButton.Location = new System.Drawing.Point(330, 320);
            this.ReturnButton.Name = "ReturnButton";
            this.ReturnButton.Size = new System.Drawing.Size(116, 23);
            this.ReturnButton.TabIndex = 4;
            this.ReturnButton.Text = "RETURN";
            this.ReturnButton.UseVisualStyleBackColor = true;
            this.ReturnButton.Click += new System.EventHandler(this.ReturnButton_Click);
            // 
            // SendRequestButton
            // 
            this.SendRequestButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold);
            this.SendRequestButton.Location = new System.Drawing.Point(314, 238);
            this.SendRequestButton.Name = "SendRequestButton";
            this.SendRequestButton.Size = new System.Drawing.Size(148, 23);
            this.SendRequestButton.TabIndex = 3;
            this.SendRequestButton.Text = "SEND REQUEST";
            this.SendRequestButton.UseVisualStyleBackColor = true;
            this.SendRequestButton.Click += new System.EventHandler(this.SendRequestButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Red;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(231, 170);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(336, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "TYPE YOUR EMAIL LINKED TO THE ACCOUNT";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Red;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(230, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(345, 18);
            this.label1.TabIndex = 1;
            this.label1.Text = "REQUEST FOR PASSWORD RESET HERE ";
            // 
            // EmailTextBox
            // 
            this.EmailTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.EmailTextBox.Location = new System.Drawing.Point(171, 195);
            this.EmailTextBox.Name = "EmailTextBox";
            this.EmailTextBox.Size = new System.Drawing.Size(448, 24);
            this.EmailTextBox.TabIndex = 0;
            // 
            // ResetPasswordForm
            // 
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(1232, 736);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ResetPasswordForm";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }
    }
}

