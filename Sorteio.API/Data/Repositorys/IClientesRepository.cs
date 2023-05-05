using Sorteio.API.Models;

namespace Sorteio.API.Data.Repositorys
{
    public interface IClientesRepository
    {
        void Adicionar(Cliente cliente)
        {
        }
        void Atualizar(string id, Cliente cliente)
        {
        }
        IEnumerable<Cliente> Get();  

        Cliente GetPorId(string id);

        Cliente GetPorEmail(string email);

        void Excluir(string id);

        
    }
}
