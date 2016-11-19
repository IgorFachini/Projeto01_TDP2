using Supermercado.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Supermercado.AcessoDados
{
    public class Inicializador : CreateDatabaseIfNotExists<BancoContexto>
    {
        protected override void Seed(BancoContexto contexto)
        {
            List<LojaFisica> lojasFisicas = new List<LojaFisica>()
            {
                new LojaFisica()
                {
                    Estado = "Santa Catarina",
                    Cidade = "Jaraguá do sul",
                    CEP =  "6969-6969",
                    Telefone = "9999-9999"
                }
            };
            lojasFisicas.ForEach(lj => contexto.LojasFisicas.Add(lj));
            List<Tipo> tipos = new List<Tipo>()
            {
                new Tipo() {Nome = "Livro" },
                new Tipo() {Nome = "Game" },
                new Tipo() {Nome = "Manga" },
                new Tipo() {Nome = "ActionFigure"},
                new Tipo() {Nome = "Filme" }
            };

            tipos.ForEach(t => contexto.Tipos.Add(t));

            List<Genero> generos = new List<Genero>()
            {
                new Genero() { Nome = "Administração" },
                new Genero() { Nome = "Didáticos" },
                new Genero() { Nome = "Drama" },
                new Genero() { Nome = "Nacional" },
                new Genero() { Nome = "Religião" },
                new Genero() { Nome = "RPG" },
                new Genero() { Nome = "Hentai" },
                new Genero() { Nome = "Ficção" },
            };

            generos.ForEach(g => contexto.Generos.Add(g));

            List<Livro> livros = new List<Livro>()
            {
                new Livro() {
                            Titulo = "O Poder do Hábito - Por Que Fazemos o Que Fazemos na Vida e Nos Negócios",
                            Autor = "Duhigg, Charles",
                            AnoEdicao = 2012,
                            Valor = 40.00m,
                            Genero = generos.FirstOrDefault(g => g.Nome == "Administração"),
                            Tipo = tipos.FirstOrDefault(t => t.Nome == "Livro")
                },
                new Livro() {
                            Titulo = "Quarto de Guerra - A Oração É Uma Arma Poderosa na Batalha Espiritual",
                            Autor = "Fabry, Chris",
                            AnoEdicao = 2015,
                            Valor = 25.50m,
                            Genero = generos.FirstOrDefault(g => g.Nome == "Religião"),
                             Tipo = tipos.FirstOrDefault(t => t.Nome == "Livro")
                },
                new Livro() {
                            Titulo = "Cristianismo Puro e Simples",
                            Autor = "Lewis, C. S.",
                            AnoEdicao = 2009,
                            Valor = 36.00m,
                            Genero = generos.FirstOrDefault(g => g.Nome == "Religião"),
                             Tipo = tipos.FirstOrDefault(t => t.Nome == "Livro")
                },
            };

            livros.ForEach(l => contexto.Livros.Add(l));

            List<Game> games = new List<Game>() {
                new Game() {
                            Titulo = "The Elder Scrolls V: Skyrim",
                            Plataforma = "PC",
                            AnoLancamento = 2011,
                            Valor = 40.00m,
                            Genero = generos.FirstOrDefault(g => g.Nome == "RPG"),
                            Tipo = tipos.FirstOrDefault(t => t.Nome == "Game")
                }
            };

            games.ForEach(l => contexto.Games.Add(l));

            List<Filme> filmes = new List<Filme>() {
                new Filme() {
                            Titulo = "Forrest Gump",
                            Diretor = "Robert Zemeckis",
                            AnoLancamento = 2011,
                            Valor = 30.00m,
                            Genero = generos.FirstOrDefault(g => g.Nome == "Drama"),
                            Tipo = tipos.FirstOrDefault(t => t.Nome == "Filme")
                }
            };

            filmes.ForEach(l => contexto.Filmes.Add(l));

            List<Manga> mangas = new List<Manga>()
            {
                new Manga()
                {
                    Titulo = "High School DXD",
                    Descricao = "Ator principal ganha poderes ao tocar peito da mestra",
                    Autor = "Mr m",
                    Ano = 2002,
                    Valor = 30.00m,
                    Genero = generos.FirstOrDefault(g => g.Nome == "Hentai"),
                    Tipo = tipos.FirstOrDefault(t => t.Nome == "Manga")
                }
            };

            mangas.ForEach(l => contexto.Mangas.Add(l));

            List<ActionFigure> actionFigures = new List<ActionFigure>()
            {
                new ActionFigure()
                {
                    Figura = "Little Groot - Guardians of The Galaxy ",
                    Fabricante = "Hot Toys",
                    Descricao = "Action figure do personagem Groot interpretado por Vin Diesel no filme Guardiões da Galáxia",
                    Valor = 300.00m,
                    Genero = generos.FirstOrDefault(g => g.Nome == "Ficção"),
                    Tipo = tipos.FirstOrDefault(t => t.Nome == "ActionFigure")

                }
            };

            actionFigures.ForEach(l => contexto.ActionFigures.Add(l));

            contexto.SaveChanges();
        }
    }
}