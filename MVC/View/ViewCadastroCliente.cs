using R5SALAO.MVC.DAO;
using R5SALAO.MVC.Model;
using System;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;

namespace R5SALAO.MVC.View
{
    public partial class ViewCadastroCliente : Form
    {
        public ViewCadastroCliente()
        {
            InitializeComponent();
            carregarDados();
        }
        int id;
        string sql;
        DAOCliente cliente = new DAOCliente();
        ModelCliente model = new ModelCliente();
        DAOUtil util = new DAOUtil();
        SQLiteCommand comando;
        DAOConexao conexao = new DAOConexao();

        public void carregarDados()
        {
            sql = "select *  from TBCLIENTE";
            conexao.Conectar();
            comando = new SQLiteCommand(sql, conexao.con);

            try
            {
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(comando);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                dbCliente.DataSource = dt;
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

        public void ClearCamp()
        {
            txtAnive.Text = null;
            txtBairro.Text = null;
            txtCep.Text = null;
            txtCidade.Text = null;
            txtCPF.Text = null;
            txtEmail.Text = null;
            txtEndereco.Text = null;
            txtNome.Text = null;
            txtNum.Text = null;
            txtObservacao.Text = null;
            txtTelefone.Text = null;
        }
        private void button1_Click(object sender, EventArgs e)
        {

            var s = txtAnive.Text;

            model.NOME = txtNome.Text;
            model.CPF = txtCPF.Text;
            model.aniversario = s;
            model.bairro = txtBairro.Text;
            model.cep = txtCep.Text;
            model.cidade = txtCidade.Text;
            model.email = txtEmail.Text;
            model.endereco = txtEndereco.Text;
            model.TELEFONE = txtTelefone.Text;
            model.observacao = txtObservacao.Text;
            model.numero = txtNum.Text;
            try
            {
                cliente.SaveCliente(model);
                carregarDados();
                ClearCamp();
            }
            catch (Exception ex)
            {
                util.Log(ex.Message);
                util.MessageError(ex);
            }


        }

        private void button4_Click(object sender, EventArgs e)
        {

            var s = txtAnive.Text;
            id = Convert.ToInt32(this.dbCliente.CurrentRow.Cells[0].Value.ToString());


            model.NOME = txtNome.Text;
            model.CPF = txtCPF.Text;
            model.aniversario = s;
            model.bairro = txtBairro.Text;
            model.cep = txtCep.Text;
            model.cidade = txtCidade.Text;
            model.email = txtEmail.Text;
            model.endereco = txtEndereco.Text;
            model.TELEFONE = txtTelefone.Text;
            model.observacao = txtObservacao.Text;
            model.numero = txtNum.Text;
            model.Id = id;

            try
            {
                cliente.UpdateCliente(model);
                carregarDados();
                ClearCamp();
            }
            catch (Exception ex)
            {
                util.MessageError(ex);
                util.Log(ex.Message);
            }


        }

        private void dbCliente_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            id = Convert.ToInt32(this.dbCliente.CurrentRow.Cells[0].Value.ToString());
            txtNome.Text = this.dbCliente.CurrentRow.Cells[1].Value.ToString();
            txtCPF.Text = this.dbCliente.CurrentRow.Cells[3].Value.ToString();
            txtTelefone.Text = this.dbCliente.CurrentRow.Cells[2].Value.ToString();
            txtAnive.Text = this.dbCliente.CurrentRow.Cells[4].Value.ToString();
            txtEmail.Text = this.dbCliente.CurrentRow.Cells[5].Value.ToString();
            txtCep.Text = this.dbCliente.CurrentRow.Cells[6].Value.ToString();
            txtEndereco.Text = this.dbCliente.CurrentRow.Cells[7].Value.ToString();
            txtNum.Text = this.dbCliente.CurrentRow.Cells[8].Value.ToString();
            txtCidade.Text = this.dbCliente.CurrentRow.Cells[9].Value.ToString();
            txtBairro.Text = this.dbCliente.CurrentRow.Cells[10].Value.ToString();
            txtObservacao.Text = this.dbCliente.CurrentRow.Cells[11].Value.ToString();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            id = Convert.ToInt32(this.dbCliente.CurrentRow.Cells[0].Value.ToString());
            try
            {


                DialogResult confirm = MessageBox.Show("Deseja Continuar?", "Excluir", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);

                if (confirm.ToString().ToUpper() == "YES")
                    cliente.DeleteCliente(Convert.ToInt32(id));
                carregarDados();
                ClearCamp();

            }
            catch (Exception ex)
            {
                util.Log(ex.Message);
                MessageBox.Show(ex.Message.ToString(), "Erro ao Excluir", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                conexao.Desconectar();
            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ClearCamp();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            sql = "select * from tbproduto where nome like('%" + txtPesquisa.Text + "%')";
            util.CarregarTabela(sql, dbCliente);
        }
    }
}
