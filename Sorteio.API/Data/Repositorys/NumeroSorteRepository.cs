using MongoDB.Driver;
using Sorteio.API.Data.Configurations;
using Sorteio.API.Models;
using System;

namespace Sorteio.API.Data.Repositorys
{
    public class NumeroSorteRepository : INumeroSorteRepository
    {
        private readonly IMongoCollection<NumeroSorte> _numSorte;
        

        public NumeroSorteRepository(IDatabaseConfig databaseConfig)
        {
            var client = new MongoClient(databaseConfig.ConnectionString);
            var database = client.GetDatabase(databaseConfig.DatabaseName);
            _numSorte = database.GetCollection<NumeroSorte>("NumeroSorte");
        }


        public void adicionarNumSorte(NumeroSorte num)
        {
            _numSorte.InsertOne(num);
        }

        public int geraNumSorte()
        {
            Random random = new Random();
            int randomNumber = 0;
            var qtdNumeros = _numSorte.Find(numSorte => true).ToList(); 

            if (qtdNumeros.Count < 99999)
            {
                randomNumber = random.Next(0, 99999);
                var validaNumSorte = _numSorte.Find(num => num.numeroSorte == randomNumber).FirstOrDefault();
              
                while (validaNumSorte != null)
                {
                    randomNumber = random.Next(0, 99999);
                    validaNumSorte = _numSorte.Find(num => num.numeroSorte == randomNumber).FirstOrDefault();
                }
                return randomNumber;
            }
            return -1;
        }

        public void ExportarDadosParaArquivoTexto(string nomeArquivo, int numSorte, string nome,string email, string cpf, string telefone)
        {
          
            using (var streamWriter = new StreamWriter(nomeArquivo))
            {
                streamWriter.WriteLine(numSorte.ToString() +" "+ nome +" "+ email+" "+ cpf +" "+  telefone);
            }
        }
    }
}
