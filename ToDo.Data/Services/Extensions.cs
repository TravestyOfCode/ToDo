using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ToDo.Data.Services;

public static class Extensions
{
    public static IQueryable<T> IncludeIf<T, TProperty>(this IQueryable<T> source, bool condition, Expression<Func<T, TProperty>> path) where T : class => condition ? source.Include(path) : source;
}
