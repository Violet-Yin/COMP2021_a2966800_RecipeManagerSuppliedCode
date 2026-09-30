using System.Collections.Generic;
using RecipeManagement.Core;

namespace RecipeManagement.Tests;

/// <summary>
/// Example tests from the assignment specification. Add your own tests as you work.
/// </summary>
public sealed class RecipeManagerTests
{
    // [Fact]
    // public void Constructor_BuildsRecipeDictionary()
    // {
    //     var manager = CreateManager();
    //     Assert.Equal(2, manager.RecipeCount);
    //     Assert.Equal("Recipe A", manager.FindRecipe(10)?.Title);
    // }

    // [Fact]
    // public void InstructionsAreCompletedInFileOrder()
    // {
    //     var manager = CreateManager();
    //     Assert.True(manager.StartCooking(10));
    //     Assert.Equal("First step", manager.PeekNextInstruction());
    //     Assert.Equal("First step", manager.CompleteNextInstruction());
    //     Assert.Equal("Second step", manager.PeekNextInstruction());
    // }

    // [Fact]
    // public void RemovedRecipesAreRestoredLastInFirstOut()
    // {
    //     var manager = CreateManager();
    //     manager.AddRecipeToCookingPlan(10);
    //     manager.AddRecipeToCookingPlan(20);
    //     manager.RemoveRecipeFromCookingPlan(10);
    //     manager.RemoveRecipeFromCookingPlan(20);
    //     Assert.Equal(20, manager.PeekLastRemovedRecipe());
    //     Assert.True(manager.RestoreLastRemovedRecipe());
    //     Assert.Equal(new[] { 20 }, manager.GetCookingPlan());
    // }

    // private static RecipeManager CreateManager()
    // {
    //     return new RecipeManager(new[]
    //     {
    //         new Recipe
    //         {
    //             Id = 10,
    //             Title = "Recipe A",
    //             Ingredients = new() { "1 apple" },
    //             Instructions = new() { "First step", "Second step" }
    //         },
    //         new Recipe
    //         {
    //             Id = 20,
    //             Title = "Recipe B"
    //         }
    //     });
    // }
    // // test the constructor
    // [Fact]
    // public void Constructor_Recipes()
    // {
    //     var catalogue = CreateCatalogue();

    //     Assert.Equal(2, catalogue.RecipeCount);
    //     Assert.NotNull(catalogue.FindRecipe(10));
    //     Assert.NotNull(catalogue.FindRecipe(20));
    // }
    // // Return true for add recipe
    // [Fact]
    // public void AddRecipe_NewRecipe()
    // {
    //     var catalogue = CreateCatalogue();
    //     var recipe = new Recipe
    //     {
    //         Id = 99,
    //         Title = "Recipe 99"
    //     };

    //     Assert.True(catalogue.AddRecipe(recipe));
    //     Assert.Equal(3, catalogue.RecipeCount);
    //     Assert.Equal("Recipe 99", catalogue.FindRecipe(99)?.Title);
    // }

    // // Duplicate recipe when add recipe
    // [Fact]
    // public void AddRecipe_RejectsDuplicateId()
    // {
    //     var catalogue = CreateCatalogue();

    //     var duplicateRecipe = new Recipe
    //     {
    //         Id = 10,
    //         Title = "Different Recipe"
    //     };

    //     Assert.False(catalogue.AddRecipe(duplicateRecipe));
    //     Assert.Equal(2, catalogue.RecipeCount);
    //     Assert.Equal("Recipe A", catalogue.FindRecipe(10)?.Title);
    // }

    // // Remove recipe true
    // [Fact]
    // public void RemoveRecipe_ExistingRecipe()
    // {
    //     var catalogue = CreateCatalogue();

    //     Assert.True(catalogue.RemoveRecipe(10));
    //     Assert.Equal(1, catalogue.RecipeCount);
    //     Assert.Null(catalogue.FindRecipe(10));
    // }
    
    // // Remove recipe false
    // [Fact]
    // public void RemoveRecipe_RejectsMissingId()
    // {
    //     var catalogue = CreateCatalogue();

    //     Assert.False(catalogue.RemoveRecipe(99));
    //     Assert.Equal(2, catalogue.RecipeCount);
    // }

    // // recipe found
    // [Fact]
    // public void FindRecipe_ExistingRecipe()
    // {
    //     var catalogue = CreateCatalogue();

    //     var recipe = catalogue.FindRecipe(10);

    //     Assert.NotNull(recipe);
    //     Assert.Equal("Recipe A", recipe.Title);
    // }

    // // recipe not found
    // [Fact]
    // public void FindRecipe_NullMissingId()
    // {
    //     var catalogue = CreateCatalogue();

    //     var recipe = catalogue.FindRecipe(99);

    //     Assert.Null(recipe);
    // }

    private static RecipeManager CreateCatalogue()
    {
        return new RecipeManager(new[]
        {
            new Recipe
            {
                Id = 10,
                Title = "Recipe A",
                Ingredients = new() { "1 apple" },
                Instructions = new() { "First step", "Second step" }
            },
            new Recipe
            {
                Id = 20,
                Title = "Recipe B",
                Ingredients = new() { "1 apple", "2 banana", "3 cherry"},
            }
        });
    }
    
    // missing recipe ID return 0 
    [Fact]
    public void AddToShoppingList_RecipeNull()
    {
        var catalogue = CreateCatalogue();
        var count = catalogue.AddIngredientsToShoppingList(99);
        
        Assert.Equal(0, count);
    }
    
    // Test items count and get shopping list
    [Fact]
    public void AddToShoppingList_GetListNCountItem()
    {
        var catalogue = CreateCatalogue();
        var recipe = catalogue.AddIngredientsToShoppingList(20);
        var ing = catalogue.GetShoppingList();
        Assert.Equal(3, recipe); 
        Assert.Contains("1 apple", ing);
        Assert.Contains("2 banana", ing);
        Assert.Contains("3 cherry", ing);  
    }

    // Add more then one recipe
    [Fact]
    public void AddToShoppingList_MutipleRecipe()
    {
        var catalogue = CreateCatalogue();
        var recipe1 = catalogue.AddIngredientsToShoppingList(20);
        var recipe2 = catalogue.AddIngredientsToShoppingList(10);
        
        Assert.True(catalogue.ShoppingItemCount == 4);  
    }

    // Empty the list
    [Fact]
    public void AddToShoppingList_ClearItem()
    {
        var catalogue = CreateCatalogue();
        catalogue.AddIngredientsToShoppingList(20);
        catalogue.ClearShoppingList();

        Assert.Equal(0, catalogue.ShoppingItemCount);
    }
}

    
