using System;
using System.Data;
using System.Windows.Forms;

namespace libveil
{
    public partial class frmAuth : Form
    {
        public frmAuth()
        {
            InitializeComponent();
        }

        private void btnUserLogin_Click(object sender, EventArgs e)
        {
            string login = txtUserLogin.Text.Trim();
            string password = txtUserPassword.Text.Trim();

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Пожалуйста, введите логин и пароль.", "Ошибка авторизации",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query = $"SELECT COUNT(*) FROM Users WHERE Login = '{login}' AND Password = '{password}'";
            object result = DBConnection.Instance.ExecuteScalar(query);

            if (result != null && int.Parse(result.ToString()) > 0)
            {
                MessageBox.Show("Успешная авторизация!", "Авторизация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль.", "Ошибка авторизации",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lblCreateAccount_Click(object sender, EventArgs e)
        {
            frmReg registrationForm = new frmReg();
            registrationForm.ShowDialog();
        }

        private void btnEmployeeLogin_Click(object sender, EventArgs e)
        {
            string login = txtEmployeeLogin.Text.Trim();
            string password = txtEmployeePassword.Text.Trim();

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Пожалуйста, введите логин и пароль.", "Ошибка авторизации",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query = $"SELECT AccessLevel FROM Employees WHERE Login = '{login}' AND Password = '{password}'";
            object result = DBConnection.Instance.ExecuteScalar(query);

            if (result != null)
            {
                string accessLevel = result.ToString();
                if (accessLevel == "Полный")
                {
                    MessageBox.Show("Успешная авторизация!", "Авторизация",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    
                    frmEmployee employeeForm = new frmEmployee(login);
                    this.Hide();
                    employeeForm.ShowDialog();
                    this.Show();
                }
                else if (accessLevel == "Ограничен")
                {
                    MessageBox.Show("У вас ограниченный доступ. Авторизация невозможна.", "Ошибка авторизации",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль.", "Ошибка авторизации",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lblCreateAccount_MouseEnter(object sender, EventArgs e)
        {
            lblCreateAccount.ForeColor = System.Drawing.Color.Blue;
            lblCreateAccount.Cursor = Cursors.Hand;
        }

        private void lblCreateAccount_MouseLeave(object sender, EventArgs e)
        {
            lblCreateAccount.ForeColor = System.Drawing.Color.Black;
            lblCreateAccount.Cursor = Cursors.Default;
        }
    }
}