using MediatR;
using ToDo.Data.Models;

namespace ToDo.Data.Services.ToDoList.Commands;

public class CreateToDoList : IRequest<Result<ToDoListModel>>
{
    public required string Title { get; set; }

    public bool IsCompleted { get; set; }
}

internal class CreateToDoListHandler : IRequestHandler<CreateToDoList, Result<ToDoListModel>>
{
    private readonly AppDbContext _dbContext;

    private readonly ILogger<CreateToDoListHandler> _logger;

    public CreateToDoListHandler(AppDbContext dbContext, ILogger<CreateToDoListHandler> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<Result<ToDoListModel>> Handle(CreateToDoList request, CancellationToken cancellationToken)
    {
        try
        {
            var entity = _dbContext.ToDoLists.Add(new ToDoListEntity()
            {
                Title = request.Title,
                IsCompleted = request.IsCompleted
            });

            await _dbContext.SaveChangesAsync(cancellationToken);

            return ToDoListModel.FromEntity(entity.Entity);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error handling request: {request}", request);
            return Result.ServerError();
        }
    }
}
