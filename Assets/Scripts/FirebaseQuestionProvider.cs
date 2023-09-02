using Firebase.Database;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using Firebase;
using System;
using Newtonsoft.Json;
public class FirebaseQuestionProvider : MonoBehaviour, IQuestionProvider
{
    private static FirebaseQuestionProvider instance;

    public static FirebaseQuestionProvider Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<FirebaseQuestionProvider>();
            }
            return instance;
        }
    }
    private DatabaseReference reference;
    public List<Question> LoadedQuestions { get; set; }
    private void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            reference = FirebaseDatabase.DefaultInstance.RootReference;
        });
    }
    public async Task<bool> TryLoadQuestionsFromCategoryName(string categoryName)
    {
        try
        {
            var categorySnapshot = await reference.Child("categories").Child(categoryName).GetValueAsync();
            if (!categorySnapshot.Exists)
            {
                Debug.LogError("Error loading questions: Category snapshot does not exist.");                
            }      
            string questionJson = categorySnapshot.GetRawJsonValue();
            Debug.Log(questionJson);
            LoadedQuestions = JsonConvert.DeserializeObject<List<Question>>(questionJson);
            LoadedQuestions.RemoveAll((question) => question == null);
            return true;
        }
        catch (Exception ex)
        {
            Debug.LogError("Error loading questions: " + ex);
            return false;
        }
    }
    public async void TrySaveQuestion(Question newQuestion,string categoryName,int questionIndex)
    {
        try
        {
            string jsonString = JsonConvert.SerializeObject(newQuestion);
            await reference.Child("categories").Child(categoryName).Child(questionIndex.ToString()).SetRawJsonValueAsync(jsonString);
        }
        catch (Exception ex)
        {
            Debug.LogError("Error saving questions:" + ex);
            throw;
        }
    }
}