using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TrickingLibrary.Data;
using TrickingLibrary.Models;
using TrickingLibrary.ViewModel;

namespace TrickingLibrary.Controllers
{
    [ApiController]
    [Route("api/comments")]
    public class CommentController : ControllerBase
    {
        private readonly AppDbContext _ctx;

        public CommentController(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        [HttpGet("{id}/replies")]
        public IEnumerable<object> GetReplies(int id) => 
            _ctx.Comments
                .Where(x => x.ParentId == id)
                .Select(CommentViewModel.Projection)
                .ToList();

        [HttpPost("{id}/replies")]
        public async  Task<IActionResult> Replies(int id, [FromBody] Comment reply)
        {
            var comment = _ctx.Comments.FirstOrDefault(x => x.Id == id);
            var regex = new Regex(@"\B(?<tag>@[a-zA-Z0-9-_]+)");
            if (comment == null)
            {
                return NoContent();
            }
            reply.HtmlContent = regex.Matches(reply.Content)
                .Aggregate(reply.Content, (content, match) =>
                {
                    var tag = match.Groups["tag"].Value;
                    return content.Replace(tag, $"<a href=\"{tag}-user-link\">{tag}</a>");
                });
            
            comment.Replies.Add(reply);
            await _ctx.SaveChangesAsync();
            return Ok(CommentViewModel.Create(reply));

        } 
    }
}