namespace ToDo.Data;

public class ToDoItemEntity
{
    public int Id { get; set; }

    public int ToDoListId { get; set; }

    public required ToDoListEntity ToDoList { get; set; }

    public bool IsCompleted { get; set; }

    public required string Description { get; set; }

    public DateOnly? DueBy { get; set; }
}