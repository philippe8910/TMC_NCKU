using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public enum Difficulty
{
    Easy,
    Medium,
    Hard
}

[System.Serializable]
public class LevelSettings
{
    public string levelName;
    public float timeLimit;
    public Difficulty difficulty;
    public bool skipTutorial;
    public string recipeFile; // 指定該關卡使用的配方 JSON 檔案
}

[System.Serializable]
public class LevelConfig
{
    public List<LevelSettings> levels;
}

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    
    
    public int currentLevelIndex = 0;
    public int maxLevelIndex = 0;
    
    [SerializeField] private LevelConfig levelData;

    private void Awake()
    {
        Instance = this;
    }
    
    private void Start()
    {
        LoadLevelSettings();
        InitializatioData();
    }

    void LoadLevelSettings()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("LevelSettings"); // Resources/LevelSettings.json
        
        if (jsonFile != null)
        {
            levelData = JsonUtility.FromJson<LevelConfig>(jsonFile.text);
            Debug.Log("成功載入關卡設定文件！");
        }
        else
        {
            Debug.LogError("找不到 JSON 關卡設定文件！");
        }
    }

    void InitializatioData()
    {
        var saveLevelIndex = PlayerPrefs.GetInt("LevelIndex");
        
        if (saveLevelIndex == 0)
        {
            currentLevelIndex = 1;
            
            PlayerPrefs.SetInt("LevelIndex", currentLevelIndex);
            Debug.Log("沒有找到存檔，從第一關開始");
        }
        else
        {
            currentLevelIndex = saveLevelIndex;
            Debug.Log("讀取存檔，從第 " + currentLevelIndex + " 關開始");
        }
        
        maxLevelIndex = levelData.levels.Count; 
        
        LoadLevelRecipes(currentLevelIndex - 1);
    }
    
    void FinishLevel()
    {
        currentLevelIndex++;

        if (currentLevelIndex > maxLevelIndex)
        {
            currentLevelIndex = 0;
        }
        
        PlayerPrefs.SetInt("LevelIndex", currentLevelIndex);
    }
    
    [ContextMenu("EarseSaveData")]
    public void EarseSaveData()
    {
        PlayerPrefs.DeleteKey("LevelIndex");
    }

    public LevelSettings GetLevelSettings(int levelIndex)
    {
        if (levelData != null && levelIndex >= 0 && levelIndex < levelData.levels.Count)
        {
            return levelData.levels[levelIndex];
        }
        return null;
    }

    public void LoadLevelRecipes(int levelIndex)
    {
        LevelSettings level = GetLevelSettings(levelIndex);
        
        if (level != null)
        {
            RecipeManager.Instance.LoadRecipeFile(level.recipeFile);
        }
    }
}