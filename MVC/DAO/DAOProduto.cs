using R5SALAO.MVC.Model;

using System;
using System.Data.SQLite;
using System.Windows.Forms;

namespace R5SALAO.MVC.DAO
{
    class DAOProduto
    {
        DAOConexao conexao = new DAOConexao();
        DAOUtil util = new DAOUtil();
        SQLiteCommand comando = new SQLiteCommand();
        string sql;

        public void Insert(ModelProduto model)
        {
            try
            {
                conexao.Conectar();
                sql = "INSERT INTO TBPRODUTO(codigo,nome,estoqueatual,estoqueminimo,venda,compra,status,GRUPO) VALUES (@codigo,@nome,@estoqueatual,@estoqueminimo,@venda,@compra,@status,@GRUPO)";

                comando = new SQLiteCommand(sql, conexao.con);

                comando.Parameters.AddWithValue("@codigo", model.codigo);
                comando.Parameters.AddWithValue("@nome", model.NOME);
                comando.Parameters.AddWithValue("@estoqueminimo", model.ESTOQUEMINIMO);
                comando.Parameters.AddWithValue("@estoqueatual", model.ESTOQUEATUAL);
                comando.Parameters.AddWithValue("@GRUPO", model.GRUPO);
                comando.Parameters.AddWithValue("@venda", model.VENDA);
                comando.Parameters.AddWithValue("@compra", model.COMPRA);
                comando.Parameters.AddWithValue("@status", model.STATUS);

                comando.ExecuteNonQuery();
                util.Log("Produto salvo : " + model.NOME);
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


        public void Update(ModelProduto model)
        {
            try
            {
                conexao.Conectar();
                sql = "UPDATE TBPRODUTO SET codigo=@codigo,nome=@nome,estoqueatual=@estoqueatual,estoqueminimo=@estoqueminimo,venda=@venda,compra=@compra,status=@status,GRUPO=@GRUPO  where id = @id";

                comando = new SQLiteCommand(sql, conexao.con);

                comando.Parameters.AddWithValue("@codigo", model.codigo);
                comando.Parameters.AddWithValue("@nome", model.NOME);
                comando.Parameters.AddWithValue("@estoqueminimo", model.ESTOQUEMINIMO);
                comando.Parameters.AddWithValue("@estoqueatual", model.ESTOQUEATUAL);
                comando.Parameters.AddWithValue("@GRUPO", model.GRUPO);
                comando.Parameters.AddWithValue("@venda", model.VENDA);
                comando.Parameters.AddWithValue("@compra", model.COMPRA);
                comando.Parameters.AddWithValue("@status", model.STATUS);
                comando.Parameters.AddWithValue("@id", model.id);

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


        public void Delete(int id)
        {
            try
            {
                sql = "DELETE FROM TBPRODUTO WHERE id=" + id + "";
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
