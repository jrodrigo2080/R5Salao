using R5SALAO.MVC.DAO;
using System;
using System.Data.SQLite;
using System.Windows.Forms;

namespace R5SALAO.MVC.View
{
    public partial class ViewPagamento : Form
    {
        string data, hora, v;
        int codigo;
        public ViewPagamento(string valor, int cod, string dat, string hor)
        {
            InitializeComponent();
            txtDinheiro.Text = "0,00";
            txtFiado.Text = "0,00";
            txtCartao.Text = "0,00";
            txtTotal.Text = valor;
            codigo = cod;
            data = dat;
            hora = hor;
            v = valor;
        }

        DAOUtil util = new DAOUtil();
        ViewPdv pdv = new ViewPdv();
        DAOConexao conexao = new DAOConexao();
        string sql;
        SQLiteCommand comando;
        DAOVenda venda = new DAOVenda();





        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        public void verificaPagamento()
        {
            conexao.Desconectar();
            float dinheiro = float.Parse(txtDinheiro.Text);
            float cartao = float.Parse(txtCartao.Text);
            float fiado = float.Parse(txtFiado.Text);
            float troco = 0;
            float total = float.Parse(txtTotal.Text);
            try
            {
                if (dinheiro == total)
                {
                    pdv.inserirVenda();
                }
            }
            catch (Exception ex)
            {

            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtDinheiro.Enabled = true;
            txtDinheiro.Focus();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            verificaPagamento();
        }

        private void txtDinheiro_KeyPress(object sender, KeyPressEventArgs e)
        {
            util.ApenasNumber(sender, e);
            if (e.KeyChar == 13)
                verificaPagamento();

        }

        public string ValorTotal { get; set; }

        private void ViewPagamento_Load(object sender, EventArgs e)
        {

        }
    }
}
