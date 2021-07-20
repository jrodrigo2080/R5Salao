namespace R5SALAO.MVC.Model
{
    //  [SQLite.Table("TBSERVICO")]
    public class ModelServico
    {
        //  [PrimaryKey,AutoIncrement]
        public int ID { get; set; }
        public string NOME { get; set; }
        public float CUSTO { get; set; }
        public string TIPO { get; set; }
        public float VALOR { get; set; }
        public string TEMPO { get; set; }
    }
}
