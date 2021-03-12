using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace 海面天氣預報MVC.Models
{
    public partial class WeatherModel : DbContext
    {
        public WeatherModel()
            : base("name=WeatherModel")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }

        public virtual DbSet<Weather> Weather { get; set; }
    }
}