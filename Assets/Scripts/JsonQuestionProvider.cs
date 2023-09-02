using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using System.Threading.Tasks;
public class JsonQuestionProvider : MonoBehaviour, IQuestionProvider
{
    public List<Question> LoadedQuestions { get; set; }
    public async Task<bool> TryLoadQuestionsFromCategoryName(string categoryName)
    {
        string path = Path.Combine(Application.dataPath, "QuestionJsons", categoryName + ".json");
        Debug.Log(path);
        if (File.Exists(path))
        {
            string jsonFile = await File.ReadAllTextAsync(path);
            LoadedQuestions = JsonConvert.DeserializeObject<List<Question>>(jsonFile);
            return true;
        }
        else
        {
            LoadedQuestions = null;
            return false;
        }
    }
}
