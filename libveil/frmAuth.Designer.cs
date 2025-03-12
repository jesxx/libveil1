namespace libveil
{
    partial class frmAuth
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
            tabControl1 = new TabControl();
            tabPageUser = new TabPage();
            lblCreateAccount = new Label();
            btnUserLogin = new Button();
            txtUserPassword = new TextBox();
            txtUserLogin = new TextBox();
            lblUserPassword = new Label();
            lblUserLogin = new Label();
            tabPageEmployee = new TabPage();
            btnEmployeeLogin = new Button();
            txtEmployeePassword = new TextBox();
            txtEmployeeLogin = new TextBox();
            lblEmployeePassword = new Label();
            lblEmployeeLogin = new Label();
            tabControl1.SuspendLayout();
            tabPageUser.SuspendLayout();
            tabPageEmployee.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPageUser);
            tabControl1.Controls.Add(tabPageEmployee);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0, 0);
            tabControl1.Margin = new Padding(4, 3, 4, 3);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(426, 301);
            tabControl1.TabIndex = 0;
            // 
            // tabPageUser
            // 
            tabPageUser.Controls.Add(lblCreateAccount);
            tabPageUser.Controls.Add(btnUserLogin);
            tabPageUser.Controls.Add(txtUserPassword);
            tabPageUser.Controls.Add(txtUserLogin);
            tabPageUser.Controls.Add(lblUserPassword);
            tabPageUser.Controls.Add(lblUserLogin);
            tabPageUser.Location = new Point(4, 24);
            tabPageUser.Margin = new Padding(4, 3, 4, 3);
            tabPageUser.Name = "tabPageUser";
            tabPageUser.Padding = new Padding(4, 3, 4, 3);
            tabPageUser.Size = new Size(418, 273);
            tabPageUser.TabIndex = 0;
            tabPageUser.Text = "Я пользователь";
            tabPageUser.UseVisualStyleBackColor = true;
            // 
            // lblCreateAccount
            // 
            lblCreateAccount.AutoSize = true;
            lblCreateAccount.Location = new Point(126, 183);
            lblCreateAccount.Margin = new Padding(4, 0, 4, 0);
            lblCreateAccount.Name = "lblCreateAccount";
            lblCreateAccount.Size = new Size(182, 15);
            lblCreateAccount.TabIndex = 5;
            lblCreateAccount.Text = "Впервые у нас? Создать аккаунт";
            lblCreateAccount.Click += lblCreateAccount_Click;
            lblCreateAccount.MouseEnter += lblCreateAccount_MouseEnter;
            lblCreateAccount.MouseLeave += lblCreateAccount_MouseLeave;
            // 
            // btnUserLogin
            // 
            btnUserLogin.Location = new Point(146, 138);
            btnUserLogin.Margin = new Padding(4, 3, 4, 3);
            btnUserLogin.Name = "btnUserLogin";
            btnUserLogin.Size = new Size(134, 27);
            btnUserLogin.TabIndex = 4;
            btnUserLogin.Text = "Войти";
            btnUserLogin.UseVisualStyleBackColor = true;
            btnUserLogin.Click += btnUserLogin_Click;
            // 
            // txtUserPassword
            // 
            txtUserPassword.Location = new Point(146, 92);
            txtUserPassword.Margin = new Padding(4, 3, 4, 3);
            txtUserPassword.Name = "txtUserPassword";
            txtUserPassword.Size = new Size(134, 23);
            txtUserPassword.TabIndex = 3;
            txtUserPassword.UseSystemPasswordChar = true;
            // 
            // txtUserLogin
            // 
            txtUserLogin.Location = new Point(146, 46);
            txtUserLogin.Margin = new Padding(4, 3, 4, 3);
            txtUserLogin.Name = "txtUserLogin";
            txtUserLogin.Size = new Size(134, 23);
            txtUserLogin.TabIndex = 2;
            // 
            // lblUserPassword
            // 
            lblUserPassword.AutoSize = true;
            lblUserPassword.Location = new Point(58, 92);
            lblUserPassword.Margin = new Padding(4, 0, 4, 0);
            lblUserPassword.Name = "lblUserPassword";
            lblUserPassword.Size = new Size(52, 15);
            lblUserPassword.TabIndex = 1;
            lblUserPassword.Text = "Пароль:";
            // 
            // lblUserLogin
            // 
            lblUserLogin.AutoSize = true;
            lblUserLogin.Location = new Point(58, 46);
            lblUserLogin.Margin = new Padding(4, 0, 4, 0);
            lblUserLogin.Name = "lblUserLogin";
            lblUserLogin.Size = new Size(44, 15);
            lblUserLogin.TabIndex = 0;
            lblUserLogin.Text = "Логин:";
            // 
            // tabPageEmployee
            // 
            tabPageEmployee.Controls.Add(btnEmployeeLogin);
            tabPageEmployee.Controls.Add(txtEmployeePassword);
            tabPageEmployee.Controls.Add(txtEmployeeLogin);
            tabPageEmployee.Controls.Add(lblEmployeePassword);
            tabPageEmployee.Controls.Add(lblEmployeeLogin);
            tabPageEmployee.Location = new Point(4, 24);
            tabPageEmployee.Margin = new Padding(4, 3, 4, 3);
            tabPageEmployee.Name = "tabPageEmployee";
            tabPageEmployee.Padding = new Padding(4, 3, 4, 3);
            tabPageEmployee.Size = new Size(440, 273);
            tabPageEmployee.TabIndex = 1;
            tabPageEmployee.Text = "Я сотрудник";
            tabPageEmployee.UseVisualStyleBackColor = true;
            // 
            // btnEmployeeLogin
            // 
            btnEmployeeLogin.Location = new Point(146, 138);
            btnEmployeeLogin.Margin = new Padding(4, 3, 4, 3);
            btnEmployeeLogin.Name = "btnEmployeeLogin";
            btnEmployeeLogin.Size = new Size(134, 27);
            btnEmployeeLogin.TabIndex = 9;
            btnEmployeeLogin.Text = "Войти";
            btnEmployeeLogin.UseVisualStyleBackColor = true;
            btnEmployeeLogin.Click += btnEmployeeLogin_Click;
            // 
            // txtEmployeePassword
            // 
            txtEmployeePassword.Location = new Point(146, 92);
            txtEmployeePassword.Margin = new Padding(4, 3, 4, 3);
            txtEmployeePassword.Name = "txtEmployeePassword";
            txtEmployeePassword.Size = new Size(134, 23);
            txtEmployeePassword.TabIndex = 8;
            txtEmployeePassword.UseSystemPasswordChar = true;
            // 
            // txtEmployeeLogin
            // 
            txtEmployeeLogin.Location = new Point(146, 46);
            txtEmployeeLogin.Margin = new Padding(4, 3, 4, 3);
            txtEmployeeLogin.Name = "txtEmployeeLogin";
            txtEmployeeLogin.Size = new Size(134, 23);
            txtEmployeeLogin.TabIndex = 7;
            // 
            // lblEmployeePassword
            // 
            lblEmployeePassword.AutoSize = true;
            lblEmployeePassword.Location = new Point(58, 92);
            lblEmployeePassword.Margin = new Padding(4, 0, 4, 0);
            lblEmployeePassword.Name = "lblEmployeePassword";
            lblEmployeePassword.Size = new Size(52, 15);
            lblEmployeePassword.TabIndex = 6;
            lblEmployeePassword.Text = "Пароль:";
            // 
            // lblEmployeeLogin
            // 
            lblEmployeeLogin.AutoSize = true;
            lblEmployeeLogin.Location = new Point(58, 46);
            lblEmployeeLogin.Margin = new Padding(4, 0, 4, 0);
            lblEmployeeLogin.Name = "lblEmployeeLogin";
            lblEmployeeLogin.Size = new Size(44, 15);
            lblEmployeeLogin.TabIndex = 5;
            lblEmployeeLogin.Text = "Логин:";
            // 
            // frmAuth
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(426, 301);
            Controls.Add(tabControl1);
            Margin = new Padding(4, 3, 4, 3);
            Name = "frmAuth";
            Text = "Авторизация";
            tabControl1.ResumeLayout(false);
            tabPageUser.ResumeLayout(false);
            tabPageUser.PerformLayout();
            tabPageEmployee.ResumeLayout(false);
            tabPageEmployee.PerformLayout();
            ResumeLayout(false);

        }

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageUser;
        private System.Windows.Forms.TabPage tabPageEmployee;
        private System.Windows.Forms.Label lblCreateAccount;
        private System.Windows.Forms.Button btnUserLogin;
        private System.Windows.Forms.TextBox txtUserPassword;
        private System.Windows.Forms.TextBox txtUserLogin;
        private System.Windows.Forms.Label lblUserPassword;
        private System.Windows.Forms.Label lblUserLogin;
        private System.Windows.Forms.Button btnEmployeeLogin;
        private System.Windows.Forms.TextBox txtEmployeePassword;
        private System.Windows.Forms.TextBox txtEmployeeLogin;
        private System.Windows.Forms.Label lblEmployeePassword;
        private System.Windows.Forms.Label lblEmployeeLogin;
    }
}