using ApiPetshop.Domain.Entities;
using ApiPetshop;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using ApiPetshop.Models;

namespace Infra.Data
{
    public class MeuDbContext : DbContext
    {
        public MeuDbContext(DbContextOptions<MeuDbContext> options) : base(options)
        {

        }

        public DbSet<Contato> Contatos { get; set; }
        public DbSet<FotosAntesDepois> FotosAntesDepois { get; set; }
    }
}
