using AutoMapper;
using FakeFootball.Data;
using FakeFootball.Dtos;
using FakeFootball.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FakeFootball.Controllers
{
    //done 100%
    [Route("api/teams")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        private ILogger<TeamsController> _logger;
        private readonly IMapper _mapper;
        private readonly ITeamRepo _teamRepo;

        public TeamsController(ILogger<TeamsController> logger, IMapper mapper, ITeamRepo teamRepo)
        {
            _logger = logger;
            _mapper = mapper;
            _teamRepo = teamRepo;
        }

        // 200-Ok,404-NotFound,400-Bad Request
        [HttpGet]
        public IActionResult GetAllTeams()
        {
            var res = _teamRepo.GetAllTeams();
            if (res == null)
            {
                return NotFound();
            }
            var output = _mapper.Map<List<TeamsReadDto>>(res);
            return Ok(output);
        }

        // 200-Ok,404-NotFound,400-Bad Request
        [HttpGet("{id}",Name ="GetOneTeam")]
        public IActionResult GetOneTeam(int id)
        {
            var res = _teamRepo.GetOneTeam(id);
            if (res == null)
            {
                return NotFound();
            }
            var output = _mapper.Map<TeamsReadDto>(res);
            return Ok(output);
        }

        // 201-Created,405-NotAllowed,400-Bad Request
        [HttpPost]
        public IActionResult CreateTeam(TeamCreateDto team)
        {
            if (ModelState.IsValid)
            {
                var teamCreated = _mapper.Map<TeamModel>(team);

                _teamRepo.CreateTeam(teamCreated);
                _teamRepo.SaveChanges();

                return CreatedAtRoute(nameof(GetOneTeam), new { Id = teamCreated.Id }, teamCreated);
            }
            else
            {
                return BadRequest();
            }
        }

        // 204-NoContent,400-BadRequest,404-Not Found
        [HttpDelete("{id}")]
        public IActionResult DeleteTeam(int id)
        {
            var res = _teamRepo.GetOneTeam(id);

            if (res == null)
            {
                return NotFound();
            }

            _teamRepo.DeleteTeam(res);
            _teamRepo.SaveChanges();

            return NoContent();
        }

        // 204-NoContent, 400-BadRequest,404- Not Found
        [HttpPut("{id}")]
        public IActionResult UpdateTeamFully(int id,TeamUpdateDto teamDto)
        {
            var res = _teamRepo.GetOneTeam(id);

            if (res == null)
            {
                return NotFound();
            }

            var team = _mapper.Map<TeamModel>(teamDto);

            _mapper.Map(team, res);

            _teamRepo.UpdateTeam(res);
            _teamRepo.SaveChanges();

            return NoContent();
        }

        // 204-NoContent, 400-BadRequest, 404- Not Found
        [HttpPatch("{id}")]
        public IActionResult UpdateTeamPartially(int id, JsonPatchDocument<TeamUpdateDto> patchDocument)
        {
            var res = _teamRepo.GetOneTeam(id);

            if (res == null)
            {
                return NotFound();
            }

            var mappedTeam = _mapper.Map<TeamUpdateDto>(res);
            patchDocument.ApplyTo(mappedTeam);

            _mapper.Map(mappedTeam, res);

            _teamRepo.UpdateTeam(res);
            _teamRepo.SaveChanges();

            return NoContent();
        }
    }
}
