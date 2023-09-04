using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
public class CategoryManager : MonoBehaviour
{
    public string CurrentCategory;
    public Dictionary<string, int> CategoryCounters { get;private set; }
    private void Start()
    {
        string progressDictJson = PlayerPrefs.GetString(QuestionController.CATEGORY_PROGRESS,string.Empty);
        if (!string.IsNullOrEmpty(progressDictJson))
        {            
            Dictionary<string,int> progressDict = JsonConvert.DeserializeObject<Dictionary<string,int>>(progressDictJson);
            CategoryCounters = progressDict;
        }
        else
        {
            CategoryCounters = new Dictionary<string, int>();
        }
        Debug.Log(CategoryCounters);
    }
    public void OnDestroy()
    {
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
        InitializeCategoryCounter(categoryName);
        return CategoryCounters[categoryName];
    }
    private void InitializeCategoryCounter(string categoryName)
    {
        Debug.Log(CategoryCounters);
        if (!CategoryCounters.ContainsKey(categoryName))
        {
            CategoryCounters.Add(categoryName, 0);            
        }
    }
}