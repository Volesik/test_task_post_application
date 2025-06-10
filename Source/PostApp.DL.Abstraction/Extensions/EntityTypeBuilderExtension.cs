using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PostApp.DL.Abstraction.Interfaces;

namespace PostApp.DL.Abstraction.Extensions;

public static class EntityTypeBuilderExtension
{
    public static void ConfigureBaseProperties<T>(
        this EntityTypeBuilder<T> entityTypeBuilder)
        where T : class, IBaseEntity
    {
        entityTypeBuilder.HasIndex(entity => entity.Id);
        
        entityTypeBuilder
            .Property(entity => entity.CreatedWhen)
            .HasColumnType("timestamp with time zone")
            .HasDefaultValueSql("NOW()")
            .ValueGeneratedOnAdd();

        entityTypeBuilder
            .Property(entity => entity.UpdatedWhen)
            .HasColumnType("timestamp with time zone")
            .HasDefaultValueSql("NOW()")
            .ValueGeneratedOnAdd();

    }
}