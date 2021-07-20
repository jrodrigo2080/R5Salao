using R5SALAO.MVC.DAO;
using R5SALAO.MVC.Model;
using System;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;

namespace R5SALAO.MVC.View
{
    public partial class ViewCadastroUsuario : Form
    {
        public ViewCadastroUsuario()
        {
            InitializeComponent();
            carregarDados();
        }

        DAOUsuario dao = new DAOUsuario();
        DAOUtil util = new DAOUtil();
        ModelUsuario user = new ModelUsuario();
        DAOConexao conexao = new DAOConexao();
        string sql;
        int id;
        SQLiteCommand comando;


        public void carregarDados()
        {
            sql = "select id,nome from TBUSUARIO";
            conexao.Conectar();
            comando = new SQLiteCommand(sql, conexao.con);

            try
            {
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(comando);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                dbUsuario.DataSource = dt;
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
            txtConfSenha.Text = null;
            txtNome.Text = null;
            txtSenha.Text = null;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            user.NOME = txtNome.Text;
            user.senha = txtSenha.Text;

            try
            {
                dao.Save(user);
                carregarDados();
            }
            catch (Exception ex)
            {
                util.Log(ex.Message);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            user.ID = id = Convert.ToInt32(this.dbUsuario.CurrentRow.Cells[0].Value.ToString());
            user.NOME = txtNome.Text;
            user.senha = txtSenha.Text;
            int x = id;

            try
            {
                DialogResult confirm = MessageBox.Show("Deseja Continuar?", "Excluir", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);

                if (confirm.ToString().ToUpper() == "YES")
                    dao.Update(user);
                carregarDados();
                ClearCamp();
                carregarDados();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void dbUsuario_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            id = Convert.ToInt32(this.dbUsuario.CurrentRow.Cells[0].Value.ToString());
            txtNome.Text = this.dbUsuario.CurrentRow.Cells[1].Value.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                id = Convert.ToInt32(this.dbUsuario.CurrentRow.Cells[0].Value.ToString());
                DialogResult confirm = MessageBox.Show("Deseja Continuar?", "Excluir", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);

                if (confirm.ToString().ToUpper() == "YES")
                    dao.Delete(id);
                carregarDados();
                ClearCamp();

            }
            catch (Exception ex)
            {
                // conexao.Log("erro ao excluir " + ex.Message);
                MessageBox.Show(ex.Message.ToString(), "Erro ao Excluir", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                conexao.Desconectar();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ClearCamp();
        }
    }
}
