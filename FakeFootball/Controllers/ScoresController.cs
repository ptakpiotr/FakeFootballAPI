using AutoMapper;
using FakeFootball.Data;
using FakeFootball.Data.JwtData;
using FakeFootball.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Text.RegularExpressions;

namespace FakeFootball.Controllers
{
    [Authorize]
    [ApiController]
    [Route("/api/scores")]
    public class ScoresController : ControllerBase
    {
        private readonly ILogger<ScoresController> _logger;
        private readonly IMapper _mapper;
        private readonly IScoresRepo _scoresRepo;
        private readonly IConfiguration _config;
        private readonly IJwtUserRepo _users;

        public ScoresController(ILogger<ScoresController> logger, IScoresRepo scoresRepo, IMapper mapper,
            IConfiguration config, IJwtUserRepo users)
        {
            _logger = logger;
            _mapper = mapper;
            _scoresRepo = scoresRepo;
            _config = config;
            _users = users;
        }

        [HttpGet]
        public IActionResult GetAllScores()
        {
            var res = _scoresRepo.GetAllScores();
            if (res == null || res.Count == 0)
            {
                return (NotFound());
            }

            var output = _mapper.Map<List<ScoresReadDto>>(res);

            return Ok(output);
        }

        [HttpGet("{id}")]
        public IActionResult GetOneResult(int id)
        {
            var res = _scoresRepo.GetOneScores(id);

            if (res == null)
            {
                return (NotFound());
            }

            var output = _mapper.Map<ScoresReadDto>(res);

            return Ok(output);
        }

        [HttpGet("teamData/{teamName}")]
        public IActionResult GetTeamResults(string teamName)
        {
            var res = _scoresRepo.GetAllTeamScores(teamName);

            if (res == null || res.Count == 0)
            {
                return NotFound();
            }
            var output = _mapper.Map<List<ScoresReadDto>>(res);
            return Ok(output);

        }

        [HttpGet("scoresAfter/{after}")]
        public IActionResult GetResultsAfter(string after)
        {
            string pattern = @"\d{2}-\d{2}-\d{2}";

            if (!Regex.IsMatch(after, pattern))
            {
                return BadRequest();
            }

            DateTime date;

            DateTime.TryParse(after, out date);

            var res = _scoresRepo.GetAllScoresAfter(date);

            if (res == null || res.Count == 0)
            {
                return NotFound();
            }

            var output = _mapper.Map<List<ScoresReadDto>>(res);

            return Ok(output);
        }

        [AllowAnonymous]
        [HttpGet("jwttoken/{username}")]
        public IActionResult GetJwtToken(string username)
        {
            var res = _users.GetOneUser(username);
            if (res != null && Request.Headers.ContainsKey("password"))
            {
                if (username == res.Email)
                {
                    StringValues vals;
                    Request.Headers.TryGetValue("password",out vals);
                    string password = vals.ToArray()[0].ToString();

                    if (res.Password==password)
                    {
                        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("Jwt:Key").Value));
                        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                        var token = new JwtSecurityToken(_config.GetSection("Jwt:Issuer").Value,
                          _config.GetSection("Jwt:Issuer").Value,
                          null,
                          expires: DateTime.Now.AddMinutes(59),
                          signingCredentials: credentials);

                        return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                    }
                    else
                    {
                        return Unauthorized();
                    }
                }
                else
                {
                    return Unauthorized();
                }
            }
            else
            {
                return NotFound();
            }
        }
    }
}
