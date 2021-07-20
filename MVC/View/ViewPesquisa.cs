using R5SALAO.MVC.DAO;
using System;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;

namespace R5SALAO.MVC.View
{
    public partial class ViewPesquisa : Form
    {
        public ViewPesquisa()
        {
            InitializeComponent();
            carregarDados();
        }
        DAOConexao conexao = new DAOConexao();
        SQLiteCommand comando = new SQLiteCommand();
        string sql;
        DAOUtil util;

        public void carregarDados()
        {
            sql = "select CODIGO,NOME,VENDA  from TBPRODUTO";
            conexao.Conectar();
            comando = new SQLiteCommand(sql, conexao.con);

            try
            {
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(comando);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dbPesquisa.DataSource = dt;
            }
            catch (Exception ex)
            {
                util.Log(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        public void dbPesquisa_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dbPesquisa.Rows.Count == 0)
            {
                return;
            }
            else
            {
                ViewPdv pdv = new ViewPdv();
                DialogResult = DialogResult.OK;
                pdv.txtQuantidade.Enabled = true;
                pdv.txtQuantidade.Focus();
                Close();
            }
        }

        private void dbPesquisa_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var codigo = this.dbPesquisa.CurrentRow.Cells[0].Value.ToString();
            var nome = this.dbPesquisa.CurrentRow.Cells[1].Value.ToString();
            var preco = this.dbPesquisa.CurrentRow.Cells[2].Value.ToString();


        }
    }
}
