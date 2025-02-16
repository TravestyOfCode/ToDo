using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ToDo.Data.Services.ToDoList.Commands;

public class DeleteToDoList : IRequest<Result>
{
    public int Id { get; set; }
}

internal class DeleteToDoListHandler : IRequestHandler<DeleteToDoList, Result>
{
    private readonly AppDbContext _dbContext;

    private readonly ILogger<DeleteToDoListHandler> _logger;

    public DeleteToDoListHandler(AppDbContext dbContext, ILogger<DeleteToDoListHandler> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<Result> Handle(DeleteToDoList request, CancellationToken cancellationToken)
    {
        try
        {
            var entity = await _dbContext.ToDoLists.SingleOrDefaultAsync(p => p.Id.Equals(request.Id), cancellationToken);

            if (entity == null)
            {
                return Result.NotFound();
            }

            _dbContext.ToDoLists.Remove(entity);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Result.Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error handling request: {request}", request);
            return Result.ServerError();
        }
    }
}
