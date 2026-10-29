# Recipe Management System — Part A and Part B

This repository contains the Recipe Management system developd for COMP2021 Software Development Practice. This project uses C# collections, LINQ and xunit testinf to manage recipe, shopping list, cooking plan and saved recipes. 

## What is supplied

- `RecipeManagement.Core/Models/` — recipe model classes
- `RecipeManagement.Core/RecipeLoader.cs` — reads `data/recipes.json`
- `RecipeManagement.Core/IRecipeManager.cs` — public API
- `RecipeManagement.Application/` — console menu (options labelled PartA / PartB)
- `RecipeManagement.Tests/` — example tests
- `data/recipes.json` — recipe dataset

## What I implement

**Part A** — `RecipeManager.cs` using:

- `Dictionary<int, Recipe>` stores and get recipes by IDs
- `List<string>` manages shopping list ingredients
- `LinkedList<int>` manages the cooking plan
- `Stack<int>` stores removed cooking-plan recipe for restoration
- `Queue<string>` process cooking instructions in order

**Part B** - Extended the existing RecipeManager with:
-SearchByTitle(): searching recipes by partial title using LINQ, ignoring ignoring letter case
-SearchByIngredient(): searching recipes by ingredient using LINQ, ignoring ignoring letter case
-GetHighestProtienRecipe(): return recipes ordered by protien content, highest first
-AddSavedRecipe(): add an existing recips ID to the saved-recipe list
-RemoveSavedRecipe(): remove recipe Id from saved list
-IsRecipeSaved(): check whether a recipe is saved
-GetSavedRecipe(): return the saved recipe Ids.

Additional xUnit test cover the Part B methods and an intergrated workflow

See Design.md for the collection choice, intergation explanation, and basic time complexity analysis

## Build and run

Open `StudentPackage/RecipeManagement.sln`:

```bash
dotnet build
dotnet test
dotnet run --project RecipeManagement.Application -- data/recipes.json
```
## AI acknowledgement

Include the required AI acknowledgement statement in your submission as described in the assignment specification.


I used Chatgpt to help me understand C# collections, nullable-reference warnings, unit-test behaviour, LINQ method and interpreting the assignment requirements. I did not copy or adapt AI-generated code or other material into my submission. I developed the submitted solution
myself based on my understanding of the course material.