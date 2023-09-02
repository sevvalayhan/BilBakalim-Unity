using System.Collections.Generic;
using System.Threading.Tasks;
public interface IQuestionProvider
{
    List<Question> LoadedQuestions { get; set; }
    public  Task<bool> TryLoadQuestionsFromCategoryName(string categoryName);
}