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
        Task<Contato> ObterPorIdAsync(Guid id);
        Task<Contato> AdicionarAsync(Contato contato);
        Task<Contato> ExcluirAsync(Contato contato);
    }
}
