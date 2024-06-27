using System.Collections.Generic;
using System.Windows;

namespace RecipeManager
{
    public partial class AddIngredientWindow : Window
    {
        private List<Ingredient> ingredients;

        public AddIngredientWindow(List<Ingredient> ingredients)
        {
            InitializeComponent();
            this.ingredients = ingredients;
        }

        private void SaveIngredientButton_Click(object sender, RoutedEventArgs e)
        {
            string name = IngredientNameTextBox.Text;
            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Please enter an ingredient name.");
                return;
            }

            if (!double.TryParse(QuantityTextBox.Text, out double quantity) || quantity <= 0)
            {
                MessageBox.Show("Please enter a valid quantity.");
                return;
            }

            string unit = UnitTextBox.Text;
            if (string.IsNullOrWhiteSpace(unit))
            {
                MessageBox.Show("Please enter a unit of measurement.");
                return;
            }

            if (!int.TryParse(CaloriesTextBox.Text, out int calories) || calories < 0)
            {
                MessageBox.Show("Please enter a valid number of calories.");
                return;
            }

            string foodGroup = FoodGroupTextBox.Text;
            if (string.IsNullOrWhiteSpace(foodGroup))
            {
                MessageBox.Show("Please enter a food group.");
                return;
            }

            var ingredient = new Ingredient(name, quantity.ToString(), unit, calories, foodGroup);
            ingredients.Add(ingredient);

            MessageBox.Show("Ingredient added successfully!");
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
