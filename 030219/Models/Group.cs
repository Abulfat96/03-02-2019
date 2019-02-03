using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _030219.Models
{
    public class Group
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Student> Students { get; set; }
    }
}