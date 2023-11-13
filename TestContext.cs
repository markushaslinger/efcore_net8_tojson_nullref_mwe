using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using NodaTime;
using NodaTime.Serialization.SystemTextJson;

namespace JsonColumnTest;

public sealed class TestContext : DbContext
{
    public const string SchemaName = "jsontest";
    
    public DbSet<Student> Students { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema(SchemaName);

        var student = modelBuilder.Entity<Student>();
        student.HasKey(s => s.Id);
        student.Property(s => s.Id).ValueGeneratedOnAdd();
        student.Property(s => s.Name).IsRequired();
        student.OwnsMany(s => s.ExamGrades).ToJson();
        /*
         // this works
         student.OwnsMany(s => s.ExamGrades, builder =>
        {
            var options = new JsonSerializerOptions();
            options = options.ConfigureForNodaTime(DateTimeZoneProviders.Tzdb);
            builder.ToJson();
            builder.Property(eg => eg.Date)
                   .HasConversion(v => JsonSerializer.Serialize(v, options),
                                  v => JsonSerializer.Deserialize<LocalDate>(v, options));
        });*/
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder
            .UseNpgsql("Host=localhost;Database=postgres;Username=postgres;Password=postgres",
                       o => o.UseNodaTime())
            .LogTo(Console.WriteLine);
    }
}
