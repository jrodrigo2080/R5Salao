namespace R5SALAO.MVC.Model
{
    //  [SQLite.Table("TBCLIENTE")]
    public class ModelCliente
    {
        // [PrimaryKey , AutoIncrement]
        public int Id { get; set; }
        public string NOME { get; set; }
        public string CPF { get; set; }
        public string TELEFONE { get; set; }
        public string aniversario { get; set; }
        public string email { get; set; }
        public string cep { get; set; }
        public string endereco { get; set; }
        public string numero { get; set; }
        public string cidade { get; set; }
        public string bairro { get; set; }
        public string observacao { get; set; }

    }
}
