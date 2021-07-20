using R5SALAO.MVC.DAO;
using R5SALAO.MVC.Model;
using System;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;

namespace R5SALAO.MVC.View
{
    public partial class ViewTipo : Form
    {
        public ViewTipo()
        {
            InitializeComponent();
            carregarDados();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Close();
        }
        DAOConexao conexao = new DAOConexao();
        ModelTipo model = new ModelTipo();
        DAOTipo dao = new DAOTipo();
        DAOUtil util = new DAOUtil();
        String sql;
        int id;
        SQLiteCommand comando = new SQLiteCommand();
        private void button1_Click(object sender, EventArgs e)
        {
            if (txtNome.Text.Equals(""))
            {
                util.MessageCampNull();
            }
            else
            {
                model.NOME = txtNome.Text;
                dao.Save(model);
                carregarDados();
            }
        }

        public void carregarDados()
        {
            sql = "select *  from TBTIPOCATEGORIA";
            conexao.Conectar();
            comando = new SQLiteCommand(sql, conexao.con);
            try
            {
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(comando);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dbTipo.DataSource = dt;
            }
            catch (Exception ex)
            {
                util.Log(ex.Message);
            }
            finally
            {
                conexao.Desconectar();
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (txtNome.Text.Equals(""))
            {
                util.MessageCampNull();
            }
            else
            {
                id = Convert.ToInt32(this.dbTipo.CurrentRow.Cells[0].Value.ToString());

                model.NOME = txtNome.Text;
                model.ID = id;
                dao.Update(model);
                carregarDados();
            }
        }

        private void dbTipo_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            id = Convert.ToInt32(this.dbTipo.CurrentRow.Cells[0].Value.ToString());
            txtNome.Text = this.dbTipo.CurrentRow.Cells[1].Value.ToString();

        }

        private void button3_Click(object sender, EventArgs e)
        {

            id = Convert.ToInt32(this.dbTipo.CurrentRow.Cells[0].Value.ToString());
            model.ID = id;
            dao.Delete(id);
            carregarDados();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            txtNome.Text = "";
        }
    }
}
