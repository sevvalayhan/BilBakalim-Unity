using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class PanelController : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputName;
    [field: SerializeField] public GameObject SavePlayerPanel { get; private set; }
    [field: SerializeField] public GameObject CategoryPanel { get; private set; }
    [field: SerializeField] public GameObject QuestionPanel { get; private set; }
    [field: SerializeField] public GameObject ResultPanel { get; private set; }
    [field: SerializeField] public GameObject MainMenuPanel { get; private set; }
    [SerializeField] private TextMeshProUGUI continueGameText;
    [SerializeField] private TextMeshProUGUI welcomePlayerText;
    [SerializeField] private TextMeshProUGUI totalScoreText;
    [SerializeField] private TextMeshProUGUI resultText;
    [SerializeField] private List<GameObject> allPanels;
    public string PLAYER_PREF_NAME { get; private set; } = "name";
    public string PLAYER_HIGHT_SCORE { get; private set; } = "score";    
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
            totalScoreText.text = "En yüksek puanýnýz " + PlayerPrefs.GetInt(PLAYER_HIGHT_SCORE).ToString();
        }
    }
    public void SavePlayerButton()
    {
        PlayerPrefs.SetString(PLAYER_PREF_NAME, inputName.text);
        PlayerPrefs.SetInt(PLAYER_HIGHT_SCORE, 0);
        PlayerPrefs.Save();
        SetPanelActive(CategoryPanel);
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
}