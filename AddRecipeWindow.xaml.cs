using System.Collections.Generic;
using System.Windows;

namespace RecipeManager
{
    public partial class AddRecipeWindow : Window
    {
        private List<Recipe> recipes;
        private List<Ingredient> ingredients = new List<Ingredient>();
        private List<Step> steps = new List<Step>();

        public AddRecipeWindow(List<Recipe> recipes)
        {
            InitializeComponent();
            this.recipes = recipes;
        }

        private void AddIngredientButton_Click(object sender, RoutedEventArgs e)
        {
            var addIngredientWindow = new AddIngredientWindow(ingredients);
            addIngredientWindow.ShowDialog();
            IngredientsListBox.ItemsSource = null;
            IngredientsListBox.ItemsSource = ingredients;
        }

        private void AddStepButton_Click(object sender, RoutedEventArgs e)
        {
            string step = StepTextBox.Text;
            if (!string.IsNullOrWhiteSpace(step))
            {
                steps.Add(new Step(step));
                StepsListBox.ItemsSource = null;
                StepsListBox.ItemsSource = steps;
                StepTextBox.Clear();
            }
        }

        private void SaveRecipeButton_Click(object sender, RoutedEventArgs e)
        {
            string recipeName = RecipeNameTextBox.Text;
            if (string.IsNullOrWhiteSpace(recipeName))
            {
                MessageBox.Show("Please enter a recipe name.");
                return;
            }

            var recipe = new Recipe(recipeName);
            foreach (var ingredient in ingredients)
            {
                recipe.AddIngredient(ingredient);
            }
            foreach (var step in steps)
            {
                recipe.Steps.Add(step);
            }
            recipes.Add(recipe);

            MessageBox.Show("Recipe added successfully!");
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
