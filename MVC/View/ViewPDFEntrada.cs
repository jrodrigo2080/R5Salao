using System;
using System.Windows.Forms;

namespace R5SALAO.MVC.View
{
    public partial class ViewPDFEntrada : Form
    {
        public ViewPDFEntrada()
        {
            InitializeComponent();
        }

        private void ViewPDFEntrada_Load(object sender, EventArgs e)
        {

            this.Report.RefreshReport();
        }
    }
}
