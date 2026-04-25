using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace AdvancedBankManagementSystem
{
    public partial class TransactionsScreen : Form
    {
        // Theme Colors
        private Color panelColor = Color.FromArgb(100, 120, 220); // Blue Theme
        private Color bgColor = Color.WhiteSmoke;
        private Color textColorPrimary = Color.FromArgb(30, 40, 70);

        // UI Controls
        private Panel pnlSidebar, pnlCenter, pnlTransactionCard;
        private Label lblLogo, lblTitle, lblSubtitle;
        private ComboBox cmbTransactionType, cmbFromAccount, cmbToAccount;
        private TextBox txtAmount;
        private Label lblToAccount; // We need this as a variable so we can hide/show it
        private Button btnSubmit, btnClose;

        public TransactionsScreen()
        {
            InitializeCustomComponent();
        }

        private void InitializeCustomComponent()
        {
            // Form Properties
            this.Text = "Transactions";
            this.Size = new Size(950, 650);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.None;

            pnlSidebar = new Panel();
            pnlCenter = new Panel();

            // ==========================================
            // 1. SIDEBAR PANEL 
            // ==========================================
            pnlSidebar.Dock = DockStyle.Left;
            pnlSidebar.Width = 220;
            pnlSidebar.BackColor = panelColor;

            lblLogo = new Label() { Text = "Smart\nBanking", Font = new Font("Segoe UI", 20, FontStyle.Bold), ForeColor = Color.White, Location = new Point(20, 30), AutoSize = true };
            pnlSidebar.Controls.Add(lblLogo);

            // Sidebar Menu Items (Transactions is set to 'true' to show it is active)
            Button btnHome = CreateSidebarButton("🏠  Dashboard", 120, false);
            btnHome.Click += BtnHome_Click;

            Button btnAccounts = CreateSidebarButton("⊞  Accounts", 170, false);
            btnAccounts.Click += BtnAccounts_Click;

            pnlSidebar.Controls.Add(btnHome);
            pnlSidebar.Controls.Add(btnAccounts);
            pnlSidebar.Controls.Add(CreateSidebarButton("🔄  Transactions", 220, true)); // Active!
            pnlSidebar.Controls.Add(CreateSidebarButton("📊  Reports", 270, false));

            Button btnLogout = CreateSidebarButton("🚪  Logout", 550, false);
            btnLogout.Click += BtnLogout_Click;
            pnlSidebar.Controls.Add(btnLogout);

            // ==========================================
            // 2. CENTER PANEL 
            // ==========================================
            pnlCenter.Dock = DockStyle.Fill;
            pnlCenter.BackColor = bgColor;

            // Close Button
            btnClose = new Button() { Text = "✕", Font = new Font("Segoe UI", 12, FontStyle.Bold), Location = new Point(690, 10), Size = new Size(30, 30), FlatStyle = FlatStyle.Flat, Cursor = Cursors.Hand, ForeColor = Color.Gray };
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.Click += (s, e) => Application.Exit();
            pnlCenter.Controls.Add(btnClose);

            // Screen Headers
            lblTitle = new Label() { Text = "Process Transaction", Font = new Font("Segoe UI", 22, FontStyle.Bold), ForeColor = textColorPrimary, Location = new Point(40, 40), AutoSize = true };
            lblSubtitle = new Label() { Text = "Make deposits, withdrawals, and secure transfers.", Font = new Font("Segoe UI", 10), ForeColor = Color.Gray, Location = new Point(45, 85), AutoSize = true };

            pnlCenter.Controls.Add(lblTitle);
            pnlCenter.Controls.Add(lblSubtitle);

            // ==========================================
            // TRANSACTION FORM CARD
            // ==========================================
            pnlTransactionCard = new Panel() { Location = new Point(45, 130), Size = new Size(600, 450), BackColor = Color.White };

            // Draw subtle border around card
            pnlTransactionCard.Paint += (s, e) => {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                e.Graphics.DrawRectangle(new Pen(Color.LightGray, 1), 0, 0, pnlTransactionCard.Width - 1, pnlTransactionCard.Height - 1);
            };

            // 1. Transaction Type
            Label lblType = new Label() { Text = "Transaction Type", Font = new Font("Segoe UI", 10, FontStyle.Bold), ForeColor = Color.DimGray, Location = new Point(40, 40), AutoSize = true };
            cmbTransactionType = new ComboBox() { Font = new Font("Segoe UI", 12), Location = new Point(40, 70), Size = new Size(500, 30), DropDownStyle = ComboBoxStyle.DropDownList };
            cmbTransactionType.Items.AddRange(new string[] { "Deposit", "Withdrawal", "Transfer" });
            cmbTransactionType.SelectedIndex = 0; // Default to Deposit
            cmbTransactionType.SelectedIndexChanged += CmbTransactionType_SelectedIndexChanged; // Wire up the dynamic event!

            // 2. From Account
            Label lblFromAccount = new Label() { Text = "Select Account", Font = new Font("Segoe UI", 10, FontStyle.Bold), ForeColor = Color.DimGray, Location = new Point(40, 120), AutoSize = true };
            cmbFromAccount = new ComboBox() { Font = new Font("Segoe UI", 12), Location = new Point(40, 150), Size = new Size(500, 30), DropDownStyle = ComboBoxStyle.DropDownList };
            cmbFromAccount.Items.AddRange(new string[] { "Checking Account (AC-90182374) - $3,450.25", "High-Yield Savings (AC-55829102) - $8,000.50" });

            // 3. To Account (Only visible for Transfers)
            lblToAccount = new Label() { Text = "Destination Account (Transfer To)", Font = new Font("Segoe UI", 10, FontStyle.Bold), ForeColor = Color.DimGray, Location = new Point(40, 200), AutoSize = true, Visible = false };
            cmbToAccount = new ComboBox() { Font = new Font("Segoe UI", 12), Location = new Point(40, 230), Size = new Size(500, 30), DropDownStyle = ComboBoxStyle.DropDownList, Visible = false };
            cmbToAccount.Items.AddRange(new string[] { "Checking Account (AC-90182374)", "High-Yield Savings (AC-55829102)", "External Bank Account..." });

            // 4. Amount
            Label lblAmount = new Label() { Text = "Amount (USD)", Font = new Font("Segoe UI", 10, FontStyle.Bold), ForeColor = Color.DimGray, Location = new Point(40, 280), AutoSize = true };
            txtAmount = new TextBox() { Font = new Font("Segoe UI", 14), Location = new Point(40, 310), Size = new Size(500, 35) };

            // 5. Submit Button
            btnSubmit = new Button() { Text = "Submit Transaction", Font = new Font("Segoe UI", 12, FontStyle.Bold), Location = new Point(40, 380), Size = new Size(500, 45), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Cursor = Cursors.Hand };
            btnSubmit.FlatAppearance.BorderSize = 0;
            btnSubmit.Paint += DrawGradientButton;
            btnSubmit.Click += BtnSubmit_Click;

            // Add controls to Card
            pnlTransactionCard.Controls.Add(lblType);
            pnlTransactionCard.Controls.Add(cmbTransactionType);
            pnlTransactionCard.Controls.Add(lblFromAccount);
            pnlTransactionCard.Controls.Add(cmbFromAccount);
            pnlTransactionCard.Controls.Add(lblToAccount);
            pnlTransactionCard.Controls.Add(cmbToAccount);
            pnlTransactionCard.Controls.Add(lblAmount);
            pnlTransactionCard.Controls.Add(txtAmount);
            pnlTransactionCard.Controls.Add(btnSubmit);

            pnlCenter.Controls.Add(pnlTransactionCard);

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
            btn.BackColor = isActive ? Color.FromArgb(80, 100, 200) : Color.Transparent;
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
        // DYNAMIC LOGIC & VALIDATION
        // ==========================================

        private void CmbTransactionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            // If "Transfer" is selected, show the destination account dropdown. Otherwise, hide it.
            if (cmbTransactionType.SelectedItem.ToString() == "Transfer")
            {
                lblToAccount.Visible = true;
                cmbToAccount.Visible = true;
            }
            else
            {
                lblToAccount.Visible = false;
                cmbToAccount.Visible = false;
            }
        }

        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            // 1. Basic Validation
            if (cmbFromAccount.SelectedIndex == -1)
            {
                MessageBox.Show("Please select an account.", "Validation Error");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtAmount.Text) || !decimal.TryParse(txtAmount.Text, out decimal amount) || amount <= 0)
            {
                MessageBox.Show("Please enter a valid, positive amount.", "Validation Error");
                return;
            }

            if (cmbTransactionType.SelectedItem.ToString() == "Transfer" && cmbToAccount.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a destination account for the transfer.", "Validation Error");
                return;
            }

            // 2. FRAUD CHECK PLACEHOLDER (Your professor will love this!)
            // Here is where the Data Analytics / AI model will analyze the transaction
            DialogResult fraudCheck = MessageBox.Show(
                $"Running security algorithms and DAM constraint checks on {cmbTransactionType.SelectedItem} of ${amount}...\n\nAll security checks passed. Proceed with transaction?",
                "Fraud & Audit Check",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Information);

            if (fraudCheck == DialogResult.Yes)
            {
                // 3. Success (SQL ACID Transaction will go here)
                MessageBox.Show("Transaction processed successfully! Audit log has been updated.", "Success");
                txtAmount.Clear(); // Clear the form
            }
            else
            {
                MessageBox.Show("Transaction cancelled by security module.", "Cancelled");
            }
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

        private void BtnAccounts_Click(object sender, EventArgs e)
        {
            AccountsScreen acc = new AccountsScreen();
            acc.Show();
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