using System.Collections.Generic;
using UnityEngine;
public class UserProgress : MonoBehaviour
{
    private User user;
    [SerializeField] private PanelController panelController;
    [SerializeField] private FirebaseProvider firebaseProvider;
    [SerializeField] private CategoryManager categoryManager;
    public string DeviceId;
    private void Start()
    {        
        user = new User();      
        DeviceId = SystemInfo.deviceUniqueIdentifier;        
    }  
    public void SaveUser()
    {
        panelController.SavePlayerButton();
        SaveUserHighScore();
        Debug.Log(user.UserName);
    }
    void SaveUserHighScore()
    {
        user.HighScore = PlayerPrefs.GetInt(PanelController.PLAYER_HIGH_SCORE);
        user.CategoryProgress = categoryManager.CategoryCounters;
        user.UserName=PlayerPrefs.GetString(PanelController.PLAYER_PREF_NAME);
        firebaseProvider.TrySaveUser(DeviceId, user);
    }
    private void OnDestroy()
    {
        SaveUserHighScore();
    }
}