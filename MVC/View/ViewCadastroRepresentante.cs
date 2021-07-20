using R5SALAO.MVC.DAO;
using R5SALAO.MVC.Model;
using System;
using System.Windows.Forms;

namespace R5SALAO.MVC.View
{
    public partial class ViewCadastroRepresentante : Form
    {
        public ViewCadastroRepresentante()
        {
            InitializeComponent();
            util.CarregarDados("select * from tbrepresentante", dbRepresentante);
        }
        ModelRepresantante model = new ModelRepresantante();
        DAORepresentante dao = new DAORepresentante();
        DAOUtil util = new DAOUtil();
        int id;


        public void Limpar()
        {
            txtBairro.Text = null;
            txtCep.Text = null;
            txtCidade.Text = null;
            txtCpf.Text = null;
            txtdata.Text = null;
            txtEmail.Text = null;
            txtEndereco.Text = null;
            txtNome.Text = null;
            txtNum.Text = null;
            txtTelefone.Text = null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            model.aniversario = txtdata.Text;
            model.bairro = txtBairro.Text;
            model.cep = txtCep.Text;
            model.cidade = txtCidade.Text;
            model.cpf = txtCpf.Text;
            model.email = txtEmail.Text;
            model.endereco = txtEndereco.Text;
            model.nome = txtNome.Text;
            model.numero = txtNum.Text;
            model.telefone = txtTelefone.Text;

            try
            {
                dao.Insert(model);
                util.CarregarDados("select * from tbrepresentante", dbRepresentante);
                Limpar();
            }
            catch (Exception ex)
            {
                util.MessageError(ex);
            }

        }

        private void dbRepresentante_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            id = Convert.ToInt32(this.dbRepresentante.CurrentRow.Cells[0].Value.ToString());
            txtNome.Text = this.dbRepresentante.CurrentRow.Cells[1].Value.ToString();
            txtTelefone.Text = this.dbRepresentante.CurrentRow.Cells[2].Value.ToString();
            txtCpf.Text = this.dbRepresentante.CurrentRow.Cells[3].Value.ToString();
            txtdata.Text = this.dbRepresentante.CurrentRow.Cells[4].Value.ToString();
            txtEmail.Text = this.dbRepresentante.CurrentRow.Cells[5].Value.ToString();
            txtCep.Text = this.dbRepresentante.CurrentRow.Cells[6].Value.ToString();
            txtEndereco.Text = this.dbRepresentante.CurrentRow.Cells[7].Value.ToString();
            txtNum.Text = this.dbRepresentante.CurrentRow.Cells[8].Value.ToString();
            txtCidade.Text = this.dbRepresentante.CurrentRow.Cells[9].Value.ToString();
            txtBairro.Text = this.dbRepresentante.CurrentRow.Cells[10].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            id = Convert.ToInt32(this.dbRepresentante.CurrentRow.Cells[0].Value.ToString());

            model.aniversario = txtdata.Text;
            model.bairro = txtBairro.Text;
            model.cep = txtCep.Text;
            model.cidade = txtCidade.Text;
            model.cpf = txtCpf.Text;
            model.email = txtEmail.Text;
            model.endereco = txtEndereco.Text;
            model.nome = txtNome.Text;
            model.numero = txtNum.Text;
            model.telefone = txtTelefone.Text;
            model.id = id;

            try
            {
                dao.Update(model);
                util.CarregarDados("select * from tbrepresentante", dbRepresentante);
                Limpar();
            }
            catch (Exception ex)
            {
                util.MessageError(ex);
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(this.dbRepresentante.CurrentRow.Cells[0].Value.ToString());
            dao.Delete(id);
            util.CarregarDados("select * from tbrepresentante", dbRepresentante);
            Limpar();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Limpar();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            var sql = "select * from tbproduto where nome like('%" + txtPesquisa.Text + "%')";
            util.CarregarTabela(sql, dbRepresentante);
        }
    }
}
