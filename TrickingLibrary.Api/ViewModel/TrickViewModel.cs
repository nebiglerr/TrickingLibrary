using System;
using System.Linq;
using System.Linq.Expressions;
using TrickingLibrary.Models;

namespace TrickingLibrary.ViewModel
{
    public static class TrickViewModel
    {
        public static Expression<Func<Trick, object>> Default => trick => new
        {
        trick.Id,
        trick.Name,
        trick.Description,
        trick.Difficulty,
        Category = trick.TrickCategories.Select(x => x.CategoryId)
        };
    }
}