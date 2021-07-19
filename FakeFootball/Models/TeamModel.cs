using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FakeFootball.Models
{
    public class TeamModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string TeamName { get; set; }

        [Range(0,100)]
        public int Points { get; set; }

        [Range(0,38)]
        public int GamesPlayed { get; set; }
    }
}
