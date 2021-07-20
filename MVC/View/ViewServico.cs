using R5SALAO.MVC.DAO;
using R5SALAO.MVC.Model;

using System;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;

namespace R5SALAO.MVC.View
{
    public partial class ViewServico : Form
    {
        public ViewServico()
        {
            InitializeComponent();
            carregarDados();
            util.carregaComboBox("select * from TBTIPOCATEGORIA", txtTipo);
        }
        ModelServico model = new ModelServico();
        DAOServico dao = new DAOServico();
        DAOUtil util = new DAOUtil();
        DAOConexao conexao = new DAOConexao();
        SQLiteCommand comando = new SQLiteCommand();
        string sql;
        int id;

        public void carregarDados()
        {
            sql = "select *  from TBSERVICO";
            conexao.Conectar();
            comando = new SQLiteCommand(sql, conexao.con);

            try
            {
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(comando);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                dbServico.DataSource = dt;
                util.Log(dt.ToString());

            }
            catch (Exception ex)
            {
                util.Log("erro ao listar: " + ex.Message);
            }
            finally
            {
                conexao.Desconectar();
            }

        }
        //metodo para limpar os campos
        public void clearCamp()
        {

            txtCusto.Text = null;
            txtNome.Text = null;
            txtTempo.Text = null;
            txtTipo.Text = null;
            txtValor.Text = null;
        }
        private void button6_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            model.CUSTO = float.Parse(txtCusto.Text);
            model.NOME = txtNome.Text;
            model.TEMPO = txtTempo.Text;
            model.TIPO = txtTipo.Text;
            model.VALOR = float.Parse(txtValor.Text);


            try
            {
                dao.Save(model);
                carregarDados();
                clearCamp();
            }
            catch (Exception ex)
            {
                util.MessageError(ex);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            id = Convert.ToInt32(this.dbServico.CurrentRow.Cells[0].Value.ToString());
            model.CUSTO = float.Parse(txtCusto.Text);
            model.NOME = txtNome.Text;
            model.TEMPO = txtTempo.Text;
            model.TIPO = txtTipo.Text;
            model.VALOR = float.Parse(txtValor.Text);
            model.ID = id;


            try
            {
                dao.Update(model);
                util.MessageUpdate();
                carregarDados();
                clearCamp();
            }
            catch (Exception ex)
            {
                util.MessageError(ex);
            }

        }

        private void dbServico_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string a;
            id = Convert.ToInt32(this.dbServico.CurrentRow.Cells[0].Value.ToString());
            txtNome.Text = this.dbServico.CurrentRow.Cells[1].Value.ToString();
            txtCusto.Text = this.dbServico.CurrentRow.Cells[2].Value.ToString();
            txtTipo.Text = this.dbServico.CurrentRow.Cells[3].Value.ToString();
            txtValor.Text = this.dbServico.CurrentRow.Cells[4].Value.ToString();
            txtTempo.Text = this.dbServico.CurrentRow.Cells[5].Value.ToString();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            id = Convert.ToInt32(this.dbServico.CurrentRow.Cells[0].Value.ToString());
            try
            {


                DialogResult confirm = MessageBox.Show("Deseja Continuar?", "Excluir", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);

                if (confirm.ToString().ToUpper() == "YES")
                    dao.DeleteCliente(Convert.ToInt32(id));

                util.Log("Registro excluido");
                carregarDados();
                clearCamp();

            }
            catch (Exception ex)
            {
                util.Log("erro ao excluir " + ex.Message);
                MessageBox.Show(ex.Message.ToString(), "Erro ao Excluir", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                conexao.Desconectar();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            var view = new ViewTipo().ShowDialog();
            util.carregaComboBox("select * from TBTIPOCATEGORIA", txtTipo);
        }

        private void txtTipo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            var sql = "select * from tbproduto where nome like('%" + textBox1.Text + "%')";
            util.CarregarTabela(sql, dbServico);
        }
    }
}
