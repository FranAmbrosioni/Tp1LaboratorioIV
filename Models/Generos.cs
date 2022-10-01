using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebTp1LaboratorioIV.Models
{
    public class Generos
    {
        [Required(ErrorMessage = "Campo obligatorio")]
        public int ID { get; set; }

        public string Descripcion { get; set; }

    }
}
