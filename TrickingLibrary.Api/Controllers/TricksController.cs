﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrickingLibrary.Data;
using TrickingLibrary.Form;
using TrickingLibrary.Models;
using TrickingLibrary.ViewModel;

namespace TrickingLibrary.Controllers
{
    [ApiController]
    [Route("api/tricks")]
    public class TricksController : ControllerBase
    {
        private readonly AppDbContext _ctx;

        public TricksController(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        // /api/tricks
        [HttpGet]
        public IEnumerable<object> All() => _ctx.Tricks.Select(TrickViewModel.Projection).ToList();   
        
        [HttpGet("test")]
        [Authorize(Policy = IdentityServerConstants.LocalApi.PolicyName)]
        public string Test() =>"test";

             
        [HttpGet("mod")]
        [Authorize(Policy = TrickingLibraryConstants.Policies.Mod)]
        public string Mod() =>"mod";
        
        [HttpGet("{id}")]
        public object Get(string id) =>
            _ctx.Tricks
                .Where(x => x.Id.Equals(id, StringComparison.InvariantCultureIgnoreCase))
                .Select(TrickViewModel.Projection)
                .FirstOrDefault();

        [HttpGet("{trickId}/submissions")]
        public IEnumerable<Submission> ListSubmissionsForTrick(string trickId) => _ctx.Submissions
            .Include(x => x.Video).Where(x => x.TrickId.Equals(trickId, StringComparison.InvariantCultureIgnoreCase)).ToList();

        [HttpPost]
        public async Task<object> Create([FromBody] TrickForm trickForm)
        {
            var trick = new Trick
            {
                Id = trickForm.Name.Replace(" ", "-").ToLowerInvariant(),
                Name = trickForm.Name,
                Description = trickForm.Description,
                Difficulty = trickForm.Difficulty,
                TrickCategories = trickForm.Categories.Select(x => new TrickCategory
                {
                    CategoryId = x
                }).ToList()
            };
            _ctx.Add(trick);
            await _ctx.SaveChangesAsync();
            return TrickViewModel.Create(trick);
        }

        [HttpPut]
        public async Task<object> Update([FromBody] Trick trick)
        {
            if (string.IsNullOrEmpty(trick.Id))
            {
                return null;
            }

            _ctx.Add(trick);
            await _ctx.SaveChangesAsync();
            return TrickViewModel.Create(trick);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var trick = _ctx.Tricks.FirstOrDefault(x => x.Id.Equals(id));
            trick.Deleted = true;
            await _ctx.SaveChangesAsync();
            return Ok();
        }
    }
}