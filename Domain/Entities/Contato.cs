namespace ApiPetshop.Domain.Entities
{
    public class Contato
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Mensagem { get; set; }
        public DateTime DataEnvio { get; set; }
    }
}
