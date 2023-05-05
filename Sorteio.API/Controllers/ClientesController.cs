using Microsoft.AspNetCore.Mvc;
using Sorteio.API.Data.Repositorys;
using Sorteio.API.Models;
using Sorteio.API.Models.InputModels;

namespace Sorteio.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        
        private IClientesRepository _clientesRepository;
        private INumeroSorteRepository _numSorte;

        public ClientesController(IClientesRepository clientesRepository, INumeroSorteRepository numSorte)
        {
            _clientesRepository = clientesRepository;
            _numSorte = numSorte;
        }




        // GET: api/<ClientesController>
        [HttpGet]
        public IActionResult Get()
        {
           var response = _clientesRepository.Get();
             
           return Ok(response);
        }

        // GET api/<ClientesController>/5
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var cliente = _clientesRepository.GetPorId(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return Ok(cliente);
        }

        // POST api/<ClientesController>
        [HttpPost]
        public IActionResult Post([FromBody] ClienteInputModel novoCliente)
        {
            var cliente = new Cliente(novoCliente.Nome, novoCliente.Telefone, novoCliente.Cpf, novoCliente.Email);


            int randomNumber = 0;
            NumeroSorte a = new NumeroSorte(randomNumber, cliente);

            var validaEmail = _clientesRepository.GetPorEmail(novoCliente.Email);

            if (validaEmail == null)
            {
                a.numeroSorte = _numSorte.geraNumSorte();
                if (a.numeroSorte > 0)
                {
                    _numSorte.adicionarNumSorte(a);
                    _clientesRepository.Adicionar(cliente);
                    _numSorte.ExportarDadosParaArquivoTexto(@"C:\Users\Carlos Araujo\source\repos\Sorteio\Sorteio.API\Recibo\"+a.numeroSorte+".txt", a.numeroSorte, novoCliente.Nome, novoCliente.Email, novoCliente.Cpf, novoCliente.Telefone);
                    return Created("", cliente);
                }
                else
                {
                    return BadRequest("Não foi possivel gerar, o limite de numeros foi atingido!");
                }

            }
            else
            {
                var clienteCadastrado = _clientesRepository.GetPorEmail(novoCliente.Email);
                a.cliente = clienteCadastrado;
                
                novoCliente.Nome = clienteCadastrado.Nome;
                novoCliente.Email = clienteCadastrado.Email;
                novoCliente.Cpf = clienteCadastrado.Cpf;
                novoCliente.Telefone = clienteCadastrado.Telefone;


                a.numeroSorte = _numSorte.geraNumSorte();
                if (a.numeroSorte > 0)
                {
                    _numSorte.adicionarNumSorte(a);
                    _numSorte.ExportarDadosParaArquivoTexto(@"C:\Users\Carlos Araujo\source\repos\Sorteio\Sorteio.API\Recibo\"+a.numeroSorte+".txt", a.numeroSorte, novoCliente.Nome,novoCliente.Email,novoCliente.Cpf,novoCliente.Telefone);
                    return Created("", cliente);
                }
                else
                {
                    return BadRequest("Não foi possivel gerar, o limite de numeros foi atingido!");
                }
            }


        }

        // PUT api/<ClientesController>/5
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] ClienteInputModel clienteAtualizado)
        {
            var clienteExistente = _clientesRepository.GetPorId(id);
            var validaEmail = _clientesRepository.GetPorEmail(clienteAtualizado.Email);
            
            if(clienteExistente == null)
            {
                return NotFound();
            }

            if (validaEmail == null)
            {
            clienteExistente.AtualizarCliente(clienteAtualizado.Nome,clienteAtualizado.Telefone, clienteAtualizado.Email);
            _clientesRepository.Atualizar(id, clienteExistente);
            return Ok(clienteExistente);

            }
            return BadRequest("Email já existente na base de dados.");

            

        }

        // DELETE api/<ClientesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var clienteExistente = _clientesRepository.GetPorId(id);

            if (clienteExistente == null)
            {
                return NotFound();
            }
            _clientesRepository.Excluir(id);
            return NoContent();
        }
    }
}
