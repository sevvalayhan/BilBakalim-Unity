using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class CategoryButton : MonoBehaviour
{
    [SerializeField] Button categoryButton;
    [SerializeField] FirebaseQuestionProvider questionProvider;
    [SerializeField] QuestionController questionController;
    [SerializeField] CategoryManager categoryManager;
    public string categoryName;
    private void Awake()
    {
        categoryButton.onClick.AddListener(() => OnCategoryButtonClick(categoryName));
    }
    public async void OnCategoryButtonClick(string categoryName)
    {
        if (questionController != null && categoryName != null)
        {
            if (await questionProvider.TryLoadQuestionsFromCategoryName(categoryName))
            {     
                categoryManager.CurrentCategory = categoryName;
                questionController.Initialize(categoryName);
            }
            else
            {
                Debug.LogError("Could not connect to Firebase!");
            }
        }
        else
        {
            Debug.Log("Question provider is null");
        }
    }
}