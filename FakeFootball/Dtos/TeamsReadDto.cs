using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FakeFootball.Dtos
{
    public class TeamsReadDto
    {
        public int Id { get; set; }
        public string TeamName { get; set; }
        public int Points { get; set; }
    }
}
