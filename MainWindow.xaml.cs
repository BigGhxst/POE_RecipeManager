using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace RecipeManager
{
    public partial class MainWindow : Window
    {
        private List<Recipe> recipes = new List<Recipe>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddRecipeButton_Click(object sender, RoutedEventArgs e)
        {
            var addRecipeWindow = new AddRecipeWindow(recipes);
            addRecipeWindow.ShowDialog();
        }

        private void DisplayRecipesButton_Click(object sender, RoutedEventArgs e)
        {
            RecipesListBox.ItemsSource = recipes.OrderBy(r => r.Name).ToList();
        }

        private void ScaleRecipeButton_Click(object sender, RoutedEventArgs e)
        {
            if (RecipesListBox.SelectedItem is Recipe selectedRecipe)
            {
                double factor = PromptForScalingFactor();
                if (factor > 0)
                {
                    selectedRecipe.ScaleRecipe(factor);
                    MessageBox.Show("Recipe scaled successfully.");
                    RecipesListBox.ItemsSource = recipes.OrderBy(r => r.Name).ToList();
                }
            }
            else
            {
                MessageBox.Show("Please select a recipe to scale.");
            }
        }

        private void ResetQuantitiesButton_Click(object sender, RoutedEventArgs e)
        {
            if (RecipesListBox.SelectedItem is Recipe selectedRecipe)
            {
                selectedRecipe.ResetQuantities();
                MessageBox.Show("Quantities reset successfully.");
                RecipesListBox.ItemsSource = recipes.OrderBy(r => r.Name).ToList();
            }
            else
            {
                MessageBox.Show("Please select a recipe to reset quantities.");
            }
        }

        private void ClearDataButton_Click(object sender, RoutedEventArgs e)
        {
            recipes.Clear();
            RecipesListBox.ItemsSource = null;
            MessageBox.Show("All recipe data has been cleared.");
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            string ingredientFilter = FilterTextBox.Text.ToLower();
            string foodGroupFilter = (FoodGroupComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
            int.TryParse(MaxCaloriesTextBox.Text, out int maxCaloriesFilter);

            var filteredRecipes = recipes.Where(r =>
                (string.IsNullOrWhiteSpace(ingredientFilter) || r.Ingredients.Any(i => i.Name.ToLower().Contains(ingredientFilter))) &&
                (foodGroupFilter == "All" || r.Ingredients.Any(i => i.FoodGroup == foodGroupFilter)) &&
                (maxCaloriesFilter == 0 || r.GetTotalCalories() <= maxCaloriesFilter)).OrderBy(r => r.Name).ToList();

            RecipesListBox.ItemsSource = filteredRecipes;
        }

        private void RecipesListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (RecipesListBox.SelectedItem is Recipe selectedRecipe)
            {
                var recipeDetailsWindow = new RecipeDetailsWindow(selectedRecipe);
                recipeDetailsWindow.ShowDialog();
            }
        }

        private double PromptForScalingFactor()
        {
            var prompt = new PromptWindow("Enter scaling factor (0.5, 2, or 3):");
            prompt.ShowDialog();
            if (double.TryParse(prompt.ResponseText, out double factor) && (factor == 0.5 || factor == 2 || factor == 3))
            {
                return factor;
            }
            MessageBox.Show("Invalid scaling factor. Please enter 0.5, 2, or 3.");
            return -1;
        }
    }
}
