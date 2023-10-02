using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;
public class CategoryManager:MonoBehaviour
{
    public static string CurrentCategory;
    public Dictionary<string, int> CategoryCounters { get; set; }
    private void Start()
    {
        string progressDictJson = PlayerPrefs.GetString(QuestionController.CATEGORY_PROGRESS);
        if (string.IsNullOrEmpty(progressDictJson))
        {
            CategoryCounters = new Dictionary<string, int>();
        }
        else
        {
            Dictionary<string, int> progressDict = JsonConvert.DeserializeObject<Dictionary<string, int>>(progressDictJson);
            CategoryCounters = progressDict;
        }
    }
    public void OnDestroy()
    {
        SetCategoryCounter(CurrentCategory,QuestionController.counter);
        Debug.Log(QuestionController.counter);
        string json = JsonConvert.SerializeObject(CategoryCounters);
        PlayerPrefs.SetString(QuestionController.CATEGORY_PROGRESS, json);
        PlayerPrefs.Save();
    }
    public void SetCategoryCounter(string categoryName,int counter)
    {
        InitializeCategoryCounter(categoryName);
        CategoryCounters[categoryName] = counter;
    }
    public int GetCategoryCounter(string categoryName)
    {
        Debug.Log(CurrentCategory);
        InitializeCategoryCounter(categoryName);
        return CategoryCounters[categoryName];
    }
    private void InitializeCategoryCounter(string categoryName)
    {
       // Debug.Log(CategoryCounters.ContainsKey(categoryName));
        if (!CategoryCounters.ContainsKey(CurrentCategory))
        {
            CategoryCounters.Add(categoryName, 0);
        }
    }
}