using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;
public class CategoryManager:MonoBehaviour
{
    public static string CurrentCategory;
    [SerializeField] private FirebaseProvider firebaseProvider;
    [SerializeField] private UserProgress userProgress;
    public Dictionary<string, int> CategoryCounters { get; set; }
    public async void CategoryProgressInitialize()
    {
        if (!await firebaseProvider.TryGetCategoryProgress(userProgress.DeviceId))
        {
            CategoryCounters = new Dictionary<string, int>();
        }
    }
    public void CategoryManagerInitialize()
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
    }
    public void SetCategoryCounter(string categoryName,int counter)
    {
        InitializeCategoryCounter(categoryName);
        CategoryCounters[categoryName] = counter;
    }
    public int GetCategoryCounter(string categoryName)
    {
        InitializeCategoryCounter(categoryName);
        return CategoryCounters[categoryName];
    }
    private void InitializeCategoryCounter(string categoryName)
    {
        if (!CategoryCounters.ContainsKey(CurrentCategory))
        {
            CategoryCounters.Add(categoryName, 0);
        }
    }
}