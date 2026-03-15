using System;
using System.Drawing;
using System.Drawing.Drawing2D; // Needed for the gradients!
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
        private Panel pnlUserBorder, pnlPassBorder; // Containers for gradient borders
        private CheckBox chkRememberMe;
        private Button btnLogin, btnClose;
        private LinkLabel lnkSignUp;

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
            this.pnlUserBorder = new Panel();
            this.pnlPassBorder = new Panel();
            this.chkRememberMe = new CheckBox();
            this.btnLogin = new Button();
            this.btnClose = new Button();
            this.lnkSignUp = new LinkLabel();

            // Main Form Settings
            this.Text = "Login";
            this.Size = new Size(800, 550);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.None;

            // === LEFT PANEL ===
            this.panelLeft.Dock = DockStyle.Left;
            this.panelLeft.Width = 350;
            this.panelLeft.BackColor = Color.FromArgb(100, 120, 220); // Matched to Sign Up

            this.lblWelcomeLeft.Text = "Welcome\nBack!";
            this.lblWelcomeLeft.Font = new Font("Segoe UI", 28, FontStyle.Bold);
            this.lblWelcomeLeft.ForeColor = Color.White; // Matched to Sign Up purple
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
            this.btnClose.ForeColor = Color.Gray;
            this.btnClose.Click += (s, e) => Application.Exit();

            // === HEADING ===
            this.lblLoginHeading.Text = "Login";
            this.lblLoginHeading.Font = new Font("Segoe UI", 22, FontStyle.Bold);
            this.lblLoginHeading.ForeColor = Color.FromArgb(100, 120, 220); // Matched to left panel
            this.lblLoginHeading.Location = new Point(60, 60);
            this.lblLoginHeading.AutoSize = true;

            // Subtitle
            this.lblSubtitle.Text = "Welcome back! Please login to your account.";
            this.lblSubtitle.Font = new Font("Segoe UI", 9);
            this.lblSubtitle.ForeColor = Color.Gray;
            this.lblSubtitle.Location = new Point(64, 105);
            this.lblSubtitle.AutoSize = true;

            // === USERNAME ===
            this.lblUsername.Text = "User Name";
            this.lblUsername.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            this.lblUsername.ForeColor = Color.DimGray;
            this.lblUsername.Location = new Point(60, 160);
            this.lblUsername.AutoSize = true;

            this.pnlUserBorder.Location = new Point(64, 180);
            this.pnlUserBorder.Size = new Size(320, 36);
            this.pnlUserBorder.BackColor = Color.White;
            this.pnlUserBorder.Paint += DrawGradientBorder;

            this.txtUsername.BorderStyle = BorderStyle.None;
            this.txtUsername.Location = new Point(5, 8);
            this.txtUsername.Size = new Size(310, 20);
            this.txtUsername.Font = new Font("Segoe UI", 11);
            this.pnlUserBorder.Controls.Add(txtUsername);

            // === PASSWORD ===
            this.lblPassword.Text = "Password";
            this.lblPassword.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            this.lblPassword.ForeColor = Color.DimGray;
            this.lblPassword.Location = new Point(60, 240);
            this.lblPassword.AutoSize = true;

            this.pnlPassBorder.Location = new Point(64, 260);
            this.pnlPassBorder.Size = new Size(320, 36);
            this.pnlPassBorder.BackColor = Color.White;
            this.pnlPassBorder.Paint += DrawGradientBorder;

            this.txtPassword.BorderStyle = BorderStyle.None;
            this.txtPassword.Location = new Point(5, 8);
            this.txtPassword.Size = new Size(310, 20);
            this.txtPassword.Font = new Font("Segoe UI", 11);
            this.txtPassword.PasswordChar = '*';
            this.pnlPassBorder.Controls.Add(txtPassword);

            // Remember Me
            this.chkRememberMe.Text = "Remember Me";
            this.chkRememberMe.Location = new Point(64, 310);
            this.chkRememberMe.AutoSize = true;

            // === LOGIN BUTTON ===
            this.btnLogin.Text = "Login";
            this.btnLogin.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            this.btnLogin.Location = new Point(64, 360);
            this.btnLogin.Size = new Size(320, 45);
            this.btnLogin.ForeColor = Color.White;
            this.btnLogin.FlatStyle = FlatStyle.Flat;
            this.btnLogin.FlatAppearance.BorderSize = 0;
            this.btnLogin.Cursor = Cursors.Hand;
            this.btnLogin.Paint += DrawGradientButton; // Applies the gradient
            this.btnLogin.Click += BtnLogin_Click;

            // Sign Up Link
            this.lnkSignUp.Text = "Don't have an account? Sign up";
            this.lnkSignUp.Font = new Font("Segoe UI", 9);
            this.lnkSignUp.LinkColor = Color.DimGray;
            this.lnkSignUp.Location = new Point(120, 420);
            this.lnkSignUp.AutoSize = true;
            this.lnkSignUp.LinkBehavior = LinkBehavior.HoverUnderline;
            this.lnkSignUp.LinkClicked += LnkSignUp_Click;

            // Add controls to Right Panel
            this.panelRight.Controls.Add(btnClose);
            this.panelRight.Controls.Add(lblLoginHeading);
            this.panelRight.Controls.Add(lblSubtitle);
            this.panelRight.Controls.Add(lblUsername);
            this.panelRight.Controls.Add(pnlUserBorder);
            this.panelRight.Controls.Add(lblPassword);
            this.panelRight.Controls.Add(pnlPassBorder);
            this.panelRight.Controls.Add(chkRememberMe);
            this.panelRight.Controls.Add(btnLogin);
            this.panelRight.Controls.Add(lnkSignUp);

            // Add Panels to Form
            this.Controls.Add(panelRight);
            this.Controls.Add(panelLeft);
        }

        // ==========================================
        // CUSTOM DRAWING METHODS FOR GRADIENTS
        // ==========================================

        private void DrawGradientBorder(object sender, PaintEventArgs e)
        {
            Panel pnl = sender as Panel;
            using (LinearGradientBrush brush = new LinearGradientBrush(pnl.ClientRectangle, Color.FromArgb(230, 10, 150), Color.FromArgb(10, 200, 240), LinearGradientMode.Horizontal))
            {
                using (Pen pen = new Pen(brush, 2))
                {
                    e.Graphics.DrawRectangle(pen, 1, 1, pnl.Width - 2, pnl.Height - 2);
                }
            }
        }

        private void DrawGradientButton(object sender, PaintEventArgs e)
        {
            Button btn = sender as Button;
            using (LinearGradientBrush brush = new LinearGradientBrush(btn.ClientRectangle, Color.FromArgb(64, 196, 255), Color.FromArgb(141, 100, 255), LinearGradientMode.Horizontal))
            {
                e.Graphics.FillRectangle(brush, btn.ClientRectangle);
            }
            TextRenderer.DrawText(e.Graphics, btn.Text, btn.Font, btn.ClientRectangle, Color.White, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }

        // ==========================================
        // DATABASE LOGIC
        // ==========================================

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

        private void LnkSignUp_Click(object sender, LinkLabelLinkClickedEventArgs e)
        {
            signup signupPage = new signup();
            signupPage.Show();
            this.Hide();
        }
    }
}