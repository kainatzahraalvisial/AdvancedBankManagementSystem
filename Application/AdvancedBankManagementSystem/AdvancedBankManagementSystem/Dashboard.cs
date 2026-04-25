using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace AdvancedBankManagementSystem
{
    public partial class Dashboard : Form
    {
        // UI Controls
        private Panel panelSidebar, panelMain, panelBalanceCard;
        private Label lblLogo, lblWelcome, lblRole, lblBalanceTitle, lblBalanceAmount;
        private Button btnHome, btnAccounts, btnTransactions, btnReports, btnLogout, btnClose;

        // User Data Variables
        private string currentUser = "Laraib";
        private string currentRole = "Administrator";

        public Dashboard()
        {
            InitializeCustomComponent();
        }

        private void InitializeCustomComponent()
        {
            this.panelSidebar = new Panel();
            this.panelMain = new Panel();
            this.panelBalanceCard = new Panel();
            this.lblLogo = new Label();
            this.lblWelcome = new Label();
            this.lblRole = new Label();
            this.lblBalanceTitle = new Label();
            this.lblBalanceAmount = new Label();
            this.btnClose = new Button();

            // Form Properties
            this.Text = "Dashboard";
            this.Size = new Size(950, 650);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.None;

            // ==========================================
            // SIDEBAR PANEL (Left)
            // ==========================================
            this.panelSidebar.Dock = DockStyle.Left;
            this.panelSidebar.Width = 220;
            this.panelSidebar.BackColor = Color.FromArgb(100, 120, 220);

            this.lblLogo.Text = "Smart\nBanking";
            this.lblLogo.Font = new Font("Segoe UI", 20, FontStyle.Bold);
            this.lblLogo.ForeColor = Color.White;
            this.lblLogo.Location = new Point(20, 30);
            this.lblLogo.AutoSize = true;

            // ==========================================
            // SIDEBAR BUTTON LINKS (FIXED HERE!)
            // ==========================================
            this.btnHome = CreateSidebarButton("🏠  Dashboard", 120);

            this.btnAccounts = CreateSidebarButton("⊞  Accounts", 170);
            this.btnAccounts.Click += (s, e) => { new AccountsScreen().Show(); this.Hide(); };

            this.btnTransactions = CreateSidebarButton("🔄  Transactions", 220);
            this.btnTransactions.Click += (s, e) => { new TransactionsScreen().Show(); this.Hide(); }; // Added Link!

            this.btnReports = CreateSidebarButton("📊  Reports", 270);
            this.btnReports.Click += (s, e) => { new ReportsScreen().Show(); this.Hide(); }; // Added Link!

            this.btnLogout = CreateSidebarButton("🚪  Logout", 550);
            this.btnLogout.Click += (s, e) =>
            {
                DialogResult result = MessageBox.Show("Are you sure you want to log out?", "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    new LoginScreen().Show();
                    this.Hide();
                }
            };

            this.panelSidebar.Controls.Add(lblLogo);
            this.panelSidebar.Controls.Add(btnHome);
            this.panelSidebar.Controls.Add(btnAccounts);
            this.panelSidebar.Controls.Add(btnTransactions);
            this.panelSidebar.Controls.Add(btnReports);
            this.panelSidebar.Controls.Add(btnLogout);

            // ==========================================
            // MAIN PANEL (Right)
            // ==========================================
            this.panelMain.Dock = DockStyle.Fill;
            this.panelMain.BackColor = Color.WhiteSmoke;

            // Close Button
            this.btnClose.Text = "✕";
            this.btnClose.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            this.btnClose.Location = new Point(690, 10);
            this.btnClose.Size = new Size(30, 30);
            this.btnClose.FlatStyle = FlatStyle.Flat;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.Cursor = Cursors.Hand;
            this.btnClose.ForeColor = Color.Gray;
            this.btnClose.Click += (s, e) => Application.Exit();

            // === WELCOME & ROLE ===
            this.lblWelcome.Text = $"Welcome back, {currentUser}!";
            this.lblWelcome.Font = new Font("Segoe UI", 22, FontStyle.Bold);
            this.lblWelcome.ForeColor = Color.FromArgb(100, 120, 220);
            this.lblWelcome.Location = new Point(40, 30);
            this.lblWelcome.AutoSize = true;

            this.lblRole.Text = $"Role: {currentRole}";
            this.lblRole.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            this.lblRole.ForeColor = Color.MediumSeaGreen;
            this.lblRole.Location = new Point(45, 75);
            this.lblRole.AutoSize = true;

            // === GRADIENT BALANCE CARD ===
            this.panelBalanceCard.Location = new Point(45, 115);
            this.panelBalanceCard.Size = new Size(350, 140);
            this.panelBalanceCard.Paint += DrawGradientCard;

            this.lblBalanceTitle.Text = "Available Balance";
            this.lblBalanceTitle.Font = new Font("Segoe UI", 12);
            this.lblBalanceTitle.ForeColor = Color.White;
            this.lblBalanceTitle.Location = new Point(20, 20);
            this.lblBalanceTitle.AutoSize = true;
            this.lblBalanceTitle.BackColor = Color.Transparent;

            this.lblBalanceAmount.Text = "$5,240.50";
            this.lblBalanceAmount.Font = new Font("Segoe UI", 32, FontStyle.Bold);
            this.lblBalanceAmount.ForeColor = Color.White;
            this.lblBalanceAmount.Location = new Point(15, 60);
            this.lblBalanceAmount.AutoSize = true;
            this.lblBalanceAmount.BackColor = Color.Transparent;

            this.panelBalanceCard.Controls.Add(lblBalanceTitle);
            this.panelBalanceCard.Controls.Add(lblBalanceAmount);

            // ==========================================
            // BIG ACTION BUTTONS GRID (Also fully linked!)
            // ==========================================
            this.panelMain.Controls.Add(CreateBigActionButton("⊞", "Accounts", "Manage user accounts", 45, 280, Color.MediumSeaGreen));
            this.panelMain.Controls.Add(CreateBigActionButton("🔄", "Transactions", "Process money transfers", 275, 280, Color.MediumPurple));
            this.panelMain.Controls.Add(CreateBigActionButton("📜", "Audit Log", "Review system activity", 505, 280, Color.SandyBrown));
            this.panelMain.Controls.Add(CreateBigActionButton("⚠️", "Fraud Alerts", "Monitor suspicious activity", 45, 420, Color.Tomato));
            this.panelMain.Controls.Add(CreateBigActionButton("📊", "Reports", "Generate financial reports", 275, 420, Color.SteelBlue));

            this.panelMain.Controls.Add(btnClose);
            this.panelMain.Controls.Add(lblWelcome);
            this.panelMain.Controls.Add(lblRole);
            this.panelMain.Controls.Add(panelBalanceCard);

            this.Controls.Add(panelMain);
            this.Controls.Add(panelSidebar);
        }

        // ==========================================
        // UI HELPER METHODS
        // ==========================================

        private Button CreateSidebarButton(string text, int yPos)
        {
            Button btn = new Button();
            btn.Text = text;
            btn.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            btn.ForeColor = Color.White;
            btn.BackColor = Color.Transparent;
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

        private Panel CreateBigActionButton(string icon, string title, string subtitle, int xPos, int yPos, Color iconColor)
        {
            Panel pnl = new Panel();
            pnl.Location = new Point(xPos, yPos);
            pnl.Size = new Size(210, 120);
            pnl.BackColor = Color.White;
            pnl.Cursor = Cursors.Hand;

            pnl.Paint += (s, e) => {
                Control c = s as Control;
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                e.Graphics.DrawRectangle(new Pen(Color.LightGray, 1), 0, 0, c.Width - 1, c.Height - 1);
            };

            Label lblIcon = new Label() { Text = icon, Font = new Font("Segoe UI", 24), ForeColor = iconColor, Location = new Point(15, 15), AutoSize = true, Cursor = Cursors.Hand };
            Label lblTitle = new Label() { Text = title, Font = new Font("Segoe UI", 12, FontStyle.Bold), ForeColor = Color.FromArgb(30, 40, 70), Location = new Point(15, 60), AutoSize = true, Cursor = Cursors.Hand };
            Label lblSub = new Label() { Text = subtitle, Font = new Font("Segoe UI", 8), ForeColor = Color.Gray, Location = new Point(15, 85), Size = new Size(180, 30), Cursor = Cursors.Hand };

            pnl.Controls.Add(lblIcon);
            pnl.Controls.Add(lblTitle);
            pnl.Controls.Add(lblSub);

            // This ensures clicking the big cards also works perfectly!
            EventHandler clickEvent = (s, e) => {
                if (title == "Accounts") { new AccountsScreen().Show(); this.Hide(); }
                else if (title == "Transactions") { new TransactionsScreen().Show(); this.Hide(); }
                else if (title == "Audit Log") { new AuditLogScreen().Show(); this.Hide(); }
                else if (title == "Fraud Alerts") { new FraudAlertsScreen().Show(); this.Hide(); }
                else if (title == "Reports") { new ReportsScreen().Show(); this.Hide(); }
            };

            pnl.Click += clickEvent;
            lblIcon.Click += clickEvent;
            lblTitle.Click += clickEvent;
            lblSub.Click += clickEvent;

            return pnl;
        }

        private void DrawGradientCard(object sender, PaintEventArgs e)
        {
            Panel pnl = sender as Panel;
            using (LinearGradientBrush brush = new LinearGradientBrush(pnl.ClientRectangle, Color.FromArgb(64, 196, 255), Color.FromArgb(141, 100, 255), LinearGradientMode.ForwardDiagonal))
            {
                e.Graphics.FillRectangle(brush, pnl.ClientRectangle);
            }
        }
    }
}