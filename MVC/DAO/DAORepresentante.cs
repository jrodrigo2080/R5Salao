using R5SALAO.MVC.Model;
using System;
using System.Data.SQLite;

namespace R5SALAO.MVC.DAO
{
    class DAORepresentante
    {
        DAOConexao conexao = new DAOConexao();
        DAOUtil util = new DAOUtil();
        SQLiteCommand comando = new SQLiteCommand();
        string sql;

        public void Insert(ModelRepresantante model)
        {
            try
            {
                conexao.Conectar();
                sql = "insert into tbrepresentante(nome,telefone,cpf,aniversario,email,cep,endereco,numero,cidade,bairro,observacao)" +
                                  "values(@nome,@telefone,@cpf,@aniversario,@email,@cep,@endereco,@numero,@cidade,@bairro,@observacao)";
                comando = new SQLiteCommand(sql, conexao.con);

                comando.Parameters.AddWithValue("@nome", model.nome);
                comando.Parameters.AddWithValue("@telefone", model.telefone);
                comando.Parameters.AddWithValue("@cpf", model.cpf);
                comando.Parameters.AddWithValue("@aniversario", model.aniversario);
                comando.Parameters.AddWithValue("@email", model.email);
                comando.Parameters.AddWithValue("@cep", model.cep);
                comando.Parameters.AddWithValue("@endereco", model.endereco);
                comando.Parameters.AddWithValue("@numero", model.numero);
                comando.Parameters.AddWithValue("@cidade", model.cidade);
                comando.Parameters.AddWithValue("@bairro", model.bairro);
                comando.Parameters.AddWithValue("@observacao", model.observacao);


                comando.ExecuteNonQuery();
                util.MessageSave();
            }
            catch (Exception ex)
            {
                util.MessageError(ex);
                util.Log(ex.Message);
            }
        }

        public void Update(ModelRepresantante model)
        {
            conexao.Conectar();
            sql = "update tbrepresentante set nome=@nome,telefone=@telefone,cpf=@cpf,aniversario=@aniversario,email=@email,cep=@cep,endereco=@endereco,numero=@numero,cidade=@cidade,bairro=@bairro,observacao=@observacao where id=@id ";
            comando = new SQLiteCommand(sql, conexao.con);

            comando.Parameters.AddWithValue("@nome", model.nome);
            comando.Parameters.AddWithValue("@telefone", model.telefone);
            comando.Parameters.AddWithValue("@cpf", model.cpf);
            comando.Parameters.AddWithValue("@aniversario", model.aniversario);
            comando.Parameters.AddWithValue("@email", model.email);
            comando.Parameters.AddWithValue("@cep", model.cep);
            comando.Parameters.AddWithValue("@endereco", model.endereco);
            comando.Parameters.AddWithValue("@numero", model.numero);
            comando.Parameters.AddWithValue("@cidade", model.cidade);
            comando.Parameters.AddWithValue("@bairro", model.bairro);
            comando.Parameters.AddWithValue("@observacao", model.observacao);
            comando.Parameters.AddWithValue("@id", model.id);

            try
            {
                comando.ExecuteNonQuery();
                util.MessageUpdate();
            }
            catch (Exception ex)
            {
                util.MessageError(ex);
                util.Log(ex.Message);
            }
        }


        public void Delete(int id)
        {
            conexao.Conectar();
            sql = "delete from tbrepresentante where id='" + id + "'";
            try
            {
                comando = new SQLiteCommand(sql, conexao.con);
                comando.ExecuteNonQuery();
                util.MessageDelete();
            }
            catch (Exception ex)
            {
                util.MessageError(ex);
                util.Log(ex.Message);
            }
        }
    }
}
