using R5SALAO.MVC.Model;
using System;
using System.Data.SQLite;

namespace R5SALAO.MVC.DAO
{
    class DAOGRUPO
    {
        DAOConexao conexao = new DAOConexao();
        ModelTipo user = new ModelTipo();
        DAOUtil util = new DAOUtil();
        string sql;
        SQLiteCommand comando = new SQLiteCommand();
        public void Save(ModelGrupo model)
        {
            try
            {
                conexao.Conectar();
                sql = "INSERT INTO TBgrupo(nome)VALUES(@nome)";
                comando = new SQLiteCommand(sql, conexao.con);

                comando.Parameters.AddWithValue("@nome", model.NOME);

                comando.ExecuteNonQuery();
                util.MessageSave();
            }
            catch (Exception ex)
            {
                util.Log("Erro ao salvar  >" + ex.Message);
                util.MessageError(ex);
            }
            finally
            {
                conexao.Desconectar();
            }
        }


        //metodo para atualizar senha ou nome do usuário
        public void Update(ModelGrupo model)
        {
            try
            {
                conexao.Conectar();
                sql = "UPDATE TBgrupo SET nome=@nome where id=@id";
                comando = new SQLiteCommand(sql, conexao.con);

                comando.Parameters.AddWithValue("@nome", model.NOME);
                comando.Parameters.AddWithValue("@id", model.ID);

                comando.ExecuteNonQuery();
                util.MessageSave();
            }
            catch (Exception ex)
            {
                util.Log("erro ao alterar usuário >" + ex.Message);
            }
        }

        public void Delete(int id)
        {
            try
            {
                conexao.Conectar();
                sql = "DELETE FROM TBgrupo WHERE id=" + id + "";
                comando = new SQLiteCommand(sql, conexao.con);
                comando.ExecuteNonQuery();
                util.MessageDelete();
            }
            catch (Exception ex)
            {
                util.MessageError(ex);
                util.Log("erro ao deletar: " + ex.Message);
            }

        }

    }
}

