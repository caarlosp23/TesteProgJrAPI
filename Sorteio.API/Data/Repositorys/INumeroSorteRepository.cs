using Sorteio.API.Models;

namespace Sorteio.API.Data.Repositorys
{
    public interface INumeroSorteRepository
    {
        void adicionarNumSorte(NumeroSorte num);
        int geraNumSorte();
        void ExportarDadosParaArquivoTexto(string nomeArquivo, int numSorte, string nome, string email, string cpf, string telefone);
    }
}
