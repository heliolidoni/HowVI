using Entities.Contracts;
using Entities.Entities;
using HowVI.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Controllers.HowVI
{
    [Route("Cliente")]
    public class ClienteController : Controller
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IService _service;

        public ClienteController(IClienteRepository clienteRepository, IService service)
        {
            _service = service;
            _clienteRepository = clienteRepository;
        }

        [HttpDelete]
        public ActionResult<dynamic> Delete([FromHeader] string token, [FromBody] Cliente cliente)
        {
            if (!_service.Autorizado(token))
            {
                return Unauthorized();
            }

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
                return BadRequest(string.Format("Erro na chamada{0}", e.Message));
            }
        }

        [HttpPut]
        public ActionResult<dynamic> Put([FromHeader] string token, [FromBody] Cliente cliente)
        {
            if (!_service.Autorizado(token))
            {
                return Unauthorized();
            }

            try
            {
                if (cliente != null && cliente.Id > 0)
                {
                    cliente.DataAlteracao = DateTime.Now;
                    cliente.UsuarioAlteracao = _service.ObterUsuarioPorToken(token);
                    cliente = _clienteRepository.Atualizar(cliente);
                    return cliente;
                }

                return NotFound("Cliente não atualizado!");
            }
            catch (Exception e)
            {
                return BadRequest(string.Format("Erro na chamada{0}", e.Message));
            }
        }

        [HttpPost]
        public ActionResult<dynamic> Post([FromHeader] string token, [FromBody] Cliente cliente)
        {
            if (!_service.Autorizado(token))
            {
                return Unauthorized();
            }

            try
            {
                if (cliente != null)
                {
                    cliente.DataAlteracao = DateTime.Now;
                    cliente.UsuarioAlteracao = _service.ObterUsuarioPorToken(token);
                    cliente.DataCriacao = DateTime.Now;
                    cliente.UsuarioCriacao = cliente.UsuarioAlteracao;
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
                return BadRequest(string.Format("Erro na chamada{0}", e.Message));
            }
        }

        [HttpGet("{Id}")]
        public ActionResult<dynamic> Get([FromHeader] string token, int Id)
        {
            if (!_service.Autorizado(token))
            {
                return Unauthorized();
            }

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
                return BadRequest(string.Format("Erro na chamada{0}", e.Message));
            }
        }

        [HttpGet]
        public object Get([FromHeader] string token)
        {
            if (!_service.Autorizado(token))
            {
                return Unauthorized();
            }

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
                return BadRequest(string.Format("Erro na chamada{0}", e.Message));
            }
        }
    }
}