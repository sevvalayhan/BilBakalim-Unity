using TMPro;
using UnityEngine;
public class TimeController : MonoBehaviour
{
    private float currentTime;
    [SerializeField] GameManager gameManager;
    [field:SerializeField] public float DefaultTime { get; private set; }
    [SerializeField] TextMeshProUGUI timeText;
    void Start()
    {
        currentTime = 60f;
        ResetTime();
    }
    public void Update()
    {
        if (GameManager.IsGameActive)
        {
            currentTime -= Time.deltaTime;
            timeText.text = Mathf.FloorToInt(currentTime).ToString();
            if (currentTime < 0)
            {               
                gameManager.OnGameOver();
            }            
        }
    }
    public void ResetTime()
    {
        currentTime = DefaultTime;
    }
}