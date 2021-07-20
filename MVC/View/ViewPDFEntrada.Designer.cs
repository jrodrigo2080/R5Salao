namespace R5SALAO.MVC.View
{
    partial class ViewPDFEntrada
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
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.Report = new Microsoft.Reporting.WinForms.ReportViewer();
            this.ModelEntradaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.ModelEntradaBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // Report
            // 
            this.Report.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "EntradaSaida";
            reportDataSource1.Value = this.ModelEntradaBindingSource;
            this.Report.LocalReport.DataSources.Add(reportDataSource1);
            this.Report.LocalReport.ReportEmbeddedResource = "R5SALAO.MVC.Relatorio.RelatorioSaida.rdlc";
            this.Report.Location = new System.Drawing.Point(0, 0);
            this.Report.Name = "Report";
            this.Report.ServerReport.BearerToken = null;
            this.Report.Size = new System.Drawing.Size(800, 450);
            this.Report.TabIndex = 0;
            // 
            // ModelEntradaBindingSource
            // 
            this.ModelEntradaBindingSource.DataSource = typeof(R5SALAO.MVC.Model.ModelEntrada);
            // 
            // ViewPDFEntrada
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Report);
            this.Name = "ViewPDFEntrada";
            this.Text = "ViewPDFEntrada";
            this.Load += new System.EventHandler(this.ViewPDFEntrada_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ModelEntradaBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer Report;
        private System.Windows.Forms.BindingSource ModelEntradaBindingSource;
    }
}