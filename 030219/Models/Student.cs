using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace _030219.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Adı boş buraxmayın")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Soyadı boş buraxmayın")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Qrupu seçin")]
        public int GroupId { get; set; }

        public Group Group { get; set; }
    }
}