using ApiPetshop.Domain.Entities;
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

        [HttpGet("por-email/{email}")]
        public async Task<IActionResult> ObterContatoPorEmail(string email)
        {
            var contato = await _repository.ObterPorEmailAsync(email);

            if (contato == null)
            {
                return NotFound(new { message = "Nenhum contato encontrado com este e-mail." });
            }

            return Ok(contato);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> ExcluirContato(Guid id)
        {
            // Tenta excluir e guarda o resultado (sucesso ou falha)
            var excluiu = await _repository.ExcluirAsync(id);

            if (!excluiu)
            {
                return NotFound(new { message = "Nenhum contato encontrado com este ID." });
            }

            return Ok(new { message = "Contato excluído com sucesso!" });
        }
    }
}
