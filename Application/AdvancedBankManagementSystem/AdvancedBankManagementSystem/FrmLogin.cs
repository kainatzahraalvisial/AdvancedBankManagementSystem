using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace AdvancedBankManagementSystem
{
    public partial class LoginScreen : Form
    {
        private string connectionString = @"Server=.\SQLEXPRESS;Database=BankManagementSystem;Integrated Security=True;";

        // UI Controls
        private Panel panelLeft, panelRight;
        private Label lblWelcomeLeft, lblLoginHeading, lblSubtitle, lblUsername, lblPassword;
        private TextBox txtUsername, txtPassword;
        private CheckBox chkRememberMe;
        private LinkLabel lnkForgot, lnkSignUp;
        private Button btnLogin, btnClose;

        public LoginScreen()
        {
            InitializeCustomComponent();
        }

        private void InitializeCustomComponent()
        {
            this.panelLeft = new Panel();
            this.panelRight = new Panel();
            this.lblWelcomeLeft = new Label();
            this.lblLoginHeading = new Label();
            this.lblSubtitle = new Label();
            this.lblUsername = new Label();
            this.txtUsername = new TextBox();
            this.lblPassword = new Label();
            this.txtPassword = new TextBox();
            this.chkRememberMe = new CheckBox();
            this.lnkForgot = new LinkLabel();
            this.btnLogin = new Button();
            this.lnkSignUp = new LinkLabel();
            this.btnClose = new Button();

            // Main Form Settings
            this.Text = "Login";
            this.Size = new Size(800, 550);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.None;

            // === LEFT PANEL ===
            this.panelLeft.Dock = DockStyle.Left;
            this.panelLeft.Width = 350;
            this.panelLeft.BackColor = Color.FromArgb(100, 120, 220);

            this.lblWelcomeLeft.Text = "Welcome\nBack!";
            this.lblWelcomeLeft.Font = new Font("Segoe UI", 28, FontStyle.Bold);
            this.lblWelcomeLeft.ForeColor = Color.White;
            this.lblWelcomeLeft.Location = new Point(40, 180);
            this.lblWelcomeLeft.AutoSize = true;

            this.panelLeft.Controls.Add(lblWelcomeLeft);

            // === RIGHT PANEL ===
            this.panelRight.Dock = DockStyle.Fill;
            this.panelRight.BackColor = Color.WhiteSmoke;

            // Close Button
            this.btnClose.Text = "✕";
            this.btnClose.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            this.btnClose.Location = new Point(400, 10);
            this.btnClose.Size = new Size(30, 30);
            this.btnClose.FlatStyle = FlatStyle.Flat;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.Cursor = Cursors.Hand;
            this.btnClose.Click += (s, e) => Application.Exit();

            // Login Heading
            this.lblLoginHeading.Text = "Login";
            this.lblLoginHeading.Font = new Font("Segoe UI", 22, FontStyle.Bold);
            this.lblLoginHeading.ForeColor = Color.FromArgb(30, 40, 70);
            this.lblLoginHeading.Location = new Point(60, 60);
            this.lblLoginHeading.AutoSize = true;

            // Subtitle
            this.lblSubtitle.Text = "Welcome back! Please login to your account.";
            this.lblSubtitle.Font = new Font("Segoe UI", 9);
            this.lblSubtitle.ForeColor = Color.Gray;
            this.lblSubtitle.Location = new Point(64, 105);
            this.lblSubtitle.AutoSize = true;

            // Username
            this.lblUsername.Text = "User Name";
            this.lblUsername.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            this.lblUsername.ForeColor = Color.DimGray;
            this.lblUsername.Location = new Point(60, 160);
            this.lblUsername.AutoSize = true;

            this.txtUsername.Location = new Point(64, 185);
            this.txtUsername.Size = new Size(320, 30);
            this.txtUsername.Font = new Font("Segoe UI", 11);

            // Password
            this.lblPassword.Text = "Password";
            this.lblPassword.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            this.lblPassword.ForeColor = Color.DimGray;
            this.lblPassword.Location = new Point(60, 240);
            this.lblPassword.AutoSize = true;

            this.txtPassword.Location = new Point(64, 265);
            this.txtPassword.Size = new Size(320, 30);
            this.txtPassword.Font = new Font("Segoe UI", 11);
            this.txtPassword.PasswordChar = '*';

            // Remember Me
            this.chkRememberMe.Text = "Remember Me";
            this.chkRememberMe.Location = new Point(64, 310);
            this.chkRememberMe.AutoSize = true;

            // Login Button
            this.btnLogin.Text = "Login";
            this.btnLogin.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            this.btnLogin.Location = new Point(64, 360);
            this.btnLogin.Size = new Size(320, 45);
            this.btnLogin.BackColor = Color.FromArgb(100, 120, 220);
            this.btnLogin.ForeColor = Color.White;
            this.btnLogin.FlatStyle = FlatStyle.Flat;
            this.btnLogin.FlatAppearance.BorderSize = 0;
            this.btnLogin.Cursor = Cursors.Hand;
            this.btnLogin.Click += BtnLogin_Click;

            // Add controls to Right Panel
            this.panelRight.Controls.Add(btnClose);
            this.panelRight.Controls.Add(lblLoginHeading);
            this.panelRight.Controls.Add(lblSubtitle);
            this.panelRight.Controls.Add(lblUsername);
            this.panelRight.Controls.Add(txtUsername);
            this.panelRight.Controls.Add(lblPassword);
            this.panelRight.Controls.Add(txtPassword);
            this.panelRight.Controls.Add(chkRememberMe);
            this.panelRight.Controls.Add(btnLogin);

            // Add Panels to Form
            this.Controls.Add(panelRight);
            this.Controls.Add(panelLeft);
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
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
                        if (result != null) MessageBox.Show("Login Successful!", "Welcome");
                        else MessageBox.Show("Invalid credentials!", "Error");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Database error: " + ex.Message);
                }
            }
        }
    }
}