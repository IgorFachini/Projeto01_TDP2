using System.ComponentModel.DataAnnotations;

namespace Supermercado.Models
{
    public class LojaFisica
    {
        public int Id { get; set; }
        [Required]
        public string Estado { get; set; }
        [Required]
        public string Cidade { get; set; }
        [Required]
        public string CEP { get; set; }
        [Required]
        public string Telefone { get; set; }
    }
}