using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldUniversityRankings.Data
{
    public class Year
    {
        public int Id { get; set; }
        public int WorldRank { get; set; }
        public int NationalRank { get; set; }
        public int QualityOfEducation { get; set; }
        public int AlumniEmployment { get; set; }
        public int QualityOfFaculty { get; set; }
        public int Publications { get; set; }
        public int Influence { get; set; }
        public int Citations { get; set; }
        public int BroadImpact { get; set; }
        public int Patents { get; set; }
        public double Score { get; set; }

        [Required]
        public List<Location> Locations { get; set; }
    }
}
