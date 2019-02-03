using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _030219.Models
{
    public class Brend
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public List<Model> Models { get; set; }
    }
}