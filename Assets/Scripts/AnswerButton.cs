using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnswerButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI answerText;
    [SerializeField] private Button buttonComponent;
    [SerializeField] private QuestionController questionController;
    public int AnswerIndex { get; private set; }
    void Start()
    {
        buttonComponent.onClick.AddListener(OnButtonClicked);
    }
    public void OnButtonClicked()
    {
        questionController.CheckAnswer(this);
        AudioManager.Instance.Play(AudioManager.Instance.ButtonClickClip);
    }
    public void SetAnswer(int answerIndex, string answerText)
    {
        this.AnswerIndex = answerIndex;
        this.answerText.text = answerText;
    }
    public IEnumerator ChangeButtonColorAnimation(Color color)
    {
        buttonComponent.image.color = color;
        yield return new WaitForSeconds(QuestionController.loadingTime);        
        buttonComponent.image.color = Color.white;
    }  
}