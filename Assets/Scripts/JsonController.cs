using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public class JsonController : MonoBehaviour
{
    Question question;
    void Start()
    {
        List<string> answers = new List<string>() { "1", "2", "3", "4" };
        question = new Question( answers,"soru", 2);    
    }
    public void SaveJson()
    {
        string jsonString = JsonConvert.SerializeObject(question);        
        File.WriteAllText(Application.dataPath + "/Saves/sciene"+".json", jsonString);
    }
}
