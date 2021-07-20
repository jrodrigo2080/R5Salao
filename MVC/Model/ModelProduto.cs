namespace R5SALAO.MVC.Model
{

    public class ModelProduto
    {
        public int id { get; set; }
        public int codigo { get; set; }
        public string NOME { get; set; }
        public float ESTOQUEATUAL { get; set; }
        public float ESTOQUEMINIMO { get; set; }
        public string GRUPO { get; set; }
        public float COMPRA { get; set; }
        public float VENDA { get; set; }
        public int STATUS { get; set; }

    }
}
