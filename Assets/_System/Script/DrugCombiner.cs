using UnityEngine;
using System.Collections.Generic;

public class DrugCombiner : MonoBehaviour
{
    public List<GameObject> currentList = new List<GameObject>(); // 放入的藥品物件
    public Transform spawnPoint; // 合成藥品生成的位置

    public void TryCombine()
    {
        List<string> ingredientTags = new List<string>();

        // 取得當前所有物件的tag
        foreach (GameObject item in currentList)
        {
            DrugTag tagScript = item.GetComponent<DrugTag>();
            if (tagScript != null)
            {
                ingredientTags.Add(tagScript.drugName);
            }
        }

        // 透過 RecipeManager 檢查合成結果
        string result = RecipeManager.Instance.GetResult(ingredientTags);
        if (result != null)
        {
            Debug.Log("合成成功！結果是：" + result);
            SpawnNewDrug(result);
            ClearCurrentList();
        }
        else
        {
            Debug.Log("合成失敗，沒有對應的配方！");
        }
    }

    void SpawnNewDrug(string resultDrug)
    {
        GameObject newDrug = Resources.Load<GameObject>("Drugs/" + resultDrug);
        if (newDrug != null)
        {
            Instantiate(newDrug, spawnPoint.position, Quaternion.identity);
        }
        else
        {
            Debug.LogError("找不到合成結果的Prefab：" + resultDrug);
        }
    }

    void ClearCurrentList()
    {
        foreach (var obj in currentList)
        {
            Destroy(obj); // 刪除舊的物件
        }
        currentList.Clear();
    }
}