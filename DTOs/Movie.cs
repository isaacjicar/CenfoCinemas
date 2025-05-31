using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class Movie : baseDTO
    {
        public string Title { get; set; } 

        public string description { get; set; } 
        public DateTime ReleaseDate { get; set; } 
        public string Genre { get; set; } 

        public string Director { get; set; } 
      
    }
    
}
