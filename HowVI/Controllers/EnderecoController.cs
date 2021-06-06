using Entities.Contracts;
using Entities.Entities;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Controllers.HowVI
{
    [Route("Endereco")]
    public class EnderecoController : Controller
    {
        private readonly IEnderecoRepository _enderecoRepository;

        public EnderecoController(IEnderecoRepository enderecoRepository)
        {
            _enderecoRepository = enderecoRepository;
        }

        [HttpDelete]
        public ActionResult<dynamic> Delete([FromBody] Endereco endereco)
        {
            try
            {
                if (endereco != null && endereco.Id > 0)
                {
                    _enderecoRepository.Remover(endereco);
                    return Ok();
                }

                return NotFound("Endereco não deletado!");
            }
            catch (Exception e)
            {
                return BadRequest(string.Format("Erro na chamada{i}", e.Message));
            }
        }

        [HttpPut]
        public ActionResult<dynamic> Put([FromBody] Endereco endereco)
        {
            try
            {
                if (endereco != null && endereco.Id > 0)
                {
                    endereco = _enderecoRepository.Atualizar(endereco);
                    return endereco;
                }

                return NotFound("Endereco não atualizado!");
            }
            catch (Exception e)
            {
                return BadRequest(string.Format("Erro na chamada{i}", e.Message));
            }
        }

        [HttpPost]
        public ActionResult<dynamic> Post([FromBody] Endereco endereco)
        {
            try
            {
                if (endereco != null)
                {
                    endereco = _enderecoRepository.Adicionar(endereco);
                    if (endereco.Id > 0)
                    {
                        return endereco;
                    }
                }

                return NotFound("Endereco não Inserido!");
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
                    var endereco = _enderecoRepository.ObterPorId(Id);
                    if (endereco != null)
                    {
                        return endereco;
                    }
                }

                return NotFound("Endereco não existe!");
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
                var endereco = _enderecoRepository.ObterTodos();
                if (endereco != null)
                {
                    return endereco;
                }

                return NotFound("Endereco não existe!");
            }
            catch (Exception e)
            {
                return BadRequest(string.Format("Erro na chamada{i}", e.Message));
            }
        }
    }
}