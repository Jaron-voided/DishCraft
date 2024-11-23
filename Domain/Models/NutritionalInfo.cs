using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;

public class NutritionalInfo
{
    [Key]
    public Guid Id { get; set; }

    [ForeignKey("Ingredient")]
    public Guid IngredientId { get; set; } // Foreign Key

    public double Calories { get; set; }
    public double Carbs { get; set; }
    public double Fat { get; set; }
    public double Protein { get; set; }

    public Ingredient Ingredient { get; set; } // Navigation property
}