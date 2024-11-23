using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Ingredient> Ingredients { get; set; }
    public DbSet<Recipe> Recipes { get; set; }
    public DbSet<Measurement> Measurements { get; set; }
    public DbSet<NutritionalInfo> NutritionalInfos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure NutritionalInfo relationship
        modelBuilder.Entity<NutritionalInfo>()
            .HasOne(n => n.Ingredient)
            .WithOne(i => i.Nutrition)
            .HasForeignKey<NutritionalInfo>(n => n.IngredientId)
            .OnDelete(DeleteBehavior.Cascade); // Delete NutritionalInfo when Ingredient is deleted

        // Configure Measurement relationships
        modelBuilder.Entity<Measurement>(entity =>
        {
            entity.HasOne(m => m.Recipe)                  // Measurement -> Recipe
                .WithMany(r => r.Measurements)
                .HasForeignKey(m => m.RecipeId)
                .OnDelete(DeleteBehavior.Cascade);      // Delete Measurements when Recipe is deleted

            entity.HasOne(m => m.Ingredient)              // Measurement -> Ingredient
                .WithMany()                             // No reverse navigation in Ingredient
                .HasForeignKey(m => m.IngredientId)
                .OnDelete(DeleteBehavior.Restrict);     // Prevent deleting Ingredients used in Measurements
        });

        // Configure Recipe InstructionsJson storage as "Instructions"
        modelBuilder.Entity<Recipe>(entity =>
        {
            entity.Property(r => r.InstructionsJson).HasColumnName("Instructions");
        });
    }
}