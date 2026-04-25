using System;
using System.Drawing;
using System.Windows.Forms;

namespace AdvancedBankManagementSystem
{
    public partial class ReportsScreen : Form
    {
        private Color sidebarColor = Color.FromArgb(100, 120, 220);

        public ReportsScreen()
        {
            InitializeCustomComponent();
        }

        private void InitializeCustomComponent()
        {
            this.Text = "Reports & Analytics";
            this.Size = new Size(950, 650);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.None;

            // --- SIDEBAR ---
            Panel pnlSidebar = new Panel() { Dock = DockStyle.Left, Width = 220, BackColor = sidebarColor };
            pnlSidebar.Controls.Add(new Label() { Text = "Smart\nBanking", Font = new Font("Segoe UI", 20, FontStyle.Bold), ForeColor = Color.White, Location = new Point(20, 30), AutoSize = true });

            pnlSidebar.Controls.Add(CreateSidebarButton("🏠  Dashboard", 120, false));
            pnlSidebar.Controls.Add(CreateSidebarButton("⊞  Accounts", 170, false));
            pnlSidebar.Controls.Add(CreateSidebarButton("📊  Reports", 370, true)); // Active
            pnlSidebar.Controls.Add(CreateSidebarButton("🚪  Logout", 550, false));

            // --- MAIN CENTER PANEL ---
            Panel pnlCenter = new Panel() { Dock = DockStyle.Fill, BackColor = Color.WhiteSmoke };

            Button btnClose = new Button() { Text = "✕", Font = new Font("Segoe UI", 12, FontStyle.Bold), Location = new Point(690, 10), Size = new Size(30, 30), FlatStyle = FlatStyle.Flat, Cursor = Cursors.Hand, ForeColor = Color.Gray };
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.Click += (s, e) => Application.Exit();

            Label lblTitle = new Label() { Text = "System Analytics", Font = new Font("Segoe UI", 22, FontStyle.Bold), ForeColor = Color.FromArgb(30, 40, 70), Location = new Point(40, 40), AutoSize = true };

            // Stats Cards
            pnlCenter.Controls.Add(CreateStatCard("Transactions Per Second (TPS)", "45.2 Avg", 45, 110, Color.MediumSeaGreen));
            pnlCenter.Controls.Add(CreateStatCard("Fraud Detection Rate", "98.7%", 270, 110, Color.MediumPurple));
            pnlCenter.Controls.Add(CreateStatCard("Total Processed (24h)", "$1.2M", 495, 110, Color.SteelBlue));

            // Chart Area
            Label lblChart = new Label() { Text = "Transaction Volume (Last 7 Days)", Font = new Font("Segoe UI", 14, FontStyle.Bold), ForeColor = Color.DimGray, Location = new Point(45, 260), AutoSize = true };
            Panel pnlChart = new Panel() { Location = new Point(45, 300), Size = new Size(640, 250), BackColor = Color.White };
            pnlChart.Paint += DrawMockChart;

            pnlCenter.Controls.Add(btnClose);
            pnlCenter.Controls.Add(lblTitle);
            pnlCenter.Controls.Add(lblChart);
            pnlCenter.Controls.Add(pnlChart);

            this.Controls.Add(pnlCenter);
            this.Controls.Add(pnlSidebar);
        }

        private Panel CreateStatCard(string title, string value, int x, int y, Color accent)
        {
            Panel pnl = new Panel() { Location = new Point(x, y), Size = new Size(210, 120), BackColor = Color.White };
            pnl.Paint += (s, e) => e.Graphics.DrawRectangle(new Pen(Color.LightGray, 1), 0, 0, pnl.Width - 1, pnl.Height - 1);

            Label lblTitle = new Label() { Text = title, Font = new Font("Segoe UI", 9, FontStyle.Bold), ForeColor = Color.Gray, Location = new Point(15, 20), Size = new Size(180, 35) };
            Label lblValue = new Label() { Text = value, Font = new Font("Segoe UI", 24, FontStyle.Bold), ForeColor = accent, Location = new Point(15, 60), AutoSize = true };

            pnl.Controls.Add(lblTitle); pnl.Controls.Add(lblValue);
            return pnl;
        }

        // Custom drawn bar chart!
        private void DrawMockChart(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            int[] dataPoints = { 80, 120, 150, 90, 200, 170, 220 };
            string[] days = { "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun" };

            int startX = 40;
            int barWidth = 40;
            int spacing = 80;

            for (int i = 0; i < dataPoints.Length; i++)
            {
                int x = startX + (i * spacing);
                int y = 200 - dataPoints[i]; // Bottom aligned

                // Draw Bar
                g.FillRectangle(new SolidBrush(Color.FromArgb(100, 120, 220)), x, y, barWidth, dataPoints[i]);

                // Draw Label
                g.DrawString(days[i], new Font("Segoe UI", 9), Brushes.Gray, x + 5, 210);
            }
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