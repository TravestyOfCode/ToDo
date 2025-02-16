namespace ToDo.Data.Models;

public class ToDoListModel
{
    public int Id { get; set; }

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
