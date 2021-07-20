using R5SALAO.MVC.Model;
using System;
using System.Data.SQLite;
using System.Windows.Forms;

namespace R5SALAO.MVC.DAO
{
    public class DAOCliente
    {
        DAOConexao conexao = new DAOConexao();
        DAOUtil util = new DAOUtil();
        SQLiteCommand comando = new SQLiteCommand();
        string sql;

        public void SaveCliente(ModelCliente model)
        {
            try
            {
                conexao.Conectar();
                sql = "INSERT INTO TBCLIENTE(nome,telefone,cpf,aniversario,email,cep,endereco,numero,bairro,cidade,observacao)VALUES(@nome,@telefone,@cpf,@aniversario,@email,@cep,@endereco,@numero,@bairro,@cidade,@observacao)";
                comando = new SQLiteCommand(sql, conexao.con);

                comando.Parameters.AddWithValue("@nome", model.NOME);
                comando.Parameters.AddWithValue("@telefone", model.TELEFONE);
                comando.Parameters.AddWithValue("@cpf", model.CPF);
                comando.Parameters.AddWithValue("@aniversario", model.aniversario);
                comando.Parameters.AddWithValue("@email", model.email);
                comando.Parameters.AddWithValue("@cep", model.cep);
                comando.Parameters.AddWithValue("@endereco", model.endereco);
                comando.Parameters.AddWithValue("@numero", model.numero);
                comando.Parameters.AddWithValue("@bairro", model.bairro);
                comando.Parameters.AddWithValue("@cidade", model.cidade);
                comando.Parameters.AddWithValue("@observacao", model.observacao);


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


        public void UpdateCliente(ModelCliente model)
        {
            try
            {
                conexao.Conectar();
                sql = "UPDATE TBCLIENTE SET nome=@nome," +
                    "telefone=@telefone," +
                    "cpf=@cpf," +
                    "aniversario=@aniversario," +
                    "email=@email," +
                    "cep=@cep," +
                    "endereco=@endereco," +
                    "numero=@numero," +
                    "bairro=@bairro," +
                    "cidade=@cidade," +
                    "observacao=@observacao where id=@id";

                comando = new SQLiteCommand(sql, conexao.con);
                int id = model.Id;
                comando.Parameters.AddWithValue("@nome", model.NOME);
                comando.Parameters.AddWithValue("@telefone", model.TELEFONE);
                comando.Parameters.AddWithValue("@cpf", model.CPF);
                comando.Parameters.AddWithValue("@aniversario", model.aniversario);
                comando.Parameters.AddWithValue("@email", model.email);
                comando.Parameters.AddWithValue("@cep", model.cep);
                comando.Parameters.AddWithValue("@endereco", model.endereco);
                comando.Parameters.AddWithValue("@numero", model.numero);
                comando.Parameters.AddWithValue("@bairro", model.bairro);
                comando.Parameters.AddWithValue("@cidade", model.cidade);
                comando.Parameters.AddWithValue("@observacao", model.observacao);
                comando.Parameters.AddWithValue("@id", id);

                comando.ExecuteNonQuery();
                util.MessageUpdate();

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


        public void DeleteCliente(int id)
        {
            try
            {
                sql = "DELETE FROM TBCLIENTE WHERE id=" + id + "";
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
