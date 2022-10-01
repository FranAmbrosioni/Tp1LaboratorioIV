using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebTp1LaboratorioIV.Models
{
    public class Libros
    {

        [Display(Name = "Nroº Identificador")]
        [Required(ErrorMessage = "El id es obligatorio")]

        public int ID { get; set; }

        public string Titulo { get; set; }

        public string Resumen { get; set; }

        [Display(Name = "Fecha de publicación")]
        public DateTime fechaPublicacion { get; set; }

        public string Portada { get; set; }

        [Display (Name ="Genero")]
        public int GeneroId { get; set; }
        public Generos Genero { get; set; }
        [Display (Name ="Autor")]
        public int AutorId { get; set; }

        public Autor Autor { get; set; }
    }
}
