using R5SALAO.MVC.DAO;
using System;
using System.Data.SQLite;
using System.Windows.Forms;

namespace R5SALAO.MVC.View
{
    public partial class ViewPdv : Form
    {

        DAOConexao conexao = new DAOConexao();
        DAOUtil util = new DAOUtil();
        float precoTotal = 0;
        string sql;
        SQLiteCommand comando = new SQLiteCommand();
        SQLiteDataReader reader;
        float total = 0;
        float troco = 0;
        string estoque = "0";
        public ViewPdv()
        {
            InitializeComponent();
            txtCliente.Text = "";

        }

        private void ViewPdv_Load(object sender, EventArgs e)
        {
            GerarCodigoVenda();
            dataHora();
            txtQuantidade.Text = "1";
            txtUnit.Text = "0,00";
            txtVtotal.Text = "0,00";
            txtDinheiro.Text = "0,00";
            txtCartao.Text = "0,00";
            txtFiado.Text = "0,00";
            util.carregaComboBox("select * from tbrepresentante", txtCliente);
            OrganizaGrid();
            txtCodigo.Focus();
            panelRepresentante.Visible = true;
            txtDescricao.Text = "CAIXA LIVRE";
            txtCliente.Text = null;
        }
        public void BuscarProduto()
        {
            conexao.Conectar();
            sql = "select * from tbproduto where codigo ='" + txtCodigo.Text + "'";
            try
            {
                comando = new SQLiteCommand(sql, conexao.con);
                SQLiteDataReader dr = comando.ExecuteReader();
                dr.Read();
                if (!dr.HasRows)
                {
                    MessageBox.Show("Produto não encontrado", "Produto não encontrado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtCodigo.Clear();
                    txtCodigo.Focus();
                }
                else
                {
                    txtDescricao.Text = dr["nome"].ToString();
                    txtUnit.Text = dr["venda"].ToString();
                    estoque = dr["estoqueatual"].ToString();
                    txtQuantidade.Enabled = true;
                    txtQuantidade.Focus();
                }
            }
            catch (Exception ex)
            {
                util.Log(ex.Message);
                MessageBox.Show(ex.Message);
            }
        }

        public void GerarCodigoVenda()
        {
            conexao.Conectar();
            sql = "select max(codigo) from tbvenda";

            try
            {
                conexao.Conectar();
                comando = new SQLiteCommand(sql, conexao.con);
                if (comando.ExecuteScalar() == DBNull.Value)
                {
                    lbCodigoVenda.Text = "1";
                }
                else
                {
                    Int32 ra = Convert.ToInt32(comando.ExecuteScalar()) + 1;
                    lbCodigoVenda.Text = ra.ToString();
                }
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

        public void LimparCampo()
        {
            txtCodigo.Text = null;
            txtDescricao.Text = null;
            txtUnit.Text = "0,00";
            txtQuantidade.Text = "1";
        }
        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                BuscarProduto();
            }
        }
        private void TimerDate_Tick(object sender, EventArgs e)
        {
            lbHora.Text = $"{DateTime.Now.Hour.ToString()}:{DateTime.Now.Minute.ToString()}:{DateTime.Now.Second.ToString()} hrs";
        }

        public void BaixaEstoque()
        {
            conexao.Conectar();
            sql = "update tbproduto set estoqueatual=@estoque where codigo=@codigo";
            comando = new SQLiteCommand(sql, conexao.con);
            try
            {
                for (int i = 0; i < dbVenda.Rows.Count; i++)
                {
                    float num1 = float.Parse((string)dbVenda.Rows[i].Cells[2].Value);
                    float num2 = float.Parse((string)dbVenda.Rows[i].Cells[5].Value);
                    float n = (num2 - num1);

                    comando.Parameters.Clear();
                    comando.Parameters.AddWithValue("@codigo", dbVenda.Rows[i].Cells[0].Value);
                    comando.Parameters.AddWithValue("@estoque", n);
                    comando.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                util.Log(ex.Message);
                util.MessageError(ex);
            }
        }


        public void inserirVendaItens()
        {
            conexao.Conectar();
            sql = "insert into tbvendaitens(dataVenda,dataentrega,dataretirada,cliente,codigo,codigoproduto,descricao,quantidade,valorTotal,status,tipo)values(@data,@dataentrega,@dataretirada,@cliente,@codigo,@codigoproduto,@descricao,@quantidade,@valorTotal,@status,@tipo)";
            comando = new SQLiteCommand(sql, conexao.con);
            string status = "1";
            string tipo = "1";
            try
            {
                for (int i = 0; i < dbVenda.Rows.Count; i++)
                {
                    comando.Parameters.Clear();
                    comando.Parameters.AddWithValue("@codigo", lbCodigoVenda.Text);
                    comando.Parameters.AddWithValue("@cliente", txtCliente.Text);
                    comando.Parameters.AddWithValue("@data", lbData.Text);
                    comando.Parameters.AddWithValue("@dataRetirada", txtRetirada.Text);
                    comando.Parameters.AddWithValue("@dataEntrega", txtEntrega.Text);
                    comando.Parameters.AddWithValue("@codigoproduto", dbVenda.Rows[i].Cells[0].Value);
                    comando.Parameters.AddWithValue("@descricao", dbVenda.Rows[i].Cells[1].Value);
                    comando.Parameters.AddWithValue("@quantidade", dbVenda.Rows[i].Cells[2].Value);
                    comando.Parameters.AddWithValue("@valorTotal", dbVenda.Rows[i].Cells[3].Value);
                    comando.Parameters.AddWithValue("@status", status);
                    comando.Parameters.AddWithValue("@tipo", tipo);
                    comando.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                util.Log(ex.Message);
                util.MessageError(ex);
            }
        }




        public void inserirVenda()
        {

            conexao.Conectar();
            sql = "insert into tbvenda(codigo,valorTotal,hora,data,pagdinheiro,pagcartao,pagfiado,troco) values " +
                            "(@codigo,@valorTotal,@hora,@data,@pagdinheiro,@pagcartao,@pagfiado,@troco)";
            comando = new SQLiteCommand(sql, conexao.con);

            comando.Parameters.AddWithValue("@codigo", lbCodigoVenda.Text);
            comando.Parameters.AddWithValue("@valorTotal", txtVtotal.Text);
            comando.Parameters.AddWithValue("@data", lbData.Text);
            comando.Parameters.AddWithValue("@hora", lbHora.Text);
            comando.Parameters.AddWithValue("@pagdinheiro", txtDinheiro.Text);
            comando.Parameters.AddWithValue("@pagcartao", txtCartao.Text);
            comando.Parameters.AddWithValue("@pagfiado", txtFiado.Text);
            comando.Parameters.AddWithValue("@troco", txtToco.Text);
            try
            {
                comando.ExecuteNonQuery();
                util.MessageSave();
            }
            catch (Exception ex)
            {
                util.Log(ex.Message);
                util.MessageError(ex);
            }
        }

        public void OrganizaGrid()
        {
            dbVenda.Columns[0].Width = 60;
            dbVenda.Columns[1].Width = 200;
            dbVenda.Columns[2].Width = 60;
            dbVenda.Columns[3].Width = 60;
            dbVenda.Columns[4].Width = 65;
        }

        private void txtQuantidade_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                float t = float.Parse(txtQuantidade.Text) * float.Parse(txtUnit.Text);
                precoTotal += float.Parse(t.ToString());
                float v = precoTotal;
                dbVenda.Rows.Add(txtCodigo.Text, txtDescricao.Text, txtQuantidade.Text, txtUnit.Text, t.ToString(), estoque);
                txtVtotal.Text = v.ToString();
                util.convertDinheiro(v);
                LimparCampo();
                txtCodigo.Focus();

            }
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtQuantidade_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (txtQuantidade.Text != string.Empty)
            {
                txtCodigo.Focus();
            }
            else if (txtQuantidade.Text.Equals("") || txtQuantidade.Text.Equals("0"))
            {
                MessageBox.Show("Quanitadade Invalida!", "Quanitadade Invalida!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtQuantidade.Focus();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            venda.SelectedTab = pag;

            txtVtotal2.Text = txtVtotal.Text;

            txtPag.Focus();
        }

        public void MontaVenda()
        {
            pag.Focus();
            float total2 = float.Parse(txtVtotal2.Text);
            float fiado = float.Parse(txtFiado.Text);
            float dinheiro = float.Parse(txtDinheiro.Text);
            float valorTotal = float.Parse(txtVtotal.Text);
            float cartao = float.Parse(txtCartao.Text);
            total = float.Parse(txtVtotal.Text);
        }
        public void pagamento()
        {
           
            BaixaEstoque();
            inserirVenda();
            inserirVendaItens();
            GerarCodigoVenda();
            dbVenda.Rows.Clear();
            LimparCampo();
            txtVtotal.Text = "0,00";
            precoTotal = 0;
            txtCliente.Text = null;
            txtEntrega.Text = null;
            txtRetirada.Text = null;
            txtVtotal2.Text = null;
            txtPag.Text = null;
            txtDinheiro.Text = "0,00";
            txtCartao.Text = "0,00";
            txtFiado.Text = "0,00";
            txtToco.Text = "0,00";
            txtCliente.Text = "";

        }

        private void rbRepresentante_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void rbNormal_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        public void limparGrid()
        {
            int cont = dbVenda.SelectedRows.Count;



        }


        private void Remover_Click(object sender, EventArgs e)
        {
            int cont = dbVenda.SelectedRows.Count;


            if (cont > 0)
            {
                var x = (dbVenda.Rows[dbVenda.CurrentRow.Index].Cells[4].Value).ToString();
                float t = (float.Parse(txtVtotal.Text) - float.Parse(x));


                dbVenda.Rows.RemoveAt(dbVenda.CurrentRow.Index);
                cont--;
                txtVtotal.Text = t.ToString();
            }
        }

        private void Pesquisa_Click(object sender, EventArgs e)
        {
            ViewPesquisa pesquisa = new ViewPesquisa();
            pesquisa.ShowDialog();

            if (pesquisa.DialogResult == DialogResult.OK)
            {
                txtCodigo.Text = pesquisa.dbPesquisa.Rows[pesquisa.dbPesquisa.CurrentRow.Index].Cells[0].Value.ToString();
                txtDescricao.Text = pesquisa.dbPesquisa.Rows[pesquisa.dbPesquisa.CurrentRow.Index].Cells[1].Value.ToString();
                txtUnit.Text = pesquisa.dbPesquisa.Rows[pesquisa.dbPesquisa.CurrentRow.Index].Cells[2].Value.ToString();

            }
            txtQuantidade.Enabled = true;
            txtQuantidade.Focus();
        }


        public void dataHora()
        {
            lbData.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void label10_MouseClick(object sender, MouseEventArgs e)
        {
            txtDinheiro.Enabled = true;
        }

        private void label12_Click(object sender, EventArgs e)
        {
            txtCartao.Enabled = true;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            panelRepresentante.Visible = true;
        }

        public void EscolhePagamento()
        {


            float total2 = float.Parse(txtVtotal2.Text);
            float fiado = float.Parse(txtFiado.Text);
            float dinheiro = float.Parse(txtDinheiro.Text);
            float valorTotal = float.Parse(txtVtotal2.Text);
            float cartao = float.Parse(txtCartao.Text);
            total = float.Parse(txtVtotal.Text);



            if (txtFiado.Text != "0,00" && txtCliente.Equals(null))
            {
                MessageBox.Show("Campo de data Vazio!");
            }
            else if (txtFiado.Text != "0,00" && txtCliente.Text != "")
            {
                pagamento();
            }
            else if (dinheiro == valorTotal && txtFiado.Equals("0,00") || txtCartao.Equals("0,00"))//dinheiro igual a total
            {
                pagamento();
                venda.SelectedTab = tabPage1;
                txtCodigo.Focus();

            }
            else if (fiado == valorTotal && txtDinheiro.Equals("0,00") || txtCartao.Equals("0,00"))//dinheiro igual a total
            {
                if (txtCliente.Text.Equals("") || txtRetirada.Equals("__/__/____") || txtEntrega.Equals("__/__/____"))
                {
                    util.MessageErro("Por favor preencha os campos do cliente!");
                }
                else
                {
                    pagamento();
                    venda.SelectedTab = tabPage1;
                    txtCodigo.Focus();
                }

            }
            else if (cartao == valorTotal && txtFiado.Equals("0,00") || txtDinheiro.Equals("0,00"))//dinheiro igual a total
            {
                pagamento();
                venda.SelectedTab = tabPage1;
                txtCodigo.Focus();
            }
            else if (fiado == valorTotal && txtCliente.Equals(""))//dinheiro igual a total
            {
                MessageBox.Show("Escolha o cliente");
            }
            else if (fiado == valorTotal && txtCliente.Text != "")//dinheiro igual a total
            {
                pagamento();
                venda.SelectedTab = tabPage1;
                txtCodigo.Focus();
            }
            else if (cartao > valorTotal)//dinheiro igual a total
            {
                util.MessageCartao();
                txtPag.Focus();
                txtCartao.Text = null;
                txtCartao.Text = "0,00";
            }
            else if (fiado > valorTotal)//dinheiro igual a total
            {
                util.MessageCartao();
                txtPag.Focus();
                txtFiado.Text = null;
                txtFiado.Text = "0,00";
            }
            else if (dinheiro > valorTotal)//dinheiro igual a total
            {
                txtToco.Text = (dinheiro - valorTotal).ToString();
                util.MessageTroco(float.Parse(txtToco.Text));
                pagamento();
                venda.SelectedTab = tabPage1;

            }
            else if (dinheiro != float.Parse("0,00") && dinheiro < valorTotal)//dinheiro igual a total
            {
                txtVtotal2.Text = (valorTotal - dinheiro).ToString();
                txtPag.Focus();
            }
            else if (cartao != float.Parse("0,00") && cartao < valorTotal)//dinheiro igual a total
            {
                txtVtotal2.Text = (valorTotal - cartao).ToString();
                txtPag.Focus();
            }
            else if (fiado != float.Parse("0,00") && fiado < valorTotal)//dinheiro igual a total
            {
                txtVtotal2.Text = (valorTotal - fiado).ToString();
                txtPag.Focus();
            }
            else if (dinheiro == valorTotal)//dinheiro igual a total
            {
                pagamento();
                venda.SelectedTab = tabPage1;
                txtCodigo.Focus();

            }
            else if (cartao == valorTotal)//dinheiro igual a total
            {
                pagamento();
                venda.SelectedTab = tabPage1;
                txtCodigo.Focus();

            }
            else if (fiado == valorTotal)//dinheiro igual a total
            {
                pagamento();
                venda.SelectedTab = tabPage1;
                txtCodigo.Focus();

            }
        }

        private void txtDinheiro_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                float m = (float.Parse(txtVtotal2.Text)) - (float.Parse(txtDinheiro.Text));
                txtVtotal2.Text = m.ToString();
                txtPag.Focus();
            }
        }

        private void txtPag_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPag_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtPag_KeyDown(object sender, KeyEventArgs e)
        {
            float total = float.Parse(txtVtotal.Text);
            float dinheiro = float.Parse(txtDinheiro.Text);
            float cartao = float.Parse(txtCartao.Text);
            float fiado = float.Parse(txtFiado.Text);
            switch (e.KeyCode)
            {
                case Keys.D:
                    txtDinheiro.Enabled = true;
                    txtDinheiro.Focus();
                    txtPag.Text = null;
                    break;

                case Keys.C:
                    txtCartao.Enabled = true;
                    txtCartao.Focus();
                    txtPag.Text = null;
                    break;

                case Keys.F:
                    txtFiado.Enabled = true;
                    txtFiado.Focus();
                    txtPag.Text = null;
                    break;
                case Keys.P:
                    

                    if (fiado != 0)
                    {
                        if (txtCliente.Text.Equals("")||txtEntrega.Text.Equals("")||txtRetirada.Text.Equals(""))
                        { 
                            txtCliente.Focus();
                        }
                        else
                        {
                            pagamento();
                        }
                    }
                    else if (dinheiro > (total-fiado))
                    {
                        util.MessageTroco(dinheiro-(total-fiado));
                        pagamento();
                    }
                    else if (dinheiro > (total - cartao)) 
                    {
                        util.MessageTroco(dinheiro - (total - cartao));
                        pagamento();
                    }
                    else
                    {
                        pagamento();
                    }
                    break;

            }
        }

        private void txtCartao_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                float m = (float.Parse(txtVtotal2.Text)) - (float.Parse(txtCartao.Text));
                txtVtotal2.Text = m.ToString();
                txtPag.Focus();
            }
        }

        private void txtFiado_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                float m = (float.Parse(txtVtotal2.Text)) - (float.Parse(txtFiado.Text));
                txtVtotal2.Text = m.ToString();
                txtPag.Focus();
            }
        }
    }
}
