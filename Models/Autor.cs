using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebTp1LaboratorioIV.Models
{
    public class Autor
    {
        [Display(Name = "Nro. de legajo")]
        [Required(ErrorMessage = "Campo requerido")]
        public int ID { get; set; }

        public string Apellido { get; set; }

        public string Nombres { get; set; }

        public string Biografia { get; set; }

        public string Foto { get; set; }

    }
}
