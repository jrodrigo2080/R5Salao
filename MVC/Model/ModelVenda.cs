
using System;

namespace R5SALAO.MVC.Model
{
    // [SQLite.Table("TBVENDA")]
    public class ModelVenda
    {
        //   [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public int codigo { get; set; }
        public int cliente { get; set; }
        public int codigoProduto { get; set; }
        public int descricao { get; set; }
        public double quantidade { get; set; }
        public Double valorUnit { get; set; }
        public double valorTotal { get; set; }
        public double valorVenda { get; set; }
        public int status { get; set; }
        public DateTime dataRetirada { get; set; }
        public DateTime dataEntrega { get; set; }
    }
}
