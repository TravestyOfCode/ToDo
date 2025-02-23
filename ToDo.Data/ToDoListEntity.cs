using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ToDo.Data;

internal class ToDoListEntity
{
    public int Id { get; set; }

    public required string UserId { get; set; }

    public AppUser? User { get; set; }

    public required string Title { get; set; }

    public bool IsCompleted { get; set; }

    public List<ToDoItemEntity>? Items { get; set; }
}

internal class ToDoListEntityConfiguration : IEntityTypeConfiguration<ToDoListEntity>
{
    public void Configure(EntityTypeBuilder<ToDoListEntity> builder)
    {
        builder.HasOne(p => p.User)
            .WithMany()
            .HasPrincipalKey(p => p.Id)
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired(true);
    }
}
