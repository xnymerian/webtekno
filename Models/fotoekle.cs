using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Http;

namespace webtekno.Models
{
    public class fotoekle
    {
        public int meslekid { get; set; }
        public string ad { get; set; }
        public Nullable<int> yas { get; set; }
        public string departman { get; set; }
        public string mevki { get; set; }
        public string tecrube { get; set; }
        public IFormFile Photo { get; set; }
    }
}