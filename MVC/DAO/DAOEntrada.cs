﻿using System;
using System.Data.SQLite;

namespace R5SALAO.MVC.DAO
{
    class DAOEntrada
    {
        DAOConexao conexao = new DAOConexao();
        DAOUtil util = new DAOUtil();
        SQLiteCommand comando;
        string sql;
        public void SalvaEntrada(string codigo, string nome, string atual, string entrada, string data)
        {
            conexao.Conectar();
            sql = "insert into TBENTRADA(CODIGO,DESCRICAO,QTDATUAL,QTDENTRADA,DATAENTRADA)values(@CODIGO,@DESCRICAO,@QTDATUAL,@QTDENTRADA,@DATAENTRADA)";
            comando = new SQLiteCommand(sql, conexao.con);

            comando.Parameters.AddWithValue("@codigo", codigo);
            comando.Parameters.AddWithValue("@descricao", nome);
            comando.Parameters.AddWithValue("@qtdatual", atual);
            comando.Parameters.AddWithValue("@qtdentrada", entrada);
            comando.Parameters.AddWithValue("@dataentrada", data);

            try
            {
                comando.ExecuteNonQuery();
                util.MessageSave();
            }
            catch (Exception ex)
            {

                util.Log(ex.Message);
            }
        }

        public void AtualizaEntrada(string codigo, string qtd)
        {
            conexao.Conectar();
            sql = "update tbproduto set estoqueatual=@estoque where codigo=@codigo";
            comando = new SQLiteCommand(sql, conexao.con);

            comando.Parameters.AddWithValue("@estoque", qtd);
            comando.Parameters.AddWithValue("@codigo", codigo);

            try
            {
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                util.Log(ex.Message);
            }
        }

    }
}