using R5SALAO.MVC.DAO;
using R5SALAO.MVC.Model;
using System;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;

namespace R5SALAO.MVC.View
{
    public partial class ViewCadastroProduto : Form
    {
        DAOConexao conexao = new DAOConexao();
        DataSet dataSet = new DataSet();
        DataTable dataTable = new DataTable();
        string sql;
        int id;
        DAOUtil util = new DAOUtil();
        MDIPrincipal principal;


        public ViewCadastroProduto()
        {
            InitializeComponent();
            txtCodigo.Focus();
            carregarDados();
            radioAtivado.Checked = true;
            util.carregaComboBox("SELECT * FROM TBGRUPO", txtGrupo);


        }

        DAOProduto daoProduto = new DAOProduto();
        ModelProduto modelProduto = new ModelProduto();
        SQLiteCommand comando = new SQLiteCommand();

        public void carregarDados()
        {
            sql = "select * from TBPRODUTO";
            conexao.Conectar();
            comando = new SQLiteCommand(sql, conexao.con); try
            {
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(comando);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dbProduto.DataSource = dt;
                util.Log(dt.ToString());
                OrganizaGrid();
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

        private void button4_Click(object sender, EventArgs e)
        {
            Close();
        }
        //metodo para organizar grid
        public void OrganizaGrid()
        {
            dbProduto.Columns[0].Visible = false;
            dbProduto.Columns[1].Width = 70;
            dbProduto.Columns[2].Width = 240;
            dbProduto.Columns[3].Width = 80;
            dbProduto.Columns[3].Width = 80;
            dbProduto.Columns[4].Width = 73;
            dbProduto.Columns[5].Width = 80;
            dbProduto.Columns[6].Width = 80;
            dbProduto.Columns[7].Visible = false;

        }

        //metodo para limpar os campos
        public void ClearCamp()
        {
            txtCodigo.Text = null;
            txtNome.Text = null;
            txtCompra.Text = null;
            txtEstoqAtual.Text = null;
            txtEstoqueMin.Text = null;
            txtGrupo.SelectedItem = null;
            txtVenda.Text = null;

        }

        //botão de salvar
        private void button1_Click(object sender, EventArgs e)
        {
            modelProduto.codigo = Convert.ToInt32(txtCodigo.Text);
            modelProduto.COMPRA = float.Parse(txtCompra.Text);
            modelProduto.NOME = txtNome.Text;
            modelProduto.VENDA = float.Parse(txtVenda.Text);
            modelProduto.ESTOQUEATUAL = float.Parse(txtEstoqAtual.Text);
            modelProduto.ESTOQUEMINIMO = float.Parse(txtEstoqueMin.Text);
            modelProduto.GRUPO = txtGrupo.Text;
            int radio;
            if (radioAtivado.Checked)
            {
                radio = 1;
            }
            else
            {
                radio = 0;
            }
            modelProduto.STATUS = radio;
            /////////////////////////////////////////////////////////////////////////////////////
            Valid();
            daoProduto.Insert(modelProduto);
            carregarDados();
        }

        //metodo para validar os campos

        public void Valid()
        {
            if (txtCodigo.Text == "" || txtNome.Text == "" || txtCompra.Text == "" || txtEstoqAtual.Text == "" || txtEstoqueMin.Text == "" ||
                txtGrupo.Text == "" || txtVenda.Text == "")
            {
                util.MessageCampNull();
            }
        }


        //botão para atualizar
        private void button7_Click(object sender, EventArgs e)
        {
            id = Convert.ToInt32(this.dbProduto.CurrentRow.Cells[0].Value.ToString());
            modelProduto.NOME = txtNome.Text;
            modelProduto.codigo = Convert.ToInt32(txtCodigo.Text);
            modelProduto.ESTOQUEATUAL = float.Parse(txtEstoqAtual.Text);
            modelProduto.ESTOQUEMINIMO = float.Parse(txtEstoqueMin.Text);
            modelProduto.COMPRA = float.Parse(txtCompra.Text);
            modelProduto.VENDA = float.Parse(txtVenda.Text);
            modelProduto.GRUPO = txtGrupo.Text;
            modelProduto.STATUS = radioAtivado.Checked ? 1 : 0;//validação do radio checked
            modelProduto.id = id;

            try
            {
                Valid();
                daoProduto.Update(modelProduto);
                carregarDados();
                ClearCamp();

            }
            catch (Exception ex)
            {
                util.Log("erro: " + ex.Message);
                //MessageBox.Show("Erro ao salvar " + ex.Message);
                util.MessageError(ex);
            }
            finally
            {
                conexao.Desconectar();
            }

        }

        private void dbProduto_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var status = Convert.ToInt32(this.dbProduto.CurrentRow.Cells[8].Value);

            id = Convert.ToInt32(this.dbProduto.CurrentRow.Cells[0].Value.ToString());
            txtCodigo.Text = this.dbProduto.CurrentRow.Cells[1].Value.ToString();
            txtNome.Text = this.dbProduto.CurrentRow.Cells[2].Value.ToString();
            txtEstoqAtual.Text = this.dbProduto.CurrentRow.Cells[3].Value.ToString();
            txtEstoqueMin.Text = this.dbProduto.CurrentRow.Cells[4].Value.ToString();
            txtGrupo.Text = this.dbProduto.CurrentRow.Cells[5].Value.ToString();
            txtCompra.Text = this.dbProduto.CurrentRow.Cells[6].Value.ToString();
            txtVenda.Text = this.dbProduto.CurrentRow.Cells[7].Value.ToString();
            if (status.Equals("1"))
            {
                MessageBox.Show("" + status);
                radioAtivado.Checked = true;
            }
            else
            {
                radioDesativado.Checked = false;
            }
        }

        //botão excluir
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                id = Convert.ToInt32(this.dbProduto.CurrentRow.Cells[0].Value.ToString());
                DialogResult confirm = MessageBox.Show("Deseja Continuar?", "Excluir", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);

                if (confirm.ToString().ToUpper() == "YES")
                    daoProduto.Delete(id);
                util.Log("Registro excluido");
                carregarDados();
                ClearCamp();

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
        private void txtEstoqueMin_KeyPress(object sender, KeyPressEventArgs e)
        {
            util.ApenasNumber(sender, e);
        }

        private void txtEstoqAtual_KeyPress(object sender, KeyPressEventArgs e)
        {
            util.ApenasNumber(sender, e);
        }

        private void txtVenda_KeyPress(object sender, KeyPressEventArgs e)
        {
            util.ApenasNumber(sender, e);
        }

        private void txtCompra_KeyPress(object sender, KeyPressEventArgs e)
        {
            util.ApenasNumber(sender, e);
        }

        private void btCancelar_Click(object sender, EventArgs e)
        {
            ClearCamp();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            var view = new ViewGrupo().ShowDialog();
            util.carregaComboBox("select * from tbgrupo", txtGrupo);
        }

        private void txtEstoqueMin_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            sql = "select * from tbproduto where nome like('%" + txtPesquisa.Text + "%')";
            util.CarregarTabela(sql, dbProduto);
        }
    }
}
