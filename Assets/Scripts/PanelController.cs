using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class PanelController : MonoBehaviour
{
    [field: SerializeField] public TMP_InputField InputName { get; private set; }
    [field: SerializeField] public GameObject SavePlayerPanel { get; private set; }
    [field: SerializeField] public GameObject CategoryPanel { get; private set; }
    [field: SerializeField] public GameObject QuestionPanel { get; private set; }
    [field: SerializeField] public GameObject ResultPanel { get; private set; }
    [field: SerializeField] public GameObject MainMenuPanel { get; private set; }
    [field: SerializeField] public GameObject BackToCategoryPanel { get; private set; }
    [field: SerializeField] public GameObject AppQuitPanel { get; private set; }
    [SerializeField] private TextMeshProUGUI continueGameText;
    [SerializeField] private TextMeshProUGUI welcomePlayerText;
    [SerializeField] private TextMeshProUGUI totalScoreText;
    [SerializeField] private TextMeshProUGUI resultText;
    [SerializeField] private List<GameObject> allPanels;
    public const string PLAYER_PREF_NAME = "name";
    public const string PLAYER_HIGH_SCORE = "score";
    void Start()
    {
        if (string.IsNullOrEmpty(PlayerPrefs.GetString(PLAYER_PREF_NAME, string.Empty)))
        {
            SavePlayerPanel.SetActive(true);
        }
        else
        {
            MainMenuPanel.SetActive(true);
            continueGameText.text = PlayerPrefs.GetString(PLAYER_PREF_NAME) + " Olarak Devam Et";
            welcomePlayerText.text = "Hoþgeldin " + PlayerPrefs.GetString(PLAYER_PREF_NAME);
            totalScoreText.text = "En yüksek puanýnýz " + PlayerPrefs.GetInt(PLAYER_HIGH_SCORE).ToString();
        }
    }    
    public void SavePlayerButton()
    {
        PlayerPrefs.SetString(PLAYER_PREF_NAME, InputName.text);
        PlayerPrefs.SetInt(PLAYER_HIGH_SCORE, 0);
        PlayerPrefs.SetString(QuestionController.CATEGORY_PROGRESS,string.Empty);
        PlayerPrefs.Save();       
        SetPanelActive(CategoryPanel);
        UpdatePanelText();
    }
    public void SetPanelActive(GameObject targetPanel)
    {
        for (int i = 0; i < allPanels.Count; i++)
        {
            if (allPanels[i] == targetPanel)
            {
                allPanels[i].SetActive(true);
            }
            else
            {
                allPanels[i].SetActive(false);
            }
        }
    }
    public void UpdatePanelText()
    {
        continueGameText.text = PlayerPrefs.GetString(PLAYER_PREF_NAME) + " Olarak Devam Et";
        welcomePlayerText.text = "Hoþgeldin " + PlayerPrefs.GetString(PLAYER_PREF_NAME);
        totalScoreText.text = "En yüksek puanýnýz " + PlayerPrefs.GetInt(PLAYER_HIGH_SCORE).ToString();
    }
}