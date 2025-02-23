using MediatR;
using Microsoft.EntityFrameworkCore;
using ToDo.Data.Models;

namespace ToDo.Data.Services.ToDoItem.Queries;

public class GetToDoItemsByListId : IRequest<Result<List<ToDoItemModel>>>
{
    public int ToDoListId { get; set; }
}

internal class GetToDoItemsByListIdHandler : IRequestHandler<GetToDoItemsByListId, Result<List<ToDoItemModel>>>
{
    private readonly AppDbContext _dbContext;

    private readonly ILogger<GetToDoItemsByListIdHandler> _logger;

    public GetToDoItemsByListIdHandler(AppDbContext dbContext, ILogger<GetToDoItemsByListIdHandler> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<Result<List<ToDoItemModel>>> Handle(GetToDoItemsByListId request, CancellationToken cancellationToken)
    {
        try
        {
            return await _dbContext.ToDoItems
                .Where(p => p.ToDoListId.Equals(request.ToDoListId))
                .ProjectToModel()
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error handling request: {request}", request);
            return Result.ServerError();
        }
    }
}
