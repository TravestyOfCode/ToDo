using MediatR;
using Microsoft.EntityFrameworkCore;
using ToDo.Data.Models;

namespace ToDo.Data.Services.ToDoList.Queries;

public class GetToDoListById : IRequest<Result<ToDoListModel>>
{
    public int Id { get; set; }
}

internal class GetToDoListByIdHandler : IRequestHandler<GetToDoListById, Result<ToDoListModel>>
{
    private readonly AppDbContext _dbContext;

    private readonly ILogger<GetToDoListByIdHandler> _logger;

    public GetToDoListByIdHandler(AppDbContext dbContext, ILogger<GetToDoListByIdHandler> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<Result<ToDoListModel>> Handle(GetToDoListById request, CancellationToken cancellationToken)
    {
        try
        {
            var entity = await _dbContext.ToDoLists.Where(p => p.Id.Equals(request.Id))
                .Include(p => p.Items)
                .Select(p => new ToDoListModel()
                {
                    Id = p.Id,
                    IsCompleted = p.IsCompleted,
                    Title = p.Title,
                    UserId = p.UserId,
                    Items = p.Items!.Select(i => new ToDoItemModel()
                    {
                        Id = i.Id,
                        Description = i.Description,
                        DueBy = i.DueBy,
                        IsCompleted = i.IsCompleted,
                        ToDoListId = i.ToDoListId
                    }).ToList()
                })
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
