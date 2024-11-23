using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Domain;

public class Ingredient
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    public Categories.MeasuredIn MeasuredIn { get; set; }
    public Categories.IngredientCategory IngredientCategory { get; set; }
    public decimal PricePerPackage { get; set; }
    public int MeasurementsPerPackage { get; set; }

    public NutritionalInfo Nutrition { get; set; } // Navigation property
}