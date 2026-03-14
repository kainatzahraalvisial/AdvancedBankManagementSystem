using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace AdvancedBankManagementSystem
{
    public partial class FrmLogin : Form
    {
        private string connectionString = @"Server=.\SQLEXPRESS;Database=BankManagementSystem;Integrated Security=True;";

        public FrmLogin()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.lblWelcome = new Label();
            this.lblSubtitle = new Label();
            this.lblUsername = new Label();
            this.txtUsername = new TextBox();
            this.lblPassword = new Label();
            this.txtPassword = new TextBox();
            this.btnLogin = new Button();
            this.btnClose = new Button();
            this.card = new Panel();

            // Main Form - Light gradient background + fully centered
            this.Text = "Login";
            this.Size = new Size(680, 620);
            this.MinimumSize = new Size(680, 620);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.BackColor = Color.FromArgb(230, 240, 255);   // Soft blue background

            // White Card (exactly like your inspiration)
            this.card.Size = new Size(420, 460);
            this.card.Location = new Point((this.Width - 420) / 2, (this.Height - 460) / 2);  // Perfectly centered
            this.card.BackColor = Color.White;
            this.card.BorderStyle = BorderStyle.None;

            // Welcome Back!
            this.lblWelcome.Text = "Welcome Back!";
            this.lblWelcome.Font = new Font("Segoe UI", 24, FontStyle.Bold);
            this.lblWelcome.ForeColor = Color.FromArgb(30, 60, 120);
            this.lblWelcome.Location = new Point(70, 60);
            this.lblWelcome.AutoSize = true;

            this.lblSubtitle.Text = "Sign in to continue";
            this.lblSubtitle.Font = new Font("Segoe UI", 11);
            this.lblSubtitle.ForeColor = Color.Gray;
            this.lblSubtitle.Location = new Point(72, 105);

            // Username
            this.lblUsername.Text = "Username";
            this.lblUsername.Font = new Font("Segoe UI", 11);
            this.lblUsername.ForeColor = Color.DimGray;
            this.lblUsername.Location = new Point(70, 160);
            this.txtUsername.Location = new Point(70, 190);
            this.txtUsername.Size = new Size(280, 38);
            this.txtUsername.Font = new Font("Segoe UI", 12);

            // Password
            this.lblPassword.Text = "Password";
            this.lblPassword.Font = new Font("Segoe UI", 11);
            this.lblPassword.ForeColor = Color.DimGray;
            this.lblPassword.Location = new Point(70, 240);
            this.txtPassword.Location = new Point(70, 270);
            this.txtPassword.Size = new Size(280, 38);
            this.txtPassword.Font = new Font("Segoe UI", 12);
            this.txtPassword.PasswordChar = '*';

            // Login Button
            this.btnLogin.Text = "LOGIN";
            this.btnLogin.Font = new Font("Segoe UI", 13, FontStyle.Bold);
            this.btnLogin.Location = new Point(70, 340);
            this.btnLogin.Size = new Size(280, 52);
            this.btnLogin.BackColor = Color.FromArgb(0, 102, 204);
            this.btnLogin.ForeColor = Color.White;
            this.btnLogin.FlatStyle = FlatStyle.Flat;
            this.btnLogin.FlatAppearance.BorderSize = 0;

            // Close Button (top right of card)
            this.btnClose.Text = "✕";
            this.btnClose.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            this.btnClose.Location = new Point(370, 15);
            this.btnClose.Size = new Size(35, 35);
            this.btnClose.BackColor = Color.Transparent;
            this.btnClose.ForeColor = Color.Gray;
            this.btnClose.FlatStyle = FlatStyle.Flat;

            // Add controls
            this.Controls.Add(card);
            this.card.Controls.Add(lblWelcome);
            this.card.Controls.Add(lblSubtitle);
            this.card.Controls.Add(lblUsername);
            this.card.Controls.Add(txtUsername);
            this.card.Controls.Add(lblPassword);
            this.card.Controls.Add(txtPassword);
            this.card.Controls.Add(btnLogin);
            this.card.Controls.Add(btnClose);

            this.btnLogin.Click += btnLogin_Click;
            this.btnClose.Click += (s, e) => Application.Exit();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            // (same working login logic as before)
            if (string.IsNullOrEmpty(txtUsername.Text) || string.IsNullOrEmpty(txtPassword.Text))
            {
                MessageBox.Show("Please enter username and password!", "Error");
                return;
            }

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string query = "SELECT Role FROM Users WHERE Username = @user AND Password = @pass";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@user", txtUsername.Text.Trim());
                        cmd.Parameters.AddWithValue("@pass", txtPassword.Text.Trim());

                        object result = cmd.ExecuteScalar();

                        if (result != null)
                        {
                            MessageBox.Show("Login Successful! Opening Dashboard...", "Welcome");
                        }
                        else
                        {
                            MessageBox.Show("Invalid credentials!", "Error");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Database error: " + ex.Message);
                }
            }
        }

        private Label lblWelcome, lblSubtitle, lblUsername, lblPassword;
        private TextBox txtUsername, txtPassword;
        private Button btnLogin, btnClose;
        private Panel card;
    }
}