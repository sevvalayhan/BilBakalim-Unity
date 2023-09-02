using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public class QuestionAttacherWindow : EditorWindow
{
    FirebaseQuestionProvider questionProvider;
    private string categoryName;
    private string questionText;
    private string answer1;
    private string answer2;
    private string answer3;
    private string answer4;
    private int rightAnswerIndex;
    private int questionIndex;
    [MenuItem("Window/QuestionAttacher")]
    public static void ShowWindow()
    {
        GetWindow<QuestionAttacherWindow>("Question Attacher");
    }
    private void OnEnable()
    {
        questionProvider = FirebaseQuestionProvider.Instance;   
    }
    void OnGUI()
    {
        questionProvider = FirebaseQuestionProvider.Instance;
        GUILayout.Label("Kategori adlarý: science, cografya, art, philosophy,music", EditorStyles.boldLabel);
        questionProvider = EditorGUILayout.ObjectField("Script Reference", questionProvider, typeof(FirebaseQuestionProvider), false) as FirebaseQuestionProvider;
        categoryName = EditorGUILayout.TextField("Kategori Adý: ", categoryName);
        questionText = EditorGUILayout.TextField("Soru metni: ", questionText);
        answer1 = EditorGUILayout.TextField("answers[0]: ", answer1);
        answer2 = EditorGUILayout.TextField("answers[1]: ", answer2);
        answer3 = EditorGUILayout.TextField("answers[2]: ", answer3);
        answer4 = EditorGUILayout.TextField("answers[3]: ", answer4);
        rightAnswerIndex = EditorGUILayout.IntField("rightAnswerIndex: ", rightAnswerIndex);
        questionIndex = EditorGUILayout.IntField("QuestionIndex:", questionIndex);
        var answerList = new List<string>() { answer1, answer2, answer3, answer4 };
        Question newQuestion = new Question(answerList, questionText, rightAnswerIndex);
        if (GUILayout.Button("Kategoriye Yeni Soru Kaydet veya Güncelle"))
        {
            questionProvider.TrySaveQuestion(newQuestion, categoryName, questionIndex);
        }       
    }
}