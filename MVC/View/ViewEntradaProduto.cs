using R5SALAO.MVC.DAO;
using R5SALAO.MVC.Model;
using System;
using System.Windows.Forms;

namespace R5SALAO.MVC.View
{
    public partial class ViewEntradaProduto : Form
    {
        DAOUtil util = new DAOUtil();
        ModelEntrada entrada = new ModelEntrada();
        DAOEntrada dao = new DAOEntrada();

        public ViewEntradaProduto()
        {
            InitializeComponent();
            util.CarregarDados("select CODIGO,NOME,ESTOQUEATUAL FROM TBPRODUTO", dbProduto);
            DateTime data = DateTime.Now;
            txtData.Text = data.ToString("dd/MM/yyyy");
            OrganizaGrid();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dbProduto_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            txtCodigo.Text = this.dbProduto.CurrentRow.Cells[0].Value.ToString();
            txtNome1.Text = this.dbProduto.CurrentRow.Cells[1].Value.ToString();
            txtAtual.Text = this.dbProduto.CurrentRow.Cells[2].Value.ToString();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        public void OrganizaGrid()
        {
            dbProduto.Columns[0].Width = 80;
            dbProduto.Columns[1].Width = 210;
            dbProduto.Columns[2].Width = 100;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            float atual = (float.Parse(txtAtual.Text) + float.Parse(txtEntrando.Text));
            dao.AtualizaEntrada(txtCodigo.Text, atual.ToString());

            dao.SalvaEntrada(txtCodigo.Text, txtNome1.Text, txtAtual.Text, txtEntrando.Text, txtData.Text);

            util.CarregarDados("select CODIGO,NOME,ESTOQUEATUAL FROM TBPRODUTO", dbProduto);
            OrganizaGrid();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void txtPesquisa_KeyPress(object sender, KeyPressEventArgs e)
        {
            var sql = "select * from tbproduto where nome like('%" + txtPesquisa.Text + "%')";
            util.CarregarTabela(sql, dbProduto);
        }
    }
}
