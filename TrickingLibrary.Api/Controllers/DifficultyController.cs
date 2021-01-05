﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrickingLibrary.Data;
using TrickingLibrary.Models;

namespace TrickingLibrary.Controllers
{
    [ApiController]
    [Route("api/difficulties")]
    public class DifficultyController : ControllerBase
    {
        private readonly AppDbContext _ctx;

        public DifficultyController(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        // /api/tricks
        [HttpGet]
        public IEnumerable<Difficulty> All() => _ctx.Difficulties.ToList();

        // /api/tricks/{id}
        [HttpGet("{id}")]
        public Difficulty Get(string id) =>
            _ctx.Difficulties.FirstOrDefault(x => x.Id.Equals(id, StringComparison.InvariantCultureIgnoreCase));

        [HttpGet("{id}/tricks")]
        public IEnumerable<Trick> ListDifficultyTricks(string id) => _ctx.Tricks
            .Where(x => x.Difficulty.Equals(id, StringComparison.InvariantCultureIgnoreCase))
            .ToList();

        [HttpPost]
        public async Task<Difficulty> Create([FromBody] Difficulty difficulty)
        {
            difficulty.Id = difficulty.Name.Replace(" ", "-").ToLowerInvariant();
            _ctx.Add(difficulty);
            await _ctx.SaveChangesAsync();
            return difficulty;
        }
    }
}