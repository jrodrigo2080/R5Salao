using R5SALAO.MVC.Model;
using System;
using System.Data.SQLite;
using System.Windows.Forms;

namespace R5SALAO.MVC.DAO
{
    class DAOUsuario
    {

        DAOConexao conexao = new DAOConexao();
        DAOUtil util = new DAOUtil();
        SQLiteCommand comando = new SQLiteCommand();
        string sql;
        public void Save(ModelUsuario model)
        {

            try
            {
                conexao.Conectar();
                sql = "INSERT into TBUSUARIO(nome,senha)VALUES(@nome,@senha)";
                comando = new SQLiteCommand(sql, conexao.con);

                comando.Parameters.AddWithValue("@nome", model.NOME);
                comando.Parameters.AddWithValue("@senha", model.senha);

                comando.ExecuteNonQuery();
                util.MessageSave();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                util.Log(ex.Message);
            }
            finally
            {
                conexao.Desconectar();
            }
        }
        //metodo para atualizar senha ou nome do usuário
        public void Update(ModelUsuario model)
        {
            try
            {

                sql = "UPDATE TBUSUARIO SET nome=@nome,senha=@senha where id='" + model.ID + "'";
                comando = new SQLiteCommand(sql, conexao.con);

                comando.Parameters.AddWithValue("@nome", model.NOME);
                comando.Parameters.AddWithValue("@senha", model.senha);

                conexao.Conectar();
                comando.ExecuteNonQuery();
                util.MessageUpdate();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                util.Log(ex.Message);
            }
        }

        public void Delete(int id)
        {
            try
            {
                sql = "delete from TBUSUARIO where id ='" + id + "'";
                comando = new SQLiteCommand(sql, conexao.con);
                conexao.Conectar();
                comando.ExecuteNonQuery();
                util.MessageDelete();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                util.Log(ex.Message);
            }

        }

    }
}
