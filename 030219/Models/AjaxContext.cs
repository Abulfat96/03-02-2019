using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace _030219.Models
{
    public class AjaxContext:DbContext
    {
        public AjaxContext() : base("AjaxContext")
        {

        }

        public DbSet<Brend> Brends { get; set; }

        public DbSet<Model> Models { get; set; }

        public DbSet<Student> Students { get; set; }

        public DbSet<Group> Groups { get; set; }
    }
}