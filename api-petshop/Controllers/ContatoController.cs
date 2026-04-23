using ApiPetshop.Models;
using Application.DTOs;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApiPetshop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContatoController : ControllerBase
    {
        private readonly IContatoRepository _repository;

        public ContatoController(IContatoRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> EnviarFormulario([FromBody] ContatoRequest request)
        {
            // Validação simples
            if (string.IsNullOrEmpty(request.Email)) return BadRequest("Email é obrigatório.");

            var novoContato = new Domain.Entities.Contato
            {
                Id = Guid.NewGuid(),
                Nome = request.Nome,
                Email = request.Email,
                Mensagem = request.Mensagem,
                DataEnvio = DateTime.UtcNow
            };

            await _repository.AdicionarAsync(novoContato);

            return Ok(new { message = "Mensagem enviada com sucesso!" });
        }
    }
}
