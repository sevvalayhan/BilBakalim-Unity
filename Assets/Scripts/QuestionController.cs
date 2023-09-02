using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class QuestionController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI questionTitleText;
    [SerializeField] private TextMeshProUGUI scoreTitleText;
    [SerializeField] private TextMeshProUGUI[] answerTexts;
    [SerializeField] private FirebaseQuestionProvider questionProvider;
    [SerializeField] private CategoryManager categoryManager;
    [SerializeField] private PanelController panelController;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private TimeController timeController;
    [SerializeField] private Animator quizAnimator;
    public static int counter;
    public static int score;
    public static float loadingTime = 1f;
    [SerializeField] private List<AnswerButton> answerButtons;
    public const string CATEGORY_PROGRESS = "categoryProgress";
    public void Initialize(string categoryName)
    {
        counter = categoryManager.GetCategoryCounter(categoryManager.CurrentCategory);
        score = 0;        
        SetAnswerButtonData();
        panelController.SetPanelActive(panelController.QuestionPanel);
        GameManager.IsGameOver = false;
        GameManager.IsGameActive = true;
    }
    public void SetAnswerButtonData()
    {
        Debug.Log(counter);
        AudioManager.Instance.Play(AudioManager.Instance.NextQuestionSoundEffect);
        questionTitleText.text = questionProvider.LoadedQuestions[counter].QuestionText;
        for (int i = 0; i < answerButtons.Count; i++)
        {
            answerButtons[i].SetAnswer(i, questionProvider.LoadedQuestions[counter].Answers[i]);
        }
    }
    public void CheckAnswer(AnswerButton clickedButton)
    {
        if (questionProvider.LoadedQuestions[counter].RightAnswerIndex == clickedButton.AnswerIndex)
        {
            counter++;
            score++;
            categoryManager.SetCategoryCounter(categoryManager.CurrentCategory,counter);
            StartCoroutine(clickedButton.ChangeButtonColorAnimation(Color.green));
            AudioManager.Instance.Play(AudioManager.Instance.TrueEffectSource);
            if (counter == questionProvider.LoadedQuestions.Count)
            {
                OnGameOver();
            }
            else
            {
                StartCoroutine(NextQuestion());
                if (PlayerPrefs.GetInt(panelController.PLAYER_HIGHT_SCORE) < score)
                {
                    PlayerPrefs.SetInt(panelController.PLAYER_HIGHT_SCORE, score);
                }
            }
        }
        else
        {
            Debug.Log("False");
            StartCoroutine(clickedButton.ChangeButtonColorAnimation(Color.red));
            AudioManager.Instance.Play(AudioManager.Instance.FalseEffectSource);
            StartCoroutine(NextQuestion());
        }
    }
    public void OnGameOver()
    {
        Debug.Log("Oyun Bitti");
        AudioManager.Instance.Play(AudioManager.Instance.TimeOverEffectSource);
        GameManager.IsGameOver = true;
        GameManager.IsGameActive = false;
        panelController.SetPanelActive(panelController.ResultPanel);
        scoreTitleText.text = score.ToString() + " doðru cevap verdiniz.";
    }
    public void RepeatGame()
    {
        GameManager.IsGameOver = false;
        GameManager.IsGameActive = true;
        counter = 0;
        score = 0;
        panelController.SetPanelActive(panelController.QuestionPanel);
        timeController.ResetTime();
    }      
    public IEnumerator NextQuestion()
    {
        quizAnimator.SetTrigger("IsExitAnimActive");//SetExitAnimActive 
        yield return new WaitForSeconds(loadingTime);
        quizAnimator.SetTrigger("IsLoadAnimationActive");
        SetAnswerButtonData();
    }
}