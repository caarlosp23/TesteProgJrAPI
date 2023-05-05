namespace Sorteio.API.Models
{
    public class NumeroSorte
    {
        public NumeroSorte(int nSorte, Cliente cliente)
        {
            Id = Guid.NewGuid().ToString();
            this.cliente = cliente;
            numeroSorte = nSorte;
        }


        public string Id { get; set; }
        public int numeroSorte { get; set; }
        
        public Cliente cliente;

    }
}
