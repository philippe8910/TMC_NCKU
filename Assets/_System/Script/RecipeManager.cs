using UnityEngine;
using System.Collections.Generic;
using System.IO;

[System.Serializable]
public class Recipe
{
    public List<string> ingredients;
    public string result;
}

[System.Serializable]
public class RecipeList
{
    public List<Recipe> recipes;
}

public class RecipeManager : MonoBehaviour
{
    public static RecipeManager Instance;
    [SerializeField] private RecipeList recipeData;

    private void Awake()
    {
        Instance = this;
    }

    public void LoadRecipeFile(string recipeFileName)
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("Recipes/" + recipeFileName);
        if (jsonFile != null)
        {
            recipeData = JsonUtility.FromJson<RecipeList>(jsonFile.text);
            Debug.Log("成功載入配方: " + recipeFileName);
        }
        else
        {
            Debug.LogError("找不到指定的 Recipe JSON: " + recipeFileName);
        }
    }

    public string GetResult(List<string> ingredientTags)
    {
        if (recipeData == null) return null;

        foreach (var recipe in recipeData.recipes)
        {
            if (new HashSet<string>(recipe.ingredients).SetEquals(ingredientTags))
            {
                return recipe.result;
            }
        }
        return null; // 沒有找到對應配方
    }
}