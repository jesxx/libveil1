using System;
using System.Data;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Globalization;

namespace libveil
{
    public partial class frmAddOrder : Form
    {
        private Dictionary<string, int> booksDictionary;
        private Dictionary<string, int> usersDictionary;

        public frmAddOrder()
        {
            InitializeComponent();
            SetupControls();
            LoadBooks();
            LoadUsers();
        }

        private void SetupControls()
        {
            // Настройка DateTimePicker
            dtpOrderDate.Format = DateTimePickerFormat.Short;
            dtpOrderDate.Value = DateTime.Now;

            // Настройка ComboBox для способа получения
            cmbAcquisitionMethod.Items.AddRange(new string[] { "Самовывоз", "Доставка" });
            cmbAcquisitionMethod.SelectedIndex = 0;

            // Настройка NumericUpDown
            nudQuantity.Minimum = 1;
            nudQuantity.Maximum = 100;
            nudQuantity.Value = 1;

            // Статус по умолчанию будет определяться при сохранении
        }

        private void LoadBooks()
        {
            try
            {
                booksDictionary = new Dictionary<string, int>();
                string query = @"SELECT BookID, Title + ' (' + Author + ')' AS BookInfo 
                               FROM Books 
                               WHERE Quantity > 0 
                               ORDER BY Title";
                DataTable dt = DBConnection.Instance.ExecuteQuery(query);

                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        string bookInfo = row["BookInfo"].ToString();
                        int bookId = Convert.ToInt32(row["BookID"]);
                        booksDictionary.Add(bookInfo, bookId);
                        cmbBook.Items.Add(bookInfo);
                    }

                    if (cmbBook.Items.Count > 0)
                        cmbBook.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки книг: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadUsers()
        {
            try
            {
                usersDictionary = new Dictionary<string, int>();
                string query = @"SELECT UserID, LastName + ' ' + FirstName + ' ' + ISNULL(MiddleName, '') AS FullName 
                               FROM Users 
                               ORDER BY LastName, FirstName";
                DataTable dt = DBConnection.Instance.ExecuteQuery(query);

                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        string fullName = row["FullName"].ToString().Trim();
                        int userId = Convert.ToInt32(row["UserID"]);
                        usersDictionary.Add(fullName, userId);
                        cmbUser.Items.Add(fullName);
                    }

                    if (cmbUser.Items.Count > 0)
                        cmbUser.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки пользователей: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbBook_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbBook.SelectedItem != null)
            {
                string selectedBook = cmbBook.SelectedItem.ToString();
                int bookId = booksDictionary[selectedBook];

                string query = "SELECT Price, Quantity FROM Books WHERE BookID = " + bookId;
                DataTable dt = DBConnection.Instance.ExecuteQuery(query);

                if (dt != null && dt.Rows.Count > 0)
                {
                    decimal price = Convert.ToDecimal(dt.Rows[0]["Price"]);
                    int availableQuantity = Convert.ToInt32(dt.Rows[0]["Quantity"]);

                    nudQuantity.Maximum = availableQuantity;
                    txtAmount.Text = (price * nudQuantity.Value).ToString("F2");
                }
            }
        }

        private void nudQuantity_ValueChanged(object sender, EventArgs e)
        {
            if (cmbBook.SelectedItem != null)
            {
                string selectedBook = cmbBook.SelectedItem.ToString();
                int bookId = booksDictionary[selectedBook];

                string query = "SELECT Price FROM Books WHERE BookID = " + bookId;
                DataTable dt = DBConnection.Instance.ExecuteQuery(query);

                if (dt != null && dt.Rows.Count > 0)
                {
                    decimal price = Convert.ToDecimal(dt.Rows[0]["Price"]);
                    txtAmount.Text = (price * nudQuantity.Value).ToString("F2");
                }
            }
        }

        private void cmbUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbUser.SelectedItem != null)
            {
                txtReaderFullName.Text = cmbUser.SelectedItem.ToString();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateInputs())
                    return;

                string selectedBook = cmbBook.SelectedItem.ToString();
                string selectedUser = cmbUser.SelectedItem.ToString();
                int bookId = booksDictionary[selectedBook];
                int userId = usersDictionary[selectedUser];

                // Проверяем наличие книг
                string checkQuery = "SELECT Quantity FROM Books WHERE BookID = " + bookId;
                DataTable dt = DBConnection.Instance.ExecuteQuery(checkQuery);

                if (dt != null && dt.Rows.Count > 0)
                {
                    int availableQuantity = Convert.ToInt32(dt.Rows[0]["Quantity"]);
                    string status = availableQuantity >= nudQuantity.Value ? "Обработка" : "Ожидание";

                    using (var transaction = new System.Transactions.TransactionScope())
                    {
                        // Добавляем заказ
                        string insertQuery = $@"INSERT INTO Orders (
                                                BookID, UserID, Amount, Quantity, 
                                                AcquisitionMethod, ReaderFullName, 
                                                OrderDate, Status
                                            ) VALUES (
                                                {bookId}, {userId}, 
                                                {decimal.Parse(txtAmount.Text, CultureInfo.InvariantCulture)}, 
                                                {(int)nudQuantity.Value},
                                                '{cmbAcquisitionMethod.Text}', 
                                                '{txtReaderFullName.Text.Replace("'", "''")}',
                                                '{dtpOrderDate.Value.ToString("yyyy-MM-dd")}', 
                                                '{status}'
                                            )";

                        int result = DBConnection.Instance.ExecuteNonQuery(insertQuery);

                        if (result > 0 && status == "Обработка")
                        {
                            // Уменьшаем количество книг
                            string updateQuery = $@"UPDATE Books 
                                                  SET Quantity = Quantity - {(int)nudQuantity.Value}
                                                  WHERE BookID = {bookId}";
                            DBConnection.Instance.ExecuteNonQuery(updateQuery);
                        }

                        transaction.Complete();
                        MessageBox.Show("Заказ успешно добавлен!", "Успех",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении заказа: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateInputs()
        {
            if (cmbBook.SelectedItem == null)
            {
                MessageBox.Show("Выберите книгу.", "Ошибка валидации",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (cmbUser.SelectedItem == null)
            {
                MessageBox.Show("Выберите пользователя.", "Ошибка валидации",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtReaderFullName.Text))
            {
                MessageBox.Show("Введите ФИО читателя.", "Ошибка валидации",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (nudQuantity.Value < 1)
            {
                MessageBox.Show("Количество должно быть больше нуля.", "Ошибка валидации",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}