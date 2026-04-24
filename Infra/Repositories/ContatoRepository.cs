using ApiPetshop.Domain.Entities;
using Domain.Interfaces;
using Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories
{
    public class ContatoRepository : IContatoRepository
    {
        private readonly MeuDbContext _meuDbContext;
        public ContatoRepository(MeuDbContext meuDbContext)
        {
            _meuDbContext = meuDbContext;
        }

        public async Task<Contato> AdicionarAsync(Contato contato)
        {
            await _meuDbContext.Contatos.AddAsync(contato);
            await _meuDbContext.SaveChangesAsync();

            return contato;
        }

        public async Task<bool> ExcluirAsync(Guid id)
        {
            // Busca o contato no banco primeiro
            var contato = await _meuDbContext.Contatos.FindAsync(id);

            // Se não existir, avisa o controller (retornando false)
            if (contato == null) return false;

            // Remove o objeto encontrado
            _meuDbContext.Contatos.Remove(contato);
            await _meuDbContext.SaveChangesAsync();

            return true;
        }

        public async Task<Contato?> ObterPorEmailAsync(string email)
        {
            return await _meuDbContext.Contatos.FirstOrDefaultAsync(c => c.Email == email);
        }

        public Task<IEnumerable<Contato>> ObterTodosAsync()
        {
            throw new NotImplementedException();
        }
    }
}
