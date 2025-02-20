using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ToDo.Data.Services.ToDoItem.Commands;

public class DeleteToDoItem : IRequest<Result>
{
    public int Id { get; set; }
}

internal class DeleteToDoItemHandler : IRequestHandler<DeleteToDoItem, Result>
{
    private readonly AppDbContext _dbContext;

    private readonly ILogger<DeleteToDoItemHandler> _logger;

    public DeleteToDoItemHandler(AppDbContext dbContext, ILogger<DeleteToDoItemHandler> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<Result> Handle(DeleteToDoItem request, CancellationToken cancellationToken)
    {
        try
        {
            var entity = await _dbContext.ToDoItems.SingleOrDefaultAsync(p => p.Id.Equals(request.Id), cancellationToken);

            if (entity == null)
            {
                return Result.NotFound();
            }

            _dbContext.ToDoItems.Remove(entity);

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
