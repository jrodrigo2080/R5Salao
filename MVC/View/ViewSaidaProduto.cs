using R5SALAO.MVC.DAO;
using System;
using System.Windows.Forms;

namespace R5SALAO.MVC.View
{
    public partial class ViewSaidaProduto : Form
    {
        DAOUtil util = new DAOUtil();
        DAOSaida dao = new DAOSaida();
        public ViewSaidaProduto()
        {
            InitializeComponent();
            util.CarregarDados("select CODIGO,NOME,ESTOQUEATUAL FROM TBPRODUTO", dbProduto);
            OrganizaGrid();
        }
        public void OrganizaGrid()
        {
            dbProduto.Columns[0].Width = 80;
            dbProduto.Columns[1].Width = 210;
            dbProduto.Columns[2].Width = 100;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            float atual1 = float.Parse(txtAtual.Text);
            float saida = float.Parse(txtEntrando.Text);
            if (saida > atual1)
            {
                util.MessageErro("Saida não pode ser maior que o total!");
            }
            else
            {
                float atual = atual1 - saida;
                dao.AtualizaSaida(txtCodigo.Text, atual.ToString());

                dao.SalvaSaida(txtCodigo.Text, txtNome1.Text, txtAtual.Text, txtEntrando.Text, txtData.Text);

            }
            util.CarregarDados("select CODIGO,NOME,ESTOQUEATUAL FROM TBPRODUTO", dbProduto);
            OrganizaGrid();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtEntrando_KeyPress(object sender, KeyPressEventArgs e)
        {
            util.ApenasNumber(sender, e);
        }

        private void dbProduto_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            txtCodigo.Text = this.dbProduto.CurrentRow.Cells[0].Value.ToString();
            txtNome1.Text = this.dbProduto.CurrentRow.Cells[1].Value.ToString();
            txtAtual.Text = this.dbProduto.CurrentRow.Cells[2].Value.ToString();
        }

        private void txtPesquisa_KeyPress(object sender, KeyPressEventArgs e)
        {
            var sql = "select * from tbproduto where nome like('%" + txtPesquisa.Text + "%')";
            util.CarregarTabela(sql, dbProduto);
        }
    }
}
