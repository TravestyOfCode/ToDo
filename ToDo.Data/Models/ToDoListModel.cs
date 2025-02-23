namespace ToDo.Data.Models;

public class ToDoListModel
{
    public int Id { get; set; }

    public required string UserId { get; set; }

    public required string Title { get; set; }

    public bool IsCompleted { get; set; }

    public List<ToDoItemModel>? Items { get; set; }

    internal static ToDoListModel FromEntity(ToDoListEntity entity)
    {
        return new ToDoListModel()
        {
            Id = entity.Id,
            Title = entity.Title,
            IsCompleted = entity.IsCompleted,
            UserId = entity.UserId,
            Items = entity.Items?.Select(p => new ToDoItemModel()
            {
                Description = p.Description,
                DueBy = p.DueBy,
                Id = p.Id,
                IsCompleted = p.IsCompleted,
                ToDoListId = p.ToDoListId
            }).ToList()
        };
    }
}

internal static class ToDoListModelExtensions
{
    public static IQueryable<ToDoListModel> ProjectToModel(this IQueryable<ToDoListEntity> source)
    {
        return source.Select(p => new ToDoListModel()
        {
            Id = p.Id,
            IsCompleted = p.IsCompleted,
            Title = p.Title,
            UserId = p.UserId,
            Items = p.Items!.ToModelList()
        });
    }
}
