namespace R5SALAO.MVC.Model
{
    public class ModelAgendamento
    {
        public int id { get; set; }
        public string cliente { get; set; }
        public string telefone { get; set; }
        public string servico { get; set; }
        public string observacao { get; set; }
        public string status { get; set; }
        public float valor { get; set; }
        public string data { get; set; }
        public string hora { get; set; }
    }
}
