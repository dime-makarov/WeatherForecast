using System;
using System.Windows.Forms;

namespace Dm.WeatherForecast.Client.Viewer.ViewerApp
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);

            ForecastClient.Dispose();
        }

        /// <summary>
        /// Setup data grid
        /// </summary>
        protected void SetupDataGrid()
        {
            DataGrid.AutoGenerateColumns = false;

            var colDate = new DataGridViewTextBoxColumn();
            colDate.Name = "Дата/время";
            colDate.DataPropertyName = "TargetDate";
            DataGrid.Columns.Add(colDate);

            var colTemp = new DataGridViewTextBoxColumn();
            colTemp.Name = "Температура (°C)";
            colTemp.DataPropertyName = "Temperature";
            DataGrid.Columns.Add(colTemp);

            var colWindSpeed = new DataGridViewTextBoxColumn();
            colWindSpeed.Name = "Скорость ветра (м/с)";
            colWindSpeed.DataPropertyName = "WindSpeed";
            DataGrid.Columns.Add(colWindSpeed);

            var colWindDir = new DataGridViewTextBoxColumn();
            colWindDir.Name = "Направление ветра";
            colWindDir.DataPropertyName = "WindDirection";
            DataGrid.Columns.Add(colWindDir);

            var colPress = new DataGridViewTextBoxColumn();
            colPress.Name = "Давление (мм. рт. ст.)";
            colPress.DataPropertyName = "Pressure";
            DataGrid.Columns.Add(colPress);

            var colHum = new DataGridViewTextBoxColumn();
            colHum.Name = "Осадки (мм.)";
            colHum.DataPropertyName = "Humidity";
            DataGrid.Columns.Add(colHum);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cmbCities = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.DataGrid = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.DataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbCities
            // 
            this.cmbCities.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCities.FormattingEnabled = true;
            this.cmbCities.Location = new System.Drawing.Point(12, 25);
            this.cmbCities.Name = "cmbCities";
            this.cmbCities.Size = new System.Drawing.Size(230, 21);
            this.cmbCities.TabIndex = 0;
            this.cmbCities.SelectionChangeCommitted += new System.EventHandler(this.cmbCities_SelectionChangeCommitted);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "City";
            // 
            // DataGrid
            // 
            this.DataGrid.AllowUserToAddRows = false;
            this.DataGrid.AllowUserToDeleteRows = false;
            this.DataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGrid.Location = new System.Drawing.Point(12, 83);
            this.DataGrid.Name = "DataGrid";
            this.DataGrid.ReadOnly = true;
            this.DataGrid.Size = new System.Drawing.Size(782, 216);
            this.DataGrid.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Forecast";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(806, 311);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.DataGrid);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbCities);
            this.Name = "MainForm";
            this.Text = "Forecast Viewer";
            ((System.ComponentModel.ISupportInitialize)(this.DataGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbCities;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView DataGrid;
        private System.Windows.Forms.Label label2;
    }
}

