using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using Firebase;
using Firebase.Database;
using System;
using Newtonsoft.Json;
public class FirebaseProvider : MonoBehaviour, IQuestionProvider
{
    private static FirebaseProvider instance;
    public static FirebaseProvider Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<FirebaseProvider>();
            }
            Debug.Log(instance.ToString());
            return instance;
        }
    }   
    public List<Question> LoadedQuestions { get; set; }
    [SerializeField] CategoryManager categoryManager;
    private DatabaseReference reference;
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
    public async void TrySaveQuestion(Question newQuestion, string categoryName, int questionIndex)
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
    public async Task<bool> TryGetCategoryProgress(string DeviceId)
    {
        try
        {
            var userCategoryProgressSnapshot = await reference.Child("UserData").Child("DeviceId").Child(DeviceId).Child("CategoryProgress").GetValueAsync();
            if (!userCategoryProgressSnapshot.Exists)
            {
                Debug.Log("Error loading questions: CategoryProgress snapshot does not exist.");
            }
            string categoryProgressJson = userCategoryProgressSnapshot.GetRawJsonValue();
            Dictionary<string,int> progressDict = JsonConvert.DeserializeObject<Dictionary<string, int>>(categoryProgressJson);
            categoryManager.CategoryCounters = progressDict;      
            return true;
        }
        catch (Exception ex)
        {
            Debug.LogError("Error: " + ex);
            return false;
        }         
    }
    public async void TrySaveUser(string DeviceId, User user)
    {
        string userJson = JsonConvert.SerializeObject(user);
        await reference.Child("UserData").Child("DeviceId").Child(DeviceId).SetRawJsonValueAsync(userJson);
    }   
}