using Entities.Contracts;
using Entities.Entities;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Controllers.HowVI
{
    [Route("ContatoCliente")]
    public class ContatoClienteController : Controller
    {
        private readonly IContatoClienteRepository _contatoClienteRepository;

        public ContatoClienteController(IContatoClienteRepository contatoClienteRepository)
        {
            _contatoClienteRepository = contatoClienteRepository;
        }

        [HttpDelete]
        public ActionResult<dynamic> Delete([FromBody] ContatoCliente contatoCliente)
        {
            try
            {
                if (contatoCliente != null && contatoCliente.Id > 0)
                {
                    _contatoClienteRepository.Remover(contatoCliente);
                    return Ok();
                }

                return NotFound("ContatoCliente não deletado!");
            }
            catch (Exception e)
            {
                return BadRequest(string.Format("Erro na chamada{i}", e.Message));
            }
        }

        [HttpPut]
        public ActionResult<dynamic> Put([FromBody] ContatoCliente contatoCliente)
        {
            try
            {
                if (contatoCliente != null && contatoCliente.Id > 0)
                {
                    contatoCliente = _contatoClienteRepository.Atualizar(contatoCliente);
                    return contatoCliente;
                }

                return NotFound("ContatoCliente não atualizado!");
            }
            catch (Exception e)
            {
                return BadRequest(string.Format("Erro na chamada{i}", e.Message));
            }
        }

        [HttpPost]
        public ActionResult<dynamic> Post([FromBody] ContatoCliente contatoCliente)
        {
            try
            {
                if (contatoCliente != null)
                {
                    contatoCliente = _contatoClienteRepository.Adicionar(contatoCliente);
                    if (contatoCliente.Id > 0)
                    {
                        return contatoCliente;
                    }
                }

                return NotFound("ContatoCliente não Inserido!");
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
                    var contatoCliente = _contatoClienteRepository.ObterPorId(Id);
                    if (contatoCliente != null)
                    {
                        return contatoCliente;
                    }
                }

                return NotFound("ContatoCliente não existe!");
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
                var contatoCliente = _contatoClienteRepository.ObterTodos();
                if (contatoCliente != null)
                {
                    return contatoCliente;
                }

                return NotFound("ContatoCliente não existe!");
            }
            catch (Exception e)
            {
                return BadRequest(string.Format("Erro na chamada{i}", e.Message));
            }
        }
    }
}