using R5SALAO.MVC.DAO;
using R5SALAO.MVC.Model;

using System;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Windows.Forms;

namespace R5SALAO.MVC.View
{
    public partial class MDIPrincipal : Form
    {
        DataSet dataSet = new DataSet();
        DataTable dataTable = new DataTable();
        string sql;
        int id;
        DAOProduto daoProduto = new DAOProduto();
        ModelProduto modelProduto = new ModelProduto();
        SQLiteCommand comando = new SQLiteCommand();
        DAOConexao conexao = new DAOConexao();
        DAOUtil util = new DAOUtil();

        public MDIPrincipal()
        {
            InitializeComponent();
            carregarDados();
            dataHora();
            carregaAgenda();
        }

        //********************** METODOS PARA UTILIZAR NESSA TELA **************************

        //METODO PARA CARREGAR A TABELA DE PRODUTOS
        public void carregarDados()
        {
            sql = "select * from TBPRODUTO where STATUS='1'";
            conexao.Conectar();
            comando = new SQLiteCommand(sql, conexao.con);
            try
            {
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(comando);
                DataTable dt = new DataTable();

                adapter.Fill(dt);
                dbProduto.DataSource = dt;
                OrganizaGrid();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                conexao.Desconectar();
            }

        }

        //********* METODO PARA CARREGAR A DATA ******************
        public void dataHora()
        {
            lbDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }

        // ******* METODO PARA ORGANIZAR A TABELA PRODUTOS *********
        public void OrganizaGrid()
        {
            dbProduto.Columns[0].Visible = false;
            dbProduto.Columns[1].Width = 100;
            dbProduto.Columns[2].Width = 320;
            dbProduto.Columns[3].Width = 140;
            dbProduto.Columns[4].Width = 140;
            dbProduto.Columns[5].Width = 130;
            dbProduto.Columns[6].DefaultCellStyle.Format = "C2";
            dbProduto.Columns[7].DefaultCellStyle.Format = "C2";
            dbProduto.Columns[6].Width = 92;
            dbProduto.Columns[7].Width = 85;
            dbProduto.Columns[8].Visible = false;
        }
        // ******* METODO PARA ORGANIZAR A AGENDA *********
        public void OrganizaaAgenda()
        {
            dbAgenda.Columns[0].Visible = false;
            dbAgenda.Columns[1].Width = 172;
            dbAgenda.Columns[2].Width = 60;
            dbAgenda.Columns[3].Visible = false;

        }

        //********* METODO PARA CARREGAR A AGENDA **************
        public void carregaAgenda()
        {
            sql = "select id,nome,hora,status from TBAGENDAMENTO where data='" + lbDate.Text + "' AND STATUS='ABERTO' ORDER BY hora";
            conexao.Conectar();
            comando = new SQLiteCommand(sql, conexao.con);
            try
            {
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(comando);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dbAgenda.DataSource = dt;
                OrganizaaAgenda();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                conexao.Desconectar();
            }

        }

        /// METODO PARA MUDAR A COR DE FUNDO DO MDI
        public void alteracordomdi()
        {
            foreach (Control control in this.Controls)
            {
                if (control is MdiClient)
                {
                    control.BackColor = Color.White;
                }
            }
        }

        /// METODO PARA FECHAR O AGENDAMENTO
        public void fechaAgendamento(int id)
        {
            sql = "UPDATE TBAGENDAMENTO SET STATUS = 'FECHADO' WHERE ID=@ID";
            conexao.Conectar();
            comando = new SQLiteCommand(sql, conexao.con);
            comando.Parameters.AddWithValue("@ID", id);
            try
            {
                comando.ExecuteNonQuery();
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
        //**********************************************************************************
        private void ShowNewForm(object sender, EventArgs e)
        {
            var VIEW = new ViewCadastroProduto().ShowDialog();
            carregarDados();
        }

        private void OpenFile(object sender, EventArgs e)
        {
            var view = new ViewCadastroCliente();
            view.ShowDialog();
        }

        private void MDIPrincipal_Load(object sender, EventArgs e)
        {
            alteracordomdi();
            var form1 = new Form1();
            lbusuario.Text = form1.txtLogin.Text;
        }
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var view = new ViewServico();
            view.ShowDialog();
        }
        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var view = new ViewMarcarHorario();
            view.ShowDialog();
            carregaAgenda();
        }
        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            ViewCadastroUsuario user = new ViewCadastroUsuario();
            user.ShowDialog();
        }

        ///Metodo para remover agendamento
        private void dbAgenda_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            DialogResult confirm = MessageBox.Show("Deseja Fechar o Agendamento?", "Fechar Agendamento", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);

            if (confirm.ToString().ToUpper() == "YES")
            {
                var id = Convert.ToInt32(this.dbAgenda.CurrentRow.Cells[0].Value.ToString());
                if (id <= 0)
                {

                }
                else
                {
                    fechaAgendamento(id);
                    carregaAgenda();
                }
            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            var view = new ViewCadastroRepresentante().ShowDialog();
        }

        private void arrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var View = new ViewPdv().ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void vendedorasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var v = new ViewVendedoras().ShowDialog();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void entradaDePeodutosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var v = new ViewEntradaProduto().ShowDialog();
        }

        private void saídaDeProdutosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var v = new ViewSaidaProduto().ShowDialog();
        }

        private void txtPesquisa_KeyPress(object sender, KeyPressEventArgs e)
        {
            sql = "select * from tbproduto where nome like('%" + txtPesquisa.Text + "%')";
            util.CarregarTabela(sql, dbProduto);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sql = "select * from tbproduto where nome like('%" + txtPesquisa.Text + "%')";
            util.CarregarTabela(sql, dbProduto);
        }
    }
}
