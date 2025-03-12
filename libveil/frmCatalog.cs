using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;

namespace libveil
{
    public partial class frmCatalog : Form
    {
        private DataTable booksTable;

        public frmCatalog()
        {
            InitializeComponent();
            InitializeControls();
            LoadBooks();
        }

        private void InitializeControls()
        {
            try
            {
                // Initialize ComboBoxes
                string[] filterFields = { "Author", "Title", "Genre", "Publisher" };
                cmbFilterField.Items.AddRange(filterFields);
                cmbSearchField.Items.AddRange(filterFields);
                cmbFilterField.SelectedIndex = 0;
                cmbSearchField.SelectedIndex = 0;

                // Set up DataGridView
                dgvBooks.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvBooks.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvBooks.MultiSelect = true;
                dgvBooks.ReadOnly = true;
                dgvBooks.AutoGenerateColumns = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка инициализации контролов: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadBooks()
        {
            try
            {
                string query = @"SELECT b.BookID, b.Author, b.Title, b.Genre, 
                               p.Name AS Publisher, b.PublishYear, 
                               b.Volumes, b.Price, b.Quantity 
                        FROM Books b
                        JOIN Publishers p ON b.PublisherID = p.PublisherID
                        ORDER BY b.BookID";
                booksTable = DBConnection.Instance.ExecuteQuery(query);

                if (booksTable != null)
                {
                    dgvBooks.DataSource = booksTable;
                    FormatDataGridView();
                }
                else
                {
                    MessageBox.Show("Не удалось загрузить данные из базы данных.",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            if (dgvBooks.Columns.Count > 0)
            {
                // Настройка отображения столбцов
                if (dgvBooks.Columns.Contains("BookID"))
                    dgvBooks.Columns["BookID"].HeaderText = "ID";
                if (dgvBooks.Columns.Contains("Author"))
                    dgvBooks.Columns["Author"].HeaderText = "Автор";
                if (dgvBooks.Columns.Contains("Title"))
                    dgvBooks.Columns["Title"].HeaderText = "Название";
                if (dgvBooks.Columns.Contains("Genre"))
                    dgvBooks.Columns["Genre"].HeaderText = "Жанр";
                if (dgvBooks.Columns.Contains("Publisher"))
                    dgvBooks.Columns["Publisher"].HeaderText = "Издательство";
                if (dgvBooks.Columns.Contains("PublishYear"))
                    dgvBooks.Columns["PublishYear"].HeaderText = "Год издания";
                if (dgvBooks.Columns.Contains("Volumes"))
                    dgvBooks.Columns["Volumes"].HeaderText = "Томов";
                if (dgvBooks.Columns.Contains("Price"))
                    dgvBooks.Columns["Price"].HeaderText = "Цена";
                if (dgvBooks.Columns.Contains("Quantity"))
                    dgvBooks.Columns["Quantity"].HeaderText = "Количество";

                // Настройка ширины столбцов
                foreach (DataGridViewColumn col in dgvBooks.Columns)
                {
                    col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                using (frmAddBook addForm = new frmAddBook())
                {
                    if (addForm.ShowDialog() == DialogResult.OK)
                    {
                        LoadBooks();
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
                if (dgvBooks.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Выберите запись для редактирования.",
                        "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var row = dgvBooks.SelectedRows[0];
                if (row.Cells["BookID"].Value == null)
                {
                    MessageBox.Show("Не удалось получить данные книги.",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int bookId = Convert.ToInt32(row.Cells["BookID"].Value);
                using (frmEditBook editForm = new frmEditBook(bookId))
                {
                    if (editForm.ShowDialog() == DialogResult.OK)
                    {
                        LoadBooks();
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
                if (dgvBooks.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Выберите записи для удаления.",
                        "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var selectedIds = dgvBooks.SelectedRows
                    .Cast<DataGridViewRow>()
                    .Select(r => r.Cells["BookID"].Value?.ToString())
                    .Where(id => !string.IsNullOrEmpty(id))
                    .ToArray();

                if (selectedIds.Length == 0)
                {
                    MessageBox.Show("Не выбраны записи для удаления.",
                        "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int count = selectedIds.Length;
                DialogResult dr = MessageBox.Show(
                    $"Вы уверены, что хотите удалить {count} {(count == 1 ? "запись" : "записей")}?",
                    "Подтверждение удаления",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (dr == DialogResult.Yes)
                {
                    string idList = string.Join(",", selectedIds);
                    string query = $"DELETE FROM Books WHERE BookID IN ({idList})";

                    int result = DBConnection.Instance.ExecuteNonQuery(query);
                    if (result > 0)
                    {
                        MessageBox.Show($"Успешно удалено {result} {(result == 1 ? "запись" : "записей")}!",
                            "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadBooks();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка удаления записей: {ex.Message}",
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

                if (booksTable == null)
                {
                    MessageBox.Show("Нет данных для фильтрации.",
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
                DataView dv = booksTable.DefaultView;
                dv.RowFilter = $"[{filterField}] LIKE '%{filterValue}%'";
                dgvBooks.DataSource = dv.ToTable();
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
                LoadBooks();
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

                foreach (DataGridViewRow row in dgvBooks.Rows)
                {
                    if (row.Cells[searchField].Value != null)
                    {
                        string cellValue = row.Cells[searchField].Value.ToString();
                        if (cellValue.IndexOf(searchValue, StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            dgvBooks.ClearSelection();
                            row.Selected = true;
                            dgvBooks.FirstDisplayedScrollingRowIndex = row.Index;
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