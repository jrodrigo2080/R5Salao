using System;
using System.IO;
using System.Text;

namespace R5SALAO.MVC.Util
{
    public class Util
    {
        //metodo para gerar um log

        public void Log(string msg)
        {
            var caminho = @"\R5SOFT\LOG\log-" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
            StreamWriter file = new StreamWriter(caminho, true, Encoding.Default);
            file.WriteLine(DateTime.Now + " > " + msg);
            file.Dispose();
        }

    }
}
