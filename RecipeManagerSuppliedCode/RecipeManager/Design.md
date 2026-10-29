# Design

Complete this document for **Part B**.

## 1. Saved recipe collection

Name the C# collection you selected for the saved/favourite feature and explain why it suits add, remove, duplicate prevention and membership checks.

-> I use List<int> for saved-recipe feature to store the recipeIDs that the users want to save. I chose this selection because it simply and easy to use also it provide built in methods for add, remove and check elements.

-AddSavedREcipe(): check whether the recipe exists in the catalogue and if its ID already saved to the list before. This method prevent duplicate save.

-RemovedSavedRecipe(): removed an IDs in the saved recipe from saved list without delete the oroginal recipe from the catalogue.

-IsRecipeSaved(): use Contains() to check whether the recipeIDs already saved in the saved recipe list. It become less effient as the list grows to ust List<int> and checking the exissting ID requies going through the list. 

## 2. Integration

In a short paragraph, explain how the new Part B features use the existing Part A `RecipeManager` and recipe catalogue.

-> The _recipeCatalogue dict continues to store recipe using their unique IDs. SearchByTitle() and SearchByIngredient() use LINQ to go through the recipes using case-insensitive partial matching. The GetHighestProteinRecipe() to filter the recipe with available protein info and sorts them from highest to lowest protien content. The saved-recipe feature store recipeIDs separately while still referring to recipes already exist. This allows the new features to work alongside the existing shopping list, cooking plan and cooking instruction.

## 3. Basic complexity

| Operation | Structure | Expected complexity | Reason |
| --- | --- | --- | --- |
| Lookup recipe by ID | Dictionary | Average O(1) | Hash-based key lookup. |
| Traverse cooking plan | LinkedList | O(n) | Each planned recipe may need to be visited. |
| Complete next instruction | Queue | O(1) | The item at the front is removed. |
| LINQ title/ingredient search | Recipe collection | O(n) | Each recipe may need to be inspected. |
| Check whether a recipe is saved | List<int> | O(n) |Contains() checks the list sequentially and may need to inspect every saved recipe ID|
