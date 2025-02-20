using MediatR;
using Microsoft.EntityFrameworkCore;
using ToDo.Data.Models;

namespace ToDo.Data.Services.ToDoItem.Commands;

public class CreateToDoItem : IRequest<Result<ToDoItemModel>>
{
    public int ToDoListId { get; set; }

    public bool IsCompleted { get; set; }

    public required string Description { get; set; }

    public DateOnly? DueBy { get; set; }
}

internal class CreateToDoItemHandler : IRequestHandler<CreateToDoItem, Result<ToDoItemModel>>
{
    private readonly AppDbContext _dbContext;

    private readonly ILogger<CreateToDoItemHandler> _logger;

    public CreateToDoItemHandler(AppDbContext dbContext, ILogger<CreateToDoItemHandler> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<Result<ToDoItemModel>> Handle(CreateToDoItem request, CancellationToken cancellationToken)
    {
        try
        {
            var list = await _dbContext.ToDoLists.SingleOrDefaultAsync(p => p.Id.Equals(request.ToDoListId), cancellationToken);

            if (list == null)
            {
                return Result.NotFound();
            }

            var entity = _dbContext.ToDoItems.Add(new ToDoItemEntity()
            {
                Description = request.Description,
                DueBy = request.DueBy,
                IsCompleted = request.IsCompleted,
                ToDoListId = request.ToDoListId,
                ToDoList = list
            });

            await _dbContext.SaveChangesAsync(cancellationToken);

            return ToDoItemModel.FromEntity(entity.Entity);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error handling request: {request}", request);
            return Result.ServerError();
        }
    }
}
