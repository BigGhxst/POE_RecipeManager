using System;

namespace RecipeManager
{
    public class Ingredient
    {
        public string Name { get; }
        public double Quantity { get; private set; }
        public string Unit { get; }
        public int Calories { get; }
        public string FoodGroup { get; }
        private double originalQuantity;

        public Ingredient(string name, string quantity, string unit, int calories, string foodGroup)
        {
            Name = name;
            Quantity = Convert.ToDouble(quantity);
            originalQuantity = Quantity;
            Unit = unit;
            Calories = calories;
            FoodGroup = foodGroup;
        }

        public void ScaleQuantity(double factor)
        {
            Quantity *= factor;
        }

        public void ResetQuantity()
        {
            Quantity = originalQuantity;
        }

        public override string ToString()
        {
            return $"{Name}: {Quantity} {Unit} ({Calories} Calories)";
        }
    }
}
