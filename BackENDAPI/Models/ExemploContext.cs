using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BackENDAPI.Models
{
    public class ExemploContext : DbContext
    {
        public ExemploContext(DbContextOptions<ExemploContext> options): base(options) { }
        public DbSet<Usuario> Usuario { get; set; }
    }
}
       