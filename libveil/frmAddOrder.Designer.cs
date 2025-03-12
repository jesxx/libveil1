namespace libveil
{
    partial class frmAddOrder
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
            this.lblBook = new System.Windows.Forms.Label();
            this.cmbBook = new System.Windows.Forms.ComboBox();
            this.lblUser = new System.Windows.Forms.Label();
            this.cmbUser = new System.Windows.Forms.ComboBox();
            this.lblQuantity = new System.Windows.Forms.Label();
            this.nudQuantity = new System.Windows.Forms.NumericUpDown();
            this.lblAmount = new System.Windows.Forms.Label();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.lblAcquisitionMethod = new System.Windows.Forms.Label();
            this.cmbAcquisitionMethod = new System.Windows.Forms.ComboBox();
            this.lblReaderFullName = new System.Windows.Forms.Label();
            this.txtReaderFullName = new System.Windows.Forms.TextBox();
            this.lblOrderDate = new System.Windows.Forms.Label();
            this.dtpOrderDate = new System.Windows.Forms.DateTimePicker();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();

            ((System.ComponentModel.ISupportInitialize)(this.nudQuantity)).BeginInit();
            this.SuspendLayout();

            // Form
            this.ClientSize = new System.Drawing.Size(464, 341);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Добавление заказа";

            int labelX = 20;
            int controlX = 150;
            int y = 20;
            int spacing = 35;
            int controlWidth = 280;

            // Book
            this.lblBook.AutoSize = true;
            this.lblBook.Location = new System.Drawing.Point(labelX, y);
            this.lblBook.Size = new System.Drawing.Size(100, 23);
            this.lblBook.Text = "Книга:";

            this.cmbBook.Location = new System.Drawing.Point(controlX, y);
            this.cmbBook.Size = new System.Drawing.Size(controlWidth, 23);
            this.cmbBook.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBook.SelectedIndexChanged += new System.EventHandler(this.cmbBook_SelectedIndexChanged);

            // User
            y += spacing;
            this.lblUser.AutoSize = true;
            this.lblUser.Location = new System.Drawing.Point(labelX, y);
            this.lblUser.Size = new System.Drawing.Size(100, 23);
            this.lblUser.Text = "Пользователь:";

            this.cmbUser.Location = new System.Drawing.Point(controlX, y);
            this.cmbUser.Size = new System.Drawing.Size(controlWidth, 23);
            this.cmbUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUser.SelectedIndexChanged += new System.EventHandler(this.cmbUser_SelectedIndexChanged);

            // Quantity
            y += spacing;
            this.lblQuantity.AutoSize = true;
            this.lblQuantity.Location = new System.Drawing.Point(labelX, y);
            this.lblQuantity.Size = new System.Drawing.Size(100, 23);
            this.lblQuantity.Text = "Количество:";

            this.nudQuantity.Location = new System.Drawing.Point(controlX, y);
            this.nudQuantity.Size = new System.Drawing.Size(100, 23);
            this.nudQuantity.ValueChanged += new System.EventHandler(this.nudQuantity_ValueChanged);

            // Amount
            y += spacing;
            this.lblAmount.AutoSize = true;
            this.lblAmount.Location = new System.Drawing.Point(labelX, y);
            this.lblAmount.Size = new System.Drawing.Size(100, 23);
            this.lblAmount.Text = "Сумма:";

            this.txtAmount.Location = new System.Drawing.Point(controlX, y);
            this.txtAmount.Size = new System.Drawing.Size(100, 23);
            this.txtAmount.ReadOnly = true;

            // AcquisitionMethod
            y += spacing;
            this.lblAcquisitionMethod.AutoSize = true;
            this.lblAcquisitionMethod.Location = new System.Drawing.Point(labelX, y);
            this.lblAcquisitionMethod.Size = new System.Drawing.Size(100, 23);
            this.lblAcquisitionMethod.Text = "Способ получения:";

            this.cmbAcquisitionMethod.Location = new System.Drawing.Point(controlX, y);
            this.cmbAcquisitionMethod.Size = new System.Drawing.Size(controlWidth, 23);
            this.cmbAcquisitionMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            // ReaderFullName
            y += spacing;
            this.lblReaderFullName.AutoSize = true;
            this.lblReaderFullName.Location = new System.Drawing.Point(labelX, y);
            this.lblReaderFullName.Size = new System.Drawing.Size(100, 23);
            this.lblReaderFullName.Text = "ФИО читателя:";

            this.txtReaderFullName.Location = new System.Drawing.Point(controlX, y);
            this.txtReaderFullName.Size = new System.Drawing.Size(controlWidth, 23);

            // OrderDate
            y += spacing;
            this.lblOrderDate.AutoSize = true;
            this.lblOrderDate.Location = new System.Drawing.Point(labelX, y);
            this.lblOrderDate.Size = new System.Drawing.Size(100, 23);
            this.lblOrderDate.Text = "Дата заказа:";

            this.dtpOrderDate.Location = new System.Drawing.Point(controlX, y);
            this.dtpOrderDate.Size = new System.Drawing.Size(200, 23);
            this.dtpOrderDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;

            // Buttons
            y += spacing + 10;
            this.btnSave.Location = new System.Drawing.Point(controlX, y);
            this.btnSave.Size = new System.Drawing.Size(120, 30);
            this.btnSave.Text = "Сохранить";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

            this.btnCancel.Location = new System.Drawing.Point(controlX + 130, y);
            this.btnCancel.Size = new System.Drawing.Size(120, 30);
            this.btnCancel.Text = "Отмена";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);

            // Add controls
            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblBook, this.cmbBook,
                this.lblUser, this.cmbUser,
                this.lblQuantity, this.nudQuantity,
                this.lblAmount, this.txtAmount,
                this.lblAcquisitionMethod, this.cmbAcquisitionMethod,
                this.lblReaderFullName, this.txtReaderFullName,
                this.lblOrderDate, this.dtpOrderDate,
                this.btnSave, this.btnCancel
            });

            ((System.ComponentModel.ISupportInitialize)(this.nudQuantity)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblBook;
        private System.Windows.Forms.ComboBox cmbBook;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.ComboBox cmbUser;
        private System.Windows.Forms.Label lblQuantity;
        private System.Windows.Forms.NumericUpDown nudQuantity;
        private System.Windows.Forms.Label lblAmount;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.Label lblAcquisitionMethod;
        private System.Windows.Forms.ComboBox cmbAcquisitionMethod;
        private System.Windows.Forms.Label lblReaderFullName;
        private System.Windows.Forms.TextBox txtReaderFullName;
        private System.Windows.Forms.Label lblOrderDate;
        private System.Windows.Forms.DateTimePicker dtpOrderDate;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
    }
}