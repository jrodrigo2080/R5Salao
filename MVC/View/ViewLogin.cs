using R5SALAO.MVC.DAO;
using R5SALAO.MVC.Util;
using R5SALAO.MVC.View;
using System;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;

namespace R5SALAO
{
    public partial class Form1 : Form
    {

        DataTable dt = new DataTable();
        string sql;
        public Form1()
        {
            InitializeComponent();
        }
        Util util = new Util();
        DAOConexao conexao = new DAOConexao();
        public static string nome = "";
        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtLogin.Text == "" || txtSenha.Text == "")
            {
                util.Log("Usuario invalido");
                MessageBox.Show("Usuario invalido");
            }
            else
            {
                try
                {
                    conexao.Conectar();
                    sql = "select * from TBUSUARIO where nome='" + txtSenha.Text + "'and senha ='" + txtSenha.Text + "'";
                    SQLiteDataAdapter dados = new SQLiteDataAdapter(sql, conexao.con);
                    DataTable usuario = new DataTable();
                    dados.Fill(usuario);

                    if (usuario.Rows.Count < 0)
                    {
                        MessageBox.Show("Usuario invalido");
                        txtLogin.Text = null;
                        txtSenha.Text = null;

                    }
                    else
                    {

                        MDIPrincipal view = new MDIPrincipal();
                        view.Show();
                        this.Visible = false;
                    }

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

        }
        private void eventLog1_EntryWritten(object sender, System.Diagnostics.EntryWrittenEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtLogin_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            Close();
        }
    }
}
