using Supermercado.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Supermercado.AcessoDados {
    public class Inicializador : CreateDatabaseIfNotExists<BancoContexto> {
        protected override void Seed(BancoContexto contexto) {
            List<Genero> generos = new List<Genero>()
            {
                new Genero() { Nome = "Administração" },
                new Genero() { Nome = "Didáticos" },
                new Genero() { Nome = "Drama" },
                new Genero() { Nome = "Nacional" },
                new Genero() { Nome = "Religião" },
                new Genero() { Nome = "RPG" },
            };

            generos.ForEach(g => contexto.Generos.Add(g));

            List<Livro> livros = new List<Livro>()
            {
                new Livro() {
                            Titulo = "O Poder do Hábito - Por Que Fazemos o Que Fazemos na Vida e Nos Negócios",
                            Autor = "Duhigg, Charles",
                            AnoEdicao = 2012,
                            Valor = 40.00m,
                            Genero = generos.FirstOrDefault(g => g.Nome == "Administração")
                },
                new Livro() {
                            Titulo = "Quarto de Guerra - A Oração É Uma Arma Poderosa na Batalha Espiritual",
                            Autor = "Fabry, Chris",
                            AnoEdicao = 2015,
                            Valor = 25.50m,
                            Genero = generos.FirstOrDefault(g => g.Nome == "Religião")
                },
                new Livro() {
                            Titulo = "Cristianismo Puro e Simples",
                            Autor = "Lewis, C. S.",
                            AnoEdicao = 2009,
                            Valor = 36.00m,
                            Genero = generos.FirstOrDefault(g => g.Nome == "Religião")
                },   
            };

            livros.ForEach(l => contexto.Livros.Add(l));

            List<Game> games = new List<Game>() {
                new Game() {
                            Titulo = "The Elder Scrolls V: Skyrim",
                            Plataforma = "PC",
                            AnoLancamento = 2011,
                            Valor = 40.00m,
                            Genero = generos.FirstOrDefault(g => g.Nome == "RPG")
                }
            };

            games.ForEach(l => contexto.Games.Add(l));

            List<Filme> filmes = new List<Filme>() {
                new Filme() {
                            Titulo = "Forrest Gump",
                            Diretor = "Robert Zemeckis",
                            AnoLancamento = 2011,
                            Valor = 30.00m,
                            Genero = generos.FirstOrDefault(g => g.Nome == "Drama")
                }
            };

            filmes.ForEach(l => contexto.Filmes.Add(l));

            contexto.SaveChanges();
        }
    }
}