using Entities.Contracts;
using Entities.Entities;
using HowVI.Arguments;
using Microsoft.AspNetCore.Mvc;
using System;

namespace HowVI.Controllers
{
    [Route("Vendedor")]
    public class VendedorController : Controller
    {
        private readonly IVendedorRepository _vendedorRepository;

        public VendedorController(IVendedorRepository vendedorRepository)
        {
            _vendedorRepository = vendedorRepository;
        }

        public ActionResult<dynamic> Login([FromBody] LoginRequest loginRequest)
        {
            try
            {
                var vendedor = _vendedorRepository.ObterPorLogin(loginRequest.Login);

                if (vendedor.Senha.Equals(loginRequest.Senha))
                {
                    return new LoginResponse()
                    {
                        IsAutenticado = true,
                        Validade = DateTime.Now.AddHours(2),
                        Token = "AUTENTICADO"
                    };
                }

                return Unauthorized();
            }
            catch (Exception Ex)
            {
                return BadRequest(string.Format("Erro ao logar + {0}", Ex.Message));
            }
        }

        [HttpDelete]
        public ActionResult<dynamic> Delete([FromBody] Vendedor vendedor)
        {
            try
            {
                if (vendedor != null && vendedor.Id > 0)
                {
                    _vendedorRepository.Remover(vendedor);
                    return Ok();
                }

                return NotFound("Vendedor não deletado!");
            }
            catch (Exception e)
            {
                return BadRequest(string.Format("Erro na chamada{i}", e.Message));
            }
        }

        [HttpPut]
        public ActionResult<dynamic> Put([FromBody] Vendedor vendedor)
        {
            try
            {
                if (vendedor != null && vendedor.Id > 0)
                {
                    vendedor = _vendedorRepository.Atualizar(vendedor);
                    return vendedor;
                }

                return NotFound("Vendedor não atualizado!");
            }
            catch (Exception e)
            {
                return BadRequest(string.Format("Erro na chamada{i}", e.Message));
            }
        }

        [HttpPost]
        public ActionResult<dynamic> Post([FromBody] Vendedor vendedor)
        {
            try
            {
                if (vendedor != null)
                {
                    vendedor = _vendedorRepository.Adicionar(vendedor);
                    if (vendedor.Id > 0)
                    {
                        return vendedor;
                    }
                }

                return NotFound("Vendedor não Inserido!");
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
                    var vendedor = _vendedorRepository.ObterPorId(Id);
                    if (vendedor != null)
                    {
                        return vendedor;
                    }
                }

                return NotFound("Vendedor não existe!");
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
                var vendedor = _vendedorRepository.ObterTodos();
                if (vendedor != null)
                {
                    return vendedor;
                }

                return NotFound("Vendedor não existe!");
            }
            catch (Exception e)
            {
                return BadRequest(string.Format("Erro na chamada{i}", e.Message));
            }
        }
    }
}
