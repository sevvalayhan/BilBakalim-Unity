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
        DeviceId = SystemInfo.deviceUniqueIdentifier;
        user = new User();        
    }
    public void SaveUser()
    {
        panelController.SavePlayerButton();
        user.HighScore = PlayerPrefs.GetInt(PanelController.PLAYER_HIGH_SCORE);
        user.UserName = PlayerPrefs.GetString(PanelController.PLAYER_PREF_NAME);
        Debug.Log(user.UserName);
        firebaseProvider.TrySaveUser(DeviceId, user);
    }
    private void OnDestroy()
    {
        //SaveUser();//kullanýcýyý siler ve yeni kullanýcý kaydeder. Bu yüzden kapattýk.
    }
}