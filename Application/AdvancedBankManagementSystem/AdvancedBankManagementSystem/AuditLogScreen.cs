using System;
using System.Drawing;
using System.Windows.Forms;

namespace AdvancedBankManagementSystem
{
    public partial class AuditLogScreen : Form
    {
        private Color sidebarColor = Color.FromArgb(100, 120, 220);

        public AuditLogScreen()
        {
            InitializeCustomComponent();
        }

        private void InitializeCustomComponent()
        {
            this.Text = "Audit Log";
            this.Size = new Size(950, 650);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.None;

            // --- SIDEBAR ---
            Panel pnlSidebar = new Panel() { Dock = DockStyle.Left, Width = 220, BackColor = sidebarColor };
            pnlSidebar.Controls.Add(new Label() { Text = "Smart\nBanking", Font = new Font("Segoe UI", 20, FontStyle.Bold), ForeColor = Color.White, Location = new Point(20, 30), AutoSize = true });

            pnlSidebar.Controls.Add(CreateSidebarButton("🏠  Dashboard", 120, false));
            pnlSidebar.Controls.Add(CreateSidebarButton("⊞  Accounts", 170, false));
            pnlSidebar.Controls.Add(CreateSidebarButton("🔄  Transactions", 220, false));
            pnlSidebar.Controls.Add(CreateSidebarButton("📜  Audit Log", 270, true)); // Active
            pnlSidebar.Controls.Add(CreateSidebarButton("⚠️  Fraud Alerts", 320, false));
            pnlSidebar.Controls.Add(CreateSidebarButton("📊  Reports", 370, false));
            pnlSidebar.Controls.Add(CreateSidebarButton("🚪  Logout", 550, false));

            // --- MAIN CENTER PANEL ---
            Panel pnlCenter = new Panel() { Dock = DockStyle.Fill, BackColor = Color.WhiteSmoke };

            Button btnClose = new Button() { Text = "✕", Font = new Font("Segoe UI", 12, FontStyle.Bold), Location = new Point(690, 10), Size = new Size(30, 30), FlatStyle = FlatStyle.Flat, Cursor = Cursors.Hand, ForeColor = Color.Gray };
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.Click += (s, e) => Application.Exit();

            Label lblTitle = new Label() { Text = "System Audit Log", Font = new Font("Segoe UI", 22, FontStyle.Bold), ForeColor = Color.FromArgb(30, 40, 70), Location = new Point(40, 40), AutoSize = true };
            Label lblSub = new Label() { Text = "Review all system changes and transactions.", Font = new Font("Segoe UI", 10), ForeColor = Color.Gray, Location = new Point(45, 85), AutoSize = true };

            // --- DATA GRID VIEW (The Table) ---
            DataGridView dgvAudit = new DataGridView();
            dgvAudit.Location = new Point(45, 130);
            dgvAudit.Size = new Size(640, 480);
            dgvAudit.BackgroundColor = Color.White;
            dgvAudit.BorderStyle = BorderStyle.None;
            dgvAudit.RowHeadersVisible = false;
            dgvAudit.AllowUserToAddRows = false;
            dgvAudit.ReadOnly = true;
            dgvAudit.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvAudit.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // Styling the Grid
            dgvAudit.EnableHeadersVisualStyles = false;
            dgvAudit.ColumnHeadersDefaultCellStyle.BackColor = sidebarColor;
            dgvAudit.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvAudit.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvAudit.ColumnHeadersHeight = 40;
            dgvAudit.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgvAudit.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 235, 255);
            dgvAudit.DefaultCellStyle.SelectionForeColor = Color.Black;

            // Adding Columns
            dgvAudit.Columns.Add("Time", "Timestamp");
            dgvAudit.Columns.Add("User", "User / Role");
            dgvAudit.Columns.Add("Action", "Operation");
            dgvAudit.Columns.Add("Details", "Details");

            // Adding Mock Data Rows
            dgvAudit.Rows.Add("10:15 AM", "Laraib (Admin)", "Login", "Successful login via Web");
            dgvAudit.Rows.Add("10:18 AM", "Laraib (Admin)", "Create Account", "Created AC-11928475");
            dgvAudit.Rows.Add("10:25 AM", "System (Auto)", "Transaction", "Transfer $500 to AC-90182374");
            dgvAudit.Rows.Add("11:05 AM", "John Doe (User)", "Failed Login", "Invalid Password Attempt");

            pnlCenter.Controls.Add(btnClose);
            pnlCenter.Controls.Add(lblTitle);
            pnlCenter.Controls.Add(lblSub);
            pnlCenter.Controls.Add(dgvAudit);

            this.Controls.Add(pnlCenter);
            this.Controls.Add(pnlSidebar);
        }

        private Button CreateSidebarButton(string text, int yPos, bool isActive)
        {
            Button btn = new Button() { Text = text, Font = new Font("Segoe UI", 11, isActive ? FontStyle.Bold : FontStyle.Regular), ForeColor = Color.White, BackColor = isActive ? Color.FromArgb(80, 100, 200) : Color.Transparent, FlatStyle = FlatStyle.Flat, Location = new Point(0, yPos), Size = new Size(220, 50), TextAlign = ContentAlignment.MiddleLeft, Padding = new Padding(20, 0, 0, 0), Cursor = Cursors.Hand };
            btn.FlatAppearance.BorderSize = 0;
            btn.Click += Sidebar_Click; // Universal click handler
            return btn;
        }

        private void Sidebar_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if (btn.Text.Contains("Dashboard")) { new Dashboard().Show(); this.Hide(); }
            else if (btn.Text.Contains("Logout")) { new LoginScreen().Show(); this.Hide(); }
        }
    }
}