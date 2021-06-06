using Entities.Contracts;
using Entities.Entities;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Controllers.HowVI
{
    [Route("Status")]
    public class StatusController : Controller
    {
        private readonly IStatusRepository _statusRepository;

        public StatusController(IStatusRepository statusRepository)
        {
            _statusRepository = statusRepository;
        }

        [HttpDelete]
        public ActionResult<dynamic> Delete([FromBody] Status status)
        {
            try
            {
                if (status != null && status.Id > 0)
                {
                    _statusRepository.Remover(status);
                    return Ok();
                }

                return NotFound("Status não deletado!");
            }
            catch (Exception e)
            {
                return BadRequest(string.Format("Erro na chamada{i}", e.Message));
            }
        }

        [HttpPut]
        public ActionResult<dynamic> Put([FromBody] Status status)
        {
            try
            {
                if (status != null && status.Id > 0)
                {
                    status = _statusRepository.Atualizar(status);
                    return status;
                }

                return NotFound("Status não atualizado!");
            }
            catch (Exception e)
            {
                return BadRequest(string.Format("Erro na chamada{i}", e.Message));
            }
        }

        [HttpPost]
        public ActionResult<dynamic> Post([FromBody] Status status)
        {
            try
            {
                if (status != null)
                {
                    status = _statusRepository.Adicionar(status);
                    if (status.Id > 0)
                    {
                        return status;
                    }
                }

                return NotFound("Status não Inserido!");
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
                    var status = _statusRepository.ObterPorId(Id);
                    if (status != null)
                    {
                        return status;
                    }
                }

                return NotFound("Status não existe!");
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
                var status = _statusRepository.ObterTodos();
                if (status != null)
                {
                    return status;
                }

                return NotFound("Status não existe!");
            }
            catch (Exception e)
            {
                return BadRequest(string.Format("Erro na chamada{i}", e.Message));
            }
        }
    }
}