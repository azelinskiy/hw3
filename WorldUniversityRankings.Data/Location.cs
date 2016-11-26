using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldUniversityRankings.Data
{
    public class Location
    {
        public int Id { get; set; }
        public Year Year { get; set; }

        [Required]
        public List<Institution> Institutions { get; set; }
    }
}
