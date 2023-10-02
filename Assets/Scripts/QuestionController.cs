using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class QuestionController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI questionTitleText;
    [SerializeField] private TextMeshProUGUI scoreTitleText;
    [SerializeField] private TextMeshProUGUI[] answerTexts;
    [SerializeField] private FirebaseProvider questionProvider;
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
    public void CheckAnswer(AnswerButton clickedButton)
    {       
        if (questionProvider.LoadedQuestions[counter].RightAnswerIndex == clickedButton.AnswerIndex)
        {
            score++;
            counter++;            
            categoryManager.SetCategoryCounter(CategoryManager.CurrentCategory,counter);
            StartCoroutine(clickedButton.ChangeButtonColorAnimation(Color.green));
            AudioManager.Instance.Play(AudioManager.Instance.TrueEffectSource);
            panelController.UpdatePanelText();
            if (counter.Equals(questionProvider.LoadedQuestions.Count))
            {
                OnGameOver();
            }
            else
            {
                StartCoroutine(NextQuestion());
                if (PlayerPrefs.GetInt(PanelController.PLAYER_HIGH_SCORE) < score)
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
        
        Debug.Log("PanelController çalýþtý");
    }
    public void OnGameOver()
    { 
        GameManager.IsGameOver = true;
        GameManager.IsGameActive = false;   
        panelController.SetPanelActive(panelController.ResultPanel);
        scoreTitleText.text = score.ToString() + " doðru cevap verdiniz.";
        Debug.Log("Oyun Bitti");
        AudioManager.Instance.Play(AudioManager.Instance.TimeOverEffectSource);     
        timeController.ResetTime();
    }
    public void RepeatGame()
    {
        counter++;
        GameManager.IsGameOver = false;
        GameManager.IsGameActive = true;        
        score = 0;
        panelController.SetPanelActive(panelController.QuestionPanel);
        timeController.ResetTime();
    }      
    public IEnumerator NextQuestion()
    {
        quizAnimator.SetTrigger("IsExitAnimActive"); 
        yield return new WaitForSeconds(loadingTime);        
        quizAnimator.SetTrigger("IsLoadAnimationActive");
        SetButtonData();
    }
}