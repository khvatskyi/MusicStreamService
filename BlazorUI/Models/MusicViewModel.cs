using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorUI.Models
{
    public class MusicViewModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string author { get; set; }
        public int? year { get; set; }
        public string photoPath { get; set; }
    }
}
