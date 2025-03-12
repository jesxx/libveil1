using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace libveil
{
    public partial class frmReg : Form
    {
        public frmReg()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            string lastName = txtLastName.Text.Trim();
            string firstName = txtFirstName.Text.Trim();
            string middleName = txtMiddleName.Text.Trim();
            string login = txtLogin.Text.Trim();
            string password = txtPassword.Text.Trim();
            DateTime birthDate = dtpBirthDate.Value;

            if (!ValidateInput(lastName, firstName, login, password, birthDate))
                return;

            string query = "INSERT INTO Users (LastName, FirstName, MiddleName, Login, Password, BirthDate) " +
                           "VALUES (@LastName, @FirstName, @MiddleName, @Login, @Password, @BirthDate)";

            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@LastName", lastName },
                { "@FirstName", firstName },
                { "@MiddleName", middleName },
                { "@Login", login },
                { "@Password", password },
                { "@BirthDate", birthDate }
            };

            int result = DBConnection.Instance.ExecuteNonQuery(query, parameters);

            if (result > 0)
            {
                MessageBox.Show("Регистрация успешна!", "Регистрация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Ошибка регистрации. Попробуйте снова.", "Ошибка регистрации",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateInput(string lastName, string firstName, string login, string password, DateTime birthDate)
        {
            if (string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(firstName))
            {
                MessageBox.Show("Пожалуйста, заполните все обязательные поля (Имя и Фамилия).", "Ошибка регистрации",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!Regex.IsMatch(login, @"^[a-zA-Z0-9]+$"))
            {
                MessageBox.Show("Логин может содержать только английские буквы и цифры.", "Ошибка регистрации",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (password.Length < 6 || !Regex.IsMatch(password, @"[a-z]") || !Regex.IsMatch(password, @"[A-Z]") || !Regex.IsMatch(password, @"[0-9]"))
            {
                MessageBox.Show("Пароль должен быть не менее 6 символов и содержать буквы верхнего и нижнего регистра, а также цифры.", "Ошибка регистрации",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (birthDate >= DateTime.Now)
            {
                MessageBox.Show("Дата рождения должна быть в прошлом.", "Ошибка регистрации",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }
    }
}