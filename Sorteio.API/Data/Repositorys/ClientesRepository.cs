using MongoDB.Driver;
using Sorteio.API.Data.Configurations;
using Sorteio.API.Models;

namespace Sorteio.API.Data.Repositorys
{
    public class ClientesRepository : IClientesRepository
    {
        private readonly IMongoCollection<Cliente> _clientes;
        

        public ClientesRepository(IDatabaseConfig databaseConfig)
        {
            var client = new MongoClient(databaseConfig.ConnectionString);
            var database = client.GetDatabase(databaseConfig.DatabaseName);

            _clientes = database.GetCollection<Cliente>("Clientes");
            
        }

        public void Adicionar(Cliente cliente)
        {
            _clientes.InsertOne(cliente);
            
        }
        public void Atualizar(string id, Cliente clienteAtualizado)
        {
            _clientes.ReplaceOne(cliente => cliente.Id == id,clienteAtualizado);
        }
        public IEnumerable<Cliente> Get()
        {
            return _clientes.Find(cliente => true).ToList();
        }

        public Cliente GetPorId(string id)
        {
            return _clientes.Find(cliente => cliente.Id == id).FirstOrDefault();
        }

        public Cliente GetPorEmail(string email)
        {
            return _clientes.Find(cliente => cliente.Email == email).FirstOrDefault();
        }

        public void Excluir(string id)
        {
            _clientes.DeleteOne(cliente => cliente.Id == id);
        }

        
    }
}
