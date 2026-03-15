using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace AdvancedBankManagementSystem
{
    public partial class signup : Form
    {
        private string connectionString = @"Server=.\SQLEXPRESS;Database=BankManagementSystem;Integrated Security=True;";

        // UI Controls
        private Panel panelLeft, panelRight;
        private Label lblWelcomeLeft, lblSignupHeading, lblSubtitle;
        private Label lblUsername, lblPassword, lblConfirmPassword;
        private TextBox txtUsername, txtPassword, txtConfirmPassword;
        private Panel pnlUserBorder, pnlPassBorder, pnlConfirmBorder; 
        private Button btnSignup, btnClose;
        private LinkLabel lnkLogin;

        public signup()
        {
            InitializeCustomComponent();
        }

        private void InitializeCustomComponent()
        {
            this.panelLeft = new Panel();
            this.panelRight = new Panel();
            this.lblWelcomeLeft = new Label();
            this.lblSignupHeading = new Label();
            this.lblSubtitle = new Label();
            this.lblUsername = new Label();
            this.txtUsername = new TextBox();
            this.lblPassword = new Label();
            this.txtPassword = new TextBox();
            this.lblConfirmPassword = new Label();
            this.txtConfirmPassword = new TextBox();
            this.btnSignup = new Button();
            this.btnClose = new Button();
            this.lnkLogin = new LinkLabel();
            
            this.pnlUserBorder = new Panel();
            this.pnlPassBorder = new Panel();
            this.pnlConfirmBorder = new Panel();

            // Form Properties
            this.Text = "Sign Up";
            this.Size = new Size(800, 550);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.None;

            // === LEFT PANEL ===
            this.panelLeft.Dock = DockStyle.Left;
            this.panelLeft.Width = 350;
            this.panelLeft.BackColor = Color.FromArgb(100, 120, 220);

            this.lblWelcomeLeft.Text = "Smart Banking\nStarts Here";
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

            // === HEADING ===
            this.lblSignupHeading.Text = "Create Account";
            this.lblSignupHeading.Font = new Font("Segoe UI", 22, FontStyle.Bold);
            // ---> THIS IS THE ONLY LINE THAT CHANGED (Matches the magenta color) <---
            this.lblSignupHeading.ForeColor = Color.FromArgb(100, 120, 220); 
            this.lblSignupHeading.Location = new Point(60, 40);
            this.lblSignupHeading.AutoSize = true;
            
            this.lblSubtitle.Text = "Sign up to get started with our banking services.";
            this.lblSubtitle.Font = new Font("Segoe UI", 9);
            this.lblSubtitle.ForeColor = Color.Gray;
            this.lblSubtitle.Location = new Point(64, 85);
            this.lblSubtitle.AutoSize = true;

            // === USERNAME ===
            this.lblUsername.Text = "User Name";
            this.lblUsername.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            this.lblUsername.ForeColor = Color.DimGray;
            this.lblUsername.Location = new Point(60, 130);
            this.lblUsername.AutoSize = true;

            this.pnlUserBorder.Location = new Point(64, 150);
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
            this.lblPassword.Location = new Point(60, 200);
            this.lblPassword.AutoSize = true;

            this.pnlPassBorder.Location = new Point(64, 220);
            this.pnlPassBorder.Size = new Size(320, 36);
            this.pnlPassBorder.BackColor = Color.White;
            this.pnlPassBorder.Paint += DrawGradientBorder;

            this.txtPassword.BorderStyle = BorderStyle.None;
            this.txtPassword.Location = new Point(5, 8);
            this.txtPassword.Size = new Size(310, 20);
            this.txtPassword.Font = new Font("Segoe UI", 11);
            this.txtPassword.PasswordChar = '*';
            this.pnlPassBorder.Controls.Add(txtPassword);

            // === CONFIRM PASSWORD ===
            this.lblConfirmPassword.Text = "Confirm Password";
            this.lblConfirmPassword.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            this.lblConfirmPassword.ForeColor = Color.DimGray;
            this.lblConfirmPassword.Location = new Point(60, 270);
            this.lblConfirmPassword.AutoSize = true;

            this.pnlConfirmBorder.Location = new Point(64, 290);
            this.pnlConfirmBorder.Size = new Size(320, 36);
            this.pnlConfirmBorder.BackColor = Color.White;
            this.pnlConfirmBorder.Paint += DrawGradientBorder;

            this.txtConfirmPassword.BorderStyle = BorderStyle.None;
            this.txtConfirmPassword.Location = new Point(5, 8);
            this.txtConfirmPassword.Size = new Size(310, 20);
            this.txtConfirmPassword.Font = new Font("Segoe UI", 11);
            this.txtConfirmPassword.PasswordChar = '*';
            this.pnlConfirmBorder.Controls.Add(txtConfirmPassword);

            // === SIGNUP BUTTON ===
            this.btnSignup.Text = "Sign Up";
            this.btnSignup.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            this.btnSignup.Location = new Point(64, 350);
            this.btnSignup.Size = new Size(320, 45);
            this.btnSignup.ForeColor = Color.White;
            this.btnSignup.FlatStyle = FlatStyle.Flat;
            this.btnSignup.FlatAppearance.BorderSize = 0;
            this.btnSignup.Cursor = Cursors.Hand;
            this.btnSignup.Paint += DrawGradientButton; 
            this.btnSignup.Click += BtnSignup_Click;

            // === LOGIN LINK ===
            this.lnkLogin.Text = "Already have an account? Login here";
            this.lnkLogin.Font = new Font("Segoe UI", 9);
            this.lnkLogin.LinkColor = Color.DimGray;
            this.lnkLogin.Location = new Point(100, 410);
            this.lnkLogin.AutoSize = true;
            this.lnkLogin.LinkBehavior = LinkBehavior.HoverUnderline;
            this.lnkLogin.LinkClicked += LnkLogin_Click;

            // Add Controls to Right Panel
            this.panelRight.Controls.Add(btnClose);
            this.panelRight.Controls.Add(lblSignupHeading);
            this.panelRight.Controls.Add(lblSubtitle);
            this.panelRight.Controls.Add(lblUsername);
            this.panelRight.Controls.Add(pnlUserBorder); 
            this.panelRight.Controls.Add(lblPassword);
            this.panelRight.Controls.Add(pnlPassBorder);
            this.panelRight.Controls.Add(lblConfirmPassword);
            this.panelRight.Controls.Add(pnlConfirmBorder);
            this.panelRight.Controls.Add(btnSignup);
            this.panelRight.Controls.Add(lnkLogin);

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

        private void BtnSignup_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Please fill out all fields.", "Error");
                return;
            }

            if (txtPassword.Text != txtConfirmPassword.Text)
            {
                MessageBox.Show("Passwords do not match!", "Error");
                return;
            }

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    
                    string checkQuery = "SELECT COUNT(*) FROM Users WHERE Username = @user";
                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, con))
                    {
                        checkCmd.Parameters.AddWithValue("@user", txtUsername.Text.Trim());
                        int userExists = (int)checkCmd.ExecuteScalar();
                        
                        if (userExists > 0)
                        {
                            MessageBox.Show("Username already exists. Please choose a different one.", "Error");
                            return;
                        }
                    }

                    string query = "INSERT INTO Users (Username, Password, Role) VALUES (@user, @pass, 'Customer')";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@user", txtUsername.Text.Trim());
                        cmd.Parameters.AddWithValue("@pass", txtPassword.Text.Trim());

                        int result = cmd.ExecuteNonQuery();
                        if (result > 0)
                        {
                            MessageBox.Show("Account created successfully! You can now log in.", "Success");
                            
                            LoginScreen login = new LoginScreen();
                            login.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Failed to create account. Try again.", "Error");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Database error: " + ex.Message);
                }
            }
        }

        private void LnkLogin_Click(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LoginScreen login = new LoginScreen();
            login.Show();
            this.Hide();
        }
    }
}