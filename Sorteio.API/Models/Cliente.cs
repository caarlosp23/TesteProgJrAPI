namespace Sorteio.API.Models
{
    public class Cliente
    {
        public Cliente(string nome, string telefone, string cpf, string email)
        {
            Id = Guid.NewGuid().ToString();
            Nome = nome;
            Telefone = telefone;
            Cpf = cpf;
            Email = email;
        }

        public string Id { get; private set; }
        public string Nome { get; private set; }
        public string Telefone { get; private set; }
        public string Cpf { get; private set; }
        public string Email { get; private set; }

        public void AtualizarCliente(string nome, string telefone, string email)
        {
            Nome= nome;
            Telefone= telefone;
            Email = email;
        }
    }
}
