using ApiPetshop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IContatoRepository
    {
        Task<IEnumerable<Contato>> ObterTodosAsync();
        Task<Contato> ObterPorEmailAsync(string email);
        Task<Contato> AdicionarAsync(Contato contato);
        Task<bool> ExcluirAsync(Guid id);
    }
}
