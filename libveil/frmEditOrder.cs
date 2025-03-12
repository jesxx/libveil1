using System;
using System.Data;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Globalization;

namespace libveil
{
    public partial class frmEditOrder : Form
    {
        private readonly int orderId;
        private Dictionary<string, int> booksDictionary;
        private Dictionary<string, int> usersDictionary;
        private int originalQuantity;
        private int originalBookId;

        public frmEditOrder(int orderId)
        {
            this.orderId = orderId;
            InitializeComponent();
            SetupControls();
            LoadBooks();
            LoadUsers();
            LoadOrderData();
        }

        private void SetupControls()
        {
            dtpOrderDate.Format = DateTimePickerFormat.Short;

            cmbAcquisitionMethod.Items.AddRange(new string[] { "Самовывоз", "Доставка" });

            nudQuantity.Minimum = 1;
            nudQuantity.Maximum = 100;

            cmbStatus.Items.AddRange(new string[] { "Обработка", "Ожидание", "Выдано", "Отменено" });
        }

        private void LoadBooks()
        {
            try
            {
                booksDictionary = new Dictionary<string, int>();
                string query = @"SELECT BookID, Title + ' (' + Author + ')' AS BookInfo 
                               FROM Books ORDER BY Title";
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
                               FROM Users ORDER BY LastName, FirstName";
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
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки пользователей: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadOrderData()
        {
            try
            {
                string query = @"SELECT o.*, b.Title + ' (' + b.Author + ')' AS BookInfo,
                                      u.LastName + ' ' + u.FirstName + ' ' + ISNULL(u.MiddleName, '') AS UserFullName
                               FROM Orders o
                               JOIN Books b ON o.BookID = b.BookID
                               JOIN Users u ON o.UserID = u.UserID
                               WHERE o.OrderID = " + orderId;

                DataTable dt = DBConnection.Instance.ExecuteQuery(query);

                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];

                    // Сохраняем оригинальные значения
                    originalBookId = Convert.ToInt32(row["BookID"]);
                    originalQuantity = Convert.ToInt32(row["Quantity"]);

                    // Заполняем контролы
                    cmbBook.SelectedItem = row["BookInfo"].ToString();
                    cmbUser.SelectedItem = row["UserFullName"].ToString().Trim();
                    nudQuantity.Value = originalQuantity;
                    txtAmount.Text = row["Amount"].ToString();
                    cmbAcquisitionMethod.Text = row["AcquisitionMethod"].ToString();
                    txtReaderFullName.Text = row["ReaderFullName"].ToString();
                    dtpOrderDate.Value = Convert.ToDateTime(row["OrderDate"]);
                    cmbStatus.Text = row["Status"].ToString();

                    // Если заказ выдан или отменен, блокируем редактирование
                    if (cmbStatus.Text == "Выдано" || cmbStatus.Text == "Отменено")
                    {
                        DisableControls();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных заказа: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void DisableControls()
        {
            cmbBook.Enabled = false;
            cmbUser.Enabled = false;
            nudQuantity.Enabled = false;
            cmbAcquisitionMethod.Enabled = false;
            txtReaderFullName.Enabled = false;
            dtpOrderDate.Enabled = false;
            cmbStatus.Enabled = false;
            btnSave.Enabled = false;
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

                    // Для текущей книги добавляем оригинальное количество
                    if (bookId == originalBookId)
                        availableQuantity += originalQuantity;

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

                string query = "SELECT Price, Quantity FROM Books WHERE BookID = " + bookId;
                DataTable dt = DBConnection.Instance.ExecuteQuery(query);

                if (dt != null && dt.Rows.Count > 0)
                {
                    decimal price = Convert.ToDecimal(dt.Rows[0]["Price"]);
                    int availableQuantity = Convert.ToInt32(dt.Rows[0]["Quantity"]);

                    // Если это та же книга, добавляем к доступному количеству исходное количество заказа
                    if (bookId == originalBookId)
                    {
                        availableQuantity += originalQuantity;
                    }

                    // Обновляем сумму
                    txtAmount.Text = (price * nudQuantity.Value).ToString("F2");

                    // Проверяем наличие книг
                    if (availableQuantity < nudQuantity.Value)
                    {
                        MessageBox.Show(
                            $"Внимание! Доступное количество книг ({availableQuantity} шт.) меньше выбранного ({nudQuantity.Value} шт.).\n" +
                            "При сохранении статус заказа будет изменен на 'Ожидание'.",
                            "Предупреждение",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                    }
                }
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

                // Проверяем наличие книг если изменилось количество или книга
                if (bookId != originalBookId || nudQuantity.Value != originalQuantity)
                {
                    string checkQuery = "SELECT Quantity FROM Books WHERE BookID = " + bookId;
                    DataTable dt = DBConnection.Instance.ExecuteQuery(checkQuery);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int availableQuantity = Convert.ToInt32(dt.Rows[0]["Quantity"]);

                        // Если это та же книга, добавляем к доступному количеству исходное количество заказа
                        if (bookId == originalBookId)
                        {
                            availableQuantity += originalQuantity;
                        }

                        // Проверяем, достаточно ли книг
                        if (availableQuantity < nudQuantity.Value)
                        {
                            // Если книг недостаточно, меняем статус на "Ожидание"
                            cmbStatus.Text = "Ожидание";
                            MessageBox.Show(
                                $"Доступное количество книг ({availableQuantity} шт.) меньше требуемого ({nudQuantity.Value} шт.).\n" +
                                "Статус заказа будет изменен на 'Ожидание'.",
                                "Предупреждение",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                        }
                    }
                }

                using (var transaction = new System.Transactions.TransactionScope())
                {
                    // Если книга или количество изменились, обновляем остатки
                    if (bookId != originalBookId || nudQuantity.Value != originalQuantity)
                    {
                        // Возвращаем оригинальное количество старой книге
                        if (bookId != originalBookId)
                        {
                            string updateOldBookQuery = $@"UPDATE Books 
                                                SET Quantity = Quantity + {originalQuantity}
                                                WHERE BookID = {originalBookId}";
                            DBConnection.Instance.ExecuteNonQuery(updateOldBookQuery);
                        }

                        // Уменьшаем количество новой книги только если статус не "Ожидание"
                        if (cmbStatus.Text != "Ожидание")
                        {
                            string updateNewBookQuery = $@"UPDATE Books 
                                                SET Quantity = Quantity - {(int)nudQuantity.Value}
                                                WHERE BookID = {bookId}";
                            DBConnection.Instance.ExecuteNonQuery(updateNewBookQuery);
                        }
                    }

                    // Обновляем заказ
                    string updateOrderQuery = $@"UPDATE Orders SET 
                                       BookID = {bookId},
                                       UserID = {userId},
                                       Amount = {decimal.Parse(txtAmount.Text, CultureInfo.InvariantCulture)},
                                       Quantity = {(int)nudQuantity.Value},
                                       AcquisitionMethod = '{cmbAcquisitionMethod.Text}',
                                       ReaderFullName = '{txtReaderFullName.Text.Replace("'", "''")}',
                                       OrderDate = '{dtpOrderDate.Value.ToString("yyyy-MM-dd")}',
                                       Status = '{cmbStatus.Text}'
                                       WHERE OrderID = {orderId}";

                    int result = DBConnection.Instance.ExecuteNonQuery(updateOrderQuery);

                    if (result > 0)
                    {
                        transaction.Complete();
                        MessageBox.Show("Заказ успешно обновлен!", "Успех",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении заказа: {ex.Message}",
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

            if (string.IsNullOrWhiteSpace(cmbStatus.Text))
            {
                MessageBox.Show("Выберите статус заказа.", "Ошибка валидации",
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