using R5SALAO.MVC.DAO;
using R5SALAO.MVC.Model;
using System;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;

namespace R5SALAO.MVC.View
{
    public partial class ViewGrupo : Form
    {
        public ViewGrupo()
        {
            InitializeComponent();
            carregarDados();
        }
        DAOConexao conexao = new DAOConexao();
        SQLiteCommand comando = new SQLiteCommand();
        string sql;
        DAOUtil util;
        DAOGRUPO dao = new DAOGRUPO();
        ModelGrupo model = new ModelGrupo();
        public void carregarDados()
        {
            sql = "select *  from TBgrupo";
            conexao.Conectar();
            comando = new SQLiteCommand(sql, conexao.con);

            try
            {
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(comando);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dbGrupo.DataSource = dt;
            }
            catch (Exception ex)
            {
                util.Log(ex.Message);
            }
            finally
            {
                conexao.Desconectar();
            }
            OrganizaGrid();
        }

        /// <summary>
        /// METODO PARA LIMPAR
        /// </summary>
        /// 
        public void limpar()
        {
            txtNome.Text = null;
        }

        public void OrganizaGrid()
        {
            dbGrupo.Columns[0].Visible = false;
            dbGrupo.Columns[1].Width = 170;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (txtNome.Text.Equals(""))
            {

            }
            else
            {
                model.NOME = txtNome.Text;
                dao.Save(model);
                carregarDados();
                limpar();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            limpar();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(this.dbGrupo.CurrentRow.Cells[0].Value.ToString());
            if (txtNome.Text.Equals(""))
            {

            }
            else
            {
                model.NOME = txtNome.Text;
                model.ID = id;
                dao.Update(model);
                carregarDados();
                limpar();
            }
        }

        private void dbGrupo_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int id = Convert.ToInt32(this.dbGrupo.CurrentRow.Cells[0].Value.ToString());
            txtNome.Text = this.dbGrupo.CurrentRow.Cells[1].Value.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(this.dbGrupo.CurrentRow.Cells[0].Value.ToString());
            dao.Delete(id);
            carregarDados();
            limpar();
        }
    }
}
