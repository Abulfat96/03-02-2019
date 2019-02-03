using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _030219.Models
{
    public class Model
    {
        public int Id { get; set; }

        public int BrendId { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public Brend Brend { get; set; }
    }
}