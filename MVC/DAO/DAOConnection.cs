
using System;
using System.Data.SQLite;
using System.IO;
using System.Text;

namespace R5SALAO.MVC.DAO
{
    public class DAOConnection
    {
        public DAOConnection() { }

        public SQLiteConnection conexao;

        public void Conectar()
        {
            String texto = File.ReadAllText("\\R5SOFT\\R5SALAO\\CONFIG.txt");
            conexao = new SQLiteConnection(texto);
        }

        public void Desconectar()
        {
            //conexao.Close();
        }
        public void Dispose()
        {
            // conexao.Dispose();
        }


        public void Log(string msg)
        {
            var caminho = @"\R5SOFT\LOG\log-" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
            StreamWriter file = new StreamWriter(caminho, true, Encoding.Default);
            file.WriteLine(DateTime.Now + " > " + msg);
            file.Dispose();
        }

    }
}
