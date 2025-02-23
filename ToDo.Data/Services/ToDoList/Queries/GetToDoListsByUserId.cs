using MediatR;
using Microsoft.EntityFrameworkCore;
using ToDo.Data.Models;

namespace ToDo.Data.Services.ToDoList.Queries;

public class GetToDoListsByUserId : IRequest<Result<List<ToDoListModel>>>
{
    public required string UserId { get; set; }

    public bool IncludeItems { get; set; }
}

internal class GetToDoListsByUserIdHandler : IRequestHandler<GetToDoListsByUserId, Result<List<ToDoListModel>>>
{
    private readonly AppDbContext _dbContext;

    private readonly ILogger<GetToDoListsByUserIdHandler> _logger;

    public GetToDoListsByUserIdHandler(AppDbContext dbContext, ILogger<GetToDoListsByUserIdHandler> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<Result<List<ToDoListModel>>> Handle(GetToDoListsByUserId request, CancellationToken cancellationToken)
    {
        try
        {
            return await _dbContext.ToDoLists
                .Where(p => p.UserId.Equals(request.UserId))
                .IncludeIf(request.IncludeItems, p => p.Items)
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
