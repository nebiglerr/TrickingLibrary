using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Channels;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TrickingLibrary.BackgroundServices;
using TrickingLibrary.Data;
using TrickingLibrary.Models;

namespace TrickingLibrary.Controllers
{
    [ApiController]
    [Route("api/submissions")]
    public class SubmissionsController : ControllerBase
    {
        private readonly AppDbContext _ctx;

        public SubmissionsController(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        [HttpGet]
        public IEnumerable<Submission> All() => _ctx.Submissions.Where(x => x.VideoProcessed).ToList();

        [HttpGet("{id}")]
        public Submission Get(int id) => _ctx.Submissions.FirstOrDefault(x => x.Id.Equals(id));


        [HttpPost]
        public async Task<Submission> Create([FromBody] Submission submission)
        {
            _ctx.Add(submission);
            await _ctx.SaveChangesAsync();
            return submission;
        }

        [HttpPut]
        public async Task<Submission> Update([FromBody] Submission submission,
            [FromServices] Channel<EditVideoMessage> channel)
        {
            if (submission.Id == 0)
            {
                return null;
            }

            submission.VideoProcessed = false;
            _ctx.Add(submission);
            await _ctx.SaveChangesAsync();
            await channel.Writer.WriteAsync(new EditVideoMessage
            {
                SubmissionId = submission.Id,
                InputPath = submission.Video,
            });
            return submission;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var submission = _ctx.Submissions.FirstOrDefault(x => x.Id.Equals(id));
            if (submission == null)
            {
                return NotFound();
            }

            submission.Deleted = true;
            await _ctx.SaveChangesAsync();
            return Ok();
        }
    }
}