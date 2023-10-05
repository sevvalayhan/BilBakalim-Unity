using UnityEngine;
using UnityEngine.UI;
public class CategoryButton : MonoBehaviour
{
    [SerializeField] Button categoryButton;
    [SerializeField] FirebaseProvider questionProvider;
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
                CategoryManager.CurrentCategory = categoryName;
                categoryManager.CategoryManagerInitialize();
                questionController.Initialize();
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