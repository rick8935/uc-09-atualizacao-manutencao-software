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

        public async Task<Contato> ExcluirAsync(Contato contato)
        {
            _meuDbContext.Contatos.Remove(contato);
            await _meuDbContext.SaveChangesAsync();

            return contato;
        }

        public async Task<Contato> ObterPorIdAsync(Guid id)
        {
            return await _meuDbContext.Contatos.FirstOrDefaultAsync(u => u.Id == id);
        }

        public Task<IEnumerable<Contato>> ObterTodosAsync()
        {
            throw new NotImplementedException();
        }
    }
}
