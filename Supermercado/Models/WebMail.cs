using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Supermercado.Models
{
    public class WebMail
    {
        [DataType(DataType.EmailAddress), Display(Name = "Para:")]
        [Required]
        public string ToEmail { get; set; }
        [Display(Name = "Mensagem:")]
        [DataType(DataType.MultilineText)]
        public string EMailBody { get; set; }
        [Display(Name = "Titulo:")]
        public string EmailSubject { get; set; }
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Copia para:")]
        public string EmailCC { get; set; }
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Cópia oculta para:")]
        public string EmailBCC { get; set; }
    }
}
