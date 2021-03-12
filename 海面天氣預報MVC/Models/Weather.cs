using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace 海面天氣預報MVC.Models
{
    public class Weather
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(100)]
        public string Title { get; set; }

        public DateTime? Date { get; set; }

        [MaxLength(50)]
        public string Weathers { get; set; }

        public string weatherTitle { get; set; }

        public string WindDir { get; set; }

        [MaxLength(50)]
        public string WaveType { get; set; }

        [MaxLength(50)]
        public string WaveHeight { get; set; }

        [MaxLength(100)]
        public string WindSpeed { get; set; }
    }
}