using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Markup;

namespace RecipeManagement.Core;

/// <summary>
/// Implement this class using the five Part A collections as private fields:
/// Dictionary&lt;int, Recipe&gt;, List&lt;string&gt;, LinkedList&lt;int&gt;,
/// Stack&lt;int&gt; and Queue&lt;string&gt;.
/// </summary>
public sealed class RecipeManager : IRecipeManager
{
    // TODO Part A: add your private collection fields here.
    private Dictionary<int, Recipe> _recipesCatalogue = new Dictionary<int, Recipe>();
    private List<string> _shoppingList = new List<string>();
    private LinkedList<int> _cookingPlan = new LinkedList<int>();
    private Stack<int> _removedCookingPlan = new Stack<int>();
    private Queue<string> _cookingInstruction = new Queue<string>();

    public RecipeManager(IEnumerable<Recipe> recipes)
    {
        // check recipes isnt null
        if (recipes == null)
        {
            throw new ArgumentNullException();
        }
        // Validate ID and Title 
        foreach (var recipe in recipes)
        {
            if (recipe.Id <= 0)
            {
                throw new Exception("Recipe ID must be positive");
            }
            else if (string.IsNullOrWhiteSpace(recipe.Title))
            {
                throw new ArgumentNullException("Title cant be blank");
            }
            else if (_recipesCatalogue.ContainsKey(recipe.Id))
            {
                throw new Exception("Recipe ID must be unique");
            }
            _recipesCatalogue.Add(recipe.Id, recipe);
        }
    }

    public int RecipeCount => _recipesCatalogue.Count;
    public int ShoppingItemCount => _shoppingList.Count;
    public int CookingPlanCount => _cookingPlan.Count;
    public int PendingInstructionCount => _cookingInstruction.Count;
    public int RemovedRecipeCount => _removedCookingPlan.Count;

    // Add the recipe to the catalogue, and only add if the recipe doesnt already exist

    public bool AddRecipe(Recipe recipe)
    {
        if (recipe == null || recipe.Id <= 0 || string.IsNullOrWhiteSpace(recipe.Title) || _recipesCatalogue.ContainsKey(recipe.Id))
        {
            return false;
        }
        _recipesCatalogue.Add(recipe.Id, recipe);
        return true;

    }
    // find recipe in the recipe catalogue
    public Recipe? FindRecipe(int recipeId)
    {
        if (_recipesCatalogue.TryGetValue(recipeId, out Recipe? recipe))
        {
            return recipe;
        }
        else
        {
            return null;
        }
    }

    /***
    it wont remove the recipe if the recipe is still exist in cookingPlan
    if the recipe ID doesnt it return false
    ***/
    public bool RemoveRecipe(int recipeId)
    {
        if (_cookingPlan.Contains(recipeId))
        {
            return false;
        }
        else if (_recipesCatalogue.Remove(recipeId, out Recipe? recipe))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // if the ricipe exist add it to the shopping list then return the amount of items added, if null return 0
    public int AddIngredientsToShoppingList(int recipeId)
    {
        Recipe? recipe = FindRecipe(recipeId);
        int countItem = 0;
        if (recipe != null)
        {
            foreach (var item in recipe.Ingredients)
            {
                _shoppingList.Add(item);
                countItem++;
            }
            return countItem;

        }
        return 0;
    }

    // show items in the shopping list 
    public IReadOnlyList<string> GetShoppingList()
    {
        return _shoppingList;
    }

    // empty shopping list
    public void ClearShoppingList()
    {
        _shoppingList.Clear();
    }

    /*  
    Add recipe to cooking plan return false is the recipe is
    null or already in the cooking. If not , add to the list add
     */
    public bool AddRecipeToCookingPlan(int recipeId)
    {
        Recipe? recipe = FindRecipe(recipeId);
        if (recipe == null || _cookingPlan.Contains(recipeId))
        {
            return false;
        }
        _cookingPlan.AddLast(recipe.Id);
        return true;
    }

    /* 
    remove recipe from cooking plan then added it to _removedCookingPlan()
    if it not in the cooking plan then do nth. 
    */
    public bool RemoveRecipeFromCookingPlan(int recipeId)
    {
        Recipe? recipe = FindRecipe(recipeId);  

        if (recipe != null && _cookingPlan.Contains(recipeId))
        {   
            _cookingPlan.Remove(recipeId);
            _removedCookingPlan.Push(recipeId);
            _removedCookingPlan.Count();
            return true;
        }
        return false;  
    }

    public bool RestoreLastRemovedRecipe()
    {   // inspect the removed-recipe stack 
        if(RemovedRecipeCount == 0) 
        return false;
    
        //  restore the most recently removed recipe
        var recipe = _removedCookingPlan.Pop();
        if(_recipesCatalogue.ContainsKey(recipe) && !_cookingPlan.Contains(recipe))
        {
            _cookingPlan.AddLast(recipe);
            return true;
        }
        return false;

    }
    // Return the top recipe ID without removing it, or null when the stack is empty.
    public int? PeekLastRemovedRecipe()
    {
        if(RemovedRecipeCount != 0)
        {
            var peekedPlan = _removedCookingPlan.Peek();
            return peekedPlan;
        }

        return null;   
    }

    // .ToList() converts LinkedList into a List, and it implements IReadOnlyList<int>
    public IReadOnlyList<int> GetCookingPlan()
    {
        return _cookingPlan.ToList();
    }

    // add the instruction to Queue<str> _cookingInstruction but clear the queue before loaded
    public bool StartCooking(int recipeId)
    {   
        Recipe? recipe = FindRecipe(recipeId);
        
        if(recipe?.Instructions.Count >= 1 && recipe != null)
        {
            _cookingInstruction.Clear();
            foreach(var instruc in recipe.Instructions)
            {
                _cookingInstruction.Enqueue(instruc); 
            }
            return true; 
        }    
        return false;
        
    }

    public string? PeekNextInstruction() =>
        throw new NotImplementedException("Part A: implement PeekNextInstruction.");

    public string? CompleteNextInstruction() =>
        throw new NotImplementedException("Part A: implement CompleteNextInstruction.");

    public IReadOnlyList<Recipe> SearchByTitle(string searchText) =>
        throw new NotImplementedException("Part B: implement SearchByTitle.");

    public IReadOnlyList<Recipe> SearchByIngredient(string searchText) =>
        throw new NotImplementedException("Part B: implement SearchByIngredient.");

    public IReadOnlyList<Recipe> GetHighestProteinRecipes(int count) =>
        throw new NotImplementedException("Part B: implement GetHighestProteinRecipes.");

    public bool AddSavedRecipe(int recipeId) =>
        throw new NotImplementedException("Part B: implement AddSavedRecipe.");

    public bool RemoveSavedRecipe(int recipeId) =>
        throw new NotImplementedException("Part B: implement RemoveSavedRecipe.");

    public bool IsRecipeSaved(int recipeId) =>
        throw new NotImplementedException("Part B: implement IsRecipeSaved.");

    public IReadOnlyList<int> GetSavedRecipes() =>
        throw new NotImplementedException("Part B: implement GetSavedRecipes.");
}

