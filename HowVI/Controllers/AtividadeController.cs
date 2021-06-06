using Entities.Contracts;
using Entities.Entities;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Controllers.HowVI
{
    [Route("Atividade")]
    public class AtividadeController : Controller
    {
        private readonly IAtividadeRepository _atividadeRepository;

        public AtividadeController(IAtividadeRepository atividadeRepository)
        {
            _atividadeRepository = atividadeRepository;
        }

        [HttpDelete]
        public ActionResult<dynamic> Delete([FromBody] Atividade atividade)
        {
            try
            {
                if (atividade != null && atividade.Id > 0)
                {
                    _atividadeRepository.Remover(atividade);
                    return Ok();
                }

                return NotFound("Atividade não deletado!");
            }
            catch (Exception e)
            {
                return BadRequest(string.Format("Erro na chamada{i}", e.Message));
            }
        }

        [HttpPut]
        public ActionResult<dynamic> Put([FromBody] Atividade atividade)
        {
            try
            {
                if (atividade != null && atividade.Id > 0)
                {
                    atividade = _atividadeRepository.Atualizar(atividade);
                    return atividade;
                }

                return NotFound("Atividade não atualizado!");
            }
            catch (Exception e)
            {
                return BadRequest(string.Format("Erro na chamada{i}", e.Message));
            }
        }

        [HttpPost]
        public ActionResult<dynamic> Post([FromBody] Atividade atividade)
        {
            try
            {
                if (atividade != null)
                {
                    atividade = _atividadeRepository.Adicionar(atividade);
                    if (atividade.Id > 0)
                    {
                        return atividade;
                    }
                }

                return NotFound("Atividade não Inserido!");
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
                    var atividade = _atividadeRepository.ObterPorId(Id);
                    if (atividade != null)
                    {
                        return atividade;
                    }
                }

                return NotFound("Atividade não existe!");
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
                var atividade = _atividadeRepository.ObterTodos();
                if (atividade != null)
                {
                    return atividade;
                }

                return NotFound("Atividade não existe!");
            }
            catch (Exception e)
            {
                return BadRequest(string.Format("Erro na chamada{i}", e.Message));
            }
        }
    }
}