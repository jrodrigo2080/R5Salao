using R5SALAO.MVC.DAO;
using R5SALAO.MVC.Model;
using System;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;

namespace R5SALAO.MVC.View
{
    public partial class ViewMarcarHorario : Form
    {
        DAOConexao conexao = new DAOConexao();
        DAOUtil util = new DAOUtil();
        ModelAgendamento model = new ModelAgendamento();
        string sql;
        DAOAgendamento dao = new DAOAgendamento();
        SQLiteCommand comando;
        int id;
        public ViewMarcarHorario()
        {
            InitializeComponent();
            carregarDados();
            util.carregaComboBox("select * from TBCLIENTE", txtNome);
            util.carregaComboBox("select * from TBSERVICO", txtServico);
        }




        public void ClearCampp()
        {
            txtData.Text = null;
            txtHora.Text = null;
            txtNome.Text = null;
            txtObservacao.Text = null;
            txtServico.Text = null;
            txtTelefone.Text = null;
            txtValor.Text = null;

        }

        public void carregarDados()
        {
            sql = "select * from TBAGENDAMENTO";
            conexao.Conectar();
            comando = new SQLiteCommand(sql, conexao.con);
            try
            {
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(comando);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dbHora.DataSource = dt;
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
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                model.cliente = txtNome.Text;
                model.data = txtData.Text;
                model.observacao = txtObservacao.Text;
                model.servico = txtServico.Text;
                model.telefone = txtTelefone.Text;
                model.hora = txtHora.Text;
                model.valor = float.Parse(txtValor.Text);
                model.status = "ABERTO";

                dao.Save(model);
                carregarDados();
                ClearCampp();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dbHora_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            id = Convert.ToInt32(this.dbHora.CurrentRow.Cells[0].Value.ToString());
            txtTelefone.Text = this.dbHora.CurrentRow.Cells[1].Value.ToString();
            txtNome.Text = this.dbHora.CurrentRow.Cells[2].Value.ToString();
            txtServico.Text = this.dbHora.CurrentRow.Cells[3].Value.ToString();
            txtObservacao.Text = this.dbHora.CurrentRow.Cells[4].Value.ToString();
            //txtStatus.Text = this.dbHora.CurrentRow.Cells[5].Value.ToString();
            txtData.Text = this.dbHora.CurrentRow.Cells[6].Value.ToString();
            txtHora.Text = this.dbHora.CurrentRow.Cells[7].Value.ToString();
            txtValor.Text = this.dbHora.CurrentRow.Cells[8].Value.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                model.cliente = txtNome.Text;
                model.data = txtData.Text;
                model.observacao = txtObservacao.Text;
                model.servico = txtServico.Text;
                model.telefone = txtTelefone.Text;
                model.hora = txtHora.Text;
                model.valor = float.Parse(txtValor.Text);
                model.status = "ABERTO";
                model.id = id = Convert.ToInt32(this.dbHora.CurrentRow.Cells[0].Value.ToString());
                dao.Update(model);
                carregarDados();
                ClearCampp();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            model.id = id = Convert.ToInt32(this.dbHora.CurrentRow.Cells[0].Value.ToString());
            dao.Delete(id);
            carregarDados();
            ClearCampp();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ClearCampp();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var view = new ViewCadastroCliente().ShowDialog();
            util.carregaComboBox("select * from TBCLIENTE", txtNome);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            var view = new ViewServico().ShowDialog();
            util.carregaComboBox("select * from TBSERVICO", txtServico);
        }
    }
}
