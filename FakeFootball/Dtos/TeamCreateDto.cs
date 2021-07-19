using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FakeFootball.Dtos
{
    public class TeamCreateDto
    {
        public string TeamName { get; set; }

        public int Points { get; private set; } = 0;

        public int GamesPlayed { get; private set; } = 0;
    }
}
