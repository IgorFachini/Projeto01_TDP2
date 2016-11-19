using Supermercado.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Collections;

namespace Supermercado.AcessoDados {
    public class BancoContexto : DbContext{


        public BancoContexto() : base("DefaultConnection") { }

        public DbSet<Genero> Generos { get;  set; }
        public DbSet<Tipo> Tipos { get;  set; }
        public DbSet<Livro> Livros { get;  set; }
        public DbSet<Game> Games { get;  set; }
        public DbSet<Filme> Filmes { get;  set; }

        public DbSet<LojaFisica> LojasFisicas { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Properties<string>().Configure(c => c.HasMaxLength(100));
        }


        public DbSet<Manga> Mangas { get; set; }

        public DbSet<ActionFigure> ActionFigures { get; set; }
       
    }
}