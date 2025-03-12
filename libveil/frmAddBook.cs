using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Globalization;

namespace libveil
{
    public partial class frmAddBook : Form
    {
        private Dictionary<string, int> publishersDictionary;

        public frmAddBook()
        {
            InitializeComponent();
            SetupControls();
            LoadPublishers();
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

                    if (cmbPublisher.Items.Count > 0)
                        cmbPublisher.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки издательств: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

                string query = $@"INSERT INTO Books (Author, Title, Genre, PublisherID, PublishYear, Volumes, Price, Quantity) 
                               VALUES ('{txtAuthor.Text.Trim()}', 
                                       '{txtTitle.Text.Trim()}', 
                                       '{txtGenre.Text.Trim()}', 
                                       {publisherId}, 
                                       {(int)nudPublishYear.Value}, 
                                       {(int)nudVolumes.Value}, 
                                       {nudPrice.Value.ToString(CultureInfo.InvariantCulture)}, 
                                       {(int)nudQuantity.Value})";

                int result = DBConnection.Instance.ExecuteNonQuery(query);
                if (result > 0)
                {
                    MessageBox.Show("Книга успешно добавлена!", "Успех",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении книги: {ex.Message}",
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