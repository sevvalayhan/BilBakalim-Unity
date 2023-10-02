using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
public class JsonController : MonoBehaviour
{   
    void Start()
    {
        List<string> answers = new List<string>() { "1", "2", "3", "4" };
        var question = new Question( answers,"soru", 2);    
    }
    public void SaveJson(Question question)
    {
        string jsonString = JsonConvert.SerializeObject(question);        
        File.WriteAllText(Application.dataPath + "/Saves/sciene"+".json", jsonString);
    }
}