using MediatR;
using Microsoft.EntityFrameworkCore;
using ToDo.Data.Models;

namespace ToDo.Data.Services.ToDoItem.Queries;

public class GetToDoItemById : IRequest<Result<ToDoItemModel>>
{
    public int Id { get; set; }
}

internal class GetToDoItemByIdHandler : IRequestHandler<GetToDoItemById, Result<ToDoItemModel>>
{
    private readonly AppDbContext _dbContext;

    private readonly ILogger<GetToDoItemByIdHandler> _logger;

    public GetToDoItemByIdHandler(AppDbContext dbContext, ILogger<GetToDoItemByIdHandler> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<Result<ToDoItemModel>> Handle(GetToDoItemById request, CancellationToken cancellationToken)
    {
        try
        {
            var entity = await _dbContext.ToDoItems
                .Where(p => p.Id.Equals(request.Id))
                .ProjectToModel()
                .SingleOrDefaultAsync(cancellationToken);

            if (entity == null)
            {
                return Result.NotFound();
            }

            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error handling request: {request}", request);
            return Result.ServerError();
        }
    }
}
