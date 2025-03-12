using System;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;

namespace libveil
{
    partial class frmEditBook : Form
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
            this.lblAuthor = new System.Windows.Forms.Label();
            this.txtAuthor = new System.Windows.Forms.TextBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.lblGenre = new System.Windows.Forms.Label();
            this.txtGenre = new System.Windows.Forms.TextBox();
            this.lblPublisher = new System.Windows.Forms.Label();
            this.cmbPublisher = new System.Windows.Forms.ComboBox();
            this.lblPublishYear = new System.Windows.Forms.Label();
            this.nudPublishYear = new System.Windows.Forms.NumericUpDown();
            this.lblVolumes = new System.Windows.Forms.Label();
            this.nudVolumes = new System.Windows.Forms.NumericUpDown();
            this.lblPrice = new System.Windows.Forms.Label();
            this.nudPrice = new System.Windows.Forms.NumericUpDown();
            this.lblQuantity = new System.Windows.Forms.Label();
            this.nudQuantity = new System.Windows.Forms.NumericUpDown();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();

            ((System.ComponentModel.ISupportInitialize)(this.nudPublishYear)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudVolumes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudQuantity)).BeginInit();
            this.SuspendLayout();

            // Form settings
            this.ClientSize = new System.Drawing.Size(484, 361);
            this.MinimumSize = new System.Drawing.Size(500, 400);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Редактирование книги";
            this.Padding = new System.Windows.Forms.Padding(10);

            int y = 20;
            int labelX = 20;
            int controlX = 140;
            int controlWidth = 300;
            int spacing = 35;

            // Author
            this.lblAuthor.AutoSize = true;
            this.lblAuthor.Location = new System.Drawing.Point(labelX, y);
            this.lblAuthor.Text = "Автор:";

            this.txtAuthor.Location = new System.Drawing.Point(controlX, y);
            this.txtAuthor.Size = new System.Drawing.Size(controlWidth, 20);

            // Title
            y += spacing;
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(labelX, y);
            this.lblTitle.Text = "Название:";

            this.txtTitle.Location = new System.Drawing.Point(controlX, y);
            this.txtTitle.Size = new System.Drawing.Size(controlWidth, 20);

            // Genre
            y += spacing;
            this.lblGenre.AutoSize = true;
            this.lblGenre.Location = new System.Drawing.Point(labelX, y);
            this.lblGenre.Text = "Жанр:";

            this.txtGenre.Location = new System.Drawing.Point(controlX, y);
            this.txtGenre.Size = new System.Drawing.Size(controlWidth, 20);

            // Publisher
            y += spacing;
            this.lblPublisher.AutoSize = true;
            this.lblPublisher.Location = new System.Drawing.Point(labelX, y);
            this.lblPublisher.Text = "Издательство:";

            this.cmbPublisher.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPublisher.Location = new System.Drawing.Point(controlX, y);
            this.cmbPublisher.Size = new System.Drawing.Size(controlWidth, 20);

            // PublishYear
            y += spacing;
            this.lblPublishYear.AutoSize = true;
            this.lblPublishYear.Location = new System.Drawing.Point(labelX, y);
            this.lblPublishYear.Text = "Год издания:";

            this.nudPublishYear.Location = new System.Drawing.Point(controlX, y);
            this.nudPublishYear.Size = new System.Drawing.Size(100, 20);

            // Volumes
            y += spacing;
            this.lblVolumes.AutoSize = true;
            this.lblVolumes.Location = new System.Drawing.Point(labelX, y);
            this.lblVolumes.Text = "Томов:";

            this.nudVolumes.Location = new System.Drawing.Point(controlX, y);
            this.nudVolumes.Size = new System.Drawing.Size(100, 20);

            // Price
            y += spacing;
            this.lblPrice.AutoSize = true;
            this.lblPrice.Location = new System.Drawing.Point(labelX, y);
            this.lblPrice.Text = "Цена:";

            this.nudPrice.Location = new System.Drawing.Point(controlX, y);
            this.nudPrice.Size = new System.Drawing.Size(100, 20);

            // Quantity
            y += spacing;
            this.lblQuantity.AutoSize = true;
            this.lblQuantity.Location = new System.Drawing.Point(labelX, y);
            this.lblQuantity.Text = "Количество:";

            this.nudQuantity.Location = new System.Drawing.Point(controlX, y);
            this.nudQuantity.Size = new System.Drawing.Size(100, 20);

            // Buttons
            y += spacing + 10;
            this.btnSave.Location = new System.Drawing.Point(controlX, y);
            this.btnSave.Size = new System.Drawing.Size(100, 30);
            this.btnSave.Text = "Сохранить";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

            this.btnCancel.Location = new System.Drawing.Point(controlX + 110, y);
            this.btnCancel.Size = new System.Drawing.Size(100, 30);
            this.btnCancel.Text = "Отмена";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);

            // Add controls to form
            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblAuthor, this.txtAuthor,
                this.lblTitle, this.txtTitle,
                this.lblGenre, this.txtGenre,
                this.lblPublisher, this.cmbPublisher,
                this.lblPublishYear, this.nudPublishYear,
                this.lblVolumes, this.nudVolumes,
                this.lblPrice, this.nudPrice,
                this.lblQuantity, this.nudQuantity,
                this.btnSave, this.btnCancel
            });

            ((System.ComponentModel.ISupportInitialize)(this.nudPublishYear)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudVolumes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPrice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudQuantity)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblAuthor;
        private System.Windows.Forms.TextBox txtAuthor;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.Label lblGenre;
        private System.Windows.Forms.TextBox txtGenre;
        private System.Windows.Forms.Label lblPublisher;
        private System.Windows.Forms.ComboBox cmbPublisher;
        private System.Windows.Forms.Label lblPublishYear;
        private System.Windows.Forms.NumericUpDown nudPublishYear;
        private System.Windows.Forms.Label lblVolumes;
        private System.Windows.Forms.NumericUpDown nudVolumes;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.NumericUpDown nudPrice;
        private System.Windows.Forms.Label lblQuantity;
        private System.Windows.Forms.NumericUpDown nudQuantity;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
    }
}