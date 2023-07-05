using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace RecipeApp
{
    public partial class MainWindow : Window
    {
        private ObservableCollection<Recipe> recipes;

        public MainWindow()
        {
            InitializeComponent();

            // Create an empty collection for recipes
            recipes = new ObservableCollection<Recipe>();

            // Set the collection as the ItemsSource of the DataGrid
            dgRecipes.ItemsSource = recipes;
        }

        private void btnAddRecipe_Click(object sender, RoutedEventArgs e)
        {
            // Create a new recipe with values from text boxes
            Recipe newRecipe = new Recipe
            {
                Name = txtName.Text,
                FoodGroup = txtFoodGroup.Text,
                Ingredients = txtIngredient.Text,
                Calories = int.Parse(txtMaxCalories.Text),
                Exceeds300Calories = int.Parse(txtMaxCalories.Text) > 300
            };

            // Add the new recipe to the collection
            recipes.Add(newRecipe);

            // Clear the text boxes
            txtName.Clear();
            txtFoodGroup.Clear();
            txtIngredient.Clear();
            txtMaxCalories.Clear();

            // Check if calories exceed 300 and notify the user
            if (newRecipe.Exceeds300Calories)
            {
                MessageBox.Show("Warning: Total calories exceed 300!");
            }
        }

        private void btnFilterIngredient_Click(object sender, RoutedEventArgs e)
        {
            string ingredient = txtFilterIngredient.Text.ToLower();
            List<Recipe> filteredRecipes = recipes.Where(r => r.Ingredients.ToLower().Contains(ingredient)).ToList();
            dgRecipes.ItemsSource = filteredRecipes;
        }


        private void btnClearFoodGroupFilter_Click(object sender, RoutedEventArgs e)
        {
            
            dgRecipes.ItemsSource = recipes;
        }

        private void btnFoodGroupFilter_Click(object sender, RoutedEventArgs e)
        {
            string foodgroup = txtFilterFoodGroup.Text.ToLower();
            List<Recipe> filteredRecipes = recipes.Where(r => r.FoodGroup.ToLower().Contains(foodgroup)).ToList();
            dgRecipes.ItemsSource = filteredRecipes;
        }

        private void btnFilterMaxCalories_Click(object sender, RoutedEventArgs e)
        {
            int maxCalories = int.Parse(txtFilterMaxCalories.Text);
            List<Recipe> filteredRecipes = recipes.Where(r => r.Calories == maxCalories).ToList();
            dgRecipes.ItemsSource = filteredRecipes;
        }
    }

    public class Recipe : INotifyPropertyChanged
    {
        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        private string foodGroup;
        public string FoodGroup
        {
            get { return foodGroup; }
            set
            {
                foodGroup = value;
                OnPropertyChanged("FoodGroup");
            }
        }

        private string ingredients;
        public string Ingredients
        {
            get { return ingredients; }
            set
            {
                ingredients = value;
                OnPropertyChanged("Ingredients");
            }
        }

        private int calories;
        public int Calories
        {
            get { return calories; }
            set
            {
                calories = value;
                OnPropertyChanged("Calories");
            }
        }

        private bool exceeds300Calories;
        public bool Exceeds300Calories
        {
            get { return exceeds300Calories; }
            set
            {
                exceeds300Calories = value;
                OnPropertyChanged("Exceeds300Calories");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
