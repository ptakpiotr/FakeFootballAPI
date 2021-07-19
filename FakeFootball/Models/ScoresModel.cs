using System;
using System.ComponentModel.DataAnnotations;

namespace FakeFootball.Models
{
    public class ScoresModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Range(0, 100)]
        public int HomeScore { get; set; }
        [Required]
        [Range(0, 100)]
        public int AwayScore { get; set; }

        [Required]
        public string HomeTeam { get; set; }
        [Required]
        public string AwayTeam { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime GamePlayedAt { get; set; } = DateTime.UtcNow;

    }
}
