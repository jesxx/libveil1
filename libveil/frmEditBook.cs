using System;
using System.Data;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Globalization;

namespace libveil
{
    public partial class frmEditBook : Form
    {
        private readonly int bookId;
        private Dictionary<string, int> publishersDictionary;

        public frmEditBook(int bookId)
        {
            this.bookId = bookId;
            InitializeComponent();
            SetupControls();
            LoadPublishers();
            LoadBookData();
        }

        private void SetupControls()
        {
            nudPublishYear.Minimum = 1800;
            nudPublishYear.Maximum = DateTime.Now.Year;
            nudPublishYear.Value = DateTime.Now.Year;

            nudVolumes.Minimum = 1;
            nudVolumes.Maximum = 100;
            nudVolumes.Value = 1;

            nudQuantity.Minimum = 0;
            nudQuantity.Maximum = 1000;
            nudQuantity.Value = 0;

            nudPrice.Minimum = 0;
            nudPrice.Maximum = 100000;
            nudPrice.DecimalPlaces = 2;
            nudPrice.Value = 0;
        }

        private void LoadPublishers()
        {
            try
            {
                publishersDictionary = new Dictionary<string, int>();
                string query = "SELECT PublisherID, Name FROM Publishers ORDER BY Name";
                DataTable dt = DBConnection.Instance.ExecuteQuery(query);

                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        string publisherName = row["Name"].ToString();
                        int publisherId = Convert.ToInt32(row["PublisherID"]);
                        publishersDictionary.Add(publisherName, publisherId);
                        cmbPublisher.Items.Add(publisherName);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки издательств: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadBookData()
        {
            try
            {
                string query = @"SELECT b.*, p.Name as PublisherName 
                               FROM Books b 
                               JOIN Publishers p ON b.PublisherID = p.PublisherID 
                               WHERE b.BookID = " + bookId;

                DataTable dt = DBConnection.Instance.ExecuteQuery(query);

                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    txtAuthor.Text = row["Author"].ToString();
                    txtTitle.Text = row["Title"].ToString();
                    txtGenre.Text = row["Genre"].ToString();
                    cmbPublisher.SelectedItem = row["PublisherName"].ToString();

                    if (decimal.TryParse(row["PublishYear"].ToString(), out decimal publishYear))
                        nudPublishYear.Value = Math.Min(Math.Max(publishYear, nudPublishYear.Minimum), nudPublishYear.Maximum);

                    if (decimal.TryParse(row["Volumes"].ToString(), out decimal volumes))
                        nudVolumes.Value = Math.Min(Math.Max(volumes, nudVolumes.Minimum), nudVolumes.Maximum);

                    if (decimal.TryParse(row["Price"].ToString(), out decimal price))
                        nudPrice.Value = Math.Min(Math.Max(price, nudPrice.Minimum), nudPrice.Maximum);

                    if (decimal.TryParse(row["Quantity"].ToString(), out decimal quantity))
                        nudQuantity.Value = Math.Min(Math.Max(quantity, nudQuantity.Minimum), nudQuantity.Maximum);
                }
                else
                {
                    MessageBox.Show("Книга не найдена.", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных книги: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateInputs())
                    return;

                if (cmbPublisher.SelectedItem == null)
                {
                    MessageBox.Show("Выберите издательство.",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string selectedPublisher = cmbPublisher.SelectedItem.ToString();
                int publisherId = publishersDictionary[selectedPublisher];

                string query = $@"UPDATE Books SET 
                               Author = '{txtAuthor.Text.Trim()}', 
                               Title = '{txtTitle.Text.Trim()}', 
                               Genre = '{txtGenre.Text.Trim()}', 
                               PublisherID = {publisherId}, 
                               PublishYear = {(int)nudPublishYear.Value}, 
                               Volumes = {(int)nudVolumes.Value}, 
                               Price = {nudPrice.Value.ToString(CultureInfo.InvariantCulture)}, 
                               Quantity = {(int)nudQuantity.Value} 
                               WHERE BookID = {bookId}";

                int result = DBConnection.Instance.ExecuteNonQuery(query);
                if (result > 0)
                {
                    MessageBox.Show("Книга успешно обновлена!", "Успех",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении книги: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtAuthor.Text) ||
                string.IsNullOrWhiteSpace(txtTitle.Text) ||
                string.IsNullOrWhiteSpace(txtGenre.Text))
            {
                MessageBox.Show("Все поля должны быть заполнены.",
                    "Ошибка валидации", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (txtAuthor.Text.Length > 100 || txtTitle.Text.Length > 200 ||
                txtGenre.Text.Length > 50)
            {
                MessageBox.Show("Превышена максимальная длина одного из полей.",
                    "Ошибка валидации", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (nudPrice.Value <= 0)
            {
                MessageBox.Show("Цена должна быть больше нуля.",
                    "Ошибка валидации", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }
    }
}