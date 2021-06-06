using Entities.Contracts;
using Entities.Entities;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Controllers.HowVI
{
    [Route("TipoContato")]
    public class TipoContatoController : Controller
    {
        private readonly ITipoContatoRepository _tipoContatoRepository;

        public TipoContatoController(ITipoContatoRepository tipoContatoRepository)
        {
            _tipoContatoRepository = tipoContatoRepository;
        }

        [HttpDelete]
        public ActionResult<dynamic> Delete([FromBody] TipoContato tipoContato)
        {
            try
            {
                if (tipoContato != null && tipoContato.Id > 0)
                {
                    _tipoContatoRepository.Remover(tipoContato);
                    return Ok();
                }

                return NotFound("TipoContato não deletado!");
            }
            catch (Exception e)
            {
                return BadRequest(string.Format("Erro na chamada{i}", e.Message));
            }
        }

        [HttpPut]
        public ActionResult<dynamic> Put([FromBody] TipoContato tipoContato)
        {
            try
            {
                if (tipoContato != null && tipoContato.Id > 0)
                {
                    tipoContato = _tipoContatoRepository.Atualizar(tipoContato);
                    return tipoContato;
                }

                return NotFound("TipoContato não atualizado!");
            }
            catch (Exception e)
            {
                return BadRequest(string.Format("Erro na chamada{i}", e.Message));
            }
        }

        [HttpPost]
        public ActionResult<dynamic> Post([FromBody] TipoContato tipoContato)
        {
            try
            {
                if (tipoContato != null)
                {
                    tipoContato = _tipoContatoRepository.Adicionar(tipoContato);
                    if (tipoContato.Id > 0)
                    {
                        return tipoContato;
                    }
                }

                return NotFound("TipoContato não Inserido!");
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
                    var tipoContato = _tipoContatoRepository.ObterPorId(Id);
                    if (tipoContato != null)
                    {
                        return tipoContato;
                    }
                }

                return NotFound("TipoContato não existe!");
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
                var tipoContato = _tipoContatoRepository.ObterTodos();
                if (tipoContato != null)
                {
                    return tipoContato;
                }

                return NotFound("TipoContato não existe!");
            }
            catch (Exception e)
            {
                return BadRequest(string.Format("Erro na chamada{i}", e.Message));
            }
        }
    }
}