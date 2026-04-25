using System;
using System.Drawing;
using System.Windows.Forms;

namespace AdvancedBankManagementSystem
{
    public partial class FraudAlertsScreen : Form
    {
        private Color sidebarColor = Color.FromArgb(100, 120, 220);

        public FraudAlertsScreen()
        {
            InitializeCustomComponent();
        }

        private void InitializeCustomComponent()
        {
            this.Text = "Fraud Alerts";
            this.Size = new Size(950, 650);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.None;

            // --- SIDEBAR ---
            Panel pnlSidebar = new Panel() { Dock = DockStyle.Left, Width = 220, BackColor = sidebarColor };
            pnlSidebar.Controls.Add(new Label() { Text = "Smart\nBanking", Font = new Font("Segoe UI", 20, FontStyle.Bold), ForeColor = Color.White, Location = new Point(20, 30), AutoSize = true });

            pnlSidebar.Controls.Add(CreateSidebarButton("🏠  Dashboard", 120, false));
            pnlSidebar.Controls.Add(CreateSidebarButton("⊞  Accounts", 170, false));
            pnlSidebar.Controls.Add(CreateSidebarButton("🔄  Transactions", 220, false));
            pnlSidebar.Controls.Add(CreateSidebarButton("⚠️  Fraud Alerts", 320, true)); // Active
            pnlSidebar.Controls.Add(CreateSidebarButton("🚪  Logout", 550, false));

            // --- MAIN CENTER PANEL ---
            Panel pnlCenter = new Panel() { Dock = DockStyle.Fill, BackColor = Color.WhiteSmoke };

            Button btnClose = new Button() { Text = "✕", Font = new Font("Segoe UI", 12, FontStyle.Bold), Location = new Point(690, 10), Size = new Size(30, 30), FlatStyle = FlatStyle.Flat, Cursor = Cursors.Hand, ForeColor = Color.Gray };
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.Click += (s, e) => Application.Exit();

            Label lblTitle = new Label() { Text = "Fraud Alerts", Font = new Font("Segoe UI", 22, FontStyle.Bold), ForeColor = Color.FromArgb(30, 40, 70), Location = new Point(40, 40), AutoSize = true };

            FlowLayoutPanel flpAlerts = new FlowLayoutPanel() { Location = new Point(45, 100), Size = new Size(640, 500), AutoScroll = true, FlowDirection = FlowDirection.TopDown, WrapContents = false };

            // Adding Mock Alert Cards
            flpAlerts.Controls.Add(CreateAlertCard("TXN-9921", "Multiple failed logins followed by large transfer attempt.", "HIGH RISK", Color.Tomato));
            flpAlerts.Controls.Add(CreateAlertCard("TXN-8842", "Login from unusual IP address (Overseas).", "MEDIUM RISK", Color.Orange));
            flpAlerts.Controls.Add(CreateAlertCard("AC-5592", "Account created with suspicious verification data.", "HIGH RISK", Color.Tomato));

            pnlCenter.Controls.Add(btnClose);
            pnlCenter.Controls.Add(lblTitle);
            pnlCenter.Controls.Add(flpAlerts);

            this.Controls.Add(pnlCenter);
            this.Controls.Add(pnlSidebar);
        }

        private Panel CreateAlertCard(string id, string details, string risk, Color riskColor)
        {
            Panel pnl = new Panel() { Size = new Size(610, 100), BackColor = Color.White, Margin = new Padding(0, 0, 0, 15) };
            pnl.Paint += (s, e) => e.Graphics.DrawRectangle(new Pen(Color.LightGray, 1), 0, 0, pnl.Width - 1, pnl.Height - 1);

            Panel pnlIndicator = new Panel() { Location = new Point(0, 0), Size = new Size(8, 100), BackColor = riskColor };
            Label lblID = new Label() { Text = $"Alert ID: {id}", Font = new Font("Segoe UI", 12, FontStyle.Bold), ForeColor = Color.FromArgb(30, 40, 70), Location = new Point(30, 15), AutoSize = true };
            Label lblDetails = new Label() { Text = details, Font = new Font("Segoe UI", 9), ForeColor = Color.DimGray, Location = new Point(30, 45), Size = new Size(400, 40) };
            Label lblRisk = new Label() { Text = risk, Font = new Font("Segoe UI", 10, FontStyle.Bold), ForeColor = riskColor, Location = new Point(480, 20), AutoSize = true };

            Button btnAction = new Button() { Text = "Investigate", Font = new Font("Segoe UI", 9, FontStyle.Bold), Location = new Point(480, 50), Size = new Size(100, 30), BackColor = Color.WhiteSmoke, FlatStyle = FlatStyle.Flat };
            btnAction.FlatAppearance.BorderSize = 1; btnAction.FlatAppearance.BorderColor = Color.LightGray;

            pnl.Controls.Add(pnlIndicator); pnl.Controls.Add(lblID); pnl.Controls.Add(lblDetails); pnl.Controls.Add(lblRisk); pnl.Controls.Add(btnAction);
            return pnl;
        }

        private Button CreateSidebarButton(string text, int yPos, bool isActive)
        {
            Button btn = new Button() { Text = text, Font = new Font("Segoe UI", 11, isActive ? FontStyle.Bold : FontStyle.Regular), ForeColor = Color.White, BackColor = isActive ? Color.FromArgb(80, 100, 200) : Color.Transparent, FlatStyle = FlatStyle.Flat, Location = new Point(0, yPos), Size = new Size(220, 50), TextAlign = ContentAlignment.MiddleLeft, Padding = new Padding(20, 0, 0, 0), Cursor = Cursors.Hand };
            btn.FlatAppearance.BorderSize = 0;
            btn.Click += Sidebar_Click;
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