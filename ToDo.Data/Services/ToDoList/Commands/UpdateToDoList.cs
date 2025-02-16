using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ToDo.Data.Services.ToDoList.Commands;

public class UpdateToDoList : IRequest<Result>
{
    public int Id { get; set; }

    public required string Title { get; set; }

    public bool IsCompleted { get; set; }
}

internal class UpdateToDoListHandler : IRequestHandler<UpdateToDoList, Result>
{
    private readonly AppDbContext _dbContext;

    private readonly ILogger<UpdateToDoListHandler> _logger;

    public UpdateToDoListHandler(AppDbContext dbContext, ILogger<UpdateToDoListHandler> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<Result> Handle(UpdateToDoList request, CancellationToken cancellationToken)
    {
        try
        {
            var entity = await _dbContext.ToDoLists.SingleOrDefaultAsync(p => p.Id.Equals(request.Id), cancellationToken);

            if (entity == null)
            {
                return Result.NotFound();
            }

            entity.Title = request.Title;
            entity.IsCompleted = request.IsCompleted;

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
