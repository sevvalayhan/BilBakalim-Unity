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
    public async Task<bool> TryCheckDeviceId(string deviceId)
    {
        var userSnapShot = await reference.Child("UserData").Child("DeviceId").Child(deviceId).GetValueAsync();
        if (!userSnapShot.Exists)
        {
            Debug.Log("DeviceId not found");
            return false;
        }
        return true;
    }
    public async void TrySaveUser(User user)
    {
        string userJson = JsonConvert.SerializeObject(user);
        await reference.Child("UserData").Child("DeviceId").Child(user.DeviceId).SetRawJsonValueAsync(userJson);
    }   
}