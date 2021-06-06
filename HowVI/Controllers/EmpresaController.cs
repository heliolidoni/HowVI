using Entities.Contracts;
using Entities.Entities;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Controllers.HowVI
{
    [Route("Empresa")]
    public class EmpresaController : Controller
    {
        private readonly IEmpresaRepository _empresaRepository;

        public EmpresaController(IEmpresaRepository empresaRepository)
        {
            _empresaRepository = empresaRepository;
        }

        [HttpDelete]
        public ActionResult<dynamic> Delete([FromBody] Empresa empresa)
        {
            try
            {
                if (empresa != null && empresa.Id > 0)
                {
                    _empresaRepository.Remover(empresa);
                    return Ok();
                }

                return NotFound("Empresa não deletado!");
            }
            catch (Exception e)
            {
                return BadRequest(string.Format("Erro na chamada{i}", e.Message));
            }
        }

        [HttpPut]
        public ActionResult<dynamic> Put([FromBody] Empresa empresa)
        {
            try
            {
                if (empresa != null && empresa.Id > 0)
                {
                    empresa = _empresaRepository.Atualizar(empresa);
                    return empresa;
                }

                return NotFound("Empresa não atualizado!");
            }
            catch (Exception e)
            {
                return BadRequest(string.Format("Erro na chamada{i}", e.Message));
            }
        }

        [HttpPost]
        public ActionResult<dynamic> Post([FromBody] Empresa empresa)
        {
            try
            {
                if (empresa != null)
                {
                    empresa = _empresaRepository.Adicionar(empresa);
                    if (empresa.Id > 0)
                    {
                        return empresa;
                    }
                }

                return NotFound("Empresa não Inserido!");
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
                    var empresa = _empresaRepository.ObterPorId(Id);
                    if (empresa != null)
                    {
                        return empresa;
                    }
                }

                return NotFound("Empresa não existe!");
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
                var empresa = _empresaRepository.ObterTodos();
                if (empresa != null)
                {
                    return empresa;
                }

                return NotFound("Empresa não existe!");
            }
            catch (Exception e)
            {
                return BadRequest(string.Format("Erro na chamada{i}", e.Message));
            }
        }
    }
}