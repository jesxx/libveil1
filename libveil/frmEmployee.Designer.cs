namespace libveil
{
    partial class frmEmployee
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            pnlTop = new Panel();
            btnGuide = new Button();
            lblWelcome = new Label();
            pnlMenu = new Panel();
            grpManagement = new GroupBox();
            btnCatalog = new Button();
            btnOrders = new Button();
            btnUsers = new Button();
            btnEmployees = new Button();
            grpReports = new GroupBox();
            btnReport1 = new Button();
            btnReport2 = new Button();
            btnReport3 = new Button();
            btnExit = new Button();
            pnlTop.SuspendLayout();
            pnlMenu.SuspendLayout();
            grpManagement.SuspendLayout();
            grpReports.SuspendLayout();
            SuspendLayout();
            // 
            // pnlTop
            // 
            pnlTop.BackColor = Color.FromArgb(240, 240, 240);
            pnlTop.Controls.Add(btnGuide);
            pnlTop.Controls.Add(lblWelcome);
            pnlTop.Dock = DockStyle.Top;
            pnlTop.Location = new Point(0, 0);
            pnlTop.Margin = new Padding(4, 3, 4, 3);
            pnlTop.Name = "pnlTop";
            pnlTop.Size = new Size(915, 69);
            pnlTop.TabIndex = 0;
            // 
            // btnGuide
            // 
            btnGuide.BackColor = Color.Ivory;
            btnGuide.Font = new Font("Segoe UI", 10F);
            btnGuide.Location = new Point(791, 23);
            btnGuide.Margin = new Padding(4, 3, 4, 3);
            btnGuide.Name = "btnGuide";
            btnGuide.Size = new Size(96, 29);
            btnGuide.TabIndex = 8;
            btnGuide.Text = "Справка";
            btnGuide.UseVisualStyleBackColor = false;
            // 
            // lblWelcome
            // 
            lblWelcome.AutoSize = true;
            lblWelcome.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point, 204);
            lblWelcome.Location = new Point(23, 23);
            lblWelcome.Margin = new Padding(4, 0, 4, 0);
            lblWelcome.Name = "lblWelcome";
            lblWelcome.Size = new Size(184, 25);
            lblWelcome.TabIndex = 0;
            lblWelcome.Text = "Добро пожаловать!";
            // 
            // pnlMenu
            // 
            pnlMenu.Controls.Add(grpManagement);
            pnlMenu.Controls.Add(grpReports);
            pnlMenu.Controls.Add(btnExit);
            pnlMenu.Dock = DockStyle.Fill;
            pnlMenu.Location = new Point(0, 69);
            pnlMenu.Margin = new Padding(4, 3, 4, 3);
            pnlMenu.Name = "pnlMenu";
            pnlMenu.Padding = new Padding(23);
            pnlMenu.Size = new Size(915, 463);
            pnlMenu.TabIndex = 1;
            // 
            // grpManagement
            // 
            grpManagement.Controls.Add(btnCatalog);
            grpManagement.Controls.Add(btnOrders);
            grpManagement.Controls.Add(btnUsers);
            grpManagement.Controls.Add(btnEmployees);
            grpManagement.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 204);
            grpManagement.Location = new Point(23, 23);
            grpManagement.Margin = new Padding(4, 3, 4, 3);
            grpManagement.Name = "grpManagement";
            grpManagement.Padding = new Padding(4, 3, 4, 3);
            grpManagement.Size = new Size(420, 323);
            grpManagement.TabIndex = 0;
            grpManagement.TabStop = false;
            grpManagement.Text = "Управление";
            // 
            // btnCatalog
            // 
            btnCatalog.Font = new Font("Segoe UI", 10F);
            btnCatalog.Location = new Point(23, 35);
            btnCatalog.Margin = new Padding(4, 3, 4, 3);
            btnCatalog.Name = "btnCatalog";
            btnCatalog.Size = new Size(373, 58);
            btnCatalog.TabIndex = 1;
            btnCatalog.Text = "Каталог";
            btnCatalog.UseVisualStyleBackColor = true;
            btnCatalog.Click += btnCatalog_Click;
            // 
            // btnOrders
            // 
            btnOrders.Font = new Font("Segoe UI", 10F);
            btnOrders.Location = new Point(23, 104);
            btnOrders.Margin = new Padding(4, 3, 4, 3);
            btnOrders.Name = "btnOrders";
            btnOrders.Size = new Size(373, 58);
            btnOrders.TabIndex = 2;
            btnOrders.Text = "Заказы";
            btnOrders.UseVisualStyleBackColor = true;
            btnOrders.Click += btnOrders_Click;
            // 
            // btnUsers
            // 
            btnUsers.Font = new Font("Segoe UI", 10F);
            btnUsers.Location = new Point(23, 173);
            btnUsers.Margin = new Padding(4, 3, 4, 3);
            btnUsers.Name = "btnUsers";
            btnUsers.Size = new Size(373, 58);
            btnUsers.TabIndex = 3;
            btnUsers.Text = "Пользователи";
            btnUsers.UseVisualStyleBackColor = true;
            // 
            // btnEmployees
            // 
            btnEmployees.Font = new Font("Segoe UI", 10F);
            btnEmployees.Location = new Point(23, 242);
            btnEmployees.Margin = new Padding(4, 3, 4, 3);
            btnEmployees.Name = "btnEmployees";
            btnEmployees.Size = new Size(373, 58);
            btnEmployees.TabIndex = 4;
            btnEmployees.Text = "Сотрудники";
            btnEmployees.UseVisualStyleBackColor = true;
            // 
            // grpReports
            // 
            grpReports.Controls.Add(btnReport1);
            grpReports.Controls.Add(btnReport2);
            grpReports.Controls.Add(btnReport3);
            grpReports.Font = new Font("Segoe UI", 9F);
            grpReports.Location = new Point(467, 23);
            grpReports.Margin = new Padding(4, 3, 4, 3);
            grpReports.Name = "grpReports";
            grpReports.Padding = new Padding(4, 3, 4, 3);
            grpReports.Size = new Size(420, 323);
            grpReports.TabIndex = 1;
            grpReports.TabStop = false;
            grpReports.Text = "Отчеты";
            // 
            // btnReport1
            // 
            btnReport1.Font = new Font("Segoe UI", 10F);
            btnReport1.Location = new Point(23, 35);
            btnReport1.Margin = new Padding(4, 3, 4, 3);
            btnReport1.Name = "btnReport1";
            btnReport1.Size = new Size(373, 58);
            btnReport1.TabIndex = 5;
            btnReport1.Text = "Отчет 1";
            btnReport1.UseVisualStyleBackColor = true;
            // 
            // btnReport2
            // 
            btnReport2.Font = new Font("Segoe UI", 10F);
            btnReport2.Location = new Point(23, 104);
            btnReport2.Margin = new Padding(4, 3, 4, 3);
            btnReport2.Name = "btnReport2";
            btnReport2.Size = new Size(373, 58);
            btnReport2.TabIndex = 6;
            btnReport2.Text = "Отчет 2";
            btnReport2.UseVisualStyleBackColor = true;
            // 
            // btnReport3
            // 
            btnReport3.Font = new Font("Segoe UI", 10F);
            btnReport3.Location = new Point(23, 173);
            btnReport3.Margin = new Padding(4, 3, 4, 3);
            btnReport3.Name = "btnReport3";
            btnReport3.Size = new Size(373, 58);
            btnReport3.TabIndex = 7;
            btnReport3.Text = "Отчет 3";
            btnReport3.UseVisualStyleBackColor = true;
            // 
            // btnExit
            // 
            btnExit.Font = new Font("Segoe UI", 10F);
            btnExit.Location = new Point(23, 369);
            btnExit.Margin = new Padding(4, 3, 4, 3);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(863, 58);
            btnExit.TabIndex = 8;
            btnExit.Text = "Выход";
            btnExit.UseVisualStyleBackColor = true;
            btnExit.Click += btnExit_Click;
            // 
            // frmEmployee
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(915, 532);
            Controls.Add(pnlMenu);
            Controls.Add(pnlTop);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            Name = "frmEmployee";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Панель сотрудника";
            pnlTop.ResumeLayout(false);
            pnlTop.PerformLayout();
            pnlMenu.ResumeLayout(false);
            grpManagement.ResumeLayout(false);
            grpReports.ResumeLayout(false);
            ResumeLayout(false);
        }

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.Panel pnlMenu;
        private System.Windows.Forms.GroupBox grpManagement;
        private System.Windows.Forms.GroupBox grpReports;
        private System.Windows.Forms.Button btnCatalog;
        private System.Windows.Forms.Button btnOrders;
        private System.Windows.Forms.Button btnUsers;
        private System.Windows.Forms.Button btnEmployees;
        private System.Windows.Forms.Button btnReport1;
        private System.Windows.Forms.Button btnReport2;
        private System.Windows.Forms.Button btnReport3;
        private System.Windows.Forms.Button btnExit;
        private Button btnGuide;
    }
}