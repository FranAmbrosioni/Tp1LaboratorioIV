using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebTp1LaboratorioIV.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }


        public DbSet<Libros> Libro { get; set; }

        public DbSet<Generos> Genero { get; set; }

        public DbSet<Autor> Autor { get; set; }
    }
}
