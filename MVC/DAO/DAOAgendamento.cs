using R5SALAO.MVC.Model;
using System;
using System.Data.SQLite;

namespace R5SALAO.MVC.DAO
{
    class DAOAgendamento
    {
        DAOConexao conexao = new DAOConexao();
        string sql;
        SQLiteCommand comando = new SQLiteCommand();
        DAOUtil util = new DAOUtil();

        public void Save(ModelAgendamento model)
        {
            try
            {
                conexao.Conectar();
                sql = "insert into TBAGENDAMENTO(telefone,nome,servico,observacao,status,data,hora,valor)VALUES(@telefone,@nome,@servico,@observacao,@status,@data,@hora,@valor)";
                comando = new SQLiteCommand(sql, conexao.con);

                comando.Parameters.AddWithValue("@nome", model.cliente);
                comando.Parameters.AddWithValue("@telefone", model.telefone);
                comando.Parameters.AddWithValue("@servico", model.servico);
                comando.Parameters.AddWithValue("@observacao", model.observacao);
                comando.Parameters.AddWithValue("@status", model.status);
                comando.Parameters.AddWithValue("@data", model.data);
                comando.Parameters.AddWithValue("@hora", model.hora);
                comando.Parameters.AddWithValue("@valor", model.valor);

                comando.ExecuteNonQuery();
                util.MessageSave();
            }
            catch (Exception ex)
            {
                util.Log("Erro ao  salvar >" + ex.Message);
            }
            finally
            {
                conexao.Desconectar();
            }
        }


        public void Update(ModelAgendamento model)
        {
            try
            {
                conexao.Conectar();
                sql = "UPDATE TBAGENDAMENTO SET telefone=@telefone,nome=@nome,servico=@servico,observacao=@observacao,status=@status,data=@data,hora=@hora,valor=@valor where id=@id";
                comando = new SQLiteCommand(sql, conexao.con);

                comando.Parameters.AddWithValue("@nome", model.cliente);
                comando.Parameters.AddWithValue("@telefone", model.telefone);
                comando.Parameters.AddWithValue("@servico", model.servico);
                comando.Parameters.AddWithValue("@observacao", model.observacao);
                comando.Parameters.AddWithValue("@status", model.status);
                comando.Parameters.AddWithValue("@data", model.data);
                comando.Parameters.AddWithValue("@hora", model.hora);
                comando.Parameters.AddWithValue("@valor", model.valor);
                comando.Parameters.AddWithValue("@id", model.id);

                comando.ExecuteNonQuery();
                util.MessageUpdate();
            }
            catch (Exception ex)
            {
                util.Log("Erro ao  ALTERAR >" + ex.Message);
            }
            finally
            {
                conexao.Desconectar();
            }
        }


        public void Delete(int id)
        {
            try
            {
                sql = "DELETE FROM TBAGENDAMENTO WHERE id=" + id + "";
                conexao.Conectar();
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
