namespace ToDo.Data.Models;

public class ToDoItemModel
{
    public int Id { get; set; }

    public int ToDoListId { get; set; }

    public bool IsCompleted { get; set; }

    public required string Description { get; set; }

    public DateOnly? DueBy { get; set; }

    internal static ToDoItemModel FromEntity(ToDoItemEntity entity)
    {
        return new ToDoItemModel()
        {
            Description = entity.Description,
            DueBy = entity.DueBy,
            Id = entity.Id,
            IsCompleted = entity.IsCompleted,
            ToDoListId = entity.ToDoListId
        };
    }
}
