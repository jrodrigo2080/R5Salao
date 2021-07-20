using R5SALAO.MVC.DAO;
using R5SALAO.MVC.Model;
using R5SALAO.MVC.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace R5SALAO.MVC.View
{
    public partial class ViewCadastroUsuario : Form
    {
        DAOUsuario dao = new DAOUsuario();
        ModelUsuario user = new ModelUsuario();
        DAOConnection conexao = new DAOConnection();
        DAOUtil util = new DAOUtil();
        
        string sql;
        public ViewCadastroUsuario()
        {
            InitializeComponent();
            carregarDados();
        }
        //botão salvar
        private void button1_Click(object sender, EventArgs e)
        {
            user.nome = txtNome.Text;
            user.senha = txtSenha.Text;
            if (txtSenha.Text.Equals(txtConfSenha.Text))
            {
                try
                {
                    dao.SaveUser(user);
                    util.MessageSave();
                }catch(Exception ex)
                {
                    util.MessageError(ex);
                }
            }
            else
            {
                MessageBox.Show("Senhas diferentes","Verifique a senha por favor!",MessageBoxButtons.OK,MessageBoxIcon.Question);
            }
        }
        //botão salvar
        private void button5_Click(object sender, EventArgs e)
        {
            //dao.UpdateUser();
        }

        //metodo para carregar os dados no grid
        public void carregarDados()
        {
            
            sql = "select id,nome from TBUSUARIO";
            conexao.Conectar();
            List<ModelUsuario> list = conexao.conexao.Query<ModelUsuario>(sql);
            
            DataTable dt = new DataTable();
            dbUser.DataSource = dt;

        }
    }
}
