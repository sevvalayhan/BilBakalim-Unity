using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
public class QuizController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI questionTitleText;
    [SerializeField] private TextMeshProUGUI scoreTitleText;
    [SerializeField] private TextMeshProUGUI[] answerTexts;
    [SerializeField] private FirebaseProvider questionProvider;
    [SerializeField] private CategoryManager categoryManager;
    [SerializeField] private PanelController panelController;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private TimeController timeController;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Animator quizAnimator;
    public static int counter;
    public static int score;
    public static float loadingTime = 1f;
    [SerializeField] private List<AnswerButton> answerButtons;
    public const string CATEGORY_PROGRESS = "categoryProgress";
    public void Initialize()
    {        
        counter = categoryManager.GetCategoryCounter(CategoryManager.CurrentCategory);
        score = 0;        
        SetButtonData();
        panelController.SetPanelActive(panelController.QuestionPanel);        
        GameManager.IsGameOver = false;
        GameManager.IsGameActive = true;
    }
    public void SetButtonData()
    {                
        questionTitleText.text = questionProvider.LoadedQuestions[counter].QuestionText;
        for (int i = 0; i < answerButtons.Count; i++)
        {
            answerButtons[i].SetAnswer(i, questionProvider.LoadedQuestions[counter].Answers[i]);
        }
        AudioManager.Instance.Play(AudioManager.Instance.NextQuestionSoundEffect);
    }
    public void CheckAnswer2(AnswerButton clickedButton)
    {    
         
        if (questionProvider.LoadedQuestions[counter].RightAnswerIndex == clickedButton.AnswerIndex)
        {                      
            SaveCategoryProgress(counter);
            StartCoroutine(clickedButton.ChangeButtonColorAnimation(Color.green));
            AudioManager.Instance.Play(AudioManager.Instance.TrueEffectSource);     
            if (counter.Equals(questionProvider.LoadedQuestions.Count))
            {
                EndGame();
            }
            else
            {
                StartCoroutine(NextQuestion());
                score++;                
                counter++;
                if (PlayerPrefs.GetInt(PanelController.PLAYER_HIGH_SCORE) <= score)
                {
                    PlayerPrefs.SetInt(PanelController.PLAYER_HIGH_SCORE, score);
                }
               
            }
        }
        else
        { 
            Debug.Log("False");
            StartCoroutine(clickedButton.ChangeButtonColorAnimation(Color.red));
            AudioManager.Instance.Play(AudioManager.Instance.FalseEffectSource);
            StartCoroutine(answerButtons[questionProvider.LoadedQuestions[counter].RightAnswerIndex].ChangeButtonColorAnimation(Color.green));
            StartCoroutine(NextQuestion());
            counter++;
        }        
        panelController.UpdatePanelText();  
    }
    public void CheckAnswer(AnswerButton clickedButton)
    {
        panelController.UpdatePanelText();
        if (!counter.Equals(questionProvider.LoadedQuestions.Count))
        {
            if (questionProvider.LoadedQuestions[counter].RightAnswerIndex == clickedButton.AnswerIndex)
            {
                TrueAnswerEffect(clickedButton);
                score++;      
                if (PlayerPrefs.GetInt(PanelController.PLAYER_HIGH_SCORE) <= score)
                {
                    PlayerPrefs.SetInt(PanelController.PLAYER_HIGH_SCORE, score);
                }
            }
            else
            {
                FalseAnswerEffect(clickedButton);
            }
            counter++;
            if (!counter.Equals(questionProvider.LoadedQuestions.Count))
            {
                StartCoroutine(NextQuestion());
            }
            else
            {
                EndGame();
            }
        }        
        panelController.UpdatePanelText();        
        SaveCategoryProgress(counter);
    }
    public void TrueAnswerEffect(AnswerButton clickedButton)
    {        
        StartCoroutine(clickedButton.ChangeButtonColorAnimation(Color.green));
        AudioManager.Instance.Play(AudioManager.Instance.TrueEffectSource);
    }
    public void FalseAnswerEffect(AnswerButton clickedButton)
    {
        StartCoroutine(clickedButton.ChangeButtonColorAnimation(Color.red));
        AudioManager.Instance.Play(AudioManager.Instance.FalseEffectSource);
        StartCoroutine(answerButtons[questionProvider.LoadedQuestions[counter].RightAnswerIndex].ChangeButtonColorAnimation(Color.green));
    }
    public void OnGameOver()
    {
        counter++;
        GameManager.IsGameOver = true;
        GameManager.IsGameActive = false;   
        panelController.SetPanelActive(panelController.ResultPanel);
        panelController.UpdatePanelText();
        scoreTitleText.text = score.ToString() + " doðru cevap verdiniz.";
        AudioManager.Instance.Play(AudioManager.Instance.TimeOverEffectSource);
        timeController.ResetTime(); 
    }
    public void EndGame()
    {
        counter = 0;
        timeController.ResetTime();
        panelController.SetPanelActive(panelController.EndGamePanel);
        panelController.UpdatePanelText();
        GameManager.IsGameOver = true; 
        GameManager.IsGameActive= false;
    }
    public void SaveCategoryProgress(int _counter)
    {
        categoryManager.SetCategoryCounter(CategoryManager.CurrentCategory, _counter);
        string categoryProgress = JsonConvert.SerializeObject(categoryManager.CategoryCounters);
        PlayerPrefs.SetString(CATEGORY_PROGRESS, categoryProgress);
    }
    public void RepeatGame()
    {
               
        score = 0;
        panelController.SetPanelActive(panelController.QuestionPanel);
        SaveCategoryProgress(counter);
        timeController.ResetTime();
        GameManager.IsGameOver = false;
        GameManager.IsGameActive = true; 
    }      
    public IEnumerator NextQuestion()
    {
        quizAnimator.SetTrigger("IsExitAnimActive"); 
        yield return new WaitForSeconds(loadingTime);        
        quizAnimator.SetTrigger("IsLoadAnimationActive");
        SetButtonData();
    }
    private void OnDestroy()
    {
        SaveCategoryProgress(counter);
    }
}