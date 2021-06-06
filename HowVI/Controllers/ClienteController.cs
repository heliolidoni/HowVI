using Entities.Contracts;
using Entities.Entities;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Controllers.HowVI
{
    [Route("Cliente")]
    public class ClienteController : Controller
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteController(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        [HttpDelete]
        public ActionResult<dynamic> Delete([FromBody] Cliente cliente)
        {
            try
            {
                if (cliente != null && cliente.Id > 0)
                {
                    _clienteRepository.Remover(cliente);
                    return Ok();
                }

                return NotFound("Cliente não deletado!");
            }
            catch (Exception e)
            {
                return BadRequest(string.Format("Erro na chamada{i}", e.Message));
            }
        }

        [HttpPut]
        public ActionResult<dynamic> Put([FromBody] Cliente cliente)
        {
            try
            {
                if (cliente != null && cliente.Id > 0)
                {
                    cliente = _clienteRepository.Atualizar(cliente);
                    return cliente;
                }

                return NotFound("Cliente não atualizado!");
            }
            catch (Exception e)
            {
                return BadRequest(string.Format("Erro na chamada{i}", e.Message));
            }
        }

        [HttpPost]
        public ActionResult<dynamic> Post([FromBody] Cliente cliente)
        {
            try
            {
                if (cliente != null)
                {
                    cliente = _clienteRepository.Adicionar(cliente);
                    if (cliente.Id > 0)
                    {
                        return cliente;
                    }
                }

                return NotFound("Cliente não Inserido!");
            }
            catch (Exception e)
            {
                return BadRequest(string.Format("Erro na chamada{i}", e.Message));
            }
        }

        [HttpGet("{Id}")]
        public ActionResult<dynamic> Get(int Id)
        {
            try
            {
                if (Id > 0)
                {
                    var cliente = _clienteRepository.ObterPorId(Id);
                    if (cliente != null)
                    {
                        return cliente;
                    }
                }

                return NotFound("Cliente não existe!");
            }
            catch (Exception e)
            {
                return BadRequest(string.Format("Erro na chamada{i}", e.Message));
            }
        }

        [HttpGet]
        public object Get()
        {
            try
            {
                var cliente = _clienteRepository.ObterTodos();
                if (cliente != null)
                {
                    return cliente;
                }

                return NotFound("Cliente não existe!");
            }
            catch (Exception e)
            {
                return BadRequest(string.Format("Erro na chamada{i}", e.Message));
            }
        }
    }
}