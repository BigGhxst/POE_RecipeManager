using System;
using System.Collections.Generic;
using System.Text;

namespace RecipeManager
{
    public delegate void RecipeNotification(string recipeName);

    public class Recipe
    {
        public string Name { get; }
        public List<Ingredient> Ingredients { get; } = new List<Ingredient>();
        public List<Step> Steps { get; } = new List<Step>(); // Use Step class for tickable steps
        private int totalCalories;

        public Recipe(string name)
        {
            Name = name;
        }

        public int GetTotalCalories()
        {
            int total = 0;
            foreach (var ingredient in Ingredients)
            {
                total += ingredient.Calories;
            }
            return total;
        }

        public event RecipeNotification NotifyCalorieExceedance;

        public void AddIngredient(string name, string quantity, string unit, int calories, string foodGroup)
        {
            Ingredients.Add(new Ingredient(name, quantity, unit, calories, foodGroup));
            totalCalories += calories;

            if (totalCalories > 300)
            {
                NotifyCalorieExceedance?.Invoke(Name);
            }
        }

        public void AddIngredient(Ingredient ingredient)
        {
            Ingredients.Add(ingredient);
            totalCalories += ingredient.Calories;

            if (totalCalories > 300)
            {
                NotifyCalorieExceedance?.Invoke(Name);
            }
        }

        public void ScaleRecipe(double factor)
        {
            foreach (var ingredient in Ingredients)
            {
                ingredient.ScaleQuantity(factor);
            }

            totalCalories = CalculateTotalCalories();
        }

        public void ResetQuantities()
        {
            foreach (var ingredient in Ingredients)
            {
                ingredient.ResetQuantity();
            }

            totalCalories = CalculateTotalCalories();
        }

        private int CalculateTotalCalories()
        {
            int total = 0;
            foreach (var ingredient in Ingredients)
            {
                total += ingredient.Calories;
            }
            return total;
        }

        public string GetRecipeDetails()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Recipe: {Name}");
            sb.AppendLine("Ingredients:");
            foreach (var ingredient in Ingredients)
            {
                sb.AppendLine($"{ingredient}");
            }

            sb.AppendLine("Steps:");
            foreach (var step in Steps)
            {
                sb.AppendLine($"{step.Description}");
            }

            if (totalCalories > 300)
            {
                sb.AppendLine("WARNING!! WARNING!! YOUR TOTAL CALORIES HAVE EXCEEDED 300");
            }
            else
            {
                sb.AppendLine($"Your Total Calories: {totalCalories}");
            }

            return sb.ToString();
        }

        public override string ToString()
        {
            return Name;
        }
    }

    public class Step
    {
        public string Description { get; set; }
        public bool IsCompleted { get; set; }

        public Step(string description)
        {
            Description = description;
            IsCompleted = false;
        }
    }
}
