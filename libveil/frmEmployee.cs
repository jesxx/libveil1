using System;
using System.Data;
using System.Windows.Forms;

namespace libveil
{
    public partial class frmEmployee : Form
    {
        private string employeeFullName;

        public frmEmployee(string login)
        {
            InitializeComponent();
            LoadEmployeeInfo(login);
        }

        private void LoadEmployeeInfo(string login)
        {
            string query = $"SELECT FirstName, LastName FROM Employees WHERE Login = '{login}'";
            DataTable dt = DBConnection.Instance.ExecuteQuery(query);

            if (dt.Rows.Count > 0)
            {
                string firstName = dt.Rows[0]["FirstName"].ToString();
                string lastName = dt.Rows[0]["LastName"].ToString();
                employeeFullName = $"{firstName} {lastName}";
                lblWelcome.Text = $"Добро пожаловать, {employeeFullName}!";
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCatalog_Click(object sender, EventArgs e)
        {
            frmCatalog catalogForm = new frmCatalog();
            catalogForm.ShowDialog();
        }

        private void btnOrders_Click(object sender, EventArgs e)
        {
            frmOrders ordersForm = new frmOrders();
            ordersForm.ShowDialog();
        }
    }
}