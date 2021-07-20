using R5SALAO.MVC.Model;
using System;
using System.Data.SQLite;

namespace R5SALAO.MVC.DAO
{
    class DAOServico
    {

        DAOConexao conexao = new DAOConexao();
        string sql;
        DAOUtil util = new DAOUtil();
        SQLiteCommand comando = new SQLiteCommand();
        public void Save(ModelServico model)
        {
            try
            {
                conexao.Conectar();
                sql = "INSERT INTO TBSERVICO(NOME,CUSTO,TIPO,VALOR,TEMPO)VALUES(@NOME,@CUSTO,@TIPO,@VALOR,@TEMPO)";
                comando = new SQLiteCommand(sql, conexao.con);

                comando.Parameters.AddWithValue("@NOME", model.NOME);
                comando.Parameters.AddWithValue("@CUSTO", model.CUSTO);
                comando.Parameters.AddWithValue("@TIPO", model.TIPO);
                comando.Parameters.AddWithValue("@TEMPO", model.TEMPO);
                comando.Parameters.AddWithValue("@VALOR", model.VALOR);

                comando.ExecuteNonQuery();
                util.MessageSave();
            }
            catch (Exception ex)
            {
                util.Log("Erro ao salvar usuario >" + ex.Message);
            }
            finally
            {
                conexao.Desconectar();
            }
        }


        public void Update(ModelServico model)
        {
            try
            {
                conexao.Conectar();
                sql = "UPDATE TBSERVICO SET NOME=@NOME,CUSTO=@CUSTO,TIPO=@TIPO,TEMPO=@TEMPO,VALOR=@VALOR WHERE ID=@ID";
                comando = new SQLiteCommand(sql, conexao.con);

                comando.Parameters.AddWithValue("@NOME", model.NOME);
                comando.Parameters.AddWithValue("@CUSTO", model.CUSTO);
                comando.Parameters.AddWithValue("@TIPO", model.TIPO);
                comando.Parameters.AddWithValue("@TEMPO", model.TEMPO);
                comando.Parameters.AddWithValue("@VALOR", model.VALOR);
                comando.Parameters.AddWithValue("@ID", model.ID);

                comando.ExecuteNonQuery();
                util.MessageSave();
            }
            catch (Exception ex)
            {
                util.Log("erro " + ex.Message);
                // MessageBox.Show(ex.Message.ToString(), "Erro ao Salvar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                conexao.Desconectar();
            }
        }


        public void DeleteCliente(int id)
        {
            try
            {
                conexao.Conectar();
                sql = "DELETE FROM TBSERVICO WHERE id=" + id + "";
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
