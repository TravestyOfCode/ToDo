namespace ToDo.Data.Models;

public class ToDoItemModel
{
    public int Id { get; set; }

    public int ToDoListId { get; set; }

    public bool IsCompleted { get; set; }

    public required string Description { get; set; }

    public DateOnly? DueBy { get; set; }
}
