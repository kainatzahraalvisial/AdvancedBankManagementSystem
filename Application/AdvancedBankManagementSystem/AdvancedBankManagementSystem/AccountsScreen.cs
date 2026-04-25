using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace AdvancedBankManagementSystem
{
    public partial class AccountsScreen : Form
    {
        // UI Controls
        private Panel pnlSidebar, pnlCenter;
        private FlowLayoutPanel flpAccountsList;
        private Label lblLogo, lblTitle, lblTotalBalance, lblBalanceAmount;
        private Button btnCreateAccount, btnClose;

        public AccountsScreen()
        {
            InitializeCustomComponent();
        }

        private void InitializeCustomComponent()
        {
            // Form Properties
            this.Text = "Manage Accounts";
            this.Size = new Size(950, 650); // Matched size with Dashboard
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.None;

            pnlSidebar = new Panel();
            pnlCenter = new Panel();

            // ==========================================
            // 1. SIDEBAR PANEL (Blue Theme)
            // ==========================================
            pnlSidebar.Dock = DockStyle.Left;
            pnlSidebar.Width = 220;
            pnlSidebar.BackColor = Color.FromArgb(100, 120, 220); // Blue Theme

            lblLogo = new Label() { Text = "Smart\nBanking", Font = new Font("Segoe UI", 20, FontStyle.Bold), ForeColor = Color.White, Location = new Point(20, 30), AutoSize = true };
            pnlSidebar.Controls.Add(lblLogo);

            // Sidebar Menu Items (Accounts is set to 'true' to show it is active)
            Button btnHome = CreateSidebarButton("🏠  Dashboard", 120, false);
            btnHome.Click += BtnHome_Click; // Links back to dashboard

            pnlSidebar.Controls.Add(btnHome);
            pnlSidebar.Controls.Add(CreateSidebarButton("⊞  Accounts", 170, true)); // Active!
            pnlSidebar.Controls.Add(CreateSidebarButton("🔄  Transactions", 220, false));
            pnlSidebar.Controls.Add(CreateSidebarButton("📊  Reports", 270, false));

            Button btnLogout = CreateSidebarButton("🚪  Logout", 550, false);
            btnLogout.Click += BtnLogout_Click;
            pnlSidebar.Controls.Add(btnLogout);

            // ==========================================
            // 2. CENTER PANEL (Accounts List View)
            // ==========================================
            pnlCenter.Dock = DockStyle.Fill;
            pnlCenter.BackColor = Color.WhiteSmoke;

            // Close Button
            btnClose = new Button() { Text = "✕", Font = new Font("Segoe UI", 12, FontStyle.Bold), Location = new Point(690, 10), Size = new Size(30, 30), FlatStyle = FlatStyle.Flat, Cursor = Cursors.Hand, ForeColor = Color.Gray };
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.Click += (s, e) => Application.Exit();
            pnlCenter.Controls.Add(btnClose);

            // Screen Headers
            lblTitle = new Label() { Text = "Manage Accounts", Font = new Font("Segoe UI", 22, FontStyle.Bold), ForeColor = Color.FromArgb(30, 40, 70), Location = new Point(40, 40), AutoSize = true };

            lblTotalBalance = new Label() { Text = "Total Balance (All Accounts)", Font = new Font("Segoe UI", 10), ForeColor = Color.Gray, Location = new Point(45, 90), AutoSize = true };
            lblBalanceAmount = new Label() { Text = "$12,450.75", Font = new Font("Segoe UI", 20, FontStyle.Bold), ForeColor = Color.MediumSeaGreen, Location = new Point(40, 110), AutoSize = true };

            // Create New Account Button
            btnCreateAccount = new Button() { Text = "+ Create New Account", Font = new Font("Segoe UI", 10, FontStyle.Bold), Location = new Point(500, 110), Size = new Size(180, 40), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Cursor = Cursors.Hand };
            btnCreateAccount.FlatAppearance.BorderSize = 0;
            btnCreateAccount.Paint += DrawGradientButton;
            btnCreateAccount.Click += (s, e) => MessageBox.Show("Opening 'Create Account' wizard...", "New Account");

            pnlCenter.Controls.Add(lblTitle);
            pnlCenter.Controls.Add(lblTotalBalance);
            pnlCenter.Controls.Add(lblBalanceAmount);
            pnlCenter.Controls.Add(btnCreateAccount);

            // ==========================================
            // ACCOUNT LIST (Dynamic Stacking Panel)
            // ==========================================
            flpAccountsList = new FlowLayoutPanel();
            flpAccountsList.Location = new Point(45, 180);
            flpAccountsList.Size = new Size(650, 420);
            flpAccountsList.AutoScroll = true;
            flpAccountsList.FlowDirection = FlowDirection.TopDown;
            flpAccountsList.WrapContents = false;

            // Adding Mock Data 
            flpAccountsList.Controls.Add(CreateAccountCard("Checking Account", "AC-90182374", "$3,450.25", Color.MediumPurple));
            flpAccountsList.Controls.Add(CreateAccountCard("High-Yield Savings", "AC-55829102", "$8,000.50", Color.MediumSeaGreen));
            flpAccountsList.Controls.Add(CreateAccountCard("Business Checking", "AC-11928475", "$1,000.00", Color.SandyBrown));

            pnlCenter.Controls.Add(flpAccountsList);

            this.Controls.Add(pnlCenter);
            this.Controls.Add(pnlSidebar);
        }

        // ==========================================
        // UI HELPER METHODS
        // ==========================================

        private Button CreateSidebarButton(string text, int yPos, bool isActive)
        {
            Button btn = new Button();
            btn.Text = text;
            btn.Font = new Font("Segoe UI", 11, isActive ? FontStyle.Bold : FontStyle.Regular);
            btn.ForeColor = Color.White;
            btn.BackColor = isActive ? Color.FromArgb(80, 100, 200) : Color.Transparent; // Darker blue if active
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(80, 100, 200);
            btn.Location = new Point(0, yPos);
            btn.Size = new Size(220, 50);
            btn.TextAlign = ContentAlignment.MiddleLeft;
            btn.Padding = new Padding(20, 0, 0, 0);
            btn.Cursor = Cursors.Hand;
            return btn;
        }

        // Dynamically creates a beautiful row for each account (Light Theme)
        private Panel CreateAccountCard(string type, string accNum, string balance, Color accentColor)
        {
            Panel pnl = new Panel() { Size = new Size(620, 80), BackColor = Color.White, Margin = new Padding(0, 0, 0, 15) };

            // Draw subtle border
            pnl.Paint += (s, e) => {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                e.Graphics.DrawRectangle(new Pen(Color.LightGray, 1), 0, 0, pnl.Width - 1, pnl.Height - 1);
            };

            // Color indicator block on the left
            Panel pnlColor = new Panel() { Location = new Point(0, 0), Size = new Size(6, 80), BackColor = accentColor };

            Label lblType = new Label() { Text = type, Font = new Font("Segoe UI", 12, FontStyle.Bold), ForeColor = Color.FromArgb(30, 40, 70), Location = new Point(30, 20), AutoSize = true };
            Label lblNum = new Label() { Text = $"Account #: {accNum}", Font = new Font("Segoe UI", 9), ForeColor = Color.Gray, Location = new Point(30, 45), AutoSize = true };

            Label lblBal = new Label() { Text = balance, Font = new Font("Segoe UI", 16, FontStyle.Bold), ForeColor = Color.FromArgb(30, 40, 70), Location = new Point(480, 25), AutoSize = true };

            pnl.Controls.Add(pnlColor);
            pnl.Controls.Add(lblType);
            pnl.Controls.Add(lblNum);
            pnl.Controls.Add(lblBal);

            return pnl;
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
        // NAVIGATION LOGIC
        // ==========================================

        private void BtnHome_Click(object sender, EventArgs e)
        {
            Dashboard dash = new Dashboard();
            dash.Show();
            this.Hide();
        }

        private void BtnLogout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to log out?", "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                LoginScreen login = new LoginScreen();
                login.Show();
                this.Hide();
            }
        }
    }
}