using System;
using System.Data;
using System.Data.SQLite;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace R5SALAO.MVC.DAO
{
    public class DAOUtil
    {
        public static string version = "1.0";
        public static int nivel = 0;
        string sql;
        SQLiteCommand comando = new SQLiteCommand();
        DAOConexao conexao = new DAOConexao();

        
        //metodo universal para carregar tabela
        public void CarregarTabela(string script, DataGridView tabela)
        {
            sql = script;
            conexao.Conectar();
            comando = new SQLiteCommand(sql, conexao.con);
            try
            {
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(comando);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                tabela.DataSource = dt;

            }
            catch (Exception ex)
            {
                Log(ex.Message);
            }
            finally
            {
                conexao.Desconectar();
            }

        }

        //metodo de pesquisa por nome 
        public DataTable LocalizarNaTabela(string sql)
        {
            conexao.Conectar();
            DataTable table = new DataTable();
            SQLiteDataAdapter da = new SQLiteDataAdapter(sql, conexao.con);
            da.Fill(table);
            return table;
        }
        //metodo universal para carregar tabela
        public void CarregarDados(string sq, DataGridView db)
        {
            sql = sq;
            conexao.Conectar();
            comando = new SQLiteCommand(sql, conexao.con);

            try
            {
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(comando);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                db.DataSource = dt;
            }
            catch (Exception ex)
            {
                Log(ex.Message);
            }
            finally
            {
                conexao.Desconectar();
            }
        }

        //metodo para gerar LOG
        public void Log(string msg)
        {
            var caminho = @"\R5SOFT\LOG\log-" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
            StreamWriter file = new StreamWriter(caminho, true, Encoding.Default);
            file.WriteLine(DateTime.Now + " > " + msg);
            file.Dispose();
        }
        //metodo para carregar combobox
        public void carregaComboBox(string sq, ComboBox cb)
        {
            try
            {
                sql = sq;
                comando = new SQLiteCommand(sql, conexao.con);
                conexao.Conectar();
                DataSet dataset = new DataSet();
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(comando);
                adapter.Fill(dataset);

                cb.DataSource = dataset.Tables[0];
                cb.DisplayMember = "nome";
                cb.ValueMember = "id";

            }
            catch (Exception ex)
            {
                Log(ex.Message);
            }
        }
        //mesagem de salvar
        public void MessageSave()
        {
            MessageBox.Show("Salvo com Sucesso!", "Salvo com Sucesso!", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
        //mesagem de erro
        public void MessageErro(string msg)
        {
            MessageBox.Show(msg, "Erro ao Tentar Salvar!", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
        //mesagem de troco
        public void MessageTroco(float troco)
        {
            var x = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", troco);

            MessageBox.Show("Esse é o seu Troco " + x, "Troco!", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        }
        //transforma ponto em virgula
        public void TranformaVirgula(float x)
        {
            x = float.Parse(string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}"));
        }
        //converte em current
        public void convertDinheiro(float valor)
        {
            var x = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", valor);
        }

        public void MessageCartao()
        {
            MessageBox.Show("Você não pode receber um valor maior que a conta!", "Erro ao salvar o pagamento ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public void MessageUpdate()
        {
            MessageBox.Show("Alterado com Sucesso!", "Deletado com Sucesso!", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
        public void MessageError(Exception ex)
        {
            MessageBox.Show(ex.Message, "Erro : ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        public void MessageDelete()
        {
            MessageBox.Show("Deletado com Sucesso!", "Deletado com Sucesso!", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        public void MessageCampNull()
        {
            MessageBox.Show("Campo Vazio!", "Existe Campo vázio!", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        public void ApenasNumber(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != ','))
            {
                e.Handled = true;
                //MessageBox.Show("este campo aceita somente numero e virgula");
            }
            if ((e.KeyChar == ',') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
                // MessageBox.Show("este campo aceita somente uma virgula");
            }
        }

    }
}
