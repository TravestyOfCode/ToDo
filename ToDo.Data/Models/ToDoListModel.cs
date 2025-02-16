namespace ToDo.Data.Models;

public class ToDoListModel
{
    public int Id { get; set; }

    public required string Title { get; set; }

    public bool IsCompleted { get; set; }

    public List<ToDoItemModel>? Items { get; set; }
}
