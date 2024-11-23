using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Domain.Enums;

namespace Persistence
{
    public class Seed
    {
        public static async Task SeedData(DataContext context)
        {
            // Check if any data already exists
            if (context.Ingredients.Any() || context.Recipes.Any() || context.Measurements.Any() || context.NutritionalInfos.Any())
                return;

            // Seed Ingredients
            var ingredients = new List<Ingredient>
            {
                new Ingredient
                {
                    Id = Guid.NewGuid(),
                    Name = "Flour",
                    MeasuredIn = Categories.MeasuredIn.Weight,
                    IngredientCategory = Categories.IngredientCategory.Baking,
                    PricePerPackage = 2.50m,
                    MeasurementsPerPackage = 1000
                },
                new Ingredient
                {
                    Id = Guid.NewGuid(),
                    Name = "Milk",
                    MeasuredIn = Categories.MeasuredIn.Volume,
                    IngredientCategory = Categories.IngredientCategory.Dairy,
                    PricePerPackage = 1.20m,
                    MeasurementsPerPackage = 1000
                },
                new Ingredient
                {
                    Id = Guid.NewGuid(),
                    Name = "Eggs",
                    MeasuredIn = Categories.MeasuredIn.Each,
                    IngredientCategory = Categories.IngredientCategory.Dairy,
                    PricePerPackage = 3.00m,
                    MeasurementsPerPackage = 12
                },
                new Ingredient
                {
                    Id = Guid.NewGuid(),
                    Name = "Sugar",
                    MeasuredIn = Categories.MeasuredIn.Weight,
                    IngredientCategory = Categories.IngredientCategory.Baking,
                    PricePerPackage = 1.50m,
                    MeasurementsPerPackage = 500
                }
            };

            // Seed NutritionalInfo (linked to Ingredients)
            var nutritionalInfos = new List<NutritionalInfo>
            {
                new NutritionalInfo
                {
                    Id = Guid.NewGuid(),
                    IngredientId = ingredients[0].Id, // Flour
                    Calories = 364,
                    Carbs = 76,
                    Fat = 1,
                    Protein = 10
                },
                new NutritionalInfo
                {
                    Id = Guid.NewGuid(),
                    IngredientId = ingredients[1].Id, // Milk
                    Calories = 42,
                    Carbs = 5,
                    Fat = 1,
                    Protein = 3
                },
                new NutritionalInfo
                {
                    Id = Guid.NewGuid(),
                    IngredientId = ingredients[2].Id, // Eggs
                    Calories = 155,
                    Carbs = 1,
                    Fat = 11,
                    Protein = 13
                },
                new NutritionalInfo
                {
                    Id = Guid.NewGuid(),
                    IngredientId = ingredients[3].Id, // Sugar
                    Calories = 387,
                    Carbs = 100,
                    Fat = 0,
                    Protein = 0
                }
            };

            // Seed Recipes
            var recipes = new List<Recipe>
            {
                new Recipe
                {
                    Id = Guid.NewGuid(),
                    Name = "Pancakes",
                    RecipeCategory = Categories.RecipeCategory.Breakfast,
                    ServingsPerRecipe = 4,
                    Instructions = new List<string>
                    {
                        "Mix flour, milk, and eggs.",
                        "Add sugar to taste.",
                        "Cook on a griddle until golden brown."
                    }
                },
                new Recipe
                {
                    Id = Guid.NewGuid(),
                    Name = "Scrambled Eggs",
                    RecipeCategory = Categories.RecipeCategory.Breakfast,
                    ServingsPerRecipe = 2,
                    Instructions = new List<string>
                    {
                        "Crack eggs into a bowl and whisk.",
                        "Add a splash of milk and salt.",
                        "Cook on low heat while stirring until fluffy."
                    }
                }
            };

            // Seed Measurements (linking Ingredients to Recipes)
            var measurements = new List<Measurement>
            {
                new Measurement
                {
                    Id = Guid.NewGuid(),
                    RecipeId = recipes[0].Id, // Pancakes
                    IngredientId = ingredients[0].Id, // Flour
                    Amount = 200
                },
                new Measurement
                {
                    Id = Guid.NewGuid(),
                    RecipeId = recipes[0].Id, // Pancakes
                    IngredientId = ingredients[1].Id, // Milk
                    Amount = 300
                },
                new Measurement
                {
                    Id = Guid.NewGuid(),
                    RecipeId = recipes[0].Id, // Pancakes
                    IngredientId = ingredients[2].Id, // Eggs
                    Amount = 2
                },
                new Measurement
                {
                    Id = Guid.NewGuid(),
                    RecipeId = recipes[0].Id, // Pancakes
                    IngredientId = ingredients[3].Id, // Sugar
                    Amount = 50
                },
                new Measurement
                {
                    Id = Guid.NewGuid(),
                    RecipeId = recipes[1].Id, // Scrambled Eggs
                    IngredientId = ingredients[2].Id, // Eggs
                    Amount = 4
                },
                new Measurement
                {
                    Id = Guid.NewGuid(),
                    RecipeId = recipes[1].Id, // Scrambled Eggs
                    IngredientId = ingredients[1].Id, // Milk
                    Amount = 50
                }
            };

            // Add to context
            await context.Ingredients.AddRangeAsync(ingredients);
            await context.NutritionalInfos.AddRangeAsync(nutritionalInfos);
            await context.Recipes.AddRangeAsync(recipes);
            await context.Measurements.AddRangeAsync(measurements);

            // Save changes to database
            await context.SaveChangesAsync();
        }
    }
}
