using Supermercado.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace Supermercado.AcessoDados {
    public class BancoContexto : DbContext{
        public DbSet<Genero> Generos { get; set; }
        public DbSet<Livro> Livros { get; set; }
        public DbSet<Game> Games { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Properties<string>().Configure(c => c.HasMaxLength(100));
        }
    }
}