using System.Data;
using System.Data.SQLite;
using System.IO;

namespace R5SALAO.MVC.DAO
{
    public class DAOConexao
    {
        string texto = File.ReadAllText("\\R5SOFT\\R5SALAO\\CONFIG.txt");
        public SQLiteConnection con = new SQLiteConnection("DATA SOURCE = C:\\R5SOFT\\DADOS\\DADOs.db");
        public void Conectar()
        {
            if (con.State == ConnectionState.Closed)
                con.Open();

        }
        public void Desconectar()
        {
            con.Close();
        }

    }
}
