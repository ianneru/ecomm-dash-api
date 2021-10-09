namespace Pedidos.API.Model
{
    public class Equipe : BaseModel
    {
        public Equipe()
        {

        }
        
        public Equipe(string Nome,string Descricao)
        {
            this.Nome = Nome;
            this.Descricao = Descricao;
        }

        public string Nome { get; set; }

        public string Descricao { get; set; }
    }
}
