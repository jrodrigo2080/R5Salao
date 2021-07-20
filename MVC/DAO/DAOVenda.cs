using System;
using System.Data.SQLite;

namespace R5SALAO.MVC.DAO
{
    class DAOVenda
    {
        DAOConexao conexao = new DAOConexao();
        string sql;
        DAOUtil util = new DAOUtil();
        SQLiteCommand comando = new SQLiteCommand();

        public void inserirVenda(float dinheiro, float cartao, float fiado, float troco, string codigo, float total, string data, string hora)
        {

            conexao.Conectar();
            sql = "insert into tbvenda(codigo,valorTotal,hora,data,pagdinheiro,pagcartao,pagfiado,troco) values " +
                            "(@codigo,@valorTotal,@hora,@data,@pagdinheiro,@pagcartao,@pagfiado,@troco)";
            comando = new SQLiteCommand(sql, conexao.con);

            comando.Parameters.AddWithValue("@codigo", codigo);
            comando.Parameters.AddWithValue("@valorTotal", total);
            comando.Parameters.AddWithValue("@data", data);
            comando.Parameters.AddWithValue("@hora", hora);
            comando.Parameters.AddWithValue("@pagdinheiro", dinheiro);
            comando.Parameters.AddWithValue("@pagcartao", cartao);
            comando.Parameters.AddWithValue("@pagfiado", fiado);
            comando.Parameters.AddWithValue("@troco", troco);
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


        /**
                public List<ModelVenda> Select(string select = "Select * from TBVENDA", params object[] parameters)
                {
                    List<ModelVenda> Data = new List<ModelVenda>();
                    try
                    {
                        conexao.Conectar();
                        if (!(parameters is null))
                            Data = conexao.conexao.Query<ModelVenda>(select, parameters);
                        else
                            Data = conexao.conexao.Query<ModelVenda>(select);

                    }
                    catch
                    {

                    }
                    finally
                    {
                        conexao.Desconectar();
                    }
                    return Data;
                }

                public void Save(ModelVenda model)
                {
                    try
                    {
                        conexao.Conectar();
                        conexao.conexao.Insert(model);
                    }
                    catch (Exception ex)
                    {
                        conexao.Log("Erro ao salvar  >" + ex.Message);
                    }
                    finally
                    {
                        conexao.Desconectar();
                    }
                }


                public void Update(ModelVenda model)
                {
                    try
                    {
                        conexao.Conectar();
                        conexao.conexao.Update(model);
                        conexao.Log("Alterado com sucesso!");

                    }
                    catch (Exception ex)
                    {
                        conexao.Log("erro " + ex.Message);
                        // MessageBox.Show(ex.Message.ToString(), "Erro ao Salvar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                        sql = "DELETE FROM TBVENDA WHERE id=" + id + "";
                        conexao.Conectar();
                        conexao.conexao.Query<ModelProduto>(sql);
                    }
                    catch (Exception ex)
                    {
                        util.MessageError(ex);
                        conexao.Log("erro ao deletar: " + ex.Message);
                    }

                }**/
    }
}
