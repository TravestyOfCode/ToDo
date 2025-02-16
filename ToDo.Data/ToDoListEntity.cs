namespace ToDo.Data;

internal class ToDoListEntity
{
    public int Id { get; set; }

    public required string Title { get; set; }

    public bool IsCompleted { get; set; }

    public List<ToDoItemEntity>? Items { get; set; }
}
