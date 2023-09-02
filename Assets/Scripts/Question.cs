using System.Collections.Generic;

public class Question
{
    public List<string> Answers { get; private set; }
    public string QuestionText { get; private set; }    
    public int RightAnswerIndex { get; private set; }

    public Question(List<string> answers, string questionText,int rightAnswerIndex)
    {        
        this.Answers = answers;
        this.QuestionText = questionText; 
        this.RightAnswerIndex = rightAnswerIndex;
    }
}