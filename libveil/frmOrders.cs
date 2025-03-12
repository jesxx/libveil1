using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;

namespace libveil
{
    public partial class frmOrders : Form
    {
        private DataTable ordersTable;

        public frmOrders()
        {
            InitializeComponent();
            InitializeControls();
            LoadOrders();
        }

        private void InitializeControls()
        {
            try
            {
                // Initialize ComboBoxes
                string[] filterFields = { "ReaderFullName", "Status", "OrderDate", "BookTitle" };
                cmbFilterField.Items.AddRange(filterFields);
                cmbSearchField.Items.AddRange(filterFields);
                cmbFilterField.SelectedIndex = 0;
                cmbSearchField.SelectedIndex = 0;

                // Set up DataGridView
                dgvOrders.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvOrders.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvOrders.MultiSelect = false;
                dgvOrders.ReadOnly = true;
                dgvOrders.AutoGenerateColumns = false;

                SetupDataGridViewColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка инициализации контролов: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupDataGridViewColumns()
        {
            dgvOrders.Columns.Clear();

            dgvOrders.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "OrderID",
                DataPropertyName = "OrderID",
                HeaderText = "ID заказа",
                Width = 70
            });

            dgvOrders.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ReaderFullName",
                DataPropertyName = "ReaderFullName",
                HeaderText = "ФИО читателя",
                Width = 200
            });

            dgvOrders.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "BookTitle",
                DataPropertyName = "BookTitle",
                HeaderText = "Название книги",
                Width = 200
            });

            dgvOrders.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "OrderDate",
                DataPropertyName = "OrderDate",
                HeaderText = "Дата заказа",
                Width = 100
            });

            dgvOrders.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Quantity",
                DataPropertyName = "Quantity",
                HeaderText = "Количество",
                Width = 80
            });

            dgvOrders.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Amount",
                DataPropertyName = "Amount",
                HeaderText = "Сумма",
                Width = 100
            });

            dgvOrders.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Status",
                DataPropertyName = "Status",
                HeaderText = "Статус",
                Width = 100
            });

            dgvOrders.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "AcquisitionMethod",
                DataPropertyName = "AcquisitionMethod",
                HeaderText = "Способ получения",
                Width = 120
            });
        }

        private void LoadOrders()
        {
            try
            {
                string query = @"SELECT o.OrderID, o.ReaderFullName, b.Title as BookTitle, 
                                      o.OrderDate, o.Quantity, o.Amount, o.Status,
                                      o.AcquisitionMethod, o.BookID, o.UserID
                               FROM Orders o
                               JOIN Books b ON o.BookID = b.BookID
                               ORDER BY o.OrderDate DESC";

                ordersTable = DBConnection.Instance.ExecuteQuery(query);

                if (ordersTable != null)
                {
                    dgvOrders.DataSource = ordersTable;
                    FormatDataGridView();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatDataGridView()
        {
            foreach (DataGridViewRow row in dgvOrders.Rows)
            {
                string status = row.Cells["Status"].Value?.ToString();
                if (status == "Отменено")
                {
                    row.DefaultCellStyle.ForeColor = Color.Red;
                }
                else if (status == "Выдано")
                {
                    row.DefaultCellStyle.ForeColor = Color.Green;
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                using (frmAddOrder addForm = new frmAddOrder())
                {
                    if (addForm.ShowDialog() == DialogResult.OK)
                    {
                        LoadOrders();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при открытии формы добавления: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvOrders.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Выберите запись для редактирования.",
                        "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var row = dgvOrders.SelectedRows[0];
                if (row.Cells["OrderID"].Value == null)
                {
                    MessageBox.Show("Не удалось получить данные заказа.",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int orderId = Convert.ToInt32(row.Cells["OrderID"].Value);
                string currentStatus = row.Cells["Status"].Value.ToString();

                if (currentStatus == "Отменено")
                {
                    MessageBox.Show("Невозможно редактировать отмененный заказ.",
                        "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (frmEditOrder editForm = new frmEditOrder(orderId))
                {
                    if (editForm.ShowDialog() == DialogResult.OK)
                    {
                        LoadOrders();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при открытии формы редактирования: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvOrders.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Выберите запись для удаления.",
                        "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var row = dgvOrders.SelectedRows[0];
                if (row.Cells["Status"].Value.ToString() == "Выдано")
                {
                    MessageBox.Show("Невозможно удалить выданный заказ.",
                        "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int orderId = Convert.ToInt32(row.Cells["OrderID"].Value);
                int bookId = Convert.ToInt32(row.Cells["BookID"].Value);
                int quantity = Convert.ToInt32(row.Cells["Quantity"].Value);

                DialogResult dr = MessageBox.Show(
                    "Вы уверены, что хотите удалить этот заказ?",
                    "Подтверждение удаления",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (dr == DialogResult.Yes)
                {
                    using (var transaction = new System.Transactions.TransactionScope())
                    {
                        // Возвращаем книги в наличие
                        string updateBookQuery = $@"UPDATE Books 
                                                 SET Quantity = Quantity + {quantity}
                                                 WHERE BookID = {bookId}";
                        DBConnection.Instance.ExecuteNonQuery(updateBookQuery);

                        // Удаляем заказ
                        string deleteQuery = $"DELETE FROM Orders WHERE OrderID = {orderId}";
                        int result = DBConnection.Instance.ExecuteNonQuery(deleteQuery);

                        if (result > 0)
                        {
                            transaction.Complete();
                            MessageBox.Show("Заказ успешно удален!",
                                "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadOrders();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка удаления заказа: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtFilterValue.Text))
                {
                    MessageBox.Show("Введите значение для фильтрации.",
                        "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string filterField = cmbFilterField.SelectedItem?.ToString();
                if (string.IsNullOrEmpty(filterField))
                {
                    MessageBox.Show("Выберите поле для фильтрации.",
                        "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string filterValue = txtFilterValue.Text.Trim();
                DataView dv = ordersTable.DefaultView;
                dv.RowFilter = $"[{filterField}] LIKE '%{filterValue}%'";
                dgvOrders.DataSource = dv.ToTable();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка фильтрации: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnResetFilter_Click(object sender, EventArgs e)
        {
            try
            {
                LoadOrders();
                txtFilterValue.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сброса фильтра: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtSearchValue.Text))
                {
                    MessageBox.Show("Введите значение для поиска.",
                        "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string searchField = cmbSearchField.SelectedItem?.ToString();
                if (string.IsNullOrEmpty(searchField))
                {
                    MessageBox.Show("Выберите поле для поиска.",
                        "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string searchValue = txtSearchValue.Text.Trim();
                bool found = false;

                foreach (DataGridViewRow row in dgvOrders.Rows)
                {
                    if (row.Cells[searchField].Value != null)
                    {
                        string cellValue = row.Cells[searchField].Value.ToString();
                        if (cellValue.IndexOf(searchValue, StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            dgvOrders.ClearSelection();
                            row.Selected = true;
                            dgvOrders.FirstDisplayedScrollingRowIndex = row.Index;
                            found = true;
                            break;
                        }
                    }
                }

                if (!found)
                {
                    MessageBox.Show("Поиск не дал результатов.", "Информация",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка поиска: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}