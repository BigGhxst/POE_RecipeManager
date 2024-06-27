using System.Windows;
using System.Windows.Media;

namespace RecipeManager
{
    public partial class RecipeDetailsWindow : Window
    {
        public RecipeDetailsWindow(Recipe recipe)
        {
            InitializeComponent();
            RecipeNameTextBlock.Text = recipe.Name;
            IngredientsListBox.ItemsSource = recipe.Ingredients;
            StepsListBox.ItemsSource = recipe.Steps;

            int totalCalories = recipe.GetTotalCalories();
            if (totalCalories > 300)
            {
                TotalCaloriesTextBlock.Text = $"WARNING!! Your total calories: {totalCalories}";
                TotalCaloriesTextBlock.Foreground = Brushes.Red;
            }
            else
            {
                TotalCaloriesTextBlock.Text = $"Your total calories: {totalCalories}";
                TotalCaloriesTextBlock.Foreground = Brushes.Green;
            }
        }
    }
}
