using MediatR;
using Microsoft.EntityFrameworkCore;
using ToDo.Data.Models;

namespace ToDo.Data.Services.ToDoItem.Commands;

public class UpdateToDoItem : IRequest<Result<ToDoItemModel>>
{
    public int Id { get; set; }

    public int ToDoListId { get; set; }

    public bool IsCompleted { get; set; }

    public required string Description { get; set; }

    public DateOnly? DueBy { get; set; }
}

internal class UpdateToDoItemHandler : IRequestHandler<UpdateToDoItem, Result<ToDoItemModel>>
{
    private readonly AppDbContext _dbContext;

    private readonly ILogger<UpdateToDoItemHandler> _logger;

    public UpdateToDoItemHandler(AppDbContext dbContext, ILogger<UpdateToDoItemHandler> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<Result<ToDoItemModel>> Handle(UpdateToDoItem request, CancellationToken cancellationToken)
    {
        try
        {
            var entity = await _dbContext.ToDoItems.SingleOrDefaultAsync(p => p.Id.Equals(request.Id), cancellationToken);

            if (entity == null)
            {
                return Result.NotFound();
            }

            entity.Description = request.Description;
            entity.ToDoListId = request.ToDoListId;
            entity.IsCompleted = request.IsCompleted;
            entity.DueBy = request.DueBy;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return ToDoItemModel.FromEntity(entity);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error handling request: {request}", request);
            return Result.ServerError();
        }
    }
}
