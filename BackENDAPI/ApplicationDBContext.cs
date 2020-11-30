using BackENDAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackENDAPI
{
    public class ApplicationDBContext : DbContext
    {
        public DbSet<Usuario> Usuario { get; set; }
    }
}
