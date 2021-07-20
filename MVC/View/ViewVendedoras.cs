using R5SALAO.MVC.DAO;
using System;
using System.Data.SQLite;
using System.Windows.Forms;

namespace R5SALAO.MVC.View
{
    public partial class ViewVendedoras : Form
    {
        DAOUtil util = new DAOUtil();
        DAOConexao conexao = new DAOConexao();
        SQLiteCommand comando = new SQLiteCommand();

        public ViewVendedoras()
        {
            InitializeComponent();
            util.carregaComboBox("select * from tbrepresentante", cbVendedor);
            cbSelecione.SelectedIndex=0;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            util.CarregarTabela("SELECT N.CODIGO,N.cliente, N.valorTotal, Z.pagfiado,N.DataVenda ,N.DataRetirada,N.DataEntrega FROM TBVENDAITENS N,TBVENDA Z WHERE Z.codigo = N.codigo AND N.cliente = '"+cbVendedor.Text+"' AND Z.pagfiado <> '0,00' GROUP BY N.codigo", dbVenda);
            //util.CarregarTabela("SELECT N.cliente,N.descricao,N.quantidade,N.valorTotal,N.DataRetirada,N.DataEntrega,Z.pagfiadoZ.pagfiado FROM TBVENDAITENS N,TBVENDA Z WHERE N.cliente='" + cbVendedor.Text + "' AND Z.pagfiado<>'0,00'", dbVenda);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dbVenda_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int codigo = Convert.ToInt32(this.dbVenda.CurrentRow.Cells[0].Value.ToString());
            txtPendente.Text = this.dbVenda.CurrentRow.Cells[3].Value.ToString();
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            conexao.Conectar();
            string pag = "";

            if (cbSelecione.Text == "DINHEIRO")
            {
                pag = "pagdinheiro";
            }else if(cbSelecione.Text == "CARTAO")
            {
                pag = "pagcartao";
            }
            else
            {
                util.MessageErro("SELECIONE UMA OPÇÃO VÁLIDA!");
            }


            string sql = "UPDATE TBVENDA SET "+pag+"='"+txtPago.Text+"";


        }
    }
}
